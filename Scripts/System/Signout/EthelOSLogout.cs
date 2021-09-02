using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthelOSLogout : MonoBehaviour
{
    public string SignoutMessage;
    public float Timer;
    public Rect windowRect;
    public int windowID;
    public GUISkin crashskin;

    public float SecTime;

    public Texture2D LogoutBackground;

    public Color32 Color1 = new Color32(0, 0, 0, 0);

    // Use this for initialization
    void Start()
    {
        //if (Application.isEditor == true)
        //{
        //    windowRect = new Rect(0, 0, Screen.width, Screen.height);
        //}
        //else
        //{
        //    windowRect = new Rect(0, 0, Customize.cust.RezX, Customize.cust.RezY);
        //}
        windowRect = new Rect(0, 0, Screen.width, Screen.height);
        LoadPresetColors();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Timers()
    {
        if (Timer > 0)
        {
            Timer -= 1 * Time.deltaTime;
            if (Color1.a < 255)
            {
                SecTime -= Time.deltaTime;
            }
        }
        if (SecTime <= 0)
        {
            SecTime = 1;
            Color1.a += 20;
        }
        if (Timer <= 0)
        {
            Application.LoadLevel(0);
        }
    }

    void LoadPresetColors()
    {
        Color1.r = 255;
        Color1.g = 255;
        Color1.b = 255;
        Color1.a = 255;
    }

    void OnGUI()
    {
        //GUI.Box (new Rect (0, 0, Screen.width, Screen.height), "");
        GUI.skin = crashskin;
        //GUI.color = Color1;
        GUI.backgroundColor = Color1;
        GUI.FocusWindow(windowID);
        windowRect = GUI.Window(windowID, windowRect, DoMyWindow, "");
    }

    void DoMyWindow(int WindowID)
    {
        GUI.backgroundColor = Color1;
        GUI.contentColor = Color.white;
        //GUI.Box(new Rect (0, 0, windowRect.width,windowRect.height), LogoutBackground);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), LogoutBackground);
        //GUI.Box(new Rect(0, 0, windowRect.width, windowRect.height), "");
        GUI.Label(new Rect(windowRect.width / 2, windowRect.height / 2, 500, 22), "Signing Out.");
    }
}
