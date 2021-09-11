using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public GameObject rowPrefab;
    public GameObject playerPrefab;
    public Transform rowsParent;

    public void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult result, string playerID){
        panel.SetActive(true);

        foreach (Transform item in rowsParent){
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            // 將分數為零，去除排行榜，玩家例外
            if(item.StatValue != 0 | playerID == item.PlayFabId){
                GameObject prefab = Instantiate(rowPrefab, rowsParent);
                Text[] texts = prefab.GetComponentsInChildren<Text>();
                texts[0].text = (item.Position + 1).ToString();
                texts[1].text = item.DisplayName;
                texts[2].text = item.StatValue.ToString();

                if(playerID == item.PlayFabId){ // 設置玩家獨立分數
                    texts[0].color = Color.yellow;
                    texts[1].color = Color.yellow;
                    texts[2].color = Color.yellow;
                }
            }
            

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.DisplayName 
            + " " + item.StatValue);
        }
    }

    public void OnLeaderboardGet(GetLeaderboardResult result){
        panel.SetActive(true);

        foreach (Transform item in rowsParent){
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject prefab = Instantiate(rowPrefab, rowsParent);
            Text[] texts = prefab.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position + 1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            // if(playerID == item.PlayFabId){ // 設置玩家獨立分數
            //     Text[] playerTexts = prefab.GetComponentsInChildren<Text>();
            //     playerTexts[0].text = (item.Position + 1).ToString();
            //     playerTexts[1].text = item.DisplayName;
            //     playerTexts[2].text = item.StatValue.ToString();
            //     Debug.Log("Player" + item.Position + " " + item.PlayFabId + " " + item.DisplayName 
            // + " " + item.StatValue);
            // }

            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.DisplayName 
            + " " + item.StatValue);
        }
    }

    public void ClosePanel(){
        panel.SetActive(false);
    }

}