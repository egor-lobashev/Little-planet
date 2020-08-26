using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pause_screen;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pause_screen.SetActive(true);
        }
    }

    public void Pause_finish()
    {
        Time.timeScale = 1;
        pause_screen.SetActive(false);
    }

    public void Quit_to_menu()
    {
        SceneManager.LoadScene(0);
    }
}
