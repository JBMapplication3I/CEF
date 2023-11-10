﻿#if !NETSTANDARD1_6
// Most of this class is sourced from the MONO project in the existing file:
//
// https://github.com/mono/mono/blob/master/mcs/class/System.Web/System.Web/HttpRequest.cs
//
// 
// Author:
//	Miguel de Icaza (miguel@novell.com)
//	Gonzalo Paniagua Javier (gonzalo@novell.com)
//      Marek Habersack <mhabersack@novell.com>
//

//
// Copyright (C) 2005-2010 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using ServiceStack.Text;

namespace ServiceStack.Host.HttpListener
{
    public partial class ListenerRequest
    {
        static internal string GetParameter(string header, string attr)
        {
            var ap = header.IndexOf(attr);
            if (ap == -1)
            {
                return null;
            }

            ap += attr.Length;
            if (ap >= header.Length)
            {
                return null;
            }

            var ending = header[ap];
            if (ending != '"')
            {
                ending = ' ';
            }

            var end = header.IndexOf(ending, ap + 1);
            if (end == -1)
            {
                return ending == '"' ? null : header.Substring(ap);
            }

            return header.Substring(ap + 1, end - ap - 1);
        }

        private void LoadMultiPart()
        {
            var boundary = GetParameter(ContentType, "; boundary=");
            if (boundary == null)
            {
                return;
            }

            var input = GetSubStream(InputStream);

            //DB: 30/01/11 - Hack to get around non-seekable stream and received HTTP request
            //Not ending with \r\n?
            var ms = MemoryStreamFactory.GetStream(32 * 1024);
            input.CopyTo(ms);
            input = ms;
            ms.WriteByte((byte)'\r');
            ms.WriteByte((byte)'\n');

            input.Position = 0;

            //Uncomment to debug
            //var content = new StreamReader(ms).ReadToEnd();
            //Console.WriteLine(boundary + "::" + content);
            //input.Position = 0;

            var multi_part = new HttpMultipart(input, boundary, ContentEncoding);

            HttpMultipart.Element e;
            while ((e = multi_part.ReadNextElement()) != null)
            {
                if (e.Filename == null)
                {
                    var copy = new byte[e.Length];

                    input.Position = e.Start;
                    input.Read(copy, 0, (int)e.Length);

                    form.Add(e.Name, (e.Encoding ?? ContentEncoding).GetString(copy));
                }
                else
                {
                    //
                    // We use a substream, as in 2.x we will support large uploads streamed to disk,
                    //
                    var sub = new HttpPostedFile(e.Filename, e.ContentType, input, e.Start, e.Length);
                    files.AddFile(e.Name, sub);
                }
            }
            EndSubStream(input);
        }

        public NameValueCollection Form
        {
            get
            {
                if (form == null)
                {
                    form = new();
                    files = new();

                    if (IsContentType("multipart/form-data", true))
                    {
                        LoadMultiPart();
                    }
                    else if (
                        IsContentType("application/x-www-form-urlencoded", true))
                    {
                        LoadWwwForm();
                    }

                    form.Protect();
                }

#if NET_4_0
				if (validateRequestNewMode && !checked_form) {
					// Setting this before calling the validator prevents
					// possible endless recursion
					checked_form = true;
					ValidateNameValueCollection ("Form", query_string_nvc, RequestValidationSource.Form);
				} else
#endif
                if (validate_form && !checked_form)
                {
                    checked_form = true;
                    ValidateNameValueCollection("Form", form);
                }

                return form;
            }
        }


        protected bool validate_cookies, validate_query_string, validate_form;
        protected bool checked_cookies, checked_query_string, checked_form;

        private static void ThrowValidationException(string name, string key, string value)
        {
            var v = "\"" + value + "\"";
            if (v.Length > 20)
            {
                v = v.Substring(0, 16) + "...\"";
            }

            var msg = string.Format("A potentially dangerous Request.{0} value was " +
                            "detected from the client ({1}={2}).", name, key, v);

            throw new HttpRequestValidationException(msg);
        }

        private static void ValidateNameValueCollection(string name, NameValueCollection coll)
        {
            if (coll == null)
            {
                return;
            }

            foreach (string key in coll.Keys)
            {
                var val = coll[key];
                if (val?.Length > 0 && IsInvalidString(val))
                {
                    ThrowValidationException(name, key, val);
                }
            }
        }

