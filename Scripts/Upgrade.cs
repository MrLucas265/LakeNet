using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Upgrade : MonoBehaviour 
{
	private GameObject Hacking;

    private Progtive pro;
	private Tracer tt;
	public List<float> ProgtiveDiskList = new List<float>();
	public List<float> TraceDiskList = new List<float>();
	public string Selected;
	public string Specs;
	public string Desc;
	public bool Display;
	public int Cost;

	// Use this for initialization
	void Start ()
    {
		Hacking = GameObject.Find("Hacking");

		pro = Hacking.GetComponent<Progtive>();
		tt = Hacking.GetComponent<Tracer>();
		UpdateList();
	}

	void UpdateList()
	{
		ProgtiveDiskList.Insert(0,0);
		ProgtiveDiskList.Insert(1,5);
		ProgtiveDiskList.Insert(2,7);
		ProgtiveDiskList.Insert(3,10);
		ProgtiveDiskList.Insert(4,10);
		ProgtiveDiskList.Insert(5,10);
		ProgtiveDiskList.Insert(6,10);
		ProgtiveDiskList.Insert(7,10);
		ProgtiveDiskList.Insert(8,10);
		ProgtiveDiskList.Insert(9,10);
		ProgtiveDiskList.Insert(10,10);
		ProgtiveDiskList.Insert(11,10);

		TraceDiskList.Insert(1,1);

	}
	// Update is called once per frame\
	void Update ()
    {
//		switch (GameControl.control.SoftwareVersion [3])
//		{
//		case 1:
//			tt.CPUUsage = 1;
//			tt.RAMUsage = 750;
//			tt.GPUUsage = 1f;
//			tt.CPUUsageWE = 1;
//			tt.RAMUsageWE = 250;
//			tt.GPUUsageWE = 1;
//			tt.DiskUsage = 1;
//			break;
//		}
	}
}
