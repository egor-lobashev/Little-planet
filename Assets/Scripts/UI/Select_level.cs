using UnityEngine;
﻿using UnityEngine.SceneManagement;

public class Select_level : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        else if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Pluto");
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene("Saturn");
        }

        else if (Input.GetKeyDown(KeyCode.V))
        {
            SceneManager.LoadScene("Venus");
        }
    }
}
