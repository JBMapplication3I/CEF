#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Xml;

namespace JPMC.MSDK.Configurator
{
    public abstract class Definitions : IDisposable
    {
        public virtual XmlDocument GetXmlDocument( string filename, bool returnNull )
        {
            return null;
        }

        public virtual bool HasFile( string filename )
        {
            return false;
        }

        public virtual string GetText( string filename, bool allowNull )
        {
            return "This is abstract";
        }

        public virtual void Dispose()
        {
        }

        public virtual string FindFileNameByExtension( string extension )
        {
            return null;
        }

        public virtual Dictionary<string, string> GetProperties( string filename )
        {
            return null;
        }
    }
}
