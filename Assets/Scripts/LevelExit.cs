using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public string NextLevelName;

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(NextLevel());
    }

    private IEnumerator NextLevel()
    {
        var sound = SoundManager.Instance.PlaySound("Level Complete");
        yield return new WaitWhile(() => sound.isPlaying);

        SceneManager.LoadScene(NextLevelName);
    }
}
