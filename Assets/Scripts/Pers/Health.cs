using UnityEngine;

public class Health : MonoBehaviour
{
    
    public float HP_max;
    private float HP;
    private bool game_over_msg = false;

    void Start()
    {
        HP = HP_max;
    }

    public float Show_HP()
    {
        return HP;
    }

    public void Receive_damage(float damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            HP = 0;
            if (!game_over_msg)
            {
                Debug.Log("Game Over. Result: " + Time_show.Time_min_sec());
                Application.Quit();
                game_over_msg = true;
            }
        }
    }
}
