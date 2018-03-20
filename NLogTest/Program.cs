using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLog;

namespace NLogTest
{
	class Program
	{
		//private static Logger logger = LogManager.GetLogger("fooLogger");
		private static Logger logger = LogManager.GetLogger("csv");
		//private static Logger logger = LogManager.GetCurrentClassLogger();

		static void Main(string[] args)
		{
			string mes = "メッセージ";

			try
			{
				logger.Fatal(mes);
				logger.Error(mes);
				logger.Warn(mes);
				logger.Info(mes);
				logger.Debug(mes);
				logger.Trace(mes);

				throw new Exception("例外発生");
			}
			catch (Exception ex)
			{
				// 引数に Exception を渡すと例外が発生した場所の情報を出力してくれる
				// FatalException というメソッドもあるが、非推奨っぽい
				logger.Fatal(ex);
				logger.Error(ex);
				logger.Warn(ex);
				logger.Info(ex);
				logger.Debug(ex);
				logger.Trace(ex);
			}

			Console.ReadKey();
		}
	}
}
