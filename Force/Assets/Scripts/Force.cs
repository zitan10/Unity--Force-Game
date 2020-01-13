using System.Collections;
using UnityEngine;


public class Force : MonoBehaviour {

    private float forceHoldSpeed = 10f;

    private float jumpForce = 10f;

    GameObject[] enemies;

    private Rigidbody rb;

    private bool grounded;

    new Animator animation;

    public GameObject player_base;

    // Use this for initialization
    void Start () {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        animation = player_base.GetComponent<Animator>();
        grounded = true;
    }
	
	// Update is called once per frame
	void Update () {

        //Player jump force attack
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //To Do: Replace StartCoroutine time delay with AnimationsEvent
            StartCoroutine(JumpAttack());
        }
        //Force Push Enemies
        else if (Input.GetButtonDown("Fire1"))
        {
            // To Do: Replace StartCoroutine time delay with AnimationsEvent
            StartCoroutine(PushAttack());
        }
        else 
        {
            foreach (GameObject enemy in enemies)
            {
                //Lift enemies off the ground
                if (Input.GetMouseButton(1))
                {

                    Vector3 upVelocity = enemy.transform.up.normalized * forceHoldSpeed;
                    rb = enemy.GetComponent<Rigidbody>();
                    rb.MovePosition(rb.position + upVelocity * Time.fixedDeltaTime);

                }
                //Slam Down enemies
                else if (Input.GetMouseButtonUp(1))
                {
                    rb = enemy.GetComponent<Rigidbody>();
                    rb.AddTorque(transform.right * 500f);
                    rb.AddTorque(transform.up * 500f);
                    rb.AddForce(-transform.up * 1000);
                    rb.GetComponent<Rigidbody>().useGravity = true;

                }
            }
        }
    }

    //To Do: Replace time delay with AnimationsEvent
    private IEnumerator JumpAttack()
    {
        yield return new WaitForSeconds(2.5f);
        foreach (GameObject enemy in enemies)
        {
            if (animation.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                rb = enemy.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                rb.AddTorque(transform.right * 500f);
                rb.AddTorque(transform.up * 500f);
            }
        }
    }

    //To Do: Replace time delay with AnimationsEvent
    private IEnumerator PushAttack()
    {
        yield return new WaitForSeconds(1.65f);
        foreach (GameObject enemy in enemies)
        {
            rb = enemy.GetComponent<Rigidbody>();
            rb.AddTorque(transform.right * 1000f);
            rb.AddTorque(transform.up * 1000f);
            rb.AddForce(transform.forward * 5000);
        }
    }
}
