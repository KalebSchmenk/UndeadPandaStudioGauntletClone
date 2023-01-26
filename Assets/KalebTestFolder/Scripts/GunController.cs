using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelLocation;
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject Player;

    PlayerController PlayerScript;
    private bool firedGun = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && !firedGun)
        {
            firedGun = true;

            StartCoroutine(GunCooldown());

            PlayerScript.Shooting();

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RayCastShoot();
            }
            else
            {
                NormalShoot();
            }
        }

    }


    private void NormalShoot()
    {
        var bulletObject = Instantiate(bullet, barrelLocation.position, Quaternion.identity);
        bulletObject.transform.rotation = Player.transform.rotation;
    }

    private void RayCastShoot()
    {
        RaycastHit hit;

        var ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            PlayerScript.QuickTurnTo(hit.point);
        }

        var bulletObject = Instantiate(bullet, barrelLocation.position, Quaternion.identity);
        bulletObject.transform.rotation = Player.transform.rotation;

    }

    IEnumerator GunCooldown()
    {
        yield return new WaitForSeconds(2);

        firedGun = false;
    }
}
