using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PeopleFaceTestSys1
{
    public string Path;
    public Texture2D Photo;

    public PeopleFaceTestSys1(string path, Texture2D photo)
    {
        Path = path;
        Photo = photo;
    }
}