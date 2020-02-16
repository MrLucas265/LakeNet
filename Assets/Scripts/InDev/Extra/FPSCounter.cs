﻿using UnityEngine;
using System.Collections;

public class FPSCounter : MonoBehaviour
{

	string label = "";
	float count;

	IEnumerator Start ()
	{
		//GUI.depth = 50;
		while (true) {
			if (Time.timeScale == 1) {
				yield return new WaitForSeconds (0.1f);
				count = (1 / Time.deltaTime);
				label = "FPS :" + (Mathf.Round (count));
			} else {
				label = "Pause";
			}
			yield return new WaitForSeconds (0.5f);
		}
	}

	void OnGUI ()
	{
		GUI.Label (new Rect (5, 40, 100, 25), label);
	}
}