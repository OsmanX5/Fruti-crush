using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSwab : MonoBehaviour
{
    // Script controling in swaping 
    // attach to Game manger
    // Methdology:  1. check if I player hovering over and fruit and set the <HoveringObject> Object for what the player hovering
    //              2. if pressed left click make the hovering object <SelectedObject> and take a list of all arounds fruit for the hovering
    //              3. if the mouse move to hover an object from the upper list make the swab
    //              4. else return the <SelectedObject> to null;
    public bool isSwaping = true;
    public static GameObject HoveringObject = null;
    public static GameObject SelectedObject = null;
    public GameObject toSwab;
    public LayerMask layer;
    GameManger manger;
    public GameObject[] FruitsArroundSelectedObject = new GameObject[4] ; // List contain refrences for the gameobjects arround the selected object
    bool playerIsSelected = false; // bool variable viewing if the player selected an object 
    private void Awake()
    {
        manger = this.GetComponent<GameManger>();
    }
    private void FixedUpdate()
    {
       // 1.check if I player hovering over and fruit and set the<HoveringObject> Object for what the player hovering
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,100, layer);
        if(hit.collider != null)
        {
            HoveringObject = hit.collider.gameObject;
        }
    }
    private void Update()
    {
        // if the player hovering Over an object
        if (HoveringObject != null )
        {
            HoveringObject.GetComponent<SpriteRenderer>().color = new Color(180,180,180,0.5f);
            // 2. if pressed left click make the hovering object <SelectedObject> and take a list of all arounds fruit for the hovering
            if (Input.GetMouseButton(0) ) {
                if(!playerIsSelected)
                {
                    SelectedObject = HoveringObject;
                    playerIsSelected = true;
                    FruitsArroundSelectedObject = SelectedObject.GetComponent<item>().aroundFruitsObjects;
                }
                
            }
            else
            {
                
                 SelectedObject = null;
                playerIsSelected = false;
                FruitsArroundSelectedObject = new GameObject[4];

            }
            if( HoveringObject!= SelectedObject && playerIsSelected)
            {
                foreach (GameObject fruit in FruitsArroundSelectedObject)
                {
                    if (fruit == HoveringObject)
                    {
                        toSwab = HoveringObject;
                    }
                }
                SelectedObject = null;
                playerIsSelected = false;
                //swab(SelectedObject, toSwab);
               // Debug.Log(SelectedObject.name + "  " + toSwab.name);
            }
        }
        if (Input.GetMouseButtonUp(0)) { 
            SelectedObject = null;
            toSwab = null;
        }


    }
    IEnumerator swab()
    {
        GameObject firstObject = SelectedObject;
        GameObject SecondObject = toSwab;
        firstObject.GetComponent<BoxCollider2D>().enabled = false;
        SecondObject.GetComponent<BoxCollider2D>().enabled = false;

        Vector3 temp = firstObject.transform.position;
        firstObject.transform.position = SecondObject.transform.position;
        SecondObject.transform.position = temp;
        
        firstObject.GetComponent<BoxCollider2D>().enabled = true;
        SecondObject.GetComponent<BoxCollider2D>().enabled = true;
        manger.GetComponent<ScoreManger>().swabs -= 1;
        firstObject.GetComponent<item>().checkForMatches();
        SecondObject.GetComponent<item>().checkForMatches();
        yield return null;
    }
}
