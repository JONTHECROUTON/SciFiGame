/* Art 157 - Checkpoint.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: Script that senses when player
 * has moved through a checkpoint, and then resets
 * the player's position and rotation
 * should the player hit a hazard object such as
 * falling into the pit.
 */

using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Vector3 checkPosition;
    Quaternion checkRotation;


    private void OnTriggerEnter(Collider other)
    {
        var hitTag = other.gameObject.tag;

        if(hitTag == "Checkpoint")
        {
            //Debug.Log("Checkpoint Reached");
            checkPosition = transform.position;
            checkRotation = transform.rotation;
        }

        else if(hitTag == "Hazard")
        {
            transform.position = checkPosition;
            transform.rotation = checkRotation;
        }
    }
}
