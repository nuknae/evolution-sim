using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Predator : MonoBehaviour
{
    public DNA dna;
    public int energy = 4;
    

    private void Awake()
    {
        if (dna == null)
            dna = new DNA(1);
    }

    private void Start()
    {
        StartCoroutine(ConsumeEnergy());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.collider.tag == "PREY" && energy <= 16)
        {
            
            Destroy(collision.collider.gameObject);
            energy++;
        }
    }

    IEnumerator ConsumeEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f, 15f));

            energy--;
            if (energy == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    
}