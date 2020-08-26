using UnityEngine;
using UnityEngine.UI;

public class Button_scaler : MonoBehaviour
{
    public GameObject[] buttons;

    void Start()
    {
        foreach (GameObject button in buttons)
        {
            RectTransform child = (RectTransform)button.transform.GetChild(0);
            Rect original_size = child.gameObject.GetComponent<Image>().sprite.rect;
            float proportion = original_size.width / original_size.height;
            child.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, child.rect.height * proportion);

            float offset = 10 * child.rect.height / original_size.height;
            RectTransform boarder = (RectTransform)child.GetChild(0);
            boarder.offsetMin = new Vector2(-offset, -offset);
            boarder.offsetMax = new Vector2(offset, offset);
        }
    }
}
