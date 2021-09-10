using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TapDataList tapDataList;
    public StageDataList stageDataList;
    public TapType currentTapType;
    public StageData currentStage;

    public int currentStageId;
    public int stageTapCount;
    public int grade;
    
    [Header("時間")]
    public float time;
	private float totalTime = 30;
	public float nowTime{
		get{
			if(time < 0)
				return 0;
			else
				return time;
		}
	}
    public bool timeOut;
    
    

    public SFXManager sfx;

    public KeyCodePair[] keyCodePairs;

    [System.Serializable]
    public struct KeyCodePair{
        public int id;
        public KeyCode keyCode;
    }

    [Header("UI")]
    [SerializeField] private GameObject tapBarPrefab;
    [SerializeField] private Transform tapBar_pos;
    [SerializeField] private List<TapBar> tapBarList;
    [SerializeField] private Text lab_score;
    [SerializeField] private Text lab_time;

    void Start(){
        GameSettings.ResetToDefaults();

        GameInit();
    }

    public void Retry(){
        GameInit();
    }

    [ContextMenu("GameInit")]
    private void GameInit(){
        grade = 0;
        stageTapCount = 0;
        currentStageId = 0;
        currentStage = stageDataList.stageDatas[0];
        time = totalTime;
        timeOut = false;
        

        DestoryAllTapBar();

        for (int i = 0; i < 5; i++)
        {
            AddTapBar();
        }
    }

    void Update(){
        UpdateTime();
        InputProcess();
        UpdateUI();
        
    }

    private void UpdateTime(){
		time -= Time.deltaTime;
		if(time < 0){
            timeOut = true;
			Debug.Log("遊戲結束");
		}
	}

    private void UpdateUI(){
        lab_score.text = $"Score: {grade}";
        lab_time.text = $"Time: " + Mathf.Ceil(nowTime);
    }

    
    public void InputProcess(){
        if(timeOut){return;}

        foreach (KeyCodePair pair in keyCodePairs)
        {
            if (Input.GetKeyDown(pair.keyCode))
            {
                OnTap(pair.id);
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

    public void OnTap(int tapId){
        TapBar currentBar = tapBarList[0];
        if(currentBar.tapId == tapId){ // 按對
            // Debug.Log($"playerTap:{playerTap}currentBar.tapType {currentBar.tapType}");
            sfx.PlaySFX(  GetTapAudioClip(currentBar.tapType));
            AddTapBar();
            DestoryTapBar(currentBar);

            grade += currentBar.grade;
            
        }
        else{ // 按錯

        }
    }

    public TapBar AddTapBar(){
        // TapType tapType = TapType.Dog;
        int id = Random.Range(0,4);
        // TapType tapType = keyCodePairs[id].tapType;

        GameObject prefab = Instantiate(tapBarPrefab, tapBar_pos);
        TapBar tapBar = prefab.GetComponent<TapBar>();
        tapBar.SetInfo(id,currentStage.tapType,  GetTapSprite(currentStage.tapType), currentStage.grade);

        tapBarList.Add(tapBar);

        tapBar.transform.SetSiblingIndex(0); // 改變物件位置

        // 關卡數增加
        stageTapCount ++;
        JudgeNextStage();

        return tapBar;
    }

    private void JudgeNextStage(){
        if(stageTapCount >= currentStage.needTapCount){
            NextStage();
            stageTapCount = 0;
        }

    }

    private void NextStage(){
        currentStageId ++;
        if(currentStageId >= stageDataList.stageDatas.Count){
            Debug.Log($"currentStageId超出範圍{currentStageId}");
            currentStageId = stageDataList.stageDatas.Count - 1;
        }
        
        currentStage = stageDataList.stageDatas[currentStageId];
    }

    

    private void DestoryAllTapBar(){
        foreach (TapBar bar in tapBarList)
        {
            // if(bar != null)
            Destroy(bar.gameObject);
        }
        
        tapBarList = new List<TapBar>();
    }

    private void DestoryTapBar(TapBar tapBar){
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