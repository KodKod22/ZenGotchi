using UnityEngine;
using UnityEngine.UI;

public enum Setting { Sound, Music, Sfx, Vibration }

public class SettingToggle : MonoBehaviour
{
    [SerializeField] Setting setting;            // which setting this button controls
    [SerializeField] Image icon;                 // the icon inside the button
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;           // optional: if empty, the icon is dimmed instead

    void OnEnable() => Show();                   // refresh every time the panel opens

    public void Toggle()                         // Button: On Click () → SettingToggle.Toggle
    {
        Set(!Get());
        Show();
    }

    bool Get()
    {
        var s = Services.Settings;
        switch (setting)
        {
            case Setting.Sound: return s.SoundOn;
            case Setting.Music: return s.MusicOn;
            case Setting.Sfx: return s.SfxOn;
            default: return s.VibrationOn;
        }
    }

    void Set(bool value)
    {
        var s = Services.Settings;
        switch (setting)
        {
            case Setting.Sound: s.SoundOn = value; break;
            case Setting.Music: s.MusicOn = value; break;
            case Setting.Sfx: s.SfxOn = value; break;
            default:
                s.VibrationOn = value;
                s.Vibrate();                     // a short buzz confirms it was turned on
                break;
        }
    }

    void Show()
    {
        bool on = Get();
        if (offSprite != null)
        {
            icon.sprite = on ? onSprite : offSprite;
            icon.color = Color.white;
        }
        else
        {
            icon.sprite = onSprite;
            icon.color = new Color(1f, 1f, 1f, on ? 1f : 0.35f);
        }
    }
}