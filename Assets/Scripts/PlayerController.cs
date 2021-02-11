using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody fizik;
    private Vector3 velocity=Vector3.zero;


    [SerializeField]
    private int speed = 10;

    [SerializeField]
    private int lookSensitivity=5;

    public Camera cam;

    public static int HEALTH;


    private const float verticalConstDown = 60f;
    private const float verticalConstUp = 270f;

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
        Vector3 rotation = new Vector3(verticalCamera, 0, 0)*lookSensitivity;
        cam.transform.Rotate(-rotation, Space.Self);
        /*
        Debug.Log(cam.transform.localRotation.eulerAngles);
        

        //for to prevent 360 degree vertical movement
        if (cam.transform.localRotation.eulerAngles.x<verticalConstDown)
        {          
            cam.transform.Rotate(-rotation, Space.Self);
        }
        if (cam.transform.localRotation.eulerAngles.x > verticalConstUp)
        {
            cam.transform.Rotate(-rotation, Space.Self);
        }
        */

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
            Destroy(other.gameObject);
            UI.AmmoChanged();
        }
    }
}

