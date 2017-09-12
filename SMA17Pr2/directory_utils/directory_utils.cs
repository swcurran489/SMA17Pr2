﻿using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks; using System.IO;  namespace SMA17Pr2 { 	public class directory_utils { 		static public void _copyDirectory(string source, string dest, bool doSubs) { 			string tempPath; 			DirectoryInfo sourceDir; 			DirectoryInfo[] sourceSubDirs; 			FileInfo[] sourceFiles;  			if (!Directory.Exists(dest)) 				Directory.CreateDirectory(dest); 			sourceDir = new DirectoryInfo(source); 			sourceSubDirs = sourceDir.GetDirectories(); 			sourceFiles = sourceDir.GetFiles(); 			foreach (FileInfo file in sourceFiles) { 				tempPath = Path.Combine(dest, file.Name); 				file.CopyTo(tempPath, true); 			} 			if (doSubs) { 				foreach (DirectoryInfo sub in sourceSubDirs) { 					tempPath = Path.Combine(dest, sub.Name); 					_copyDirectory(sub.FullName, tempPath, doSubs); 				} 			} 		}  		static public bool getUser(ref string user, string slash = @"\") { 			string pwd; 			int ind2, ind3; 			int ind1 = 2; 			 			ind2 = ind3 = 0; 			try { pwd = Directory.GetCurrentDirectory(); } 			catch { return false; } 			ind2 = pwd.IndexOf(slash, ind1 + 1); 			if (ind2 <= 0) 				return false; 			ind3 = pwd.IndexOf(slash, ind2 + 1); 			if (ind3 <= 0) 				return false; 			user = pwd.Substring(ind2 + 1, ind3 - ind2 - 1); 			return true; 		}  		static public bool onWindows() {
			return Directory.GetCurrentDirectory()[0] == 'C';
		}  		static void Main(string[] args) { 		} 	} }  