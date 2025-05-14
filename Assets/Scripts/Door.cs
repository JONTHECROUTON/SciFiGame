/* Art 157 - Door.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: Script to make the door
 * interactive by playing animations and
 * sounds when player comes near it, and
 * track whether or not the door is unlocked.
 */

using UnityEngine;
using UnityEngine.UIElements;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject doorLink;
    Animator animatorLink;
    AudioSource audioLink;
    [SerializeField] AudioClip openSound;
    [SerializeField] AudioClip closeSound;
    [SerializeField] AudioClip lockedSound;
    public bool doorLocked = true;
    Display displayLink;



    void Start()
    {
        animatorLink = doorLink.GetComponent<Animator>();
        audioLink = GetComponent<AudioSource>();
        displayLink = FindAnyObjectByType<Display>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (doorLocked == false)
            {
                animatorLink.Play("Door Open");
                audioLink.PlayOneShot(openSound);
            }

            else
            {
                audioLink.PlayOneShot(lockedSound);
                displayLink.ShowMessage("Need Keycard (Blue)");
            }
        }     
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (doorLocked == false)
            {
                animatorLink.Play("Door Close");
                audioLink.PlayOneShot(closeSound);
            }
        }
    }
}
