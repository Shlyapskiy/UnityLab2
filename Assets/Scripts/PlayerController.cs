using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float sideSpeed = 5f;
    public float jumpForce = 5f;
    public float boostMultiplier = 2f;
    public float boostDuration = 3f;

    private Rigidbody rb;
    private bool isGrounded = true;
    private bool isBoosting = false;
    private float boostTimeLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 forwardMove = transform.forward * forwardSpeed;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, forwardMove.z);

        float horizontal = Input.GetAxis("Horizontal"); // A/D або ←/→
        rb.linearVelocity = new Vector3(horizontal * sideSpeed, rb.linearVelocity.y, forwardMove.z);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isBoosting)
        {
            StartBoost();
        }

        if (isBoosting)
        {
            boostTimeLeft -= Time.deltaTime;
            if (boostTimeLeft <= 0)
            {
                EndBoost();
            }
        }
    }

    void StartBoost()
    {
        forwardSpeed *= boostMultiplier;
        isBoosting = true;
        boostTimeLeft = boostDuration;
    }

    void EndBoost()
    {
        forwardSpeed /= boostMultiplier;
        isBoosting = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Зіткнення з перешкодою!");
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("Ти фінішував! Вітаю!");
        }
    }
}
