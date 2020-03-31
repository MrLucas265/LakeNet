using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu  
{
	public static Rect ContextMenuPos(Rect r)
	{
		r.x = Mathf.Clamp(r.x,0,200);
		r.y = Mathf.Clamp(r.y,0,500);
		return r;
	}
}
