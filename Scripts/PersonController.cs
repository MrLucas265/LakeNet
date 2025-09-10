using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;


public class PersonController : MonoBehaviour
{
    public static PersonController control;

    public List<PeopleSystem> People = new List<PeopleSystem>();

    public GlobalSystem Global;

    public string ProfilePath;
    public string ProfileName;

    public string ActualFilePath;

    void Awake()
    {
        //if (!Directory.Exists(ProfilePath))
        //{
        //    if (ProfilePath != "")
        //    {
        //        Directory.CreateDirectory(ProfilePath);
        //    }
        //}

        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        //ActualFilePath = Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber + "/people/" + ProfileName;
    }

    // Update is called once per frame
    void Update()
    {
        ActualFilePath = Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber + "/people/" + ProfileName;
    }

    public void DeleteFile()
    {
        File.Delete(Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber + "/people/" + ProfileName + ".dat");
        File.Delete(Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber + "/people/" + ProfileName + ".json");
    }

    public void Save()
    {
        //RuntimeText.WriteString();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(ActualFilePath +".dat");
        CustomData data = new CustomData();

        data.People = People;
        data.Global = Global;

        string JsonString = JsonUtility.ToJson(data);
        StreamWriter sw = new StreamWriter(ActualFilePath + ".json");
        sw.Write(JsonString);
        sw.Close();

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load() // JSON
    {
        if (File.Exists(ActualFilePath + ".json"))
        {
            StreamReader sr = new StreamReader(ActualFilePath + ".json");
            string json = sr.ReadToEnd();
            //CompressionHelper.DecompressFile(ActualFilePath + ".json");
            CustomData data = JsonUtility.FromJson<CustomData>(json);

            sr.Close();

            People = data.People;
            Global = data.Global;
        }
    }

    //public void Load() // Dat
    //{
    //    if (File.Exists(ActualFilePath + ".dat"))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        FileStream file = File.Open(ActualFilePath + ".dat", FileMode.Open);
    //        CustomData data = (CustomData)bf.Deserialize(file);

    //        file.Close();

    //        People = data.People;
    //    }
    //}

    [Serializable]
    class CustomData
    {
        public List<PeopleSystem> People = new List<PeopleSystem>();

        public GlobalSystem Global;
    }
}
