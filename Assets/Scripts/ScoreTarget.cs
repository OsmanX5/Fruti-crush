using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTarget : MonoBehaviour
{
    int countInsideTime = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MovingFruit")
        {
            ScoreManger.score += 10;
            Destroy(collision.gameObject);
        }
    }
}
