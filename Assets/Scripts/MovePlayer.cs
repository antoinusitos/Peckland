using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private Transform myTransform = null;
    private Rigidbody2D myRigidbody2D = null;

    private float mySpeed = 250.0f;

    [SerializeField]
    private float myJumpForce = 8.0f;

    private Vector3 myVelocity = Vector3.zero;

    private bool myIsGrounded = false;

    private bool mycanJump = true;
    private float myTimeToJump = 0;

    private bool myCanMove = true;

    private bool myCanDash = true;
    private float myTimeToDash = 0;

    private bool myIsDashing = false;

    private Vector2 myStickDirection = Vector2.zero;

    private CircleCollider2D myTestGroundCheck = null;

    [SerializeField]
    private Transform myPlayerPivot = null;

    private void Awake()
    {
        myTransform = transform;
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!mycanJump)
        {
            myTimeToJump += Time.deltaTime;
        }

        if (!myCanDash)
        {
            myTimeToDash += Time.deltaTime;
        }

        if (!myCanMove)
        {
            return;
        }

        if (myIsGrounded)
        {
            if (myTimeToJump >= 0.1f)
            {
                myTimeToJump = 0;
                mycanJump = true;
            }
            if (myTimeToDash >= 0.1f)
            {
                myTimeToDash = 0;
                myCanDash = true;
            }
        }

        if (Input.GetKeyDown(PlayerInput.myJumpKey) && mycanJump)
        {
            myRigidbody2D.velocity = Vector2.up * myJumpForce;
            //mycanJump = false;
            myRigidbody2D.gravityScale = 1;
        }
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");
        myRigidbody2D.velocity = new Vector2((moveInput * mySpeed) * Time.deltaTime, myRigidbody2D.velocity.y);
        if (moveInput != 0)
        {
            myRigidbody2D.gravityScale = 1;
        }

        if (myRigidbody2D.velocity.x > 0)
            myPlayerPivot.localScale = new Vector3(1, 1, 1);
        else if (myRigidbody2D.velocity.x < 0)
            myPlayerPivot.localScale = new Vector3(-1, 1, 1);
    }

}
