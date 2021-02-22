using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody fizik;
    private Vector3 velocity=Vector3.zero;

    [SerializeField]
    private int speed = 8, lookSensitivity = 5;

    public Camera cam;

    public static int HEALTH;

    private const float verticalConstDown = -8f;
    private const float verticalConstUp = 11f;
    private float currentLocation=0;

    
    void Start()
    {
        HEALTH = 100;
        Cursor.visible = false;//for hiding the mouse cursor
        fizik = transform.GetComponent<Rigidbody>();      
    }

    
    void FixedUpdate()
    {
        //GAME OVER STATEMENT
        if(HEALTH<=0)
        {
            GameOver();
        }

        
        BodyMovement();
        HeadMovementHorizontal();
        HeadMovementVertical();
        SpeedControl();//sprint or not

        UI.HealthChanged(HEALTH);
    }

    void BodyMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 horizontalMovement = horizontal * transform.right;
        Vector3 verticalMovement = vertical * transform.forward;

        this.velocity = (horizontalMovement + verticalMovement).normalized * speed;

        fizik.velocity = velocity;
    }

    void HeadMovementHorizontal()
    {
        float horizontalCamera = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0, horizontalCamera, 0)* lookSensitivity;

        transform.Rotate(rotation, Space.World);
    }

    void HeadMovementVertical()
    {
        float verticalCamera = Input.GetAxisRaw("Mouse Y");
        currentLocation = Mathf.Clamp(currentLocation + verticalCamera, verticalConstDown, verticalConstUp);
        Vector3 rotation = new Vector3(verticalCamera, 0, 0) * lookSensitivity;

        if (currentLocation==verticalConstDown)//lower limit
        {
            if(verticalCamera>0)//input can not be negative at the lower limit
            {
                cam.transform.Rotate(-rotation, Space.Self);
            }            
        }
        else if(currentLocation==verticalConstUp)//upper limit
        {
            if(verticalCamera<0)//input can not be positive at the upper limit
            {
                cam.transform.Rotate(-rotation, Space.Self);
            }
        }        
        else//between two consts
        {
            cam.transform.Rotate(-rotation, Space.Self);
        }
    }

    void SpeedControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))//sprint
        {
            speed = 15;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 8;
        }
    }

    void GameOver()
    {
        Application.Quit();
    }

    private void OnTriggerEnter(Collider other)
    {
        //max ammo
        if (other.gameObject.tag == "maxammo" && FireScript.ammo<100)
        {
            FireScript.ammo = 100;
            GrenadeLauncher.ammo = 4;
            Destroy(other.gameObject);
            UI.AmmoChanged(); UI.FragChanged();
        }
    }
}

