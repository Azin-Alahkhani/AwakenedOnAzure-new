using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;
using Ink.Runtime;

public class RiddleManager : MonoBehaviour
{
    private List<Riddle> availableRiddles;
    private List<Riddle> usedRiddles;
    private List<Riddle> riddles = new List<Riddle>();
    public static RiddleManager instance;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadRiddlesFromFile("riddleFile.json");
    }
    private class Wrapper<T>
    {
        public List<T> riddlesFile;
    }

    private void LoadRiddlesFromFile(string filePath)
    {
        riddles = new List<Riddle>();
        string fullPath = System.IO.Path.Combine(Application.streamingAssetsPath, filePath);

        if (!File.Exists(fullPath))
        {
            Debug.LogError("File does not exist: " + fullPath);
            return;
        }

        string json = File.ReadAllText(fullPath);
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("JSON data is empty.");
            return;
        }

       // Debug.Log("JSON Read: " + json);

        try
        {
            var jsonNode = JSON.Parse(json);
            var riddleArray = jsonNode["riddlesFile"].AsArray;
            
            foreach (var jsonRiddle in riddleArray)
            {
                
                RiddleData data = new RiddleData
                {
                    textQuestion = jsonRiddle.Value["textQuestion"],
                    textAnswers = ConvertJSONArrayToList(jsonRiddle.Value["textAnswers"].AsArray),
                    correctAnswerIndex = jsonRiddle.Value["correctAnswerIndex"].AsInt
                };

                Riddle riddle = new Riddle(data);
                riddles.Add(riddle);
            }

            availableRiddles = new List<Riddle>(riddles);
            Debug.Log("Deserialized riddles count: " + riddles.Count);

            foreach (var rid in riddles)
            {
                if (rid != null)
                {
                    availableRiddles.Add(rid);
                   // Debug.Log("Question added: " + rid.TextQuestion);
                }
                else
                {
                    Debug.LogWarning("Found a null riddle in the list.");
                }
            }

            usedRiddles = new List<Riddle>();
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Failed to parse JSON: " + ex.Message);
        }


    }
    private List<string> ConvertJSONArrayToList(JSONArray jsonArray)
    {
        List<string> list = new List<string>();
        foreach (var node in jsonArray)
        {
            list.Add(node.Value);
        }
        return list;
    }
    public Riddle GetRandomRiddle()
    {
        if (availableRiddles.Count == 0)
        {
            Debug.Log("All riddles have been used.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, availableRiddles.Count);
        Riddle selectedRiddle = availableRiddles[randomIndex];

        availableRiddles.RemoveAt(randomIndex);
        usedRiddles.Add(selectedRiddle);

        return selectedRiddle;
    }
}
