using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject normalNotePrefab;
    public GameObject holdNotePrefab;
    public GameObject quickNotePrefab;
    public TextAsset noteMapJson;

    [Header("Lane Settings")]
    public float laneSpacing = 2f;
    public float spawnHeight = 6f;
    
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

        int lane = keyToLane.ContainsKey(data.key) ? keyToLane[data.key] : 0;
        Vector3 spawnPosition = new Vector3(lane * laneSpacing, spawnHeight, 0);
        GameObject note = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

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
            default: return null;
        }
    }
}