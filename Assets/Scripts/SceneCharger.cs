using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
public class SceneCharger : MonoBehaviour
{

   private Animator animator;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private IEnumerator GoToScene(string sceneName)
    {
        animator.SetTrigger("exit");
        yield return new WaitForSecondsRealtime(HungryLouse.transitionSeconds);
        SceneManager.LoadScene(sceneName);
    }

    public void GoToMenu()
    {
        StartCoroutine(GoToScene("Menu"));
        Time.timeScale = 1f;
        HungryLouse.PlayMenuMusic();
    }
}
