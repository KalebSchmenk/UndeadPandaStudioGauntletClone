using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 50.0f;
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * bulletSpeed);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            Debug.Log("Bullet hit an enemy. Implement functionality");
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
