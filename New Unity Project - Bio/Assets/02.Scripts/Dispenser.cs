using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    public float Frequency = 1f;
    public float Amount = 3f;
    public float Range = 10f;
    public GameObject food;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DispenseFood());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DispenseFood()
    {
        while (true)
        {
            for (int i = 0; i < Amount; i++)
            {
                GameObject.Instantiate(food, (Vector2)transform.position + Random.insideUnitCircle * Range, Quaternion.identity);
            }
            yield return new WaitForSeconds(Frequency);
        }
    }
}
