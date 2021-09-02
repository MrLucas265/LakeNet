// Logitech Gaming SDK
//
// Copyright (C) 2011-2014 Logitech. All rights reserved.
// Author: Tiziano Pigliucci
// Email: devtechsupport@logitech.com

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class LogiStart : MonoBehaviour
{
    public float Timer;
    byte[] pixelMatrix;

    public int red;
    public int green;
    public int blue;
    public int alpha;

    public bool Enabled;


    // Use this for initialization
    void Start()
    {
        Enabled = false;

        if(Enabled == true)
        {
            LogitechGSDK.LogiLcdInit("14K3N37", 3);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Enabled == true)
        {
            int StoredPage = GameControl.control.LCDPage;

            if (GameControl.control.Gateway.Status.Shutdown == true)
            {
                LogitechGSDK.LogiLcdShutdown();
            }

            //BUTTON TEST
            String colorButtons = "";
            String monoButtons = "";
            if (LogitechGSDK.LogiLcdIsButtonPressed(LogitechGSDK.LOGI_LCD_COLOR_BUTTON_CANCEL))
                colorButtons += "Cancel";
            if (LogitechGSDK.LogiLcdIsButtonPressed(LogitechGSDK.LOGI_LCD_COLOR_BUTTON_DOWN))
                colorButtons += "Down";
            if (LogitechGSDK.LogiLcdIsButtonPressed(LogitechGSDK.LOGI_LCD_COLOR_BUTTON_LEFT))
            {
                colorButtons += "Left";
                GameControl.control.LCDPage--;
            }
            if (LogitechGSDK.LogiLcdIsButtonPressed(LogitechGSDK.LOGI_LCD_COLOR_BUTTON_MENU))
                colorButtons += "Menu";
            if (LogitechGSDK.LogiLcdIsButtonPressed(LogitechGSDK.LOGI_LCD_COLOR_BUTTON_OK))
                colorButtons += "Ok";
            if (LogitechGSDK.LogiLcdIsButtonPressed(LogitechGSDK.LOGI_LCD_COLOR_BUTTON_RIGHT))
            {
                colorButtons += "Right";
                GameControl.control.LCDPage++;
            }
            if (LogitechGSDK.LogiLcdIsButtonPressed(LogitechGSDK.LOGI_LCD_COLOR_BUTTON_UP))
                colorButtons += "Up";
            if (LogitechGSDK.LogiLcdIsButtonPressed(LogitechGSDK.LOGI_LCD_MONO_BUTTON_0))
                monoButtons += "Button 0";
            if (LogitechGSDK.LogiLcdIsButtonPressed(LogitechGSDK.LOGI_LCD_MONO_BUTTON_1))
                monoButtons += "Button 1";
            if (LogitechGSDK.LogiLcdIsButtonPressed(LogitechGSDK.LOGI_LCD_MONO_BUTTON_2))
                monoButtons += "Button 2";
            if (LogitechGSDK.LogiLcdIsButtonPressed(LogitechGSDK.LOGI_LCD_MONO_BUTTON_3))
                monoButtons += "Button 3";

            // LogitechGSDK.LogiLcdMonoSetText(0, monoButtons);
            // LogitechGSDK.LogiLcdColorSetText(5, colorButtons, 255, 255, 0);

            if (GameControl.control.LCDPage < 0)
            {
                GameControl.control.LCDPage = 0;
            }

            if (GameControl.control.LCDPage != StoredPage)
            {
                for (int i = 0; i < 8; i++)
                {
                    LogitechGSDK.LogiLcdColorSetText(i, "", 0, 0, 0);
                }
            }

            if (GameControl.control.ChangeColor == true)
            {

                Timer -= 1 * Time.deltaTime;

                if (Timer <= 0)
                {
                    Timer = 1;
                    RaveParty();
                }
            }

            LogitechGSDK.LogiLcdUpdate();
        }
    }

    void RandomColor()
    {
        System.Random random = new System.Random();
        red = random.Next(0, 255);
        blue = random.Next(0, 255);
        green = random.Next(0, 255);
        alpha = random.Next(0, 255);
    }

    void RaveParty()
    {
        pixelMatrix = new byte[307200];
        RandomColor();
        for (int i = 0; i < 307200; i++)
        {

            if ((i % 1) == 0) pixelMatrix[i] = (byte)blue; // blue
            if ((i % 2) == 0) pixelMatrix[i] = (byte)green; // green
            if ((i % 3) == 0) pixelMatrix[i] = (byte)red; // red
            if ((i % 4) == 0) pixelMatrix[i] = (byte)alpha; // red


        }

        RandomColor();
        LogitechGSDK.LogiLcdColorSetBackground(pixelMatrix);
        LogitechGSDK.LogiLcdColorSetText(0, "JUST", red, blue, green);

        RandomColor();
        LogitechGSDK.LogiLcdColorSetBackground(pixelMatrix);
        LogitechGSDK.LogiLcdColorSetText(1, "LIVING", red, blue, green);

        RandomColor();
        LogitechGSDK.LogiLcdColorSetBackground(pixelMatrix);
        LogitechGSDK.LogiLcdColorSetText(2, "IN", red, blue, green);

        RandomColor();
        LogitechGSDK.LogiLcdColorSetBackground(pixelMatrix);
        LogitechGSDK.LogiLcdColorSetText(3, "THE", red, blue, green);

        RandomColor();
        LogitechGSDK.LogiLcdColorSetBackground(pixelMatrix);
        LogitechGSDK.LogiLcdColorSetText(4, "DATABASE", red, blue, green);

        RandomColor();
        LogitechGSDK.LogiLcdColorSetBackground(pixelMatrix);
        LogitechGSDK.LogiLcdColorSetText(5, "WOH ", red, blue, green);

        RandomColor();
        LogitechGSDK.LogiLcdColorSetBackground(pixelMatrix);
        LogitechGSDK.LogiLcdColorSetText(6, "OWW", red, blue, green);
    }

    void OnDestroy()
    {
        LogitechGSDK.LogiLcdShutdown();
    }
}
