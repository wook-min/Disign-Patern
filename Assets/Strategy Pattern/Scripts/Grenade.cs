using System.Collections;
using UnityEngine;

public class Grenade : Weapon
{
    [SerializeField] private Color changeColor;
    [SerializeField] private float changeCoolTime = 3f;

    // 메테리얼 자체를 두 가지 넣는 방법도 있다.

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
            changeColor = mat.color;
            originR = changeColor.r;
            originG = changeColor.g;
            originB = changeColor.b;
        }
    }

    private void OnDisable()
    {
        changeColor = new Color(originR, originG, originB);
        goRenderer.material.color = changeColor;
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

        // coroutine 변수를 선언 후, 코루틴이 null일때만 코루틴 함수가 실행되게 설정하면 더 깔끔
    }


    private IEnumerator ChangeColor()
    {
        Debug.Log("Start Grenade Coroutine");
        changeTrigger = false;
        float newR = Random.Range(0f, 1f);
        float newG = Random.Range(0f, 1f);
        float newB = Random.Range(0f, 1f);
        float time = 0f;

        changeColor = new Color(newR, newG, newB);
        goRenderer.material.color = changeColor;

        while (!changeTrigger)
        {
            time += Time.deltaTime;

            if (time >= changeCoolTime)
            {
                changeColor = new Color(originR, originG, originB);
                goRenderer.material.color = changeColor;
                changeTrigger = true;
                yield break;
            }
            yield return null;
        }

        yield return null;
    }


}
