using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebsiteFileCount : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
	{
		GUI.Label (new Rect(Screen.width / 2, Screen.height / 2, 100, 22),"" + GameControl.control.WebsiteFiles.Count);
	}
}
