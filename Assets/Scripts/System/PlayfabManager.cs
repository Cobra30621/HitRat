using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    public LeaderboardUI leaderboardUI;
    public bool whetherLogin;
    public string playerId;

    string loggedInPlayfabId;
    // Start is called before the first frame update
    void Start()
    {
        if(whetherLogin)
            Login();
    }

    void Login(){
        var request = new LoginWithCustomIDRequest{
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams{
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccessLogin, OnError);
    }

    // public int score;
    public string name;

    public string[] names;

    [ContextMenu("RandomSendDatasToLeaderboard")]
    public void RandomSendDatasToLeaderboard(){
        StartCoroutine(RandomSends());
    }

    IEnumerator RandomSends(){
        for (int i = 0; i < names.Length; i++)
        {
            name = names[i];
            yield return RandomSend();
        }
    }


    [ContextMenu("RandomSendLeaderboard")]
    public void RandomSendLeaderboard(){
        
        StartCoroutine(RandomSend());
    }

    IEnumerator RandomSend(){
        // Login
        string id = Random.Range(0, 100000) + "";
        var request = new LoginWithCustomIDRequest{
            CustomId = id,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams{
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccessLogin, OnError);

        yield return new WaitForSeconds(5f);
        // SetName
        SubmitNameButton(name);

        yield return new WaitForSeconds(5f);
        // SendData
        Score();
        yield return new WaitForSeconds(2f);
        Debug.Log("新增資料完成");
    }


    public void SubmitNameButton(string name){
        var request = new UpdateUserTitleDisplayNameRequest{
            DisplayName = name,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    

    [ContextMenu("SendScore")]
    public void Score(){
        int score =  Random.Range(0, 10000);
        SendLeaderboard(score);
    }


    public void SendLeaderboard(int score){
        var request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "RatScore", Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboradUpdate, OnError);
    }


    [ContextMenu("GetLeaderBoard")]
    public void GetLeaderBoard(){
        var request = new GetLeaderboardRequest{
            StatisticName = "RatScore",
            StartPosition = 0,
            MaxResultsCount = 100
        };

        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    [ContextMenu("GetLeaderBoardAroundPlayer")]
    public void GetLeaderBoardAroundPlayer(){
        var request = new GetLeaderboardAroundPlayerRequest{
            StatisticName = "RatScore",
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnLeaderboardAroundPlayerGet, OnError);
    }

    void OnLeaderboardGet(GetLeaderboardResult result){
        leaderboardUI.OnLeaderboardGet(result);
    }

    void OnLeaderboardAroundPlayerGet(GetLeaderboardAroundPlayerResult result){
        leaderboardUI.OnLeaderboardAroundPlayerGet(result, loggedInPlayfabId);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result){
        Debug.Log("Successful login");
        
    }

    void OnSuccessLogin(LoginResult result){
        loggedInPlayfabId = result.PlayFabId;
        Debug.Log("Successful login/account Create");
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        
        if(name == null){

        }

    }

    void OnError(PlayFabError error){
        Debug.Log("Error while login");
        Debug.Log(error.GenerateErrorReport());
    }

    void OnLeaderboradUpdate(UpdatePlayerStatisticsResult result ){
        Debug.Log("Successfull leaderboard sent");
    }

}

