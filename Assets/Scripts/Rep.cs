using UnityEngine;
using System.Collections;

public class Rep : MonoBehaviour 
{
    private NotfiPrompt noti;
    private GameObject Prompt;

    void Start()
	{
        Prompt = GameObject.Find("Prompts");
        noti = Prompt.GetComponent<NotfiPrompt>();
    }
	void Update ()
	{
		if (GameControl.control.Rep.Count > 0) 
		{
			for (int i = 0; i < GameControl.control.Rep.Count; i++)
			{
				switch (GameControl.control.Rep[i].Name)
				{
					case "REVA":
						if (GameControl.control.StoredLogins.Count > 1)
						{
							if (GameControl.control.Rep[i].CurrentRep >= GameControl.control.Rep[i].RepLevelRequirement) 
							{
								GameControl.control.Rep[i].RepLevel += 1;
								GameControl.control.Rep[i].RepLevelMod = 1 + GameControl.control.Rep[i].RepLevel/100;
								GameControl.control.Rep[i].RepLevelRequirement = 100 * GameControl.control.Rep[i].RepLevel * GameControl.control.Rep[i].RepLevelMod;
								GameControl.control.EmailData.Add (new EmailSystem ("Reva Promotion","www.reva.com",GameControl.control.Time.FullDate,"Your reva grade has been increased Agent " + GameControl.control.StoredLogins[0].Username + " Your grade is now " + GameControl.control.Rep[i].RepLevel, 0, 0, 0, false,EmailSystem.EmailType.New));
                                noti.NewNotification("New Mail", "Reva Promotion", "Your reva grade has been increased Agent");
                            }
						}
					break;
				}
			}
		}
	}
}
