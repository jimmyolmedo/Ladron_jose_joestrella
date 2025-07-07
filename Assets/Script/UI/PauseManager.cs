using UnityEngine;

public class PauseManager : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
    }

    public void pause()
    {
        if(UIManager.Instance.CurrentIdentifier == "Game Status")
        {
            UIManager.Instance.SwitchPanel("Pause");
            Time.timeScale = 0f;
        }
        else if (UIManager.Instance.CurrentIdentifier == "Pause")
        {
            UIManager.Instance.SwitchPanel("Game Status");
            Time.timeScale = 1f;
        }
    }
}
