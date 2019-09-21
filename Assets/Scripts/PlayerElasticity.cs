using UnityEngine;

public class PlayerElasticity : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField] float elasticityAmount = .4f;
    [SerializeField] float elasticitySpeed = 1;

    private Rigidbody2D rb;
    private float desiredHeight;

    #region Awake
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    #endregion

    #region Update
    void Update()
    {
        desiredHeight = Mathf.Clamp(
            rb.velocity.y / 5f,
            1 - elasticityAmount,
            1 + elasticityAmount
        );

        GetInShape();
    }
    #endregion

    #region Get In Shape
    void GetInShape()
    {
        float speed = elasticitySpeed * Time.deltaTime;
        float y = transform.localScale.y;

             if (desiredHeight >= y + speed)   y += speed;
        else if (desiredHeight <= y - speed)   y -= speed;
        else                                   y  = desiredHeight;

        float x = (1 + elasticityAmount) - y + (1 - elasticityAmount);
        transform.localScale = new Vector3(x, y, 1);
    }
    #endregion
}