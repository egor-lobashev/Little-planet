using UnityEngine;

public class Audio_controller : MonoBehaviour
{
    public AudioSource falling, explode, small_explode, small_stones, damage, fire_damage;
    public float falling_range, explode_range, small_explode_range, small_stones_delay;
    private bool set_parameters = false, first_collision = true;

    public static float last_start_of_small_stones = 0;

    void Update()
    {
        if (!set_parameters && transform.parent != null)
        {
            set_parameters = true;

            float mass = transform.parent.gameObject.GetComponent<Meteor_damage>().mass;
            falling.volume += mass * falling_range;
            explode.volume += mass * explode_range;
            small_explode.volume += mass * small_explode_range;
        }
    }

    public void Fall_sound(Collision2D other)
    {
        if (other.gameObject.tag == "Meteor")
            small_explode.Play();

        explode.Play();
    }

    public void Collision_sound(Collision2D other, float received_damage)
    {
        float since_last_small_stones = Time.time - last_start_of_small_stones;
        
        if ((other.gameObject.tag == "Meteor") &&
            !first_collision &&
            (since_last_small_stones >= small_stones_delay))
        {
            small_stones.Play();
            last_start_of_small_stones = Time.time;
        }

        if ((other.gameObject.tag == "Player") && (received_damage >= Health.min_damage))
        {
            damage.Play();
        }

        if (first_collision)
        {
            first_collision = false;
        } 
    }

    public void Fire_damage_sound()
    {
        fire_damage.Play();
    }
}
