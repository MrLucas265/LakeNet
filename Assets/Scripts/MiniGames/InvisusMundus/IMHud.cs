using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMHud : MonoBehaviour 
{

	public List<Texture2D> HealthBars = new List<Texture2D>();
	public List<Rect> HealthBar = new List<Rect>();

	public float MaxHealth;
	public float CurrentHealth;
	public float HealthPercentage;
	public float HealthMulti;

	public Color32 HealthColor = new Color32(0, 0, 0, 0);
	public Color32 StaminaColor = new Color32(0, 0, 0, 0);
	public Color32 ManaColor = new Color32(0, 0, 0, 0);

	// Use this for initialization
	void Start () 
	{
		HealthBar.Add(new Rect(2,50,100,40));
		HealthBar.Add(new Rect(10, 56, 100, 25));
		HealthBar.Add(new Rect(2, 100, 100, 20));

		MaxHealth = 100;
		CurrentHealth = 100;

		HealthMulti = 0.81f;
		LoadPresetColors();
	}

	void LoadPresetColors()
	{
		HealthColor.r = 0;
		HealthColor.g = 0;
		HealthColor.b = 0;
		HealthColor.a = 255;

		StaminaColor.r = 148;
		StaminaColor.g = 0;
		StaminaColor.b = 211;
		StaminaColor.a = 255;

		ManaColor.r = 0;
		ManaColor.g = 255;
		ManaColor.b = 255;
		ManaColor.a = 255;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI()
	{

	}

	public void RenderHud()
	{
		RenderHealthBars();
	}

	public void RenderHealthBars()
	{
		HealthPercentage = CurrentHealth / MaxHealth * 100;
		GUI.color = Color.white;
		GUI.DrawTexture(HealthBar[2], HealthBars[2]);
		GUI.color = HealthColor;
		GUI.DrawTexture(new Rect(HealthBar[1].x, HealthBar[1].y, HealthPercentage * HealthMulti, HealthBar[1].height), HealthBars[1]);
		GUI.color = Color.white;
		GUI.DrawTexture(HealthBar[0], HealthBars[0]);
	}
}
