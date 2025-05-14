/* ART 157 - DoorLocked.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: Script to only play
 * door locked sound for unused doors.
 */
using UnityEngine;

public class DoorLocked : MonoBehaviour
{
    [SerializeField] GameObject doorLink;
    AudioSource audioLink;
    [SerializeField] AudioClip lockedSound;

    void Start()
    {
        audioLink = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioLink.PlayOneShot(lockedSound);
        }
    }
}
