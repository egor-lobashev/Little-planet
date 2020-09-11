using UnityEngine;

public class Cloud_spawner : MonoBehaviour
{
    public float sapwn_period, sapwn_period_range, sapwn_period_min, first_period, growth_speed,
        finish_time, finish_time_max = 16f;
    public GameObject cloud_prefab;
    private float sapwn_period_range_0, sapwn_period_0, finish_time_0;
    private float timer, sapwn_period_multiplier = 1f;

    void Start()
    {
        timer = first_period;
        
        sapwn_period_0 = sapwn_period;
        sapwn_period_range_0 = sapwn_period_range;
        finish_time_0 = finish_time;
    }

    void FixedUpdate()
    {
        if ((growth_speed > 0) && (sapwn_period > sapwn_period_min))
        {
            sapwn_period = 1 / (1/sapwn_period_0 + (Time.time - Time_show.start_time)*growth_speed);
            sapwn_period_multiplier = sapwn_period/sapwn_period_0;
            
            sapwn_period_range = sapwn_period_range_0 * sapwn_period_multiplier;
            finish_time = finish_time_0 * sapwn_period_multiplier;
            finish_time = finish_time > finish_time_max ? finish_time_max : finish_time;
        }

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
        cloud.transform.position = new Vector3(0, 0, 0);
	if (growth_speed > 0)
	{
        	cloud.transform.GetChild(0).gameObject.GetComponent<Volcano>().finish_time = finish_time;
		var main = cloud.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().main;
		main.duration = finish_time - 3f;
	}
    }
}
