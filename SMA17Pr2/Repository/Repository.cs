using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SMA17Pr2 {
	public class Repository {

		//public
		public string repoPath { get; private set; }

		public Repository(string rp) {
			repoPath = rp;
			_projects = new List<string>();
			_makeRepo(rp);
			_loadProjects();
		}

		//public bool makeProjectDir(string proj) {
		//    if (Directory.Exists(baseDir+proj))
		//        return false;
		//    Directory.CreateDirectory(baseDir + proj);
		//    return true;
		//}

		public bool storeProject(string proj) {
			string dest;
			DirectoryInfo proj_di = new DirectoryInfo(proj);
			dest = repoPath + proj_di.Name;
			if (!projExists(dest))
				Directory.CreateDirectory(dest);
			_copyDirectory(proj, dest, true);
			return true;
		}

		public bool projExists(string name) {
			return Directory.Exists(name);
		}


		//private
		private void _loadProjects() {
			foreach (var d in Directory.GetDirectories(repoPath)) {
				DirectoryInfo di = new DirectoryInfo(d);
				_projects.Add(di.Name);
			}
		}

		private void _copyDirectory(string source, string dest, bool doSubs) {
			string tempPath;
			DirectoryInfo sourceDir;
			DirectoryInfo[] sourceSubDirs;
			FileInfo[] sourceFiles;

			if (!Directory.Exists(dest))
				Directory.CreateDirectory(dest);
			sourceDir = new DirectoryInfo(source);
			sourceSubDirs = sourceDir.GetDirectories();
			sourceFiles = sourceDir.GetFiles();
			foreach(FileInfo file in sourceFiles) {
				tempPath = Path.Combine(dest, file.Name);
				file.CopyTo(tempPath, true);
			}
			if (doSubs) {
				foreach(DirectoryInfo sub in sourceSubDirs) {
					tempPath = Path.Combine(dest, sub.Name);
					_copyDirectory(sub.FullName, tempPath, doSubs);
				}
			}
		}

		private void _makeRepo(string path) {
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
		}


		private List<string> _projects;

		static void Main(string[] args) {

		}
	}


	
}
