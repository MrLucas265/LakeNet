using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardAssetSystem
{
    public string PartName;
    public string PartFilePath;
    public SRect PartPos;
    public int Layer;
    public bool Drag;
    public bool Selected;

    public BoardAssetSystem() {}

    public BoardAssetSystem(string name)
    { PartName = name; }

    public BoardAssetSystem(string name,string path) 
    { PartName = name; PartFilePath = path; }
    public BoardAssetSystem(string name, string path,int layer)
    { PartName = name; PartFilePath = path; Layer = layer; }

    public BoardAssetSystem(string name, string path, int layer,SRect pos) 
    { PartName = name; PartFilePath = path; Layer = layer; PartPos = pos; }
}