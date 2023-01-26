using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float speed = 5.0f;

    private bool shootingGun = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!shootingGun)
        {
            MovePlayer();
            RotatePlayer();
        }
    }


    private void MovePlayer()
    {
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.World);
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0, Space.World);
    }

    private void RotatePlayer()
    {
        Vector3 lookTowards = new Vector3(0.0f, 0.0f, 0.0f);

        lookTowards.z = Input.GetAxis("Vertical");
        lookTowards.x = Input.GetAxis("Horizontal");

        if (lookTowards.x == 0.0f && lookTowards.z == 0.0f) return;

        Quaternion rotation = Quaternion.LookRotation(lookTowards);
        transform.rotation = rotation;
    }

    public void QuickTurnTo(Vector3 turnTo)
    {
        transform.LookAt(turnTo);
        //transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
    }

    public void Shooting()
    {
        shootingGun = true;
        StartCoroutine(ShootingMovementPause());
    }

    IEnumerator ShootingMovementPause()
    {
        yield return new WaitForSeconds(0.5f);

        shootingGun = false;
    }
}
