using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ECSystems
{
	public string Name;
	public string Description;
	public float Price;
	public float Production;
	public float Cooldown;
	public float Timer;
	public float Health;
	public float Effeciency;
	public int Owned;

	public ECSystems(string name, string description, float price,float production,float cooldown,float timer,float health,float effeciency, int owned)
	{
		Name = name;
		Description = description;
		Price = price;
		Production = production;
		Cooldown = cooldown;
		Timer = timer;
		Health = health;
		Effeciency = effeciency;
		Owned = owned;
	}
}