using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU : MonoBehaviour
{
	public List<float> CPUSpeed = new List<float>();
    public List<float> CPUCores = new List<float>();
    public List<float> Voltages = new List<float>();

    public string CPUName;

	public int Cores;
	public float MaxCPUSpeed;
	public float FactoryMaxSpeed;
	public float FactoryMinSpeed;
	public float TotalCorePower;
	public float TotalCpuPower;

	public float RemainingCPUUsage;

	public float CPUTemp;
	public float MaxTEMP;
	public float ThrottleTEMP;

	public float PowerDraw;
	public float PowerEff;

	public float AirFlow;

	public float cd;
	public float CoolDown;

	public bool Throttled;
	public bool Locked;

	public int CPUEff;

	public float Voltage;

	public float PowerEffSetting;
	public float MaxTempSetting;

	public bool CoreThrottle;

	//private CPUParts cpup;

	//Program Stuff
	public bool Program;
	public float ProTime;
	public float WebSecLevel;
	public float Usage;
	public bool setSpeed;
	//-----

	//Fan Stats
	public float MaxFlowRate;
	public float MaxFanRPM;
    public float CurrentFanRPM;
	public float FanSize;
	public float FlowRate;
	public float FanSpeed;
	public float FanEff;
	public float Wattage;

    public float Timer;

    private GameObject Software;



    // Use this for initialization
    void Start ()
	{
        Software = GameObject.Find("Software");

        HardwareController.hdcon.Load();
		CoolDown = 0.15f;
		UpdateCPUStats();

		FanSize = 92;
		FanEff = 0.9f;
		MaxFanRPM = 4800;

		MaxCPUSpeed = FactoryMaxSpeed * HardwareController.hdcon.CPUVoltage;

		MaxFlowRate = MaxFanRPM / FanSize * 2 * FanEff;

        for (int i = 0; i < GameControl.control.Gateway.InstalledCPU.Count; i++)
        {
            CPUCores.Add(GameControl.control.Gateway.InstalledCPU[i].Cores);
        }

        for (int i = 0; i < GameControl.control.Gateway.InstalledCPU.Count; i++)
        {
            Voltages.Add(GameControl.control.Gateway.InstalledCPU[i].Voltage);
        }

    }

	public void UpdateCPUStats()
	{
		Voltage = HardwareController.hdcon.CPUVoltage;
	}
	
	// Update is called once per frame
	void AirFlowMath()
	{
		AirFlow = Random.Range (0.97f, 1.03f);
	}

	public void OverclockMath()
	{
		if (HardwareController.hdcon.CPUVoltage != 0) 
		{
			MaxCPUSpeed = FactoryMaxSpeed * HardwareController.hdcon.CPUVoltage;
		}
	}

	void Update () 
	{
		cd += Time.deltaTime;
        Timer += Time.deltaTime;

		CPUMath();

        if (Timer > 1)
        {
            CPUHealthDegredationMath();
        }

        if (cd >= CoolDown) 
		{
			AirFlowMath();
			cd = 0;
		}

		if (Program == true)
		{
			SetProgramStuff();
		}

		if (HardwareController.hdcon.CPUCheck == true) 
		{
			//HardwareCheck();
			HardwareController.hdcon.CPUCheck = false;
		}
	}

    void PowerCheck()
    {

    }

	void CPUMath()
	{
        MaxCPUSpeed = GameControl.control.Gateway.InstalledCPU[0].MaxSpeed;

        if (CPUCores.Count > 0)
		{
            TotalCpuPower = 0;
            for (int i = 0; i <= Cores; i++)
            {
                TotalCpuPower = TotalCpuPower + CPUSpeed[i];
            }

            for (int i = 0; i < GameControl.control.Gateway.InstalledCPU.Count; i++)
            {
                GameControl.control.Gateway.InstalledCPU[i].PowerDraw = GameControl.control.Gateway.InstalledCPU[i].IdlePowerDraw + GameControl.control.Gateway.InstalledCPU[i].Usage / GameControl.control.Gateway.InstalledCPU[i].PowerEff * GameControl.control.Gateway.InstalledCPU[i].Voltage;
            }

            CPUTemp = 0;

            RemainingCPUUsage = MaxCPUSpeed - Usage;

			if(Locked == true && CPUTemp > ThrottleTEMP)
			{
				Usage-= 0.01f*Cores;
				MaxCPUSpeed -= 0.01f*Cores;
			}		
		}
	}

    public void CPUHealthDegredationMath()
    {
        for (int i = 0; i < GameControl.control.Gateway.InstalledCPU.Count; i++)
        {
            if (GameControl.control.Gateway.InstalledCPU[i].CurrentHealth <= 0)
            {
                GameControl.control.Gateway.InstalledCPU[i].CurrentHealth = 0;
            }

            GameControl.control.Gateway.InstalledCPU[i].SpeedDiffrence = GameControl.control.Gateway.InstalledCPU[i].Usage - GameControl.control.Gateway.InstalledCPU[i].MaxSpeed * GameControl.control.Gateway.InstalledCPU[i].SpeedBoostMod;
            GameControl.control.Gateway.InstalledCPU[i].CurrentHealth -= GameControl.control.Gateway.InstalledCPU[i].DegredationRateMod;
            GameControl.control.Gateway.InstalledCPU[i].HealthPercentage = GameControl.control.Gateway.InstalledCPU[i].CurrentHealth / GameControl.control.Gateway.InstalledCPU[i].MaxHealth * 100;
            GameControl.control.Gateway.InstalledCPU[i].UsagePercent = GameControl.control.Gateway.InstalledCPU[i].Usage / GameControl.control.Gateway.InstalledCPU[i].MaxSpeed * 100;
            GameControl.control.Gateway.InstalledCPU[i].CurrentSpeed = GameControl.control.Gateway.InstalledCPU[i].MaxSpeed - GameControl.control.Gateway.InstalledCPU[i].SpeedDiffrence;
            GameControl.control.Gateway.InstalledCPU[i].DegredationRateMod = GameControl.control.Gateway.InstalledCPU[i].DegredationRate * GameControl.control.Gateway.InstalledCPU[i].Usage * GameControl.control.Gateway.InstalledCPU[i].Voltage;

            if (GameControl.control.Gateway.InstalledCPU[i].HealthPercentage < 25)
            {
                GameControl.control.Gateway.InstalledCPU[i].Status  = "Critical";
            }
            if (GameControl.control.Gateway.InstalledCPU[i].HealthPercentage > 25)
            {
                GameControl.control.Gateway.InstalledCPU[i].Status = "Failing";
            }
            if (GameControl.control.Gateway.InstalledCPU[i].HealthPercentage > 50)
            {
                GameControl.control.Gateway.InstalledCPU[i].Status = "Healthly";
            }
        }
        Timer = 0;
    }

	void SetProgramStuff()
	{
		ProTime = 1;
		WebSecLevel = 240;
		ProTime *= WebSecLevel;
		ProTime /= Usage;

		if (setSpeed == true)
		{
			SetSpeeds();
		}
	}

	public void SetSpeeds()
	{
		for (int i = 0; i < Cores; i++)
		{
			CPUSpeed [i] = Usage / Cores;
		}
		setSpeed = false;
	}
}
