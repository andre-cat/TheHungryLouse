using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public static float transitionSeconds = 0.5f;

    public static bool pause = false;

    private static HungryLouse instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            StartPreferences();
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

    private void StartPreferences()
    {
        if (!PlayerPrefs.HasKey(VOLUME)) { Volume = 0.75f; }
        Level = 1;
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

    /*
    private void ResetPreferences()
    {
        PlayerPrefs.DeleteAll();
        StartPreferences();
    }
    */

    #region attributes
    private static readonly string VOLUME = "volume";
    private static readonly string LEVEL = "level";

    public static float Volume
    {
        get { return PlayerPrefs.GetFloat(VOLUME); }
        set { PlayerPrefs.SetFloat(VOLUME, value); audioSource.volume = PlayerPrefs.GetFloat(VOLUME); }
    }

    public static int Level
    {
        get { return PlayerPrefs.GetInt(LEVEL); }
        set { PlayerPrefs.SetInt(LEVEL, value); }
    }

    #endregion attributes
}
