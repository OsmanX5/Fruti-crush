using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDown : MonoBehaviour
{

    item item;
    public bool moving = false;
    Rigidbody2D rb;
    BoxCollider2D col;

    private void Awake()
    {
        item = this.GetComponent<item>();
        rb = this.GetComponent<Rigidbody2D>();
        col = this.GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if (!moving && item.Fruitsarround[1] == null && !item.touchingTheGround) StartCoroutine(movingDown());
    }
    IEnumerator movingDown()
    {
        moving = true;
        yield return new WaitForSeconds(1f);
        Debug.Log(item.Fruitsarround[1] == null);
        while (item.Fruitsarround[1] == null && !item.touchingTheGround)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0.2f;
            
        }
        rb.gravityScale = 0f;
        moving = false;
    }

}
