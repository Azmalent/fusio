using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    const int LEFT = -1;
    const int IDLE = 0;
    const int RIGHT = 1;

    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (player.IsAlive)
        {
            if (Input.GetKey(KeyCode.LeftArrow)) player.SetMovingDirection(LEFT);
            else if (Input.GetKey(KeyCode.RightArrow)) player.SetMovingDirection(RIGHT);
            else player.SetMovingDirection(IDLE);

            if (Input.GetKeyDown(KeyCode.Space)) player.Jump();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ScoreManager.GetInstance().SetZero();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
