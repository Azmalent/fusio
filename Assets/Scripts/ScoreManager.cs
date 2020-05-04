using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static int score;

    public static ScoreManager GetInstance()
    {
        return GameObject.Find("Score Manager").GetComponent<ScoreManager>();
    }

    void Start()
    {
        UpdateCounter();
    }

    public void Increment()
    {
        score++;
        UpdateCounter();
    }

    public void SetZero()
    {
        score = 0;
        UpdateCounter();
    }

    public void UpdateCounter()
    {
        GameObject.Find("HUD/Score Counter").GetComponent<Text>().text = score.ToString();
    }
}