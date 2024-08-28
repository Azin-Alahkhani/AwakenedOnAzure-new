using System.Collections.Generic;
using UnityEngine;

public class Riddle
{
    public string TextQuestion { get;  set; }
    public List<string> TextAnswers { get;  set; }
    public Sprite ImageQuestion { get; set; }
    public List<Sprite> ImageAnswers { get; set; }
    public int CorrectAnswerIndex { get; set; }

    public Riddle(RiddleData data)
    {
        if (!string.IsNullOrEmpty(data.textQuestion))
        {
            TextQuestion = data.textQuestion;
            TextAnswers = data.textAnswers;
        }
        //else if (!string.IsNullOrEmpty(data.imageQuestion))
        //{
        //    ImageQuestion = Resources.Load<Sprite>(data.imageQuestion);
        //    ImageAnswers = new List<Sprite>();
        //    foreach (var imageAnswer in data.imageAnswers)
        //    {
        //        ImageAnswers.Add(Resources.Load<Sprite>(imageAnswer));
        //    }
        //}

        CorrectAnswerIndex = data.correctAnswerIndex;
    }

    public bool IsCorrectAnswer(int index)
    {
        return index == CorrectAnswerIndex;
    }
}

[System.Serializable]
public class RiddleData
{
    public string textQuestion;
    public List<string> textAnswers;
    //public string imageQuestion;
    //public List<string> imageAnswers;
    public int correctAnswerIndex;
}
