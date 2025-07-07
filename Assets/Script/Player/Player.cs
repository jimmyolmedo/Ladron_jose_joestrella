using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Singleton<Player>, Idamageable
{
    //variables
    [SerializeField] Treasure treasure;//objeto que robar
    [SerializeField] bool stolen;//se activara cuando haya terminado de robar el objeto
    [SerializeField] RigidbodyMovement rigidbodyMovement;

    //properties
    protected override bool persistent => false;
    public bool Stolen { get => stolen; set => stolen = value; }

    //methods

    protected override void Awake()
    {
        base.Awake();
    }

    public void GetDamage()
    {
        Debug.Log("Me atraparon");
        SceneManager.instance.ResetScene();
    }

    public void Steal(InputAction.CallbackContext _context)
    {
        if(_context.started)
        {
            if(treasure != null)
            {
                treasure.StealingObject = true;
                rigidbodyMovement.disableMovement();
                
            }
        }

        if (_context.canceled)
        {
            if (treasure != null)
            {
                treasure.StealingObject = false;
            }
            rigidbodyMovement.CanMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Treasure _treasure))
        {
            treasure = _treasure;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Treasure _treasure))
        {
            treasure.StealingObject = false;    
            treasure = null;
        }
    }
}
