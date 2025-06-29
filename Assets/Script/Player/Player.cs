using UnityEngine;

public class Player : Singleton<Player>
{
    //variables

    //properties
    protected override bool persistent => false;

    //methods

    protected override void Awake()
    {
        base.Awake();
    }
}
