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
    public UI_Controller uiController;
    public Animator mainCharAnimArms;
    public Animator mainCharAnimLegs;

    void Start () {

        oldSpeed = speed;

        LockCursor();

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
            if (Physics.Raycast(transform.position, Vector3.forward, rayDis))
            {
                print("HitWall");
            }
            else
            {
                transform.Translate(Vector3.forward * speed * Input.GetAxis("Vertical") * Time.deltaTime);
                mainCharAnimLegs.SetFloat("Move", 1);
                Boost();
            }
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            if (Physics.Raycast(transform.position, Vector3.back, rayDis))
            {
                print("HitWall");
            }
            else
            {
                transform.Translate(Vector3.back * speed * -Input.GetAxis("Vertical") * Time.deltaTime);
                mainCharAnimLegs.SetFloat("Move", 1);
                Boost();
            }
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            if (Physics.Raycast(transform.position, Vector3.right, rayDis))
            {
                print("HitWall");
            }
            else
            {
                transform.Translate(Vector3.right * speed * Input.GetAxis("Horizontal") * Time.deltaTime);
                mainCharAnimLegs.SetFloat("Move", 1);
                Boost();
            }
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            if (Physics.Raycast(transform.position, Vector3.left, rayDis))
            {
                print("HitWall");
            }
            else
            {
                transform.Translate(Vector3.left * speed * -Input.GetAxis("Horizontal") * Time.deltaTime);
                mainCharAnimLegs.SetFloat("Move", 1);
                Boost();
            }
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

    void Boost ()
    {

        if (Input.GetButton("Sprint"))
        {
            if (stamina >= 0.1f)
            {

                speed = sprintSpeed;
                ReduceStamina(staminaReduceSprint, false);
                mainCharAnimLegs.SetFloat("SprintSpeedMulti", 3f);


            }
            else
            {

                speed = oldSpeed;
                mainCharAnimLegs.SetFloat("SprintSpeedMulti", 1f);

            }

        }
        else
        {

            mainCharAnimLegs.SetFloat("SprintSpeedMulti", 1f);

        }

        if (Input.GetButton("Boost"))
        {

            if (stamina >= 0.1f)
            {

                speed = boostSpeed;
                ReduceStamina(staminaReduceBoost, false);
                mainCharAnimLegs.SetFloat("SprintSpeedMulti", 6f);

            }
            else
            {

                speed = oldSpeed;
                mainCharAnimLegs.SetFloat("SprintSpeedMulti", 1f);

            }

        }
        else
        {

            speed = oldSpeed;
            mainCharAnimLegs.SetFloat("SprintSpeedMulti", 1f);

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

    void CameraController ()
    {

        float verticalCam = vCamSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        float horizontalCam = hCamSpeed * -Input.GetAxis("Mouse Y") * Time.deltaTime;

        transform.Rotate(0, verticalCam, 0);
        mainCam.transform.Rotate(Mathf.Clamp(horizontalCam, -20f, 20f), 0, 0);

    }

    void LockCursor ()
    {

        Cursor.lockState = CursorLockMode.Locked;

    }

    public void CheckHealth (int damage)
    {
        health -= damage;

        uiController.UpdateHealthBar((float)health);

        if(health < 1)
        {

            print("death");

        }

    }

}
