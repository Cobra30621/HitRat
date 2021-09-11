using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TapDataList tapDataList;
    public StageDataList stageDataList;
    public GradeDataList gradeDataList;
    public TapType currentTapType;
    public StageData currentStage;

    public int currentStageId;
    public int stageTapCount;
    public int grade;

    
    
    [Header("時間")]
    public float time;
	public float totalTime = 30;
	public float nowTime{
		get{
			if(time < 0)
				return 0;
			else
				return time;
		}
	}
    public bool timeOut;
    private bool hadShowResult;
    private bool gaming;
    
    

    public SFXManager sfx;

    public KeyCodePair[] keyCodePairs;

    [System.Serializable]
    public struct KeyCodePair{
        public int id;
        public KeyCode keyCode;
    }
    public int tapCount = 3;

    [Header("UI")]
    [SerializeField] private GameObject tapBarPrefab;
    [SerializeField] private Transform tapBar_pos;
    [SerializeField] private List<TapBar> tapBarList;
    [SerializeField] private Text lab_score;
    [SerializeField] private Text lab_time;
    public ResultUI resultUI;
    [SerializeField] private GameObject gamePanel;

    public LabAnime labAnime;

    void Awake(){
        instance = this;
    }

    void Start(){
        GameSettings.ResetToDefaults();
        gaming = false;
        // GameInit();
    }

    public void Retry(){
        StartGame();
    }


    void Update(){
        UpdateTime();
        InputProcess();
        UpdateUI();
        
    }

    private void UpdateTime(){
        if(!gaming){return;}

		time -= Time.deltaTime;
		if(time < 0){
            timeOut = true;
			gaming = false;
            if(!hadShowResult){
                StartCoroutine(ShowResult());
            }
            
		}
        
	}

    

    private void UpdateUI(){
        // lab_score.text = $"Score: {grade}";
        lab_time.text = "" + Mathf.Ceil(nowTime);
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
        GameInit();
        yield return new WaitForSeconds(0.4f);
        
        sfx.PlaySFX(1);

        for (int i = 3; i > 0 ; i -- )
        {
            yield return labAnime.Play(i + "");
        }

        yield return labAnime.Play("Start");
        gaming = true;

    }

    public void GameInit(){
        gamePanel.SetActive(true);
        stageTapCount = 0;
        currentStageId = 0;
        currentStage = stageDataList.stageDatas[0];
        time = totalTime;
        timeOut = false;
        hadShowResult = false;
        failing = false;
        

        gradeDataList.Init(stageDataList.stageDatas);

        DestoryAllTapBar();

        for (int i = 0; i < 5; i++)
        {
            AddTapBar();
        }
    }

    private IEnumerator ShowResult(){
        Debug.Log("遊戲結束");
        
        yield return labAnime.Play("Time Out");
        yield return new WaitForSeconds(0.4f);
        gamePanel.SetActive(false);
        resultUI.Show(gradeDataList); // 撥放結算動畫
        hadShowResult = true;
    }

    public bool failing;

    public void OnTap(int tapId){
        if(!gaming){return;}

        if(failing){return;}

        TapBar currentBar = tapBarList[0];
        if(currentBar.tapId == tapId){ // 按對
            // Debug.Log($"playerTap:{playerTap}currentBar.tapType {currentBar.tapType}");
            sfx.PlaySFX(  tapDataList.GetTapAudioClip(currentBar.tapType));
            AddTapBar();
            DestoryTapBar(currentBar);

            gradeDataList.AddGrade(currentBar.tapType);
            // grade += currentBar.grade;
            
        }
        else{ // 按錯
            failing = true;
            sfx.PlaySFX(0);
            StartCoroutine(PlayFailAnime(currentBar.tapId ));
        }
    }

    private IEnumerator PlayFailAnime(int tapId){
        yield return tapBarList[0].PlayFailAnime(tapId);

        failing = false;
    }

    public TapBar AddTapBar(){
        // TapType tapType = TapType.Dog;
        int id = Random.Range(0, tapCount);
        // TapType tapType = keyCodePairs[id].tapType;

        GameObject prefab = Instantiate(tapBarPrefab, tapBar_pos);
        TapBar tapBar = prefab.GetComponent<TapBar>();
        tapBar.SetInfo(id,currentStage.tapType, tapDataList.GetTapSprite(currentStage.tapType), currentStage.grade);

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

    
}