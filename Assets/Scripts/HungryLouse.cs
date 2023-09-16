using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HungryLouse : MonoBehaviour
{
    public static AudioSource audioSource;

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

    private void StartPreferences()
    {
        if (!PlayerPrefs.HasKey(VOLUME)) { Volume = 0.75f; }
        Level = 1;
    }

    private void StartComponents()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = Volume;
    }

    /*
    private void ResetPreferences()
    {
        PlayerPrefs.DeleteAll();
        StartPreferences();
    }
    */

    private static readonly string VOLUME = "volume";
    private static readonly string LEVEL = "level";

    public static float Volume
    {
        get { return PlayerPrefs.GetFloat(VOLUME); }
        set { PlayerPrefs.SetFloat(VOLUME, value); }
    }

    public static int Level
    {
        get { return PlayerPrefs.GetInt(LEVEL); }
        set { PlayerPrefs.SetInt(LEVEL, value); }
    }
}
