using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InfectionSystem
{
	public InfectionType Type;
	public bool Payload;
	public bool Spread;
	public bool Infected;
	public float Timer;
	public float InitalTimer;
	public float Percentage;
	public float Version;
	public string PayloadDate;

	public enum InfectionType
	{
		Null,
		Malware,
		Worm,
		Keylogger,
		Virus
	}

	public InfectionSystem(InfectionType type, bool payload, bool spread, bool infected, float timer, float initaltimer,float percentage,float version,string payloaddate) //,Texture2D icon)
	{
		Type = type;
		Payload = payload;
		Spread = spread;
		Infected = infected;
		Timer = timer;
		InitalTimer = initaltimer;
		Percentage = percentage;
		Version = version;
		PayloadDate = payloaddate;
	}
}