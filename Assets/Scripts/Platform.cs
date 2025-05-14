/* Art 157 - Platform.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: A script to have the platform in the pit
 * move from door to door as well as parent the player
 * to the platform.
 */

using UnityEngine;

public class Platform : MonoBehaviour
{

    [SerializeField] GameObject currentGoal;
    [SerializeField] float moveModifier;
    float moveAmount;
    Vector3 currentPosition;
    Vector3 goalPosition;
    float goalDistance;
    [SerializeField] GameObject startGoal;
    [SerializeField] GameObject endGoal;
    bool moveToGoal = true;
    [SerializeField] float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        goalPosition = currentGoal.transform.position;
        moveAmount = moveModifier * Time.deltaTime;

        goalDistance = Vector3.Distance(currentPosition, goalPosition);

        if(goalDistance > moveAmount && moveToGoal == true)
        {
            transform.position = Vector3.MoveTowards(currentPosition, goalPosition, moveAmount);
        }

        else if(goalDistance < moveAmount && moveToGoal == true)
        {
            //Debug.Log("Reached Goal");
            Invoke(nameof(SwitchDirection), waitTime);
            moveToGoal = false;
        }
    }

    void SwitchDirection()
    {
        if(currentGoal == startGoal)
        {
            currentGoal = endGoal;
        }

        else
        {
            currentGoal = startGoal;
        }

        moveToGoal = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
