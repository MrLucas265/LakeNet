using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMenu : MonoBehaviour
{
	public bool show;
	public int windowID;

	private GameObject AppMenus;

	private AppatureAppMenu appatureAppMenu;
	private TreeOSAppMenu treeOSAppMenu;
	private IceOSAppMenu iceOSAppMenu;
	private EthelOSAppMenu ethelOSAppMenu;

	void Start () 
	{
		AppMenus = GameObject.Find("AppMenus");
		//Desktop Enviros
		appatureAppMenu = AppMenus.GetComponent<AppatureAppMenu>();
		treeOSAppMenu = AppMenus.GetComponent<TreeOSAppMenu>();
		iceOSAppMenu = AppMenus.GetComponent<IceOSAppMenu>();
		ethelOSAppMenu = AppMenus.GetComponent<EthelOSAppMenu>();
	}

	void Update()
	{
		switch (GameControl.control.SelectedOS.Name) 
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
