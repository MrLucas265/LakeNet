using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoginSystem
{
	public string Name;
	public string Username;
	public string Password;
	public int Privileges;

	public LoginSystem (string name, string username, string password, int privileges)
	{
		Name = name;
		Username = username;
		Password = password;
		Privileges = privileges;
	}
}