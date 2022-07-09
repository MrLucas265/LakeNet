using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LocalRegistry
{


	public static void AddNewKey(string PersonsName, int PID, string KeyName)
    {
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);

		person.Gateway.RunningPrograms[PID].LocalRegister.Add(new RegistrySystem(KeyName));
	}

	public static void AddNewValue(string PersonsName, int PID, string KeyName,string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);

		reg.Values.Add(new RegistryDataSystem(ValueName));
	}

	public static void SetColorData(string PersonsName, int PID, string KeyName, string ValueName, SColor color)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorData = color;
	}
	public static void SetRedColorData(string PersonsName,int PID, string KeyName, string ValueName, byte FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorData.r = FloatData;
	}
	public static void SetGreenColorData(string PersonsName, int PID, string KeyName, string ValueName, byte FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorData.g = FloatData;
	}
	public static void SetBlueColorData(string PersonsName, int PID, string KeyName, string ValueName, byte FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorData.b = FloatData;
	}
	public static void SetAlphaColorData(string PersonsName, int PID, string KeyName, string ValueName, byte FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorData.a = FloatData;
	}
	public static byte GetAlphaColorData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorData.a;
	}
	public static byte GetRedColorData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorData.r;
	}
	public static byte GetGreenColorData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorData.g;
	}
	public static byte GetBlueColorData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorData.b;
	}

	public static SColor GetColorData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorData;
	}

	public static void SetAlphaFloatColorData(string PersonsName, int PID, string KeyName, string ValueName, float FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorFloatData.Alpha = FloatData;
	}
	public static void SetFloatColorData(string PersonsName, int PID, string KeyName, string ValueName, float RFloatData, float GFloatData, float BFloatData, float AFloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorFloatData.Red = RFloatData;
		RegValue.ColorFloatData.Green = GFloatData;
		RegValue.ColorFloatData.Blue = BFloatData;
		RegValue.ColorFloatData.Alpha = AFloatData;
	}
	public static void SetRedFloatColorData(string PersonsName, int PID, string KeyName, string ValueName, float FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorFloatData.Red = FloatData;
	}
	public static void SetGreenFloatColorData(string PersonsName, int PID, string KeyName, string ValueName, float FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorFloatData.Green = FloatData;
	}
	public static void SetBlueFloatColorData(string PersonsName, int PID, string KeyName, string ValueName, float FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ColorFloatData.Blue = FloatData;
	}

	public static float GetAlphaFloatColorData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorFloatData.Alpha;
	}
	public static float GetRedFloatColorData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorFloatData.Red;
	}
	public static float GetGreenFloatColorData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorFloatData.Green;
	}
	public static float GetBlueFloatColorData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorFloatData.Blue;
	}

	public static ColorSystem GetFloatColorData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ColorFloatData;
	}

	public static void AddStringData(string PersonsName, int PID, string KeyName, string ValueName, string StringData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.StringListData.Add(StringData);
	}

	public static string GetStringListData(string PersonsName, int PID, string KeyName, string ValueName, int array)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.StringListData[array];
	}

	public static void RemoveAtStringListData(string PersonsName, int PID, string KeyName, string ValueName, int array)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.StringListData.RemoveAt(array);
	}

	public static bool StringDataContains(string PersonsName, int PID, string KeyName, string ValueName, string StringData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.StringListData.Contains(StringData);
	}

	public static void RemoveAllStringData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.StringListData.RemoveRange(0, GetStringDataCount(PersonsName, PID, KeyName, ValueName));
	}

	public static int GetStringDataCount(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.StringListData.Count();
	}

	public static void AddProgramData(string PersonsName, int PID, string KeyName, string ValueName, ProgramSystemv2 ProgramData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ProgramData.Add(ProgramData);
	}

	public static void RemoveAtProgramData(string PersonsName, int PID, string KeyName, string ValueName, int array)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ProgramData.RemoveAt(array);
	}

	public static ProgramSystemv2 GetProgramData(string PersonsName, int PID, string KeyName, string ValueName,int array)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ProgramData[array];
	}

	public static bool ProgramDataContains(string PersonsName, int PID, string KeyName, string ValueName, ProgramSystemv2 ProgramData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ProgramData.Contains(ProgramData);
	}

	public static void RemoveAllProgramData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.ProgramData.RemoveRange(0, GetProgramDataCount(PersonsName, PID, KeyName, ValueName));
	}

	public static int GetProgramDataCount(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.ProgramData.Count();
	}

	public static string GetStringData(string PersonsName,int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataString;
	}

	public static void SetStringData(string PersonsName,int PID, string KeyName, string ValueName, string StringData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataString = StringData;
	}

	public static int GetIntData(string PersonsName,int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataInt;
	}

	public static void SetIntData(string PersonsName,int PID, string KeyName, string ValueName, int IntData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataInt = IntData;
	}

	public static void SetBoolData(string PersonsName, int PID, string KeyName, string ValueName, bool BoolData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataBool = BoolData;
	}

	public static bool GetBoolData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataBool;
	}

	public static void SetRectData(string PersonsName, int PID, string KeyName, string ValueName, SRect RectData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataRect = RectData;
	}

	public static SRect GetRectData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataRect;
	}

	public static void SetFloatData(string PersonsName, int PID, string KeyName, string ValueName, float FloatData)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataFloat = FloatData;
	}

	public static float GetFloatData(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataFloat;
	}

	public static void SetVector3Data(string PersonsName, int PID, string KeyName, string ValueName, SVector3 Vector3Data)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataVector3 = Vector3Data;
	}

	public static SVector3 GetVector3Data(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataVector3;
	}

	public static void SetVector2Data(string PersonsName, int PID, string KeyName, string ValueName, SVector2 Vector2Data)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		RegValue.DataVector2 = Vector2Data;
	}

	public static SVector2 GetVector2Data(string PersonsName, int PID, string KeyName, string ValueName)
	{
		var person = PersonController.control.People.FirstOrDefault(x => x.Name == PersonsName);
		var reg = person.Gateway.RunningPrograms[PID].LocalRegister.FirstOrDefault(x => x.KeyName == KeyName);
		var RegValue = reg.Values.FirstOrDefault(x => x.ValueName == ValueName);

		return RegValue.DataVector2;
	}
}
