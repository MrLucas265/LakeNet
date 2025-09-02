using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextureWNameSystem
{
    public string Name;
    public Texture2D Texture;

    public TextureWNameSystem(string name)
    {
        Name = name;
    }
}