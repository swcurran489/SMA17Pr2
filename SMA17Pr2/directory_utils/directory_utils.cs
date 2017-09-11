using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SMA17Pr2 {
	public class directory_utils {
		static public void _copyDirectory(string source, string dest, bool doSubs) {
			string tempPath;
			DirectoryInfo sourceDir;
			DirectoryInfo[] sourceSubDirs;
			FileInfo[] sourceFiles;

			if (!Directory.Exists(dest))
				Directory.CreateDirectory(dest);
			sourceDir = new DirectoryInfo(source);
			sourceSubDirs = sourceDir.GetDirectories();
			sourceFiles = sourceDir.GetFiles();
			foreach (FileInfo file in sourceFiles) {
				tempPath = Path.Combine(dest, file.Name);
				file.CopyTo(tempPath, true);
			}
			if (doSubs) {
				foreach (DirectoryInfo sub in sourceSubDirs) {
					tempPath = Path.Combine(dest, sub.Name);
					_copyDirectory(sub.FullName, tempPath, doSubs);
				}
			}
		}

		static void Main(string[] args) {
		}
	}
}
