using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDown : MonoBehaviour
{
    // Script making the fruit moveing down if there is nothing untill ground
    // Mechanism : using raycast to detect what is the down game object and moveing untill reaching it
    // 
    public LayerMask layer;
    GameObject Downme;
    float distacne = 10f;
    RaycastHit2D hit2D;
    item item;
    BoxCollider2D myCollider;

    private void Awake()
    {
        item = this.GetComponent<item>();
        myCollider = this.GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // 1.Casting ray from under this collider to down working in fruit and ground layer
        hit2D = Physics2D.Raycast(transform.position + new Vector3(0, -0.51f), Vector2.down, 100f, layer);
        
        //2. check if there is object in under if it's found set Downme object
        if (hit2D.collider != null)
        {
            Downme = hit2D.collider.gameObject;
            //3.calculating the distacne from the player
            distacne = transform.position.y-Downme.transform.position.y;
        }
        // 4. if distance >1 move the fruit down ### note : the default distance between two object centers = 1
        if (distacne > 1)
        {
            transform.Translate(Vector2.down * Time.deltaTime * 4);
        }
        // 5. if the distance smaller than 1 so the fruit reached the body so put the fruit with 1 unity above
        if (distacne <= 1)
        {
            transform.position = new Vector2(transform.position.x, Downme.transform.position.y + 1);
        }
    }


}
