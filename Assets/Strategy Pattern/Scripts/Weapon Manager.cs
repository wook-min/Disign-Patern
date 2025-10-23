using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 하위 개체는 변하지 않는 것에 의존해야 합니다.
// 상위 : abstract, interface ...
// 하위 : 상속받은 자식

public class WeaponManager : MonoBehaviour
{
    [SerializeField] List<Weapon> weapons;

    private int currentIndex;

    private void Awake()
    {

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }

        currentIndex = 0;

        if (weapons != null)
        {
            weapons[0].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Attack();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SwapWeapon();
        }
    }

    public void SwapWeapon()
    {
        if (weapons.Count <= 1) return;

        weapons[currentIndex].gameObject.SetActive(false);
        currentIndex = (currentIndex + 1) % weapons.Count;
        weapons[currentIndex].gameObject.SetActive(true);
    }

    public void Attack()
    {
        if (weapons[currentIndex].TryGetComponent<Weapon>(out var weapon))
        {
            weapon.Attack();
        }
    }
}
