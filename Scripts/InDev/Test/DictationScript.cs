using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System.Text.RegularExpressions;

public class DictationScript : MonoBehaviour
{
    [SerializeField]
    public string Hypotheses;

    [SerializeField]
    public string RecognizedWords;


    public DictationRecognizer DictationRecognizer;

    public bool Listening;

    public string Status;

    public List<string> PastCommands = new List<string>();
    public List<string> Functions = new List<string>();
    public List<string> CommandNames = new List<string>();
    public List<string> DirectoryHistory = new List<string>();
    public List<CLICMDS> SystemCommands = new List<CLICMDS>();
    public string[] inputArray;
    public string[] ParseArray;
    public int ParseArrayLength;
    public int CurrentParse;

    // Use this for initialization
    void Start()
    {
        DictationRecognizer = new DictationRecognizer();

        DictationRecognizer.DictationResult += (text, confidence) =>
        {
            //Debug.LogFormat("Dictation result: {0}",text);
            RecognizedWords += text + "\n";
        };

        DictationRecognizer.DictationHypothesis += (text) =>
        {
            //Debug.LogFormat("Dictation hypothesis: {0}", text);
            Hypotheses += text;
        };

        DictationRecognizer.DictationComplete += (completationCause) =>
        {
            if (completationCause != DictationCompletionCause.Complete)
            {
                Debug.LogFormat("Dictation completed unsucessfully: {0}", completationCause);
            }
        };

        DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogFormat("Dictation error: {0}; HResult = {1} ", error, hresult);
        };
    }

    void Update()
    {
        Status = "" + DictationRecognizer.Status;
        if (Listening == true)
        {
            DictationRecognizer.Start();
        }
        else
        {
            DictationRecognizer.Stop();
            Hypotheses = "";
            RecognizedWords = "";
            //DictationRecognizer.Dispose();
        }

        if (RecognizedWords != "")
        {
            CommandCheck();
        }
    }

    void Commands()
    {
        //if(RecognizedWords.)
    }

    public void CommandCheck()
    {
        Functions.RemoveRange(0, Functions.Count);
        //ParseArray = Parse.Split(' ');
        ParseArray = Regex.Split(RecognizedWords, Customize.cust.TerminalCommandCharacterSplit, RegexOptions.IgnoreCase);
        ParseArrayLength = ParseArray.Length;
        if (GameControl.control.Commands.Count > 0)
        {
            for (int i = 0; i < GameControl.control.Commands.Count; i++)
            {
                //ParseArray = Regex.Split(Parse,GameControl.control.Commands[i].Name,RegexOptions.IgnoreCase);
                for (int j = 0; j < ParseArray.Length; j++)
                {
                    CurrentParse = j;
                    if (ParseArray[j] == GameControl.control.Commands[i].Name)
                    {
                        if (!Functions.Contains(GameControl.control.Commands[i].Func))
                        {
                            Functions.Add(GameControl.control.Commands[i].Func);
                        }
                    }
                }
            }
        }

        RecognizedWords = "";
    }
}
