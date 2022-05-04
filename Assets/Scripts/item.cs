using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class item : MonoBehaviour
{
    // Fruit Item class 
    // attach to Fruit prefab

    public enum fruitTypes { Null = 0, Apple, Banana, Orange, Strawberry, Lemon, Mango, Pear, Blueberry, Grape, Cherry, GreenApple };

    [Header("Fruit proberaties")]
    public bool isMatched = false;
    public bool isMoving;

    [Header("Effects")]
    public GameObject CollectingEffect;
    public GameObject MovingFruit;

    [Header("For helping")]
    public GameObject[] aroundFruitsObjects = new GameObject[4];
    // Controls
    public Dictionary<string, GameObject> aroundFruits = new Dictionary<string, GameObject>();
    GameObject manger;
    List<Collider2D> aroundColliders;
    SpriteRenderer spriteRenderer;
    fruitTypes fruitType;


    public fruitTypes FruitType { get => fruitType; set {fruitType = value; setSprite();}}

    private void Awake()
    {
        manger = GameObject.Find("GameManger");
        aroundColliders = CreateCollidersCheckArround();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        aroundFruits = getArroundFruits();
        StartCoroutine(moveCheck());
    }
    
    private void Update()
    {
        
        if(this.gameObject != FruitSwab.HoveringObject)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        }
        if (isMatched) makeTheMatch();
        aroundFruitsObjects[0] = aroundFruits["Up"];
        aroundFruitsObjects[1] = aroundFruits["Down"];
        aroundFruitsObjects[2] = aroundFruits["Right"];
        aroundFruitsObjects[3] = aroundFruits["Left"];
    }
    private void FixedUpdate()
    {
        if (!isMoving)
        {
            checkForMatches();
        }
    }
    // finding and set this fruit sprite from the FruitsID data table
    void setSprite()
    {
        // 1. Read the table which will be found in game manger
        FruitsID fruitsID = manger.GetComponent<FruitsID>();

        // 2. Search in the table for the sprite with comparying this fruit type vs the names i the table if not found will be apple
        foreach (var fruit in fruitsID.fruits)
        {
            if (fruit.name == fruitType.ToString())
            {
                spriteRenderer.sprite = fruit.fruitSprite;
                break;
            }
        }
    }

    // Creating 4 box colliders arround the fruit to check what arround
    List<Collider2D> CreateCollidersCheckArround()
    {
        // CollidersData : is a dictionary the keys are the direction of the coolider
        //                 the valus is a list of 2 elemnts { collider size , collider offset} 
        
        Dictionary<string, List<Vector2>> CollidersData = new Dictionary<string, List<Vector2>>();
        CollidersData["Up"] = new List<Vector2> { new Vector2(0.2f, 0.8f), new Vector2(0, 0.6f) };
        CollidersData["Down"] = new List<Vector2> { new Vector2(0.2f, 0.8f), new Vector2(0, -0.6f) };
        CollidersData["Right"] = new List<Vector2> { new Vector2(0.8f, 0.2f), new Vector2(0.6f, 0) };
        CollidersData["Left"] = new List<Vector2> { new Vector2(0.8f, 0.2f), new Vector2(-0.6f, 0) };
        
        List<Collider2D> aroundColidersList = new List<Collider2D>();
        GameObject temp = new GameObject(); //Temprory empty object
        foreach (var collider in CollidersData)
        {
            //  foreach item in CollidersData will create a game object holds the collider
            // 1. creating empty game Object and make a refrance to it
            GameObject checkGameObject = Instantiate(temp, this.transform);
            checkGameObject.name = collider.Key;

            // 2. adding the box collider and make refrance to it
            BoxCollider2D checkCollider = checkGameObject.AddComponent<BoxCollider2D>();
            checkCollider.isTrigger = true;
            checkCollider.size = collider.Value[0];
            checkCollider.offset = collider.Value[1];

            // 3. adding CollidingData class to get the information about what this collider collided with
            checkGameObject.AddComponent<CollidingData>();

            // 4. finally adding this object to the list to the aroundColidersList
            aroundColidersList.Add(checkGameObject.GetComponent<BoxCollider2D>());
        }
        Destroy(temp); // delet the temprory object after creating all the colliders
        return aroundColidersList;

    }
    
    // Getting the data of the objects arround this object
    Dictionary<string, GameObject> getArroundFruits()
    {
        // 1. creating temprory dictionary to save the data
        Dictionary<string, GameObject> temp = new Dictionary<string, GameObject>() { { "Up" ,null}, { "Down", null }, { "Right", null }, { "Left", null } };
        
        // 2. check the all 4 colliders 
        foreach (var direction in aroundColliders)
        {
            // 2.1 get the collided object if found
            GameObject collidedFruit = direction.GetComponent<CollidingData>().CollidedObject;
            if (collidedFruit != null)
                temp[direction.name] = collidedFruit;
        }
        return temp;
    }

    //checking if there is a 3match or more
    // Methdology : cheking if the this and up and down are the same fruit type or not
    bool checkForMatchesDirections(string direction1, string direction2)
    {
        // 1.check if there is 2 objects in the 2 directions
        GameObject firsObject = aroundFruits[direction1];
        GameObject secondObject = aroundFruits[direction2];
        if (firsObject == null || secondObject == null) return false;

        // 2.check if there is 2 objects are fruits
        item direction1Fruit = firsObject.GetComponent<item>();
        item direction2Fruit = secondObject.GetComponent<item>();
        if (direction1Fruit == null || direction2Fruit == null) return false;
        // 3.if the 2 items are fruits check their type
        else
        {
            if ((direction1Fruit.fruitType == fruitType) && (direction2Fruit.fruitType == fruitType))
            {
                // 4.if they are from the same type then set " isMatched" to true for the all 3
                this.isMatched = true;
                direction1Fruit.isMatched = true;
                direction2Fruit.isMatched = true;
                return true;
            }
            else return false;
        }
    }

    // check for the match up&down + right&left
    public bool checkForMatches()
    {
        aroundFruits = getArroundFruits();
        if (checkForMatchesDirections("Up", "Down") || checkForMatchesDirections("Right", "Left")) return true;
        else return false;
    }

    //Apply the matching effect
    public void makeTheMatch()
    {

        Instantiate(CollectingEffect,transform.position, Quaternion.identity);
        GameObject movingFruit =  Instantiate(MovingFruit, transform.position, Quaternion.identity);
        movingFruit.GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite;
        manger.GetComponent<GameManger>().playCollectingSound();
        Destroy(this.gameObject);
    }

    IEnumerator moveCheck()
    {
        while (true)
        {
            var p1 = transform.position;
            yield return new WaitForSeconds(0.1f);
            var p2 = transform.position;
            isMoving = (p1 != p2);
        }
        
    }
}
