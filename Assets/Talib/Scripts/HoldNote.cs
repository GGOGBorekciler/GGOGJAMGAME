using UnityEngine;

public class HoldNote : MonoBehaviour
{
    public float speed = 5f;
    private float holdDuration;
    private bool isHolding = false;
    private float holdTimer = 0f;

    private bool isInHoldZone = false;

    public void SetHoldDuration(float duration)
    {
        holdDuration = duration;
        Vector3 scale = transform.localScale;
        scale.y += holdDuration; // Stretch vertically based on hold duration
        transform.localScale = scale;
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (isHolding)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= holdDuration)
            {
                Debug.Log("Hold completed successfully!");
                Destroy(gameObject); // Remove the note after successful hold
            }
        }
    }

    public void StartHolding()
    {
        if (isInHoldZone)
        {
            isHolding = true;
            Debug.Log("Started Holding");
        }
    }

    public void StopHolding()
    {
        if (isHolding)
        {
            if (holdTimer >= holdDuration)
            {
                Debug.Log("Hold finished properly!");
            }
            else
            {
                Debug.Log("Hold failed! Released too early!");
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitZone"))
        {
            isInHoldZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HitZone"))
        {
            isInHoldZone = false;
            if (!isHolding)
            {
                Destroy(gameObject); // Missed hold
            }
        }
    }
}