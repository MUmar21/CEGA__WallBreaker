using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public BaseEnemy currentEnemy;
    public BaseStates currentState;
    Attack attack = new Attack();
    Die die = new Die();


    public void OnAttack()
    {
        SwitchState(attack);
        currentState.StartState();
        currentEnemy.Attack();
    }

    public void OnDie()
    {
        SwitchState(die);
        currentState.StartState();
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public void SwitchState(BaseStates state)
    {
        currentState = state;
    }

}
