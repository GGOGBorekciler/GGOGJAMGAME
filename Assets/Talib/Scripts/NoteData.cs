[System.Serializable]
public class NoteData
{
    public float time;
    public string key;
    public string type;
    public float holdDuration; // Only for "hold" type
}