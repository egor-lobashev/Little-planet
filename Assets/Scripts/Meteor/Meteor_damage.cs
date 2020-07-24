using UnityEngine;

public class Meteor_damage : MonoBehaviour
{
    public float momental_damage, continious_damage, cooldown, dark_time, smooth_time;
    private Rigidbody2D rb;
    private Animator animator;
    private Telomere telomere;
    private float timer = 0;
    private bool ready_to_attack = true, hot = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        telomere = GetComponent<Telomere>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Health>().Receive_damage(
                momental_damage * rb.velocity.magnitude * rb.mass);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (hot && (other.gameObject.tag == "Player"))
        {
            if (ready_to_attack)
            {
                other.gameObject.GetComponent<Health>().Receive_damage(continious_damage);
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