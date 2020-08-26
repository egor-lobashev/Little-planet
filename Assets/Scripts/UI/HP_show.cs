using UnityEngine;

public class HP_show : MonoBehaviour
{
    public GameObject player;
    private Health health;
    private RectTransform rect_transform;
    private float HP_max;

    void Start()
    {
        health = player.GetComponent<Health>();
        HP_max = health.HP_max;
        rect_transform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float HP = health.Show_HP();
        HP = HP > 0 ? HP : 0;
        float delta_size = ((RectTransform)rect_transform.parent).rect.width * (1 - HP/HP_max);
        rect_transform.offsetMax = new Vector2(-delta_size, 0f);
    }
}
