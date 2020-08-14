using UnityEngine;

public class Rain : MonoBehaviour
{
    public float time_before_rain, rain_duration;
    private bool rain_is_going = false, rain_is_finished = false;
    private float last_second = 1f;
    public AudioSource rain_sound, extinguish;

    void FixedUpdate()
    {
        if (!rain_is_going && !rain_is_finished)
        {
            time_before_rain -= Time.fixedDeltaTime;
            if (time_before_rain <= 0)
            {
                rain_is_going = true;
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<ParticleSystem>().Play();
                if (rain_sound != null)
                    rain_sound.Play();
            }
        }
        else if (!rain_is_finished)
        {
            rain_duration -= Time.fixedDeltaTime;
            if (rain_duration <= 0)
            {
                GetComponent<ParticleSystem>().Stop();
                rain_is_going = false;
                GetComponent<BoxCollider2D>().enabled = false;
                rain_is_finished = true;
                transform.parent.GetChild(0).gameObject.GetComponent<Animator>().SetBool("exit", true);
            }
        }
        else
        {
            last_second -= Time.fixedDeltaTime;
            if (last_second <= 0)
            {
                Object.Destroy(transform.parent.gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if ((other.tag == "Meteor") && other.GetComponent<Meteor_damage>().hot == true)
        {
            other.GetComponent<Meteor_damage>().hot = false;
            other.GetComponent<Animator>().SetBool("extinguished", true);
            extinguish.Play();
        }
    }
}
