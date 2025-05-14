/* ART 157 - LevelStartup.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: Script to make changes
 * when a scene is loaded.
 * 
 */

using UnityEngine;

public class LevelStartup : MonoBehaviour
{
    [SerializeField] bool isInterior;
    [SerializeField] bool isExterior;
    [SerializeField] Door doorScript;
    [SerializeField] GameObject keyPrefab;
    [SerializeField] GameObject playerLink;
    [SerializeField] GameObject returnLocation;
    [SerializeField] GameObject globalPrefab;
    [SerializeField] GlobalData globalScript;

 
    void Start()
    {
        globalScript = FindAnyObjectByType<GlobalData>();

        if (globalScript != null)
        {
            if (isInterior && globalScript.visitedExterior)
            {
                doorScript.doorLocked = false;
                Destroy(keyPrefab);
                playerLink.transform.position = returnLocation.transform.position;
                playerLink.transform.rotation = returnLocation.transform.rotation;
            }
        }

        else
        {
            var instance = Instantiate(globalPrefab);
            instance.GetComponent<AudioSource>().Play();
            globalScript =
                instance.GetComponent<GlobalData>();
        }

        if (isExterior)
        {
            globalScript.visitedExterior = true;
        }

        Destroy(gameObject);
    }


}
