using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelLocation;
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject Player;

    private bool firedGun = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && !firedGun)
        {
            firedGun = true;

            StartCoroutine(GunCooldown());

            NormalShoot();
        }

    }


    private void NormalShoot()
    {
        var bulletObject = Instantiate(bullet, barrelLocation.position, Quaternion.identity);
        bulletObject.transform.rotation = Player.transform.rotation;
    }


    IEnumerator GunCooldown()
    {
        yield return new WaitForSeconds(0.75f);

        firedGun = false;
    }
}
