using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume = 1f;
    [Range(0f, 0.5f)]
    public float VolumeVariance = 0.1f;

    [Range(0f, 1f)]
    public float Pitch = 1f;
    [Range(0f, 0.5f)]
    public float PitchVariance = 0.1f;

    private AudioSource source;

    public void SetSource(AudioSource source)
    {
        this.source = source;
    }

    public AudioSource Play()
    {
        source.volume = Volume * (1 + Random.Range(-VolumeVariance, VolumeVariance) / 2);
        source.pitch = Pitch * (1 + Random.Range(-PitchVariance, PitchVariance) / 2);
        source.Play();
        return source;
    }
}
