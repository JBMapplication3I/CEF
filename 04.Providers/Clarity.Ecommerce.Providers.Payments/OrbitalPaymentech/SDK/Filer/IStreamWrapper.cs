using System;
using System.IO;

namespace JPMC.MSDK.Filer
{
	/// <summary>
	/// Summary description for IStreamWrapper.
	/// </summary>
	public interface IStreamWrapper
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		void CreateWriteStream(string filename);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="filename"></param>
		void CreateReadStream(string filename);
		/// <summary>
		/// 
		/// </summary>
		void Close();
		/// <summary>
		/// 
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="origin"></param>
		void Seek(long offset, SeekOrigin origin);
		/// <summary>
		/// 
		/// </summary>
		long Position { get; }
		/// <summary>
		/// 
		/// </summary>
		bool IsWriteStream { get; }
		/// <summary>
		/// 
		/// </summary>
		bool IsOpen { get; }
		/// <summary>
		/// 
		/// </summary>
		string FileName { get; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="bytes"></param>
		void Write(byte[] bytes);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="integer"></param>
		void Write(int integer);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="integer"></param>
		void Write(long integer);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		void Write(string text);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="chr"></param>
		void Write(char chr);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		char[] ReadChars(int count);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		byte[] ReadBytes(int count);
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		int ReadInt();
		/// <summary>
		/// 
		/// </summary>
		long Length { get; }
		/// <summary>
		/// 
		/// </summary>
		bool EOF { get; }
		/// <summary>
		/// 
		/// </summary>
		void Flush();
		/// <summary>
		/// 
		/// </summary>
		/// <param name="length"></param>
		void SetLength(long length);
	}
}
