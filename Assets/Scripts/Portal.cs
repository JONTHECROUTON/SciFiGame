/* ART 157 - Portal.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: Script to control player
 * movement between scenes.
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] int sceneToLoad;
    [SerializeField] float delayTime;
    GlobalData globalScript;

    void ActivatePortal()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Invoke(nameof(ActivatePortal), delayTime);
            globalScript = FindAnyObjectByType<GlobalData>();
            globalScript.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
