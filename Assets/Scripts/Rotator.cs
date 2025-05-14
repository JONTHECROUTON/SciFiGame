/* Art 157 - Rotator.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: Script to allow continuous
 * rotation of the object it is assigned to.
 */
using UnityEngine;

public class Rotator : MonoBehaviour
{

    [SerializeField] Vector3 rotationAmount;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(rotationAmount * Time.deltaTime);
    }
}
