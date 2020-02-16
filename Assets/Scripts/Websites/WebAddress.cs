using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAddress : MonoBehaviour
{
	public List<string> IPAddress = new List<string>();
	public int IP1;
	public int IP2;
	public int IP3;
	public int IP4;

	public void IPGenerator()
	{
		IP1 = Random.Range (0, 255);
		IP2 = Random.Range (0, 255);
		IP3 = Random.Range (0, 255);
		IP4 = Random.Range (0, 255);

		IPAddress.Add ("" + IP1 + "." + IP2 + "." + IP3 + "." + IP4);
	}
}
