using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] string nextLevel;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openDoor;

    private void Update()
    {
        if(Player.instance.Stolen == true)
        {
            spriteRenderer.sprite = openDoor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            if(player.Stolen == true)
            {
                SceneManager.instance.LoadScene(nextLevel);
            }
        }
    }
}
