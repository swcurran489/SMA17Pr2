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
			_copyDirectory(dest, proj, true);
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

		private void _copyDirectory(string dest, string source, bool subs) {
			string temp_path;
			string temp_dir_path;

			DirectoryInfo source_di = new DirectoryInfo(source);
			DirectoryInfo[] source_subdirs = source_di.GetDirectories();
			FileInfo[] source_files = source_di.GetFiles();
			foreach (var file in source_files) {
				temp_path = Path.Combine(dest, file.Name);
				file.CopyTo(temp_path, true);
			}
			if (subs) {
				foreach (var sub_dir in source_subdirs) {
					temp_dir_path = Path.Combine(dest, sub_dir.Name);
					_copyDirectory(temp_dir_path, sub_dir.FullName, subs);
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
