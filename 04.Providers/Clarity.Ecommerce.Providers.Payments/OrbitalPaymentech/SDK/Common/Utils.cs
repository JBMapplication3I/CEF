using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Common
{
    /// <summary>
    /// This class contains a collection of static utility methods
    /// that can be used by multiple components to perform common
    /// tasks.
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Determines if the specified path is an absolute path or is a
        /// relative path. The two criteria that it checks is to verify that
        /// the second character contains a colon, or verifies that the
        /// first two characters are forward slashes to denote a UNC path.
        /// </summary>
        /// <param name="path">The path to test.</param>
        /// <returns>true if the supplied path is an absolute path, false
        /// if it is relative.</returns>
        public static bool IsAbsolutePath(string path)
        {
            if (path.Length > 1 && path[1] == ':')
            {
                return true;
            }

            if (path.StartsWith(@"\\"))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines if the specified text represents a number.
        /// </summary>
        /// <param name="text">The text to be tested.</param>
        /// <returns>True if the text is fully numeric, false if it is not.</returns>
        public static bool IsNumeric(string text)
        {
            var objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(text);
        }

        /// <summary>
        /// Takes a string with the values of either "true" or "false" and returns the appropriate
        /// boolean value. The method will return a value of false if the supplied string is anything
        /// other than "true", including null. This method is not case sensitive.
        /// </summary>
        /// <param name="boolString">The string that contains either "true" or "false"</param>
        /// <returns>The boolean representation of the string value.</returns>
        static public bool StringToBoolean(string boolString)
        {
            return boolString != null && boolString.ToLower().Trim() == "true";
        }

        /// <summary>
        /// Returns the text value of the node specified by the supplied XPath
        /// string. It will get the first node that meets the requirements of
        /// the XPath string and will return its value. It will return the
        /// value if the client specifies "text()" in the XPath or not.
        /// </summary>
        /// <param name="document">The XmlDocument to search for the text.</param>
        /// <param name="xPath">The XPath expression used to find the value.</param>
        /// <param name="nsMgr">Used to support namespaces</param>
        /// <returns>The inner text value of the specified node, or null if not found.</returns>
        public static string GetXPathValue(XmlDocument document, string xPath, XmlNamespaceManager nsMgr)
        {
            var node = nsMgr == null
                ? document.DocumentElement.SelectSingleNode(xPath)
                : document.DocumentElement.SelectSingleNode(xPath, nsMgr);
            return node?.InnerText;
        }

        /// <summary>
        /// Alternative for:
        /// public static string GetXPathValue(
        /// XmlDocument document, string xPath,
        /// XmlNamespaceManager nsMgr )
        /// </summary>
        /// <param name="document"></param>
        /// <param name="xPath"></param>
        /// <returns></returns>
        public static string GetXPathValue(XmlDocument document, string xPath)
        {
            return GetXPathValue(document, xPath, null);
        }

        /// <summary>
        /// Searches only the children (non-recursively) for the first child
        /// whose NodeType is of the one specified.
        /// </summary>
        /// <param name="node">The node whose children are to be searched.</param>
        /// <param name="type">The node type to search for.</param>
        /// <returns>The first XmlNode found of the specified type.</returns>
        public static XmlNode GetChildNodeOfType(XmlNode node, XmlNodeType type)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType.Equals(type))
                {
                    return child;
                }
            }

            return null;
        }

        /// <summary>
        /// Searches for the data element and returns it to the caller.
        /// </summary>
        /// <remarks>
        /// The data element is the element that contains the request's fields.
        /// </remarks>
        /// <param name="doc">The document to search for the element in.</param>
        /// <returns>An XmlNode that refers to the element, or null if it was not found.</returns>
        public static XmlNode GetDataElement(XmlDocument doc)
        {
            var request = doc.DocumentElement.SelectSingleNode("/Request");
            if (request == null)
            {
                return doc.DocumentElement;
            }
            foreach (XmlNode child in request.ChildNodes)
            {
                if (child.NodeType.Equals(XmlNodeType.Element) && child.Name != "TemplateInclude")
                {
                    return child;
                }
            }
            return null;
        }

        /// <summary>
        /// This function converts a string into an interger based on the OS
        /// If the input is null or not a valid value ,  0 is returned
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int StringToInt(string data)
        {
            return StringToInt(data, 0);
        }

        /// <summary>
        /// Converts the given string to an integer without worrying about
        /// the OS. It will return the given default value if the string
        /// failed to parse.
        /// </summary>
        /// <param name="data">The string to convert.</param>
        /// <param name="defValue">The value to return on an error.</param>
        /// <returns>An integer representation of the given string, or the default value on error.</returns>
        public static int StringToInt(string data, int defValue)
        {
            return data == null
                ? defValue
                : !int.TryParse(data, out var value)
                    ? defValue
                    : value;
        }

        /// <summary>
        /// Returns a list of nodes in document that match the criteria of
        /// the supplied XPath string.
        /// </summary>
        /// <param name="document">The XmlDocument to search.</param>
        /// <param name="xpath">The XPath string that defines what to search for.</param>
        /// <returns>An XmlNodeList of nodes that met the XPath criteria.</returns>
        static public XmlNodeList GetDOMNodeList(XmlDocument document, string xpath)
        {
            var nodeList = document.SelectNodes(xpath);

            return nodeList == null || nodeList.Count == 0 ? null : nodeList;
        }

        /// <summary>
        /// Return the node list which matches the xpath from a  XPath XML document
        /// </summary>
        /// <param name="document">The XmlDocument to search.</param>
        /// <param name="xpath">The XPath string that defines what to search for.</param>
        /// <returns></returns>
        static public XPathNodeIterator GetXPathNodes(XPathDocument document, string xpath)
        {
            var nav = document.CreateNavigator();
            var iter = nav.Select(xpath);
            if (iter.Count == 0)
            {
                return null;
            }
            iter.MoveNext();
            return iter;
        }

        /// <summary>
        /// Return a  node value from a XPath XML document
        /// </summary>
        /// <param name="document">The XmlDocument to search.</param>
        /// <param name="xPath">The XPath string that defines what to search for.</param>
        /// <returns></returns>
        public static string GetXPathValue(XPathDocument document, string xPath)
        {
            var nav = document.CreateNavigator();
            var iter = nav.Select(xPath);
            if (iter.Count == 0)
            {
                return null;
            }
            iter.MoveNext();
            return iter.Current.Value;
        }

        /// <summary>
        /// Gets the text node of the specified node.
        /// </summary>
        /// <remarks>
        /// If the node does not
        /// have a immediate child that is a text node, it will not dig
        /// deeply, but will instead just return null.
        /// </remarks>
        /// <param name="node">The node whose text element to return.</param>
        /// <returns>An XmlNode referencing the text node, or null if none was found.</returns>
        public static XmlNode GetTextNode(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType.Equals(XmlNodeType.Text))
                {
                    return child;
                }
            }
            return null;
        }

        /// <summary>
        /// Replaces the specified range of characters in a string with another string.
        /// </summary>
        /// <param name="sourceString">The string to be modified</param>
        /// <param name="start">The starting index.</param>
        /// <param name="end">The ending index.</param>
        /// <param name="replaceWith">The string to put in its place.</param>
        /// <returns>The modified string.</returns>
        public static string Replace(string sourceString, int start, int end, string replaceWith)
        {
            var endLength = end - start;

            sourceString = sourceString.Remove(start, endLength);

            return sourceString.Insert(start, replaceWith);
        }

        /// <summary>
        /// Replaces the specified range of characters in a string with another string.
        /// </summary>
        /// <param name="sourceString">The StringBuffer to be modified</param>
        /// <param name="start">The starting index.</param>
        /// <param name="end">The ending index.</param>
        /// <param name="replaceWith">The string to put in its place.</param>
        /// <returns>The modified string.</returns>
        public static StringBuilder Replace(StringBuilder sourceString, int start, int end, string replaceWith)
        {
            var endLength = end - start;
            sourceString.Remove(start, endLength);
            sourceString.Insert(start, replaceWith);
            return sourceString;
        }

        /// <summary>
        /// Converts a string to a hexadecimal character.
        /// </summary>
        /// <remarks>
        /// <para>Takes a hexadecimal representation that is stored in a string and
        /// converts it to an actual hex character.
        /// </para>
        ///
        /// <para>
        /// The string representation that is passed in as value takes the form
        /// "[1C]", where 1C is the hex number.
        /// </para>
        ///
        /// </remarks>
        /// <param name="value">The string value to convert.</param>
        /// <returns></returns>
        public static string StringToHexChar(string value)
        {
            if (value.StartsWith("["))
            {
                value = value.Substring(1);
            }
            if (value.EndsWith("]"))
            {
                value = value.Substring(0, value.Length - 1);
            }
            var str = new StringBuilder();
            str.Append((char)Convert.ToInt32(value, 16));
            return str.ToString();
        }

        /// This method repeatedly adds a string of bytes at intervals in a byte array and
        /// returns the new array with the added bytes.
        /// @param srcBytes - read-only array of bytes that is the original array to add to
        /// @param skipLen - number of bytes to skip in srcBytes between adding the addBytes
        /// @param addBytes - bytes to add to the array at skipLen intervals
        /// @return - new array with the repeated addBytes added.
        public static byte[] InsertBytes(byte[] srcBytes, int skipLen, byte[] addBytes)
        {
            // calculate how much extra space will be needed for "addBytes"
            var extraLen = addBytes.Length / (float)skipLen;
            // calculate size of destination buffer
            var destsize = (int)(srcBytes.Length * (1 + extraLen));
            // set up the destination buffer
            var retBytes = new byte[destsize];
            var srcBytesLen = srcBytes.Length;
            var addBytesLen = addBytes.Length;
            var retBytesLen = retBytes.Length;
            // Now add bytes after each skipBytes length in the srcBytes
            for (int i = 0, j = 0; i < srcBytesLen && j < retBytesLen;)
            {
                // first copy skipLen number of bytes from the srcBytes
                for (var k = 0; k < skipLen; k++)
                {
                    if (j >= retBytesLen || i >= srcBytesLen)
                    {
                        break;
                    }
                    retBytes[j++] = srcBytes[i++];
                }
                // now copy in the addBytes
                for (var x = 0; x < addBytesLen; x++)
                {
                    if (j >= retBytesLen)
                    {
                        break;
                    }
                    retBytes[j++] = addBytes[x];
                }
            }
            return retBytes;
        }

        /// <summary>
        ///  Convert an array of bytes to a hex string representation
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string ByteArrayToHex(byte[] array)
        {
            var retVal = new StringBuilder();
            foreach (var t in array)
            {
                // first get the high 4 bits
                var highNibble = (byte)(t & 0xf0);
                // now shift them down to the low 4 bits
                highNibble >>= 4;
                // mask off sign extension just in case
                highNibble &= 0xf;
                // low nibble is easy, just mask off the high nibble
                var lowNibble = (byte)(t & 0xf);
                // high digit is always first (convention)
                retVal.Append(ValueToHexChar(highNibble));
                retVal.Append(ValueToHexChar(lowNibble));
            }
            return retVal.ToString();
        }

        /// <summary>
        ///  Convert a 4 bit value (nibble) into a hex character
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static char ValueToHexChar(byte val)
        {
            char retVal;
            // brute force but clear
            switch (val)
            {
                case 0:
                    retVal = '0';
                    break;
                case 1:
                    retVal = '1';
                    break;
                case 2:
                    retVal = '2';
                    break;
                case 3:
                    retVal = '3';
                    break;
                case 4:
                    retVal = '4';
                    break;
                case 5:
                    retVal = '5';
                    break;
                case 6:
                    retVal = '6';
                    break;
                case 7:
                    retVal = '7';
                    break;
                case 8:
                    retVal = '8';
                    break;
                case 9:
                    retVal = '9';
                    break;
                case 10:
                    retVal = 'A';
                    break;
                case 11:
                    retVal = 'B';
                    break;
                case 12:
                    retVal = 'C';
                    break;
                case 13:
                    retVal = 'D';
                    break;
                case 14:
                    retVal = 'E';
                    break;
                case 15:
                    retVal = 'F';
                    break;
                default:
                    throw new Exception("Value: " + val + " not a hex digit");
            }
            return retVal;
        }

        /// <summary>
        /// Gets the text value of a the Text child node of the supplied XmlNode
        /// object.
        /// </summary>
        /// <param name="node">The node whose text value is to be returned.</param>
        /// <returns></returns>
        public static string GetNodeValue(XmlNode node)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType.Equals(XmlNodeType.Text))
                {
                    return child.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// Convert a XML object to string for logging
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static string ConvertXMLToString(XmlDocument document)
        {
            // Create StringWriter object to get data from xml document.
            var stringWriter = new StringWriter();
            var xmlWriter = new XmlTextWriter(stringWriter) { Formatting = Formatting.Indented };
            document.WriteTo(xmlWriter);
            return stringWriter.ToString();
        }

        /// <summary>
        /// Returns the value of the given attribute in the specified node.
        /// It will return the default value if the attribute does not exist.
        /// </summary>
        /// <param name="name">The name of the attribute to find.</param>
        /// <param name="defValue">The default value.</param>
        /// <param name="node">The node containing the attribute.</param>
        /// <returns>A string containing the attribute's value, or the default value on error.</returns>
        public static string GetAttributeValue(string name, string defValue, XmlNode node)
        {
            if (node.Attributes[name] != null)
            {
                return node.Attributes[name].Value;
            }
            return defValue;
        }

        public static bool GetBoolAttributeValue(string name, bool defValue, XmlNode node)
        {
            if (node.Attributes[name] != null)
            {
                return node.Attributes[name].Value.ToLower() == "true";
            }
            return defValue;
        }

        /// <summary>This method automates loading data fields for an object from a DOM Document.</summary>
        /// <remarks>It loads the values for elements in the specified base XPath whose names are the same as the fields'
        /// names except that the first character is matched regardless of case.
        /// This method only supports fields of type int, string, or bool.</remarks>
        /// <param name="obj">      The object whose methods are to be called.</param>
        /// <param name="baseXPath">The XPath to the element that contains the field elements.</param>
        /// <param name="doc">      The DOM Document that contains the XML data.</param>
        /// <param name="nameSpace">.</param>
        /// <param name="nsMgr">    .</param>
        /// <param name="binding">  .</param>
        public static void FillDataClassWithDefaults(
            object obj,
            string baseXPath,
            XmlDocument doc,
            string nameSpace,
            XmlNamespaceManager nsMgr,
            BindingFlags binding)
        {
            var type = obj.GetType();
            do  // do for this derived class and all it's base classes
            {
                var props = type.GetProperties(binding);
                var bname = new StringBuilder(baseXPath);
                // Build the XPath string to the fields.
                if (nameSpace != null)
                {
                    bname.Append(nameSpace);
                    if (!nameSpace.EndsWith(":"))
                    {
                        bname.Append(":");
                    }
                }
                else
                {
                    var testStr = bname.ToString();
                    if (!testStr.EndsWith("/"))
                    {
                        bname.Append("/");
                    }
                }
                var xPathBase = bname.ToString();
                foreach (var prop in props)
                {
                    if (!(prop.PropertyType == typeof(string)
                          || prop.PropertyType == typeof(int)
                          || prop.PropertyType == typeof(bool)
                          || prop.PropertyType == typeof(long)
                          || prop.PropertyType == typeof(ulong)
                          || prop.PropertyType == typeof(uint)))
                    {
                        continue;
                    }
                    var xPath = xPathBase + prop.Name + "/text()";
                    var val = nsMgr == null ? GetXPathValue(doc, xPath) : GetXPathValue(doc, xPath, nsMgr);
                    if (val == null || val.Length <= 0)
                    {
                        continue;
                    }
                    if (!prop.CanWrite)
                    {
                        throw new ApplicationException("Could not load element: " + xPath +
                            " with value: " + val +
                            " because Property: " + prop.Name + " has no set() method");
                    }
                    if (prop.PropertyType == typeof(int))
                    {
                        try
                        {
                            var intVal = int.Parse(val);
                            prop.SetValue(obj, intVal, null);
                        }
                        catch
                        {
                            // Do Nothing
                        }
                    }
                    else if (prop.PropertyType == typeof(uint))
                    {
                        try
                        {
                            var intVal = (uint)int.Parse(val);
                            prop.SetValue(obj, intVal, null);
                        }
                        catch
                        {
                            // Do Nothing
                        }
                    }
                    else if (prop.PropertyType == typeof(long))
                    {
                        try
                        {
                            var longVal = long.Parse(val);
                            prop.SetValue(obj, longVal, null);
                        }
                        catch
                        {
                            // Do Nothing
                        }
                    }
                    else if (prop.PropertyType == typeof(ulong))
                    {
                        try
                        {
                            var longVal = (ulong)long.Parse(val);
                            prop.SetValue(obj, longVal, null);
                        }
                        catch
                        {
                            // Do Nothing
                        }
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        var clean = val.Trim().ToLower();
                        if (clean == "true")
                        {
                            prop.SetValue(obj, true, null);
                        }
                        else if (clean == "false")
                        {
                            prop.SetValue(obj, false, null);
                        }
                    }
                    else
                    {
                        prop.SetValue(obj, val, null);
                    }
                }
                type = type.BaseType;
                if (!type.FullName.StartsWith("ChasePaymentech"))
                {
                    type = null;
                }
            } while (type != null);
        }

        /// <summary>
        /// Alternative for:
        /// public static void FillDataClassWithDefaults(
        ///	object obj, string baseXPath, XmlDocument doc,
        ///	string nameSpace, XmlNamespaceManager nsMgr,
        ///	BindingFlags binding)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="baseXPath"></param>
        /// <param name="doc"></param>
        /// <param name="nameSpace"></param>
        /// <param name="binding"></param>
        public static void FillDataClassWithDefaults(
            object obj,
            string baseXPath,
            XmlDocument doc,
            string nameSpace,
            BindingFlags binding)
        {
            FillDataClassWithDefaults(obj, baseXPath, doc, nameSpace, null, binding);
        }

        /// <summary>
        /// Alternative for:
        /// public static void FillDataClassWithDefaults(
        ///	object obj, string baseXPath, XmlDocument doc,
        ///	string nameSpace, XmlNamespaceManager nsMgr,
        ///	BindingFlags binding)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="baseXPath"></param>
        /// <param name="doc"></param>
        /// <param name="nameSpace"></param>
        /// <param name="nsMgr"></param>
        public static void FillDataClassWithDefaults(
            object obj,
            string baseXPath,
            XmlDocument doc,
            string nameSpace,
            XmlNamespaceManager nsMgr)
        {
            FillDataClassWithDefaults(obj, baseXPath, doc, nameSpace, nsMgr, BindingFlags.NonPublic
                | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance
                | BindingFlags.FlattenHierarchy);
        }

        /// <summary>
        /// Alternative for:
        /// public static void FillDataClassWithDefaults(
        ///	object obj, string baseXPath, XmlDocument doc,
        ///	string nameSpace, XmlNamespaceManager nsMgr,
        ///	BindingFlags binding)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="baseXPath"></param>
        /// <param name="doc"></param>
        /// <param name="nameSpace"></param>
        public static void FillDataClassWithDefaults(
            object obj,
            string baseXPath,
            XmlDocument doc,
            string nameSpace)
        {
            FillDataClassWithDefaults(obj, baseXPath, doc, nameSpace, null, BindingFlags.NonPublic
                | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance
                | BindingFlags.FlattenHierarchy);
        }

        /// <summary>
        /// Converts the specified string into an array of bytes.
        /// </summary>
        /// <param name="str">The string to convert.</param>
        /// <returns>An array of bytes created from the string.</returns>
        public static byte[] StringToByteArray(string str)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// Converts the specified array of bytes to a string.
        /// </summary>
        /// <param name="bytes">The array of bytes to convert.</param>
        /// <returns>A string built from the byte array.</returns>
        public static string ByteArrayToString(byte[] bytes)
        {
            var enc = new ASCIIEncoding();
            return enc.GetString(bytes);
        }

        /// <summary>
        /// Converts a byte array to a string.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteArrayToISOString(byte[] bytes)
        {
            var iso = Encoding.GetEncoding("ISO-8859-1");
            var msg = iso.GetString(bytes);
            return msg;
        }

        /// <summary>
        /// This method will convert a "native" endian integer value
        /// to a "network order" (big-endian) array of 4 bytes
        /// </summary>
        /// <param name="value">32 bit (4 byte) integer</param>
        /// <returns>array of 4 bytes.</returns>
        public static byte[] IntToBigEndianBytes(uint value)
        {
            // get the 4 bytes with the length filled in
            var newbuf = BitConverter.GetBytes(
                value);

            if (newbuf.Length != 4)
            {
                throw new Exception(
                    "UInt32 value " + value +
                    " has more than 4 bytes");
            }

            // Since this is Windows, it should be little endian but
            // let's make sure
            if (BitConverter.IsLittleEndian)
            {
                ReverseBytes(newbuf);
            }

            return newbuf;
        }

        /// <summary>
        /// This method converts a 4 byte array representing a
        /// "network order" 4 byte big-endian integer into a
        /// 4 byte integer in the native endian form for the host
        /// </summary>
        /// <param name="bytes">4 bytes representing big-endian number</param>
        /// <returns>32 bit (4 byte) native integer.</returns>
        public static uint BigEndianBytesToInt(byte[] bytes)
        {
            uint retVal;
            // assume bytes are endian compatible for now
            var newbuf = bytes;
            if (bytes.Length != 4)
            {
                throw new Exception(
                    "An array of " + bytes.Length +
                    " cannot be converted to UInt32");
            }
            // if host is little endian we need to reverse the bytes
            if (BitConverter.IsLittleEndian)
            {
                ReverseBytes(bytes);
            }
            try
            {
                retVal = BitConverter.ToUInt32(bytes, 0);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Could not convert 4 bytes to UInt32", ex);
            }
            return retVal;
        }

        /// <summary>Reverses the byte values in a byte array.</summary>
        public static void ReverseBytes(byte[] bytes)
        {
            var newbuf = new byte[bytes.Length];
            for (int i = 0, j = bytes.Length - 1; i < bytes.Length; i++, j--)
            {
                newbuf[i] = bytes[j];
            }
            // now put reversed bytes back into original array
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = newbuf[i];
            }
        }

        /// <summary>All directory paths must have backslashes instead of forward slashes, and cannot end with a slash.
        /// This converts a badly formatted path and turns it into a good one.</summary>
        /// <param name="path">The path to clean up.</param>
        /// <returns>The properly formatted path.</returns>
        public static string FormatDirectoryPath(string path)
        {
            if (path == null)
            {
                return null;
            }
            var retVal = path.Replace("/", "\\");
            if (retVal.EndsWith("\\"))
            {
                retVal = retVal.Remove(retVal.Length - 1, 1);
            }
            if (retVal.StartsWith("\\\\"))
            {
                return retVal;
            }
            while (retVal.Contains("\\\\"))
            {
                retVal = retVal.Replace("\\\\", "\\");
            }
            return retVal;
        }

        /// <summary>
        /// Convert a number from 0 to 63 ( 6 bits ) into a base 64 letter
        /// </summary>
        /// <param name="ch">Number from 0 to 63</param>
        /// <returns>Base 64 digit.</returns>
        public static int Base64Digit(int ch)
        {
            var retVal = 0;
            if (ch < 0)
            {
                throw new Exception(
                "value " + ch + " out of range");
            }
            if (ch < 26)
            {
                retVal = ch + 'A';
            }
            else if (ch < 52)
            {
                retVal = ch - 26 + 'a';
            }
            else if (ch < 62)
            {
                retVal = ch - 52 + '0';
            }
            else if (ch == 62)
            {
                retVal = '+';
            }
            else if (ch == 63)
            {
                retVal = '/';
            }
            else
            {
                throw new Exception("value " + ch + " out of range");
            }
            return retVal;
        }

        /// <summary>
        /// Generates a filename that the SFTP server will accept.
        /// </summary>
        /// <remarks>
        /// The filename of an batch file being sent to the server via SFTP must
        /// follow a very specific format. This data used to generate a proper
        /// filename can be gotten from the config file. This method will
        /// generate a proper filename and return it. This filename will be unique
        /// and will be named for the specific PID that is configured for the
        /// merchant.
        /// </remarks>
        /// <param name="fileExtension">The file extension</param>
        /// <param name="sftpPid">The merchant's SFTP PID stored in the
        /// ProtocolManager.</param>
        /// <param name="fileName">The name of the batch file (without extension).</param>
        /// <returns>The filename on success, null on failure.</returns>
        public static string GetUniqueFileName(string fileExtension, string sftpPid, string fileName)
        {
            var dt = DateTime.Now;
            if (sftpPid == null || fileName == null || fileExtension == null || !Regex.IsMatch(sftpPid, "[0-9]*"))
            {
                return null;
            }
            var remoteFileName = new StringBuilder();
            remoteFileName.Append(ValidateSFTPFileName(sftpPid));
            remoteFileName.Append(".SDK");
            remoteFileName.Append(ValidateSFTPFileName(fileName));
            remoteFileName.Append(dt.ToString("ddhhmmsss"));
            remoteFileName.Append(".");
            remoteFileName.Append(ValidateSFTPFileName(fileExtension));
            return remoteFileName.Length > 31 ? null : remoteFileName.ToString();
        }

        /// <summary>
        /// Verifies that the given filename conforms to the SFTP filename standard.
        /// </summary>
        /// <param name="fileName">The filename to validate.</param>
        /// <returns>An adjusted filename or the same filename if it is already fine.</returns>
        public static string ValidateSFTPFileName(string fileName)
        {
            return Regex.Replace(fileName, "[^0-9A-Za-z\\-_]", "_");
        }

        /// <summary>
        /// Retun the string of hex representation
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static string HexAsciiToString(string hex)
        {
            try
            {
                var temp = new StringBuilder();
                if (hex.Length % 2 != 0)
                {
                    return null;
                }

                for (var idx = 0; idx <= hex.Length - 2; idx += 2)
                {
                    temp.Append(Convert.ToString(Convert.ToChar(int.Parse(hex.Substring(idx, 2), System.Globalization.NumberStyles.HexNumber))));
                }
                if (temp.Length > 0)
                {
                    return temp.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Pads the source string with the supplied character to the specified max length.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="padWith"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string PadLeft(string source, char padWith, int length)
        {
            var safeSource = "";
            if (source != null)
            {
                safeSource = source;
            }
            var buffer = new StringBuilder(safeSource);
            for (var i = safeSource.Length; i < length; i++)
            {
                buffer.Insert(0, padWith);
            }
            return buffer.ToString();
        }

        /// <summary>
        /// Pads the source string with the supplied character to the specified max length.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="padWith"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string PadRight(string source, char padWith, int length)
        {
            var safeSource = "";
            StringBuilder buffer;
            if (source != null)
            {
                safeSource = source;
            }
            buffer = new StringBuilder(safeSource);
            for (var i = safeSource.Length; i < length; i++)
            {
                buffer.Append(padWith);
            }
            return buffer.ToString();
        }

        /// <summary>
        /// Returns the XmlDocument of a file that is stored as an embedded resource,
        /// but can alternately be stored in the file system. It will first try to
        /// load the file from the specified directory path, and failing that,
        /// will load it from the resource.
        /// </summary>
        /// <param name="resourcePath">The path to the embedded resource.</param>
        /// <param name="filePath">The path to the file in the file system.
        /// This is a path relative to MSDK_HOME.</param>
        /// <returns>The XmlDocument of the resource file.</returns>
        public static XmlDocument GetResourceDocument(string resourcePath, string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            var fileName = Path.GetFileName(filePath);
            var absolutePath = GetFilePath(fileName, directory, null);
            if (absolutePath == null)
            {
                return GetEmbeddedDocument(resourcePath);
            }
            var document = new XmlDocument();
            document.Load(absolutePath);
            return document;
        }

        /// <summary>
        /// Gets an XmlDocument from an embedded resource.
        /// </summary>
        /// <remarks>
        /// The resourcePath must be the full namespace path to the file, including the filename,
        /// such as:
        ///
        /// JPMC.MSDK.Common.RuleEngine.xml
        /// </remarks>
        /// <param name="resourcePath">The path (including namespace) to the
        /// resource to be loaded.</param>
        /// <returns>An XmlDocument containing the XML that was loaded, and null
        /// if the resource could not be loaded.</returns>
        public static XmlDocument GetEmbeddedDocument(string resourcePath)
        {
            var asm = Assembly.GetExecutingAssembly();
            var xmlStream = asm.GetManifestResourceStream(resourcePath);
            if (xmlStream == null)
            {
                return null;
            }
            var doc = new XmlDocument { PreserveWhitespace = true };
            doc.Load(xmlStream);
            xmlStream.Close();
            return doc;
        }

        /// <summary>
        /// Searches for the specified file in the given subdirectory.
        /// It uses a planned search path to find the file.
        /// </summary>
        /// <remarks>
        /// This is a static version of FindFilePath.
        /// </remarks>
        /// <param name="fileName"></param>
        /// <param name="directory"></param>
        /// <param name="homeDir"></param>
        /// <returns></returns>
        public static string GetFilePath(string fileName, string directory, string homeDir)
        {
            Exception exp = null;
            var fname = fileName;
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }
            if (directory != null)
            {
                fname = Path.Combine(directory, fileName);
            }
            string path;
            if (homeDir != null)
            {
                path = Path.Combine(homeDir, fname);
                if (File.Exists(path))
                {
                    return path;
                }
            }
            try
            {
                if ((path = GetFileFromEnvironmentVariable(fname)) != null)
                {
                    return path;
                }
            }
            catch (Exception e)
            {
                exp = e;
            }
            if (IsAbsolutePath(fileName) && File.Exists(fileName))
            {
                return fileName;
            }
            if (File.Exists(fname))
            {
                var file = new FileInfo(fname);
                return file.FullName;
            }
            try
            {
                if ((path = GetFileFromPathVariable(fname)) != null)
                {
                    return path;
                }
            }
            catch (Exception e)
            {
                exp = e;
            }
            if (exp != null)
            {
                throw exp;
            }
            return null;
        }

        private static string ExtractDirectoryFromPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) { return string.Empty; }
            var uri = new UriBuilder(path);
            path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path) ?? string.Empty;
        }

        /// <summary>
        /// Look for a file in MSDK_HOME directory
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GetFileFromEnvironmentVariable(string fileName)
        {
            try
            {
                var path = Environment.GetEnvironmentVariable("MSDK_HOME");
                if (path != null)
                {
                    path = Path.Combine(path, fileName);
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }
            }
            catch (System.Security.SecurityException)
            {
                throw;
            }
            return null;
        }

        /// <summary>
        /// Look for a file in all the directories in the PATH
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GetFileFromPathVariable(string fileName)
        {
            try
            {
                var path = Environment.GetEnvironmentVariable("PATH");
                if (path == null)
                {
                    return null;
                }
                var folders = path.Split(';');
                foreach (var folder in folders)
                {
                    path = Path.Combine(folder, fileName);
                    if (File.Exists(path))
                    {
                        return path;
                    }
                }
            }
            catch (System.Security.SecurityException)
            {
                throw;
            }
            return null;
        }

        /// <summary>
        /// Number of smallest time increments in a millisecond
        /// </summary>
        public const long TicksPerMillisecond = 10000;

        /// <summary>
        /// Get current time in milliseconds
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentMilliseconds()
        {
            return DateTime.Now.Ticks / TicksPerMillisecond;
        }

        public static string StripIndexer(string name)
        {
            var newName = name;
            if (name.Contains("["))
            {
                var start = name.IndexOf('[');
                var end = name.IndexOf(']');
                newName = name.Substring(0, start) + name.Substring(end + 1);
                // String index = name.Substring( start + 1, end + 1 );
                //newName = name.Replace( "[" + index + "]", "" );
            }
            return newName;
        }

        /// <summary>
        /// Return true only if arrays are identical
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static bool IsEqualBytes(byte[] array1, byte[] array2)
        {
            var size = array1.Length;
            if (size != array2.Length)
            {
                return false;
            }
            var retVal = true;
            for (var i = 0; i < size; i++)
            {
                if (array1[i] == array2[i]) { continue; }
                retVal = false;
                break;
            }
            return retVal;
        }

        public static string StripArrayIndex(string array)
        {
            var start = array.IndexOf('[');
            var end = array.IndexOf(']');
            if (start == -1 || end == -1)
            {
                return array;
            }
            var ret = array.Substring(0, start) + array.Substring(end + 1);
            return ret;
        }

        public static string FindNodeValue(string nodeName, string defaultValue, XmlElement docNode, bool simple)
        {
            var nodes = docNode.GetElementsByTagName(nodeName);
            if (nodes.Count == 0)
            {
                return defaultValue;
            }
            if (simple && !nodes.Item(0).ParentNode.Equals(docNode))
            {
                return defaultValue;
            }
            var node = GetTextNode(nodes.Item(0));
            if (node == null)
            {
                return defaultValue;
            }
            var text = node.InnerText;
            return text.Length == 0 ? defaultValue : text;
        }
    }
}
