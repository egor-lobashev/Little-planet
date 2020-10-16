using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cosmonaut_color : MonoBehaviour
{
    public GameObject unlocked_image;
    public UnityEngine.U2D.SpriteAtlas cosmonaut_avatars;

    void Update()
    {
        if (name == "Golden skin")
            if (Controller.golden)
            {
                unlocked_image.GetComponent<UnityEngine.UI.Image>().sprite = cosmonaut_avatars.GetSprite("Cosmonaut_golden_0");
            }
            else
            {
                unlocked_image.GetComponent<UnityEngine.UI.Image>().sprite = cosmonaut_avatars.GetSprite("right");
            }
    }
}
