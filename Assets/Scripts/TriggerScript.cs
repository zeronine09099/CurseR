using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D triggerTarget;
    [SerializeField]
    float dropGravityScale = 1.0f;

    private bool hasTriggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            hasTriggered = true;
            triggerTarget.AddForce(Vector2.down * 5.0f, ForceMode2D.Impulse);
            triggerTarget.gravityScale = dropGravityScale;

        }
    }
}
