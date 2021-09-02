//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Skills : MonoBehaviour
//{

//	// Use this for initialization
//	void Start()
//	{

//	}

//	// Update is called once per frame
//	void Update()
//	{
//		if (GameControl.control.skills.Count > 0)
//		{
//			for (int i = 0; i < GameControl.control.skills.Count; i++)
//			{
//				if (GameControl.control.skills[i].Name == "Rifle Marksmanship")
//				{
//					if (GameControl.control.skills[i].CurrentXP >= GameControl.control.skills[i].LevelRequirement)
//					{
//						GameControl.control.skills[i].CurrentLevel += 1;
//						//GameControl.control.skills[i].LevelMod = 1 + GameControl.control.skills[i].CurrentLevel / 100;
//						//GameControl.control.skills[i].LevelRequirement = 100 * GameControl.control.skills[i].CurrentLevel * GameControl.control.skills[i].LevelMod;
//						//GameControl.control.EmailData.Add(new EmailSystem("Reva Promotion", "www.reva.com", GameControl.control.Time.FullDate, "Your reva grade has been increased Agent " + GameControl.control.StoredLogins[0].Username + " Your grade is now " + GameControl.control.Rep[i].RepLevel, 0, 0, 0, false, EmailSystem.EmailType.New));
//						//	//noti.NewNotification("New Mail", "Reva Promotion", "Your reva grade has been increased Agent");
//					}
//				}
//			}
//		}
//	}
//}