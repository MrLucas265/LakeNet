//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AutoType : MonoBehaviour 
//{
//	private SoundControl sc;
//	private GameObject Database;

//	private CLI cmd;

//	public AudioClip AudioClips;
//	public AudioSource AudioSoucres;

//	// Use this for initialization
//	void Start () 
//	{
//		AfterStart();
//	}

//	void AfterStart()
//	{
//		Database = GameObject.Find("Computer");
//		sc = Database.GetComponent<SoundControl>();
//		cmd = Database.GetComponent<CLI>();
//	}

//	// Update is called once per frame
//	void Update()
//	{
////		if (cmd.show == true) 
////		{
////			Typing();
////		}
//	}

//	public void InputKeyPress()
//	{
//		if (Input.anyKeyDown) 
//		{
//			if (Input.GetMouseButton (0) || Input.GetMouseButton (1) || Input.GetMouseButton (2))
//			{

//			} 
//			else
//			{
//				AudioSoucres.pitch = Random.Range (0.96f, 1.04f);
//				AudioSoucres.PlayOneShot (AudioClips);
//				AudioSoucres.pitch = 1;
//			}
//		}
//	}

//	void Typing()
//	{
//		if (Input.GetKeyDown(KeyCode.A))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.B))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.C))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.D))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.E))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.F))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}


//		else if (Input.GetKeyDown(KeyCode.G))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.H))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.I))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.J))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.K))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.L))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.M))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.N))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.O))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.P))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.Q))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.R))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.S))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.T))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.U))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.V))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.W))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.X))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.Y))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}

//		else if (Input.GetKeyDown(KeyCode.Z))
//		{
//			sc.SoundSelect = 4;
//			sc.PlaySound();
//		}
//	}
//}
