using UnityEngine;

public class SlimeEnemy : BaseEnemy
{
    private void Start()
    {
        ShowHealth();
    }

    public override void Attack()
    {
        Debug.Log("Slime Enemy Attacked");
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Slime Enemy Dead");
    }
}
