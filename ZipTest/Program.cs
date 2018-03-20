using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace ZipTest
{
	class Program
	{
		static void Main(string[] args)
		{
			// .old ファイルは最初に削除しておく
			string exeFileName = Assembly.GetExecutingAssembly().Location;
			File.Delete(Path.GetFileName(exeFileName) + ".old");

			//Archive(Directory.GetCurrentDirectory(), "Backup.zip");
			Entry(Directory.GetCurrentDirectory(), "Backup3.zip");
			//Extract("Backup3.zip");
			ExtractEntries("Backup3.zip");
		}

		// 指定したディレクトリをzipにする
		static void Archive(string dir, string destinationFileName)
		{
			try
			{
				ZipFile.CreateFromDirectory(dir, destinationFileName);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		// 指定したディレクトリ内のファイルとフォルダをまとめてzipにする
		static void Entry(string dir, string destinationFileName)
		{
			File.Delete(destinationFileName);

			// ファイル一覧を取得
			IEnumerable<string> files = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);

			// 順にzipに追加
			using (var z = ZipFile.Open(destinationFileName, ZipArchiveMode.Update))
			{
				foreach (string file in files)
				{
					z.CreateEntryFromFile(file, GetRelativePath(dir + '\\', file), CompressionLevel.Optimal);
					Console.WriteLine("ZIPに追加:" + file);
				}
			}
		}

		/// <summary>
		/// 引数１のディレクトリから見た引数２のファイルへの相対パスを取得する
		/// </summary>
		/// <param name="uri1">基準となるディレクトリへの絶対パス(最後は\で終わっている必要あり)</param>
		/// <param name="uri2">目的のファイルへの絶対パス</param>
		/// <returns>引数１のディレクトリから見た引数２のファイルへの相対パス</returns>
		/// <example>
		/// GetRelativePath(@"C:\Windows\System\", @"C:\Windows\file.txt")
		/// ..\file.txt
		/// </example>
		static string GetRelativePath(string uri1, string uri2)
		{
			Uri u1 = new Uri(uri1);
			Uri u2 = new Uri(uri2);

			Uri relativeUri = u1.MakeRelativeUri(u2);

			string relativePath = relativeUri.ToString();

			relativePath.Replace('/', '\\');

			Console.WriteLine("相対パスに変換:" + relativePath);

			return (relativePath);
		}

		// zipファイルを展開する
		static void Extract(string archiveFileName)
		{
			ZipFile.ExtractToDirectory(archiveFileName, "hoge");
		}

		// 指定したzipファイルの中身を１つずつカレントディレクトリに展開する
		// 参考：http://www.atmarkit.co.jp/ait/articles/1502/03/news080.html
		static void ExtractEntries(string archiveFileName)
		{
			// ZIPファイルを開いてZipArchiveオブジェクトを作る
			using (ZipArchive archive = ZipFile.OpenRead(archiveFileName))
			{
				// 選択したファイルを指定したフォルダーに書き出す
				foreach (ZipArchiveEntry entry in archive.Entries)
				{
					try
					{
						string exeFileName = Assembly.GetExecutingAssembly().Location;
						if (entry.FullName == Path.GetFileName(exeFileName))
						{
							// exeファイルは直接上書きできないので、リネームして展開し、再起動後に古いファイルを削除する
							Console.WriteLine("リネームして展開: " + entry.FullName);
							string oldFileName = entry.FullName + ".old";
							File.Delete(oldFileName);
							File.Move(entry.FullName, oldFileName);
						}
						else
						{
							File.Delete(entry.FullName);
						}
						// ZipArchiveEntryオブジェクトのExtractToFileメソッドにフルパスを渡す
						entry.ExtractToFile(entry.FullName);
						Console.WriteLine("展開成功: " + entry.FullName);
					}
					catch (Exception e)
					{
						Console.WriteLine("展開失敗: " + e.Message);
					}
				}
			}
		}
	}
}

