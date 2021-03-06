using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidingData : MonoBehaviour
{

    GameObject collidedObject = null;

    public GameObject CollidedObject { get => collidedObject; set => collidedObject = value; }

    private void Awake()
    {
        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), transform.parent.GetComponent<Collider2D>());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<item>() != null)
        {
            collidedObject = collision.gameObject;
        }     
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<item>() != null)
        {
            collidedObject = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<item>() != null)
        {
            collidedObject = null;
        }
    }
}
