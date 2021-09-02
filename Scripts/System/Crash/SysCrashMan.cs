using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SysCrashMan : MonoBehaviour 
{
	public string Type;

	public string StopCodeNumber;
	public string StopCodeWord;
	public string CodeDetail;
	public string ExtraDetail;

	private BlueCrash bc;
	private YellowCrash yc;
	// Use this for initialization
	void Start ()
	{
		bc = GetComponent<BlueCrash>();
		yc = GetComponent<YellowCrash>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (GameControl.control.SelectedOS.Name)
		{
		case OperatingSystems.OSName.FluidicIceOS:
			if (bc.Timer == 0)
			{
				bc.Timer = 10;
			}
			bc.StopCodeWord = StopCodeWord;
			bc.StopCodeNumber = StopCodeNumber;
			bc.CodeDetail = CodeDetail;
			bc.ExtraDetail = ExtraDetail;
			bc.enabled = true;
			bc.Timers ();
		break;

		case OperatingSystems.OSName.EthelOS:
			if (yc.Timer == 0)
			{
				yc.Timer = 10;
			}
			yc.StopCodeWord = StopCodeWord;
			yc.StopCodeNumber = StopCodeNumber;
			yc.CodeDetail = CodeDetail;
			yc.ExtraDetail = ExtraDetail;
			yc.enabled = true;
			yc.Timers();
		break;

		case OperatingSystems.OSName.TreeOS:
            if (bc.Timer == 0)
            {
                bc.Timer = 10;
            }
            bc.StopCodeWord = StopCodeWord;
            bc.StopCodeNumber = StopCodeNumber;
            bc.CodeDetail = CodeDetail;
            bc.ExtraDetail = ExtraDetail;
            bc.enabled = true;
            bc.Timers();
         break;
        }
	}

//	void SystemCrashSelector()
//	{
//		switch (Type)
//		{
//		case "Test":
//			bc.enabled = true;
//			break;
//		}
//	}
}
