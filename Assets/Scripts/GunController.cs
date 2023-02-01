using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{

    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelLocation;
    [SerializeField] GameObject player;
    [SerializeField] AudioClip gunShot;

    private bool firedGun = false;
    private PlayerController playerScript;
    private ObjectSoundController soundController;

    private void Start()
    {
        playerScript = player.GetComponent<PlayerController>();

        soundController = GetComponent<ObjectSoundController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Probably a better way to check what the playerDead bool is
        if (Input.GetAxis("Fire1") > 0 && !firedGun && !playerScript.playerDead)
        {
            firedGun = true;

            StartCoroutine(IEGunCooldown());

            NormalShoot();
        }

    }

    // Gun shoot mechanic
    private void NormalShoot()
    {
        playerScript.isShooting = true;
        bool isUsingController = playerScript.isUsingController;

        if (!isUsingController) 
        {
            RaycastHit hit;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                var direction = hit.point - transform.position;
                direction.y = 0f;
                direction.Normalize();
                player.transform.forward = direction;
            }
        }

        var bulletObject = Instantiate(bullet, barrelLocation.position, Quaternion.identity);
        bulletObject.transform.rotation = player.transform.rotation;

        soundController.PlayAudio(gunShot);
    }

    // Shoot cooldown to prevent spam
    IEnumerator IEGunCooldown()
    {
        yield return new WaitForSeconds(0.5f);

        firedGun = false;
        playerScript.isShooting = false;
    }
}
