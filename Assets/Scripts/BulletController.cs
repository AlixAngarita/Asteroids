using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    void Start()
    {
        // Lasts for 1 s
        Destroy (gameObject, 1.0f);
 
        // Force in the same direction of the ship
        GetComponent<Rigidbody2D>().AddForce(transform.up * 400);
    }
}
