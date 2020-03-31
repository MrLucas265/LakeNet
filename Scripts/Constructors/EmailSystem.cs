using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EmailSystem
{
	public string Subject;
	public string Sender;
	public string Date;
	public string Content;
	public EmailType Type;
	public int Encryption;
	public int Id;
	public int Size;
	public bool Infected;

	public enum EmailType
	{
		New,
		Read,
		Sent,
		Junk,
		Important,
		Contract
	}

	public EmailSystem(string subject, string sender, string date,string content,int encryption,int id,int size,bool infected, EmailType type)
	{
		Subject = subject;
		Sender = sender;
		Date = date;
		Content = content;
		Type = type;
		Encryption = encryption;
		Id = id;
		Size = size;
		Infected = infected;
	}

	public EmailSystem()
	{
		Id = -1;
	}

}