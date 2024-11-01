using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip collides;
    public AudioClip dead;
    public AudioClip deadGhost;
    public AudioClip hitHurt;
    public AudioClip intro;
    public AudioClip pickup;
    public AudioClip scare;
    public AudioClip walk;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "collides":
                audioSource.PlayOneShot(collides);
                break;
            case "dead":
                audioSource.PlayOneShot(dead);
                break;
            case "deadGhost":
                audioSource.PlayOneShot(deadGhost);
                break;
            case "hitHurt":
                audioSource.PlayOneShot(hitHurt);
                break;
            case "intro":
                audioSource.PlayOneShot(intro);
                break;
            case "pickup":
                audioSource.PlayOneShot(pickup);
                break;
            case "scare":
                audioSource.PlayOneShot(scare);
                break;
            case "walk":
                audioSource.PlayOneShot(walk);
                break;
            default:
                Debug.LogWarning("Sound name not found: " + soundName);
                break;
        }
    }
}
