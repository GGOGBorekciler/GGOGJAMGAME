using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject normalNotePrefab;
    public GameObject holdNotePrefab;
    public GameObject quickNotePrefab;
    public GameObject brokenNotePrefab;
    public TextAsset noteMapJson;
    public Transform canvas;

    [Header("Lane Settings")]
    public float laneSpacing = 270f;
    public float spawnHeight = 2000f;
    public List<HoldNote> activeHoldNotes = new List<HoldNote>(); // <<< ADD THIS

    private List<NoteData> notes = new List<NoteData>();
    private float songTime;
    private int nextNoteIndex = 0;

    private Dictionary<string, int> keyToLane = new Dictionary<string, int>
    {
        { "C", 0 },
        { "D", 1 },
        { "E", 2 },
        { "G", 3 }
    };

    void Start()
    {
        LoadNotes();
    }

    void Update()
    {
        songTime += Time.deltaTime;

        while (nextNoteIndex < notes.Count && songTime >= notes[nextNoteIndex].time)
        {
            SpawnNote(notes[nextNoteIndex]);
            nextNoteIndex++;
        }
    }

    void LoadNotes()
    {
        notes = new List<NoteData>(JsonHelper.FromJson<NoteData>(noteMapJson.text));
    }

    void SpawnNote(NoteData data)
    {
        GameObject prefabToSpawn = GetPrefabForType(data.type);
        if (prefabToSpawn == null) return;
        
        
        if(nextNoteIndex > 0) {
            if(notes[nextNoteIndex-1].key == data.key) {
            
            if(data.key == "E") {
                data.key = "G";
            } else if(data.key == "D")  {
                data.key = "E";
            } else if(data.key == "C") {
                data.key = "D";
            } else if(data.key == "G"){
                data.key = "E";
            }
        }
        }

        int lane = keyToLane.ContainsKey(data.key) ? keyToLane[data.key] : 0;
        Vector3 spawnPosition = new Vector3(200 + lane * laneSpacing, spawnHeight, 0);
        GameObject note = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, canvas);

        if (data.type == "hold")
        {
            HoldNote holdNote = note.GetComponent<HoldNote>();
            if (holdNote != null)
                holdNote.SetHoldDuration(data.holdDuration);
        }
    }

    GameObject GetPrefabForType(string type)
    {
        switch (type)
        {
            case "normal": return normalNotePrefab;
            case "hold": return holdNotePrefab;
            case "quick": return quickNotePrefab;
            case "broken": return brokenNotePrefab;
            default: return null;
        }
    }
}