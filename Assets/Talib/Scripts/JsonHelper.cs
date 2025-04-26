using System;
using UnityEngine;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(WrapJson(json));
        return wrapper.notes;
    }

    private static string WrapJson(string json)
    {
        return "{\"notes\":" + json + "}";
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] notes;
    }
}