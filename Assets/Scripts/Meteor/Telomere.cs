using UnityEngine;

public class Telomere : MonoBehaviour
{
    public float life_time;

    void FixedUpdate()
    {
        life_time -= Time.fixedDeltaTime;

        if (life_time <= 0)
        {
            Object.Destroy(gameObject);
        }
    }
}
