using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI pointsText = null;
    [SerializeField] Vector2 horizontalDistanceRange = Vector2.zero;
    [SerializeField] float verticalDistance = 4;
    [SerializeField] int levelEndPoint = 25;

    [HideInInspector] public Vector2 lastPosition;

    private Transform platform;

    #region Points (Property)
    private int points;
    public int Points {
        get => points;
        set {
            points = value;
            pointsText.text = value.ToString();

            if (points == levelEndPoint)
            {
                LevelManager.OnLevelComplete();
                StartCoroutine("StopTime", 1);
            }
        }
    }
    #endregion

    #region Awake
    void Awake()
    {
        platform = GameObject.Find("Platform").transform;
    }
    #endregion

    #region Generate Next Platform
    public void GenerateNextPlatform(bool showGhost)
    {
        var xOffMag =  Random.Range(horizontalDistanceRange.x, horizontalDistanceRange.y);
        var toRight = (Random.value > 0.5f);

        float xOff = toRight ? xOffMag : -xOffMag;
        Vector2 newPos = lastPosition + new Vector2(xOff, verticalDistance);

        platform.position = newPos;
        lastPosition = newPos;
        Points ++;
    }
    #endregion

    #region Reset
    public void Reset()
    {
        Platforms platforms = FindObjectOfType<Platforms>();
        Transform player    = GameObject.Find("Player").transform;
        
        player.position = Vector2.up * 3;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Player>().StopCoroutine("IMove");
        Camera.main.transform.position = Vector2.zero;

        platform.position = Vector2.zero;
        lastPosition = Vector2.zero;

        Time.timeScale = 1;
        Points = 0;
    }
    #endregion

    #region Get Vertical Distance
    public static float GetVerticalDistance()
        => FindObjectOfType<Platforms>().verticalDistance;
    #endregion

    #region Stop Time
    System.Collections.IEnumerator StopTime(float speed)
    {
        while(Time.timeScale > Time.unscaledDeltaTime)
        {
            Time.timeScale -= speed * Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }

        Time.timeScale = 0;
    }
    #endregion
}