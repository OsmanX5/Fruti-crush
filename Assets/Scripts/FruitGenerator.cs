using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGenerator : MonoBehaviour
{
    public GameObject FruitPrefab;
    public Transform startPoint;
    public Vector2 grid = new Vector2(9,9);
    private void Start()
    {
        StartCoroutine(FruitsCreation());
    }

    IEnumerator FruitsCreation()
    {
        for (int i = 0; i < grid.x; i++)
        {
            for (int j = 0; j < grid.y; j++)
            {
                GameObject fruit = Instantiate(FruitPrefab, startPoint.position + new Vector3(j, i), Quaternion.identity);
                fruit.GetComponent<item>().FruitType = (item.fruitTypes)Random.Range(1, System.Enum.GetNames(typeof(item.fruitTypes)).Length-1);
                fruit.name = fruit.GetComponent<item>().FruitType.ToString();
                fruit.transform.parent = this.transform;
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.02f);
        }
        GameManger manger = this.GetComponent<GameManger>();

    }

    
}
