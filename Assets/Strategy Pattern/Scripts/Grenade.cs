using System.Collections;
using UnityEngine;

public class Grenade : Weapon
{
    [SerializeField] private Color originalColor;
    [SerializeField] private float changeCoolTime = 3f;

    private Renderer goRenderer;
    private float originR;
    private float originG;
    private float originB;

    private bool changeTrigger = true;

    private void OnEnable()
    {
        changeTrigger = true;

        if (gameObject.TryGetComponent<Renderer>(out var Renderer))
        {
            this.goRenderer = Renderer;
            var mat = goRenderer.material;
            originalColor = mat.color;
            originR = originalColor.r;
            originG = originalColor.g;
            originB = originalColor.b;
        }
    }

    private void OnDisable()
    {
        originalColor = new Color(originR, originG, originB);
        goRenderer.material.color = originalColor;
        changeTrigger = true;
    }

    public override void Attack()
    {
        if (changeTrigger)
        {
            StartCoroutine(ChangeColor());
        }
        else
        {
            Debug.Log("Weapon Cooldown Now");
            return;
        }
    }


    private IEnumerator ChangeColor()
    {
        Debug.Log("Start Grenade Coroutine");
        changeTrigger = false;
        float newR = Random.Range(0f, 1f);
        float newG = Random.Range(0f, 1f);
        float newB = Random.Range(0f, 1f);
        float time = 0f;

        originalColor = new Color(newR, newG, newB);
        goRenderer.material.color = originalColor;

        while (!changeTrigger)
        {
            time += Time.deltaTime;

            if (time >= changeCoolTime)
            {
                originalColor = new Color(originR, originG, originB);
                goRenderer.material.color = originalColor;
                changeTrigger = true;
                yield break;
            }
            yield return null;
        }

        yield return null;
    }


}
