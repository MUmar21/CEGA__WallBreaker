using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public int LevelNumber;
    public Difficulty Difficulty;
    public string Instructions;
    public int MaxTaps;
    public bool IsCompleted;
    public bool IsLocked;

    public int GetLevelNumber()
    {
        return LevelNumber;
    }
}

public enum Difficulty
{
    None,
    Easy,
    Medium,
    Hard
}
