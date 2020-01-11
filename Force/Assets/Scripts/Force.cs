using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour {

    private bool forceHold;
    private bool inAir;
    private float forceHoldSpeed = 250f;

    GameObject[] enemies;

    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        forceHold = false;
        inAir = false;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
	}
	
	// Update is called once per frame
	void Update () {

        //Force Hold
        if (Input.GetButtonDown("Fire2"))
        {
            //Force hold enemies
            if (!forceHold)
            {
                foreach (GameObject enemy in enemies)
                {
                    Vector3 upVelocity = enemy.transform.up.normalized * forceHoldSpeed;
                    rb = enemy.GetComponent<Rigidbody>();
                    rb.MovePosition(rb.position + upVelocity * Time.fixedDeltaTime);
                    rb.GetComponent<Rigidbody>().useGravity = false;
                    forceHold = true;
                    inAir = true;
                }
            }
            //Slam Down enemies
            else if (forceHold && inAir)
            {
                foreach (GameObject enemy in enemies)
                {
                    //Vector3 upVelocity = enemy.transform.up.normalized * forceHoldSpeed;
                    rb = enemy.GetComponent<Rigidbody>();
                    //rb.MovePosition(rb.position - upVelocity * Time.fixedDeltaTime);
                    rb.AddForce(-transform.up * 1000);
                    rb.GetComponent<Rigidbody>().useGravity = true;
                    forceHold = true;
                    inAir = true;
                }
            }
        }
        else if (Input.GetButtonDown("Fire1")) 
        {
            foreach (GameObject enemy in enemies)
            {
                rb = enemy.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 5000);
            }
        }

    }
}
