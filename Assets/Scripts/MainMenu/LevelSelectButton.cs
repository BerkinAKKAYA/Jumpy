using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour
{
    [SerializeField] int levelIndex = 1;

    #region Awake
    void Awake()
    {
        AdManager.HideBanner();
        SetInteractable();
    }
    #endregion
    
    #region Load Level
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }
    #endregion

    #region Load Endless
    public void LoadEndless()
    {
        SceneManager.LoadScene("LevelEndless");
    }
    #endregion

    #region Set Interactable
    void SetInteractable()
    {
        var button = GetComponent<UnityEngine.UI.Button>();
        bool interactable = (levelIndex <= LevelManager.Level);
        button.interactable = interactable;
    }
    #endregion
}