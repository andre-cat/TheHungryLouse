using UnityEngine;

public class Reset : MonoBehaviour
{
    private void Start()
    {
        ResetPreferences();
    }

    private void ResetPreferences()
    {
        PlayerPrefs.DeleteAll();
    }

}
