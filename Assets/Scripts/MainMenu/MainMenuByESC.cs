using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuByESC : MonoBehaviour
{
    #region Update
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(0);
    }
    #endregion
}