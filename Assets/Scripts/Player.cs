using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Camera cam;
    private Platforms platforms;
    private PlayerMovement movement;
    private AdManager adManager;

    #region Awake
    void Awake()
    {
        cam = Camera.main;
        Time.timeScale = 1;
        Application.targetFrameRate = 600;
        platforms = FindObjectOfType<Platforms>();
        movement = GetComponent<PlayerMovement>();
        adManager = FindObjectOfType<AdManager>();
    }
    #endregion

    #region Update
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }
    #endregion

    #region On Became Invisible
    void OnBecameInvisible() => Invoke("OnFall", .5f);
    #endregion

    #region On Fall
    void OnFall()
    {
        if (cam.WorldToViewportPoint(transform.position).y < 0)
        {
            bool adShown;
            adManager.ShowAdByChance(out adShown);

            if (adShown)
                Time.timeScale = 0;
            else
                platforms.Reset();
        }
    }
    #endregion

    #region On Collision
    void OnCollisionEnter2D (Collision2D col)
    {
        DeconstructEffect.Generate(platforms.lastPosition);
        platforms.GenerateNextPlatform(Random.value > .8f);
        movement.Jump();
    }
    #endregion
}