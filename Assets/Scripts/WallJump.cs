using UnityEngine;

public class WallJump : MonoBehaviour
{
    public Transform wallCheck;
    public float wallCheckDistance = 0.5f;
    public LayerMask wallLayer;
    RaycastHit2D isWallTouchingRight;
    bool isGrounded;
    RaycastHit2D hit;

    void Update()
    {
        isWallTouchingRight = Physics2D.Raycast(
            wallCheck.position,
            Vector2.right * transform.localScale.x,
            wallCheckDistance,
            wallLayer
        );
        Debug.Log(isWallTouchingRight.collider);

        if (Input.GetKeyDown(KeyCode.Space) && isWallTouchingRight && !isGrounded)
        {
            WallJump();
        }

        hit = Physics2D.Raycast(transform.position + Vector3.down * 1.7f, Vector2.down, -0.1f);
        Debug.Log(hit.collider);
        if (hit.collider == null)
        {
            isGrounded = false;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision != null && transform.position.y > collision.contacts[0].point.y)
        {
            isGrounded = true;
            //Debug.Log("IsGrounded");
        }
    }
}
