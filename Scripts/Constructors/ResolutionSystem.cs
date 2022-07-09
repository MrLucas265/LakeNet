using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResolutionSystem
{
    public string RezName;
    public string AspectRatio;
    public int RezX;
    public int RezY;
    public bool Fullscreen;

    public ResolutionSystem(string rezname,string aspectratio, int rezx,int rezy,bool fullscreen)
    {
        RezName = rezname;
        AspectRatio = aspectratio;
        RezX = rezx;
        RezY = rezy;
        Fullscreen = fullscreen;
    }
}