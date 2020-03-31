using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowClamp
{
	public static Rect ClampToScreen(Rect r)
	{
		r.x = Mathf.Clamp(r.x,0,Screen.width-r.width);
		r.y = Mathf.Clamp(r.y,0,Screen.height-r.height);
		return r;
	}
}
