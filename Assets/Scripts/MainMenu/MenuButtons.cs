using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI clearProgressText = null;

    void Start() => AdManager.HideBanner();

    #region Update
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
    #endregion

    public void Play() => SceneManager.LoadScene("Level" + LevelManager.Level);
    public void OpenLevelSelect() => SceneManager.LoadScene("LevelSelect");

    #region Clear Progress
    public void ClearProgress()
    {
        LevelManager.Level = 1;
        LevelText.UpdateText();
        ShowClearedText(1);
    }

    public void ShowClearedText(float duration)
    {
        ShowClearedText();
        Invoke("RestoreClearText", duration);
    }

    public void ShowClearedText()
    {
        clearProgressText.text = "Cleared !";
    }

    public void RestoreClearText()
    {
        clearProgressText.text = "Clear Progress";
    }
    #endregion
}