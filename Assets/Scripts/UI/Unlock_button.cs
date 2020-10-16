using System.Collections.Generic;
using UnityEngine;

public class Unlock_button : MonoBehaviour
{
    public int stars_required;
    public GameObject locked_image, unlocked_image, star, text;

    void OnEnable()
    {
        if (PlayerPrefs.GetInt("stars") >= stars_required)
        {
            GetComponent<UnityEngine.UI.Button>().interactable = true;
            locked_image.SetActive(false);
            star.SetActive(false);
            text.SetActive(false);
            if (name == "Golden skin")
            {
                text.SetActive(false);
            }
            unlocked_image.GetComponent<UnityEngine.UI.Image>().enabled = true;
        }
        else
        {
            text.GetComponent<UnityEngine.UI.Text>().text = stars_required.ToString();
        }
    }
}