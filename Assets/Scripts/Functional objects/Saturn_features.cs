using UnityEngine;

public class Saturn_features : MonoBehaviour
{
    void Start()
    {
        float phi = (Random.value - 0.5f) * 180;
        transform.parent.eulerAngles = new Vector3(0, 0, phi);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 prev = other.gameObject.transform.position;
            other.gameObject.transform.position = new Vector3(prev.x, prev.y, -2);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 prev = other.gameObject.transform.position;
            other.gameObject.transform.position = new Vector3(prev.x, prev.y, 16);
        }
    }
}
