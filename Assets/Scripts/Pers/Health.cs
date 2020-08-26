using UnityEngine;
﻿using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    
    public float HP_max;
    private float HP;
    private bool game_over_msg = false, damaged_right_now = false;
    private Animator animator;

    void Start()
    {
        HP = HP_max;
        animator = GetComponent<Animator>();
    }

    public float Show_HP()
    {
        return HP;
    }

    public void Receive_damage(float damage, bool continious = false)
    {
        if ((damage < 1) && !continious)
            return;
            
        HP -= damage;
        damaged_right_now = true;

        if (HP <= 0)
        {
            HP = 0;
            if (!game_over_msg)
            {
                Debug.Log("Game Over. Result: " + Time_show.Time_min_sec());
		SceneManager.LoadScene("Menu");
                game_over_msg = true;
            }
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
}
