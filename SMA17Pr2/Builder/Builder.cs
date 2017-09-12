using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Build.BuildEngine;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace SMA17Pr2 {
	public class Builder {
	
		//public
		public Builder(string appBase, string builderName, string repoPath) {
			_buildServer = Path.Combine(appBase, builderName);
			_repoPath = repoPath;
		}

		public bool init() {
			try {
				Directory.CreateDirectory(_buildServer);
			}
			catch (Exception e) {
				Console.WriteLine(e.Message);
				return false;
			}
			return true;
		}

		public bool runBuild(string projName) {
			string solPath = "";

			if (!_pullRepository(projName, ref solPath)) {
				Console.WriteLine("error pulling repository");
				return false;
			}
			_compile(solPath, );
			return true;
		}


		//private

		private bool _pullRepository(string projName, ref string solPath) {
			string localPath, projPath, solFile, t;
			DirectoryInfo repoDI;

			projPath = Path.Combine(_repoPath, projName);
			if (!Directory.Exists(projPath))
				return false;
			repoDI = new DirectoryInfo(projName);
			localPath = Path.Combine(_buildServer, repoDI.Name);
			if (!Directory.Exists(localPath))
				Directory.CreateDirectory(localPath);
			directory_utils._copyDirectory(projPath, localPath, true);
			t = projName + ".sln";
			solFile = Path.Combine(localPath, t);
			if (!File.Exists(solFile))
				return false;
			solPath = solFile;
			return true;
		}

		private void _compile(string solPath, ref string output) {
			Project p;
			Microsoft.Build.BuildEngine.FileLogger log = new FileLogger();

			Microsoft.Build.BuildEngine.Engine.GlobalEngine.BuildEnabled = true;
			p = new Project(Microsoft.Build.BuildEngine.Engine.GlobalEngine);
			p.BuildEnabled = true;
			p.Load(solPath);
			p.Build();
		}

		private string _buildServer { get; set; }
		private string _repoPath { get; set; }
		
		static void Main(string[] args) {
			string appBase, user, name, localProj, repoPath;
			user = "";
			if (!directory_utils.getUser(ref user)) {
				Console.WriteLine("error on getUser");
				return;
			}
			appBase = @"/Users/" + user + @"/Projects/SMA17Pr2.git/";
			//appBase = @"C:\Users\" + user + @"\source\repos\SMA17Pr2\";
			name = @"build_server";
			localProj = @"test_project";
			repoPath = appBase + @"repository/";
			Builder b = new Builder(appBase, name, repoPath);
			if (!b.init()) {
				Console.WriteLine("error in Builder.init");
				return;
			}
			b.runBuild(localProj);

		}
	}
}
