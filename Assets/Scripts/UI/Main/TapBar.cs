using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapBar : MonoBehaviour
{
    public TapType tapType;
    public Image [] images;
    public int tapId;
    public int grade;

    public void SetInfo(int id, TapType tapType, Sprite sprite, int grade){
        if(id >= images.Length){
            Debug.Log("照片超出範圍了");
            return;
        }

        foreach (Image item in images)
        {
            item.gameObject.SetActive(false);
        }

        images[id].gameObject.SetActive(true);
        images[id].sprite = sprite;

        tapId = id;
        this.tapType = tapType;
        this.grade = grade;
    }
}
