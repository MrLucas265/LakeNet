//using UnityEngine;
//using System.Collections;
//
//public class BlinkingCursor : MonoBehaviour {      
//
//	private float m_TimeStamp;
//	private bool cursor = false;
//	private string cursorChar = "";
//	private int maxStringLength = 200;
//	public string enteredString; 
//
//	void update() {
//		if (Time.time - m_TimeStamp >= 0.5)
//		{
//			m_TimeStamp = Time.time;
//			if (cursor == false)
//			{
//				cursor = true;
//				if (enteredString.Length < maxStringLength)
//				{
//					cursorChar += "_";
//				}
//			}
//			else
//			{
//				cursor = false;
//				if (cursorChar.Length != 0)
//				{
//					cursorChar = cursorChar.Substring(0, cursorChar.Length - 1);
//				}
//			}
//		}
//	}
//}