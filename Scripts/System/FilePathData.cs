using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilePathData : MonoBehaviour
{
	public static string Path;
	public static bool FileSelected;
	public static string SelectFile(string path)
	{
		Path = path;
		FileSelected = true;
		return path;
	}

	public static void GetSelectedFile()
	{
		FileSelected = false;
		Path = "";
	}
}
