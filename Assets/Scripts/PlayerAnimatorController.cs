using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    public GameObject SpriteHolder;

    private Animator animator;
    private Vector3 scale;

    void Start()
    {
        animator = SpriteHolder.GetComponent<Animator>();
        scale = transform.localScale;
    }

    private void Flip()
    {
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void UpdateParameters(float motion, bool isGrounded, bool isHurt)
    {
        animator.SetFloat("motion", motion);
        if (motion * scale.x < 0) Flip();

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isHurt", isHurt);
    }
}
