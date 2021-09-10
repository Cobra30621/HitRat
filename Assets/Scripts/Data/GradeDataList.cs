using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GradeDataList{
    public List<Grade> grades; // 暴力解

    public void Init(List<StageData> stageDatas){
        for (int i = 0; i < grades.Count; i++)
        {
            grades[i].count = 0;
            grades[i].grade = stageDatas[i].grade;
        }
    }

    public int GetTotalGrade(){
        int totalGrade = 0;
        foreach (Grade grade in grades)
        {
            totalGrade += grade.count * grade.grade; 
        }
        return totalGrade;
    }

    public void AddGrade(TapType tapType){
        grades[(int)tapType].count ++;

    }

    public TapType GetArriveTapType(){
        TapType tapType = TapType.Dog;
        foreach (Grade grade in grades)
        {
            if(grade.count != 0){
                tapType = grade.tapType;
            }
        }

        return tapType;
    }
}

[System.Serializable]
public class Grade{
    public TapType tapType;
    public int grade;
    public int count;
}