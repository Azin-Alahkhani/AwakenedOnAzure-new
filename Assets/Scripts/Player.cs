using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public int[] inventoryItems;

    void Start()
    {
        LoadPlayer();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;

            health = data.health;
            inventoryItems = data.inventoryItems;
        }
    }
}
