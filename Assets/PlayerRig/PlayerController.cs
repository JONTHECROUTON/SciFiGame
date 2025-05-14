/* PlayerController.cs
 * 
 * Developed by Drew Snyder for Unity 2022.3
 * 
 * Version 2.0
 * 
 * Description: A Rigid Body first person rig that
 * uses Unity's new Input System.  The rig includes 
 * moving with WASD and Arrow Keys, looking with Mouse,
 * and Jumping with the Spacebar.  Additional code uses
 * Q to Quit the game (only works in Build), and R to 
 * Reload the scene.  The Input Actions includes a Fire
 * action that is not used in the script. 
 * 
 * Removed the maxVelocity and replaced with a horizontal
 * speed clamp.  Then revised the vertical look system. 
 * Added gravity amplifier. 
 * 
 */

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float jumpForce = 1500;
    [SerializeField] float rayDistance = 2f;
    [SerializeField] float moveAdjustment = 500;
    [SerializeField] float rotateAdjustX = 0.15f;
    [SerializeField] float rotateAdjustY = -0.1f;
    [SerializeField] float maxVelocity = 4;
    bool jumpInput;
    Rigidbody rigidLink;
    public bool isGrounded;
    Vector2 moveInput;
    bool movePlayer;
    float moveX;
    float moveY;
    Vector2 lookInput;
    float rotateX;
    float rotateY;
    bool rotatePlayerX;
    GameObject cameraLink;
    bool rotatePlayerY;
    float cameraRotX;

    //Variables added after F24 videos were made, do not change.
    float lookAngle;
    float horizontalSpeed;
    float lookVerticalMax = 45;
    float lookVerticalMin = -30;
    float gravityAdjust = -20;
    

    void Start()
    {
        rigidLink = GetComponent<Rigidbody>();
        cameraLink = transform.GetChild(0).gameObject;
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        Grounded();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Look()
    {
        if (rotatePlayerX)
        {
            transform.Rotate(new Vector3(0, rotateX, 0));
        }

        //Revised after F24 Videos were made, do not change
        if (rotatePlayerY)
        {
            cameraRotX = cameraLink.transform.rotation.eulerAngles.x;

            if(cameraRotX < 180)
            {
                lookAngle = cameraRotX * -1;
            }

            else
            {
                lookAngle = 360 - cameraRotX;
            }

            //Debug.Log("lookangle: " + lookAngle + " rotateY: " + rotateY);

            if (lookAngle - rotateY > lookVerticalMin && lookAngle - rotateY < lookVerticalMax)
            {
                cameraLink.transform.Rotate(new Vector3(rotateY, 0, 0));
            }
        }

    }

    
    private void Move()
    {
        //Revised after F24 Videos were made, do not change
        if (movePlayer == true) 
        {
            horizontalSpeed = new Vector3(rigidLink.velocity.x, 0.0f, rigidLink.velocity.z).magnitude;

            if (horizontalSpeed < maxVelocity)
            {
                rigidLink.AddRelativeForce(new Vector3(moveX, 0, moveY));
            }
        }
    }

    void Grounded()
    {
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.down), rayDistance))
        {
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
            //Added after F24 Videos were made, do not change
            rigidLink.AddRelativeForce(new Vector3(0, gravityAdjust, 0));
        }
    }
    
    public void OnJump()
    {

        if (isGrounded == true)
        {
            rigidLink.AddForce(new Vector3(0, jumpForce, 0));
        }
    }

    public void OnMove(InputAction.CallbackContext inputData)
    {
        moveInput = inputData.ReadValue<Vector2>();
        moveX = moveInput.x * moveAdjustment;
        moveY = moveInput.y * moveAdjustment;

        if (moveX != 0 || moveY !=0)
        {
            movePlayer = true;
        }

        else
        {
            movePlayer = false;
        }
    }

    public void OnLook(InputAction.CallbackContext inputData)
    {
        //Revised after F24 Videos were made, do not change

        lookInput = inputData.ReadValue<Vector2>();
        //Debug.Log("lookInput" + lookInput);

        if(lookInput.x < -50 || lookInput.x > 50)
        {
            lookInput.x = 0;
        }

        if(lookInput.y < -50 || lookInput.x > 50)
        {
            lookInput.y = 0;
        }
        
        rotateX = lookInput.x * rotateAdjustX;
        rotateY = lookInput.y * rotateAdjustY;

        if (rotateX != 0)
        {
            rotatePlayerX = true;
        }

        else
        {
            rotatePlayerX = false;
        }
       
        if (rotateY != 0)
        {
            rotatePlayerY = true;
        }

        else
        {
            rotatePlayerY = false;
        }

    }

    public void OnFire()
    {
        //Debug.Log("Fire Button Pressed");
    }

    public void OnReload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuit()
    {
        Application.Quit();
        Debug.Log("Quit Game - Function works in Build but not in Editor");
    }



}
