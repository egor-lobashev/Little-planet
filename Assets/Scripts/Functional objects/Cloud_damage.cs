using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_damage : MonoBehaviour
{
    public float continious_damage, cooldown;
    private bool ready_to_attack = false;
    private float timer = 0;
    public AudioSource damage_sound;

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if (other.tag == "Player")
        {
            if (ready_to_attack)
            {
                other.GetComponent<Health>().Receive_damage(continious_damage);
                damage_sound.Play();
                timer = cooldown;
                ready_to_attack = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (!ready_to_attack)
        {
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
            {
                ready_to_attack = true;
            }
        }
    }
}
