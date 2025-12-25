using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Unity.VisualScripting;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    //-------------------------------------------------------
    //[SerializeField]
    //bool useKeyHorizontal = true;
    //[SerializeField]

    public float speed = 8;
    //[SerializeField]
    //float accelerator = 50;
    //[SerializeField]
    //float decelerator = 70;


    public float jumpVelocity = 8;
    public float maxJumpTime = 0.2f;
    public float hoveringTime = 0.2f;
    public float gravity = 2.5f;
    public float jumpHoldForce = 10f;
    //-------------------------------------------------------

    Rigidbody2D rigid;

    Vector2 vel;
    float horizontal;
    float vertical;
    float lastGroundTime;
    float jumpTimer;
    bool isGrounded;

    RaycastHit2D hit;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        vel = rigid.linearVelocity;
        if (Keyboard.current.aKey.isPressed)
        {
            horizontal += -1;
            horizontal = Mathf.Clamp(horizontal, -1, 1);
        }
        if (Keyboard.current.dKey.isPressed)
        {
            horizontal += 1;
            horizontal = Mathf.Clamp(horizontal, -1, 1);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            vertical += 1;
            jumpTimer = maxJumpTime;
            vertical = Mathf.Clamp(vertical, -1, 1);
        }

        if (horizontal != 0)
        {
            float scale = vel.x * horizontal;
            float sign = horizontal;
          
            scale = speed;
            vel.x = scale * sign;
        }
        else
        {
            float scale = Mathf.Abs(vel.x);
            float sign = Mathf.Sign(vel.x);
          
            scale =  0;
            vel.x = scale * sign;
        }

        if (vertical == 1)
        {
            if (Time.time < lastGroundTime + hoveringTime)
            {
                rigid.gravityScale = 0f;
                vel.y = Mathf.Max(vel.y, jumpVelocity);
                jumpTimer -= Time.deltaTime;
            }               
        }

        if (jumpTimer > 0 && Keyboard.current.spaceKey.isPressed == true)
        {
            rigid.gravityScale = 0f;
            vel.y += jumpHoldForce * Time.deltaTime;
        }
        else
        {
            jumpTimer = 0f;
            rigid.gravityScale = gravity;
        }

            jumpTimer -= Time.deltaTime;
        horizontal = 0;
        vertical = 0;
        rigid.linearVelocity = vel;

        hit = Physics2D.Raycast(transform.position + Vector3.down * 1.7f, Vector2.down, -0.1f);
        Debug.Log(hit.collider);
        if (hit.collider == null)
        {
                isGrounded = false;

        }

    }

    #region onCollision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionChange(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision != null && transform.position.y > collision.contacts[0].point.y)
        {
            isGrounded = true;
            //Debug.Log("IsGrounded");
        }

            OnCollisionChange(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionChange(collision);
    }
    #endregion

    void OnCollisionChange(Collision2D collision)
    {
        

        foreach (var item in collision.contacts)
        {
            if (item.normal.y > 0.6f)
            {
                lastGroundTime = Time.time;
                break;
            }
        }
    }

    public void Move(float horizontal, float vertical)
    {
        this.horizontal += horizontal;
        this.horizontal = Mathf.Clamp(this.horizontal, -1, 1);
        this.vertical += vertical;
        this.vertical = Mathf.Clamp(this.vertical, -1, 1);
    }
}

