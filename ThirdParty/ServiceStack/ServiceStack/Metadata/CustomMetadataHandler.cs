using System;
using System.Text;
using ServiceStack.Host;
using ServiceStack.Logging;
using ServiceStack.Text;

namespace ServiceStack.Metadata
{
    public class CustomMetadataHandler
        : BaseMetadataHandler
    {
        private static readonly new ILog Log = LogManager.GetLogger(typeof(CustomMetadataHandler));

        public CustomMetadataHandler(string contentType, string format)
        {
            ContentType = contentType;
            ContentFormat = format;
        }

        public override Format Format => ContentFormat.ToFormat();

        protected override string CreateMessage(Type dtoType)
        {
            try
            {
                var requestObj = AutoMappingUtils.PopulateWith(Activator.CreateInstance(dtoType));

                using (var ms = MemoryStreamFactory.GetStream())
                {
                    HostContext.ContentTypes.SerializeToStream(
                        new BasicRequest { ContentType = ContentType }, requestObj, ms);

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                var error = $"Error serializing type '{dtoType.GetOperationName()}' with custom format '{ContentFormat}'";
                Log.Error(error, ex);

                return $"{{Unable to show example output for type '{dtoType.GetOperationName()}' using the custom '{ContentFormat}' filter}}{ex.Message}";
            }
        }
    }
}