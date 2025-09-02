using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingGame : MonoBehaviour 
{
	public List<string> EnemyWords = new List<string>(); // This contains all the words we picked from the dictionary
	public List<string> DictionaryOfWords = new List<string>(); // This contains all the words were going to use
	public int WordSelect;
	public string MenuSelector;

	public string TypedWord;
	public string TypedWord1;

	public float Timer;
	public float Cooldown;

    public float MTimer;
    public float MCooldown;

    private GameObject PersonController;

    public PasswordList NamesList;

    public List<float> enemyPOSX = new List<float>();
    public List<float> enemyPOSY = new List<float>();

    public float MoveMod;

    public int Count;

    public int CorrectWords;
    public int InCorrectWords;
    public int Score;

    public float Math;

    public GUISkin skin;

    public bool Game;

    public bool TimerBool;

    void Start ()
	{
        PersonController = GameObject.Find("System");
        NamesList = PersonController.GetComponent<PasswordList>();
        NamesList.PasswordListResource();
        Cooldown = 5;
        MCooldown = 0.5f;
        MoveMod = 25;

    }
	
	// Update is called once per frame
	void Update () 
	{
		if(TimerBool == true)
        {
            Timers();
        }
	}

	void AddWords()
	{
		WordSelect = Random.Range (0, DictionaryOfWords.Count - 1);
		EnemyWords.Add(DictionaryOfWords[WordSelect].Trim());
	}

	public void MiniGameRender()
	{
        GUI.skin = skin;

        TimerBool = true;

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.Return)
        {
            for (Count = 0; Count < EnemyWords.Count; Count++)
            {
                int i = Count;
                if (TypedWord == EnemyWords[i])
                {
                    Score += TypedWord.Length;
                    Math = TypedWord.Length / 10;
                    EnemyWords.RemoveAt(i);
                    enemyPOSX.RemoveAt(i);
                    enemyPOSY.RemoveAt(i);
                    TypedWord = "";
                    CorrectWords++;
                    if(MoveMod < 100)
                    {
                        MoveMod += 5;
                    }
                    //MoveMod+=5;
                    Cooldown -= 0.05f;
                    MCooldown -= 0.01f;
                }
            }
        }

        TypedWord = GUI.TextField(new Rect(0, 270, 200, 22), TypedWord);

        GUI.TextField(new Rect(210, 270, 80, 22),"CW: " + CorrectWords);

        GUI.TextField(new Rect(300, 270, 80, 22), "Missed: " + InCorrectWords);

        GUI.TextField(new Rect(390, 270, 100, 22), "Score: " + Score);

        for (int i = 0; i < enemyPOSX.Count; i++)
        {
            GUIContent content = new GUIContent(EnemyWords[i]);

            GUIStyle style = GUI.skin.box;
            style.alignment = TextAnchor.UpperLeft;

            Vector2 size = style.CalcSize(content);

            GUI.Box(new Rect(enemyPOSX[i], enemyPOSY[i],size.x, 23), EnemyWords[i]);
        }
    }

	public void GameRender()
	{
		switch (MenuSelector)
		{
		case "Main Menu":
			MiniGameRender();
			break;
		}
	}

	void CreateEnemy()
	{
        EnemyWords.Add(NamesList.PasswordWords[Random.Range(0, NamesList.PasswordWords.Count - 1)].Trim());
        enemyPOSX.Add(-10);
        enemyPOSY.Add(Random.Range(30, 250));
    }

	void Timers()
	{
        Spawn();
        Move();
    }

    void Spawn()
    {
        if (enemyPOSX.Count < 50)
        {
            Timer += 1 * Time.deltaTime;

            if (Timer >= Cooldown)
            {
                CreateEnemy();
                Timer = 0;
            }
        }
    }

    void Move()
    {
        MTimer += 1 * Time.deltaTime;

        if (MTimer >= MCooldown)
        {
            if (enemyPOSX.Count > 0)
            {
                for (int i = 0; i < enemyPOSX.Count; i++)
                {
                    enemyPOSX[i] += 1 * Time.deltaTime * MoveMod;

                    if (enemyPOSX[i] > 500)
                    {
                        InCorrectWords++;
                        EnemyWords.RemoveAt(i);
                        enemyPOSX.RemoveAt(i);
                        enemyPOSY.RemoveAt(i);
                    }
                }
            }
            MTimer = 0;
        }
    }
}
