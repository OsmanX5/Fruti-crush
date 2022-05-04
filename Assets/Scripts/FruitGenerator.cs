using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGenerator : MonoBehaviour
{
    // Script for automatic generation for fruit map can control if map size with Vector2 variable(grid)
    public GameObject FruitPrefab;
    public GameObject GridPrefab;
    public Transform playArea;
    public Transform startPoint;
    public Vector2 grid = new Vector2(9,9);
    private void Start()
    {
        StartCoroutine(FruitsCreation());
    }

    IEnumerator FruitsCreation()
    {
        // Loop moving from 0 => y row
        for (int i = 0; i < grid.y; i++)
        {
            // Loop moving from 0 => x column
            for (int j = 0; j < grid.x; j++)
            {
                // 1. Creating the tile background as a chield of play area
                Instantiate(GridPrefab, transform.position + new Vector3(j, i), Quaternion.identity, playArea);

                // 2. Creating the fruit Object 
                GameObject fruit = Instantiate(FruitPrefab,transform.position+new Vector3(j, i), Quaternion.identity, this.transform);
                
                // 3. Setup the fruit Object proparaties "Fruit type randomly" + fruit name + 
                fruit.GetComponent<item>().FruitType = (item.fruitTypes)Random.Range(1, System.Enum.GetNames(typeof(item.fruitTypes)).Length-1);
                fruit.name = fruit.GetComponent<item>().FruitType.ToString();

                // 4. for the first line set touchingGround = true;
                if (i == 0)  fruit.GetComponent<item>().touchingTheGround = true;  

                // 5. time puse effect
                yield return new WaitForSeconds(0.005f);
            }
            yield return new WaitForSeconds(0.005f);
        }
    }

    
}
