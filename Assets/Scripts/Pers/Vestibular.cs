using UnityEngine;

public class Vestibular : MonoBehaviour
{
    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        float phi = Mathf.Atan(pos.x/pos.y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, pos.y>=0 ? -phi : 180-phi);
    }
}
