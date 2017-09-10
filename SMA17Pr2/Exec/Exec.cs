using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SMA17Pr2 {
	public class Exec {

		static private bool getUser(ref string user) {
			string pwd, slash;
			int ind2, ind3;
			int ind1 = 2;

			slash = @"\";
			ind2 = ind3 = 0;
			try { pwd = Directory.GetCurrentDirectory(); }
			catch { return false; }
			ind2 = pwd.IndexOf(slash, ind1 + 1);
			if (ind2 <= 0)
				return false;
			ind3 = pwd.IndexOf(slash, ind2 + 1);
			if (ind3 <= 0)
				return false;
			user = pwd.Substring(ind2 + 1, ind3 - ind2 - 1);
			return true;
		}

		static void Main(string[] args) {
            string user, appBase, repoPath, localProj;
			user = "";
			if (!getUser(ref user)) {
				Console.WriteLine("error on getUser");
				return;
			}
			appBase = @"C:\Users\" + user + @"\Source\repos\SMA17Pr2\";
			repoPath = appBase + @"repository\";
			localProj = appBase + @".\test_project\";

			Repository repo = new Repository(repoPath);
			repo.storeProject(localProj);
		}
	}
}



