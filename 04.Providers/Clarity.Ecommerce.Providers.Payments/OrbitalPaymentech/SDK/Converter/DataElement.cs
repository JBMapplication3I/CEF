using System;
using System.Collections.Generic;
using JPMC.MSDK.Framework;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Converter
{
	/// <summary>
	/// Represents a format or a field in a Request XML. 
	/// </summary>
	/// <remarks>
	/// The Request XML document is parsed into a RequestData object, 
	/// which contains a DataElement for each format and field. 
	/// </remarks>
	[System.Diagnostics.DebuggerDisplay( "{GetType()}, Name: {Name}" )]
	public class DataElement
	{
		private string name;
		private string value;
        private string maskedValue;
		private string fieldID;
		private IFullResponse responseData;
		private bool hide;
		private List<DataElement> elements = new List<DataElement>();
		private DataElement parent;
        private int numAliases = 1;
        public bool IsAlias { get; set; }

		public DataElement()
		{
		}
		
		/// <summary>
		/// Constructor. Sets the name and value for the DataElement.
		/// </summary>
		/// <param name="name">The element's name.</param>
		/// <param name="value">The element's value, as set by the merchant.</param>
		public DataElement( string name, string value, DataElement parent )
		{
			this.name = name;
			this.value = value;
			this.parent = parent;
			this.responseData = parent.ResponseData;
			this.fieldID = $"{parent.FieldID}.{name}";
		}

		/// <summary>
		/// Constructor. Sets the name and value for the DataElement.
		/// </summary>
		/// <param name="name">The element's name.</param>
		/// <param name="value">The element's value, as set by the merchant.</param>
		public DataElement( string name, string value, IFullResponse responseData )
		{
			this.name = name;
			this.value = value;
			this.responseData = responseData;
			this.fieldID = $"{name}";
		}

		public DataElement( DataElement copy )
		{
			name = copy.Name;
			value = copy.value;
            maskedValue = copy.maskedValue;
			fieldID = copy.fieldID;
			responseData = copy.responseData;
			hide = copy.hide;
			elements = new List<DataElement>();
			foreach ( var element in copy.Elements )
			{
				elements.Add( new DataElement( element ) );
			}
			parent = copy.parent;
		}

		/// <summary>
		/// Gets the element's name.
		/// </summary>
		public string Name
		{
			get => name;
            set 
			{
				if ( name != value )
				{
					name = value;
					var oldElementPath = fieldID;
					if ( parent != null )
					{
						fieldID = $"{parent.FieldID}.{name}";
					}
					else
					{
						fieldID = $"{name}";
					}
				}
			}
		}

		public bool HideFromFieldID
		{
			get => hide;
            set => hide = value;
        }

		public string FieldID
		{
			get => fieldID;
            set => fieldID = value;
        }

		public IFullResponse ResponseData => responseData;

        /// <summary>
		/// Gets and sets the element's value.
		/// </summary>
		public string Value
		{
			get => value;
            set => this.value = value;
        }

        /// <summary>
        /// Gets and sets the element's masked value.
        /// </summary>
        public string MaskedValue
        {
            get => maskedValue != null ? maskedValue : value;
            set => this.maskedValue = value;
        }

        /// <summary>
		/// Gets the list of child elements.
		/// </summary>
		/// <remarks>
		/// These are typically fields, when the element is a format.
		/// Occasionally, a format might have child formats, and in rare
		/// cases, a field may have a child format.
		/// </remarks>
		public List<DataElement> Elements => elements;

        public DataElement FindField(string fieldID )
	    {
            foreach ( var element in elements )
            {
        	    if ( element.FieldID.Equals( fieldID ) )
        	    {
        		    return element;
        	    }
        	
    		    var elem = element.FindField( fieldID );
    		    if ( elem != null )
    		    {
    			    return elem;
    		    }
            }
        
            return null;
	    }


		
		/// <summary>
		/// Retrieves an element from the Elements list by its name.
		/// </summary>
		/// <param name="name">The name of the element to retrieve.</param>
		/// <returns>The indicated element, or null if not found.</returns>
		public DataElement Element( string name )
		{
			foreach ( var element in elements )
			{
				if ( element.Name == name )
				{
					return element;
				}
			}
			
			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public DataElement GetElement( string name )
		{
			foreach ( var element in Elements )
			{
				if ( element.name == name )
				{
					return element;
				}
			}
			return null;
		}
		
		/// <summary>
		/// Returns true if the element represents a format.
		/// </summary>
		public bool IsFormat => elements.Count > 0;

        public DataElement Parent => parent;

        /// <summary>
        /// Gets and sets the number of aliases.
        /// </summary>
        public int NumAliases
        {
            get => numAliases;
            set => this.numAliases = value;
        }

        public void SetArrayIndex( int index )
	    {
		    if ( fieldID.Contains( "[" ) && fieldID.Contains( "]" ) )
		    {
			    return;
		    }
		
		    if ( elements.Count == 0 )
		    {
    		    var pos = fieldID.LastIndexOf( '.' );
    		    if ( pos > -1 )
    		    {
    			    fieldID = $"{fieldID.Substring(0, pos)}[{index}]{fieldID.Substring(pos)}";
    		    }
		    }
		    else if ( elements[ 0 ].Elements.Count == 0 )
		    {
   			    fieldID = $"{fieldID}[{index}]";
		    }

		    foreach ( var element in elements )
            {
        	    element.SetArrayIndex( index );
            }
	    }

    }
}
