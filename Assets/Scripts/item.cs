using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class item : MonoBehaviour
{

    public enum fruitTypes { Null = 0, Apple, Banana, Orange, Strawberry, Lemon, Mango, Pear, Blueberry, Grape, Cherry, GreenApple };
    public fruitTypes fruitType;
    public List<Collider2D> aroundColliders;
    public Dictionary<string, GameObject> aroundFruits = new Dictionary<string, GameObject>();
    public GameObject[] Fruitsarround = new GameObject[4];
    public string[] names = new string[5];
    SpriteRenderer spriteRenderer;
    public bool isMatched = false;
    public bool touchingTheGround = false;
    public GameObject CollectingEffect;
    public GameObject MovingFruit;
    private void Awake()
    {
        aroundColliders = CreateCollidersCheckArround();
        spriteRenderer = this.GetComponent<SpriteRenderer>();

    }
    
    private void Update()
    {
        aroundFruits = getArroundFruits();
        if(this.gameObject != FruitSwab.HoveringObject)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        }

        Fruitsarround[0] = aroundFruits["Up"];
        Fruitsarround[1] = aroundFruits["Down"];
        Fruitsarround[2] = aroundFruits["Right"];
        Fruitsarround[3] = aroundFruits["Left"];
       
    }

    List<Collider2D> CreateCollidersCheckArround()
    {
        Dictionary<string, List<Vector2>> data = new Dictionary<string, List<Vector2>>();
        data["Up"] = new List<Vector2> { new Vector2(0.2f, 0.8f), new Vector2(0, 0.6f) };
        data["Down"] = new List<Vector2> { new Vector2(0.2f, 0.8f), new Vector2(0, -0.6f) };
        data["Right"] = new List<Vector2> { new Vector2(0.8f, 0.2f), new Vector2(0.6f, 0) };
        data["Left"] = new List<Vector2> { new Vector2(0.8f, 0.2f), new Vector2(-0.6f, 0) };
        List<Collider2D> arrounds = new List<Collider2D>();
        GameObject temp = new GameObject();

        foreach (var direction in data)
        {
            GameObject checkGameObject = Instantiate(temp, this.transform);
            BoxCollider2D checkCollider = checkGameObject.AddComponent<BoxCollider2D>();
            checkGameObject.AddComponent<Arounds>();
            checkCollider.isTrigger = true;
            checkGameObject.name = direction.Key;
            checkCollider.size = direction.Value[0];
            checkCollider.offset = direction.Value[1];

            arrounds.Add(checkGameObject.GetComponent<BoxCollider2D>());
        }
        Destroy(temp);
        return arrounds;

    }
    Dictionary<string, GameObject> getArroundFruits()
    {
        Dictionary<string, GameObject> temp = new Dictionary<string, GameObject>();
        foreach (var direction in aroundColliders)
        {
            GameObject collidedFruit = direction.GetComponent<Arounds>().CollidedObject;
            if (collidedFruit != null)
                temp[direction.name] = collidedFruit;
            else temp[direction.name] = null;
        }
        return temp;
    }


    public void setSprite()
    {
        spriteRenderer.sprite = getMySprite(fruitType);
    }
    Sprite getMySprite(fruitTypes type)
    {
        GameObject manger = GameObject.Find("GameManger");
        FruitsID fruitsID = manger.GetComponent<FruitsID>();
        foreach (var fruit in fruitsID.fruits)
        {
            if (fruit.name == type.ToString()) return fruit.fruitSprite;
        }
        return null;
    }

    bool checkForMatchesDirections(string direction1, string direction2)
    {
       // Debug.Log("Iam" + this.name + "cheking matchingNOW IN " + direction1+ direction2);
        item direction1Fruit = null;
        item direction2Fruit = null;
        if (aroundFruits[direction1] != null)
            direction1Fruit = aroundFruits[direction1].GetComponent<item>();
        if (aroundFruits[direction2] != null)
            direction2Fruit = aroundFruits[direction2].GetComponent<item>();
        
        if (direction1Fruit != null && direction2Fruit != null)
        {
        //    Debug.Log(direction1Fruit.gameObject.name + "   " + direction2Fruit.gameObject.name);
            if ((direction1Fruit.fruitType == fruitType) && (direction2Fruit.fruitType == fruitType))
            {
              //  Debug.Log("In " + direction1 + direction2 + " the type is" + direction1Fruit.fruitType.ToString());
                this.isMatched = true;
                direction1Fruit.isMatched = true;
                direction2Fruit.isMatched = true;
                return true;
            }
        }

        return false;
    }

    public bool checkForMatches()
    {
        
        if (checkForMatchesDirections("Up", "Down") || checkForMatchesDirections("Right", "Left")) return true;
        else return false;
    }

    public void makeTheMatch()
    {

        Instantiate(CollectingEffect,transform.position, Quaternion.identity);
        GameObject movingFruit =  Instantiate(MovingFruit, transform.position, Quaternion.identity);
        movingFruit.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
        Destroy(this.gameObject);
    }

    
}
