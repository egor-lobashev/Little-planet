using UnityEngine;

public class Star_counter : MonoBehaviour
{
    public Vector2[] bonuses;
    public static int stars;
    private int stage = 0;
    private float timer = 0;

    void Start()
    {
        stars = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= bonuses[stage].x)
        {
            stars += (int)bonuses[stage].y;
            GetComponent<UnityEngine.UI.Text>().text = stars.ToString();
            timer = 0;

            if (stage < bonuses.Length - 1)
                stage++;
        }
    }
}
