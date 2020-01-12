using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public Camera PlayerCam;

    private float speed;

    [SerializeField]
    private float lookSensitivity = 5f;

    private float jumpForce = 25f;

    private PlayerMotor motor;

    private Rigidbody rb;

    private bool grounded;

    new Animator animation;

    public GameObject player_base;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
        rb = GetComponent<Rigidbody>();
        animation = player_base.GetComponent<Animator>();
    }

    private void Update()
    {
        //Check if player wants to sprint
        speed = Input.GetKey(KeyCode.LeftShift) ? 20f : 5f;

        //Calculate movement velocity as a 3D vector
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVertical = transform.forward * zMov;

        //Animation Control
        //Jump animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animation.SetBool("isIdle", false);
            animation.SetBool("isWalk", false);
            animation.SetBool("isJump", true);
            animation.SetBool("isPush", false);
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            animation.SetBool("isWalk", false);
            animation.SetBool("isIdle", false);
            animation.SetBool("isJump", false);
            animation.SetBool("isPush", true);
        }
        //Walk Animation
        else if (xMov != 0 || zMov != 0)
        {
            animation.SetBool("isWalk", true);
            animation.SetBool("isIdle", false);
            animation.SetBool("isJump", false);
            animation.SetBool("isPush", false);
        }
        //Walk animations
        else
        {
            animation.SetBool("isWalk", false);
            animation.SetBool("isIdle", true);
            animation.SetBool("isJump", false);
            animation.SetBool("isPush", false);
        }

        //Final movement vector
        Vector3 velocity = (movHorizontal + movVertical).normalized * speed;

        //Apply movement
        motor.Move(velocity);

        //Calculate rotation as a 3D vector (turning around)
        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;

        //Apply rotation
        motor.Rotate(rotation);

        //Calculate camera as a 3D vector (turning around)
        //float xRot = Input.GetAxisRaw("Mouse Y");

        //Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;

        //Apply rotation
        //motor.RotateCamera(cameraRotation);

    }

}
