using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LeaderBoardManager : MonoBehaviour
{
    public string playerName;

    public void SavePlayerName(string name){
        playerName = name;
        PlayerPrefs.SetString("playerName", name);

    }

    public void LoadPlayerName(){
        playerName = PlayerPrefs.GetString("playerName", "");
    }
}