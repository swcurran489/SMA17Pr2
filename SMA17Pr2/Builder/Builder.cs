using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Builder {
	public class Builder {

	//public
		public Builder(string appBase, string builderName) {
			_buildServer = Path.Combine(appBase, builderName);
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

		public bool pullRepository(string repoPath) {
			string localPath;
			DirectoryInfo repoDI;

			if (!Directory.Exists(repoPath))
				return false;
			repoDI = new DirectoryInfo(repoPath);
			localPath = Path.Combine(_buildServer, repoDI.Name);
			if (!Directory.Exists(localPath))
				Directory.CreateDirectory(localPath);
			return true;
		}

		//private
		private string _buildServer { get; set; }
		
		static void Main(string[] args) {
			string appBase = @"C: \Users\Sean\Source\repos\SMA17Pr2\";
			string name = @"build_server";
			Builder b = new Builder(appBase, name);
			if (!b.init()) {
				Console.WriteLine("error in builder.init");
				return;
			}
		}
	}
}
