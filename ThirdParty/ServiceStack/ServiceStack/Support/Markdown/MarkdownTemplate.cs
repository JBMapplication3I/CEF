using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using ServiceStack.Markdown;
using ServiceStack.Text;
using ServiceStack.Web;

namespace ServiceStack.Support.Markdown
{
    public class MarkdownTemplate : ITemplateWriter, IExpirable
    {
        public MarkdownTemplate() { }

        public MarkdownTemplate(string fullPath, string name, string contents)
        {
            FilePath = fullPath;
            Name = name;
            Contents = contents;
        }

        public string FilePath { get; set; }
        public string Name { get; set; }
        public string Contents { get; set; }
        public DateTime? LastModified { get; set; }

        public string[] TextBlocks { get; set; }
        public string[] VarRefBlocks { get; set; }

        public static string BodyPlaceHolder = "Body";
        public static string PlaceHolderPrefix = "<!--@";
        public static string PlaceHolderSuffix = "-->";

        internal Exception initException;
        private readonly object readWriteLock = new();
        private bool isBusy;

        public void Reload(string templateContents, DateTime lastModified)
        {
            lock (readWriteLock)
            {
                try
                {
                    isBusy = true;

                    Contents = templateContents;
                    LastModified = lastModified;
                    initException = null;
                    Prepare();
                }
                catch (Exception ex)
                {
                    initException = ex;
                }
                isBusy = false;
                Monitor.PulseAll(readWriteLock);
            }
        }

        public void Prepare()
        {
            if (Contents == null)
            {
                throw new ArgumentNullException("Contents");
            }

            var textBlocks = new List<string>();
            var variablePositions = new Dictionary<int, string>();

            int pos;
            var lastPos = 0;
            while ((pos = Contents.IndexOf(PlaceHolderPrefix, lastPos)) != -1)
            {
                var contentBlock = Contents.Substring(lastPos, pos - lastPos);

                textBlocks.Add(contentBlock);

                var endPos = Contents.IndexOf(PlaceHolderSuffix, pos);
                if (endPos == -1)
                {
                    throw new InvalidDataException("Unterminated PlaceHolder expecting -->");
                }

                var varRef = Contents.Substring(
                    pos + PlaceHolderPrefix.Length, endPos - (pos + PlaceHolderPrefix.Length));

                var index = textBlocks.Count;
                variablePositions[index] = varRef;

                lastPos = endPos + PlaceHolderSuffix.Length;
            }
            if (lastPos != Contents.Length - 1)
            {
                var lastBlock = Contents.Substring(lastPos);
                textBlocks.Add(lastBlock);
            }

            TextBlocks = textBlocks.ToArray();
            VarRefBlocks = new string[TextBlocks.Length];

            foreach (var varPos in variablePositions)
            {
                VarRefBlocks[varPos.Key] = varPos.Value;
            }
        }

        public string RenderToString(Dictionary<string, object> scopeArgs)
        {
            lock (readWriteLock)
            {
                while (isBusy)
                {
                    Monitor.Wait(readWriteLock);
                }
            }

            if (TextBlocks == null || VarRefBlocks == null)
            {
                throw new InvalidDataException("Template has not been Initialized.");
            }

            var sb = StringBuilderCache.Allocate();
            for (var i = 0; i < TextBlocks.Length; i++)
            {
                var textBlock = TextBlocks[i];
                var varName = VarRefBlocks[i];
                if (varName != null && scopeArgs != null)
                {
                    if (scopeArgs.TryGetValue(varName, out var varValue))
                    {
                        sb.Append(varValue);
                    }
                }
                sb.Append(textBlock);
            }
            return StringBuilderCache.ReturnAndFree(sb);
        }

        public void Write(MarkdownViewBase instance, TextWriter textWriter, Dictionary<string, object> scopeArgs)
        {
            lock (readWriteLock)
            {
                while (isBusy)
                {
                    Monitor.Wait(readWriteLock);
                }
            }

            if (TextBlocks == null || VarRefBlocks == null)
            {
                throw new InvalidDataException("Template has not been Initialized.");
            }

            for (var i = 0; i < TextBlocks.Length; i++)
            {
                var textBlock = TextBlocks[i];
                var varName = VarRefBlocks[i];
                if (varName != null)
                {
                    if (scopeArgs.TryGetValue(varName, out var varValue))
                    {
                        textWriter.Write(varValue);
                    }
                }
                textWriter.Write(textBlock);
            }
        }

    }

}