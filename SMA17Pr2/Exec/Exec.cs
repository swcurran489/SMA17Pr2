using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SMA17Pr2 {
	public class Exec {

		static void Main(string[] args) {
			string appBase, repoPath, testProj, builderName;
			appBase = Path.GetFullPath(@"../../../../");
			repoPath = Path.Combine(appBase, @"repository");
			testProj = Path.Combine(appBase, @"test_project");
			builderName = @"build_server";
			Repository r = new Repository(repoPath);
			Builder b = new Builder(appBase, builderName, repoPath);

		}
	}
}



