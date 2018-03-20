using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace Basic
{
	/// <summary>
	/// GitHub の Releases からファイルをダウンロードしてくるテスト
	/// </summary>
	class GetGitHubReleases
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public GetGitHubReleases()
		{
			// HtmlAgilityPack では直接URLのデータを取得できないようなので、
			// WebClient を使用して取得する
			using (WebClient webClient = new WebClient())
			using (Stream stream = webClient.OpenRead("https://github.com/gitbucket/gitbucket/releases"))
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
			}
		}
	}
}
