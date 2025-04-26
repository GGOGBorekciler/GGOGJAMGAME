using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickNote : MonoBehaviour
{
    public float speed = 50f;

    void Update()   
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }
}
