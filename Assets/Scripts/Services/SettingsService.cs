// SettingsService.cs: settings stored through the save service, never PlayerPrefs directly
using UnityEngine;

public class SettingsService : ISettingsService
{
    const string SoundKey = "soundOn";
    readonly ISaveService save;

    public SettingsService(ISaveService save)
    {
        this.save = save;
        Apply();                                  // sound state is correct from the first scene
    }

    public bool SoundOn
    {
        get => save.GetInt(SoundKey, 1) == 1;     // default: sound on
        set
        {
            save.SetInt(SoundKey, value ? 1 : 0);
            Apply();
        }
    }

    void Apply() => AudioListener.volume = SoundOn ? 1f : 0f;
}