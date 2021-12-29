using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Movement : MonoBehaviour
{
    public bool check = false;

    Vector3 initialRotation;
    Vector3 targetRotation;

    Vector3 pointA;
    Vector3 pointB;

    float detectRange = 8f;

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

    void Awake()
    {
        core = this.GetComponent<Core>();
        if (core.dna == null)
            core.dna = new DNA(0);

        dna = this.GetComponent<Core>().dna;
        
        movementSpeed = dna.MovementSpeed;
        rotSpeed = dna.RotationSpeed;
        moveChangeFrequency = dna.MoveChangeFrequency;
        rotFrequency = dna.RotationFrequency;
        size = Mathf.Sqrt(transform.localScale.x * transform.localScale.y);

        pointA = transform.position + transform.up * (transform.localScale.y / 2 + detectRange) + transform.right * (transform.localScale.x / 2 + detectRange);
        pointB = transform.position - transform.up * (transform.localScale.y / 2 + detectRange) - transform.right * (transform.localScale.x / 2 + detectRange);

        transform.rotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, 360f));
        currentMoveSpeed = UnityEngine.Random.Range(movementSpeed * 0.85f, movementSpeed * 1.15f);

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
            currentMoveSpeed = Mathf.Lerp(initialMoveSpeed, targetMoveSpeed, Mathf.Clamp01(accelProcess += Time.deltaTime / accelduration));// * 9);
        }

        transform.position += transform.up * currentMoveSpeed / 3 * Time.deltaTime;

        
    }

    IEnumerator ChangeRotation()
    {
        float targetAngle;

        float minDistance;
        Collider2D[] colls = new Collider2D[30];
        Collider2D collider = GetComponent<Collider2D>();
        Vector3 targetDir;

        while (true)
        {
            initialRotation = transform.rotation.eulerAngles;

            minDistance = float.MaxValue;

            pointA = transform.position + transform.up * (transform.localScale.y / 2 + detectRange) + transform.right * (transform.localScale.x / 2 + detectRange);
            pointB = transform.position - transform.up * (transform.localScale.y / 2 + detectRange) - transform.right * (transform.localScale.x / 2 + detectRange);

            colls = Physics2D.OverlapAreaAll(pointA, pointB, 1 << 9);
            
                foreach (Collider2D coll in colls)
                {
                    if ((coll.transform.position - transform.position).sqrMagnitude < minDistance)
                    {
                        collider = coll;
                        minDistance = (coll.transform.position - transform.position).sqrMagnitude;
                    }
                }
            
            if(Random.Range(0, 10) == 1)
            {
                minDistance = float.MaxValue;
            }

            
            if (minDistance != float.MaxValue)
            {

                targetDir = transform.position - collider.transform.position;
                targetAngle = Vector3.SignedAngle(Vector3.up, targetDir, transform.forward) + Random.Range(-45f, 45f);

                Array.Clear(colls, 0, colls.Length);
            }
            else
            {
                colls = Physics2D.OverlapAreaAll(pointA, pointB, 1 << 10);
                

                foreach (Collider2D coll in colls)
                {
                    if ((coll.transform.position - transform.position).sqrMagnitude < minDistance)
                    {
                        collider = coll;
                        minDistance = (coll.transform.position - transform.position).sqrMagnitude;
                    }
                }
                

                if (minDistance != float.MaxValue)
                {

                    targetDir = collider.transform.position - transform.position;
                    targetAngle = Vector3.SignedAngle(Vector3.up, targetDir, transform.forward);

                    Array.Clear(colls, 0, colls.Length);
                }
                else
                {
                    targetAngle = Random.Range(-1f, 1f) * 180f;
                }
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

        currentMoveSpeed = Random.Range(movementSpeed * 0.85f, movementSpeed * 1.15f);// * 9;
        size = Mathf.Sqrt(transform.localScale.x * transform.localScale.y);

        pointA = transform.position + transform.up * (transform.localScale.y / 2 + detectRange) + transform.right * (transform.localScale.x / 2 + detectRange);
        pointB = transform.position - transform.up * (transform.localScale.y / 2 + detectRange) - transform.right * (transform.localScale.x / 2 + detectRange);

        StopCoroutine(ChangeSpeed());
        StopCoroutine(ChangeRotation());

        

        StartCoroutine(ChangeRotation());
        StartCoroutine(ChangeSpeed());
        accelProcess = 0.95f;
    }

}
