//using JetBrains.Annotations;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//using UnityEngine.Rendering;
//using static UnityEngine.Rendering.DebugUI;

//class DistinctItemComparer : IEqualityComparer<RegistrySystem>
//{
//    public bool Equals(RegistrySystem x, RegistrySystem y)
//    {
//        return x.KeyNameEnum == y.KeyNameEnum &&
//            x.Values == y.Values;
//    }

//    public int GetHashCode(RegistrySystem obj)
//    {
//        return obj.KeyNameEnum.GetHashCode() ^
//            obj.Values.GetHashCode();
//    }
//}

//public class RegistryLoader : MonoBehaviour
//{
//    public static RegistryLoader RegLoad;

//    // Use this for initialization
//    public bool RunRegLoad;
//    public bool RunRegCheck;

//    public List<RegistrySystem> DefaultRegistryKeys = new List<RegistrySystem>();

//    public List<RegistrySystem> list1 = new List<RegistrySystem>();

//    void Awake()
//    {
//        DefaultKeys();

//        RegLoad = this;
//    }



//    // Update is called once per frame
//    void Update()
//    {
//        if (RunRegLoad == true)
//        {
//            CheckRegKeys();
//        }
//        //if(RunRegCheck == true)
//        //      {
//        //	CheckNullRegInfo();
//        //      }
//    }


//    void Check2()
//    {

//    }

//    void ValueData()
//    {
//        Parallel.For(0, DefaultRegistryKeys.Count, i =>
//        {
//            Parallel.For(0, DefaultRegistryKeys[i].Values.Count, j =>
//            {
//                DefaultRegistryKeys[i].Valuesv2.Add(new RegistryValueSystem(DefaultRegistryKeys[i].Values[j].ValueName));
//            });

//        });
//        RunRegLoad = true;
//    }

//    void DefaultKeys()
//    {
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.Core));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.CLI));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.FileManager));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.OS));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.MediaPlayer));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.System));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.ControlPanel));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.Network));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.Calculator));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.Notepad));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.FileUtility));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.WindowManager));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.PlayerData));
//        DefaultRegistryKeys.Add(new RegistrySystem(RegistrySystem.KeyNames.Discord));

//        Parallel.For(0, DefaultRegistryKeys.Count, i =>
//        {
//            switch (DefaultRegistryKeys[i].KeyNameEnum)
//            {
//                case RegistrySystem.KeyNames.Core:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("RunProgram"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Clipboard"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InstalledPrograms"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Action"));
//                    break;
//                case RegistrySystem.KeyNames.FileManager:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedDirectory"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CurrentDirectory"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MainScrollPos"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MainScrollSize"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("UserName"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedMenu"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LastClick"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedFile"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColor"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColor"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColor"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Test 1"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("PageHistory"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowRect"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Ribbon"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("RenderedRibbon"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedButton"));
//                    break;
//                case RegistrySystem.KeyNames.Calculator:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Result"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Value"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Value1"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Pastop"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Operator"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("OperatorPressed"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FirstNumber"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondNumber"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ResultNumber"));
//                    break;
//                case RegistrySystem.KeyNames.CLI:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WorkingPath"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CommandLine"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Pinned"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CustomPos"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedPastCommand"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Scrollsize"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollPos"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CommandHistory"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TextFieldPos"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Input"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Output"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Skin"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColor"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColor"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColor"));
//                    break;
//                case RegistrySystem.KeyNames.System:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Skin"));

//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColor"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColor"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColor"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryFontColor"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryWindowColor"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryButtonColor"));

//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedBackground"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ShowDesktopIcons"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ShowDesktopBackground"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("DesktopBackgroundColor"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("DoubleClickSpeed"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ForcedBackground"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("UpdatePinLists"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Test"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Aspect"));

//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColorRedFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColorGreenFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColorBlueFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ButtonColorAlphaFloat"));

//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColorRedFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColorGreenFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColorBlueFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FontColorAlphaFloat"));

