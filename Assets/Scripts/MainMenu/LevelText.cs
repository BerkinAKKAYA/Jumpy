using UnityEngine;

public class LevelText : MonoBehaviour
{
    static TMPro.TextMeshProUGUI uiText;

    #region Awake & Start
    void Awake()
    {
        uiText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Start() => UpdateText();
    #endregion

    #region Update Text
    public static void UpdateText()
    {
        var lvl = LevelManager.Level.ToString();
        uiText.text = "Level: " + lvl;
    }
    #endregion
}