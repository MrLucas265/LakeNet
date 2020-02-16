using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonopolyPropSystem
{
    public string Name;
    public int HouseCost;
    public int HotelCost;
	public int PurchasePrice;
    public int Rent;
    public int MortageValue;
    public bool Owned;
    public int Owner;
    public bool Ownable;
    public int TotalHouses;
    public bool Mortaged;
    public int TileLocation;
	public PropType Type;
    public PropColor Colour;

    public enum PropType
    {
		None,
		Normal,
		Train,
		Utility,
		Chance,
        Chest,
		Community,
        Tax,
        Spin,
        Jail,
        Police,
        FreeParking,
        Start
	}

    public enum PropColor
    {
        None,
        Red,
        Yellow,
        Green,
        Blue,
        Purple,
        Pink,
        Orange,
        Cyan,
        Grey,
        Black
    }

    public MonopolyPropSystem(string name,int housecost,int hotelcost,int purchaseprice,int rent,int mortagevalue,bool owned,int owner,bool ownable,int totalhouses,bool mortaged,int tilelocation,PropType type, PropColor colour)
	{
		Name = name;
		HouseCost = housecost;
		HotelCost = hotelcost;
        PurchasePrice = purchaseprice;
        Rent = rent;
        MortageValue = mortagevalue;
        Owned = owned;
        Owner = owner;
        Ownable = ownable;
        TotalHouses = totalhouses;
        Mortaged = mortaged;
        TileLocation = tilelocation;
        Type = type;
        Colour = colour;
    }
}