using System.Collections.Generic;
using UnityEngine;

public class RiddlePool
{
    private List<Riddle> riddles;

    public RiddlePool()
    {
        riddles = new List<Riddle>{};
    }

    public Riddle GetRandomRiddle()
    {
        int randomIndex = Random.Range(0, riddles.Count);
        return riddles[randomIndex];
    }
}
