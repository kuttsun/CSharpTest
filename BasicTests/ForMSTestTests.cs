using Microsoft.VisualStudio.TestTools.UnitTesting;
using Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Tests
{
	[TestClass()]
	public class ForMSTestTests
	{
		[TestMethod()]
		public void AddTest()
		{
			var test = new ForMSTest();
			Assert.AreEqual(5, test.Add(2,3));
		}

		[TestMethod()]
		public void SubTest()
		{
			var test = new ForMSTest();
			Assert.AreEqual(-1, test.Sub(2, 3));
		}

		[TestMethod()]
		public void MultTest()
		{
			var test = new ForMSTest();
			Assert.AreEqual(6, test.Mult(2, 3));
			Assert.AreEqual(0, test.Mult(2, 0));
			Assert.AreEqual(0, test.Mult(0, 2));
		}

		[TestMethod()]
		public void DivTest()
		{
			var test = new ForMSTest();
			Assert.AreEqual(3, test.Div(9, 3));
			Assert.AreEqual(2, test.Div(8, 3));
			Assert.AreEqual(0, test.Div(0, 2));

			// 0 の除算のテスト
			try {
				int x = test.Div(2, 0);
			}
			catch(DivideByZeroException)
			{
				// DivideByZeroException の例外が発生すれば OK
				return;
			}

			// 例外が発生しない or
			// DivideByZeroException 以外の例外が発生した場合は NG
			Assert.Fail();
		}
	}
}