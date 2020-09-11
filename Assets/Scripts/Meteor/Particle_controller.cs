using UnityEngine;

public class Particle_controller : MonoBehaviour
{
    public float dust_time;
    public ParticleSystem trace, front, dust;
    private Telomere telomere;
    private bool spawned_dust = false;
    private Vector3 default_direction;

    void Start()
    {
        front = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        trace = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        dust = transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();

        Sprite source_metor = transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite;

        float mass = transform.parent.gameObject.GetComponent<Meteor_damage>().mass;

        Vector3 parent_scale = transform.parent.localScale;

        var front_shape = front.shape;
        front_shape.sprite = source_metor;
        front_shape.scale = parent_scale;
        front_shape.rotation -= transform.localEulerAngles;
        
        var front_emission = front.emission;
        front_emission.rateOverTime = new ParticleSystem.MinMaxCurve(mass * front_emission.rateOverTime.constantMax);


        var dust_shape = dust.shape;
        dust_shape.sprite = source_metor;
        dust_shape.scale = parent_scale;
        dust_shape.rotation -= transform.localEulerAngles;

        var dust_emission = dust.emission;
        dust_emission.rateOverTime = new ParticleSystem.MinMaxCurve(mass * dust_emission.rateOverTime.constantMax);


        telomere = transform.parent.gameObject.GetComponent<Telomere>();

        default_direction = transform.eulerAngles;
    }

    void Update()
    {
        if (!spawned_dust && (telomere.life_time <= dust_time))
        {
            spawned_dust = true;
            dust.Play();
            dust.gameObject.transform.SetParent(null);
            dust.gameObject.AddComponent<Telomere>();
            dust.gameObject.GetComponent<Telomere>().life_time =
                dust.main.duration + dust.main.startLifetime.constantMax;
            dust.GetComponent<AudioSource>().Play();
            dust.GetComponent<Rigidbody2D>().velocity =
                transform.parent.gameObject.GetComponent<Rigidbody2D>().velocity;
        }

        transform.GetChild(1).eulerAngles = default_direction;
    }
}
