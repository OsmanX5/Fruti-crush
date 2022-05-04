using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LeaderBordScreen : MonoBehaviour
{
    public static Dictionary<int, List<string>> leaderBord =null;
    public List<TMP_Text> leaders_names;
    public List<TMP_Text> leaders_score;
    private void Start()
    {
        this.GetComponent<PlayFabControl>().Login();

    }
    private void Update()
    {
        if (leaderBord != null)
        {
            foreach (var item in leaderBord)
            {
                leaders_names[item.Key].text = item.Value[0];
                leaders_score[item.Key].text = item.Value[1];
            }
        }
    }

}
