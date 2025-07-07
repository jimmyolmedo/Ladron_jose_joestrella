using System.Collections;
using UnityEngine;
using UnitySceneManager =  UnityEngine.SceneManagement.SceneManager;

public class SceneManager : Singleton<SceneManager>
{
    //variables 
    private bool isLoading = false;
    [SerializeField] Animator animator;
    //properties
    protected override bool persistent => true;

    //methods
    protected override void Awake()
    {
        base.Awake();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        UnitySceneManager.LoadScene(sceneName);
    }

    public void ResetScene()
    {
        UnitySceneManager.LoadScene(UnitySceneManager.GetActiveScene().name);
    }
}
