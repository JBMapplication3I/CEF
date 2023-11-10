#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace JPMC.MSDK.Common
{
    /// <summary>
    /// Provides convenient methods and properties for manipulating filenames.
    /// </summary>
    public class FileName
    {
        private string filename = "";

        /// <summary>
        /// Constructs a FileName object for a specific filename.
        /// </summary>
        /// <param name="filename"></param>
        public FileName(string filename)
        {
            this.filename = filename;
        }

        /// <summary>
        /// Builds the absolute path to the file and returns the name.
        /// </summary>
        /// <returns>The absolute path to the file.</returns>
        public override string ToString()
        {
            return filename;
        }

        /// <summary>
        /// The name of the file without path or extension.
        /// </summary>
        public string Name
        {
            get => System.IO.Path.GetFileNameWithoutExtension( filename );
            set
            {
                var path = System.IO.Path.GetDirectoryName( filename );
                var ext = System.IO.Path.GetExtension( filename );
                filename = string.Concat( System.IO.Path.Combine( path, value ), ext );
            }
        }

        /// <summary>
        /// The directory path that the file is in, but without the filename
        /// itself.
        /// </summary>
        public string Path
        {
            get => System.IO.Path.GetDirectoryName( filename );
            set => filename = System.IO.Path.Combine( value, System.IO.Path.GetFileName( filename ) );
        }

        /// <summary>
        /// Gets or sets the file extension. Does not contain the period.
        /// </summary>
        public string Extension
        {
            get => System.IO.Path.GetExtension( filename );
            set
            {
                var path = System.IO.Path.GetDirectoryName( filename );
                var name = System.IO.Path.GetFileNameWithoutExtension( filename );
                path = System.IO.Path.Combine( path, name );
                filename = value.StartsWith( "." ) ? string.Concat( path, value ) : $"{path}.{value}";
            }
        }

        /// <summary>
        /// Gets or sets the combination of filename and extension.
        /// </summary>
        public string NameAndExtension
        {
            get => System.IO.Path.GetFileName( filename );
            set
            {
                var fname = value != null ? value : "";
                var path = System.IO.Path.GetDirectoryName( filename );
                filename = path != null ? System.IO.Path.Combine( path, fname ) : fname;
            }
        }

        /// <summary>
        /// Returns the name without path or extension.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetName(string path)
        {
            return System.IO.Path.GetFileNameWithoutExtension( path );
        }

        /// <summary>
        /// Returns the file extension without the name or path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetExtension(string path)
        {
            return System.IO.Path.GetExtension( path );
        }

        /// <summary>
        /// Returns the directory path without the filename.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetPath(string path)
        {
            return System.IO.Path.GetDirectoryName( path );
        }

        /// <summary>
        /// Returns the combination of filename and extension.
        /// </summary>
        public static string GetNameExtension(string path)
        {
            return System.IO.Path.GetFileName( path );
        }

        public static string Combine( string path1, string path2 )
        {
            var newPath2 = path2;
            while ( newPath2.StartsWith( "\\" ) )
            {
                newPath2 = newPath2.Remove( 0, 1 );
            }

            return System.IO.Path.Combine( path1, newPath2 );
        }
    }
}
