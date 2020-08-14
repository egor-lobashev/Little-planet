using UnityEngine;

public class Snow : MonoBehaviour
{
    public GameObject snow;
    public float snow_time;
    public UnityEngine.U2D.SpriteAtlas snow_sprites;

    private float not_moving;
    private bool already_snowed = false;
    private Rigidbody2D rb;
    private BoxCollider2D box_collider;

    void Start()
    {
        rb = transform.parent.gameObject.GetComponent<Rigidbody2D>();
        box_collider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        if (!box_collider.enabled)
            return;

        not_moving += Time.fixedDeltaTime;

        if ((not_moving >= snow_time) && !already_snowed)
        {
            Summon_snow();
            already_snowed = true;
            not_moving = 0;
        }

        if (rb.angularVelocity != 0)
        {
            not_moving = 0;
            already_snowed = false;
        }
    }

    void Summon_snow()
    {
        GameObject new_snow = Object.Instantiate(snow);

        int number = (int)(Random.value * snow_sprites.spriteCount);
        number = number < snow_sprites.spriteCount ? number : 0;

        new_snow.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite =
            snow_sprites.GetSprite("snow_" + number.ToString());
        new_snow.transform.rotation = transform.rotation;
    }
}
