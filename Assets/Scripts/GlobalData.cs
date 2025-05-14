/* ART 157 - GlobalData.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: Script to preserve data
 * between scenes.
 */
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public bool visitedExterior;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
