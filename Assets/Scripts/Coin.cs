using UnityEngine;

public class Coin : MonoBehaviour
{
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = ScoreManager.GetInstance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            scoreManager.Increment();
            SoundManager.Instance.PlaySound("Coin");
            Object.Destroy(gameObject);
        }
    }
}