        internal static bool IsInvalidString(string val)
        {

            return IsInvalidString(val, out var validationFailureIndex);
        }

        internal static bool IsInvalidString(string val, out int validationFailureIndex)
        {
            validationFailureIndex = 0;

            var len = val.Length;
            if (len < 2)
            {
                return false;
            }

            var current = val[0];
            for (var idx = 1; idx < len; idx++)
            {
                var next = val[idx];
                // See http://secunia.com/advisories/14325
                if (current == '<' || current == '\xff1c')
                {
                    if (next == '!' || next < ' '
                        || (next >= 'a' && next <= 'z')
                        || (next >= 'A' && next <= 'Z'))
                    {
                        validationFailureIndex = idx - 1;
                        return true;
                    }
                }
                else if (current == '&' && next == '#')
                {
                    validationFailureIndex = idx - 1;
                    return true;
                }

                current = next;
            }

            return false;
        }

        public void ValidateInput()
        {
            validate_cookies = true;
            validate_query_string = true;
            validate_form = true;
        }

        private bool IsContentType(string ct, bool starts_with)
        {
            if (ct == null || ContentType == null)
            {
                return false;
            }

            if (starts_with)
            {
                return StrUtils.StartsWith(ContentType, ct, true);
            }

            return string.Compare(ContentType, ct, true, Helpers.InvariantCulture) == 0;
        }

        private void LoadWwwForm()
        {
            using (var input = GetSubStream(InputStream))
            {
                using (var s = new StreamReader(input, ContentEncoding))
                {
                    var key = StringBuilderCache.Allocate();
                    var value = StringBuilderCacheAlt.Allocate();
                    int c;

                    while ((c = s.Read()) != -1)
                    {
                        if (c == '=')
                        {
                            value.Length = 0;
                            while ((c = s.Read()) != -1)
                            {
                                if (c == '&')
                                {
                                    AddRawKeyValue(key, value);
                                    break;
                                }
                                else
                                {
                                    value.Append((char)c);
                                }
                            }
                            if (c == -1)
                            {
                                AddRawKeyValue(key, value);
                                return;
                            }
                        }
                        else if (c == '&')
                        {
                            AddRawKeyValue(key, value);
                        }
                        else
                        {
                            key.Append((char)c);
                        }
                    }
                    if (c == -1)
                    {
                        AddRawKeyValue(key, value);
                    }

                    EndSubStream(input);

                    StringBuilderCache.Free(key);
                    StringBuilderCacheAlt.Free(key);
                }
            }
        }

        private void AddRawKeyValue(StringBuilder key, StringBuilder value)
        {
            var decodedKey = HttpUtility.UrlDecode(key.ToString(), ContentEncoding);
            form.Add(decodedKey,
                  HttpUtility.UrlDecode(value.ToString(), ContentEncoding));

            key.Length = 0;
            value.Length = 0;
        }

        private WebROCollection form;

        private HttpFileCollection files;

        public sealed class HttpFileCollection : NameObjectCollectionBase
        {
            internal HttpFileCollection()
            {
            }

            internal void AddFile(string name, HttpPostedFile file)
            {
                BaseAdd(name, file);
            }

            public void CopyTo(Array dest, int index)
            {
                /* XXX this is kind of gross and inefficient
                 * since it makes a copy of the superclass's
                 * list */
                var values = BaseGetAllValues();
                values.CopyTo(dest, index);
            }

            public string GetKey(int index)
            {
                return BaseGetKey(index);
            }

            public HttpPostedFile Get(int index)
            {
                return (HttpPostedFile)BaseGet(index);
            }

            public HttpPostedFile Get(string key)
            {
                return (HttpPostedFile)BaseGet(key);
            }

            public HttpPostedFile this[string key] => Get(key);

            public HttpPostedFile this[int index] => Get(index);

            public string[] AllKeys => BaseGetAllKeys();
        }

        private class WebROCollection : NameValueCollection
        {
            private bool got_id;
            private int id;

            public bool GotID => got_id;

            public int ID
            {
                get => id;
                set
                {
                    got_id = true;
                    id = value;
                }
            }
            public void Protect()
            {
                IsReadOnly = true;
            }

