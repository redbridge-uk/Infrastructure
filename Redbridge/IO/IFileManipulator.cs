using System;
using System.IO;

namespace Redbridge.IO
{
	public interface IFileManipulator
	{
		byte[] ReadAllBytes(string path);
		void WriteAllBytes(string path, byte[] bytes);
		string GetTempFileName();
		void DeleteFile(string path);
		StreamReader OpenText(string path);
		bool FileExists(string path);
		StreamWriter CreateFile(string fileName, FileClashBehaviour clashBehaviour = FileClashBehaviour.Error);
		bool DirectoryExists(string path);
		void CreateDirectory(string path);
		string GetDirectory(string path);
		bool FolderExists(string path);
		void DeleteFolder(string path, bool recursive = false);
        void Copy (string source, string target);
	}
}
