using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerHealth = 5;
    [SerializeField] GameObject glowstick;
    [SerializeField] float speed = 0.05f;
    [SerializeField] int glowstickCount = 15;
    [SerializeField] GameObject glowstickSpawn;

    public bool isUsingController = false;
    private bool isGlowstickCooldown = false;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") > 0) 
        {
            if(glowstickCount > 0 && !isGlowstickCooldown) { ThrowGlowstick(); StartCoroutine(GlowstickCooldown()); }        
        }


        if (playerHealth <= 0)
        {
            Debug.Log("Player health is equal to or under 0. Implement functionality");
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }


    private void MovePlayer()
    {
        // FIXME!!! Since we access the same inputs multiple times we should store them

        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.World);
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0, 0, Space.World);

        // Colliding with enemies would sometimes add a force to the player that would
        // never stop slowly moving the player. This fixes that bug
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            rb.velocity = Vector3.zero;
        }

    }

    private void RotatePlayer()
    {
        // Mouse rotation controls
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

    public void HurtPlayer(int damage)
    {
        playerHealth -= damage;

        Debug.Log("Player has been hurt. Health is now at: " + playerHealth);
    }
}