            public void Unprotect()
            {
                IsReadOnly = false;
            }

            public override string ToString()
            {
                var sb = StringBuilderCache.Allocate();
                foreach (var key in AllKeys)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append('&');
                    }

                    if (key?.Length > 0)
                    {
                        sb.Append(key);
                        sb.Append('=');
                    }
                    sb.Append(Get(key));
                }
                return StringBuilderCache.ReturnAndFree(sb);
            }
        }

        public sealed class HttpPostedFile
        {
            private string name;
            private string content_type;
            private Stream stream;

            private class ReadSubStream : Stream
            {
                private Stream s;
                private long offset;
                private long end;
                private long position;

                public ReadSubStream(Stream s, long offset, long length)
                {
                    this.s = s;
                    this.offset = offset;
                    end = offset + length;
                    position = offset;
                }

                public override void Flush()
                {
                }

                public override int Read(byte[] buffer, int dest_offset, int count)
                {
                    if (buffer == null)
                    {
                        throw new ArgumentNullException(nameof(buffer));
                    }

                    if (dest_offset < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(dest_offset), "< 0");
                    }

                    if (count < 0)
                    {
                        throw new ArgumentOutOfRangeException(nameof(count), "< 0");
                    }

                    var len = buffer.Length;
                    if (dest_offset > len)
                    {
                        throw new ArgumentException("destination offset is beyond array size");
                    }
                    // reordered to avoid possible integer overflow
                    if (dest_offset > len - count)
                    {
                        throw new ArgumentException("Reading would overrun buffer");
                    }

                    if (count > end - position)
                    {
                        count = (int)(end - position);
                    }

                    if (count <= 0)
                    {
                        return 0;
                    }

                    s.Position = position;
                    var result = s.Read(buffer, dest_offset, count);
                    if (result > 0)
                    {
                        position += result;
                    }
                    else
                    {
                        position = end;
                    }

                    return result;
                }

                public override int ReadByte()
                {
                    if (position >= end)
                    {
                        return -1;
                    }

                    s.Position = position;
                    var result = s.ReadByte();
                    if (result < 0)
                    {
                        position = end;
                    }
                    else
                    {
                        position++;
                    }

                    return result;
                }

                public override long Seek(long d, SeekOrigin origin)
                {
                    long real;
                    switch (origin)
                    {
                        case SeekOrigin.Begin:
                            real = offset + d;
                            break;
                        case SeekOrigin.End:
                            real = end + d;
                            break;
                        case SeekOrigin.Current:
                            real = position + d;
                            break;
                        default:
                            throw new ArgumentException();
                    }

                    var virt = real - offset;
                    if (virt < 0 || virt > Length)
                    {
                        throw new ArgumentException();
                    }

                    position = s.Seek(real, SeekOrigin.Begin);
                    return position;
                }

                public override void SetLength(long value)
                {
                    throw new NotSupportedException();
                }

                public override void Write(byte[] buffer, int offset, int count)
                {
                    throw new NotSupportedException();
                }

                public override bool CanRead => true;

                public override bool CanSeek => true;

                public override bool CanWrite => false;

                public override long Length => end - offset;

                public override long Position
                {
                    get => position - offset;
                    set
                    {
                        if (value > Length)
                        {
                            throw new ArgumentOutOfRangeException();
                        }

                        position = Seek(value, SeekOrigin.Begin);
                    }
                }
            }

            internal HttpPostedFile(string name, string content_type, Stream base_stream, long offset, long length)
            {
                this.name = name;
                this.content_type = content_type;
                stream = new ReadSubStream(base_stream, offset, length);
            }

            public string ContentType => content_type;

            public int ContentLength => (int)stream.Length;

            public string FileName => name;

            public Stream InputStream => stream;

            public void SaveAs(string filename)
            {
                var buffer = new byte[16 * 1024];
                var old_post = stream.Position;

                try
                {
                    File.Delete(filename);
                    using (var fs = File.Create(filename))
                    {
                        stream.Position = 0;
                        int n;

                        while ((n = stream.Read(buffer, 0, 16 * 1024)) != 0)
                        {
                            fs.Write(buffer, 0, n);
                        }
                    }
                }
                finally
                {
                    stream.Position = old_post;
                }
            }
        }

        private class Helpers
        {
            public static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;
        }

        internal sealed class StrUtils
        {
            private StrUtils() { }

            public static bool StartsWith(string str1, string str2)
            {
                return StartsWith(str1, str2, false);
            }

            public static bool StartsWith(string str1, string str2, bool ignore_case)
            {
                var l2 = str2.Length;
                if (l2 == 0)
                {
                    return true;
                }

                var l1 = str1.Length;
                if (l2 > l1)
                {
                    return false;
                }

                return 0 == string.Compare(str1, 0, str2, 0, l2, ignore_case, Helpers.InvariantCulture);
            }

            public static bool EndsWith(string str1, string str2)
            {
                return EndsWith(str1, str2, false);
            }

            public static bool EndsWith(string str1, string str2, bool ignore_case)
            {
                var l2 = str2.Length;
                if (l2 == 0)
                {
                    return true;
                }

                var l1 = str1.Length;
                if (l2 > l1)
                {
                    return false;
                }

                return 0 == string.Compare(str1, l1 - l2, str2, 0, l2, ignore_case, Helpers.InvariantCulture);
            }
        }

        private class HttpMultipart
        {

            public class Element
            {
                public string ContentType;
                public string Name;
                public string Filename;
                public Encoding Encoding;
                public long Start;
                public long Length;

                public override string ToString()
                {
                    return "ContentType " + ContentType + ", Name " + Name + ", Filename " + Filename + ", Start " +
                        Start.ToString() + ", Length " + Length.ToString();
                }
            }

            private Stream data;
            private string boundary;
            private byte[] boundary_bytes;
            private byte[] buffer;
            private bool at_eof;
            private Encoding encoding;
            private StringBuilder sb;

            private const byte HYPHEN = (byte)'-', LF = (byte)'\n', CR = (byte)'\r';

            // See RFC 2046 
            // In the case of multipart entities, in which one or more different
            // sets of data are combined in a single body, a "multipart" media type
            // field must appear in the entity's header.  The body must then contain
            // one or more body parts, each preceded by a boundary delimiter line,
            // and the last one followed by a closing boundary delimiter line.
            // After its boundary delimiter line, each body part then consists of a
            // header area, a blank line, and a body area.  Thus a body part is
            // similar to an RFC 822 message in syntax, but different in meaning.

            public HttpMultipart(Stream data, string b, Encoding encoding)
            {
                this.data = data;
                //DB: 30/01/11: cannot set or read the Position in HttpListener in Win.NET
                //var ms = new MemoryStream(32 * 1024);
                //data.CopyTo(ms);
                //this.data = ms;

                boundary = b;
                boundary_bytes = encoding.GetBytes(b);
                buffer = new byte[boundary_bytes.Length + 2]; // CRLF or '--'
                this.encoding = encoding;
                sb = new();
            }

            private string ReadLine()
            {
                // CRLF or LF are ok as line endings.
                var got_cr = false;
                var b = 0;
                sb.Length = 0;
                while (true)
                {
                    b = data.ReadByte();
                    if (b == -1)
                    {
                        return null;
                    }

                    if (b == LF)
                    {
                        break;
                    }
                    got_cr = b == CR;
                    sb.Append((char)b);
                }

                if (got_cr)
                {
                    sb.Length--;
                }

                return sb.ToString();
            }

            private static string GetContentDispositionAttribute(string l, string name)
            {
                int idx = l.IndexOf(name + "=\""), begin, end;
                if (idx < 0)
                {
                    idx = l.IndexOf(name + "=");
                    if (idx < 0)
                    {
                        return null;
                    }

                    begin = idx + name.Length + "=".Length;
                    end = l.IndexOf(';', begin);
                    if (end < 0)
                    {
                        end = l.Length;
                    }
                }
                else
                {
                    begin = idx + name.Length + "=\"".Length;
                    end = l.IndexOf('"', begin);
                    if (end < 0)
                    {
                        return null;
                    }
                }
                return begin == end ? "" : l.Substring(begin, end - begin);
            }

            private string GetContentDispositionAttributeWithEncoding(string l, string name)
            {
                var temp = GetContentDispositionAttribute(l, name);
                if (string.IsNullOrEmpty(temp))
                {
                    return temp;
                }

                var source = new byte[temp.Length];
                for (var i = temp.Length - 1; i >= 0; i--)
                {
                    source[i] = (byte)temp[i];
                }

                return encoding.GetString(source);
            }

            private bool ReadBoundary()
            {
                try
                {
                    var line = ReadLine();
                    while (line == "")
                    {
                        line = ReadLine();
                    }

                    if (line[0] != '-' || line[1] != '-')
                    {
                        return false;
                    }

                    if (!StrUtils.EndsWith(line, boundary, false))
                    {
                        return true;
                    }
                }
                catch
                {
                }

                return false;
            }

            private string ReadHeaders()
            {
                var s = ReadLine();
                if (s == "")
                {
                    return null;
                }

                return s;
            }

            private bool CompareBytes(byte[] orig, byte[] other)
            {
                for (var i = orig.Length - 1; i >= 0; i--)
                {
                    if (orig[i] != other[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            private long MoveToNextBoundary()
            {
                long retval = 0;
                var got_cr = false;

                var state = 0;
                var c = data.ReadByte();
                while (true)
                {
                    if (c == -1)
                    {
                        return -1;
                    }

                    if (state == 0 && c == LF)
                    {
                        retval = data.Position - 1;
                        if (got_cr)
                        {
                            retval--;
                        }

                        state = 1;
                        c = data.ReadByte();
                    }
                    else if (state == 0)
                    {
                        got_cr = c == CR;
                        c = data.ReadByte();
                    }
                    else if (state == 1 && c == '-')
                    {
                        c = data.ReadByte();
                        if (c == -1)
                        {
                            return -1;
                        }

                        if (c != '-')
                        {
                            state = 0;
                            got_cr = false;
                            continue; // no ReadByte() here
                        }

                        var nread = data.Read(buffer, 0, buffer.Length);
                        var bl = buffer.Length;
                        if (nread != bl)
                        {
                            return -1;
                        }

                        if (!CompareBytes(boundary_bytes, buffer))
                        {
                            state = 0;
                            data.Position = retval + 2;
                            if (got_cr)
                            {
                                data.Position++;
                                got_cr = false;
                            }
                            c = data.ReadByte();
                            continue;
                        }

                        if (buffer[bl - 2] == '-' && buffer[bl - 1] == '-')
                        {
                            at_eof = true;
                        }
                        else if (buffer[bl - 2] != CR || buffer[bl - 1] != LF)
                        {
                            state = 0;
                            data.Position = retval + 2;
                            if (got_cr)
                            {
                                data.Position++;
                                got_cr = false;
                            }
                            c = data.ReadByte();
                            continue;
                        }
                        data.Position = retval + 2;
                        if (got_cr)
                        {
                            data.Position++;
                        }

                        break;
                    }
                    else
                    {
                        // state == 1
                        state = 0; // no ReadByte() here
                    }
                }

                return retval;
            }

            public Element ReadNextElement()
            {
                if (at_eof || ReadBoundary())
                {
                    return null;
                }

                var elem = new Element();
                string header;
                while ((header = ReadHeaders()) != null)
                {
                    if (StrUtils.StartsWith(header, "Content-Disposition:", true))
                    {
                        elem.Name = GetContentDispositionAttribute(header, "name");
                        elem.Filename = StripPath(GetContentDispositionAttributeWithEncoding(header, "filename"));
                    }
                    else if (StrUtils.StartsWith(header, "Content-Type:", true))
                    {
                        elem.ContentType = header.Substring("Content-Type:".Length).Trim();
                        elem.Encoding = GetEncoding(elem.ContentType);
                    }
                }

                long start = 0;
                start = data.Position;
                elem.Start = start;
                var pos = MoveToNextBoundary();
                if (pos == -1)
                {
                    return null;
                }

                elem.Length = pos - start;
                return elem;
            }

            private static string StripPath(string path)
            {
                if (path == null || path.Length == 0)
                {
                    return path;
                }

                if (path.IndexOf(":\\") != 1 && !path.StartsWith("\\\\"))
                {
                    return path;
                }

                return path.Substring(path.LastIndexOf('\\') + 1);
            }
        }

    }
}
#endif
