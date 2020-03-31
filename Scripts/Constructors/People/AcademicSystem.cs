using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AcademicSystem
{
    public List<CollageSystem> CollageQualifications = new List<CollageSystem>();
    public List<UniversitySystem> UniversityQualifications = new List<UniversitySystem>();
    public string OtherQualifications;

    public AcademicSystem(List<CollageSystem> collagequalifications, List<UniversitySystem> universityqualifications, string otherqualifications)
    {
        CollageQualifications = collagequalifications;
        UniversityQualifications = universityqualifications;
        OtherQualifications = otherqualifications;
    }

}
