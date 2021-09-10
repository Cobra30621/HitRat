using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradeBar : MonoBehaviour
{
    public Image image;
    public Text lab_grade;
    public Text lab_count;

    public void SetInfo(Grade grade, Sprite sprite){
        lab_count.text = "x " + grade.count;
        lab_grade.text = "" + grade.grade;
        image.sprite = sprite;
    }

}
