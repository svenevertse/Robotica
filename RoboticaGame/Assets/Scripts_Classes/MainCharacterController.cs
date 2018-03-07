using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour {

    public float speed = 5f;
    public float sprintSpeed = 3f;
    public float jumpSpeed = 10f;
    public float rayDis = 1f;
    public float hCamSpeed = 20f;
    public float vCamSpeed = 20f;

    float oldSpeed;

    public GameObject mainCam;


    void Start () {

        oldSpeed = speed;

        LockCursor();

	}
	
	void Update () {

        Movement ();
        CameraController();
        Jump();
		
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
            }
        }

        if (Input.GetButton("Sprint"))
        {
            print("sprint");
            speed = sprintSpeed;

        }
        else
        {

            speed = oldSpeed;

        }

    }

    void Jump ()
    {

        if(Input.GetButtonDown("Jump"))
        {

            if(Physics.Raycast(transform.position, Vector3.down, rayDis + (transform.localScale.y / 2)))
            {

                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 10, 0);
                

            }
            else
            {

                print("cant jump");

            }


        }

    }

    void CameraController ()
    {

        float verticalCam = vCamSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        float horizontalCam = hCamSpeed * -Input.GetAxis("Mouse Y") * Time.deltaTime;

        transform.Rotate(0, verticalCam, 0);
        mainCam.transform.Rotate(Mathf.Clamp(horizontalCam, -40f, 40f), 0, 0);

    }

    void LockCursor ()
    {

        Cursor.lockState = CursorLockMode.Locked;

    }

}
