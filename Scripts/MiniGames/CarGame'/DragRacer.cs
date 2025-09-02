using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRacer : MonoBehaviour 
{
	public float PositionX;
	public float ThrottlePos;

	public List<float> GearRatio = new List<float>();

	public int CurrentGear;
	public int TotalGears;
	public List<float> maxSpeedsPerGear = new List<float>();

	public float Speed;
	public float Acceleration;

	public float RPM;
	public float MaxRPM;

	public float max;

	// Use this for initialization
	void Start ()
	{
		SetMaxSpeedsPerGear();
		MaxRPM = 8500;
		Acceleration = 100;
	}

	void SetMaxSpeedsPerGear()
	{
		maxSpeedsPerGear.Add (0);
		maxSpeedsPerGear.Add (40);
		maxSpeedsPerGear.Add (70);
		maxSpeedsPerGear.Add (100);
		maxSpeedsPerGear.Add (130);
		maxSpeedsPerGear.Add (170);
	}

	void Math()
	{
		PositionX += Speed / 0.001f;
	}

	void GearUp()
	{
		CurrentGear++;
	}

	void GearDown()
	{
		CurrentGear--;
	}

	public void GameRender()
	{
		if (ThrottlePos >= 1) 
		{
			ThrottlePos = 1;
		}

		if (ThrottlePos <= 0) 
		{
			ThrottlePos = 0;
		}

		if (Speed >= maxSpeedsPerGear [CurrentGear])
		{
			Speed = maxSpeedsPerGear[CurrentGear];
		}

		Speed = ((float)RPM / (float)MaxRPM) * (float)maxSpeedsPerGear[CurrentGear];
		RPM = (MaxRPM * Speed) / maxSpeedsPerGear[CurrentGear] ;

		if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Space)
		{
			GearUp();
		} 

		if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.W) 
		{
			RPM += 0.1f * Acceleration;
		} 
		else
		{
//			if (Speed > 0) 
//			{
//				RPM -= 0.1f * Acceleration;
//			}
		}

		GUI.Button (new Rect (PositionX, 200, 50, 20), "Car");

		GUI.Label (new Rect (5, 50, 200, 20), "Selected Gear " + CurrentGear);
		GUI.Label (new Rect (5, 75, 200, 20), "Throttle Pos " + ThrottlePos.ToString("F2"));

		GUI.Label (new Rect (205, 50, 200, 20), "Speed " + Speed);
		GUI.Label (new Rect (205, 75, 200, 20), "RPM " + RPM);

		Math();
	}
}
