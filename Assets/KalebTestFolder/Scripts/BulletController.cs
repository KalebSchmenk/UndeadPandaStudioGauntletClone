using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 50.0f;
    [SerializeField] Rigidbody rb;
    
    
    void Start()
    {
        rb.AddForce(transform.forward * bulletSpeed);
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        Destroy(this.gameObject);
 
    }
}
