using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_functions : MonoBehaviour
{
    public List<GameObject> menu_history;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    public void Load_level(int number)
    {
        SceneManager.LoadScene(number);
    }

    public void Open_menu(GameObject next)
    {
        int depth = menu_history.Count;

        next.SetActive(true);
        menu_history[depth - 1].SetActive(false);

        menu_history.Add(next);
    }

    public void Back()
    {
        int depth = menu_history.Count;
        if (depth > 1)
        {
            menu_history[depth - 2].SetActive(true);
            menu_history[depth - 1].SetActive(false);

            menu_history.RemoveAt(depth - 1);
        }
        else
        {
            Application.Quit();
        }
    }
}