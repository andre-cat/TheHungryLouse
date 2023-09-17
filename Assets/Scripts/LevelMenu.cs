using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class LevelMenu : MonoBehaviour
{
    [SerializeField] private Image cover;

    private Animator transition;

    private void Awake()
    {
        transition = gameObject.GetComponent<Animator>();
    }

    public void BackToMenu()
    {
        StartCoroutine(ChangeScene("Menu"));
    }

    private IEnumerator ChangeScene(string sceneName)
    {
        transition.SetTrigger("exit");
        yield return new WaitForSecondsRealtime(HungryLouse.transitionSeconds);
        SceneManager.LoadScene(sceneName);
        HungryLouse.PlayMenuMusic();
    }
}
