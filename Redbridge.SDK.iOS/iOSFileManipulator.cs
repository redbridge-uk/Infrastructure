using System;
using System.IO;
using Redbridge.SDK;

namespace Redbridge.IO
{
	public class iOSFileManipulator : IFileManipulator
	{
		public byte[] ReadAllBytes(string path)
		{
            if (path == null) throw new ArgumentNullException(nameof(path));
			return File.ReadAllBytes(path);
		}

		public void WriteAllBytes(string path, byte[] bytes)
		{
            if (path == null) throw new ArgumentNullException(nameof(path));
			File.WriteAllBytes(path, bytes);
		}

		public string GetTempFileName()
		{
			return Path.GetTempFileName();
		}

		public void DeleteFile(string path)
		{
            if (path == null) throw new ArgumentNullException(nameof(path));
			File.Delete(path);
		}

		public void DeleteFolder(string path, bool recursive = false)
		{
            if (path == null) throw new ArgumentNullException(nameof(path));
			Directory.Delete(path, recursive);
		}

		public StreamReader OpenText(string path)
		{
            if (path == null) throw new ArgumentNullException(nameof(path));
            return File.OpenText(path);
		}

		public bool FileExists(string path)
		{
            if (path == null) throw new ArgumentNullException(nameof(path));
			return File.Exists(path);
		}

		public bool FolderExists(string path)
		{
            if (path == null) throw new ArgumentNullException(nameof(path));
			return Directory.Exists(path);
		}

		public StreamWriter CreateFile(string fileName, FileClashBehaviour clashBehaviour = FileClashBehaviour.Error)
		{
			return File.CreateText(fileName);
		}

		public bool DirectoryExists(string path)
		{
            if (path == null) throw new ArgumentNullException(nameof(path));
			return Directory.Exists(path);
		}

		public void CreateDirectory(string path)
		{
            if (path == null) throw new ArgumentNullException(nameof(path));
			Directory.CreateDirectory(path);
		}

		public string GetDirectory(string path)
		{
			return Path.GetDirectoryName(path);
		}

        public void Copy(string source, string target)
        {
            File.Copy(source, target);
        }
    }
}

