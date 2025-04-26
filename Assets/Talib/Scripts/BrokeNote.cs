using UnityEngine;

public class BrokenNote : MonoBehaviour
{
    public float speed = 50f;

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}