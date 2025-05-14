/* Art 157 - Display.cs
 * Version 1.0 by Jonathan Collins
 * 
 * Description: Script to display messages
 * on the screen.
 */



using UnityEngine;
using UnityEngine.UIElements;

public class Display : MonoBehaviour
{
    VisualElement root;
    Label message;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        message = root.Query<Label>("Message");

        ClearMessage();
    }


    void Update()
    {
        
    }

    void ClearMessage()
    {
        message.text = null;
    }

    public void ShowMessage (string phrase)
    {
        message.text = phrase;
        Invoke(nameof(ClearMessage), 3);
    }
}
