using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TapDataList tapDataList;
    public TapType [] tapIds;
    public int grade;
    public float time;

    public SFXManager sfx;

    public KeyCodePair[] keyCodePairs;

    [System.Serializable]
    public struct KeyCodePair{
        public KeyCode keyCode;
        public TapType tapType;
    }

    [Header("UI")]
    [SerializeField] private GameObject tapBarPrefab;
    [SerializeField] private Transform tapBar_pos;
    [SerializeField] private List<TapBar> tapBarList;
    [SerializeField] private Text lab_score;

    void Start(){
        GameSettings.ResetToDefaults();

        for (int i = 0; i < 5; i++)
        {
            AddTapBar();
        }
    }

    void Update(){
        InputProcess();

        lab_score.text = $"Score: {grade}";
    }



    public void InputProcess(){
        foreach (KeyCodePair pair in keyCodePairs)
        {
            if (Input.GetKeyDown(pair.keyCode))
            {
                OnTap(pair.tapType);
            }
        }
    }

    

    [ContextMenu("StartGaming")]
    public void StartGame(){
        StartCoroutine(GameCoroutine());
    }

    IEnumerator GameCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
    }

    public void OnTap(TapType playerTap){
        TapBar currentBar = tapBarList[0];
        if(currentBar.tapType == playerTap){ // 按對
            Debug.Log($"playerTap:{playerTap}currentBar.tapType {currentBar.tapType}");
            grade ++;
            sfx.PlaySFX(  GetTapAudioClip(playerTap));
            AddTapBar();
            DestoryTapBar(currentBar);
        }
        else{ // 按錯

        }
    }

    public TapBar AddTapBar(){
        // TapType tapType = TapType.Dog;
        TapType tapType = (TapType) Random.Range(0,4);

        GameObject prefab = Instantiate(tapBarPrefab, tapBar_pos);
        TapBar tapBar = prefab.GetComponent<TapBar>();
        tapBar.SetInfo((int)tapType, tapType, GetTapSprite(tapType));

        tapBarList.Add(tapBar);

        tapBar.transform.SetSiblingIndex(0); // 改變物件位置

        return tapBar;
    }

    public void DestoryTapBar(TapBar tapBar){
        tapBarList.Remove(tapBar);
        Destroy(tapBar.gameObject);
    }

    private Sprite GetTapSprite(TapType tapType){
        foreach (TapData data in tapDataList.tapDatas)
        {
            if(data.tapType == tapType){
                return data.sprite;
            }
        }
        Debug.Log($"找不到Sprite{tapType}");

        return null;
    }

    private AudioClip GetTapAudioClip(TapType tapType){
        foreach (TapData data in tapDataList.tapDatas)
        {
            if(data.tapType == tapType){
                return data.audioClip;
            }
        }
        Debug.Log($"找不到audioClip{tapType}");

        return null;
    }
}