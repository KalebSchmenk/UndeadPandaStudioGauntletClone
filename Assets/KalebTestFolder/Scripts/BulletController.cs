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

    // FIXME
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
