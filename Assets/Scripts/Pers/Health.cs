using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    
    public float HP_max;
    public GameObject canvas;
    private GameObject main_screen, game_over_screen;
    private float HP;
    private bool damaged_right_now = false;
    private Animator animator;
    public static float min_damage = 1f;

    void Start()
    {
        HP = HP_max;
        animator = GetComponent<Animator>();

        main_screen = canvas.transform.GetChild(2).gameObject;
        game_over_screen = canvas.transform.GetChild(1).gameObject;
    }

    public float Show_HP()
    {
        return HP;
    }

    public void Receive_damage(float damage, bool continious = false)
    {
        if ((damage < min_damage) && !continious)
            return;
            
        HP -= damage;
        damaged_right_now = true;

        if (HP <= 0)
        {
            HP = 0;
            
            Game_over();
        }
    }

    void Update()
    {
        if (damaged_right_now == true)
        {
            damaged_right_now = false;
            animator.SetBool("damaged", true);
        }
        else
        {
            animator.SetBool("damaged", false);
        }
    }

    public void Game_over()
    {
        Time.timeScale = 0;
        main_screen.SetActive(false);
	AudioListener.pause = true;
        game_over_screen.SetActive(true);

        game_over_screen.transform.GetChild(2).GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text = 
            Time_show.Time_min_sec();
        
        List<int> records = Menu_functions.Read_records(gameObject.scene.name);
        if ((records.Count == 0) || records[0] < Time_show.Time_sec())
        {
            game_over_screen.transform.GetChild(4).gameObject.SetActive(true);
            game_over_screen.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text =
                "New record!";
        }
        else if ((records.Count < Menu_functions.records_count) ||
            (records[records.Count - 1] < Time_show.Time_sec()))
        {
            game_over_screen.transform.GetChild(4).gameObject.SetActive(true);
            game_over_screen.transform.GetChild(1).gameObject.GetComponent<UnityEngine.UI.Text>().text =
                "Good result!";
        }
        else
        {
            game_over_screen.transform.GetChild(4).gameObject.SetActive(false);
        }
    }
}
