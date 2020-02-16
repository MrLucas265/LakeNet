using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpreedSheetSystem
{
    public int Coloum;
    public int Row;
    public string Inputted;
    public string Displayed;

    public SpreedSheetSystem(int coloum, int row,string inputted, string displayed)
    {
        Coloum = coloum;
        Row = row;
        Inputted = inputted;
        Displayed = displayed;
    }
}