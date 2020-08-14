using UnityEngine;

public class Neptune_features : MonoBehaviour
{
    public PhysicsMaterial2D ice;

    void Start()
    {
        GetComponent<CircleCollider2D>().sharedMaterial = ice;
    }
}
