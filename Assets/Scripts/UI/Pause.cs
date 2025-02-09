﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pause_screen;

    void Start()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
	    AudioListener.pause = false;
    }

    void Update()
    {
        if ((Time.timeScale > 0) && (Input.GetKeyDown(KeyCode.Escape)))
        {
            Set_pause();
        }
    }

    public void Set_pause()
    {
        Cursor.visible = true;
            Time.timeScale = 0;
            pause_screen.SetActive(true);
            AudioListener.pause = true;
    }

    public void Pause_finish()
    {
        GetComponent<AudioSource>().Play();
        Cursor.visible = false;
        Time.timeScale = 1;
        pause_screen.SetActive(false);
        AudioListener.pause = false;
    }

    public void Quit_to_menu()
    {
        GetComponent<AudioSource>().Play();
        int stars = PlayerPrefs.GetInt("stars");
        PlayerPrefs.SetInt("stars", stars + Star_counter.stars);
        
        SceneManager.LoadScene(0);
    }

    public void Save_record()
    {
        GameObject record = transform.GetChild(1).GetChild(4).gameObject;
        if (!record.activeInHierarchy)
            return;
        
        List<int> records = Menu_functions.Read_records(gameObject.scene.name);
        List<string> record_names = Menu_functions.Read_record_names(gameObject.scene.name);
        for (int i=0; i < records.Count + 1; i++)
        {
            if ((i == records.Count) || (Time_show.Time_sec() > records[i]))
            {
                string name = record.transform.GetChild(2).gameObject.GetComponent<UnityEngine.UI.Text>().text;
                    // not GetChild(1), because the caret appears
                name = name == "" ? (Translator.language == "russian" ? "Космонавт" : "Cosmonaut") : name;

                records.Insert(i, Time_show.Time_sec());
                record_names.Insert(i, name);
                break;
            }
        }

        string planet = gameObject.scene.name;
        for (int i=0; (i < records.Count) && (i<5); i++)
        {
            PlayerPrefs.SetInt(planet + i.ToString(), records[i]);
            PlayerPrefs.SetString(planet + i.ToString() + "_name", record_names[i]);
        }
    }

    public void Retry()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
