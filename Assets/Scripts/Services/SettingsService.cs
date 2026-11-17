// SettingsService.cs: settings stored through the save service, never PlayerPrefs directly
using UnityEngine;

public class SettingsService : ISettingsService
{
    const string SoundKey = "soundOn";
    const string MusicKey = "musicOn";
    const string SfxKey = "sfxOn";
    const string VibrationKey = "vibrationOn";

    readonly ISaveService save;

    public SettingsService(ISaveService save)
    {
        this.save = save;
        ApplySound();                                // correct from the first scene
    }

    public bool SoundOn
    {
        get => GetBool(SoundKey);
        set { SetBool(SoundKey, value); ApplySound(); }
    }

    public bool MusicOn
    {
        get => GetBool(MusicKey);
        set => SetBool(MusicKey, value);             // read by music players when they exist
    }

    public bool SfxOn
    {
        get => GetBool(SfxKey);
        set => SetBool(SfxKey, value);               // read by sound effects when they exist
    }

    public bool VibrationOn
    {
        get => GetBool(VibrationKey);
        set => SetBool(VibrationKey, value);
    }

    public void Vibrate()
    {
        if (!VibrationOn) return;
#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
    }

    bool GetBool(string key) => save.GetInt(key, 1) == 1;    // every setting defaults to on
    void SetBool(string key, bool value) => save.SetInt(key, value ? 1 : 0);

    void ApplySound() => AudioListener.volume = SoundOn ? 1f : 0f;
}