/* ART 157 - Pulse.cs
 * Version 1.0 By Jonathan Collins
 * 
 * Description: Script to allow adjustment to the
 * speed and offset of an animation.
 */

using UnityEngine;

public class Pulse : MonoBehaviour
{
    Animation animationLink;
    string clipName;
    [SerializeField] float speedAdjustment;
    [SerializeField] float timeOffset;

    void Start()
    {
        animationLink = GetComponent<Animation>();
        clipName = animationLink.clip.name;
        animationLink[clipName].time = timeOffset;
        animationLink[clipName].speed = speedAdjustment;
    }


}
