using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Treasure : MonoBehaviour
{
    //variables
    [SerializeField] int maxTimeToSteal;    
    float timeToSteal;
    [SerializeField] Image stealBar;
    [SerializeField] bool stealingObject;

    //properties
    public bool StealingObject { get => stealingObject; set => stealingObject = value; }

    //method
    private void Update()
    {
        stealBar.fillAmount = timeToSteal/maxTimeToSteal;
        if (!stealingObject)
        {
            timeToSteal = maxTimeToSteal;
            stealBar.enabled = false;
        }
        else
        {
            timeToSteal -= Time.deltaTime;
            stealBar.enabled = true;
        }

        if (timeToSteal <= 0)
        {
            Debug.Log("He robado el objeto");
            Player.instance.Stolen = true;
            Destroy(gameObject);
        }
    }
}
