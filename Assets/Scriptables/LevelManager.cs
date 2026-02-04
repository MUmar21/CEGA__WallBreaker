using System.Linq;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelData[] LevelDatas;
    [SerializeField] private LevelData currentLevel;
    [SerializeField] private int tapCount;
    [SerializeField] private TMP_Text tapText;

    private void Awake()
    {
        currentLevel = LevelDatas.FirstOrDefault(data => !data.IsLocked && !data.IsCompleted);
    }
    
    private void Start()
    {
        tapCount = 0;
        tapText.text = "Taps : " + tapCount.ToString();

        OnLevelUpdate();
    }

    public void IncrementTaps()
    {
        tapCount++;
        tapText.text = "Taps : " + tapCount.ToString();

        if (tapCount >= currentLevel.MaxTaps)
        {
            Debug.Log("Level Complete");
            currentLevel.IsCompleted = true;
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        tapCount = 0;
        tapText.text = "Taps : " + tapCount.ToString();

        LevelData nextLevel = LevelDatas.FirstOrDefault(data => data.LevelNumber > currentLevel.LevelNumber);
        if(nextLevel == null)
        {
            Debug.Log("All Levels Clear!!!!!!!!");
            return;
        }
        nextLevel.IsLocked = false;
        currentLevel = nextLevel;
        OnLevelUpdate();
    }

    private void OnLevelUpdate()
    {
        Debug.Log($"Current Level : {currentLevel.LevelNumber}");
        Debug.Log($"Level Instruction: {currentLevel.Instructions}");
        Debug.Log($"Level Difficulty: {currentLevel.Difficulty}");
    }

}
