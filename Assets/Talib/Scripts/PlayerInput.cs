using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                HoldNote holdNote = hit.collider.GetComponent<HoldNote>();
                if (holdNote != null)
                {
                    holdNote.StartHolding();
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) // Mouse released
        {
            HoldNote[] holdNotes = FindObjectsOfType<HoldNote>();
            foreach (var holdNote in holdNotes)
            {
                holdNote.StopHolding();
            }
        }
    }
}