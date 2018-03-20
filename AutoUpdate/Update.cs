using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace AutoUpdate
{
	public class Update
	{
		readonly string releaseURL = "https://github.com/kuttsun/CSharpTest/releases/";
		string latestVersion = string.Empty;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public Update()
		{

		}

		/// <summary>
		/// 最新のアップデートがあるかどうかチェックする
		/// </summary>
		/// <param name="currentVersion"></param>
		/// <returns></returns>
		public bool IsExistUpdate(string currentVersion, out string latestVersion)
		{
			// HtmlAgilityPack では直接URLのデータを取得できないようなので、
			// WebClient を使用して取得する
			using (WebClient webClient = new WebClient())
			using (Stream stream = webClient.OpenRead(releaseURL))
			{
				var html = new HtmlDocument();

				// ページを取得
				html.Load(stream);

				// spanタグのやつの中で
				var nodes = html.DocumentNode.Descendants("span")
					// class が css-truncate-target のやつを取得し
					.Where(node => node.GetAttributeValue("class", string.Empty).Contains("css-truncate-target"))
					// タグの中身を取得
					.Select(node => node.InnerHtml);

				// 取得したタグの中身を順に表示
				foreach (var version in nodes)
				{
					Console.WriteLine(version);
				}

				// バージョンを取得できた
				if (nodes.Count() > 0)
				{
					// 一番最初が最も新しいバージョン
					latestVersion = nodes.First().ToString();

					var version1 = new Version(currentVersion);
					var version2 = new Version(latestVersion);
					if (version1 < version2)
					{
						this.latestVersion = latestVersion;
						return true;
					}
				}
			}

			latestVersion = string.Empty;
			return false;
		}

		/// <summary>
		/// 最新バージョンのバイナリファイルをダウンロードする
		/// </summary>
		/// <param name="latestVersion"></param>
		/// <returns></returns>
		bool DownloadLatestVersion()
		{
			using (WebClient webClient = new WebClient())
			{
				webClient.DownloadFile(releaseURL + "download/" + latestVersion + "/AutoUpdate.exe", @".\hoge.exe");
				return true;
			}
		}

		/// <summary>
		/// アップデートを実行する
		/// </summary>
		/// <returns></returns>
		public void Run()
		{
			DownloadLatestVersion();

			File.Delete("AutoUpdate.old");
			File.Move("AutoUpdate.exe", "AutoUpdate.old");
			File.Move("hoge.exe", "AutoUpdate.exe");

			Process.Start("AutoUpdate.exe", "/up " + Process.GetCurrentProcess().Id);
		}

		/// <summary>
		/// アップデート後の後始末を行う
		/// </summary>
		/// <returns></returns>
		public bool CleanUp()
		{
			// プログラム引数 /up が指定されているインデックスを取得
			int index = Environment.CommandLine.IndexOf("/up");
			if (index != -1)
			{
				try
				{
					// プログラム引数を取得
					string[] args = Environment.GetCommandLineArgs();
					// プログラム引数 /up の次がプロセスID
					int pid = Convert.ToInt32(args[index+1]);
					Console.WriteLine("id=" + pid);
					// 終了待ち
					Process.GetProcessById(pid).WaitForExit();
				}
				catch (Exception)
				{
				}

				// プログラムが終了したので古いファイルを削除
				File.Delete("AutoUpdate.old");
				Console.WriteLine("Update Completed");
				return true;
			}

			return false;
		}
	}
}
