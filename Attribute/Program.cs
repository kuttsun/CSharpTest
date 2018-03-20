using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Attribute
{
	class Program
	{
		static void Main(string[] args)
		{
			Gender gender = Gender.Female;

			Console.WriteLine(gender.DisplayName());
		}
	}

	// enum 定義
	enum Gender
	{
		/// <summary>
		/// 不明
		/// </summary>
		[Display(Name = "不明")]
		Unknown,
		/// <summary>
		/// 男性
		/// </summary>
		[Display(Name = "男性")]
		Male,
		/// <summary>
		/// 女性
		/// </summary>
		[Display(Name = "女性")]
		Female
	}

	/// <summary>
	/// Genderに対する拡張メソッドを定義する
	/// </summary>
	static class GenderExt
	{
		/// <summary>
		/// 属性を使用して表示名を取得する
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string DisplayName(this Enum value)
		{
			var fieldInfo = value.GetType().GetField(value.ToString());
			var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

			if ((descriptionAttributes != null) && (descriptionAttributes.Length > 0))
			{
				return descriptionAttributes[0].Name;
			}
			else
			{
				return value.ToString();
			}
		}
	}
}
