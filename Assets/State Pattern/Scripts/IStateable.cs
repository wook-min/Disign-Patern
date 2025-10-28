using System.Linq.Expressions;
using UnityEngine;

public interface IStateable
{
    public abstract void OnEnter(Animator animator, StateManager character);
    public abstract void OnUpdate();
    public abstract void OnExit();

}
