using UnityEngine;

public class Audio_controller : MonoBehaviour
{
    public AudioSource falling, explode, small_explode, small_stones;
    public float falling_range, explode_range, small_explode_range;
    private bool set_parameters = false, first_collision = true;

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

    public void Fall_sound(string tag)
    {
        if (tag == "Meteor")
            small_explode.Play();

        explode.Play();
    }

    public void Collision_sound(string tag)
    {
        if (first_collision)
        {
            first_collision = false;
            return;
        } 

        if (tag == "Meteor")
        {
            small_stones.Play();
        }
    }
}
