using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class MainCharacterController : MonoBehaviour {

    public float speed;
    public float sprintSpeed;
    public float boostSpeed;
    public float jumpSpeed;
    public float rayDis;
    public float rayDisJump;
    public float hCamSpeed;
    public float vCamSpeed;
    public float stamina;
    public float maxStamina;
    public float staminaRegain;
    public float staminaReduceSprint, staminaReduceJump, staminaReduceBoost;

    float oldSpeed;

    bool mayGiveBackStamina;

    public int health;
    public int maxHealth;

    public GameObject mainCam;
    public GameObject gameOverMenu;
    public GameObject mesh;

    public Animator MainCharAnim;

    private ChromaticAberrationModel caModel;
    private PostProcessingProfile pProfile;

    public static MainCharacterController ins;

    void Awake ()
    {

        ins = this;

    }
    

    void Start () {

        oldSpeed = speed;
        health = maxHealth;

        var behaviour = mainCam.GetComponent<PostProcessingBehaviour>();
        pProfile = behaviour.profile;

        caModel = pProfile.chromaticAberration;

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
                MainCharAnim.SetFloat("Speed", Input.GetAxis("Vertical"));
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
                MainCharAnim.SetFloat("Speed", -Input.GetAxis("Vertical"));
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
                MainCharAnim.SetFloat("Speed", Input.GetAxis("Horizontal"));
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
                MainCharAnim.SetFloat("Speed", -Input.GetAxis("Horizontal"));
            }
        }


        if (Input.GetButton("Sprint"))
        {

            Boost(sprintSpeed, 3f, staminaReduceSprint, 0.7f);

        }
        else if (Input.GetButton("Boost"))
        {

            Boost(boostSpeed, 6f, staminaReduceBoost, 1f);

        }
        else
        {

            speed = oldSpeed;
            MainCharAnim.SetFloat("SprintSpeedMulti", 1f);
            ChangeCA(0f);

        }

    }

    void Jump (float staminaAmount)
    {

        if(Input.GetButtonDown("Jump"))
        {

            if(Physics.Raycast(transform.position, Vector3.down, rayDisJump + (transform.localScale.y / 2)))
            {
                if(stamina >= staminaAmount)
                {

                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, jumpSpeed + (speed / 3.5f), 0);
                    ReduceStamina(staminaAmount, true);


                }

            }

        }

    }

    void Boost (float newSpeed, float animSpeed, float reduceStamina, float caAmount)
    {


        if (stamina > 0.1f)
        {

            ChangeCA(caAmount);
            
            speed = newSpeed;
            ReduceStamina(reduceStamina, false);
            MainCharAnim.SetFloat("SprintSpeedMulti", animSpeed);

        }

    }

    void ChangeCA (float amount)
    {

        var ca = caModel.settings;

        ca.intensity = amount;

        pProfile.chromaticAberration.settings = ca;

    }

    void ReduceStamina (float lostStamina, bool jumped)
    {

        if(jumped == false)
        {

            stamina -= lostStamina * Time.deltaTime;
            UI_Controller.ins.UpdateStaminaBar(stamina);

        }
        else
        {

            stamina -= lostStamina;
            UI_Controller.ins.UpdateStaminaBar(stamina);

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

            UI_Controller.ins.UpdateStaminaBar(stamina);

        }

    }

    public void StaminaPickup (float addedStamina)
    {

        stamina += addedStamina;
        UI_Controller.ins.UpdateStaminaBar(stamina);

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

        UI_Controller.ins.UpdateHealthBar((float)health);

        if(health < 1)
        {

            gameOverMenu.SetActive(true);
            gameOverMenu.GetComponent<GameOverMenu>().LoadHighscore();
            gameOverMenu.GetComponent<GameOverMenu>().GetCurrentScore();
            Destroy(mesh);
            Destroy(gameObject);
            Time.timeScale = 0;

        }

    }

}
