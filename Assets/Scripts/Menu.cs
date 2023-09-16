using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Image cover;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject credits;

    [SerializeField] private TMP_Text titleField;
    [SerializeField] private Button playButtom;

    [SerializeField] private AudioClip levelClip;

    [SerializeField] private float bleedSeconds = 0;

    private GameObject currentPanel;
    private Animator transition;

    private void Awake()
    {
        transition = cover.gameObject.GetComponent<Animator>();
        
        cover.gameObject.SetActive(true);
        
        menu.SetActive(true);
        options.SetActive(false);
        tutorial.SetActive(false);
        credits.SetActive(false);

        currentPanel = menu;
    }

    private void Start()
    {
        transition.SetTrigger("enter");
    }

    public void GoToGame()
    {
        StartCoroutine(BleedTitle(titleField, playButtom, levelClip, bleedSeconds));
    }

    public void GoToOptions()
    {
        StartCoroutine(Crossfade(options, 0.5f));
    }

    public void GoToTutorial()
    {
        StartCoroutine(Crossfade(tutorial, 0.5f));
    }

    public void GoToCredits()
    {
        StartCoroutine(Crossfade(credits, 0.5f));
    }

    public void GoToMenu()
    {
        StartCoroutine(Crossfade(menu, 0.5f));
    }

    private IEnumerator Crossfade(GameObject panel, float seconds)
    {
        transition.SetTrigger("exit");
        yield return new WaitForSeconds(seconds);
        currentPanel.SetActive(false);
        panel.SetActive(true);
        currentPanel = panel;
        transition.SetTrigger("enter");
    }

    public IEnumerator BleedTitle(TMP_Text titleTextField, Button playButton, AudioClip levelClip, float seconds)
    {
        playButton.interactable = false;

        Color oldColor = titleTextField.color;
        Color newColor = new(229, 0, 0, 255);

        float secondsElapsed = 0.0f;
        float transitionSeconds = 0.2f;

        while (secondsElapsed <= transitionSeconds)
        {
            Color color = Color.Lerp(oldColor, newColor, secondsElapsed / transitionSeconds);
            titleTextField.text = $"The <color=#{ColorUtility.ToHtmlStringRGBA(color)}>Hungry</color> Louse";
            secondsElapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene($"Level{HungryLouse.Level}");

        HungryLouse.audioSource.clip = levelClip;
        HungryLouse.audioSource.Play();

        yield break;
    }

}
