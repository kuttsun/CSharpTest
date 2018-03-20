using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Threading;

namespace AutoUpdate
{
	class Program
	{
		// 参考：https://mo.kerosoft.com/095
		static void Main(string[] args)
		{
			var update = new Update();

			// アップデート後の起動の場合は後始末を行う
			if (update.CleanUp() == false)
			{
				// アップデート後の起動ではない
				// →アップデートをチェックする

				//自分自身のバージョン情報を取得する
				FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
				//結果を表示
				Console.WriteLine("製品バージョン {0}", fvi.ProductVersion);// AssemblyVersion
				Console.WriteLine("ファイルバージョン {0}", fvi.FileVersion);// AssemblyFileVersion

				string latestVersion;

				if (update.IsExistUpdate(fvi.ProductVersion, out latestVersion) == true)
				{
					MessageBox.Show("最新のアップデートがあります {0}", latestVersion);

					update.Run();

				}
			}
		}
	}
}
