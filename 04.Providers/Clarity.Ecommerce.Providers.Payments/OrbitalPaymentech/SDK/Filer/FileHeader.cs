using System;
using System.Reflection;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Filer
{
	/// <summary>
	/// Summary description for FileHeader.
	/// </summary>
	public class FileHeader
	{
		#region Private Members

		/// <summary>
		/// Used by encryption. 
		/// </summary>
		private int iterationCount = 2;
		/// <summary>
		/// An 20-byte series of bytes that is used for 
		/// encryption/decryption.
		/// </summary>
		private byte[] salt;
		private string version;
		/// <summary>
		/// A 25-character string that specifies the type of file this is.
		/// </summary>
		private string fileType;
		/// <summary>
		/// A 10-character string that represents the XML node used when 
		/// converting  the file.
		/// </summary>
		private string formatName;
		/// <summary>
		/// A 1-byte boolean value to specify whether the batch was
		/// successfully closed.
		/// </summary>
		private bool batchClosed;
		/// <summary>
		/// An integer that specifies the length of each line.
		/// This applies only to files that have a static length for
		/// each record. This has a value of zero if the the file
		/// contains variable length records.
		/// </summary>
		private int lineLength;
		/// <summary>
		/// A 5-character string used to specify the end of a field.
		/// This is only used for variable length files.
		/// </summary>
		private string fieldDelimiter;
		/// <summary>
		/// A 5-character string used to specify the end of a record.
		/// This is only used for variable length files.
		/// </summary>
		private string recordDelimiter = "\r\n";
		#endregion

		#region Public Members

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <remarks>
		/// The version field is determined by getting the assembly version. 
		/// This is because the assembly version is automatically set during
		/// the build process, so no code change will be required between
		/// builds to keep the version up-to-date.
		/// </remarks>
		public FileHeader()
		{
			var appName = Assembly.GetAssembly(this.GetType()).Location;
			var assemblyName = AssemblyName.GetAssemblyName(appName);
			this.version = assemblyName.Version.ToString();
		}

		/// <summary>
		/// This method properly compares another FileHeader object
		/// with this one.
		/// </summary>
		/// <param name="obj">The object to compare to.</param>
		/// <returns>True if they match, false if they do not.</returns>
		public override bool Equals(object obj )
		{
			if ( !obj.GetType().Equals( typeof(FileHeader) ) )
				return false;

			var header = (FileHeader) obj;

			var same = true;
			
			same = same && header.BatchClosed == this.BatchClosed;
			same = same && header.Version == this.Version;
			same = same && header.RecordDelimiter == this.RecordDelimiter;
			same = same && header.FieldDelimiter == this.FieldDelimiter;
			same = same && header.FileType == this.FileType;
			same = same && header.FormatName == this.FormatName;
			same = same && header.IterationCount == this.IterationCount;
			same = same && header.salt.Length == this.salt.Length;
			same = same && header.LineLength == this.LineLength;
			for ( var i = 0; i < header.salt.Length; i++ )
			{
				same = same && header.Salt[ i ] == this.Salt[ i ];
			}
			return same;
		}

		/// <summary>
		/// Get the hashcode.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode() { return 0; }

		/// <summary>
		/// Used by encryption. 
		/// </summary>
		public int IterationCount
		{
			get => this.iterationCount;
            set => this.iterationCount = value;
        }

		/// <summary>
		/// An 8-byte series of bytes that is used for 
		/// encryption/decryption.
		/// </summary>
		public byte[] Salt
		{
			get => this.salt;
            set => this.salt = value;
        }

		/// <summary>
		/// A 25-byte string that specifies the type of file this is.
		/// </summary>
		public string FileType
		{
			get => this.fileType;
            set 
			{ 
				if (value == null)
					fileType = " ".PadRight(25, ' ');
				else
					fileType = value; 
			}
		}

		/// <summary>
		/// A 30-byte string that represents the XML node used when 
		/// converting  the file.
		/// </summary>
		public string FormatName
		{
			get => this.formatName;
            set { 
				if (value == null)
					formatName = " ".PadRight(30, ' ');
				else
					formatName = value; 
			}
		}

		/// <summary>
		/// A 20-byte string that represents the XML node used when 
		/// converting  the file.
		/// </summary>
		public string Version
		{
			get => this.version;
            set 
			{ 
				if (value == null)
					version = " ".PadRight(20, ' ');
				else
					version = value; 
			}
		}

		/// <summary>
		/// A 1-byte boolean value to specify whether the batch was
		/// successfully closed.
		/// </summary>
		public bool BatchClosed
		{
			get => this.batchClosed;
            set => batchClosed = value;
        }

		/// <summary>
		/// An integer that specifies the length of each line.
		/// This applies only to files that have a static length for
		/// each record. This has a value of zero if the the file
		/// contains variable length records.
		/// </summary>
		public int LineLength
		{
			get => this.lineLength;
            set => lineLength= value;
        }

		/// <summary>
		/// A 5-character string used to specify the end of a field.
		/// This is only used for variable length files.
		/// </summary>
		public string FieldDelimiter
		{
			get => this.fieldDelimiter;
            set 
			{ 
				if (value == null)
					fieldDelimiter = " ".PadRight(5, ' ');
				else
					fieldDelimiter = value; 
			}
		}

		/// <summary>
		/// A 5-character string used to specify the end of a record.
		/// This is only used for variable length files.
		/// </summary>
		public string RecordDelimiter
		{
			get => this.recordDelimiter;
            set 
			{ 
				if (value == null)
					recordDelimiter = " ".PadRight(5, ' ');
				else
					recordDelimiter = value; 
			}
		}

		/// <summary>
		/// Generates the entire file header as a string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var buff = new System.Text.StringBuilder();
			buff.Append(this.BatchClosed.ToString().PadRight(20, ' '));
			buff.Append(this.Version.PadRight(20, ' '));
			if ( Salt != null )
			{
				buff.Append( Utils.ByteArrayToString( Salt ) );
			}
			else
			{
				buff.Append( "        " );
			}
			buff.Append(this.IterationCount.ToString().PadRight(20, ' '));
			buff.Append(this.FileType.PadRight(25, ' '));
			buff.Append(this.Version.PadRight(30, ' '));
			buff.Append(this.LineLength.ToString().PadRight(20, ' '));
			buff.Append(this.RecordDelimiter.PadRight(5, ' '));
			buff.Append(this.FieldDelimiter.PadRight(30, ' '));

			return buff.ToString();
		}

		/// <summary>
		/// Generates the entire file header as a byte array.
		/// </summary>
		/// <returns></returns>
		public byte[] ToByteArray()
		{
			return Utils.StringToByteArray(ToString());
		}

		#endregion
	}
}
