using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using DG.Tweening;

public class StartUI : MonoBehaviour
{
    // public string playName;
    [SerializeField] private InputField inputField;
    [SerializeField] private GameObject panel;

    
    private string name;
    public PlayfabManager playfabManager;

    void Start(){
        // leaderBoardManager.LoadPlayerName();
        if(name != null)
            inputField.text = name;
    }

    public void StartGame(){
        panel.SetActive(false);
        GameManager.instance.StartGame(); // 開始遊戲
    }

    public void SetPlayerName(){
        playfabManager.SubmitNameButton(inputField.text);
        name = inputField.text;
        // leaderBoardManager.SavePlayerName(inputField.text);
        // playName = inputField.text;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying=false;
        #else
            Application.Quit();
        #endif
    }
    
}

/*
版權頁

【影片】

【圖片】
吉哇哇 // 狗 
pop cat // 貓
黃金船 // 馬
天竺鼠車車 // 天竺鼠


*/