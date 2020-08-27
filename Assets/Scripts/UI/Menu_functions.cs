using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_functions : MonoBehaviour
{
    public List<GameObject> menu_history;
    public GameObject records_times, records_names, records_planets;
    public static int records_count = 5;
    private static bool from_game = false;

    void Start()
    {
        if (from_game)
        {
            from_game = false;
            Open_menu(transform.GetChild(1).gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }

    public void Load_level(int number)
    {
        from_game = true;
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

    public static List<int> Read_records(string planet)
    {
        List<int> ans = new List<int>();

        for (int i = 0; i < records_count; i++)
        {
            if (PlayerPrefs.HasKey(planet + i.ToString()))
            {
                ans.Add(PlayerPrefs.GetInt(planet + i.ToString()));
            }
        }

        return ans;
    }

    public static List<string> Read_record_names(string planet)
    {
        List<string> ans = new List<string>();

        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey(planet + i.ToString()))
            {
                ans.Add(PlayerPrefs.GetString(planet + i.ToString() + "_name"));
            }
        }

        return ans;
    }

    public string Record_times_string(string planet)
    {
        string ans = "";
        List<int> records = Read_records(planet);

        for (int i=0; (i<5); i++)
        {
            if (i < records.Count)
            {
                int time = records[i];
                int minutes = time/60;
                int seconds = time%60;
                string min_sec = minutes.ToString() + (seconds>=10 ? ":" : ":0") + seconds.ToString();

                ans += min_sec;
            }
            ans += "\n";
        }

        return ans;
    }

    public string Record_names_string(string planet)
    {
        string ans = "";
        List<string> record_names = Read_record_names(planet);

        for (int i=0; (i<5); i++)
        {
            if (i < record_names.Count)
            {
                ans += record_names[i];
            }
            ans += "\n";
        }

        return ans;
    }

    public void Set_records(string planet)
    {
        if (planet == "all")
        {
            string record_times_string = "", record_names_string = "", record_planets_string = "";

            List<int>[] all_records_times = new List<int>[6];
            List<string>[] all_records_names = new List<string>[6];
            string[] planets = {"Mercury", "Neptune", "Mars", "Venus", "Saturn", "Pluto"};

            for (int i=0; i<6; i++)
            {
                all_records_times[i] = Read_records(planets[i]);
                all_records_names[i] = Read_record_names(planets[i]);
            }

            for (int i=0; (i<5); i++)
            {
                int max = -1;
                int max_index = -1;

                for (int j=0; (j<6); j++)
                {
                    if (all_records_times[j].Count > 0)
                    {
                        if (all_records_times[j][0] > max)
                        {
                            max = all_records_times[j][0];
                            max_index = j;
                        }
                    }
                }

                if (max_index != -1)
                {
                    int time = all_records_times[max_index][0];
                    all_records_times[max_index].RemoveAt(0);

                    int minutes = time/60;
                    int seconds = time%60;
                    string min_sec = minutes.ToString() + (seconds>=10 ? ":" : ":0") + seconds.ToString();

                    record_times_string += min_sec;


                    record_names_string += all_records_names[max_index][0];
                    all_records_names[max_index].RemoveAt(0);


                    record_planets_string += planets[max_index];
                }

                record_times_string += "\n";
                record_names_string += "\n";
                record_planets_string += "\n";
            }

            records_times.GetComponent<UnityEngine.UI.Text>().text = record_times_string;
            records_names.GetComponent<UnityEngine.UI.Text>().text = record_names_string;
            records_planets.GetComponent<UnityEngine.UI.Text>().text = record_planets_string;
        }

        else
        {
            records_times.GetComponent<UnityEngine.UI.Text>().text = Record_times_string(planet);
            records_names.GetComponent<UnityEngine.UI.Text>().text = Record_names_string(planet);
            records_planets.GetComponent<UnityEngine.UI.Text>().text = "";
        }
    }
}