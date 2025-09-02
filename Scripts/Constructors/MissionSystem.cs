using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MissionSystem
{
	public string Name;
	public string File;
	public string Target;
	public string Address;
	public string MDesc;
    public string EDesc;
	public string Cat;
	public string ContractServer;
	public int IdleDeleteTime;
	public int Cash;
	public int Rep;
	public float Patience;
	public float LevelRequirement;
	public MissionType Type;

	public enum MissionType
	{
		//jaildew
		JDelete,
		PJDelete,
		PJCopy,
		JCopy,
		//unicom
		UDelete,
		PUDelete,
		UCopy,
		PUCopy,
		//test
		PTCopy,
		TCopy,
		PTDelete,
		TDelete,
		//becas
		BDelete,
		PBDelete,
		PBCopy,
		BCopy,
        //academic
        UniUpgrade,
        UniDowngrade,
        UniSwapClass,
        UniFullChange

    }

	public MissionSystem(string name,string file,string target,string address,string mdesc,string edesc,string cat,string contractserver,int idledeletetime,int cash,int rep, float patience,float levelrequirement, MissionType type)
	{
		Name = name;
		File = file;
		Target = target;
		Address = address;
		MDesc = mdesc;
        EDesc = edesc;
        Cat = cat;
		ContractServer = contractserver;
		IdleDeleteTime = idledeletetime;
		Cash = cash;
		Rep = rep;
		Type = type;
		Patience = patience;
		LevelRequirement = levelrequirement;
	}

	public void AcceptMission(string PersonName, MissionSystem AcceptedMission)
    {
		for(int i = 0; i < PersonController.control.People.Count;i++)
        {
            if (PersonController.control.People[i].Name == PersonName)
            {
                PersonController.control.People[i].Contracts.Add(AcceptedMission);
			}
        }
    }
}