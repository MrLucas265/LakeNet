using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonopolyPlayerSystem
{
    public string Name;
    public int TurnOrder;
    public int Cash;
    public bool Jailed;
    public int JailRolls;
    public int PassTurns;
    public int JailFreeCards;
    public int CurrentPos;
    public int SelectedIcon;


    public MonopolyPlayerSystem(string name,int turnorder, int cash, bool jailed, int jailrolls, int passturns,int jailfreecards,int currentpos,int selectedicon)
    {
        Name = name;
        TurnOrder = turnorder;
        Cash = cash;
        Jailed = jailed;
        JailRolls = jailrolls;
        PassTurns = passturns;
        JailFreeCards = jailfreecards;
        CurrentPos = currentpos;
        SelectedIcon = selectedicon;
    }
}