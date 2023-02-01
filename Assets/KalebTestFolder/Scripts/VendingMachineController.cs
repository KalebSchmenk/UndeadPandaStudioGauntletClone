using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineController : MonoBehaviour
{
    [SerializeField] int healthOutput = 1;
    [SerializeField] int healsLeft = 1;

    public Light vmLight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            Debug.Log("Confirmed player. Heals left: " + healsLeft);
            if (healsLeft > 0) 
            {
                Debug.Log("About to grab player controller");
                other.gameObject.GetComponent<PlayerController>().HealPlayer(healthOutput);

                healsLeft--;

                if (healsLeft <= 0)
                {
                    vmLight.enabled = false;
                }
            }
        }
    }
}
