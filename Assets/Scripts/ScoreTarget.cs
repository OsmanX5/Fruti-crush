using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreTarget : MonoBehaviour
{
    int countInsideTime = 0;
    bool startCounting = false;
    float countingTime = 1f;
    float timer = 0f;
    public TMP_Text floatingText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "MovingFruit")
        {
            if (startCounting == false) startCounting = true;
            countInsideTime += 1;
            Destroy(collision.gameObject);
        }
    }
    private void Update()
    {
        if (startCounting)
        {
            timer += Time.deltaTime;
        }
        if(timer >= countingTime)
        {
            timer = 0;
            startCounting = false;
            StartCoroutine(floatingTextUp());
            floatingText.text = "X"+countInsideTime.ToString();
            ScoreManger.score += countInsideTime +( countInsideTime /3 -1)* countInsideTime;
            countInsideTime = 0;
        }
    }
    IEnumerator floatingTextUp()
    {
        floatingText.gameObject.transform.position = transform.position;
        while (floatingText.gameObject.transform.position.y - transform.position.y < 1)
        {
            floatingText.gameObject.transform.Translate(Vector2.up * Time.deltaTime * 10);
            yield return new WaitForSeconds(0.01f);
        }
        floatingText.text = "";
        yield return null;
    }
}
