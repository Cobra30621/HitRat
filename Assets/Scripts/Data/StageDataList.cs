using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDataList", menuName = "ScriptableObjects/StageDataList")]
public class StageDataList : ScriptableObject
{
    public List<StageData> stageDatas;

}

[System.Serializable]
public class StageData{
    public TapType tapType;
    public int needTapCount;
    public int grade;

}
