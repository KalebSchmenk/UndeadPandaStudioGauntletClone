using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelLocation;
    [SerializeField] GameObject player;

    private bool firedGun = false;
    private PlayerController playerScript;

    private void Start()
    {
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && !firedGun)
        {
            firedGun = true;

            StartCoroutine(IEGunCooldown());

            NormalShoot();
        }

    }


    private void NormalShoot()
    {
        playerScript.isShooting = true;

        RaycastHit hit;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var direction = hit.point - transform.position;
            direction.y = 0f;
            direction.Normalize();
            player.transform.forward = direction;
        }

        var bulletObject = Instantiate(bullet, barrelLocation.position, Quaternion.identity);
        bulletObject.transform.rotation = player.transform.rotation;
    }


    IEnumerator IEGunCooldown()
    {
        yield return new WaitForSeconds(0.5f);

        firedGun = false;
        playerScript.isShooting = false;
    }
}
