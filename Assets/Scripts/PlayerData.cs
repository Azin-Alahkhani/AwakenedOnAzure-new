[System.Serializable]
public class PlayerData
{
    public float[] position;
    public int health;
    public int[] inventoryItems;

    public PlayerData(Player player)
    {
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        health = player.health;
        inventoryItems = player.inventoryItems;
    }
}
