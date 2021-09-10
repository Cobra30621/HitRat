using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TapDataList", menuName = "ScriptableObjects/TapDataList")]
public class TapDataList : ScriptableObject
{
    public List<TapData> tapDatas;
    
    public Sprite GetTapSprite(TapType tapType){
        foreach (TapData data in tapDatas)
        {
            if(data.tapType == tapType){
                return data.sprite;
            }
        }
        Debug.Log($"找不到Sprite{tapType}");

        return null;
    }

    public AudioClip GetTapAudioClip(TapType tapType){
        foreach (TapData data in tapDatas)
        {
            if(data.tapType == tapType){
                return data.audioClip;
            }
        }
        Debug.Log($"找不到audioClip{tapType}");

        return null;
    }

}

[System.Serializable]
public class TapData{
    public TapType tapType;
    public Sprite sprite;
    public AudioClip audioClip;

}

[System.Serializable]
public enum TapType{
    Dog = 0, Cat = 1, Horse = 2, GuineaPig = 3, Rat = 4
}