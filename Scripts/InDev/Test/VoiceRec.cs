using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceRec : MonoBehaviour
{
	private Defalt def;
	private AppMan appman;
	public string ProgramName;
	public string Speech;
	public KeywordRecognizer keywordRecognizer;
	public Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
	// Use this for initialization
	void Start()
	{
		appman = GetComponent<AppMan>();
		KeywordList();
		keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
		keywordRecognizer.OnPhraseRecognized += KeywordRecognizerOnPhaseRecognized;
		keywordRecognizer.Start();
		def = GetComponent<Defalt>();
	}

	public void KeywordList()
	{
		keywords.Add("close notepad", () =>
		   {
			   Notepad();
		   });
		keywords.Add("application", () =>
		   {
			   Desktop();
		   });
		keywords.Add("show applications", () =>
		   {
			   Desktop();
		   });
		keywords.Add("close applications", () =>
		   {
			   Desktop();
		   });
		keywords.Add("Test", () =>
		   {
			   Test();
		   });
	}
	void KeywordRecognizerOnPhaseRecognized(PhraseRecognizedEventArgs args)
	{
		System.Action keywordAction;

		if (keywords.TryGetValue(args.text, out keywordAction))
		{
			keywordAction.Invoke();
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Run()
	{
		appman.SelectedApp = ProgramName;
	}
	void Close()
	{
		appman.SelectedApp = ProgramName;
	}

	void Notepad()
	{
		//def.shownote = !def.shownote;
		print("You just said Notepad");
	}

	void Desktop()
	{
		//def.showam = !def.showam;
		print("You just said Start");
	}

	void Test()
	{
		print("This is a working test");
	}
}
