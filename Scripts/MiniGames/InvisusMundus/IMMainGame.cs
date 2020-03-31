using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMMainGame : MonoBehaviour 
{
	private GameObject Mundus;
	private IMHud Hud;
	// Use this for initialization
	void Start () 
	{
		Mundus = GameObject.Find("Invisus Mundus");
		Hud = Mundus.GetComponent<IMHud>();
		Hud.enabled = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void GameScreen()
	{
		Hud.RenderHud();
	}
}
