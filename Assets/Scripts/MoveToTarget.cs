using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 1f;
    Vector2 dir;
    Rigidbody2D rb;
    private void Start()
    {
        target = GameObject.Find("ScoreTarget").GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(Move());
    }
    public IEnumerator Move()
    {
        yield return new WaitForSeconds(0.5f);
        dir = target.position - transform.position;
        while (Vector2.Distance(transform.position,target.position) > 0.1f)
        {
            
            transform.Translate(dir * Time.deltaTime * speed);
           
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
    }
}
