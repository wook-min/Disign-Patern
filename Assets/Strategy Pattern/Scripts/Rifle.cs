using System.Collections;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform muzzle;

    private GameObject bulletGO;
    

    public override void Attack()
    {
        bulletGO = Instantiate(this.bullet);
        Debug.Log("Instantiate bullet");
        if (bulletGO.TryGetComponent<Bullet>(out var bullet))
        {
            Debug.Log("Bullet has Script");
            bullet.Init(muzzle);
            bullet.StartCo();
        }
    }

   
}
