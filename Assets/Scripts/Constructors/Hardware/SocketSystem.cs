using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SocketSystem
{
    public string Name;
    public int POSX;
    public int POSY;
    public int SelectedImageNumber;

    public SocketSystem(string name,int posx,int posy,int selectedimagenumber) //,Texture2D icon)
    {
        Name = name;
        POSX = posx;
        POSY = posy;
        SelectedImageNumber = selectedimagenumber;
    }
}
