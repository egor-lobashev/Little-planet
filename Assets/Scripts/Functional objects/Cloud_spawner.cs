using UnityEngine;

public class Cloud_spawner : MonoBehaviour
{
    public float sapwn_period, sapwn_period_range, first_period;
    public GameObject cloud_prefab;
    private float timer;

    void Start()
    {
        timer = first_period;
    }

    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            timer = sapwn_period + (2*Random.value - 1) * sapwn_period_range;
            Spawn_cloud();
        }
    }

    void Spawn_cloud()
    {
        GameObject cloud = Object.Instantiate(cloud_prefab);
        cloud.transform.eulerAngles = new Vector3(0, 0, Random.value * 360);
        cloud.transform.position = new Vector3(0, 0, 10);
    }
}
