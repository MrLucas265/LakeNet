using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Registry
{
	public static string GetStringData(string PersonsName,string KeyName,string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataString;
	}

	public static void SetStringData(string PersonsName, string KeyName, string ValueName,string StringData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataString = StringData;
	}

	public static int GetIntData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataInt;
	}

	public static void SetIntData(string PersonsName, string KeyName, string ValueName, int IntData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataInt = IntData;
	}

	public static void SetRequestData(string PersonsName, string KeyName, string ValueName, ProgramRequest RequestData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.Request = RequestData;
	}

	public static ProgramRequest GetRequestData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.Request;
	}

	public static void SetColorData(string PersonsName, string KeyName, string ValueName, SColor color)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorData = color;
	}
	public static void SetRedColorData(string PersonsName, string KeyName, string ValueName, byte FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorData.r = FloatData;
	}
	public static void SetGreenColorData(string PersonsName, string KeyName, string ValueName, byte FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorData.g = FloatData;
	}
	public static void SetBlueColorData(string PersonsName, string KeyName, string ValueName, byte FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorData.b = FloatData;
	}
	public static void SetAlphaColorData(string PersonsName, string KeyName, string ValueName, byte FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorData.a = FloatData;
	}
	public static byte GetAlphaColorData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorData.a;
	}
	public static byte GetRedColorData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorData.r;
	}
	public static byte GetGreenColorData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorData.g;
	}
	public static byte GetBlueColorData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorData.b;
	}

	public static SColor GetColorData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorData;
	}

	public static void SetAlphaFloatColorData(string PersonsName, string KeyName, string ValueName, float FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorFloatData.Alpha = FloatData;
	}
	public static void SetFloatColorData(string PersonsName, string KeyName, string ValueName, float RFloatData,float GFloatData, float BFloatData, float AFloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorFloatData.Red = RFloatData;
		RegValue.ColorFloatData.Green = GFloatData;
		RegValue.ColorFloatData.Blue = BFloatData;
		RegValue.ColorFloatData.Alpha = AFloatData;
	}
	public static void SetRedFloatColorData(string PersonsName, string KeyName, string ValueName, float FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorFloatData.Red = FloatData;
	}
	public static void SetGreenFloatColorData(string PersonsName, string KeyName, string ValueName, float FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorFloatData.Green = FloatData;
	}
	public static void SetBlueFloatColorData(string PersonsName, string KeyName, string ValueName, float FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorFloatData.Blue = FloatData;
	}

	public static float GetAlphaFloatColorData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorFloatData.Alpha;
	}
	public static float GetRedFloatColorData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorFloatData.Red;
	}
	public static float GetGreenFloatColorData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorFloatData.Green;
	}
	public static float GetBlueFloatColorData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorFloatData.Blue;
	}

	public static ColorSystem GetFloatColorData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorFloatData;
	}

	public static void SetBoolData(string PersonsName, string KeyName, string ValueName, bool BoolData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataBool = BoolData;
	}

	public static bool GetBoolData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataBool;
	}

	public static void SetRectData(string PersonsName, string KeyName, string ValueName, SRect RectData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataRect = RectData;
	}
	public static void SetWidthData(string PersonsName, string KeyName, string ValueName, float Width)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataRect.width = Width;
	}

	public static void SetHeightData(string PersonsName, string KeyName, string ValueName, float Height)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataRect.height = Height;
	}

	public static SRect GetRectData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataRect;
	}

	public static void SetFloatData(string PersonsName, string KeyName, string ValueName, float FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataFloat = FloatData;
	}

	public static float GetFloatData(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataFloat;
	}

	public static void SetVector3Data(string PersonsName, string KeyName, string ValueName, SVector3 Vector3Data)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataVector3 = Vector3Data;
	}

	public static SVector3 GetVector3Data(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataVector3;
	}

	public static void SetVector2Data(string PersonsName, string KeyName, string ValueName, SVector2 Vector2Data)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataVector2 = Vector2Data;
	}

	public static SVector2 GetVector2Data(string PersonsName, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.Registry.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataVector2;
	}

	//public static void CreateNewKeyValue(string PersonsName, string KeyName, string ValueName, string StringData, bool BoolData, int IntData, float FloatData)
	//{
	//		int Count = 0;

	//		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);

	//	for (Count = 0; Count < person.Gateway.Registry.Count; Count++)
	//       {
	//		if (person.Gateway.Registry[Count].KeyName == KeyName)
	//           {
	//			person.Gateway.Registry[Count].Values.Add(new RegistryDataSystem(ValueName, StringData, IntData, BoolData, FloatData));
	//		}
	//	}

	//	if (Count >= person.Gateway.Registry.Count)
	//	{
	//		if (person.Gateway.Registry[Count].KeyName != KeyName)
	//		{
	//			person.Gateway.Registry[Count].Values.Add(new RegistryDataSystem(ValueName, StringData, IntData, BoolData, FloatData));
	//		}
	//	}

	//	for (int i = 0; i < person.Gateway.Registry[Count].Values.Count;i++)
	//       {
	//		person.Gateway.Registry[Count].Values.Add(new RegistryDataSystem(ValueName, StringData, IntData, BoolData, FloatData));
	//	}
	//}
}
