using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public static bool waitingPlayerMove = true;
    private void Start()
    {
        InvokeRepeating("CheckAllMatches", 1, 1);
        InvokeRepeating("MakeAllMatches", 1, 1);
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CheckAllMatches();
            MakeAllMatches();
        }
        if (Input.GetMouseButtonDown(0))
        {
            CheckAllMatches();
            MakeAllMatches();
        }

    }
    public void MakeAllMatches()
    {
        item[] allFruits = this.transform.GetComponentsInChildren<item>();
        foreach (var fruit in allFruits)
        {
            if (fruit.isMatched == true)
            {
                fruit.makeTheMatch();
            }
        }
    }
    public void CheckAllMatches()
    {
        item[] allFruits = this.transform.GetComponentsInChildren<item>();
        foreach (var fruit in allFruits)
        {
            fruit.checkForMatches();
        }
    }
}
