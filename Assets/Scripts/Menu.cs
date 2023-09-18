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
    [SerializeField] private GameObject credits;

    [SerializeField] private TMP_Text titleField;
    [SerializeField] private Button playButton;

    [SerializeField] private float bleedSeconds = 0;

    private GameObject currentPanel;
    private Animator transition;

    private void Awake()
    {
        transition = cover.gameObject.GetComponent<Animator>();

        cover.gameObject.SetActive(true);

        menu.SetActive(true);
        options.SetActive(false);
        credits.SetActive(false);

        currentPanel = menu;

        playButton.interactable = true;
    }

    private void Start()
    {
        transition.SetTrigger("enter");
        HungryLouse.PlayMenuMusic();
    }

    public void GoToGame()
    {
        StartCoroutine(BleedTitle(titleField, bleedSeconds));
    }

    public void GoToOptions()
    {
        StartCoroutine(CrossfadePanel(options));
    }

    public void GoToTutorial()
    {
        transition.SetTrigger("exit");
        StartCoroutine(CrossfadeScene("Tutorial"));
    }

    public void GoToCredits()
    {
        StartCoroutine(CrossfadePanel(credits));
    }

    public void GoToMenu()
    {
        StartCoroutine(CrossfadePanel(menu));
    }

    private IEnumerator CrossfadePanel(GameObject panel)
    {
        transition.SetTrigger("exit");
        yield return new WaitForSeconds(HungryLouse.crossfadeSeconds);
        currentPanel.SetActive(false);
        panel.SetActive(true);
        currentPanel = panel;
        transition.SetTrigger("enter");
    }

    private IEnumerator CrossfadeScene(string sceneName)
    {
        transition.SetTrigger("exit");
        yield return new WaitForSeconds(HungryLouse.crossfadeSeconds);
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator BleedTitle(TMP_Text titleTextField, float seconds)
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
        yield return StartCoroutine(CrossfadeScene($"Level{HungryLouse.Level}"));

        HungryLouse.PlayLevelMusic();

        yield break;
    }

}
