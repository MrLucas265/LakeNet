using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmailClient : MonoBehaviour
{
	public float native_width = 1920;
	public float native_height = 1080;
	public Rect windowRect = new Rect(100, 100, 200, 200);
	public int windowID;
	public Vector2 scrollpos = Vector2.zero;
	public Vector2 scrollpos1 = Vector2.zero;
	public bool Drag;
	public bool show;

	public int scrollsize;
	public int Select;
	public int ContractSelect;
	public int SelectedFile;

    public string LCDContent;
    public string LCDSubject;

	public bool minimize;
	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

	private CurContracts cc;
	private Computer com;
	private Defalt def;

	string Title;
	int Posx;
	int Posy;
	int Mod = 21;

	bool showInbox;
	bool showSent;
	bool showJunk;
	bool showMarked;
	bool showContracts;
	bool showFolders;
	bool showReply;
	bool showFiles;
	private GameObject Missions;
	private GameObject SysSoftware;

	public int MaxCount;

	public List<EmailSystem> JunkMail = new List<EmailSystem>();
	public List<EmailSystem> Inbox = new List<EmailSystem>();
	public List<EmailSystem> Important = new List<EmailSystem>();
	public List<EmailSystem> Sent = new List<EmailSystem>();
	public List<EmailSystem> Contracts = new List<EmailSystem>();

	public string Subject;
	public string Content;
	public string Attachment;

	public float MaxAttachmentSize;

	public int EmailIndex;

	private AppMan appman;

	// Use this for initialization
	void Start () 
	{
		Missions = GameObject.Find("Missions");
		SysSoftware = GameObject.Find("System");
		def = SysSoftware.GetComponent<Defalt>();
		com = SysSoftware.GetComponent<Computer>();
		appman = SysSoftware.GetComponent<AppMan>();
		cc = Missions.GetComponent<CurContracts>();
		Title = "Email - Contracts";
		showContracts = true;
		native_height = Customize.cust.native_height;
		native_width = Customize.cust.native_width;

		//GameControl.control.EmailData.Add (new EmailSystem ("Test", "Lucas", "N/A", "This is a test email", 0, 0, 0, false,EmailSystem.EmailType.Junk));

		MaxAttachmentSize = 20;

		RefreshList();

		PosCheck();

		ShowInbox();

		SetPos();
	}

	void PosCheck()
	{
		if (Customize.cust.windowx[windowID] == 0) 
		{
			Customize.cust.windowx [windowID] = Screen.width / 2;
		}
		if (Customize.cust.windowy[windowID] == 0) 
		{
			Customize.cust.windowy [windowID] = Screen.height / 2;
		}

		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
	}

	void SetPos()
	{
		CloseButton = new Rect (526,2,20,21);
		MiniButton = new Rect (504,2,21,21);
		DefaltSetting = new Rect (Customize.cust.windowx[windowID],Customize.cust.windowy[windowID],548,300);
		windowRect = DefaltSetting;
		DefaltBoxSetting = new Rect (2,2,501,21);
	}

	void Minimize()
	{
		if (minimize == true) 
		{
			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,25));
		}
		else
		{
			windowRect = (new Rect(windowRect.x,windowRect.y,DefaltSetting.width,DefaltSetting.height));
		}
	}

	void OnGUI()
	{
		Customize.cust.windowx[windowID] = windowRect.x;
		Customize.cust.windowy[windowID] = windowRect.y;
		GUI.skin = com.Skin[GameControl.control.GUIID];

        if (GameControl.control.LCDPage > 50 && GameControl.control.LCDPage < 100)
        {
            LogitechGSDK.LogiLcdColorSetTitle("Emails", 0, 255, 0);
            if(ContractSelect > -1)
            {
                LogitechGSDK.LogiLcdColorSetText(0, "Subject: " + Contracts[ContractSelect].Subject, 0, 255, 0);
                LogitechGSDK.LogiLcdColorSetText(1, "Target: " + GameControl.control.Contracts[ContractSelect].Target, 0, 255, 0);
                LogitechGSDK.LogiLcdColorSetText(2, "File: " + GameControl.control.Contracts[ContractSelect].File, 0, 255, 0);
                LogitechGSDK.LogiLcdColorSetText(3, "Cash: " + GameControl.control.Contracts[ContractSelect].Cash.ToString(), 0, 255, 0);
            }
        }

        if (show == true)
		{
			GUI.color = com.colors[Customize.cust.WindowColorInt];
			windowRect = WindowClamp.ClampToScreen(GUI.Window(windowID,windowRect,DoMyWindow,""));
		}
	}

	void DoMyWindow(int WindowID)
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			def.SelectedWindowID = windowID;
		}

		if (CloseButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [0])) 
			{
				appman.SelectedApp = "Email";
				Select = -1;
				ContractSelect = 1;
				ShowInbox();
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			GUI.Button (new Rect (CloseButton), "X", com.Skin [GameControl.control.GUIID].customStyles [1]);
		}

		if (MiniButton.Contains (Event.current.mousePosition)) 
		{
			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
			{
				minimize = !minimize;
				Minimize();
			}
		} 
		else
		{
			GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
			GUI.contentColor = com.colors[Customize.cust.FontColorInt];
			if (GUI.Button (new Rect (MiniButton), "-",com.Skin [GameControl.control.GUIID].customStyles [2])) 
			{
				minimize = !minimize;
				Minimize();
			}
		}

		GUI.backgroundColor = com.colors[Customize.cust.ButtonColorInt];
		GUI.contentColor = com.colors[Customize.cust.FontColorInt];

		GUI.DragWindow(new Rect(DefaltBoxSetting));
		GUI.Box(new Rect(DefaltBoxSetting),Title);

		EmailFolders();
		NewEmailSystemTest();
	}

	void NewEmailSystemTest()
	{
		if (showFolders == false) 
		{
			Posx = 2;
			Posy = 45;
		}
		else
		{
			Posx = 43;
			Posy = 45;
		}

		if (showReply == true) 
		{
			if (GUI.Button (new Rect (2, 24, 20, 20), "<-"))
			{
				ShowInbox ();
			}

			if (GUI.Button (new Rect (23, 24, 20, 20), "@")) 
			{
				showFiles = !showFiles;
			}

			if (GUI.Button (new Rect (44, 24, 40, 20), "Send")) 
			{
				SendReply();
			}

			if (showFiles == true) 
			{
				GUI.Box (new Rect (2, 45, 100, 21), "File Name");
				GUI.Box (new Rect (103, 45, 100, 21), "File Size");
				GUI.Label (new Rect (250, 100, 200, 21), "Max Attachment size is " + MaxAttachmentSize);

				scrollpos = GUI.BeginScrollView (new Rect (2, 67, 220, 200), scrollpos, new Rect (0, 0, 0, scrollsize * 21));
				for (scrollsize = 0; scrollsize < GameControl.control.ProgramFiles.Count; scrollsize++) 
				{
					if (GUI.Button (new Rect (0, scrollsize * 21, 100, 20), "" + GameControl.control.ProgramFiles [scrollsize].Name)) 
					{
						SelectedFile = scrollsize;
						Attachment = GameControl.control.ProgramFiles[SelectedFile].Name;
						cc.AttachtedFile = GameControl.control.ProgramFiles[SelectedFile].Name;
						AttachedTest();
					}
					if (GUI.Button (new Rect (101, scrollsize * 21, 100, 20), "" + GameControl.control.ProgramFiles [scrollsize].Used)) 
					{
						SelectedFile = scrollsize;
						Attachment = GameControl.control.ProgramFiles[SelectedFile].Name;
						cc.AttachtedFile = GameControl.control.ProgramFiles[SelectedFile].Name;
						AttachedTest();
					}
				}
				GUI.EndScrollView ();
			} 
			else 
			{
				if (GameControl.control.Contracts.Count > 0) 
				{
					if (ContractSelect > -1)
					{
						GUI.Label (new Rect (2, 46, 55, 21), "To: ");
						GUI.TextField (new Rect (65, 46, 200, 21), GameControl.control.Contracts [ContractSelect].Address);

						GUI.Label (new Rect (2, 67, 55, 21), "Subject: ");
						GUI.TextField (new Rect (65, 67, 200, 21), "RE: " + GameControl.control.Contracts [ContractSelect].Name);
					} 
					else 
					{
//						GUI.Label (new Rect (2, 46, 55, 21), "To: ");
//						GUI.TextField (new Rect (65, 46, 200, 21), GameControl.control.Contracts [ContractSelect].Address);
//
//						GUI.Label (new Rect (2, 67, 55, 21), "Subject: ");
//						GUI.TextField (new Rect (65, 67, 200, 21), "RE: " + GameControl.control.Contracts [ContractSelect].Name);
					}

					if (SelectedFile >= 0) 
					{
						if (GameControl.control.ProgramFiles [SelectedFile].Used <= MaxAttachmentSize) 
						{
							GUI.Label (new Rect (2, 88, 55, 21), "Attached: ");
							GUI.TextField (new Rect (65, 88, 200, 21), "" + GameControl.control.ProgramFiles[SelectedFile].Name);

							if (GUI.Button (new Rect (267, 89, 45, 20), "Clear")) 
							{
								SelectedFile = -1;
							}
						}
					}
				}

				Content = GUI.TextArea (new Rect (2,173,544,125), Content);
			}
			//GUI.Label (new Rect (2, 46, 55, 21), "Subject: ");


		} 
		else 
		{
			GUI.Box(new Rect(Posx,Posy,180,20),"Subject");
			GUI.Box(new Rect(181 + Posx,Posy,180,20),"Sender");
			GUI.Box(new Rect(362 + Posx,Posy,120,20),"Date");

			EmailView();

			if (GUI.Button(new Rect(124, 24, 45, 20),"Reply"))
			{
				showReply = true;
				showInbox = false;
				showJunk = false;
				showMarked = false;
				showContracts = false;
				showSent = false;
				showFolders = false;
			}

			if (GUI.Button(new Rect(63, 24, 60, 20),"Refresh"))
			{
				RefreshList();
			}

			if (GUI.Button(new Rect(2, 24, 60, 20),"Folders"))
			{
				showFolders =! showFolders;
			}
		}
	}

	void AttachedTest()
	{
		if (GameControl.control.ProgramFiles [SelectedFile].Used > MaxAttachmentSize) 
		{
			SelectedFile = -1;
		}
		showFiles = false;
	}

	void SendReply()
	{
		cc.Select = ContractSelect;

		if (cc.done == true)
		{
			ContractSelect = -1;
		}

		if (Attachment != "") 
		{
			GameControl.control.EmailData.Add (new EmailSystem ("RE: " + GameControl.control.Contracts[ContractSelect].Name, GameControl.control.ProfileName,GameControl.control.Time.FullDate, "Attached: " + Attachment + "\n" + Content, 0, 0, 0, false, EmailSystem.EmailType.Sent));
			cc.Complete();
		} 
		else 
		{
			GameControl.control.EmailData.Add (new EmailSystem ("RE: " + GameControl.control.Contracts[ContractSelect].Name,GameControl.control.ProfileName, GameControl.control.Time.FullDate, Content, 0, 0, 0, false, EmailSystem.EmailType.Sent));
			cc.Complete();
		}

		Attachment = "";
		Content = "";
		SelectedFile = -1;
		ContractSelect = -1;
	}

	void EmailView()
	{
		if (GameControl.control.EmailData.Count > 0) 
		{
			if (showInbox == true) 
			{
				if(Inbox.Count > 0)
				{
					scrollpos = GUI.BeginScrollView(new Rect(Posx, Posy + Mod, 495, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
					for (scrollsize = 0; scrollsize < Inbox.Count; scrollsize++)
					{
						if(GUI.Button(new Rect(0, scrollsize * 21, 180, 20), "" + Inbox[scrollsize].Subject))
						{
							Select = scrollsize;
						}
						if(GUI.Button(new Rect(181, scrollsize * 21, 180, 20), "" + Inbox[scrollsize].Sender))
						{
							Select = scrollsize;
						}
						if(GUI.Button(new Rect(362, scrollsize * 21 , 120, 20), "" + Inbox[scrollsize].Date))
						{
							Select = scrollsize;
						}
					}
					GUI.EndScrollView();
					if (Select >= 0) 
					{
						GUI.TextArea(new Rect(2,173,544,125),"" + Inbox[Select].Content);

						if (GUI.Button(new Rect(170, 24, 60, 20),"Delete"))
						{
							GameControl.control.EmailData.Remove (Inbox[Select]);
                            RefreshList();
                        }
					}
				}
			}

			if (showJunk == true) 
			{
				if(JunkMail.Count > 0)
				{
					scrollpos = GUI.BeginScrollView(new Rect(Posx, Posy + Mod, 495, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
					for (scrollsize = 0; scrollsize < JunkMail.Count; scrollsize++)
					{
						if(GUI.Button(new Rect(0, scrollsize * 21, 180, 20), "" + JunkMail[scrollsize].Subject))
						{
							Select = scrollsize;
						}
						if(GUI.Button(new Rect(181, scrollsize * 21, 180, 20), "" + JunkMail[scrollsize].Sender))
						{
							Select = scrollsize;
						}
						if(GUI.Button(new Rect(362, scrollsize * 21 , 120, 20), "" + JunkMail[scrollsize].Date))
						{
							Select = scrollsize;
						}
					}
					GUI.EndScrollView();
					if (Select >= 0) 
					{
						GUI.TextArea(new Rect(2,173,544,125),"" + JunkMail[Select].Content);
					}
				}
			}

			if (showSent == true) 
			{
				if(Sent.Count > 0)
				{
					scrollpos = GUI.BeginScrollView(new Rect(Posx, Posy + Mod, 495, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
					for (scrollsize = 0; scrollsize < Sent.Count; scrollsize++)
					{
						if(GUI.Button(new Rect(0, scrollsize * 21, 180, 20), "" + Sent[scrollsize].Subject))
						{
							Select = scrollsize;
						}
						if(GUI.Button(new Rect(181, scrollsize * 21, 180, 20), "" + Sent[scrollsize].Sender))
						{
							Select = scrollsize;
						}
						if(GUI.Button(new Rect(362, scrollsize * 21 , 120, 20), "" + Sent[scrollsize].Date))
						{
							Select = scrollsize;
						}
					}
					GUI.EndScrollView();
					if (Select >= 0) 
					{
						GUI.TextArea(new Rect(2,173,544,125),"" + Sent[Select].Content);

						if (GUI.Button(new Rect(170, 24, 60, 20),"Delete"))
						{
							GameControl.control.EmailData.Remove (Sent[Select]);
						}
					}
				}
			}

			if (showMarked == true) 
			{
				if(Important.Count > 0)
				{
					scrollpos = GUI.BeginScrollView(new Rect(Posx, Posy + Mod, 495, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
					for (scrollsize = 0; scrollsize < Important.Count; scrollsize++)
					{
						if(GUI.Button(new Rect(0, scrollsize * 21, 180, 20), "" + Important[scrollsize].Subject))
						{
							Select = scrollsize;
						}
						if(GUI.Button(new Rect(181, scrollsize * 21, 180, 20), "" + Important[scrollsize].Sender))
						{
							Select = scrollsize;
						}
						if(GUI.Button(new Rect(362, scrollsize * 21 , 120, 20), "" + Important[scrollsize].Date))
						{
							Select = scrollsize;
						}
					}
					GUI.EndScrollView();
					if (Select >= 0) 
					{
						GUI.TextArea(new Rect(2,173,544,125),"" + Important[Select].Content);
					}
				}
			}

			if (showContracts == true) 
			{
				if(Contracts.Count > 0)
				{
					scrollpos = GUI.BeginScrollView(new Rect(Posx, Posy + Mod, 495, 100), scrollpos, new Rect(0, 0, 0, scrollsize * 21));
					for (scrollsize = 0; scrollsize < Contracts.Count; scrollsize++)
					{
						if(GUI.Button(new Rect(0, scrollsize * 21, 180, 20), "" + Contracts[scrollsize].Subject))
						{
							ContractSelect = scrollsize;
							EmailIndex = GameControl.control.EmailData.IndexOf(Contracts[ContractSelect]);
						}
						if(GUI.Button(new Rect(181, scrollsize * 21, 180, 20), "" + Contracts[scrollsize].Sender))
						{
							ContractSelect = scrollsize;
						}
						if(GUI.Button(new Rect(362, scrollsize * 21 , 120, 20), "" + Contracts[scrollsize].Date))
						{
							ContractSelect = scrollsize;
						}
					}
					GUI.EndScrollView();
					if (ContractSelect >= 0) 
					{
						GUI.TextArea(new Rect(2,173,544,125),"" + Contracts[ContractSelect].Content);
						if (GUI.Button(new Rect(170, 24, 60, 20),"Delete"))
						{
							GameControl.control.EmailData.Remove (Contracts[ContractSelect]);
							GameControl.control.Contracts.RemoveAt (ContractSelect);
						}
					}
				}
			}
		}
	}

	void ShowInbox()
	{
		Title = "Email - Inbox";
		showInbox = true;
		showJunk = false;
		showSent = false;
		showMarked = false;
		showContracts = false;
		showReply = false;
		Select = -1;
		SelectedFile = -1;
		ContractSelect = -1;
	}

	void EmailFolders()
	{
		if (showFolders == true) 
		{
			//GUI.Box(new Rect(3,50,50,120),"");
			GUI.BeginGroup(new Rect(2, 23, 140, 180));
			if(GUI.Button(new Rect(0, 22, 40, 20), "Inbox"))
			{
				ShowInbox();
			}
			if(GUI.Button(new Rect(0, 43, 40, 20), "Sent"))
			{
				Title = "Email - Sent";
				showSent = true;
				showInbox = false;
				showJunk = false;
				showMarked = false;
				showContracts = false;
				Select = -1;
			}
			if(GUI.Button(new Rect(0, 64, 40, 20), "Junk"))
			{
				Title = "Email - Junk";
				showJunk = true;
				showInbox = false;
				showSent = false;
				showMarked = false;
				showContracts = false;
				Select = -1;
			}
			if(GUI.Button(new Rect(0, 85, 40, 20), "Mark"))
			{
				Title = "Email - Marked";
				showMarked = true;
				showInbox = false;
				showSent = false;
				showJunk = false;
				showContracts = false;
				Select = -1;
			}
			if(GUI.Button(new Rect(0, 106, 40, 20), "Con"))
			{
				Title = "Email - Contracts";
				showContracts = true;
				showInbox = false;
				showSent = false;
				showJunk = false;
				showMarked = false;
				Select = -1;
			}
			GUI.EndGroup();
		}
	}

	public void RefreshList()
	{
		JunkMail.RemoveRange (0, JunkMail.Count);
		Inbox.RemoveRange (0, Inbox.Count);
		Important.RemoveRange (0, Important.Count);
		Contracts.RemoveRange (0, Contracts.Count);
		Sent.RemoveRange (0, Sent.Count);

		for (int SearchCount = 0; SearchCount < GameControl.control.EmailData.Count; SearchCount++) 
		{
			if (GameControl.control.EmailData[SearchCount].Type == EmailSystem.EmailType.Junk) 
			{
				JunkMail.Add (GameControl.control.EmailData [SearchCount]);
			}

			if (GameControl.control.EmailData[SearchCount].Type == EmailSystem.EmailType.New) 
			{
				Inbox.Add (GameControl.control.EmailData [SearchCount]);
			}

			if (GameControl.control.EmailData[SearchCount].Type == EmailSystem.EmailType.Sent) 
			{
				Sent.Add (GameControl.control.EmailData [SearchCount]);
			}

			if (GameControl.control.EmailData[SearchCount].Type == EmailSystem.EmailType.Important) 
			{
				Important.Add (GameControl.control.EmailData [SearchCount]);
			}

			if (GameControl.control.EmailData[SearchCount].Type == EmailSystem.EmailType.Contract) 
			{
				Contracts.Add (GameControl.control.EmailData [SearchCount]);
			}
		}
	}
}
