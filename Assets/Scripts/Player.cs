using UnityEngine;

public class Player : MonoBehaviour
{
    private const float FALL_GRAVITY_FACTOR = 1.5f;
    private const float LOW_JUMP_GRAVITY_FACTOR = 1f;

    public float Speed = 5f;
    public float JumpVelocity = 8f;

    private bool isGrounded = false;
    public bool IsAlive { get; private set; } = true;
    private float motion = 0f;
    private float width;

    private Rigidbody rigidBody;
    private PlayerAnimatorController animatorController;

    private Transform groundDetector;

    public static Player GetInstance()
    {
        return GameObject.Find("Player").GetComponent<Player>();
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animatorController = GetComponent<PlayerAnimatorController>();

        width = GetComponent<BoxCollider>().size.x;

        groundDetector = GameObject.Find(this.name + "/Ground Detector").transform;
    }

    void Update()
    {
        isGrounded =
             Physics.Linecast(transform.position, groundDetector.position + Vector3.right * width / 2)
             || Physics.Linecast(transform.position, groundDetector.position + Vector3.left * width / 2);

        animatorController.UpdateParameters(motion, isGrounded, !IsAlive);
    }

    void FixedUpdate()
    {
        rigidBody.velocity = new Vector3(motion, rigidBody.velocity.y, 0);

        if (rigidBody.velocity.y < 0)
        {
            var additionalGravity = Vector3.up * Physics.gravity.y * FALL_GRAVITY_FACTOR;
            rigidBody.velocity += additionalGravity * Time.deltaTime;
        }
        else if (rigidBody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            var additionalGravity = Vector3.up * Physics.gravity.y * LOW_JUMP_GRAVITY_FACTOR;
            rigidBody.velocity += additionalGravity * Time.deltaTime;
        }
    }

    public void SetMovingDirection(int direction)
    {
        motion = Speed * direction;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            var v = rigidBody.velocity;
            rigidBody.velocity = new Vector3(v.x, JumpVelocity, v.z);
            SoundManager.Instance.PlaySound("Jump");
        }
    }

    public void Die()
    {
        if (!IsAlive) return;

        IsAlive = false;
        float yVelocity = JumpVelocity * Random.Range(1f, 2.5f);
        float xVelocity = (motion == 0) ? Random.Range(-Speed, Speed) : -motion * Random.value;
        rigidBody.velocity = new Vector3(xVelocity, yVelocity, 0);
        transform.position += Vector3.back * 10;
        SoundManager.Instance.PlaySound("PlayerDeath");

        var deathMessage = GameObject.Find("HUD/Death Message");
        deathMessage.GetComponent<FadingText>().FadeIn();
    }
}
