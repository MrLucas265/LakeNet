
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{

    DialogueParser parser;

    public string dialogue, characterName;
    public int lineNum;
    int pose;
    string position;
    public string[] options;
    public bool playerTalking;

    public string nameBox;
    public string dialogueBox;

    public bool reset;

    public bool NextLine;

    public List<string> OptionTextList = new List<string>();
    public int OptionSelection;
    public bool SelectOption;
    public string SelectedOption;
    public bool parseOptionSelection;

    // Use this for initialization
    void Start()
    {
        dialogue = "";
        characterName = "";
        pose = 0;
        position = "L";
        playerTalking = false;
        parser = this.GetComponent<DialogueParser>();
        lineNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (NextLine && playerTalking == false)
        {
            ShowDialogue();

            lineNum++;
            NextLine = false;
        }

        if(reset == true)
        {
            Reset();
        }

        if(parseOptionSelection)
        {
            ParseOption();
        }

        UpdateUI();

        if (playerTalking == true)
        {
            PlayerOptionSelection();
        }
    }

    public void ShowDialogue()
    {
        ParseLine();
    }

    void UpdateUI()
    {
        if (!playerTalking)
        {
        }
        dialogueBox = dialogue;
        nameBox = characterName;
    }

    void ParseLine()
    {
        if (parser.GetName(lineNum) != "Player")
        {
            playerTalking = false;
            characterName = parser.GetName(lineNum);
            dialogue = parser.GetContent(lineNum);
            pose = parser.GetPose(lineNum);
            position = parser.GetPosition(lineNum);
        }
        else
        {
            playerTalking = true;
            characterName = "";
            dialogue = "";
            pose = 0;
            position = "";
            options = parser.GetOptions(lineNum);
            ChatStuff();
        }
    }

    public void Reset()
    {
        playerTalking = false;
        lineNum = 0;
        Array.Clear(options, 0, options.Length);
        options = parser.GetOptions(lineNum);
        reset = false;
        parser.UpdateDialog = true;
    }

    public void ChatStuff()
    {
        for (int i = 0; i < options.Length; i++)
        {
            OptionTextList.Add(options[i].Split(':')[0]);
        }
    }

    public void PlayerOptionSelection()
    {
        if (SelectOption)
        {
            SelectedOption = options[OptionSelection].Split(':')[1];
            SelectOption = false;
        }
    }

    public void ParseOption()
    {
        string command = SelectedOption.Split(',')[0];
        string commandModifier = SelectedOption.Split(',')[1];
        playerTalking = false;
        if (command == "line")
        {
            lineNum = int.Parse(commandModifier);
            ShowDialogue();
        }
        else if (command == "scene")
        {
            Application.LoadLevel("Scene" + commandModifier);
        }
        else if (command == "goto")
        {
            lineNum = int.Parse(commandModifier);
            ShowDialogue();
        }
        parseOptionSelection = false;
    }
}

