using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Button))]
public class MainMenuButton : MonoBehaviour
{
    private Button pauseButton;
    private void Awake()
    {
        pauseButton = gameObject.GetComponent<Button>();
        pauseButton.onClick.AddListener(GoToMainMenu);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
