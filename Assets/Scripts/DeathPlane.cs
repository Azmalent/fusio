using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") Player.GetInstance().Die();
        else if (other.tag == "Block") Object.Destroy(other.gameObject);
    }
}