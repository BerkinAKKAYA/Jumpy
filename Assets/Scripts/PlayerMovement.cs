using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float horizontalMoveAmount = 4;
    [SerializeField] float moveSpeed = 16;
    [SerializeField] float jumpForce = 27;

    private Camera cam;
    private CameraController camController;
    private Rigidbody2D rb;
    private bool isEditor;

    #region Awake
    void Awake()
    {
        rb  = GetComponent<Rigidbody2D>();
        isEditor = Application.platform == RuntimePlatform.WindowsEditor;
        cam = Camera.main;
        camController = cam.GetComponent<CameraController>();
    }
    #endregion

    void Update() => CheckTouch();

    #region Check Touch
    void CheckTouch()
    {
        #if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
            OnTouch(Input.mousePosition);
        }
        #else
        if(Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnTouch(Input.GetTouch(0).position);
        }
        #endif
    }
    #endregion
    
    #region OnTouch
    void OnTouch(Vector2 touchPosition)
    {
        var touchPos = cam.ScreenToViewportPoint(touchPosition).x;
        var x = transform.position.x;

             if (touchPos > 0.48f)  Move(x + horizontalMoveAmount);
        else if (touchPos < 0.52f)  Move(x - horizontalMoveAmount);
    }
    #endregion

    #region Move
    void Move(float x)
    {
        StopCoroutine("IMove");
        StartCoroutine("IMove", x);
    }

    IEnumerator IMove(float x)
    {
        Vector3 desired = new Vector3(x,0,0);

        while (transform.position != desired)
        {
            desired.y = transform.position.y;

            transform.position = Vector3.MoveTowards(
                transform.position,
                desired,
                moveSpeed * Time.deltaTime
            );

            yield return new WaitForEndOfFrame();
        }
    }
    #endregion

    #region Jump
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        camController.ZoomOut(.5f, 8, 10);
    }
    #endregion
}