//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColorRedFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColorGreenFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColorBlueFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowColorAlphaFloat"));

//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryButtonColorRedFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryButtonColorGreenFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryButtonColorBlueFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryButtonColorAlphaFloat"));

//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryFontColorRedFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryFontColorGreenFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryFontColorBlueFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryFontColorAlphaFloat"));

//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryWindowColorRedFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryWindowColorGreenFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryWindowColorBlueFloat"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SecondaryWindowColorAlphaFloat"));

//                    break;
//                case RegistrySystem.KeyNames.ControlPanel:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ShowResize"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("BackgroundField"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("BackgroundAddress"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Window"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Resize"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedAddress"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MaxPerRowGrid"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("RowCount"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollRect"));
//                    break;
//                case RegistrySystem.KeyNames.OS:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("BackgroundPath"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CursorPath"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Secondary"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ColorOption"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SerialKey"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("DefaultVolumeAddress"));
//                    break;
//                case RegistrySystem.KeyNames.MediaPlayer:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Path"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Looping"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Loop"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedFile"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CurrentFilePath"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LoadVideo"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("HideUI"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Menu"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LoadPlaylist"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CloseButton"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MiniButton"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SettingsButton"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ListButton"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("HomeButton"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("DefaltBoxSetting"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("VideoRect"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Volume"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Fullscreen"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Exists"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Seek"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Startup"));


//                    //Scroll
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollRect"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollPos"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollView"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ScrollSize"));
//                    //UIControlsPositions
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("PlayUI"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("BackUI"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FowardUI"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("StopUI"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("LoopUI"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MuteUI"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SeekTextUI"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("VolumeTextUI"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CurrentTrackUI"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("FullscreenUI"));
//                    break;
//                case RegistrySystem.KeyNames.Network:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Remote"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Local"));
//                    break;
//                case RegistrySystem.KeyNames.Notepad:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedFile"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowRect"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowName"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedText"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedTextRect"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedTitle"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedTitleRect"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SaveLocation"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SaveLocationRect"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("MenuBar"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedMenu"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("OpenedFile"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("PageHistory"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("PageHistory1"));
//                    break;
//                case RegistrySystem.KeyNames.FileUtility:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Window"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("CurrentFile"));
//                    break;
//                case RegistrySystem.KeyNames.WindowManager:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedWindow"));
//                    break;
//                case RegistrySystem.KeyNames.Discord:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("InitalRun"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedFile"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowRect"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("WindowName"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedText"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedTextRect"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedTitle"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("TypedTitleRect"));
//                    //Contacts List
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ShowContacts"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ContactsRect"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ContactsList"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ContactsScrollSize"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("ContactsScrollList"));
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("SelectedContact"));
//                    break;
//                case RegistrySystem.KeyNames.PlayerData:
//                    DefaultRegistryKeys[i].Values.Add(new RegistryDataSystem("Stats"));
//                    break;
//            }
//        });
//        ValueData();
//    }

//    void CheckRegKeys()
//    {
//        for (int i = 0; i < PersonController.control.People.Count; i++)
//        {
//            var personPC = PersonController.control.People[i].Gateway;

//            if (personPC.Registry.Count == 0)
//            {
//                for (int j = 0; j < DefaultRegistryKeys.Count; j++)
//                {
//                    personPC.Registry.Add(new RegistrySystem(DefaultRegistryKeys[j].KeyNameEnum));

