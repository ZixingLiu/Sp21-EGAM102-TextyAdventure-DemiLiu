using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruityTree : MonoBehaviour
{

    public int wood, stars;
    public List<GameObject> Fruit;
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Fruit.Count; i++)
        {
            Fruit[i].transform.localPosition = new Vector3(Random.Range(-1.3f, 1.7f), Random.Range(3f, 5f), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
