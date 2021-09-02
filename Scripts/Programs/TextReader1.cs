//using UnityEngine;
//using System.Collections;

//public class TextReader : MonoBehaviour
//{
//	public Rect windowRect = new Rect(100, 100, 200, 200);
//	public float native_width = 1920;
//	public float native_height = 1080;
//	public int windowID;

//	public string Text;
//	public string Title;

//	public bool show;

//	private Computer com;
//	// Use this for initialization
//	void Start () 
//	{
//		com = GetComponent<Computer>();
//		native_height = Customize.cust.native_height;
//		native_width = Customize.cust.native_width;
//	}

//	// Update is called once per frame
//	void Update () 
//	{
//		if (show == true) 
//		{
//			DataCheck();
//		}
//	}

//	void DataCheck()
//	{
////		switch(hp.Address)
////		{
////		case "www.becassystems.com/documents/emails":
////			switch (Title) 
////			{
////			case "Test 1":
////				Text = "Hey there lucas just testing a new text reading programing for documents and such :p";
////				break;
////
////			case "Test 2":
////				Text = "This file is empty lolz";
////				break;
////			}
////			break;
////		}
//	}

//	void OnGUI()
//	{
//		Customize.cust.windowx[windowID] = windowRect.x;
//		Customize.cust.windowy[windowID] = windowRect.y;

//		GUI.skin = com.Skin[GameControl.control.GUIID];

//		//set up scaling
//		float rx = Screen.width / native_width;
//		float ry = Screen.height / native_height;

//		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 

//		if(show == true)
//		{
//			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,"");
//		}
//	}

//	void DoMyWindow(int WindowID)
//	{
//		GUI.DragWindow (new Rect (5, 5, 370, 21));
//		GUI.Box (new Rect (5, 5, 370, 21), Title);
//		if(GUI.Button(new Rect(375, 5, 21, 21),"X"))
//		{
//			show = false;
//		}

//		GUI.TextArea((new Rect (5, 30, 385, 90)),Text);

//		if(GUI.Button(new Rect(150, 125, 50, 20),"Ok"))
//		{
//			show = false;
//		}
//	}
//}
