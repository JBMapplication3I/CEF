using System;
using System.IO;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Filer
{
	/// <summary>
	/// Summary description for StreamWrapper.
	/// </summary>
	public class StreamWrapper : IStreamWrapper
	{
		private const int INTEGER_LENGTH = 20;
		private FileStream stream;
		private string filename;

		/// <summary>
		/// 
		/// </summary>
		public StreamWrapper()
		{
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="create"></param>
		/// <param name="forWriting"></param>
		public StreamWrapper(string filename, bool create, bool forWriting)
		{
			if (create && forWriting)
				stream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
			else if (forWriting)
				stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
			else
				stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
			this.filename = filename;
		}

		#region IStreamWrapper Members

		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		public void CreateWriteStream(string filename) 
		{
			stream = new FileStream(filename, FileMode.CreateNew, FileAccess.ReadWrite);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		public void CreateReadStream(string filename)
		{
			stream = new FileStream(filename, FileMode.CreateNew, FileAccess.ReadWrite);
		}

		/// <summary>
		/// 
		/// </summary>
		public string FileName => this.filename;

        /// <summary>
		/// 
		/// </summary>
		public bool EOF => stream.Position >= stream.Length;

        /// <summary>
		/// 
		/// </summary>
		public void Close()
		{
			if (stream != null)
			{
				stream.Close();
				stream = null;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool IsOpen => stream != null;

        /// <summary>
		/// 
		/// </summary>
		public bool IsWriteStream => true;

        /// <summary>
		/// 
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="origin"></param>
		public void Seek(long offset, SeekOrigin origin)
		{
			stream.Seek(offset, origin);
		}

		/// <summary>
		/// 
		/// </summary>
		public long Position => stream.Position;

        /// <summary>
		/// 
		/// </summary>
		public long Length => stream.Length;

        /// <summary>
		/// 
		/// </summary>
		/// <param name="bytes"></param>
		public void Write(byte[] bytes)
		{
			stream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		public void Write(string text)
		{
			var bytes = Utils.StringToByteArray(text);
			stream.Write(bytes, 0, bytes.Length);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="chr"></param>
		public void Write(char chr)
		{
			stream.Write(Utils.StringToByteArray(new string(chr, 1)), 0, 1);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="integer"></param>
		public void Write(int integer)
		{
			Write((long)integer);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="integer"></param>
		public void Write(long integer)
		{
			var bytes = Utils.StringToByteArray(integer.ToString());
			stream.Write(bytes, 0, bytes.Length);
			if (bytes.Length < INTEGER_LENGTH)
			{
				bytes = Utils.StringToByteArray(new string(' ', INTEGER_LENGTH - bytes.Length));
				stream.Write(bytes, 0, bytes.Length);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public char[] ReadChars(int count)
		{
			var buffer = new byte[count];
			stream.Read(buffer, 0, count);
			return Utils.ByteArrayToString(buffer).ToCharArray();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public byte[] ReadBytes(int count)
		{
			var buffer = new byte[count];
			stream.Read(buffer, 0, count);
			return buffer;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int ReadInt()
		{
			var buffer = new byte[INTEGER_LENGTH];
			stream.Read(buffer, 0, INTEGER_LENGTH);
			var str = Utils.ByteArrayToString(buffer).Trim();
            try
            {
                return int.Parse(str);
            }
            catch
            {
                return 0;
            }
		}

		/// <summary>
		/// 
		/// </summary>
		public void Flush()
		{
			stream.Flush();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="length"></param>
		public void SetLength(long length)
		{
			if (stream.CanWrite)
				stream.SetLength(length);
		}

		#endregion
	}
}
