using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;

namespace JPMC.MSDK.Framework
{
	/// <summary>
	/// Singleton class that scans the pnsinclude folder to build a map of all formats/fields, 
	/// for use by the SDK.
	/// </summary>
	/// <remarks>
	/// This does a one-time scan that builds this list. It takes a while to scan all the files, 
	/// so this should only be used when needed. But the lengthy scan process only happens once. 
	/// </remarks>
	public class TemplateInfo
	{
		// singleton instance
		private static TemplateInfo instance;
		private static readonly object lock_ = new object();
		private List<string> allowsMultiples = new List<string>();
		private KeySafeDictionary<string, List<string>> fields = new KeySafeDictionary<string, List<string>>();
		private IDispatcherFactory factory;

		/// <summary>
		/// Default Contructor. For testing only.
		/// </summary>
		public TemplateInfo()
		{
		}

		/// <summary>
		/// Contructor. 
		/// </summary>
		/// <param name="factory"></param>
		public TemplateInfo( IDispatcherFactory factory )
		{
			this.factory = factory;
		}

		/// <summary>
		/// The singleton instance.
		/// </summary>
		public static TemplateInfo Instance
		{
			get
			{
				lock ( lock_ )
				{
					if ( instance != null )
					{
						return instance;
					}
					instance = new TemplateInfo( new DispatcherFactory() );
					instance.ScanTemplates();
					return instance;
				}
			}
		}

		/// <summary>
		/// Returns true if multiples are allowed for that field.
		/// </summary>
		/// <param name="name">The name of the field.</param>
		/// <returns></returns>
		public bool AllowsMultiples( string name )
		{
			return allowsMultiples.Contains( name );
		}

		/// <summary>
		/// Test getFieldPaths. These paths are not element paths, although they follow 
		/// the basic syntax. Sub-elements that are not additional formats are not 
		/// included, which means MessageHeader in PNS. This is okay.  
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public virtual string[] GetFieldPaths( string name )
		{
			if ( !fields.ContainsKey( name ) )
			{
				return new string[0];
			}
			var array = fields[name];
			return array.ToArray();
		}

		/// <summary>
		/// Cause the templates to scan. The Instance will cause this automatically.
		/// Calling it manually is only necessary for testing.
		/// </summary>
		public void ScanTemplates()
		{
            var document = factory.GetTemplate( "PNSNewTransaction", true );
			ScanFile( document, "" );
		}

		private void ScanFile( XmlDocument document, string path )
		{
			if ( document == null )
			{
				return;
			}
			var nodes = document.DocumentElement.ChildNodes;
			for ( var i = 0; i < nodes.Count; i++ )
			{
				ScanFields( nodes[i], path );
			}
			ProcessTemplateIncludes( document, path );
		}

		private void ProcessTemplate( string templateName, string path )
		{
            var document = factory.GetTemplate( templateName, true );
			ScanFile( document, path );
		}

		private void ScanFields( XmlNode node, string path )
		{
			if ( node == null )
			{
				return;
			}
			if ( node.NodeType == XmlNodeType.Element )
			{
				var value = Utils.GetNodeValue( node );
				if ( value != null && value.Length > 0 )
				{
					value = value.Trim();
					if ( value.StartsWith( "[%" ) && value.EndsWith( "%]" ) )
					{
						var name = node.Name;
						AddField( name, path );
						return;
					}
				}
			}

			var nodes = node.ChildNodes;
			for ( var i = 0; i <= nodes.Count; i++ )
			{
				ScanFields( nodes[i], path );
			}
		}

		private void AddField( string name, string path )
		{
			if ( fields.ContainsKey( name ) )
			{
                fields[ name ].Add( path + FieldIdentifier.Separator + name );
			}
			else
			{
				var list = new List<string>();
                list.Add( path + FieldIdentifier.Separator + name );
				fields.Add( name, list );
			}
		}

		private void ProcessTemplateIncludes( XmlDocument document, string path )
		{
			var tiNodes = document.GetElementsByTagName( "TemplateInclude" );
			for ( var i = 0; i < tiNodes.Count; i++ )
			{
				var value = Utils.GetAttributeValue( "max", "0", tiNodes[i] );
				var name = Utils.GetNodeValue( tiNodes[i] );
				name = name.Replace( "[#", "" ).Replace( "#]", "" ).Trim();
				var withPrefix = Utils.GetAttributeValue( "filePrefix", null, tiNodes[i] );
                if ( withPrefix != null )
                {
                    withPrefix = withPrefix + name;
                }
                else
                {
                    withPrefix = name;
                }
				if ( value != "1" )
				{
					allowsMultiples.Add( withPrefix );
				}
                var fullName = path.Length > 0 ? path + FieldIdentifier.Separator + name : name;
                ProcessTemplate( withPrefix, fullName );
			}
		}

		/// <summary>
		/// Used for testing. It dumps all the template info to the console.
		/// </summary>
		public string DumpTemplateInfo()
		{
			var dump = new StringBuilder( "TemplateInfo Dump Begins" );
			dump.AppendLine( "Templates that allow multiples:" );
			for ( var i = 0; i < allowsMultiples.Count; i++ )
			{
				dump.Append( "  " );
				dump.AppendLine( allowsMultiples[i] );
			}

			dump.AppendLine();
			dump.AppendLine( "Fields:" );
			foreach ( var key in fields.Keys )
			{
				dump.Append( "  " );
				dump.Append( key );
				dump.Append( " = " );
				var list = fields[key];
				if ( list.Count == 1 )
				{
					dump.AppendLine( list[0] );
				}
				else
				{
					for ( var i = 0; i < list.Count; i++ )
					{
						dump.AppendLine();
						dump.Append( "    " );
						dump.Append( list[i] );
					}
					dump.AppendLine();
				}
			}

			return dump.ToString();
		}
	}
}
