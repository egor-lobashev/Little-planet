using UnityEngine;

public class Star_show : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt("stars").ToString();
    }
}
