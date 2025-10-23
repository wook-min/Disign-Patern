using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public abstract void Attack();
}
