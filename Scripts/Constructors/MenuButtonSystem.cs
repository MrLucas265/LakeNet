using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MenuButtonSystem
{
	public string Name;
	public string Menu;
	public float Width;
	public float Height;
	public float PosX;
    public float PosY;
    public bool Selected;

    public MenuButtonSystem()
    {
    }
    
	public MenuButtonSystem(string name,string menu, float width, float height)
	{
		Name = name;
		Menu = menu;
		Width = width;
		Height = height;
	}
}