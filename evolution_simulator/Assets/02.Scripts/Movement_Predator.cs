using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Movement_Predator : MonoBehaviour
{
    public bool check = false;

    Vector3 initialRotation;
    Vector3 targetRotation;

    float initialMoveSpeed;
    float targetMoveSpeed;



    float rotSpeed;
    float currentRotSpeed;
    float rotProcess = 0f;
    float rotFrequency;


    float movementSpeed;
    float currentMoveSpeed;
    float accelProcess = 0f;
    float accelduration;

    float moveChangeFrequency;

    float size;

    DNA dna;
    Core core;
    Division division;

    void Awake()
    {
        //GameManager.predatorNum++;
        core = this.GetComponent<Core>();
        division = GetComponent<Division>();

        if (core.dna == null)
            core.dna = new DNA(1);

        dna = this.GetComponent<Core>().dna;
        

        movementSpeed = dna.MovementSpeed;
        rotSpeed = dna.RotationSpeed;
        moveChangeFrequency = dna.MoveChangeFrequency;
        rotFrequency = dna.RotationFrequency;
        size = Mathf.Sqrt(transform.localScale.x * transform.localScale.y);

        transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f));
        currentMoveSpeed = Random.Range(movementSpeed * 0.85f, movementSpeed * 1.15f);

        StartCoroutine(ChangeRotation());
        StartCoroutine(ChangeSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        
        if (check)
        {
            check = false;
            Debug.Log("MoveSpeed : " + movementSpeed + " RotationSpeed : " + rotSpeed + " RotationFrequency : " + rotFrequency);
        }

        if (rotProcess < 1)
        {
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(initialRotation), Quaternion.Euler(targetRotation), Mathf.Clamp01(rotProcess += Time.deltaTime * currentRotSpeed / 10));
        }

        if (accelProcess < 1)
        {
            currentMoveSpeed = Mathf.Lerp(initialMoveSpeed, targetMoveSpeed, Mathf.Clamp01(accelProcess += Time.deltaTime / accelduration));
        }

        transform.position += transform.up * currentMoveSpeed / 3 * Time.deltaTime;


    }

    IEnumerator ChangeRotation()
    {
        float targetAngle;

        float minDistance;

        float meanDistance;
        Vector3 meanVec = new Vector3();

        Collider2D[] colls = new Collider2D[60];
        Collider2D collider = GetComponent<Collider2D>();


        while (true)
        {
            initialRotation = transform.rotation.eulerAngles;
            colls = Physics2D.OverlapCircleAll(transform.position, 60, 1 << 8);
            

            minDistance = float.MaxValue;
            meanVec.Set(0, 0, 0);

            foreach (Collider2D coll in colls)
            {
                meanVec += coll.transform.position;
                if ((coll.transform.position - transform.position).sqrMagnitude < minDistance)
                {
                    collider = coll;
                    minDistance = (coll.transform.position - transform.position).sqrMagnitude;
                }
            }


            meanVec = meanVec / colls.Length;
            
            
            if(Random.Range(0, 15) == 1)
            {
                minDistance = float.MaxValue;
            }
            
            if (minDistance != float.MaxValue)
            {
                meanDistance = meanVec.sqrMagnitude / colls.Length;

                Vector3 targetDir;

                if (Physics2D.OverlapCircleAll(transform.position, 30, 1 << 8).Length <= 5 && minDistance > meanDistance)
                {
                
                    targetDir = meanVec - transform.position;
                }
                else
                {
                    targetDir = collider.transform.position - transform.position;
                }

                 
                targetAngle = Vector3.SignedAngle(Vector3.up, targetDir, transform.forward);

                Array.Clear(colls, 0, colls.Length);
            }
            else
            {
                targetAngle = Random.Range(-1f, 1f) * 180f;
            }



            targetRotation = new Vector3(0, 0, targetAngle);

            currentRotSpeed = Random.Range(rotSpeed * 0.85f, rotSpeed * 1.15f);
            rotProcess = 0;

            yield return new WaitForSeconds(Random.Range(rotFrequency * 0.85f, rotFrequency * 1.15f));

        }
    }

    IEnumerator ChangeSpeed()
    {
        while (true)
        {
            initialMoveSpeed = currentMoveSpeed;
            targetMoveSpeed = Random.Range(movementSpeed * 0.85f, movementSpeed * 1.15f);

            accelProcess = 0;
            accelduration = Random.Range(moveChangeFrequency * 0.85f, moveChangeFrequency * 1.15f);

            yield return new WaitForSeconds(accelduration);

        }
    }

    public void ResetTarget()
    {
        StopCoroutine(ChangeRotation());
        StartCoroutine(ChangeRotation());
    }

    public void SetProperties()
    {
        movementSpeed = dna.MovementSpeed;
        rotSpeed = dna.RotationSpeed;
        moveChangeFrequency = dna.MoveChangeFrequency;
        rotFrequency = dna.RotationFrequency;

        
        

        StopCoroutine(ChangeSpeed());
        StopCoroutine(ChangeRotation());

        size = Mathf.Sqrt(transform.localScale.x * transform.localScale.y);
        currentMoveSpeed = Random.Range(movementSpeed * 0.85f, movementSpeed * 1.15f);

        StartCoroutine(ChangeRotation());
        StartCoroutine(ChangeSpeed());
        
        accelProcess = 0;
    }

}
