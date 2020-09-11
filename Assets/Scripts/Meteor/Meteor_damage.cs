using UnityEngine;

public class Meteor_damage : MonoBehaviour
{
    public float momental_damage, continious_damage, cooldown, dark_time, smooth_time;
    public float mass;

    private Rigidbody2D rb;
    private Animator animator;
    private Audio_controller audio_controller;
    private Telomere telomere;

    private float timer = 0;

    private bool ready_to_attack = true;
    public bool hot = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.isKinematic = false;
        mass = rb.mass;
        rb.isKinematic = true;

        animator = GetComponent<Animator>();
        audio_controller = transform.GetChild(1).gameObject.GetComponent<Audio_controller>();
        telomere = GetComponent<Telomere>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        float received_damage = 0;

        if (other.gameObject.tag == "Player")
        {
            float velocity_projection = Vector2.Dot(rb.velocity,
                ((Vector2)(transform.position - other.transform.position)).normalized);
            received_damage = momental_damage * Mathf.Pow(velocity_projection, 2) * mass;
            other.gameObject.GetComponent<Health>().Receive_damage(received_damage);

            if (hot && other.gameObject.GetComponent<Controller>().ground_is_meteor &&
                other.gameObject.GetComponent<Controller>().grounded)
            {
                other.gameObject.GetComponent<Controller>().Jump();
            }
        }

        audio_controller.Collision_sound(other, received_damage);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (hot && (other.gameObject.tag == "Player"))
        {
            if (ready_to_attack)
            {
                other.gameObject.GetComponent<Health>().Receive_damage(continious_damage, true);
                audio_controller.Fire_damage_sound();
                timer = cooldown;
                ready_to_attack = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (hot && !ready_to_attack)
        {
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
            {
                ready_to_attack = true;
            }
        }
    }

    void Update()
    {
        if (hot && (telomere.life_time <= dark_time + smooth_time))
        {
            animator.SetBool("hot", false);
        }

        if (hot && (telomere.life_time <= dark_time))
        {
            hot = false;
        }
    }
}