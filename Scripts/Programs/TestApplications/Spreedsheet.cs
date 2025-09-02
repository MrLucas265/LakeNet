using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spreedsheet : MonoBehaviour
{
    public bool show;
    public int windowID;
    public Rect windowRect;
    public float native_width = 1920;
    public float native_height = 1080;
    public bool Drag;

    public bool close;
    public bool execute;

    private Computer com;

    private GameObject SysSoftware;

    public float timer;
    public float startTime;

    public float percentage;
    public float StartingCount;
    public float CurrentCount;

    public string Password;
    public string CurrentWord;

    public bool Matched;

    public Rect CloseButton;
    public Rect ExecuteButton;

    public int WordCount;

    public int Coloums = 26;
    public int Rows;

    public float ColoumWidth;
    public float RowHeight;
    public List<SpreedSheetSystem> Cells = new List<SpreedSheetSystem>();
    public List<SpreedSheetSystem> CCells = new List<SpreedSheetSystem>();

    public int SelectedRow;
    public int SelectedCol;

    // Progtive is the one at a time sequential cracker
    // Use this for initialization
    void Start()
    {
        SysSoftware = GameObject.Find("System");

        com = SysSoftware.GetComponent<Computer>();

        windowRect = new Rect(100, 100, 460, 415);

        CloseButton = new Rect(windowRect.width - 22, 1, 21, 21);
        ExecuteButton = new Rect(45, 100, 60, 24);

        windowID = 99;

        ColoumWidth = 50;
        RowHeight = 22;

        CreateDefaultCells();
    }

    void CreateDefaultCells()
    {
        CCells.Add(new SpreedSheetSystem(0, 0, "", "A"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "B"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "C"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "D"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "E"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "F"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "G"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "H"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "I"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "J"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "K"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "L"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "M"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "N"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "O"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "P"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "Q"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "R"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "S"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "T"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "U"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "V"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "W"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "X"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "Y"));
        CCells.Add(new SpreedSheetSystem(0, 0, "", "Z"));

    }

    void Close()
    {
        show = false;
        this.enabled = false;
    }

    void OnGUI()
    {
        Customize.cust.windowx[windowID] = windowRect.x;
        Customize.cust.windowy[windowID] = windowRect.y;
        GUI.skin = GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")];

        if (show == true)
        {
            GUI.color = Registry.Get32ColorData("Player", "System", "WindowColor");
            windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID, windowRect, DoMyWindow, ""));
        }
    }

    void DoMyWindow(int WindowID)
    {

        if (CloseButton.Contains(Event.current.mousePosition))
        {
            if (GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[0]))
            {
                Close();
            }
        }
        else
        {
            GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
            GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");
            GUI.Button(new Rect(CloseButton), "X", GameControl.control.Skins[Registry.GetIntData("Player", "System", "Skin")].customStyles[1]);
        }

        GUI.backgroundColor = Registry.Get32ColorData("Player", "System", "ButtonColor");
        GUI.contentColor = Registry.Get32ColorData("Player", "System", "FontColor");

        GUI.DragWindow(new Rect(1, 1, windowRect.width - 22, 21));
        GUI.Box(new Rect(1, 1, windowRect.width - 22, 21), "Spreedsheet");

        KeyboardControls();

        RenderCells();
    }

    void KeyboardControls()
    {
        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.DownArrow)
        {
            SelectedCol++;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.UpArrow)
        {
            SelectedCol--;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.RightArrow)
        {
            SelectedRow++;
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.LeftArrow)
        {
            SelectedRow--;
        }
    }

    void RenderCells()
    {
        Rows = 0;
        int coloums = 0;
        float x = 0;
        float y = 0;
        float Celly = 0;
        float Cellx = 0;

        for (int i = 0; i < CCells.Count; i++)
        {
            GUI.Box(new Rect(x += ColoumWidth + 1, y + 22 + 1, ColoumWidth, RowHeight), CCells[i].Displayed);
        }
        
        //for (int i = 0; i < RCells.Count; i++)
        //{
        //    GUI.Box(new Rect(2, 23 * i + 46, ColoumWidth, RowHeight), RCells[i].Displayed);
        //}

        if (Cells.Count > 0)
        {
            for (int j = 0; j < Cells.Count; j++)
            {
                x += ColoumWidth + 1;
                GUI.Box(new Rect(2, 23 * Rows + 46, ColoumWidth-3, RowHeight), "" + Rows);
                GUI.TextField(new Rect(Cellx, Celly, ColoumWidth, RowHeight), Cells[j].Displayed);
                //GUI.Label (new Rect (x+IconWidth/xmod, y+IconHeight+15, 200, 23), GameControl.control.DesktopIconList [i].Name,BTextSize);
                Cells[j].Coloum = coloums;
                Cellx = 51 * Cells[j].Coloum + 51;
                coloums++;
                Cells[j].Row = Rows;
                Celly = 23 * Cells[j].Row + 46;
                //y += 22 + 1;
                if (coloums == 25)
                {
                    coloums = 0;
                    x = 0;
                    y += 22 + 1;
                    Rows++;
                }
            }
        }
    }
}
