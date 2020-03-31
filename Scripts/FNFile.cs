using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class FNFile
{
    public string fileName;
    public string fileContents;
    public int fileSize;
}