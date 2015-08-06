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
			RTC.get_Month().ToString(),
			"/",
			RTC.get_DayOfTheMonth().ToString(),
			"/",
			RTC.get_Year().ToString(),
			" ",
			global::b.a()
		});
		return global::b.a[(int)(RTC.get_DayOfTheWeek() + 1)] + " " + str;
	}
	public static string a()
	{
		string text = RTC.get_Hour().ToString() + ":";
		if (RTC.get_Hour().ToString() == "0")
		{
			text = "12:";
		}
		if (RTC.get_Minute() > 9)
		{
			text += RTC.get_Minute().ToString();
		}
		else
		{
			text = text + "0" + RTC.get_Minute().ToString();
		}
		return text;
	}
}
