using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class COGSystem
{
	public string Name;
	public string Description;
	public string Team;
	public float Price;
	public float Health;
	public float Damage;
	public float DeployTime;

	public COGSystem(string name, string description,string team, float price,float health, float damage, float deploytime)
	{
		Name = name;
		Description = description;
		Team = team;
		Price = price;
		Health = health;
		Damage = damage;
		DeployTime = deploytime;
	}
}