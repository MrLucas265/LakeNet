using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NotepadSystem
{
	public string CurrentWorkingTitle;
	public string SaveLocation;
	public string TypedTitle;
	public string TypedText;
	public string CurrentMenu;
	public int SelectedDocument;

	public NotepadSystem(string currentworkingtitle, string savelocation,string typedtitle,string typedtext,string currentmenu,int selecteddocument)
	{
		CurrentWorkingTitle = currentworkingtitle;
		SaveLocation = savelocation;
		TypedTitle = typedtitle;
		TypedText = typedtext;
		CurrentMenu = currentmenu;
		SelectedDocument = selecteddocument;
	}
}