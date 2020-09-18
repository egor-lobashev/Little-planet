using UnityEngine.UI;
using UnityEngine;

public class Star_scaler : MonoBehaviour
{
    void Start()
    {
        RectTransform rect_transform = GetComponent<RectTransform>();
        Rect original_size = GetComponent<Image>().sprite.rect;
        float proportion = original_size.width / original_size.height;
        rect_transform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, rect_transform.rect.height * proportion);
    }
}
