using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
public class PlayFabControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }
    void Login()
    {
        var request = new LoginWithCustomIDRequest { CustomId = "Osman", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, LogInSuccessed, RequestFail);
    }
    void LogInSuccessed(LoginResult _)
    {
        Debug.Log("LogInSuccessed");
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

    void LeaderBordUpdatedSuccefully(UpdatePlayerStatisticsResult _)
    {
        Debug.Log("LEADER BORD UPDATED");
    }
    
}
