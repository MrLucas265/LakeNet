using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringGenerator 
{
	public static string RandomNumberChar(int min, int max)
	{
		const string AccNo = "1234567890";

		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
		string retMe = "";
		for (int i = 0; i < charAmount; i++)
		{
			retMe += AccNo[Random.Range(0, AccNo.Length)];
		}
		return retMe;
	}

	public static string RandomMixedChar(int min, int max)
	{
		const string glyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
		string retMe = "";
		for (int i = 0; i < charAmount; i++)
		{
			retMe += glyphs[Random.Range(0, glyphs.Length)];
		}
		return retMe;
	}

	public static string RandomSmallWithNumbersChar(int min, int max)
	{
		const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";

		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
		string retMe = "";
		for (int i = 0; i < charAmount; i++)
		{
			retMe += glyphs[Random.Range(0, glyphs.Length)];
		}
		return retMe;
	}

	public static string RandomCapsWithNumbersChar(int min, int max)
	{
		const string glyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
		string retMe = "";
		for (int i = 0; i < charAmount; i++)
		{
			retMe += glyphs[Random.Range(0, glyphs.Length)];
		}
		return retMe;
	}

	public static string RandomSmallChar(int min, int max)
	{
		const string glyphs = "abcdefghijklmnopqrstuvwxyz";

		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
		string retMe = "";
		for (int i = 0; i < charAmount; i++)
		{
			retMe += glyphs[Random.Range(0, glyphs.Length)];
		}
		return retMe;
	}

	public static string RandomCapsChar(int min, int max)
	{
		const string glyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
		string retMe = "";
		for (int i = 0; i < charAmount; i++)
		{
			retMe += glyphs[Random.Range(0, glyphs.Length)];
		}
		return retMe;
	}

	public static string RandomCapsWithSmallChar(int min, int max)
	{
		const string glyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

		int charAmount = Random.Range(min, max); //set those to the minimum and maximum length of your string
		string retMe = "";
		for (int i = 0; i < charAmount; i++)
		{
			retMe += glyphs[Random.Range(0, glyphs.Length)];
		}
		return retMe;
	}
}
