using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour {

    public float speed = 5f;
    public float sprintSpeed = 18f;
    public float boostSpeed = 30f;
    public float jumpSpeed = 10f;
    public float rayDis = 1f;
    public float hCamSpeed = 20f;
    public float vCamSpeed = 20f;

    float oldSpeed;

    bool mayGiveBackStamina;

    public int health = 100;
    public float stamina = 100f;
    public float staminaRegain;
    public float staminaReduceSprint, staminaReduceJump, staminaReduceBoost;

    public GameObject mainCam;
    public GameObject gameOverMenu;
    public GameObject mesh;

    public UI_Controller uiController;
    public Animator mainCharAnimArms;
    public Animator mainCharAnimLegs;

    void Start () {

        oldSpeed = speed;

	}
	
	void Update () {

        GiveStamina(staminaRegain);

    }

    void FixedUpdate ()
    {

        Movement();
        CameraController();
        Jump(staminaReduceJump);

    }

    void Movement () {

        if (Input.GetAxis("Vertical") > 0)
        {
            if (Physics.Raycast(transform.position, transform.forward, rayDis))
            {
                print("HitWall");
            }
            else
            {
                transform.Translate(Vector3.forward * speed * Input.GetAxis("Vertical") * Time.deltaTime);
                mainCharAnimLegs.SetFloat("Move", 1);
            }
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            if (Physics.Raycast(transform.position, -transform.forward, rayDis))
            {

            }
            else
            {
                transform.Translate(Vector3.back * speed * -Input.GetAxis("Vertical") * Time.deltaTime);
                mainCharAnimLegs.SetFloat("Move", 1);
            }
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            if (Physics.Raycast(transform.position, transform.right, rayDis))
            {

            }
            else
            {
                transform.Translate(Vector3.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime);
                mainCharAnimLegs.SetFloat("Move", 1);
            }
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            if (Physics.Raycast(transform.position, -transform.right, rayDis))
            {

            }
            else
            {
                transform.Translate(Vector3.left * speed * -Input.GetAxis("Horizontal") * Time.deltaTime);
                mainCharAnimLegs.SetFloat("Move", 1);
            }
        }


        if (Input.GetButton("Sprint"))
        {

            Boost(sprintSpeed, 3f, staminaReduceSprint);

        }
        else if (Input.GetButton("Boost"))
        {

            Boost(boostSpeed, 6f, staminaReduceBoost);

        }
        else
        {

            speed = oldSpeed;
            mainCharAnimLegs.SetFloat("SprintSpeedMulti", 1f);

        }

    }

    void Jump (float staminaAmount)
    {

        if(Input.GetButtonDown("Jump"))
        {

            if(Physics.Raycast(transform.position, Vector3.down, rayDis + (transform.localScale.y / 2)))
            {
                if(stamina >= staminaAmount)
                {

                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, jumpSpeed + speed / 2, 0);
                    mainCharAnimLegs.SetTrigger("Jump");
                    ReduceStamina(staminaAmount, true);


                }

            }

        }

    }

    void Boost (float newSpeed, float animSpeed, float reduceStamina)
    {

        if (stamina >= 0.1f)
           {

             speed = newSpeed;
             ReduceStamina(reduceStamina, false);
             mainCharAnimLegs.SetFloat("SprintSpeedMulti", animSpeed);


        }

    }

    void ReduceStamina (float lostStamina, bool jumped)
    {

        if(jumped == false)
        {

            stamina -= lostStamina * Time.deltaTime;
            uiController.UpdateStaminaBar(stamina);

        }
        else
        {

            stamina -= lostStamina;
            uiController.UpdateStaminaBar(stamina);

        }

        if(mayGiveBackStamina == false)
        {

            StartCoroutine(GiveStaminaOverTime(3f));

        }

    }

    private IEnumerator GiveStaminaOverTime (float timer)
    {

       mayGiveBackStamina = true;

       yield return new WaitForSeconds(timer);

       mayGiveBackStamina = false;

    }

    void GiveStamina (float staminaGained)
    {

        if (mayGiveBackStamina == false && stamina < 100f)
        {

            stamina += staminaGained * Time.deltaTime;

            uiController.UpdateStaminaBar(stamina);

        }

    }

    public void StaminaPickup (float addedStamina)
    {

        stamina += addedStamina;
        uiController.UpdateStaminaBar(stamina);

    }

    void CameraController ()
    {

        float verticalCam = vCamSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        float horizontalCam = hCamSpeed * -Input.GetAxis("Mouse Y") * Time.deltaTime;

        transform.Rotate(0, verticalCam, 0);

        mainCam.transform.Rotate(horizontalCam, 0, 0);


    }

    public void LockCursor (bool lockCursor)
    {

        if(lockCursor == true)
        {

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

        }
        else
        {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
            

    }

    public void CheckHealth (int damage)
    {
        health -= damage;

        uiController.UpdateHealthBar((float)health);

        if(health < 1)
        {

            gameOverMenu.SetActive(true);
            gameOverMenu.GetComponent<GameOverMenu>().LoadHighscore();
            gameOverMenu.GetComponent<GameOverMenu>().GetCurrentScore();
            Destroy(mesh);
            Destroy(gameObject);
            Time.timeScale = 0;
            print("death");

        }

    }

}
