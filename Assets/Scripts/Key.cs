/* Art 157 - Checkpoint.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: Script to rotate key object
 * and then make the key interactive including
 * having the key unlock the door, playing a sound,
 * the key object disappearing, and a message
 * displayed about the door now being unlocked.
 */

using UnityEngine;

public class Key : MonoBehaviour
{
    AudioSource audioLink;
    [SerializeField] GameObject keyObject;
    [SerializeField] Door doorScript;
    [SerializeField] float rotationRate;
    Display displayLink;

    void Start()
    {
        audioLink = GetComponent<AudioSource>();
        displayLink = FindAnyObjectByType<Display>();
    }


    void Update()
    {
        transform.Rotate(0, rotationRate * Time.deltaTime, 0);
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            audioLink.Play();
            Destroy(keyObject);
            doorScript.doorLocked = false;
            Invoke(nameof(DestroyObject), 2);
            displayLink.ShowMessage("Door Unlocked");
        }
    }
}
