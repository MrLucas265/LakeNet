using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPU : MonoBehaviour
{
    public List<float> GPUSpeed = new List<float>();
    public List<float> GPUCores = new List<float>();
    public List<float> Voltages = new List<float>();

    public string CPUName;

    public int Cores;
    public float MaxCPUSpeed;
    public float FactoryMaxSpeed;
    public float FactoryMinSpeed;
    public float TotalCorePower;
    public float TotalGPUPower;

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



    // Use this for initialization
    void Start()
    {
        HardwareController.hdcon.Load();
        CoolDown = 0.15f;
        UpdateCPUStats();

        FanSize = 92;
        FanEff = 0.9f;
        MaxFanRPM = 4800;

        MaxCPUSpeed = FactoryMaxSpeed * HardwareController.hdcon.CPUVoltage;

        MaxFlowRate = MaxFanRPM / FanSize * 2 * FanEff;

        for (int i = 0; i < GameControl.control.Gateway.InstalledGPU.Count; i++)
        {
            GPUCores.Add(GameControl.control.Gateway.InstalledGPU[i].Cores);
        }

        for (int i = 0; i < GameControl.control.Gateway.InstalledGPU.Count; i++)
        {
            Voltages.Add(GameControl.control.Gateway.InstalledGPU[i].Voltage);
        }

    }

    public void UpdateCPUStats()
    {
        Voltage = HardwareController.hdcon.CPUVoltage;
    }

    // Update is called once per frame
    void AirFlowMath()
    {
        AirFlow = Random.Range(0.97f, 1.03f);
    }

    public void OverclockMath()
    {
        if (HardwareController.hdcon.CPUVoltage != 0)
        {
            MaxCPUSpeed = FactoryMaxSpeed * HardwareController.hdcon.CPUVoltage;
        }
    }

    void Update()
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
    }

    void CPUMath()
    {
        MaxCPUSpeed = GameControl.control.Gateway.InstalledCPU[0].MaxSpeed;
        if (GPUCores.Count > 0)
        {
            TotalGPUPower = 0;
            for (int i = 0; i < Cores; i++)
            {
                TotalGPUPower = TotalGPUPower + GPUSpeed[i];
            }

            for (int i = 0; i < GameControl.control.Gateway.InstalledGPU.Count; i++)
            {
                GameControl.control.Gateway.InstalledGPU[i].PowerDraw = GameControl.control.Gateway.InstalledGPU[i].Usage / GameControl.control.Gateway.InstalledGPU[i].PowerEff * GameControl.control.Gateway.InstalledGPU[i].Voltage;
            }

            CPUTemp = 0;

            RemainingCPUUsage = MaxCPUSpeed - Usage;

            if (Locked == true && CPUTemp > ThrottleTEMP)
            {
                Usage -= 0.01f * Cores;
                MaxCPUSpeed -= 0.01f * Cores;
            }
        }
    }

    public void CPUHealthDegredationMath()
    {
        for (int i = 0; i < GameControl.control.Gateway.InstalledGPU.Count; i++)
        {
            GameControl.control.Gateway.InstalledGPU[i].CurrentHealth -= GameControl.control.Gateway.InstalledGPU[i].DegredationRate * GameControl.control.Gateway.InstalledGPU[i].Voltage;
            GameControl.control.Gateway.InstalledGPU[i].HealthPercentage = GameControl.control.Gateway.InstalledGPU[i].CurrentHealth / GameControl.control.Gateway.InstalledGPU[i].MaxHealth * 100;
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
            GPUSpeed[i] = Usage / Cores;
        }
        setSpeed = false;
    }
}
