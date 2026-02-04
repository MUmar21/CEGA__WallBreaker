using UnityEngine;

public class RangeEnemy : BaseEnemy
{
    private void Start()
    {
        ShowHealth();
    }

    /// <summary>
    /// This is attack method 
    /// </summary>
    public override void Attack()
    {
        Debug.Log("Range Enemy Attacked");

    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Range Enemy Dead");
    }

    public void StartState(BaseEnemy enemy)
    {
        Attack();
    }

    #region Movement

    #endregion

    #region Inputs

    #endregion
}

public abstract class BaseStates
{
    public virtual void StartState()
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual void EndState()
    {

    }
}


public class Attack : BaseStates
{
    public override void StartState()
    {
        Debug.Log("ATTACK STATE START");
    }

    public override void UpdateState()
    {

    }

    public override void EndState()
    {

    }
}


public class Die : BaseStates
{
    public override void StartState()
    {
        Debug.Log("DIE STATE START");
    }

    public override void UpdateState()
    {

    }

    public override void EndState()
    {

    }
}

public class Patrol : BaseStates
{
    public override void StartState()
    {

    }

    public override void UpdateState()
    {

    }

    public override void EndState()
    {

    }
}

public abstract class Chase : MonoBehaviour
{

    public virtual void StartState()
    {

    }

    public virtual void UpdateState()
    {

    }

    public virtual void EndState()
    {

    }
}