using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        ResetPreferences();
    }

    private void ResetPreferences()
    {
        PlayerPrefs.DeleteAll();
    }

}
