using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebsiteData : MonoBehaviour 
{
	public string FileName;
	public float FileSize;
	public string FileContent;
	public string FileLocation;
	public string InitalLocation;
	public Type FileType;

	public enum Type
	{
		Exe,
		Rar,
		Zip,
		Txt,
		Fdl,
		Dir
	}
}
