using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance { get; private set; }

    public AudioClip scoreSound;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayScoreSound()
    {
        AudioSource.PlayClipAtPoint(scoreSound, transform.position);
    }
}
