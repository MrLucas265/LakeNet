using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class StockTransactionHistory : MonoBehaviour
{
    public List<string> ListOfCompaniesNames = new List<string>();


    public List<string> ListOfSavedCompanies = new List<string>();

    public List<StockExchangeShareSystem> Exchange = new List<StockExchangeShareSystem>();

    public List<int> StockPrice = new List<int>();

    public Vector2 scrollpos = Vector2.zero;
    public int scrollsize;

    private GameObject AppSoftware;
    private GameObject SysSoftware;

    private Clock clk;
    private InternetBrowser ib;
    private Defalt def;

    public float cd;
    public float Cooldown;

    public bool showSellMenu;

    public int Index;

    public List<Color> Colors = new List<Color>();
    public Color32 rgb1 = new Color32(0, 0, 0, 0);
    public Color32 ButtonColor = new Color32(0, 0, 0, 0);
    public Color32 FontColor = new Color32(0, 0, 0, 0);

    public int ColorSelect;

    public bool EnableTime;

    public float Count;
    public float SLCount;

    public string ExchangeName;

    public int SelectedCompany;
    public int Amount;
    public string ShareQTY;
    public float CurrentSharePrice;

    // Use this for initialization
    void Start()
    {
        ExchangeName = "Memes Exchange";
        AppSoftware = GameObject.Find("Applications");
        SysSoftware = GameObject.Find("System");
        WebSearch();
        LoadPresetColors();
        EnableTime = true;
        Cooldown = 2;
    }

    void WebSearch()
    {
        ib = AppSoftware.GetComponent<InternetBrowser>();
        def = SysSoftware.GetComponent<Defalt>();
    }
    void LoadPresetColors()
    {
        rgb1.r = 100;
        rgb1.g = 100;
        rgb1.b = 100;
        rgb1.a = 255;

        ButtonColor.r = 75;
        ButtonColor.g = 75;
        ButtonColor.b = 75;
        ButtonColor.a = 255;

        FontColor.r = 255;
        FontColor.g = 255;
        FontColor.b = 255;
        FontColor.a = 255;
    }


    public void RenderSite()
    {
        GUI.backgroundColor = ButtonColor;
        GUI.contentColor = FontColor;
        //GUI.color = rgb1;
        RenderReady();
    }

    void RenderReady()
    {
        GUI.Button(new Rect(2, 25, 174, 21), "Company Name");

        GUI.Button(new Rect(177, 25, 120, 21), "Traded Date");

        GUI.Button(new Rect(278+20, 25, 80, 21), "Price");

        GUI.Button(new Rect(359+20, 25, 60, 21), "QTY");

        GUI.Button(new Rect(420+20, 25, 40, 21), "Type");

        //GUI.Label(new Rect(205,55,100,100),"Selected: " + GStockSave.stocks.SelectedCompanyIndex);

        GUI.contentColor = Color.white;

        if (GameControl.control.TransactionHistory.Count > 0)
        {
            scrollpos = GUI.BeginScrollView(new Rect(0, 47, 499, 196), scrollpos, new Rect(0, 0, 0, scrollsize * 22));
            for (scrollsize = 0; scrollsize < GameControl.control.TransactionHistory.Count; scrollsize++)
            {

                GUI.contentColor = Color.white;

                if (GUI.Button(new Rect(2, scrollsize * 22, 174, 21), "" + GameControl.control.TransactionHistory[scrollsize].Company))
                {

                }

                if (GUI.Button(new Rect(177, scrollsize * 22, 120, 21), "" + GameControl.control.TransactionHistory[scrollsize].PDate))
                {

                }

                if (GUI.Button(new Rect(278+20, scrollsize * 22, 80, 21), "$" + GameControl.control.TransactionHistory[scrollsize].Price))
                {

                }

                if (GUI.Button(new Rect(359+20, scrollsize * 22, 60, 21), "" + GameControl.control.TransactionHistory[scrollsize].Ammount))
                {

                }

                if (GUI.Button(new Rect(420+20, scrollsize * 22, 40, 21), "" + GameControl.control.TransactionHistory[scrollsize].Abv))
                {

                }
            }
            GUI.EndScrollView();
        }
    }
}
