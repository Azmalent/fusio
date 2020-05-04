using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField]
    public Sound[] Sounds;

    void Start()
    {
        if (Instance == null) Instance = this;
        else
        {
            Debug.Log("Error: multiple instances of " + nameof(SoundManager));
            return;
        }

        for (int i = 0; i < Sounds.Length; i++)
        {
            var go = new GameObject("Sound_" + Sounds[i].Name);
            var source = go.AddComponent<AudioSource>();
            source.clip = Sounds[i].Clip;
            Sounds[i].SetSource(source);
        }
    }

    public AudioSource PlaySound(string name)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (Sounds[i].Name == name) return Sounds[i].Play();
        }

        Debug.Log("Error: sound '" + name + "' not found!");
        return null;
    }
}