using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HungryLouse : MonoBehaviour
{
    #region music
    [SerializeField] private AudioClip menuClip_;
    [SerializeField] private AudioClip levelClip_;

    private static AudioClip menuClip;
    private static AudioClip levelClip;

    private static AudioSource audioSource;

    public static void PlayMenuMusic()
    {
        if (!audioSource.clip.Equals(menuClip))
        {
            audioSource.clip = menuClip;
            audioSource.Play();
        }
    }

    public static void PlayLevelMusic()
    {
        if (!audioSource.clip.Equals(levelClip))
        {
            audioSource.clip = levelClip;
            audioSource.Play();
        }
    }
    #endregion music

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            StartComponents();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource.Play();
    }

    private void StartComponents()
    {
        levelClip = levelClip_;
        menuClip = menuClip_;

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = Volume;
        audioSource.playOnAwake = false;
        audioSource.loop = true;
        audioSource.clip = menuClip;
    }

    #region attributes

    private static HungryLouse instance = null;

    public static float crossfadeSeconds = 0.5f;
    
    private static readonly string VOLUME = "volume";
    private static readonly string LEVEL = "level";

    public static float Volume
    {
        get { return PlayerPrefs.GetFloat(VOLUME, 0.25f); }
        set { PlayerPrefs.SetFloat(VOLUME, value); audioSource.volume = PlayerPrefs.GetFloat(VOLUME); }
    }

    public static int Level
    {
        get { return PlayerPrefs.GetInt(LEVEL, 1); }
        set { PlayerPrefs.SetInt(LEVEL, value); }
    }

    public static bool Pause
    {
        get { return Time.timeScale == 0; }
        set { if (value) { Time.timeScale = 0; } else { Time.timeScale = 1; } }
    }
    #endregion attributes
}
