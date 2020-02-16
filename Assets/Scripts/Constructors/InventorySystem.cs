using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySystem
{
	public string Name;
	public string Desc;
	public float Qty;
	public float Value;
	public float Weight;
	public float Stat;
	//public Texture2D Icon;
	//public Texture2D Icon;

	public enum ItemType
	{
		Stats,
		Weapon,
		Shield,
		Clothing,
		Armour,
		Transport,
		Generic,
		Currency,
		Consumable,
		Evidence,
		Furniture,
		House,
		Quest
	}

	public InventorySystem(string name,string desc,float qty,float value,float weight,float stat, ItemType type) //,Texture2D icon)
	{
		Name = name;
		Desc = desc;
		Qty = qty;
		Value = value;
		Weight = weight;
		Stat = stat;
	}
}