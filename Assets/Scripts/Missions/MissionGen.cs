using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionGen : MonoBehaviour 
{
	public List<MissionSystem> MissionList = new List<MissionSystem>();
    public List<MissionSystem> StoryList = new List<MissionSystem>();
   // public List<string> FilesToDelete = new List<string>();
    public int DeleteAmt;
    public int RandomMissionNumber;
    public int RandomFile;
    public int RandomAddress;

    public int MissionTotal;
    public int Index;
    public bool startGen;
    public bool ForceGen;

    public int count;
    public bool remove;

    public string MissionName;
    public string MissionAddress;

    public int MissionRep;

	public string MDesc;
    public string EDesc;

	public int DescName;
	public int DescSelector;

	public bool CopyMission;
	public bool CopyPublicMission;

	public bool DeleteMission;
	public bool DeletePublcMission;

	public bool EditDatabase;

	public int DescCash;
	public string DescFrom;
	public string DescFile;
	public string DescTarget;

	public int SelectedFile;

	public int Count;

	public int LevelRequirement;

	public bool ComplierDone;

	public List<string> JaildewPublic = new List<string>();
	public List<string> JaildewPrivate = new List<string>();
	public List<string> BecasPublic = new List<string>();
	public List<string> BecasPrivate = new List<string>();
	public List<string> UnicomPublic = new List<string>();
	public List<string> UnicomPrivate = new List<string>();
	public List<string> REVATestPublic = new List<string>();
	public List<string> REVATestPrivate = new List<string>();
	public List<string> ParaPublic = new List<string>();
	public List<string> ParaPrivate = new List<string>();

	public int ScannedInt;

	public int IdleDeleteTime;

	private GameObject Prompts;
	private NotfiPrompt noti;
	private MissionBrow missionbrow;

	public float Timer;
	public float StartTime;

	public float DeleteTimer;
	public float DeleteStartTime;

	public float NewMissionTimer;
	public float NewMissionStartTime;

	// Use this for initialization
	void Start () 
    {
        //FilesToDelete.Capacity = DeleteAmt;
		Prompts = GameObject.Find("Prompts");
		noti = Prompts.GetComponent<NotfiPrompt>();
		missionbrow = GetComponent<MissionBrow>();

		StartTime = 5;

		DeleteStartTime = 1;

		MissionTotal = 100;

        NewMissionStartTime = Random.Range(1, 60);
        NewMissionTimer = NewMissionStartTime;

		StoryMissionSystem();

        if (GameControl.control.WebsiteFiles.Count > 200)
        {
            if (MissionList.Count >= MissionTotal)
            {
                GeneratingMissions();
            }
        }
    }
	
    void Update()
    {
		if (missionbrow.Select > MissionList.Count-1)
		{
			missionbrow.Select = MissionList.Count-1;
		}

        if (MissionList.Count > 0)
        {
            if (DeleteTimer <= 0)
            {
                for (int i = 0; i < MissionList.Count; i++)
                {
                    if (MissionList[i].IdleDeleteTime <= 0)
                    {
                        MissionList.RemoveAt(i);
                    }
                    else
                    {
                        MissionList[i].IdleDeleteTime--;
                    }
                }
                DeleteTimer = DeleteStartTime;
            }

            DeleteTimer -= 1 * Time.deltaTime;
        }

        if (MissionList.Count < MissionTotal)
		{
			NewMissionTimer -= 1 * Time.deltaTime;
			if (NewMissionTimer <= 0)
			{
                if (GameControl.control.WebsiteFiles.Count > 200)
                {
                    GeneratingMissions();
                }
                NewMissionStartTime = Random.Range(1, 60);
				NewMissionTimer = NewMissionStartTime;
            }
		}

        if (ScannedInt <= GameControl.control.WebsiteFiles.Count && ComplierDone == false)
		{
			ComplierDone = true;
		}

		if (Count <= GameControl.control.WebsiteFiles.Count) 
		{
			ListComplier();
		}
    }

	public void FirstMission()
	{
		if (REVATestPublic.Count > 10 && REVATestPrivate.Count > 10) 
		{
			SelectedFile = Random.Range (0, REVATestPublic.Count);
			DescFile = REVATestPublic[SelectedFile];
			GameControl.control.Contracts.Add(new MissionSystem ("REVA Test",DescFile,"www.revatest.com","www.reva.com","done","done","Public","REVA",0,0,0,Random.Range (200, 200),0,MissionSystem.MissionType.PTCopy));
			GameControl.control.EmailData.Add (new EmailSystem ("Reva Test","www.reva.com",GameControl.control.Time.FullDate,"We would like you to connect to www.revatest.com by opening net viewer in the app menu then typing the URL in the address bar then goto the public file section find and copy this file " + DescFile +  " then reply to this email and click the @ to attach the file to the email and send it then we will give you the membership login details", 0, 0, 0, false,EmailSystem.EmailType.Contract));
			noti.ShowNoti = true;
			noti.Notification = "To open the new mail click the letter Icon>Folders>Con";
			noti.playsound = true;
			noti.DisplayTime = 15;
            GameControl.control.NewAccount = false;

        }
	}

	void ListComplier()
	{
		for(int i = 0; i < GameControl.control.WebsiteFiles.Count; i++)
		{
			switch(GameControl.control.WebsiteFiles [i].Location)
			{
			case "Jaildew Public":
				if (!JaildewPublic.Contains (GameControl.control.WebsiteFiles [i].Name)) 
				{
					JaildewPublic.Add (GameControl.control.WebsiteFiles [i].Name);
					Count++;
				}
				break;

			case "Jaildew Private":
				if (!JaildewPrivate.Contains (GameControl.control.WebsiteFiles [i].Name)) 
				{
					JaildewPrivate.Add (GameControl.control.WebsiteFiles [i].Name);
					Count++;
				}
				break;

			case "Becas Public":
				if (!BecasPublic.Contains (GameControl.control.WebsiteFiles [i].Name)) 
				{
					BecasPublic.Add (GameControl.control.WebsiteFiles [i].Name);
					Count++;
				}
				break;

			case "Becas Private":
				if (!BecasPrivate.Contains (GameControl.control.WebsiteFiles [i].Name)) 
				{
					BecasPrivate.Add (GameControl.control.WebsiteFiles [i].Name);
					Count++;
				}
				break;

			case "Unicom Public":
				if (!UnicomPublic.Contains (GameControl.control.WebsiteFiles [i].Name)) 
				{
					UnicomPublic.Add (GameControl.control.WebsiteFiles [i].Name);
					Count++;
				}
				break;

			case "Unicom Private":
				if (!UnicomPrivate.Contains (GameControl.control.WebsiteFiles [i].Name)) 
				{
					UnicomPrivate.Add (GameControl.control.WebsiteFiles [i].Name);
					Count++;
				}
				break;

			case "REVA Private":
				if (!REVATestPrivate.Contains (GameControl.control.WebsiteFiles [i].Name)) 
				{
					REVATestPrivate.Add (GameControl.control.WebsiteFiles [i].Name);
					Count++;
				}
				break;

			case "REVA Public":
				if (!REVATestPublic.Contains (GameControl.control.WebsiteFiles [i].Name)) 
				{
					REVATestPublic.Add (GameControl.control.WebsiteFiles [i].Name);
					Count++;
				}
				break;

			case "Para Private":
				if (!ParaPrivate.Contains (GameControl.control.WebsiteFiles [i].Name)) 
				{
					ParaPrivate.Add (GameControl.control.WebsiteFiles [i].Name);
					Count++;
				}
				break;

			case "Para Public":
				if (!ParaPublic.Contains (GameControl.control.WebsiteFiles [i].Name)) 
				{
					ParaPublic.Add (GameControl.control.WebsiteFiles [i].Name);
					Count++;
				}
				break;
			}

			ScannedInt = i;
		}
	}

	// Update is called once per frame
	void GeneratingMissions ()
	{
        if (GameControl.control.Rep.Count >= 1)
        {
            RandomMissionNumber = Random.Range(1, 10);
        }


        switch (RandomMissionNumber) 
		{
		case 1:
			MissionName = "Sabotage Server Files";
			MissionAddress = "www.jaildew.com";
			DescTarget = "www.unicom.com";
			DescCash = Random.Range (150, 500);
			LevelRequirement = Random.Range(2,4);
            MissionRep = Random.Range (100, 250);
			SelectedFile = Random.Range (0, UnicomPrivate.Count);
			DescFile = UnicomPrivate[SelectedFile];
			IdleDeleteTime = Random.Range(300, 900);
			DeleteMission = true;
			DescGenerator ();
			MissionList.Add (new MissionSystem (MissionName, DescFile,DescTarget, MissionAddress, MDesc, EDesc, "Private","REVA",IdleDeleteTime,DescCash,MissionRep,Random.Range (75, 125),LevelRequirement,MissionSystem.MissionType.UDelete));
			break;

		case 2:
			MissionName = "Sabotage Server Files";
            MissionAddress = "www.unicom.com";
			DescTarget = "www.jaildew.com";
			DescCash = Random.Range (150, 500);
			LevelRequirement = Random.Range(2,4);
			MissionRep = Random.Range (100, 250);
			SelectedFile = Random.Range (0, JaildewPrivate.Count);
			DescFile = JaildewPrivate[SelectedFile];
			IdleDeleteTime = Random.Range(300, 900);
			DeleteMission = true;
			DescGenerator ();
			MissionList.Add (new MissionSystem (MissionName, DescFile,DescTarget, MissionAddress, MDesc, EDesc, "Private","REVA",IdleDeleteTime,DescCash,MissionRep,Random.Range (75, 125),LevelRequirement,MissionSystem.MissionType.JDelete));
			break;

		case 3:
			MissionName = "Delete Public Files";
            MissionAddress = "www.unicom.com";
			DescTarget = "www.unicom.com";
			DescCash = Random.Range (5, 100);
			LevelRequirement = Random.Range(1,3);
			MissionRep =  Random.Range (5, 50);
			SelectedFile = Random.Range (0, UnicomPublic.Count);
			DescFile = UnicomPublic[SelectedFile];
			IdleDeleteTime = Random.Range(300, 900);
			DeletePublcMission = true;
			DescGenerator ();
			MissionList.Add (new MissionSystem (MissionName,DescFile,DescTarget,MissionAddress, MDesc, EDesc, "Public","REVA",IdleDeleteTime,DescCash, MissionRep, Random.Range (75, 125),LevelRequirement,MissionSystem.MissionType.PUDelete));
			break;

		case 4:
            MissionName = "Delete Public Files";
			MissionAddress =  "www.jaildew.com";
			DescTarget = "www.jaildew.com";
			DescCash = Random.Range (5, 100);
			LevelRequirement = Random.Range(1,3);
			MissionRep = Random.Range (5, 50);
			SelectedFile = Random.Range (0, JaildewPublic.Count);
			DescFile = JaildewPublic[SelectedFile];
			IdleDeleteTime = Random.Range(300, 900);
			DeletePublcMission = true;
			DescGenerator ();
			MissionList.Add (new MissionSystem (MissionName,DescFile,DescTarget,MissionAddress, MDesc, EDesc, "Public","REVA",IdleDeleteTime,DescCash, MissionRep, Random.Range (75, 125),LevelRequirement,MissionSystem.MissionType.PJDelete));
           // MissionDesc.Add("Hey man Want some quick legal cash? Can you clean our public files?");
			break;

		case 5:
			MissionName = "Backup Public Files";
			MissionAddress = "www.jaildew.com";
			DescTarget = "www.jaildew.com";
			DescCash = Random.Range (5, 100);
			LevelRequirement = Random.Range(1,3);
			MissionRep = Random.Range (5, 50);
			SelectedFile = Random.Range (0, JaildewPublic.Count);
			DescFile = JaildewPublic[SelectedFile];
			IdleDeleteTime = Random.Range(300, 900);
			CopyPublicMission = true;
			DescGenerator ();
			MissionList.Add (new MissionSystem (MissionName,DescFile,DescTarget,MissionAddress, MDesc, EDesc, "Public","REVA",IdleDeleteTime,DescCash, MissionRep, Random.Range (75, 125),LevelRequirement,MissionSystem.MissionType.PJCopy));
			//MissionDesc.Add("G'Day I need some help backing up a file. I accidently uninstalled my file utility");
			break;

		case 6:
			MissionName =  "Backup Public Files";
			MissionAddress = "www.unicom.com";
			DescTarget = "www.unicom.com";
			DescCash = Random.Range (5, 100);
			LevelRequirement = Random.Range(1,3);
			MissionRep = Random.Range (5, 50);
			SelectedFile = Random.Range (0, UnicomPublic.Count);
			DescFile = UnicomPublic[SelectedFile];
			IdleDeleteTime = Random.Range(300, 900);
			CopyPublicMission = true;
			DescGenerator();
			MissionList.Add (new MissionSystem (MissionName, DescFile,DescTarget, MissionAddress, MDesc, EDesc, "Public","REVA",IdleDeleteTime,DescCash, MissionRep, Random.Range (75, 125),LevelRequirement,MissionSystem.MissionType.PUCopy));
			break;

		case 7:
			MissionName = "Backup Public Files";
			MissionAddress = "www.becassystems.com";
			DescTarget = "www.becassystems.com";
			DescCash = Random.Range (5, 100);
			LevelRequirement = Random.Range(1,3);
			MissionRep = Random.Range (5, 50);
			SelectedFile = Random.Range (0, BecasPublic.Count);
			DescFile = BecasPublic[SelectedFile];
			IdleDeleteTime = Random.Range(300, 900);
			CopyPublicMission = true;
			DescGenerator();
			MissionList.Add (new MissionSystem (MissionName, DescFile,DescTarget, MissionAddress, MDesc, EDesc, "Public","REVA",IdleDeleteTime,DescCash, MissionRep, Random.Range (75, 125),LevelRequirement,MissionSystem.MissionType.PBCopy));
			break;

		case 8:
			MissionName = "Delete Public Files";
			MissionAddress = "www.becassystems.com";
			DescTarget = "www.becassystems.com";
			DescCash = Random.Range (5, 100);
			LevelRequirement = Random.Range(1,3);
			MissionRep = Random.Range (5, 50);
			SelectedFile = Random.Range (0, BecasPublic.Count);
			DescFile = BecasPublic[SelectedFile];
			IdleDeleteTime = Random.Range(300, 900);
			DeletePublcMission = true;
			DescGenerator();
			MissionList.Add (new MissionSystem (MissionName, DescFile,DescTarget, MissionAddress, MDesc,EDesc,"Public","REVA",IdleDeleteTime,DescCash, MissionRep, Random.Range (75, 125),LevelRequirement,MissionSystem.MissionType.PBDelete));
			break;

		case 9:
			MissionName = "Change University Grade";
			MissionAddress = ("www.melvena.com");
			DescCash = Random.Range (750, 1000);
			LevelRequirement = Random.Range(3,6);
			MissionRep = Random.Range (150, 250);
			DescName = Random.Range (0, PersonController.control.People.Count);
            DescTarget = PersonController.control.People[DescName].Name;
            int CurrentGradeIndex = PersonController.control.UniGrades.IndexOf(DescTarget);
            DescFile = PersonController.control.UniGrades[Random.Range(0, PersonController.control.UniGrades.Count)];
            IdleDeleteTime = Random.Range(300, 900);
			EditDatabase = true;
			DescGenerator ();
            MissionList.Add(new MissionSystem(MissionName, DescFile, DescTarget, MissionAddress, MDesc, EDesc, "Edits", "REVA", IdleDeleteTime, DescCash, MissionRep, Random.Range(75, 125), LevelRequirement, MissionSystem.MissionType.UniUpgrade));
            break;
        }

	}

	void StoryMissionSystem()
	{
		
	}

	void NameGenerator()
	{

	}

	public void DescGenerator()
	{
		DescSelector = Random.Range (1, 3);
		if (DeleteMission == true) 
		{
			switch (DescSelector) 
			{
			case 1:
				EDesc = "Hey there I have a friend who said to contact you about a particuler job. Im willing to pay " + DescCash +
					" amount of credits. And all you have to do is delete this file " + DescFile + " at this website " + DescTarget +
					" I'll even put in a good word for you";

                MDesc = "Hey there I have a friend who said to contact you about a particuler job. Im willing to pay " + DescCash +
                    " amount of credits.  I'll even put in a good word for you";

                    DeleteMission = false;
				break;
			case 2:
				EDesc = "Hey there I was told to contact you if I had any trouble. Well I need you to delete this file " + DescFile +
					" at " + DescTarget +" and I will pay " + DescCash + " for your work.";

                MDesc = "Hey there I was told to contact you if I had any trouble Ill pay " + DescCash +
                        " amount of credits for your work";

                    DeleteMission = false;
				break;
			}
		}

		if (DeletePublcMission == true) 
		{
			DescSelector = Random.Range (1, 2);
			switch (DescSelector) 
			{
			case 1:
				EDesc = "Greetings I heard your looking for freelance work. I got a job for you if your intrested. Can you delete this file " + DescFile +
					" We need this file gone and in return we will pay you " + DescCash +
					" The file is located at " + DescTarget +
					" Email me upon deletion and ill put a good word for you so you can get better work";

                MDesc = "Greetings I heard your looking for freelance work. I got a job for you if your intrested." +
                    " We need this file gone and in return we will pay you " + DescCash +
                    " Also upon deletion email me and ill put a good word for you so you can get better work";
                    DeletePublcMission = false;
				break;
			}
		}

		if (CopyPublicMission == true) 
		{
			DescSelector = Random.Range (1, 2);
			switch (DescSelector) 
			{
			case 1:
				EDesc = "G'Day I need some help. I accidently uninstalled my file utility and I need this file backed up " + DescFile +
					". Upon receving the file I will transfer " + DescCash +
					" The file is located at " + DescTarget +
					" I will also put a good word for you so you can get better work";

                MDesc = "G'Day I need some help. I accidently uninstalled my file utility" +
                    ". Upon receving the file I will transfer " + DescCash +
                    " I will also put a good word for you so you can get better work";
                    CopyPublicMission = false;
				break;
			}
		}

		if (EditDatabase == true) 
		{
			DescSelector = Random.Range (1, 2);
			switch (DescSelector) 
			{
			case 1:
                    //MissionDesc.Add("Hello my name is " + GameControl.control.AcaName[DescName] + "My grades are horrible at the moment Mind giving them a boost?");

                    EDesc = "Hello my name is " + DescTarget + "My grades are horrible at the moment Mind giving them a boost? " + DescFile +
                            ". Upon receving the file I will transfer " + DescCash +
                            "The file is located at " + DescTarget +
                            " I will also put a good word for you so you can get better work";

                    MDesc = "Hello my name is " + DescTarget + "My grades are horrible at the moment Mind giving them a boost? " + DescFile +
					". Upon receving the file I will transfer " + DescCash +
					"The file is located at " + DescTarget +
					" I will also put a good word for you so you can get better work";
				EditDatabase = false;
				break;
			}
		}
	}
}
