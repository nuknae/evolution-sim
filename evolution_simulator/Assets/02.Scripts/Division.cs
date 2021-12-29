using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Division : MonoBehaviour
{
    DNA dna;
    DNA childDna;
    Core core;

    public int RequiredEnergy = 4;
    public float MinDelay = 10f;
    public float MaxDelay = 20f;
    public bool isMature = false;

    // Start is called before the first frame update
    void Start()
    {
        dna = this.GetComponent<Core>().dna;
        core = GetComponent<Core>();
        StartCoroutine(DivisionDelay());
    }

    private void Update()
    {
        if (core.energy >= RequiredEnergy && isMature)
        {
            if (this.tag == "PREDATOR")
                Divide();
            else if (this.tag == "PREY")
                Divide();
        }
    }

    IEnumerator DivisionDelay()
    {
       yield return new WaitForSeconds(Random.Range(MinDelay, MaxDelay));
       isMature = true;
    }

    void Divide()
    {
        core.energy -= (RequiredEnergy / 2);
        isMature = false;
        StartCoroutine(DivisionDelay());

        GameObject child = GameObject.Instantiate(this.gameObject);
     

        childDna = child.GetComponent<Core>().dna;

        if (this.tag == "PREY")
        {
            dna.CopyDNA(dna, childDna);
        }
        else if(this.tag == "PREDATOR")
        {
            dna.CopyDNA_Predator(dna, childDna);
        }

        core.SetProperties();
        child.GetComponent<Core>().SetProperties();

        if (this.tag == "PREY")
        {
            GetComponent<Movement>().SetProperties();
            child.GetComponent<Movement>().SetProperties();
        }
        else if (this.tag == "PREDATOR")
        {
            GetComponent<Movement_Predator>().SetProperties();
            child.GetComponent<Movement_Predator>().SetProperties();
        }

        //core.Revive();
        

        

        
    }
}
