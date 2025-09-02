//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class EdgeWindows : MonoBehaviour 
//{
//	public int windowID;
//	public Rect windowRect = new Rect(100, 100, 200, 200);
//	public float native_width = 1920;
//	public float native_height = 1080;
//	public bool Drag;
//	public GameObject computer;
//	private Computer com;
//	public bool show;
//	public bool minimize;
//	public Rect DefaltSetting;
//	public Rect DefaltBoxSetting;
//
//	public  RenderTexture video;
//	public Texture2D guivideo;
//
//	Texture2D screen = null;
//	WaitForEndOfFrame frameEndWait = new WaitForEndOfFrame();
//
//	// Use this for initialization
//	void Start () 
//	{
//		computer = GameObject.Find("System");
//		com = computer.GetComponent<Computer>();
//		windowRect.x = Customize.cust.windowx[windowID];
//		windowRect.y = Customize.cust.windowy[windowID];
//
//		screen = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false, false);
//	}
//	
//	// Update is called once per frame
//	void Update ()
//	{
//		
//	}
//
//	void Minimize()
//	{
//		if (minimize == true) 
//		{
//			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,25));
//		}
//		else
//		{
//			//private Rect DefaltSetting = new Rect (windowRect.x,windowRect.y,300,205);
//			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,DefaltSetting.height));
//		}
//	}
//
//	void OnGUI()
//	{
//		GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];
//
//		Customize.cust.windowx[windowID] = windowRect.x;
//		Customize.cust.windowy[windowID] = windowRect.y;
//
//		//float rx = Screen.width / native_width;
//		//float ry = Screen.height / native_height;
//
//		//GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1));
//
//		if(show == true)
//		{
//			GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
//			windowRect = GUI.Window(windowID,windowRect,DoMyWindow,""); 
//		}
//
//	}
//
//	void DoMyWindow(int WindowID)
//	{
//		int TempX = (int)windowRect.width;
//		int TempY = (int)windowRect.height;
//		video = new RenderTexture (512, 512, 16, RenderTextureFormat.ARGB32);
//		//video.Create ();
//		//guivideo = video.toTexture2D();
//		GUI.Box(new Rect(0,0,windowRect.width,windowRect.height),screen);
//
//		//StartCoroutine(DecodeScreen());
//
//		//RenderTexture rt = new RenderTexture(windowRect.width / 2, windowRect.height / 2, 0);
//		RenderTexture.active = video;
//		// Copy your texture ref to the render texture
//		if (guivideo == null)
//			guivideo = new Texture2D(video.width, video.height, TextureFormat.RGBA32, true);
//		guivideo.ReadPixels(new Rect(0, 0, video.width, video.height), 0, 0, false);
//	}
//
//	IEnumerator DecodeScreen()
//	{
//		yield return frameEndWait;
//		RenderTexture RT = RenderTexture.GetTemporary(Screen.width, Screen.height, 24);
//		//Resize only when Texture2D is null or when its size changes
//		if (screen == null || screen.width != RT.width || screen.height != RT.height)
//		{
//			screen.Resize(RT.width, RT.height, TextureFormat.RGB24, false);
//			//screen = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false, false);
//		}
//		screen.ReadPixels(new Rect(0, 0, RT.width, RT.height), 0, 0);
//		screen.Apply();
//		RenderTexture.ReleaseTemporary(RT);
//	}
//
//}
//
//public static class ExtensionMethod
//{
//	public static Texture2D toTexture2D(this RenderTexture rTex)
//	{
//		Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
//		RenderTexture.active = rTex;
//		tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
//		tex.Apply();
//		return tex;
//	}
//}