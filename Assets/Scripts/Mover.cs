/*Art 157 - Mover.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: Script to move a collection
 * of objects in the same direction, and
 * then move the objects back to the start location
 * to allow continuous movement.
 * 
 */
using System.Linq;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform moverParent;
    Transform[] moverObjects;
    int moverCount;
    [SerializeField] Vector3 moveAmount;
    [SerializeField] Vector3 resetAmount;

    void Start()
    {
        moverObjects = moverParent.GetComponentsInChildren<Transform>();
        moverCount = moverObjects.Count();
    }


    void Update()
    {
        for(int index=1; index < moverCount; index++)
        {
            moverObjects[index].Translate(moveAmount * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.transform.Translate(resetAmount);
    }
}
