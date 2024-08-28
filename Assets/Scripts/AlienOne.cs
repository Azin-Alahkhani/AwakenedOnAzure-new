using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienOne : NPC
{
    public override void Interact(TextAsset inkJSON)
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        
    }

  
}
