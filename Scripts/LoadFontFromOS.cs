using UnityEngine;

public class LoadFontFromOS : MonoBehaviour
{

    const string ANDROID_FONT_NAME = "Roboto";
    const string IOS_FONT_NAME = "SanFrancisco";

    public string[] OSFonts;
    static Font selectedFont;
    public static Font SelectedFont
    {
        get
        {
            return selectedFont;
        }
    }
    static bool isFontFound;
    public static bool IsFontFound
    {
        get
        {
            return isFontFound;
        }
    }

    private void Awake()
    {
        isFontFound = false;
        OSFonts = Font.GetOSInstalledFontNames();
        foreach (var font in OSFonts)
        {
#if UNITY_ANDROID
            if (font == ANDROID_FONT_NAME) {
                selectedFont = Font.CreateDynamicFontFromOSFont(ANDROID_FONT_NAME, 1);
                isFontFound = true;
            }
#elif UNITY_IOS
            if (font == IOS_FONT_NAME) {
                selectedFont = Font.CreateDynamicFontFromOSFont(IOS_FONT_NAME, 1);
                isFontFound = true;
            }
#endif
        }
    }
}