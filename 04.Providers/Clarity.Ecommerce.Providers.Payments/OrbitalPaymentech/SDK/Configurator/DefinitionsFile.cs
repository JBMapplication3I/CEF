#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK.Configurator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Xml;
    using log4net;

    public class DefinitionsFile : Definitions, IDisposable
    {
        private ZipArchive archive;
        private ILog logger;

        public DefinitionsFile( string pathToDefinitions, IConfiguratorFactory factory )
        {
            logger = factory.EngineLogger;

            try
            {
                archive = ZipFile.OpenRead( pathToDefinitions );
            }
            catch ( Exception e )
            {
                var msg = $"Failed to load definitions file at {pathToDefinitions}.";
                logger.ErrorFormat( msg );
                throw new ConfiguratorException( Error.DefinitionsFileFailure, msg, e );
            }
        }

        public override XmlDocument GetXmlDocument( string filename, bool returnNull )
        {
            var text = GetText( filename, returnNull );
            if ( text == null )
            {
                return null;
            }

            var doc = new XmlDocument();
            doc.LoadXml( text );
            return doc;
        }

        public override bool HasFile( string filename)
        {
            return archive.GetEntry( filename ) != null;
        }

        public override string GetText( string filename, bool allowNull )
        {
            var entry = archive.GetEntry( filename );

            if ( entry == null )
            {
                if ( allowNull )
                {
                    return null;
                }
                throw new ConfiguratorException( Error.FileNotFound, "The file \"" + filename + "\" was not found in definitions.jar. The file may be old." );
            }

            using ( var stream = entry.Open() )
            {
                using ( var reader = new StreamReader( stream ) )
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public override void Dispose()
        {
            if ( archive != null )
            {
                archive.Dispose();
            }
        }

        public override string FindFileNameByExtension( string extension )
        {
            foreach ( var entry in archive.Entries )
            {
                var ext = Path.GetExtension( entry.Name );
                if ( ext == extension )
                {
                    return entry.Name;
                }
            }

            return null;
        }

        public override Dictionary<string, string> GetProperties( string filename )
        {
            var entry = archive.GetEntry( filename );

            if ( entry == null )
            {
                throw new ConfiguratorException( Error.FileNotFound, "The file \"" + filename + "\" was not found in definitions.jar. The file may be old." );
            }

            var props = new Dictionary<string, string>();

            using ( var stream = entry.Open() )
            {
                using ( var reader = new StreamReader( stream ) )
                {
                    for ( var line = reader.ReadLine(); line != null; line = reader.ReadLine() )
                    {
                        var parts = line.Split( '=' );
                        if ( parts.Length < 2 )
                        {
                            continue;
                        }
                        props.Add( parts[ 0 ].Trim(), parts[ 1 ].Trim() );
                    }
                }
            }

            return props;
        }
    }
}
