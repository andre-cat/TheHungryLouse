using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Win : MonoBehaviour
{
    [SerializeField] private Animator coverAnimator;
    [SerializeField] private GameObject environment;
    [SerializeField] private Camera winCamera;
    [SerializeField] private GameObject winGirl;
    [SerializeField] private Image lastCover;

    private Animator animator;
    private AudioSource audioSource;
    private GameObject mainCamera;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        winGirl.GetComponent<WinGirlController>().enabled = false;
    }

    public IEnumerator ShowLastScreen()
    {
        coverAnimator.SetTrigger("win");
        yield return new WaitForSeconds(1);
        environment.SetActive(false);
        winCamera.gameObject.SetActive(true);
        mainCamera.SetActive(false);
        animator.SetTrigger("win");
        audioSource.Play();
        winGirl.GetComponent<WinGirlController>().enabled = true;
    }

    public void Return()
    {
        lastCover.gameObject.GetComponent<Animator>().SetTrigger("exit");
        float time = 0;
        while (time < HungryLouse.crossfadeSeconds){
            time += Time.deltaTime;
        }
        SceneManager.LoadScene("Menu");
    }
}
