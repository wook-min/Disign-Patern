using System.Collections;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Bee>(out var Bee))
        {
            Bee.SetMove(false);
            Bee.DelayDie();
        }
    }
}
