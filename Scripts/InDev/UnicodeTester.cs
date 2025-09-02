using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicodeTester : MonoBehaviour
{
	public string Test;
	public string Input;
	// Use this for initialization
	void Start ()
	{
		Test = ">▮";
		Input += Test;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
    {
		GUI.Button(new Rect(0, Screen.height / 2, 200, 21), Test);
    }
}
