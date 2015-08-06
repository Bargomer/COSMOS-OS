using Cosmos.Hardware;
using System;
internal class b
{
	private static string[] a = new string[]
	{
		"",
		"Wen",
		"Thu",
		"Fri",
		"Sat",
		"Sun",
		"Mon",
		"Tue",
		"Wen",
		"Thur",
		"Fri"
	};
	private static string[] b = new string[]
	{
		"Jan",
		"Feb",
		"Mar",
		"Apr",
		"May",
		"Jun",
		"Jul",
		"Aug",
		"Sept",
		"Oct",
		"Nov",
		"Dec"
	};
	public static string b()
	{
		string str = string.Concat(new string[]
		{
			RTC.Month.ToString(),
			"/",
			RTC.DayOfTheMonth.ToString(),
			"/",
			RTC.Year.ToString(),
			" ",
			global::b.a()
		});
		return global::b.a[(int)(RTC.DayOfTheWeek + 1)] + " " + str;
	}
	public static string a()
	{
		string text = RTC.Hour.ToString() + ":";
		if (RTC.Hour.ToString() == "0")
		{
			text = "12:";
		}
		if (RTC.Minute > 9)
		{
			text += RTC.Minute.ToString();
		}
		else
		{
			text = text + "0" + RTC.Minute.ToString();
		}
		return text;
	}
}
