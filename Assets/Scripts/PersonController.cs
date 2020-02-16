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


    public List<OrgnizationSystem> Orgnizations = new List<OrgnizationSystem>();


    public List<string> PeoplesName = new List<string>();

    public List<string> CollageClasses = new List<string>();
    public List<string> CollageGrades = new List<string>();
    public List<string> UniClasses = new List<string>();
    public List<string> UniGrades = new List<string>();

    public string ProfilePath;
    public string ProfileName;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DeleteFile()
    {
        File.Delete(Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber + "/people/" + ProfileName + ".dat");
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber + "/people/" + ProfileName + ".dat");
        CustomData data = new CustomData();

        data.People = People;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber + "/people/" + ProfileName + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/saves/" + ProfileController.procon.VersionNumber + "/people/" + ProfileName + ".dat", FileMode.Open);
            CustomData data = (CustomData)bf.Deserialize(file);
            file.Close();

            People = data.People;
        }
    }

    [Serializable]
    class CustomData
    {
        public List<PeopleSystem> People = new List<PeopleSystem>();
    }
}
