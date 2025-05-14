/* ART 157 - ThirdPersonControls.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: Script to control the movement,
 * rotation, animations, and sounds of the 
 * third person character rig.
 */

using UnityEngine;
using UnityEngine.InputSystem;


public class ThirdPersonControls : MonoBehaviour
{
    Vector2 moveInput;
    Animator animatorLink;
    Rigidbody rigidLink;
    [SerializeField] float speedControl = 1500;
    Vector2 lookInput;
    [SerializeField] GameObject pivotLink;
    bool playerAligned;
    AudioSource audioLink;
    [SerializeField] AudioClip leftFootSound;
    [SerializeField] AudioClip rightFootSound;
    bool isGrounded;
    [SerializeField] float jumpForce;
    [SerializeField] float rayDistance;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip landSound;
    [SerializeField] AudioClip unarmedAttackSound;
    int currentWeapon = 1;
    [SerializeField] AudioClip armedAttackSound;
    AudioClip soundToPlay;
    [SerializeField] GameObject weapon2;

    public AudioClip RightFootSound { get; private set; }

    void Start()
    {
        animatorLink = GetComponent<Animator>();
        rigidLink = GetComponent<Rigidbody>();
        rigidLink.maxLinearVelocity = 6;
        audioLink = GetComponent<AudioSource>();
        weapon2.SetActive(false);
    }


    void Update()
    {
        RotatePivot();
        RotatePlayer();
        CheckGround();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if(moveInput.y > 0 && playerAligned)
        {
            var moveAmount = moveInput.y * speedControl * Time.deltaTime;
            rigidLink.AddRelativeForce(new Vector3(0, 0, moveAmount));
        }
    }

    private void RotatePivot()
    {
        if(lookInput.x !=0)
        {
            var rotateAmount = lookInput.x * 20 * Time.deltaTime;
            pivotLink.transform.Rotate(new Vector3(0, rotateAmount, 0));
        }
    }

    private void RotatePlayer()
    {
        var rotateAmount = 300 * Time.deltaTime;

        if (moveInput.y > 0)
        {
            var pivotRotY = pivotLink.transform.rotation.eulerAngles.y;
            var playerRotY = transform.rotation.eulerAngles.y;

            var rotOffset = pivotRotY - playerRotY;

            if(rotOffset > 180)
            {
                rotOffset = rotOffset - 360;
            }

            else if(rotOffset < -180)
            {
                rotOffset = 360 + rotOffset;
            }

            playerAligned = false;

            if(rotOffset > 10)
            {
                transform.Rotate(0, rotateAmount, 0);
            }

            else if(rotOffset < -10)
            {
                transform.Rotate(0, -rotateAmount, 0);
            }

            else
            {
                playerAligned = true;
            }
        }

        else
        {
            if(moveInput.x > 0)
            {
                transform.Rotate(0, rotateAmount / 2, 0);
                animatorLink.SetFloat("Speed", 1);
            }

            else if(moveInput.x < 0)
                {
                    transform.Rotate(0, -rotateAmount / 2, 0);
                    animatorLink.SetFloat("Speed", 1);
                }
        }
    }

    private void CheckGround()
    {
        var rayStart = transform.position + new Vector3(0, 0.1f, 0);

        var rayDirection = transform.TransformDirection(Vector3.down);

        isGrounded = Physics.Raycast(rayStart, rayDirection, rayDistance);

        animatorLink.SetBool("Grounded", isGrounded);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        animatorLink.SetFloat("Speed", moveInput.y);
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();

        if(lookInput.x > 50)
        {
            lookInput.x = 0;
        }
    }

    public void OnJump()
    {
        rigidLink.AddForce(new Vector3(0, jumpForce, 0));
    }

    public void OnFire()
    {
        animatorLink.SetTrigger("Attack");
    }

    public void OnWeapon1()
    {
        if(animatorLink.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            currentWeapon = 1;
            animatorLink.SetInteger("Weapon", 1);
            weapon2.SetActive(false);
        }
    }

    public void OnWeapon2()
    {
        if (animatorLink.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            currentWeapon = 2;
            animatorLink.SetInteger("Weapon", 2);
            weapon2.SetActive(true);
        }
    }

    public void PlayLeftFootSound()
    {
        audioLink.PlayOneShot(leftFootSound);
    }

    public void PlayRightFootSound()
    {
        audioLink.PlayOneShot(rightFootSound);
    }

    public void PlayJumpSound()
    {
        audioLink.PlayOneShot(jumpSound);
    }

    public void PlayLandSound()
    {
        audioLink.PlayOneShot(landSound);
    }

    public void PlayAttackSound()
    {
        switch(currentWeapon)
        {
            case 1:
                soundToPlay = unarmedAttackSound;
                break;

            case 2:
                soundToPlay = armedAttackSound;
                break;
        }

        audioLink.PlayOneShot(unarmedAttackSound);
    }
}
