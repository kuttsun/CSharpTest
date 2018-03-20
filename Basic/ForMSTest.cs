using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic
{
	/// <summary>
	/// MSTest の使い方を勉強するためのクラス
	/// </summary>
	public class ForMSTest
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public ForMSTest() { }

		public int Add(int x, int y)
		{
			return (x + y);
		}

		public int Sub(int x, int y)
		{
			return (x - y);
		}

		public int Mult(int x, int y)
		{
			return (x * y);
		}

		public int Div(int x, int y)
		{
			return (x / y);
		}
	}
}
