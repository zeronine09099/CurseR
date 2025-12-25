using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField]
    float cooltime = 5f;
    [SerializeField]
    Rigidbody2D[] TargetObject;
    [SerializeField]
    float dropGravityScale = 1.0f;
    private Vector2 initialPosition;
    private float currentCooltime;
    private bool isFalling = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 pos = transform.position;
        initialPosition = pos;
        currentCooltime = 1f;
    }

    void Update()
    {
        currentCooltime -= Time.deltaTime;
        if (currentCooltime <= 0f)
        {
            currentCooltime = cooltime;
            isFalling = true;
        }
    }

    void FixedUpdate()
    {
        if (isFalling)
        {
            foreach (Rigidbody2D rb in TargetObject)
            {
                rb.linearVelocityY -= (Time.deltaTime * dropGravityScale);
            }
        }
    }



    public void Delete(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.gravityScale = 0f;

        rb.position = initialPosition;

        isFalling = false;
    }
}
