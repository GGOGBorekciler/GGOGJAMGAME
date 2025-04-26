using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private NoteSpawner noteSpawner;

    void Start()
    {
        noteSpawner = FindObjectOfType<NoteSpawner>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                HoldNote holdNote = hit.collider.GetComponent<HoldNote>();
                if (holdNote != null)
                {
                    holdNote.StartHolding();
                    return;
                }

                BrokenNote brokenNote = hit.collider.GetComponent<BrokenNote>();
                if (brokenNote != null)
                {
                    Debug.Log("Clicked a broken note! Game Over!");
                    // Game over logic
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0); // Example: reload the scene
                    return;
                }
            }
        }

        if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            if (noteSpawner != null)
            {
                foreach (var holdNote in noteSpawner.activeHoldNotes)
                {
                    holdNote.StopHolding();
                }
            }
        }
    }
}
