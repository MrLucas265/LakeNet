//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//public class RuntimeText : MonoBehaviour
//{
//    public static void WriteString()
//    {
//        string path = Application.persistentDataPath + "/test.txt";
//        //Write some text to the test.txt file
//        StreamWriter writer = new StreamWriter(path, true);
//        writer.WriteLine("Test");
//        writer.Close();
//        StreamReader reader = new StreamReader(path);
//        //Print the text from the file
//        Debug.Log(reader.ReadToEnd());
//        reader.Close();
//    }
//    public static void ReadString()
//    {
//        string path = Application.persistentDataPath + "/test.txt";
//        //Read the text from directly from the test.txt file
//        StreamReader reader = new StreamReader(path);
//        Debug.Log(reader.ReadToEnd());
//        reader.Close();
//    }
//}