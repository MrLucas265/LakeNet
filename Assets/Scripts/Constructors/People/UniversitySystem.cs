using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UniversitySystem
{
    public string Attended;
    public string Qualifications;
    public string Grade;

    public UniversitySystem(string attended, string qualifications, string grade)
    {
        Attended = attended;
        Qualifications = qualifications;
        Grade = grade;
    }

}
