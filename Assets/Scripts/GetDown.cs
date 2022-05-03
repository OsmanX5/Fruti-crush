using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDown : MonoBehaviour
{

    item item;
    public bool moving = false;
    public LayerMask layer;
    public GameObject Downme;
    public float distacne;
    BoxCollider2D myCollider;

    private void Awake()
    {
        item = this.GetComponent<item>();
        myCollider = this.GetComponent<BoxCollider2D>();

    }

    private void Update()
    {
        if (!moving && item.Fruitsarround[1] == null && !item.touchingTheGround) StartCoroutine(movingDown());
    }
    IEnumerator movingDown()
    {
        moving = true;
        yield return new WaitForSeconds(0.2f);
        RaycastHit2D hit2D;
        hit2D = Physics2D.Raycast(transform.position - new Vector3(0, 0.6f), Vector2.down, 1000, layer);
        if (hit2D.collider != null)
        {
            Downme = hit2D.collider.gameObject;
            distacne = hit2D.distance;
        }
        while (distacne > 0)
        {
            hit2D = Physics2D.Raycast(transform.position - new Vector3(0, 0.6f), Vector2.down, 1000, layer);
            distacne = hit2D.distance;
            transform.position -= transform.up * 0.06f;
            if (Input.GetKeyDown(KeyCode.Escape)) break;
            yield return new WaitForSeconds(0.01f);
        }
        moving = false;

    }

}
