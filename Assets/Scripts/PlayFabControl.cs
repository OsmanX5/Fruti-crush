using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
public class PlayFabControl : MonoBehaviour
{
    public static bool LogedIn = false;
    public void Login()
    {
        var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceName, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, LogInSuccessed, RequestFail);
    }
    void LogInSuccessed(LoginResult _)
    {
        Debug.Log("LogInSuccessed");
        sendScoreToBord(ScoreManger.score);
        
    }
    void RequestFail(PlayFabError _)
    {
        Debug.Log("LogInFaild");
    }

    public void sendScoreToBord(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighestScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, LeaderBordUpdatedSuccefully, RequestFail);
    }

    public void GetLeadebord()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighestScore",
            MaxResultsCount = 3,
            StartPosition = 0
        };
        PlayFabClientAPI.GetLeaderboard(request, LeaderBordGetedSuccefully, RequestFail);
    }
    void LeaderBordUpdatedSuccefully(UpdatePlayerStatisticsResult _)
    {
        Debug.Log("LEADER BORD UPDATED");
        GetLeadebord();
    }
    void LeaderBordGetedSuccefully(GetLeaderboardResult result)
    {
        Dictionary<int, List<string>> leaderBord = new Dictionary<int, List<string>>();
        foreach (var item in result.Leaderboard)
        {
            Debug.Log(item.Position+ " | " +item.PlayFabId +" | "+ item.StatValue);
            leaderBord[item.Position] = new List<string> { item.PlayFabId, item.StatValue.ToString() };
        }
        LeaderBordScreen.leaderBord = leaderBord;
        Debug.Log("LEADER BORD geted successfully");
    }

}
