using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestSite : MonoBehaviour 
{
	public Vector2 scrollpos = Vector2.zero;
	public int scrollsize;

	public int Select;
	public string SelectedProgram;
	public List<string> ListOfSoftware = new List<string>();
	public int Price;
	public float Size;
	public int Version;
	public string Desc;
	public int MaxProgramVersion;
	public int SelectedVersion;
	public string ProgramName;
	public int ProgramID;

	private GameObject Computer;
	private InternetBrowser ib;

	private ErrorProm ep;
	private Upgrade upg;
	private Defalt defalt;
	private PurchasePrompt pp;

	private Progtive prog;
	private Tracer trace;

	public bool Buying;

	void Start()
	{
		Computer = GameObject.Find("Computer");
	}

	public void RenderSite()
	{
		
	}
}

// A7XPB3