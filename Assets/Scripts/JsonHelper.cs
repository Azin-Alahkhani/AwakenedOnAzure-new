using System;
using System.Collections.Generic;
using UnityEngine;

public static class JsonHelper
{
    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> riddlesFile;
    }

    public static List<T> FromJson<T>(string json)
    {
        Debug.Log("Attempting to deserialize JSON: " + json);

        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);

        if (wrapper == null)
        {
            Debug.LogError("Wrapper is null after deserialization.");
        }
        else if (wrapper.riddlesFile == null)
        {
            Debug.LogError("Wrapper's riddles list is null.");
        }
        else
        {
            Debug.Log("Deserialized riddles count: " + wrapper.riddlesFile.Count);
        }

        return wrapper?.riddlesFile ?? new List<T>();
    }

    public static string ToJson<T>(List<T> array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.riddlesFile = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(List<T> array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.riddlesFile = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }
}