//                    for (int k = 0; k < DefaultRegistryKeys[j].Values.Count; k++)
//                    {
//                        var RegValue = DefaultRegistryKeys[j].Values[k];
//                        personPC.Registry[j].Values.Add(new RegistryDataSystem(RegValue.ValueName));
//                    }
//                }
//            }
//            else
//            {
//                if (personPC.Registry.Count < DefaultRegistryKeys.Count)
//                {
//                    for (int j = 0; j < DefaultRegistryKeys.Count; j++)
//                    {
//                        if (j >= personPC.Registry.Count)
//                        {
//                            personPC.Registry.Add(new RegistrySystem(DefaultRegistryKeys[j].KeyNameEnum));
//                        }
//                        else
//                        {
//                            if (personPC.Registry[j].KeyNameEnum != DefaultRegistryKeys[j].KeyNameEnum)
//                            {
//                                personPC.Registry.Insert(j, new RegistrySystem(DefaultRegistryKeys[j].KeyNameEnum));
//                            }
//                        }
//                    }
//                }
//                for (int j = 0; j < DefaultRegistryKeys.Count; j++)
//                {
//                    if (personPC.Registry[j].KeyNameEnum != DefaultRegistryKeys[j].KeyNameEnum)
//                    {
//                        personPC.Registry.Insert(j, new RegistrySystem(DefaultRegistryKeys[j].KeyNameEnum));
//                    }

//                    if (personPC.Registry[j].Values.Count > 0)
//                    {
//                        for (int k = 0; k < DefaultRegistryKeys[j].Values.Count; k++)
//                        {
//                            if (k >= personPC.Registry[j].Values.Count)
//                            {
//                                var RegValue = DefaultRegistryKeys[j].Values[k];
//                                personPC.Registry[j].Values.Insert(k, new RegistryDataSystem(RegValue.ValueName));
//                            }
//                            else
//                            {
//                                if (personPC.Registry[j].Values[k].ValueName != DefaultRegistryKeys[j].Values[k].ValueName)
//                                {
//                                    var RegValue = DefaultRegistryKeys[j].Values[k];
//                                    personPC.Registry[j].Values.Insert(k, new RegistryDataSystem(RegValue.ValueName));
//                                }
//                            }
//                        }
//                    }
//                    else
//                    {
//                        for (int k = 0; k < DefaultRegistryKeys[j].Values.Count; k++)
//                        {
//                            var RegValue = DefaultRegistryKeys[j].Values[k];
//                            personPC.Registry[j].Values.Add(new RegistryDataSystem(RegValue.ValueName));
//                        }
//                    }
//                }
//            }
//        }


//        if (PersonController.control.People.Count > 0)
//        {
//            for (int i = 0; i < PersonController.control.People.Count; i++)
//            {
//                for (int j = 0; j < DefaultRegistryKeys.Count; j++)
//                {
//                    if (PersonController.control.People[i].Gateway.Registry[j].Valuesv2.Count > 0)
//                    {
//                        for (int k = 0; k < DefaultRegistryKeys[j].Valuesv2.Count; k++)
//                        {
//                            if (k >= PersonController.control.People[i].Gateway.Registry[j].Valuesv2.Count)
//                            {
//                                var RegValue = DefaultRegistryKeys[j].Valuesv2[k];
//                                PersonController.control.People[i].Gateway.Registry[j].Valuesv2.Insert(k, new RegistryValueSystem(RegValue.ValueName));
//                            }
//                            else
//                            {
//                                if (PersonController.control.People[i].Gateway.Registry[j].Valuesv2[k].ValueName != DefaultRegistryKeys[j].Values[k].ValueName)
//                                {
//                                    var RegValue = DefaultRegistryKeys[j].Valuesv2[k];
//                                    PersonController.control.People[i].Gateway.Registry[j].Valuesv2.Insert(k, new RegistryValueSystem(RegValue.ValueName));
//                                }
//                            }
//                        }
//                    }
//                    else
//                    {
//                        for (int k = 0; k < DefaultRegistryKeys[j].Valuesv2.Count; k++)
//                        {
//                            var RegValue = DefaultRegistryKeys[j].Valuesv2[k];
//                            PersonController.control.People[i].Gateway.Registry[j].Valuesv2.Add(new RegistryValueSystem(RegValue.ValueName));
//                        }
//                    }
//                }
//            }
//        }
//        RunRegLoad = false;
//    }
//}
