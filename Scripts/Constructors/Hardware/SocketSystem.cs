using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SocketSystem
{
    public int POSX;
    public int POSY;

    public SocketSystem(int posx,int posy) //,Texture2D icon)
    {
        POSX = posx;
        POSY = posy;
    }
}
