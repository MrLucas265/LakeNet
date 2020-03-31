using UnityEngine;
using System.Collections;

public class MonitorBypass : MonoBehaviour 
{
	public bool Active;
	private WebSec ws;
	// Use this for initialization
	void Start () 
	{
		ws = GetComponent<WebSec>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Active == true)
		{
			Activate ();
		}
		else 
		{
			ws.Monitor = true;
		}
	}

	void Activate()
	{
//		if (hp.Address != "") 
//		{
//			if (ws.MonitorLevel <= GameControl.control.SoftwareVersion [7]) 
//			{
//				ws.Monitor = false;
//			} 
//			else 
//			{
//				ws.Monitor = true;
//			}
//		} 
//		else 
//		{
//			ws.Monitor = true;
//		}
	}

	void DeActivate()
	{

	}
}
