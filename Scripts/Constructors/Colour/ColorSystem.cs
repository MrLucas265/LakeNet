using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorSystem
{
    public ButtonColorSystem Button;
    public FontColorSystem Font;
    public WindowColorSystem Window;

    public ColorSystem(ButtonColorSystem button, FontColorSystem font, WindowColorSystem window)
    {
        Button = button;
        Font = font;
        Window = window;
    }

}