using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDown : MonoBehaviour
{

    item item;
    public bool moving = false;
    public LayerMask layer;
    public GameObject Downme;
    public float distacne = 10f;
    BoxCollider2D myCollider;

    private void Awake()
    {
        item = this.GetComponent<item>();
        myCollider = this.GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if (!moving && item.aroundFruits["Down"] == null && !item.touchingTheGround) StartCoroutine(movingDown());
    }
    IEnumerator movingDown()
    {
        moving = true;
        RaycastHit2D hit2D;
        hit2D = Physics2D.Raycast(transform.position - new Vector3(0, 0.6f), Vector2.down, 1000, layer);
        if (hit2D.collider != null)
        {
            Downme = hit2D.collider.gameObject;
        }
        distacne = transform.position.y -  Downme.transform.position.y;
        while (distacne >= 1.1 )
        {
            if (Downme == null) 
                Downme = Physics2D.Raycast(transform.position - new Vector3(0, 0.6f), Vector2.down, 1000, layer).collider.gameObject;
            distacne = transform.position.y - Downme.transform.position.y;
            transform.position -= transform.up * 0.1f;
            if (Input.GetKeyDown(KeyCode.Escape)) break;
            yield return new WaitForSeconds(0.01f);
        }

        transform.position = new Vector3(transform.position.x, Downme.transform.position.y+1f);
        moving = false;

    }

}
