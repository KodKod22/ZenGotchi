using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFlow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Play()
    {
        SceneManager.LoadScene("Hoops");
    }
    public void Back()
    {
        SceneManager.LoadScene("Home");

    }
}
