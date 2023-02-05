using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerHealth = 5;
    [SerializeField] GameObject glowstick;
    [SerializeField] float speed = 0.05f;
    [SerializeField] int glowstickCount = 15;
    [SerializeField] GameObject glowstickSpawn;
    [SerializeField] TextMeshProUGUI glowstickText;
    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] GameObject GameOverObj;
    [SerializeField] TextMeshProUGUI GameOverText;
    [SerializeField] GameObject GameWinObj;
    [SerializeField] TextMeshProUGUI GameWinText;

    public static int score = 0;

    [SerializeField] AudioClip soundOnHeal;
    [SerializeField] AudioClip soundOnDeath;
    [SerializeField] AudioClip soundOnDamage;
    [SerializeField] AudioClip soundOnGlowstickThrow;

    public int keyCount = 0;
    public bool isUsingController = false;
    public bool isShooting = false;

    private bool isGlowstickCooldown = false;
    float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;
    private Rigidbody rb;
    private bool isGameOver = false;
    public bool playerDead = false;
    private ObjectSoundController soundController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        soundController = GetComponent<ObjectSoundController>();

        isUsingController = ControllerManager.isUsingController;
    }

    // Update is called once per frame
    void Update()
    {
        // Uses Input Axis "Jump" since that is what spacebar is in the Input system
        // This allows the controller to throw a glowstick as well via the same if statement
        if (Input.GetAxis("Jump") > 0 && !playerDead) 
        {
            if(glowstickCount > 0 && !isGlowstickCooldown) ThrowGlowstick();       
        }

        // Game over handler
        if (playerHealth <= 0 && !isGameOver)
        {
            playerDead = true;

            Debug.Log("Score before death: " + ScoreManager.score);

            ScoreManager.score = 0;

            Debug.Log("Player health is equal to or under 0.");
            Debug.Log(ScoreManager.score);

            StartCoroutine(GameOver(false));
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        HandleUI();
    }

    private void FixedUpdate()
    {
        if (playerDead) return;
        MovePlayer();
        RotatePlayer();
    }

    private void HandleUI()
    {
        glowstickText.text = "X " + glowstickCount;
        healthText.text = "X " + playerHealth;
    }


    private void MovePlayer()
    {
        if (isShooting || isGameOver) return;

        // FIXME!!! Since we access the same inputs multiple times we should store them

        transform.Translate(0, 0, Input.GetAxis("Vertical") * 0.02f * speed, Space.World); // 0.02f was Time.deltaTime
        transform.Translate(Input.GetAxis("Horizontal") * 0.02f * speed, 0, 0, Space.World);

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
        //if (!isUsingController && !isShooting)
        if (!isShooting && !isGameOver)
        {
            float horiztonalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horiztonalInput, 0f, verticalInput).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
    }

    // Throws given glowstick prefab in the direction the player is facing
    private void ThrowGlowstick()
    {
        StartCoroutine(IEGlowstickCooldown());

        soundController.PlayAudio(soundOnGlowstickThrow);

        glowstickCount--;

        var glowstickObj = Instantiate(glowstick, glowstickSpawn.transform.position, Quaternion.identity);
        glowstickObj.transform.rotation = this.transform.rotation;

        Rigidbody glowstickRB = glowstickObj.GetComponent<Rigidbody>();
        glowstickRB.AddForce(glowstickObj.transform.forward * 800);

    }

    // Stops glowsticks being spammed
    IEnumerator IEGlowstickCooldown()
    {
        isGlowstickCooldown= true;

        yield return new WaitForSeconds(0.75f);

        isGlowstickCooldown = false;
    }

    // Instead of having a public int for player health, we use a public function
    // that ensures we are recieving the correct type of damage/health (int) and does the math
    // here
    public void HurtPlayer(int damage)
    {
        playerHealth -= damage;

        soundController.PlayAudio(soundOnDamage);

        Debug.Log("Player has been hurt. Health is now at: " + playerHealth);
    }

    public void HealPlayer(int heal)
    {
        playerHealth += heal;

        soundController.PlayAudio(soundOnHeal);

        Debug.Log("Player has been healed. Health is now at: " + playerHealth);
    }

    public IEnumerator GameOver(bool wonGame)
    {
        isGameOver = true;
        if (wonGame)
        {
            GameWinText.text = "You win! Final Score: " + PlayerController.score;
            GameWinObj.SetActive(true);
        }
        else
        {
            soundController.PlayAudio(soundOnDeath);

            Debug.Log("Game over, about to return to main menu. Enable game over overlay");
            Debug.Log(ScoreManager.score);


            GameOverText.text = "Game Over! Final Score: " + PlayerController.score;
            GameOverObj.SetActive(true);
        }

        /*soundController.PlayAudio(soundOnDeath);

        Debug.Log("Game over, about to return to main menu. Enable game over overlay");
        Debug.Log(ScoreManager.score);

  
        GameOverText.text = "Game Over! Final Score: " + PlayerController.score;
        GameOverObj.SetActive(true);*/
        
      
        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene("MainMenu");
    }
}
