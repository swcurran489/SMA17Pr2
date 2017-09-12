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
		
		public bool storeProject(string proj) {
			string dest;
			DirectoryInfo proj_di = new DirectoryInfo(proj);
			dest = repoPath + proj_di.Name;
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



	//main
		static void Main(string[] args) {
			string user, appBase, repoPath, testProj;
			user = "";
			if (!directory_utils.getUser(ref user)) {
				Console.WriteLine("error on getUser");
				return;
			}
			appBase = @"/Users/" + user + @"/Projects/SMA17Pr2.git/";
			//appBase = @"C:\Users\" + user + @"\source\repos\SMA17Pr2\";
			repoPath = appBase + @"repository/";
			testProj = appBase + @"./test_project/";

			Repository repo = new Repository(repoPath);
			repo.storeProject(testProj);
		}
	}
}
