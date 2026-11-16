using TMPro;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField] TMP_Text label;              // drag the button's text here

    void Start() => Show();

    public void Toggle()                          // Button: On Click () → SoundButton.Toggle
    {
        Services.Settings.SoundOn = !Services.Settings.SoundOn;
        Show();
    }

    void Show() => label.text = Services.Settings.SoundOn ? "Sound: On" : "Sound: Off";
}