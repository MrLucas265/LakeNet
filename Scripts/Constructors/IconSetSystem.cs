using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IconSetSystem
{
    public string IconSetName;
    public List<Texture2D> OSBaseIcons = new List<Texture2D>();
    public List<Texture2D> OSHighlightIcons = new List<Texture2D>();

    public IconSetSystem(string iconsetname, List<Texture2D> osbaseicons,List<Texture2D> oshighlighticons)
    {
        IconSetName = iconsetname;
        OSBaseIcons = osbaseicons;
        OSHighlightIcons = oshighlighticons;
    }
}
