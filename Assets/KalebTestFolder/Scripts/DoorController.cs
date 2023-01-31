using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    PlayerController playerscript;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerscript = collision.gameObject.GetComponent<PlayerController>();

            if (playerscript.keyCount > 0)
            {
                playerscript.keyCount--;

                Destroy(this.gameObject);
            }
        }
    }
}
