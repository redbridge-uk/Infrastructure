using System;
using System.IO;
using Redbridge.IO;

namespace Redbridge.SDK.Droid
{
	public class DroidFileManipulator: IFileManipulator
	{
		public byte[] ReadAllBytes(string path)
		{
			return File.ReadAllBytes(path);
		}

		public void WriteAllBytes(string path, byte[] bytes)
		{
			File.WriteAllBytes(path, bytes);
		}

		public string GetTempFileName()
		{
			return Path.GetTempFileName();
		}

		public void DeleteFile(string path)
		{
			File.Delete(path);
		}

		public void DeleteFolder(string path, bool recursive = false)
		{
			Directory.Delete(path, recursive);
		}

		public StreamReader OpenText(string path)
		{
			return File.OpenText(path);
		}

		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		public bool FolderExists(string path)
		{
			return Directory.Exists(path);
		}

		public StreamWriter CreateFile(string fileName, FileClashBehaviour clashBehaviour = FileClashBehaviour.Error)
		{
			return File.CreateText(fileName);
		}

		public bool DirectoryExists(string path)
		{
			return Directory.Exists(path);
		}

		public void CreateDirectory(string path)
		{
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
