using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using DG.Tweening;

public class ResultUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public Text lab_grade;

    public List<GradeBar> gradeBars;
    public TapDataList tapDataList;
    public VideoManager videoManager;

    private GradeDataList gradeDataList;
    private List<Grade> grades;
    private TapType arriveTapType;
    [SerializeField] private GameObject buttonPanel;

    public void Show(GradeDataList gradeDataList){
        panel.SetActive(true);
        buttonPanel.SetActive(false);

        this.gradeDataList = gradeDataList;
        grades = gradeDataList.grades;
        arriveTapType = gradeDataList.GetArriveTapType();

        StartCoroutine(Play());

    }

    IEnumerator Play(){
        if(arriveTapType == TapType.Rat){
            videoManager.PlayFullVideo(); // 直接撥完
        }
        else{
            videoManager.PlayVideo(arriveTapType); // 幫放影片
        }

        for (int i = 0; i < gradeBars.Count; i++)
        {
            if(grades[i].count != 0 | i == 0){ // 有打到的
                // 等待影片撥放
                float stopTime = videoManager.GetIntervelTime(grades[i].tapType);
                yield return new WaitForSeconds(stopTime);

                gradeBars[i].gameObject.SetActive(true);
                gradeBars[i].SetInfo(grades[i], tapDataList.GetTapSprite(grades[i].tapType));

            }
            else{ // 沒打到
                gradeBars[i].gameObject.SetActive(false);
            }
        }

        yield return new WaitForSeconds(0.5f);
        lab_grade.text = "Grade: " + gradeDataList.GetTotalGrade();
        buttonPanel.SetActive(true);
    }

    public void Close(){
        panel.SetActive(false);
    }

    public void Again(){
        SceneManager.LoadScene("Game");
    }

}