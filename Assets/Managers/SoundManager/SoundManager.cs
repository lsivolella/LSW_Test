using UnityEngine;

/// <summary>
/// Manages environment music and sound effects
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] AudioClip introMusic;
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip buttonsSoundClip;

    private AudioSource[] audioSource;
    public AudioSource AudioSource { get; private set; }

    private void Awake()
    {
        SingletonSetup();
        GetComponents();
    }

    private void SingletonSetup()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void GetComponents()
    {
        audioSource = GetComponents<AudioSource>();
        AudioSource = audioSource[1];
    }

    private void Start()
    {
        PlayIntroMusic();
    }

    private void PlayIntroMusic()
    {
        audioSource[0].clip = introMusic;
        audioSource[0].volume = 0.03f;
        audioSource[0].Play();
    }

    private void Update()
    {
        if (audioSource[0].isPlaying) return;

        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        audioSource[0].clip = backgroundMusic;
        audioSource[0].volume = 0.02f;
        audioSource[0].loop = true;
        audioSource[0].Play();
    }

    public void PlayButtonsSoundEffect()
    {
        AudioSource.PlayOneShot(buttonsSoundClip);
    }
}
