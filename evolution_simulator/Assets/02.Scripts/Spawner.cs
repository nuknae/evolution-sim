using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float Range = 50f;

    public int Preys = 40;
    public int Predators = 10;

    public GameObject prey;
    public GameObject predator;

    GameObject temp;
    DNA dna;

    private void Awake()
    {
        int i;

        for (i = 0; i < Preys; i++)
        {
            temp = GameObject.Instantiate(prey, (Vector2)transform.position + (Random.insideUnitCircle * Range), Quaternion.identity);
            dna = temp.GetComponent<Core>().dna;
            dna.CopyDNA(dna, dna);
            temp.GetComponent<Core>().SetProperties();
            temp.GetComponent<Movement>().SetProperties();
        }

        for (i = 0; i < Predators; i++)
        {
            temp = GameObject.Instantiate(predator, (Vector2)transform.position + (Random.insideUnitCircle * Range), Quaternion.identity);
            dna = temp.GetComponent<Core>().dna;
            dna.CopyDNA(dna, dna);
            temp.GetComponent<Core>().SetProperties();
            temp.GetComponent<Movement_Predator>().SetProperties();
        }
    }


}
