using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{


    [SerializeField] private KeyCode LineKey ;
    [SerializeField] private TextMeshProUGUI scoreText;

    private Transform Note;

    private float score ;
    // Start is called before the first frame update
    void Start()
    {
        score = 0f;
        Note = null;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
        if (Note == null){
            return;
        }
        else{
            if  (Input.GetKey(LineKey)){
                score += 10f;
                Destroy(Note.gameObject);
                Note = null;
            }
        }

    }

     private void  OnTriggerEnter2D (Collider2D other)
    {
        if(other.CompareTag("Note")) 
        Note = other.transform;
    }

    private void  OnTriggerExit2D (Collider2D other)
    {
        if(other.CompareTag("Note")) 
        Note = null;
    }
}
