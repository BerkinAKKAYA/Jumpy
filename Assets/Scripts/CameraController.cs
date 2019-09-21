using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] float smoothAmount = .3f;
    [SerializeField] float orthographicSize = 9;

    private Transform player;
    private Vector3 velocity;
    private Camera cam;

    #region Awake
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        cam = Camera.main;
        cam.orthographicSize = orthographicSize;
    }
    #endregion

    void Update() => Move();

    #region Move
    void Move()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            player.position,
            ref velocity,
            smoothAmount
        );
    }
    #endregion

    #region Zoom Out
    public void ZoomOut(float amount, float outSpeed, float inSpeed)
    {
        StopCoroutine("IZoomOut");
        StartCoroutine(IZoomOut(amount, outSpeed, inSpeed));
    }

    IEnumerator IZoomOut(float amount, float outSpeed, float inSpeed)
    {
        cam.orthographicSize = orthographicSize;
        float start = orthographicSize;

        while (cam.orthographicSize < start + amount)
        {
            cam.orthographicSize += outSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        cam.orthographicSize = start + amount;

        while (cam.orthographicSize > start)
        {
            cam.orthographicSize -= inSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        
        cam.orthographicSize = start;
    }
    #endregion
}