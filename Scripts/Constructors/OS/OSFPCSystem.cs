using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OSFPCSystem
{
    public string BackgroundAddress;
    public string ScreenSaverBackgroundAddress;
    public string ScreenSaverPictureAddress;
    public string MouseCursorAddress;

    public OSFPCSystem(string backgroundaddress, string screensaverbackgroundaddress, string screensaverpictureaddress, string mousecursoraddress)
    {
        BackgroundAddress = backgroundaddress;
        ScreenSaverBackgroundAddress = screensaverbackgroundaddress;
        ScreenSaverPictureAddress = screensaverpictureaddress;
        MouseCursorAddress = mousecursoraddress;
    }

}