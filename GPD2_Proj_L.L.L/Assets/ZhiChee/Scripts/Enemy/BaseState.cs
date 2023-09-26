using UnityEngine;

public abstract class BaseState
{
    protected EnemyStats curEnmy;

    public abstract void ForStateEnter(EnemyStats _manager);

    public abstract void ForStateUpdate();
    
    public abstract void ForStateExit();
}
