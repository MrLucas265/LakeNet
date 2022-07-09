using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMenu : MonoBehaviour
{
	public bool show;
	public int windowID;

	private GameObject AppMenus;

	private Boot boot;

	private AppatureAppMenu appatureAppMenu;
	private TreeOSAppMenu treeOSAppMenu;
	private IceOSAppMenu iceOSAppMenu;
	private EthelOSAppMenu ethelOSAppMenu;

	void Start () 
	{
		boot = GetComponent<Boot>();

		AppMenus = GameObject.Find("AppMenus");
		//Desktop Enviros
		appatureAppMenu = AppMenus.GetComponent<AppatureAppMenu>();
		treeOSAppMenu = AppMenus.GetComponent<TreeOSAppMenu>();
		iceOSAppMenu = AppMenus.GetComponent<IceOSAppMenu>();
		ethelOSAppMenu = AppMenus.GetComponent<EthelOSAppMenu>();
	}

	void UpdateDesktopListV2()
	{
		for (int i = 0; i < PersonController.control.People.Count; i++)
		{
			for (int j = 0; j < PersonController.control.People[i].Gateway.CurrentOS.Partitions.Count; j++)
			{
				for (int k = 0; k < PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files.Count; k++)
				{
					if (PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files[k].PinToDesktop)
					{
						if (!PersonController.control.People[i].Gateway.CurrentOS.FPC.DesktopList.Contains(PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files[k]))
						{
							PersonController.control.People[i].Gateway.CurrentOS.FPC.DesktopList.Add(PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files[k]);
						}
					}
				}
			}
		}
	}
	void UpdateQuickListV2()
	{
		for (int i = 0; i < PersonController.control.People.Count; i++)
		{
			for (int j = 0; j < PersonController.control.People[i].Gateway.CurrentOS.Partitions.Count; j++)
			{
				for (int k = 0; k < PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files.Count; k++)
				{
					if (PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files[k].PinToMenu)
					{
						if (!PersonController.control.People[i].Gateway.CurrentOS.FPC.QuickList.Contains(PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files[k]))
						{
							PersonController.control.People[i].Gateway.CurrentOS.FPC.QuickList.Add(PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files[k]);
						}
					}
				}
			}
		}
	}
	void UpdateTaskBarListV2()
	{
		for (int i = 0; i < PersonController.control.People.Count; i++)
		{
			for (int j = 0; j < PersonController.control.People[i].Gateway.CurrentOS.Partitions.Count; j++)
			{
				for (int k = 0; k <= PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files.Count; k++)
				{
					if(k < PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files.Count)
                    {
						if (PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files[k].PinToBar)
						{
							if (!PersonController.control.People[i].Gateway.CurrentOS.FPC.BarList.Contains(PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files[k]))
							{
								PersonController.control.People[i].Gateway.CurrentOS.FPC.BarList.Add(PersonController.control.People[i].Gateway.CurrentOS.Partitions[j].Files[k]);
							}
						}
					}
					else
                    {
						GameControl.control.GlobalCheckForPinnedFiles = false;
					}
				}
			}
		}
	}

	void Update()
	{
		if(GameControl.control.GlobalCheckForPinnedFiles)
        {
			UpdateDesktopListV2();
			UpdateQuickListV2();
			UpdateTaskBarListV2();
		}

		switch (boot.SelectedOS.Name) 
		{
		case OperatingSystems.OSName.AppatureOS:
			if (show == true) 
			{
				appatureAppMenu.enabled = true;
				appatureAppMenu.show = true;
			} 
			else 
			{
				appatureAppMenu.enabled = false;
				appatureAppMenu.show = false;
			}
			break;
		case OperatingSystems.OSName.EthelOS:
			if (show == true) 
			{
				ethelOSAppMenu.enabled = true;
				ethelOSAppMenu.show = true;
			} 
			else 
			{
				ethelOSAppMenu.enabled = false;
				ethelOSAppMenu.show = false;
			}
			break;
		case OperatingSystems.OSName.TreeOS:
			if (show == true) 
			{
				treeOSAppMenu.enabled = true;
				treeOSAppMenu.show = true;
				//treeOSAppMenu.AppMenuState = 1;
			} 
			else 
			{
				//treeOSAppMenu.enabled = false;
				//treeOSAppMenu.show = false;
				//treeOSAppMenu.AppMenuState = 2;
			}
			break;
		case OperatingSystems.OSName.FluidicIceOS:
			if (show == true) 
			{
				iceOSAppMenu.enabled = true;
				iceOSAppMenu.show = true;
				//treeOSAppMenu.AppMenuState = 1;
			} 
			else 
			{
				//treeOSAppMenu.enabled = false;
				//treeOSAppMenu.show = false;
				//treeOSAppMenu.AppMenuState = 2;
			}
			break;
        case OperatingSystems.OSName.QuantinitumOS:
            if (show == true)
            {
                iceOSAppMenu.enabled = true;
                iceOSAppMenu.show = true;
                //treeOSAppMenu.AppMenuState = 1;
            }
            else
            {
                //treeOSAppMenu.enabled = false;
                //treeOSAppMenu.show = false;
                //treeOSAppMenu.AppMenuState = 2;
            }
            break;
        }
    }
}
