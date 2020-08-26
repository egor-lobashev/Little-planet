using UnityEngine;

public class Time_show : MonoBehaviour
{
    private UnityEngine.UI.Text text_component;
    public static float start_time;

    void Start()
    {
        text_component = GetComponent<UnityEngine.UI.Text>();
        start_time = Time.time;
    }

    void Update()
    {
        text_component.text = Time_min_sec();
    }

    static public string Time_min_sec()
    {
        int time = (int)(Time.time - start_time);
        int minutes = time/60;
        int seconds = time%60;
        return minutes.ToString() + (seconds>=10 ? ":" : ":0") + seconds.ToString();
    }
}
