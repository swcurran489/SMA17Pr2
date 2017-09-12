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
			if (!_compile(solPath)) {
				Console.WriteLine("error compiling project");
				return false;
			}
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

		private bool _compile(string solPath) {
			//Project p;
			//Microsoft.Build.BuildEngine.FileLogger log = new FileLogger();

			////Microsoft.Build.Evaluation.ProjectCollection
			//Microsoft.Build.BuildEngine.Engine.GlobalEngine.BuildEnabled = true;
			//p = new Project(Microsoft.Build.BuildEngine.Engine.GlobalEngine);
			//p.BuildEnabled = true;
			//try {
			//	p.Load(solPath);
			//}
			//catch (Exception e) {
			//	Console.WriteLine(e.Message);
			//	return false;
			//}
			//p.Build();
			//return true;
			List<ILogger> loggers = new List<ILogger>();
			loggers.Add(new ConsoleLogger());
			var projectCollection = new Microsoft.Build.Evaluation.ProjectCollection();
			projectCollection.RegisterLoggers(loggers);
			var project = projectCollection.LoadProject(buildFileUri); // Needs a reference to System.Xml
			try {
				project.Build();
			}
			finally {
				projectCollection.UnregisterAllLoggers();
			}
		}

		private string _buildServer { get; set; }
		private string _repoPath { get; set; }
		
		static void Main(string[] args) {
			string appBase, name, testProj, repoPath;
			Builder b;

			name = @"build_server";
			appBase = Path.GetFullPath(@"../../../../");
			repoPath = Path.Combine(appBase, @"repository");
			testProj = @"test_project";
			b = new Builder(appBase, name, repoPath);
			if (!b.init()) {
				Console.WriteLine("error in Builder.init");
				return;
			}
			b.runBuild(testProj);

		}
	}
}
