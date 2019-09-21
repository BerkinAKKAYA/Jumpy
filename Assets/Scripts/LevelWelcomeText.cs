using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelWelcomeText : MonoBehaviour
{
    private Image image;
    private TMPro.TextMeshProUGUI text;

    #region Awake
    void Awake()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }
    #endregion

    void Start() => StartCoroutine("HideText");

    #region Hide Text
    IEnumerator HideText()
    {
        float opacity = 1;
        image.color = new Color(0,0,0,1);
        text.color  = new Color(1,1,1,1);

        while(opacity > Time.deltaTime)
        {
            opacity -= Time.deltaTime;
            image.color = new Color(0,0,0, opacity);
            text.color  = new Color(1,1,1, opacity);
            yield return new WaitForEndOfFrame();
        }

        image.color = new Color(0,0,0,0);
        text.color  = new Color(1,1,1,0);
        gameObject.SetActive(false);
    }
    #endregion
}