using UnityEngine;

public class Meteor_spawner : MonoBehaviour
{
    public float sapwn_period, velocity, spawn_radius, min_size, angular_drag, life_time, life_time_range,
        explode_force, explode_radius, momental_damage, continious_damage, cooldown, dark_time, smooth_time;
    public RuntimeAnimatorController controller;
    public UnityEngine.U2D.SpriteAtlas dark_meteors;
    private float timer;

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (Random.value * timer >= sapwn_period)
        {
            timer = 0;
            Spawn_meteor();
        }
    }

    void Spawn_meteor()
    {
        int meteor_kind = (int)(Random.value * 34f);
        meteor_kind = meteor_kind<=33 ? meteor_kind : 0;
        GameObject meteor = (GameObject)Object.Instantiate(Resources.Load("meteor_" + meteor_kind.ToString()));

        GameObject dark_meteor = new GameObject();
        dark_meteor.AddComponent<SpriteRenderer>();
        dark_meteor.GetComponent<SpriteRenderer>().sprite = dark_meteors.GetSprite("dark_meteors_" + meteor_kind.ToString());
        dark_meteor.transform.SetParent(meteor.transform);
        dark_meteor.transform.position = new Vector3(meteor.transform.position.x, meteor.transform.position.y, 6);

        float phi = Random.value * 2 * Mathf.PI;
        meteor.transform.position = new Vector3(spawn_radius * Mathf.Cos(phi), spawn_radius * Mathf.Sin(phi), 5);

        float scale = Random.value*(1-min_size) + min_size;
        meteor.transform.localScale = new Vector2(scale, scale * (Random.value < 0.5 ? -1 : 1));
        meteor.transform.eulerAngles = new Vector3(0, 0, Random.value * 360);

        meteor.AddComponent<Rigidbody2D>();
        meteor.GetComponent<Rigidbody2D>().velocity = -meteor.transform.position.normalized * velocity;
        meteor.GetComponent<Rigidbody2D>().isKinematic = true;
        meteor.GetComponent<Rigidbody2D>().useAutoMass = true;
        meteor.GetComponent<Rigidbody2D>().angularDrag = angular_drag;

        meteor.AddComponent<Telomere>();
        meteor.GetComponent<Telomere>().life_time = life_time + (Random.value-0.5f)*life_time_range;

        meteor.AddComponent<Explode_falling>();
        meteor.GetComponent<Explode_falling>().explode_force = explode_force;
        meteor.GetComponent<Explode_falling>().explode_radius = explode_radius;

        meteor.AddComponent<Meteor_damage>();
        meteor.GetComponent<Meteor_damage>().momental_damage = momental_damage;
        meteor.GetComponent<Meteor_damage>().continious_damage = continious_damage;
        meteor.GetComponent<Meteor_damage>().cooldown = cooldown;
        meteor.GetComponent<Meteor_damage>().dark_time = dark_time;
        meteor.GetComponent<Meteor_damage>().smooth_time = smooth_time;

        meteor.AddComponent<Animator>();
        meteor.GetComponent<Animator>().runtimeAnimatorController = controller;
    }
}
