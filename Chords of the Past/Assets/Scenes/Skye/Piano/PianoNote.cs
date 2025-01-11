using UnityEngine;

public enum Keys
{
    C,
    CS,
    D,
    DS,
    E,
    F,
    FS,
    G,
    GS,
    A,
    AS,
    B
}

public class PianoNote : MonoBehaviour
{
    public Keys note;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.Log("Missing audio source");
        }
    }

    public void PlayAudioSource()
    {
        audioSource.Play();
    }
}
