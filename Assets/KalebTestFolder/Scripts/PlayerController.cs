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

    public int keyCount = 0;

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
        // Uses Input Axis "Jump" since that is what spacebar is in the Input system
        // This allows the controller to throw a glowstick as well via the same if statement
        if (Input.GetAxis("Jump") > 0) 
        {
            if(glowstickCount > 0 && !isGlowstickCooldown) ThrowGlowstick();       
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

    // If the player wants to use a controller, they must do so via some setting that changes
    // the bool value of isUsingController. The reason for this is because these two character
    // controls are handled differently. One uses the location of the mouse on the screen via a raycast,
    // the other uses the input of the rightstick.
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

    // Throws given glowstick prefab in the direction the player is facing
    private void ThrowGlowstick()
    {
        StartCoroutine(GlowstickCooldown());

        glowstickCount--;

        var glowstickObj = Instantiate(glowstick, glowstickSpawn.transform.position, Quaternion.identity);
        glowstickObj.transform.rotation = this.transform.rotation;

        Rigidbody glowstickRB = glowstickObj.GetComponent<Rigidbody>();
        glowstickRB.AddForce(glowstickObj.transform.forward * 800);

    }

    // Stops glowsticks being spammed
    IEnumerator GlowstickCooldown()
    {
        isGlowstickCooldown= true;

        yield return new WaitForSeconds(0.75f);

        isGlowstickCooldown = false;
    }

    // Instead of having a public int for player health, we use a public function
    // that ensures we are recieving the correct type of damage (int) and does the math
    // here
    public void HurtPlayer(int damage)
    {
        playerHealth -= damage;

        Debug.Log("Player has been hurt. Health is now at: " + playerHealth);
    }
}
