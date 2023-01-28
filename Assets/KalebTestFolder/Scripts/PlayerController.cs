using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject glowstick;
    [SerializeField] float speed = 0.05f;
    [SerializeField] int glowstickCount = 15;
    [SerializeField] GameObject glowstickSpawn;

    public bool isUsingController = false;
    private bool isGlowstickCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        MovePlayer();
        RotatePlayer();

        if (Input.GetAxis("Jump") > 0) 
        {
            if(glowstickCount > 0 && !isGlowstickCooldown) { ThrowGlowstick(); StartCoroutine(GlowstickCooldown()); }        
        }
    }


    private void MovePlayer()
    {
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.World);
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0, Space.World);
    }

    private void RotatePlayer()
    {
        if(!isUsingController)
        {
            RaycastHit hit;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                var direction = hit.point - transform.position;
                direction.y = 0f;
                direction.Normalize();
                transform.forward = direction;
            }
        }

        // Controller rotation controls
        if (Input.GetAxisRaw("RightStickHorizontal") != 0 || Input.GetAxisRaw("RightStickVertical") != 0)
        {
            Vector3 lookTowards = new Vector3(Input.GetAxisRaw("RightStickHorizontal"), 0, Input.GetAxisRaw("RightStickVertical"));
            transform.rotation = Quaternion.LookRotation(lookTowards);
        }

    }

    private void ThrowGlowstick()
    {
        glowstickCount--;

        var glowstickObj = Instantiate(glowstick, glowstickSpawn.transform.position, Quaternion.identity);
        glowstickObj.transform.rotation = this.transform.rotation;

        Rigidbody glowstickRB = glowstickObj.GetComponent<Rigidbody>();
        glowstickRB.AddForce(glowstickObj.transform.forward * 800);

    }

    IEnumerator GlowstickCooldown()
    {
        isGlowstickCooldown= true;

        yield return new WaitForSeconds(0.75f);

        isGlowstickCooldown = false;
    }
}
