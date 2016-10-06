using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{

    GameManager gm;
    Animator animator;
    Rigidbody2D rb;

    public float WALK_SPEED_SCALE;
    public float JUMP_SPEED_SCALE;
    public float MAX_VELOCITY;

    public string MOVE_ANIMATION_PARAM = "Walking";

    bool moveRight = true;
    bool jumping = false;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        animator = this.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        WALK_SPEED_SCALE = 0.05f;
        JUMP_SPEED_SCALE = 3;

        StartCoroutine(MovementController());
    }

    void Update()
    {

    }

    void FlipSprite()
    {
        float x = this.gameObject.transform.localScale.x;
        float y = this.gameObject.transform.localScale.y;
        float z = this.gameObject.transform.localScale.x;

        x = x * -1;

        this.gameObject.transform.localScale = new Vector3(x, y, z);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collider hit");
        if (jumping && other.gameObject.tag == "Platform")
        {
            Debug.Log("landed");
            jumping = false;
        }
    }

    IEnumerator Jump()
    {
        if (!jumping)
        {
            jumping = true;

            float xDirect = 0;
            if (Input.GetKey(KeyCode.D)) { xDirect = 1; }
            else if (Input.GetKey(KeyCode.A)) { xDirect = -1; }
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(xDirect, JUMP_SPEED_SCALE), ForceMode2D.Impulse);
        }

        yield return 0;

    }


    IEnumerator MovementController()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.D))
            {
                animator.SetBool(MOVE_ANIMATION_PARAM, true);
                if (!moveRight) { FlipSprite(); }
                this.transform.Translate(1f * WALK_SPEED_SCALE, 0, 0);
                moveRight = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                animator.SetBool(MOVE_ANIMATION_PARAM, true);
                if (moveRight) { FlipSprite(); }
                this.transform.Translate(-1f * WALK_SPEED_SCALE, 0, 0);

                moveRight = false;
            }
            else
            {
                animator.SetBool(MOVE_ANIMATION_PARAM, false);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(Jump());
            }
            yield return 0;
        }
    }

}
