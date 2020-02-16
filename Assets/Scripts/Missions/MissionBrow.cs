using UnityEngine;
using System.Collections;

public class MissionBrow : MonoBehaviour 
{
    public int Select;
    public bool showAccept;
    private Computer com;
    //private Files files;
    private MissionGen misgen;
	private GameObject missions;
	private GameObject prompt;
	private EmailClient ec;
	private NotfiPrompt noti;
	// Use this for initialization
	void Start () 
    {
		missions = GameObject.Find("Missions");
		prompt = GameObject.Find("Prompts");
		misgen = missions.GetComponent<MissionGen>();
		noti = prompt.GetComponent<NotfiPrompt>();
		ec = GetComponent<EmailClient>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void Accept()
    {

		GameControl.control.Contracts.Add (misgen.MissionList[Select]);

		GameControl.control.EmailData.Add(new EmailSystem(misgen.MissionList[Select].Name,
			misgen.MissionList[Select].Address,GameControl.control.Time.FullDate,
			misgen.MissionList[Select].EDesc,0,1,1,false,
			EmailSystem.EmailType.Contract));

		noti.NewNotification("New Contract",misgen.MissionList[Select].Name,"You got a new contract.");

		misgen.MissionList.RemoveAt(Select);

//		noti.ShowNoti = true;
//		noti.Notification = "YOU GOT MAIL!!!";
//		noti.playsound = true;
//		noti.DisplayTime = 4;

		Select = -1;

		//GameControl.control.DateTime.Add(ProfileController.procon.Day + "/" + ProfileController.procon.Month + "/" + ProfileController.procon.Year + " " + ProfileController.procon.Hour.ToString("00") + ":" + ProfileController.procon.Min.ToString("00"));
    }
}
