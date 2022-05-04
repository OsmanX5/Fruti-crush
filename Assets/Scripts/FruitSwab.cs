using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSwab : MonoBehaviour
{
    public bool canSwab = true;
    public static GameObject HoveringObject = null;
    public static GameObject SelectedObject = null;
    GameObject toSwab;
    public LayerMask layer;
    bool startDraging = false;
    bool endDraging = false;
    GameManger manger; 
    private void Awake()
    {
        manger = this.GetComponent<GameManger>();
    }
    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,100, layer);
        if(hit.collider != null)
        {
            HoveringObject = hit.collider.gameObject;
        }
    }
    private void Update()
    {
        
        if (HoveringObject != null && endDraging==false)
        {
            HoveringObject.GetComponent<SpriteRenderer>().color = new Color(180,180,180,0.5f);
            if (Input.GetMouseButtonDown(0) && !startDraging) {
                startDraging = true;
                SelectedObject = HoveringObject;
            }
            if(startDraging && HoveringObject!= SelectedObject)
            {
                toSwab = HoveringObject;
                endDraging = true;
                swab(SelectedObject, toSwab);
               // Debug.Log(SelectedObject.name + "  " + toSwab.name);
            }
        }
        if (Input.GetMouseButtonUp(0)) { 
            endDraging = false; 
            startDraging = false;
            SelectedObject = null;
            toSwab = null;
        }


    }
    void swab(GameObject firstObject,GameObject SecondObject)
    {
        firstObject.GetComponent<BoxCollider2D>().enabled = false;
        SecondObject.GetComponent<BoxCollider2D>().enabled = false;

        Vector3 temp = firstObject.transform.position;
        firstObject.transform.position = SecondObject.transform.position;
        SecondObject.transform.position = temp;
        
        firstObject.GetComponent<BoxCollider2D>().enabled = true;
        SecondObject.GetComponent<BoxCollider2D>().enabled = true;
        GameManger.waitingPlayerMove = false;
        ScoreManger.swabs -= 1;
        firstObject.GetComponent<item>().checkForMatches();
        SecondObject.GetComponent<item>().checkForMatches();
    }
}
