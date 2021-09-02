using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CurContracts : MonoBehaviour 
{
    public float native_width = 1920;
    public float native_height = 1080;
    public Rect windowRect = new Rect(100, 100, 200, 200);
    public int windowID;
    public Vector2 scrollpos = Vector2.zero;
    public bool Drag;
    public bool show;
    public int scrollsize;
    public int Select;
    public string AddressFile;
	private Defalt def;
    private JailDew jd;
    private Unicom uc;
	private Test test;
	private Becas becas;

	private EmailClient email;

	public Rect CloseButton;
	public Rect MiniButton;
	public Rect DefaltSetting;
	public Rect DefaltBoxSetting;

	public bool minimize;

	private GameObject Computer;
	private GameObject Applications;

	public bool done;

	public List<string> FileNames = new List<string>();
	public List<string> SenderFileNames = new List<string>();
	public List<string> TargetFileNames = new List<string>();

	public string AttachtedFile;

	public string EmailSubject;
	public string EmailContent;
	public string EmailSender;

	public int SelectedResponse;

	public bool Skip;

	void Start () 
    {
		Computer = GameObject.Find("System");
		Applications = GameObject.Find("Applications");
		def = Computer.GetComponent<Defalt>();
		email = Applications.GetComponent<EmailClient>();
		windowRect.x = Customize.cust.windowx[windowID];
		windowRect.y = Customize.cust.windowy[windowID];
		//GameControl.control.LoginUser.Add("Agent" + def.GetRandomNumber(6,6));
		//GameControl.control.LoginPass.Add(def.GetRandomString(12,12));
	}

    //int GetMissionTypeCount()
    //{
    //    return GameControl.control.MissionType.Count;
    //}

	void SendContractorEmail()
	{
		GameControl.control.EmailData.Add (new EmailSystem (EmailSubject,GameControl.control.Contracts[Select].Address, GameControl.control.Time.FullDate, EmailContent, 0, 0, 0, false,EmailSystem.EmailType.New));
	}

	void SendAccountDetailsEmail()
	{
        GameControl.control.EmailData.Add (new EmailSystem (EmailSubject,"www.reva.com", GameControl.control.Time.FullDate, EmailContent, 0, 0, 0, false,EmailSystem.EmailType.New));

		GameControl.control.StoredLogins.Add (new LoginSystem ("LEC Bank", StringGenerator.RandomNumberChar(6, 6), StringGenerator.RandomMixedChar(15, 15), 0));
		//GameControl.control.MyBankDetails.Add (new BankSystem("192.168.56.91","LEC Bank",GameControl.control.StoredLogins[0].Username,GameControl.control.StoredLogins[1].Username,GameControl.control.StoredLogins[1].Password,0,1,0,0,1));
		EmailSender = "www.reva.com";
		EmailSubject = "LEC Bank Account Details";
		EmailContent = "Due to being a new member of our group we have created a new LEC Account for you as you complete contracts funds will automatically go there." +
			"\n" + "URL: www.lecbank.com" + 
			"\n" + "Account Name: " + GameControl.control.StoredLogins[0].Username + 
			"\n" + "Account Number: " + GameControl.control.StoredLogins[1].Username + 
			"\n" + "Account Password: " + GameControl.control.StoredLogins[1].Password;
		//SendBankDetailsEmail();
	}

	void SendBankDetailsEmail()
	{
        GameControl.control.EmailData.Add (new EmailSystem (EmailSubject,"www.reva.com",GameControl.control.Time.FullDate, EmailContent, 0, 0, 0, false,EmailSystem.EmailType.New));
	}

	void SendEmail()
	{
        GameControl.control.EmailData.Add (new EmailSystem (EmailSubject,EmailSender,GameControl.control.Time.FullDate, EmailContent, 0, 0, 0, false,EmailSystem.EmailType.New));
	}

    void RemoveMission()
    {
		for (int i = 0; i < GameControl.control.BankData.Count; i++)
		{
			for (int j = 0; j < GameControl.control.BankData[i].Accounts.Count; j++)
			{
				if (GameControl.control.BankData[i].Accounts[j].Primary == true)
				{
					GameControl.control.BankData[i].Accounts[j].AccountBalance += GameControl.control.Contracts[Select].Cash;
				}
			}
		}
		GameControl.control.Rep[0].CurrentRep += GameControl.control.Contracts[Select].Rep;
		SendContractorEmail();
		//GameControl.control.EmailData.RemoveAt(email.ContractSelect);
//		email.Contracts.RemoveAt(email.ContractSelect);
//		email.ContractSelect = -1;
		GameControl.control.EmailData.RemoveAt(email.EmailIndex);
		email.EmailIndex = -1;
		GameControl.control.Contracts.RemoveAt(Select);
		done = true;
        Select = 0;
		done = false;
    }

	void RemoveFirstMission()
	{
		for (int i = 0; i < GameControl.control.BankData.Count; i++)
		{
			for (int j = 0; j < GameControl.control.BankData[i].Accounts.Count; j++)
			{
				if (GameControl.control.BankData[i].Accounts[j].Primary == true)
				{
					GameControl.control.BankData[i].Accounts[j].AccountBalance += GameControl.control.Contracts[Select].Cash;
				}
			}
		}
		GameControl.control.Rep[0].CurrentRep += GameControl.control.Contracts[Select].Rep;
		//GameControl.control.EmailData.RemoveAt(email.ContractSelect);
		//		email.Contracts.RemoveAt(email.ContractSelect);
		//		email.ContractSelect = -1;
		GameControl.control.EmailData.RemoveAt(email.EmailIndex);
		email.EmailIndex = -1;
		GameControl.control.Contracts.RemoveAt(Select);
		done = true;
		Select = 0;
		done = false;
	}

	void RemoveRankMission()
	{
		//SendContractorEmail();
        RemoveMission();
		done = true;
		Select = 0;
		done = false;
	}

	void RemoteFileCheck()
	{
		if(GameControl.control.CompanyServerData.Count > 0)
		{
			for (int Index = 0; Index < GameControl.control.CompanyServerData.Count; Index++)
			{
				if (GameControl.control.CompanyServerData[Index].Files.Count > 0)
				{
					for (int i = 0; i < GameControl.control.CompanyServerData[Index].Files.Count; i++)
					{
						FileNames.Add(GameControl.control.CompanyServerData[Index].Files[i].Name);
					}
				}
			}
		}
	}

	void LocalFileCheck()
	{
		for (int i = 0; i < GameControl.control.ProgramFiles.Count; i++) 
		{
			FileNames.Add (GameControl.control.ProgramFiles[i].Name);
		}
	}

	void TargetFileCheck()
	{
		
	}

	public void SkipMission()
	{
		SelectedResponse = Random.Range(1, 3);
		switch (SelectedResponse)
		{
			case 1:
				EmailSubject = "Contract Completion";
				EmailContent = "We have wired the agreed ammount we have also taken the liberty of increasing your reputation with us and our friends well be in touch.";
				SelectedResponse = 0;
				break;
			case 2:
				EmailSubject = "Contract Completion";
				EmailContent = "We have transferred the agreed ammount and your reputation with us has improved";
				SelectedResponse = 0;
				break;
		}
		for (int i = 0; i < GameControl.control.EmailData.Count; i++)
		{
			if (GameControl.control.EmailData[i].Type == EmailSystem.EmailType.Contract)
			{
				GameControl.control.EmailData.RemoveAt(i);
			}
		}
		GameControl.control.StoredLogins.Add(new LoginSystem("REVA", StringGenerator.RandomNumberChar(4, 4), StringGenerator.RandomMixedChar(12, 12), 0));
		EmailSubject = "REVA Login Details";
		EmailContent = "Your new account has been created in this email you will find your login details for www.reva.com this is where you can find contracts,software,hardware upgrades once again congratz on being a new member" +
			"\n" + "Username: " + GameControl.control.StoredLogins[1].Username +
			"\n" + "Password: " + GameControl.control.StoredLogins[1].Password;
		SendContractorEmail();
		GameControl.control.StoryMis[1] = true;
	}

    void CompletedDeleteResponse()
    {
        SelectedResponse = Random.Range(1, 3);
        switch (SelectedResponse)
        {
            case 1:
                EmailSubject = "Contract Completion";
                EmailContent = "We can see that the file is deleted we will transfer the funds asap " + GameControl.control.Contracts[Select].Cash + " Has been transferred";
                break;
            case 2:
                EmailSubject = "Contract Completion";
                EmailContent = "We see that the file is destoryed we will wire the funds asap " + GameControl.control.Contracts[Select].Cash + " Has been transferred";
                break;
        }
        RemoveMission();
        SelectedResponse = 0;
        FileNames.RemoveRange(0, FileNames.Count);
    }

    void NotCompletedDeleteResponse()
    {
        SelectedResponse = Random.Range(1, 3);
        switch (SelectedResponse)
        {
            case 1:
                EmailSubject = "Contract Status";
                EmailContent = "We still see the file on there server make sure to delete";
                break;
            case 2:
                EmailSubject = "Contract Status";
                EmailContent = "Why are you contacting us when the file still exists";
                break;
        }
        GameControl.control.Contracts[Select].Patience -= 15;
        FileNames.RemoveRange(0, FileNames.Count);
        SendContractorEmail();
        SelectedResponse = 0;
    }

    void CompletedCopyResponse()
    {
        SelectedResponse = Random.Range(1, 3);
        switch (SelectedResponse)
        {
            case 1:
                EmailSubject = "Contract Completion";
                EmailContent = "We have wired the agreed ammount we have also taken the liberty of increasing your reputation with us and our friends well be in touch. " + GameControl.control.Contracts[Select].Cash + " Has been transferred";
                break;
            case 2:
                EmailSubject = "Contract Completion";
                EmailContent = "We have transferred the agreed ammount and your reputation with us has improved " + GameControl.control.Contracts[Select].Cash + " Has been transferred";
                break;
        }
        RemoveMission();
        SelectedResponse = 0;
    }

    void NotCompletedCopyResponse()
    {
        SelectedResponse = Random.Range(1, 3);
        switch (SelectedResponse)
        {
            case 1:
                EmailSubject = "Contract Status";
                EmailContent = "We have not recived the correct file you better not be toying with us.";
                break;
            case 2:
                EmailSubject = "Contract Status";
                EmailContent = "We got a file but not the file we want give us what we want";
                break;
        }
        GameControl.control.Contracts[Select].Patience -= 15;
        SendContractorEmail();
        SelectedResponse = 0;
    }

    void SentNoAttachmentResponse()
    {
        SelectedResponse = Random.Range(1, 3);
        switch (SelectedResponse)
        {
            case 1:
                EmailSubject = "Contract Status";
                EmailContent = "We havent we recived a file at all make sure to attach it";
                SelectedResponse = 0;
                break;
            case 2:
                EmailSubject = "Contract Status";
                EmailContent = "We didnt get a file on our end double check that you have sent it";
                SelectedResponse = 0;
                break;
        }
        GameControl.control.Contracts[Select].Patience -= 15;
        SendContractorEmail();
    }

    public void Complete()
    {
		switch (GameControl.control.Contracts[Select].Type)
        {
		case MissionSystem.MissionType.JDelete:
			RemoteFileCheck();
			for (int i = 0; i < FileNames.Count; i++) 
			{
				if (FileNames.Contains(GameControl.control.Contracts [Select].File))
				{
                        NotCompletedDeleteResponse();
				}
				else 
				{
                        CompletedDeleteResponse();
				}
			}
            break;
		case MissionSystem.MissionType.PJDelete:
			RemoteFileCheck();
			for (int i = 0; i < FileNames.Count; i++) 
			{
				if (FileNames.Contains(GameControl.control.Contracts [Select].File))
				{
                        NotCompletedDeleteResponse();
				}
				else 
				{
                        CompletedDeleteResponse();
				}
			}
			break;

		case MissionSystem.MissionType.PJCopy:
			if (AttachtedFile != "") 
			{
				if (AttachtedFile == GameControl.control.Contracts [Select].File)
				{
                        CompletedCopyResponse();
				}
				else 
				{
                        NotCompletedCopyResponse();
				}
			}
			else 
			{
                    SentNoAttachmentResponse();
            }
            break;

		case MissionSystem.MissionType.PUCopy:
			if (AttachtedFile != "") 
			{
				if (AttachtedFile == GameControl.control.Contracts [Select].File)
				{
                    CompletedCopyResponse();
				}
				else 
				{
                    NotCompletedCopyResponse();
				}
			}
			else 
			{
                    SentNoAttachmentResponse();
			}
			break;

		case MissionSystem.MissionType.JCopy:
			if (AttachtedFile != "") 
			{
				if (AttachtedFile == GameControl.control.Contracts [Select].File)
				{
                        CompletedCopyResponse();
                }
				else 
				{
                     NotCompletedCopyResponse();
				}
			}
			else 
			{
                SentNoAttachmentResponse();
			}
			break;

		case MissionSystem.MissionType.UDelete:
			RemoteFileCheck();
			for (int i = 0; i < FileNames.Count; i++) 
			{
				if (FileNames.Contains(GameControl.control.Contracts [Select].File))
				{
                        NotCompletedDeleteResponse();
				}
				else 
				{
                        CompletedDeleteResponse();
				}
			}
        	break;

		case MissionSystem.MissionType.PUDelete:
			RemoteFileCheck();
			for (int i = 0; i < FileNames.Count; i++) 
			{
				if (FileNames.Contains(GameControl.control.Contracts [Select].File))
				{
                        NotCompletedDeleteResponse();
				}
				else 
				{
                        CompletedDeleteResponse();
				}
			}
			break;

		case MissionSystem.MissionType.PBCopy:
			if (AttachtedFile != "") 
			{
				if (AttachtedFile == GameControl.control.Contracts [Select].File)
				{
                        CompletedCopyResponse();
                }
				else 
				{
                        NotCompletedCopyResponse();
                }
			}
			else 
			{
                    SentNoAttachmentResponse();
            }
			break;

		case MissionSystem.MissionType.PBDelete:
			RemoteFileCheck();
			for (int i = 0; i < FileNames.Count; i++) 
			{
				if (FileNames.Contains(GameControl.control.Contracts [Select].File))
				{
                        NotCompletedDeleteResponse();
				}
				else 
				{
                        CompletedDeleteResponse();
				}
			}
			break;

		case MissionSystem.MissionType.BDelete:
			RemoteFileCheck();
			for (int i = 0; i < FileNames.Count; i++) 
			{
				if (FileNames.Contains(GameControl.control.Contracts [Select].File))
				{
                    NotCompletedDeleteResponse();
				}
				else 
				{
                        CompletedDeleteResponse();
				}
			}
			break;

		case MissionSystem.MissionType.PTCopy:
			if (AttachtedFile != "") 
			{
				if (AttachtedFile == GameControl.control.Contracts [Select].File)
				{
					SelectedResponse = Random.Range (1, 3);
					switch (SelectedResponse) 
					{
					case 1:
						EmailSubject = "Contract Completion";
						EmailContent = "We have wired the agreed ammount we have also taken the liberty of increasing your reputation with us and our friends well be in touch.";
						SelectedResponse = 0;
						break;
					case 2:
						EmailSubject = "Contract Completion";
						EmailContent = "We have transferred the agreed ammount and your reputation with us has improved";
						SelectedResponse = 0;
						break;
					}
					RemoveFirstMission();
					GameControl.control.StoredLogins.Add (new LoginSystem ("REVA", StringGenerator.RandomNumberChar(4, 4), StringGenerator.RandomMixedChar(12, 12), 0));
					EmailSubject = "REVA Login Details";
					EmailContent = "Your new account has been created in this email you will find your login details for www.reva.com this is where you can find contracts,software,hardware upgrades once again congratz on being a new member" + 
						"\n" + "Username: " + GameControl.control.StoredLogins[1].Username + 
						"\n" + "Password: " + GameControl.control.StoredLogins[1].Password;
					GameControl.control.StoryMis[1] = true;
					SendEmail();
					//SendAccountDetailsEmail();
				}
				else 
				{
					SelectedResponse = Random.Range (1, 3);
					switch (SelectedResponse) 
					{
					case 1:
						EmailSubject = "Contract Status";
						EmailContent = "We have not recived the correct file you better not be toying with us.";
						SelectedResponse = 0;
						break;
					case 2:
						EmailSubject = "Contract Status";
						EmailContent = "We got a file but not the file we want give us what we want";
						SelectedResponse = 0;
						break;
					}
					GameControl.control.Contracts [Select].Patience -= 15;
					SendContractorEmail();
				}
			}
			else 
			{
				SelectedResponse = Random.Range (1, 3);
				switch (SelectedResponse) 
				{
				case 1:
					EmailSubject = "Contract Status";
					EmailContent = "We havent we recived a file at all make sure to attach it";
					SelectedResponse = 0;
					break;
				case 2:
					EmailSubject = "Contract Status";
					EmailContent = "We didnt get a file on our end double check that you have sent it";
					SelectedResponse = 0;
					break;
				}
				GameControl.control.Contracts [Select].Patience -= 15;
				SendContractorEmail();
			}
			break;


		//case MissionSystem.MissionType.TCopy:
		//	if(GameControl.control.MyFiles.Contains(GameControl.control.Contracts[Select].File))
		//	{
		//		RemoveMission();
		//		GameControl.control.StoryMis[3] = true;
		//	}
		//	else
		//	{

		//	}
		//	break;

            case MissionSystem.MissionType.UniUpgrade:
                int IndexPerson = PersonController.control.PeoplesName.IndexOf(GameControl.control.Contracts[Select].Target);
                if (PersonController.control.People[IndexPerson].University.Grade == GameControl.control.Contracts[Select].File)
                {
                    RemoveMission();
                }
                else
                {

                }
                break;
		}
	}
}
