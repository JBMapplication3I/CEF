using System;
using System.Collections.Generic;
using System.Reflection;
using JPMC.MSDK.Common;

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
    /// Contains all the properties of a Field in a converter template file.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay( "{GetType().Name}, Name: {Name}" )]
    public class Field
    {
        private string name;
        private string fieldID;
        private string alias;
        public int Position { get; set; }
        public string DefaultValue { get; set; }
        private bool fillDefault;
        private TextJustification justification = TextJustification.None;
        public string FillWith { get; set; }
        public DataType FieldType { get; set; }
        public Case CaseType { get; set; }
        public bool HasMask { get; set; }
        public string MaskWith { get; set; }
        public int MaskLength { get; set; }
        public TextJustification MaskJustification { get; set; }
        public bool MultiLength { get; set; }
        public bool Hide { get; set; }
        public int PrefixLength { get; set; }
        public List<int> PrefixValues { get; private set; }
        public List<int> Lengths { get; private set; }
        public bool IsFloating { get; set; }
        public string Start { get; set; }
        public string Next { get; set; }
        public string MatchParentField { get; set; }
        public Format ParentFormat { get; set; }

        // For Response
        public int Index { get; set; }
        public bool IsSuppress { get; set; }
        public int ConditionID { get; set; }
        public int SuffixLength { get; set; }
        public string SuffixColumnName { get; set; }
        public bool HideSuffixColumnName { get; set; }
        public string PrefixColumnName { get; set; }
        public bool HidePrefixColumnName { get; set; }
        public int ArrayLength { get; set; }
        public string ArrayNodeName { get; set; }
        public bool IsCalcArraySize { get; private set; }
        public bool ArrayIndicator { get; set; }
        public bool MultiColumn { get; set; }
        public List<Field> Fields { get; private set; }
        public List<string> Aliases { get; private set; }
        public string Required { get; set; }

        public Field()
        {
            HideSuffixColumnName = true;
            PrefixValues = new List<int>();
            Fields = new List<Field>();
            Aliases = new List<string>();
            Position = -1;
            Lengths = new List<int>();
            FieldType = DataType.AlphaNumeric;
            CaseType = Case.None;
            MaskLength = -1;
            MaskJustification = TextJustification.Left;
            PrefixLength = -1;
            Index = -1;
            ConditionID = -1;
            SuffixLength = -1;
            ArrayLength = -1;
        }

        public Field( Field copy )
        {
            name = copy.name;
            fieldID = copy.fieldID;
            alias = copy.alias;
            Position = copy.Position;
            DefaultValue = copy.DefaultValue;
            fillDefault = copy.fillDefault;
            justification = copy.justification;
            FillWith = copy.FillWith;
            FieldType = copy.FieldType;
            CaseType = copy.CaseType;
            HasMask = copy.HasMask;
            MaskWith = copy.MaskWith;
            MaskLength = copy.MaskLength;
            MaskJustification = copy.MaskJustification;
            MultiLength = copy.MultiLength;
            Hide = copy.Hide;
            PrefixLength = copy.PrefixLength;
            PrefixValues = new List<int>( copy.PrefixValues );
            Lengths = new List<int>( copy.Lengths );
            IsFloating = copy.IsFloating;

            // For Response
            Index = copy.Index;
            IsSuppress = copy.IsSuppress;
            ConditionID = copy.ConditionID;
            SuffixLength = copy.SuffixLength;
            SuffixColumnName = copy.SuffixColumnName;
            HideSuffixColumnName = copy.HideSuffixColumnName;
            PrefixColumnName = copy.PrefixColumnName;
            HidePrefixColumnName = copy.HidePrefixColumnName;
            ArrayLength = copy.ArrayLength;
            ArrayNodeName = copy.ArrayNodeName;
            IsCalcArraySize = copy.IsCalcArraySize;
            ArrayIndicator = copy.ArrayIndicator;
            MultiColumn = copy.MultiColumn;
            Fields = new List<Field>( copy.Fields );
            Aliases = copy.Aliases;
        }

        public bool IsEmpty => name == null;

        private void IsValid()
        {
            if ( IsEmpty )
            {
                throw new ConverterException( Error.InvalidField,
                    "The Converter Field is empty. The converter template and request templates may not be in sync." );
            }
        }

        public int GetPrefixValue( string index )
        {
            if ( !Utils.IsNumeric( index ) )
            {
                return -1;
            }
            return GetPrefixValue( Convert.ToInt32( index ) );
        }

        public int GetPrefixValue( int index )
        {
            if ( index > PrefixValues.Count )
            {
                return -1;
            }
            if ( index <= 0 )
            {
                return 0;
            }
            return PrefixValues[ index - 1 ];
        }

        public void AddPrefixValue( int value )
        {
            PrefixValues.Add( value );
        }

        public Field GetField( string name )
        {
            var fieldName = name;
            if ( fieldName.Contains( "." ) )

            {
                fieldName = fieldName.Substring( fieldName.LastIndexOf( '.' ) + 1 );
            }

            foreach ( var field in Fields )

            {
                if ( field.Name == fieldName )
                {
                    return field;
                }
            }

            return new Field();
        }


        public string Name
        {
            get
            {
                IsValid();
                return name;
            }
            set
            {
                name = value;
                if ( Aliases.Count == 0 )
                {
                    Aliases.Add( name );
                }
            }
        }
        public string FieldID
        {
            get
            {
                IsValid();
                return fieldID;
            }
            set => fieldID = value.StartsWith( "." ) ? value.Substring( 1 ) : value;
        }

        public TextJustification Justification
        {
            get => justification;
            set
            {
                justification = value;
                if ( MaskJustification == TextJustification.None )
                {
                    MaskJustification = justification;
                }
            }
        }
        public void SetMaskJustification( TextJustification just )
        {
            MaskJustification = just;
        }
        public string Alias
        {
            get
            {
                IsValid();
                return alias;
            }
            set
            {
                alias = value;
                if ( alias != null )
                {
                    var list = alias.Split( ',' );
                    foreach ( var item in list )
                    {
                        if ( !Aliases.Contains( item ) )
                        {
                            Aliases.Add( item );
                        }
                    }
                }

                if ( !Aliases.Contains( name ) )
                {
                    if ( Aliases.Count > 0 )
                    {
                        Aliases.Insert( 0, name );
                    }
                    else
                    {
                        Aliases.Add( name );
                    }
                }
            }
        }

        public int Length
        {
            get
            {
                if ( Lengths == null || Lengths.Count == 0 )
                {
                    return 0;
                }
                return Lengths[ 0 ];
            }
            set
            {
                if ( Lengths.Count > 0 )
                {
                    Lengths.Insert( 0, value );
                }
                else
                {
                    Lengths.Add( value );
                }
            }
        }



        // For Response
        public void SetField( Field field )
        {
            for ( var i = 0; i < Fields.Count; i++ )
            {
                if ( Fields[ i ].Name == field.Name )
                {
                    Fields.Insert( i, field );
                    return;
                }
            }

            Fields.Add( field );
        }

        public bool IsRequired => Required != null && Required == "M";
    }
}
