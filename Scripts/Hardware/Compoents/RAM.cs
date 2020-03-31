using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RAM : MonoBehaviour
{
	public float MaxRAM;
	public float RemainingRAM;
	public float UsedRAM;

    void Start () 
	{

    }

	void Update ()
	{
		Math();
	}

	void Math()
	{
        MaxRAM = 0;
        for (int i = 0; i < GameControl.control.Gateway.InstalledRAM.Count; i++)
        {
            MaxRAM = MaxRAM + GameControl.control.Gateway.InstalledRAM[i].Max;
        }

        RemainingRAM = MaxRAM - UsedRAM;
    }
}
