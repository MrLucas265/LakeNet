using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompanyServerSystem
{
    public string Name;
    public string IP;
    public List<RemoteFileSystem> WebPages = new List<RemoteFileSystem>();
    public List<RemoteFileSystem> Files = new List<RemoteFileSystem>();
    public List<RemoteFileSystem> QuickList = new List<RemoteFileSystem>();
    public List<RemoteFileSystem> DesktopList = new List<RemoteFileSystem>();
    public ServerType Type;

    public enum ServerType
    {
        PC,
        Webserver,
        FileServer,
        EmailServer,
        PhoneServer
    }

    public CompanyServerSystem(string name,string ip, List<RemoteFileSystem> webpages, List<RemoteFileSystem> files, List<RemoteFileSystem> quicklist, List<RemoteFileSystem> desktoplist, ServerType type)
    {
        Name = name;
        IP = ip;
        WebPages = webpages;
        Files = files;
        QuickList = quicklist;
        DesktopList = desktoplist;
        Type = type;
    }
}
