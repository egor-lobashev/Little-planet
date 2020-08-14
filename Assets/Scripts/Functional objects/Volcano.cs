using UnityEngine;

public class Volcano : MonoBehaviour
{
    public float warning_time, finish_time, delete_time, continious_damage, cooldown;
    private bool ready_to_attack = false, eruption_started = false, eruption_finished = false;
    private float timer = 0, start_time;

    private ParticleSystem particle_system;
    private Animator animator;
    public AudioSource fire_damage;

    void Start()
    {
        animator = GetComponent<Animator>();
        particle_system = GetComponent<ParticleSystem>();

        start_time = Time.time;
    }

    void FixedUpdate()
    {
        float local_time = Time.time - start_time;
        animator.SetFloat("time", local_time);

        if (local_time >= warning_time && !eruption_started)
        {
            eruption_started = true;
            particle_system.Play();
            GetComponent<BoxCollider2D>().enabled = true;
            transform.parent.GetChild(1).gameObject.SetActive(true);
        }

        if (local_time >= finish_time && !eruption_finished)
        {
            eruption_finished = true;
            GetComponent<BoxCollider2D>().enabled = false;
            transform.parent.GetChild(1).gameObject.SetActive(false);
        }

        if (local_time >= delete_time)
        {
            Object.Destroy(transform.parent.gameObject);
        }

        if (!ready_to_attack)
        {
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
            {
                ready_to_attack = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if (other.tag == "Player")
        {
            if (ready_to_attack)
            {
                other.GetComponent<Health>().Receive_damage(continious_damage);
                fire_damage.Play();
                timer = cooldown;
                ready_to_attack = false;
            }
        }
    }
}
