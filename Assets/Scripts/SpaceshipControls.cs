﻿using System.Collections;
using System.Collections.Generic;
//using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;


public class SpaceshipControls : MonoBehaviour {

    public Rigidbody2D rb;
    public float thrust;
    public float turnThrust;
    private float thrustInput;
    private float turnInput;
    public float screenTop;
    public float screenBottom;
    public float screenLeft;
    public float screenRight;
    public float bulletForce;

    public GameObject bullet;

	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

        //Checks for input from keyboard

        //Vector2 moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal") * thrust, CrossPlatformInputManager.GetAxis("Vertical") );
        thrustInput = Input.GetAxis("Vertical")*-1;
        turnInput = Input.GetAxis("Horizontal")*-1;

        //Check for input from the fire key and make bullets
        if(Input.GetButtonDown("Fire1"))
        {
           GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
            newBullet.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * bulletForce);
            Destroy(newBullet, 3.0f);
        }

        //rotate the ship

        transform.Rotate(Vector3.forward * turnInput * Time.deltaTime * -turnThrust);



        //Screen Wrapping

        Vector2 newPos = transform.position;
        if(transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }

        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }

        transform.position = newPos;

    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector2.up * thrustInput);
        rb.AddTorque(turnInput);
        
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
