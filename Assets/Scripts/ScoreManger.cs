using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreManger : MonoBehaviour
{
    public static int score = 0;
    public int swabs = 20;
    public TMP_Text swabsText;
    public TMP_Text scoreText;
    private void Update()
    {
        swabsText.text = swabs.ToString();
        scoreText.text = score.ToString();
        if(swabs == 0)
        {
            this.GetComponent<GameManger>().gameOver();
        }
    }


}
