using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class GameManager : MonoBehaviour
{
    public bool Check = false;
    public float TimeScale = 1f;
    public bool AddTime = false;
    public float AddTimeAmount = 10;
    public bool Write = false;
    public bool Count_Prey = false;
    public bool Count_Predator = false;


    [Header("Prey Count")]
    public int Prey = 0;
    [Header("Predator Count")]
    public int Predator = 0;

    void Start()
    {
        StartCoroutine(LogCount());

    }


    void Update()
    {
        if (Check)
        {
            Time.timeScale = TimeScale;
            Check = false;
        }

        if (AddTime)
        {
            TimeScale += AddTimeAmount;
            AddTime = false;
        }

        if (Write)
        {
            WriteString();
            Write = false;
        }

        if (Count_Prey)
        {
            CountPrey();
            Count_Prey = false;
        }

        if (Count_Predator)
        {
            CountPredator();
            Count_Predator = false;
        }
    }

    public void WriteString()
    {
        int i;
        string path = Application.dataPath + "/Log.txt";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Prey\tPredator\n\n\n");
        }
        else
        {
            File.AppendAllText(path, Prey.ToString() + "\t" + Predator.ToString() + "\n");

            
        }
    }

    public void CountPrey()
    {
        Prey = GameObject.FindGameObjectsWithTag("PREY").GetLength(0);


    }

    public void CountPredator()
    {
        Predator = GameObject.FindGameObjectsWithTag("PREDATOR").GetLength(0);
    }

    IEnumerator LogCount()
    {
        while (true)
        {
            CountPrey();
            CountPredator();
            WriteString();

            yield return new WaitForSeconds(0.2f);
        }
    }
}
