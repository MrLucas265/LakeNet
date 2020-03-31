using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollageSystem
{
    public string Attended;
    public string Qualifications;
    public string Grade;

    public CollageSystem(string attended, string qualifications, string grade)
    {
        Attended = attended;
        Qualifications = qualifications;
        Grade = grade;
    }

}
