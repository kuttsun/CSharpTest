using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AsyncAwait
{
	class Program
	{
		static void Main(string[] args)
		{
			//Task.Run(() => Hoge());
			Hoge();
			Foo();

			Console.WriteLine("Please any key ...");
			Console.ReadKey();
		}

		static async void Hoge()
		{
			Console.WriteLine("Hoge Begin");
			for (int i = 0; i < 10; i++)
			{
				await HogeAsync($"test{i}");
			}
			Console.WriteLine("Hoge End");
		}

		static async Task<bool> HogeAsync(string str)
		{
			Console.WriteLine($"HogeAsync Begin {str}");
			var ret = await Task.Run(() =>
			{
				Thread.Sleep(5000);
				return true;
			});
			Console.WriteLine($"HogeAsync End {str}");
			return ret;
		}

		static void Foo()
		{
			Console.WriteLine("Foo Begin");
			for (int i = 0; i < 10; i++)
			{
				FooAsync($"test{i}");
			}
			Console.WriteLine("Foo End");
		}

		static async void FooAsync(string str)
		{
			Console.WriteLine($"FooAsync Begin {str}");
			await Task.Run(() =>
			{
				Thread.Sleep(5000);
			});
			Console.WriteLine($"FooAsync End {str}");
		}
	}
}
