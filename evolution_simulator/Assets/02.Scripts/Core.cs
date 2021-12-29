using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public DNA dna;
    public int energy = 2;
    public int energyLimit = 6;
    public float minConsume = 5f;
    public float maxConsume = 7f;
    public float LifeSpan = 60f;

    Collider2D coll;

    bool isFull = false;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (dna == null)
        {
            if (this.tag == "PREY")
            {
                dna = new DNA(0);
            }
            else if (this.tag == "PREDATOR")
            {
                dna = new DNA(1);
            }
        }

        coll = GetComponent<Collider2D>();
        SetProperties();
    }

    private void Start()
    {
        StartCoroutine(ConsumeEnergy());
        //StartCoroutine(LifeCount());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "FOOD" && energy <= energyLimit)
        {
            Destroy(collider.gameObject);
            energy++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "PREY" && energy <= energyLimit)
        {
            Destroy(collision.collider.gameObject);
            energy++;

        }
    }

    public void SetProperties()
    {
        Vector3 size = new Vector3(dna.Width, dna.Height, 0);

        transform.localScale = size;


        minConsume = Mathf.Clamp(15 - Mathf.Sqrt(transform.localScale.x * transform.localScale.y) * 2, 1f, float.MaxValue);
        if (this.tag == "PREY")
            minConsume = Mathf.Clamp(minConsume, 3f, float.MaxValue);
        else if (this.tag == "PREDATOR")
            minConsume = Mathf.Clamp(minConsume - 4, 1f, float.MaxValue);

        maxConsume = minConsume + 2;

    }

    IEnumerator ConsumeEnergy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minConsume, maxConsume));



            energy--;

            if (energy == 0)
            {
                Destroy(this.gameObject);

            }
        }
    }

    IEnumerator LifeCount()
    {
        yield return new WaitForSeconds(Random.Range(LifeSpan * 0.85f, LifeSpan * 1.15f));
        Destroy(this.gameObject);
    }

    public void Revive()
    {

        StopCoroutine(LifeCount());
        StartCoroutine(LifeCount());
    }
}
