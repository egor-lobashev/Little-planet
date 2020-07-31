using UnityEngine;

public class Particle_controller : MonoBehaviour
{
    public float dust_time;
    public ParticleSystem trace, front, dust;
    private Telomere telomere;
    private bool spawned_dust = false;

    void Start()
    {
        front = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        trace = transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        dust = transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();

        Sprite source_metor = transform.parent.gameObject.GetComponent<SpriteRenderer>().sprite;

        float mass = transform.parent.gameObject.GetComponent<Meteor_damage>().mass;

        var shape = front.shape;
        shape.sprite = source_metor;
        Vector3 parent_scale = transform.parent.localScale;
        shape.scale = new Vector3(parent_scale.x * shape.scale.x, parent_scale.y * shape.scale.y,
            parent_scale.z * shape.scale.z);
        shape.rotation -= transform.localEulerAngles;
        
        var emission = front.emission;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(mass * emission.rateOverTime.constantMax);

        shape = dust.shape;
        shape.sprite = source_metor;

        telomere = transform.parent.gameObject.GetComponent<Telomere>();
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
        }
    }
}
