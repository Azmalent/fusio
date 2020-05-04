using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform Target;
    public Vector3 TargetOffset;
    public float MinimumY;
    public float LerpTime = 0.3f;

    private float shakingAmplitude = 0f;
    private float shakingDuration = 0f;
    private float remainingShakingDuration = 0f;

    void FixedUpdate()
    {
        var p = Target.position + TargetOffset;

        if (remainingShakingDuration > 0)
        {
            float a = shakingAmplitude * remainingShakingDuration / shakingDuration;
            var shakingOffset = Vector3.right * a * Mathf.Sin(20 * Time.time);
            remainingShakingDuration -= Time.deltaTime;
            if (remainingShakingDuration <= 0) ShopShaking();

            p += shakingOffset;
        }

        var endPosition = new Vector3(p.x, Mathf.Max(p.y, MinimumY), p.z);
        transform.position = Vector3.Lerp(transform.position, endPosition, LerpTime);
    }

    public void Shake(float amplitude, float duration)
    {
        shakingAmplitude = amplitude;
        shakingDuration = duration;
        remainingShakingDuration = duration;
    }

    public void ShopShaking()
    {
        remainingShakingDuration = 0f;
        shakingAmplitude = 0f;
    }
}
