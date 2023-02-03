using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] AudioClip keyPickup;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().keyCount++;
            
            collision.gameObject.GetComponent<ObjectSoundController>().PlayAudio(keyPickup);

            Destroy(this.gameObject);
        }
    }
}
