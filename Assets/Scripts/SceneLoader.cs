using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator animatorCover;
    [SerializeField] private string scene = "";

    private Animator animator;

    public void GoToScene()
    {
        StartCoroutine(GoToScene(scene));
    }

    private IEnumerator GoToScene(string sceneName)
    {
        animator.SetTrigger("exit");
        yield return new WaitForSecondsRealtime(HungryLouse.crossfadeSeconds);
        SceneManager.LoadScene(sceneName);
        HungryLouse.Pause = false;
    }

}
