using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    private Button pauseButton;
    private void Awake()
    {
        pauseButton = gameObject.GetComponent<Button>();
        pauseButton.onClick.AddListener(RestartScene);
    }
    private void RestartScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
