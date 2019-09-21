using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Level (property)
    public static int Level
    {
        get => PlayerPrefs.GetInt("Level", 1);
        set => PlayerPrefs.SetInt("Level", value);
    }
    #endregion

    #region On Level Complete
    public static void OnLevelComplete()
    {
        int completedLevel = CurrentLevelIndex();
        int nextLevel = (completedLevel + 1);

        if (Application.CanStreamedLevelBeLoaded("Level" + nextLevel))
        {
            if (Level < nextLevel)
                Level = nextLevel;
                
            SceneManager.LoadScene("Level" + nextLevel);
        }
        else
        {
            Level = completedLevel;
            SceneManager.LoadScene(0);
        }
    }
    #endregion
    
    #region Current Level Index
    static int CurrentLevelIndex()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string indexSTR = sceneName.Substring(sceneName.Length-1, 1);
        return int.Parse(indexSTR);
    }
    #endregion
}