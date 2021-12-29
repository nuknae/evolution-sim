using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float Duration = 15f;
    void Start()
    {
        Destroy(this.gameObject, Duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PREY")
        {
            Destroy(this.gameObject);
        }
    }


}
