using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GatewayStatusSystem
{
    public bool Booting;
    public bool Booted;
    public bool Sleep;
    public bool Off;
    public bool On;
    public bool Active;
    public bool POST;
    public bool BIOS;
    public bool SafeMode;
    public bool Terminal;
    public bool LoggedIn;
    public bool SigningOut;
    public bool SigningIn;
    public bool Shutdown;
    public bool Restart;

    public GatewayStatusSystem(bool booting, bool booted, bool sleep, bool off, bool on, bool active, bool post,bool bios,bool safemode,bool terminal,bool loggedin,bool signingin,bool signingout,bool shutdown)
    {
        Booting = booting;
        Booted = booted;
        Sleep = sleep;
        Off = off;
        On = on;
        Active = active;
        POST = post;
        BIOS = bios;
        SafeMode = safemode;
        Terminal = terminal;
        LoggedIn = loggedin;
        SigningOut = signingout;
        SigningIn = signingin;
        Shutdown = shutdown;
    }

    public GatewayStatusSystem()
    {

    }
}

