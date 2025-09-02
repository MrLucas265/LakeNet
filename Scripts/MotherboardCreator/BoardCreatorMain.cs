using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BoardCreatorMain : MonoBehaviour
{
	public bool LoadTextures;
	public List<Texture2D> LoadedTextures = new List<Texture2D>();
	public List<string> LoadedTexturesPath = new List<string>();
	public Texture2D[] LoadedTexturesGrid;
	public string FilePath;

	public bool addpart;
	public List<BoardAssetSystem> Parts = new List<BoardAssetSystem>();

	public bool show;
	public int windowID;
	public Rect windowRect;

	public Rect Grid = new Rect(100,100,100,100);
	public int SelectedPart;
	public int RowAmt;

	public int CurrentLayer;

	public int Drag;

	// Use this for initialization
	void Start()
	{
		windowID = 87;
		FilePath = "D:/gateway_stuff/Parts";
		windowRect = new Rect(100, 100, 450, 450);
		Grid = new Rect(4, windowRect.height - Grid.height - 10, 300, 100);
		RowAmt = 8;
	}

	// Update is called once per frame
	void Update()
	{
		if (LoadTextures == true)
		{
			LoadedTextures.Clear();
			Resources.UnloadUnusedAssets();
			foreach (string file in Directory.GetFiles(FilePath))
			{
				LoadedTexturesPath.Add(file);
				LoadedTextures.Add(TextureLoader.LoadPNG(file));
			}

			LoadedTexturesGrid = LoadedTextures.ToArray();

			LoadTextures = false;
		}

		if (addpart == true)
		{
			Parts.Add(new BoardAssetSystem("", LoadedTexturesPath[SelectedPart],CurrentLayer,new SRect(new Rect(0,0,24,24))));
			addpart = false;
		}
	}

	void OnGUI()
	{
		//GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

		//float rx = Screen.width / native_width;
		//float ry = Screen.height / native_height;

		//GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(rx, ry, 1));

		if (show == true)
		{
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
		}
	}

	void DoMyWindow(int WindowID)
	{
		if (GUI.Button(new Rect(windowRect.width - 23, 2, 21, 21), "X"))
		{
			show = false;
		}

		//GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
		//GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

		GUI.DragWindow(new Rect(2, 2, windowRect.width - 26, 21));
		GUI.Box(new Rect(2, 2, windowRect.width - 26, 21), "Motherboard Creator");

		SelectedPart = GUI.SelectionGrid(Grid, SelectedPart, LoadedTexturesGrid, RowAmt);

		if (GUI.Button(new Rect(Grid.width + 10, windowRect.height - 30, 80, 24), "Add Part"))
		{
			addpart = true;
		}

		RenderParts();
	}

	void RenderParts()
    {
        if (Parts.Count > 0)
        {
			for(int i = 0; i < Parts.Count; i++)
            {

                GUI.DrawTexture(Parts[i].PartPos, TextureLoader.LoadPNG(Parts[i].PartFilePath));

				if(Parts[i].PartPos.Contains(Event.current.mousePosition))
                {
					if(CurrentLayer == Parts[i].Layer)
                    {
						if(Input.GetMouseButton(0))
                        {
							Parts[i].Selected = true;
							Parts[i].PartPos.x = Event.current.mousePosition.x - Parts[i].PartPos.width/2;
							Parts[i].PartPos.y = Event.current.mousePosition.y - Parts[i].PartPos.height/2;
						}
						if(Parts[i].Selected == true)
                        {
							if (Input.mouseScrollDelta.y > 0)
							{
								Parts[i].PartPos.width++;
								Parts[i].PartPos.height++;
							}
							if (Input.mouseScrollDelta.y < 0)
							{
								Parts[i].PartPos.width--;
								Parts[i].PartPos.height--;
							}
						}
					}
                }
            }
        }
    }
}