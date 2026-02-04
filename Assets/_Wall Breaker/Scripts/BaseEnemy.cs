using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    public int health = 100;

    public abstract void Attack();

    public virtual void Die()
    {
        Debug.Log("Enemy dead!!");
    }

    public void ShowHealth()
    {
        Debug.Log($"Health : {health}");
    }
}
