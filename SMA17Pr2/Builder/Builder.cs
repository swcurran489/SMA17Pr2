using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Build.Construction;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;
using Microsoft.Build.Framework;

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

		private bool _pullRepository(string projName, ref string solnPath) {
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
			solnPath = solFile;
			return true;
		}

		private bool _compile(string solnPath) {
			var props = new Dictionary<string, string>();
			ConsoleLogger l = new ConsoleLogger();
			List<ILogger> logs = new List<ILogger>();
			logs.Add(l);
			props["Configuration"] = "Release";
			BuildRequestData request = new BuildRequestData(solnPath, props, null, new string[] { "Build" }, null);
			var buildParams = new BuildParameters();
			buildParams.Loggers = logs;

			var result = BuildManager.DefaultBuildManager.Build(buildParams, request);
			return result.OverallResult == BuildResultCode.Success;
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
