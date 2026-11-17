// ISettingsService.cs: what the game can ask about player settings
public interface ISettingsService
{
    bool SoundOn { get; set; }       // master switch for all audio
    bool MusicOn { get; set; }
    bool SfxOn { get; set; }
    bool VibrationOn { get; set; }

    void Vibrate();                  // vibrates only if VibrationOn
}