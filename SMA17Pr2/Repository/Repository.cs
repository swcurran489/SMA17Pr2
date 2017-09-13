using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SMA17Pr2 {
	public class Repository {

	//public
		public string repoPath { get; private set; }

		public Repository(string path) {
			repoPath = path;
			_projects = new List<string>();
			_makeRepo(path);
			_loadProjects();
		}
		
		public bool storeProject(string proj) {
			string dest;
			DirectoryInfo proj_di = new DirectoryInfo(proj);
			dest = Path.Combine(repoPath, proj_di.Name);
			if (!projExists(dest))
				Directory.CreateDirectory(dest);
			directory_utils._copyDirectory(proj, dest, true);
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
		
		private void _makeRepo(string path) {
			if (!Directory.Exists(path))
				Directory.CreateDirectory(path);
		}

		private List<string> _projects;


		
		static void Main(string[] args) {
			string appBase, repoPath, testProj;
			appBase = Path.GetFullPath(@"../../../../");
			repoPath = Path.Combine(appBase, @"repository");
			testProj = Path.Combine(appBase, @"test_project");


			Repository repo = new Repository(repoPath);
			repo.storeProject(testProj);
		}
	}
}
