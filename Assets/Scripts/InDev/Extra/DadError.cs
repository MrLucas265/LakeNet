using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadError : MonoBehaviour
{
	public AudioSource AS;
	public AudioClip AC;
	public float Timer;
	public float CoolDown;
	public float Pitch;
	public int count;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		Test ();
		Timer-=Time.deltaTime;
		AS.pitch = Pitch;

		if (Timer < 0) 
		{
			Timer = CoolDown;
		}
		if (Timer >= CoolDown) 
		{
			count--;
			AS.PlayOneShot(AC);
		}
	}

	void Test()
	{
		switch (count) 
		{
		case 8:
			CoolDown = 0.12f;
			Pitch = 1.025f;
			break;
		case 7:
			CoolDown = 0.12f;
			Pitch = 1.05f;
			break;
		case 6:
			CoolDown = 0.12f;
			Pitch = 1.075f;
			break;
		case 5:
			CoolDown = 0.12f;
			Pitch = 1.05f;
			break;
		case 4:
			CoolDown = 0.12f;
			Pitch = 1.1f;
			break;
		case 3:
			CoolDown = 0.12f;
			Pitch = 1.1f;
			break;
		case 2:
			CoolDown = 0.115f;
			Pitch = 1.15f;
			break;
		case 1:
			CoolDown = 0.115f;
			Pitch = 1.2f;
			break;
		case 0:
			CoolDown = 0.115f;
			Pitch = 1f;
			count = 8;
			break;
		}
	}
}
