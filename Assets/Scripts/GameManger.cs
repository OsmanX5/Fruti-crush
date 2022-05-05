using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManger : MonoBehaviour
{

    public AudioClip collectingSound;
    public AudioSource AudSource;

    public void playCollectingSound()
    {
        if (AudSource.isPlaying == false)
        {
            AudSource.PlayOneShot(collectingSound);
        }
    }

    public void RestartTheGame()
    {
        ScoreManger.score = 0;
        
        SceneManager.LoadScene("Game");
    }
    public void gameOver()
    {

        SceneManager.LoadScene("GameOver");
    }
    public void OpenAScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
