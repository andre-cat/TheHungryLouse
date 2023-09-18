using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCharger : MonoBehaviour
{
    [SerializeField] private string scene = "";

    private Animator animator;

    private void Awake()
    {
        animator = GameObject.FindGameObjectWithTag("Cover").GetComponent<Animator>();
    }

    public void GoToScene()
    {
        StartCoroutine(GoToScene(scene));
    }

    private IEnumerator GoToScene(string sceneName)
    {
        animator.SetTrigger("exit");
        yield return new WaitForSecondsRealtime(HungryLouse.crossfadeSeconds);
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

}
