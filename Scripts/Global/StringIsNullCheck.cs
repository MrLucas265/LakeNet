using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringIsNullCheck
{
	public static string NotNull(string s)
	{
		if(s == null)
        {
			s = "";
        }
		return s;
	}
}
