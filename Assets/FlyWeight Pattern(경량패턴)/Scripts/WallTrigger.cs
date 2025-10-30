using System.Collections;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{

    private WaitForSeconds wait = new(2f);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Animator>(out var animator))
        {
            animator.SetInteger("IntType", 2);
        }
        Debug.Log($"Collder {other.name}");

        if (other.TryGetComponent<BearMove>(out var move))
        {
            move.Stop();
        }

        StartCoroutine(DestroyGO(other.gameObject));
    }

    private IEnumerator DestroyGO(GameObject go)
    {
        yield return wait;

        Destroy(go);
    }
}
