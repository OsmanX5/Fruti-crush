using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
public class PlayFabControl : MonoBehaviour
{
    // Methdology
    // 1. Loging with MAC adress
    // 2. send your current score
    // 3. check if you have display name
    // 4. if you don't have display name activate "EnterYour name" screen and disactivce the "leader bord"
    //    4.1 when you press submit in "Enter your name" panel send a request to update the display name
    //    4.2 when the name updated successfully disactive "EnterYour name" screen and active "leader bord"
    // 5. Get the leader bord

    public static bool LogedIn = false;
    public GameObject LeaderBordObject;
    public GameObject EnterYourName;
    public TMP_Text Playername;
    
    //######## Play fab actions Functions#############
    
    public void Login()
    {
        Debug.Log("start making login request");
        var request = new LoginWithCustomIDRequest {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams { GetPlayerProfile = true }
        };
        Debug.Log("done the request");
        PlayFabClientAPI.LoginWithCustomID(request, LogInSuccessed, RequestFail);
        Debug.Log("done sending the request");
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
        PlayFabClientAPI.UpdatePlayerStatistics(request, ScoreSendSuccessed, RequestFail);
    }
    public void UpdateDisplayName()
    {
        Debug.Log("start making DisplayName updating request");
        Debug.Log(Playername.text);
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = Playername.text };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, UpdatedDisplayNameSuccessed, RequestFail);

    }
    public void GetLeadebord()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "HighestScore",
            MaxResultsCount = 3,
            StartPosition = 0
        };
        PlayFabClientAPI.GetLeaderboard(request, GetLeaderBordSuccessed, RequestFail);
    }


    //######## PlayFab Results Functions
    void LogInSuccessed(LoginResult result)
    {
        Debug.Log("LogInSuccessed" );
        
        if (MydisplyName(result.InfoResultPayload.PlayerProfile) == null)
        {
            EnterYourName.SetActive(true);
            LeaderBordObject.SetActive(false);
        }
        else
        {
            Debug.Log("Welcome " + MydisplyName(result.InfoResultPayload.PlayerProfile));
            EnterYourName.SetActive(false);
            LeaderBordObject.SetActive(true);
            sendScoreToBord(ScoreManger.score);
        }
    }
    void ScoreSendSuccessed(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("ScoreSendSuccessed");
        GetLeadebord();
    }
    void GetLeaderBordSuccessed(GetLeaderboardResult result)
    {
        Dictionary<int, List<string>> leaderBord = new Dictionary<int, List<string>>();
        foreach (var item in result.Leaderboard)
        {
            Debug.Log(item.Position + " | " + item.DisplayName + " | " + item.StatValue);
            leaderBord[item.Position] = new List<string> { item.DisplayName, item.StatValue.ToString() };
        }
        LeaderBordScreen.leaderBord = leaderBord;
        Debug.Log("LEADER BORD geted successfully");
    }
    void UpdatedDisplayNameSuccessed(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Successfully updated the name and it's" +result.DisplayName);
        sendScoreToBord(ScoreManger.score);
        EnterYourName.SetActive(false);
        LeaderBordObject.SetActive(true);
    }

    void RequestFail(PlayFabError error)
    {
        Debug.Log(error.ErrorMessage);
    }
   
    
    //###### Unity actions Functions
    string MydisplyName(PlayerProfileModel player)
    {
        if((player != null) && (player.DisplayName != null))
        {
            return player.DisplayName;
        }
        else return null;
    }
    

}
