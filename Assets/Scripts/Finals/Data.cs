[System.Serializable]
public class PlayerPosition
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class Data 
{
    public string playerName;
    public int playerHealth;
    public int score;
    public PlayerPosition playerPosition;
}
