using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FileSystem
{
    public List<ProgramSystem> FileList = new List<ProgramSystem>();
    public List<ProgramSystem> QuickList = new List<ProgramSystem>();
    public List<ProgramSystem> DesktopList = new List<ProgramSystem>();

    public FileSystem(List<ProgramSystem> files, List<ProgramSystem> quicklist, List<ProgramSystem> desktoplist)
    {
        FileList = files;
        QuickList = quicklist;
        DesktopList = desktoplist;
    }
}
