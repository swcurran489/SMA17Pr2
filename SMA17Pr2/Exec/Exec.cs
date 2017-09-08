using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMA17Pr2 {
	public class Exec {
		static void Main(string[] args) {
			string app_base = @"C:\Users\a571906\Source\repos\SMA17Pr2\";
			string repo_path = app_base + @"repository\";
			string local_proj = app_base + @".\test_project\";

			Repository repo = new Repository(repo_path);
			repo.storeProject(local_proj);
		}
	}
}



