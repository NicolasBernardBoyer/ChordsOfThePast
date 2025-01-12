using UnityEngine;

public class QuestionMark : MonoBehaviour
{
    public GameObject midiImage;
    public GameObject keyboardImage;

    public GameObject folder;
    public bool isPressed;

    public void MidiPressed()
    {
        midiImage.SetActive(true);
        keyboardImage.SetActive(false);
    }

    public void KeyboardPresse()
    {
        midiImage.SetActive(false);
        keyboardImage.SetActive(true);
    }

    public void QuestionPressed()
    {
        isPressed = !isPressed;
        folder.SetActive(isPressed); 
    }
}
