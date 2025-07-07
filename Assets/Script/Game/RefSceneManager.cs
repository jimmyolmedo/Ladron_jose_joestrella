using UnityEngine;

public class RefSceneManager : MonoBehaviour
{
    public void LoadScene(string sceneManager)
    {
        SceneManager.instance.LoadScene(sceneManager);
    }
}
