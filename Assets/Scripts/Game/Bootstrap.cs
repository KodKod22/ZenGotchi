using UnityEngine;
using UnityEngine.SceneManagement;

// The only script in the Bootstrap scene, the first scene in the Scene List.
public class Bootstrap : MonoBehaviour
{
    [SerializeField] EconomyConfig economy;  // drag Assets/Config/EconomyConfig here
    [SerializeField] HoopsConfig hoops;      // drag Assets/Config/HoopsConfig here

    void Awake()
    {
        DontDestroyOnLoad(gameObject);       // Services survives every scene change

        // Start every service here, in order
        Services.Save = new PlayerPrefsSaveService();
        Services.Config = new ScriptableObjectConfigService(economy, hoops); 
        Services.Settings = new SettingsService(Services.Save);
        Debug.Log("Bootstrap: services ready");
        SceneManager.LoadScene("Home");      // only now: the first screen
    }
}