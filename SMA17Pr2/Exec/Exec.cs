using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SMA17Pr2 {
	public class Exec {

		static void Main(string[] args) {
			string appBase, repoPath, failProj, builderPath, failProjPath, successProj, successProjPath;
			appBase = Path.GetFullPath(@"../../../../");
			repoPath = Path.Combine(appBase, @"repository");
			failProj = "test_project";
			successProj = "herpderp";
			failProjPath = Path.Combine(appBase, failProj);
			successProjPath = Path.Combine(appBase, successProj);
			builderPath = Path.Combine(appBase, @"build_server");
			Repository r = new Repository(repoPath, builderPath);
			Builder b = new Builder(builderPath, repoPath);
			b.init();
			Console.WriteLine("beginning build fail project\n");
			r.storeProject(failProjPath);
			b.runBuild(failProj);
			Console.WriteLine();
			Console.WriteLine("beginning build success project\n");
			r.storeProject(successProjPath);
			b.runBuild(successProj);
			Console.WriteLine("beginning test request");
			r.createTestRequest("herpderp");

		}
	}
}



