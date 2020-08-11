using UnityEngine;

public class Cloud_Venus : MonoBehaviour
{
    public float push_force;
    private ParticleSystem particle_system;
    
    void Start()
    {
        particle_system = GetComponent<ParticleSystem>();

        uint seed = (uint)(Random.value * 1000000);
        particle_system.randomSeed = seed;

        GameObject cloud_light = transform.GetChild(0).gameObject;
        cloud_light.GetComponent<ParticleSystem>().randomSeed = seed;

        particle_system.Play();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if ((other.tag == "Meteor") && other.GetComponent<Rigidbody2D>().isKinematic)
        {
            float phi = transform.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 other_velocity = other.GetComponent<Rigidbody2D>().velocity;
            float direction = Mathf.Sign(Vector2.Dot(other_velocity, new Vector2(Mathf.Cos(phi), Mathf.Sin(phi))));

            transform.parent.gameObject.GetComponent<Rigidbody2D>().angularVelocity -=
                direction * push_force * other.GetComponent<Meteor_damage>().mass;
        }
    }
}
