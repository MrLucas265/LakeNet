using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataSystem
{
    public string Name;
    public SVector3 CurPos;
    public float MaxHealth;
    public float CurHealth;
    public float MaxArmor;
    public float CurArmor;
    public int SelectedWeapon; //0 no weapon 1 primary 2 secondary 3 sidearm
    public string PrimaryWeapon;
    public string SecondaryWeapon;
    public string Sidearm;
    public string Skill1;
    public string Skill2;
    public string Skill3;
    public float Money;
    public int Level;
    public float CurXP;
    public float XpToLevel;

    public PlayerDataSystem()
    {
    }
}