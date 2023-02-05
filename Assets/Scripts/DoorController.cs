using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    PlayerController playerscript;

    [SerializeField] AudioClip doorOpenSound;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerscript = collision.gameObject.GetComponent<PlayerController>();

            if (playerscript.keyCount > 0)
            {
                playerscript.keyCount--;

                collision.gameObject.GetComponent<ObjectSoundController>().PlayAudio(doorOpenSound);

                Destroy(this.gameObject);
            }
        }
    }
}
