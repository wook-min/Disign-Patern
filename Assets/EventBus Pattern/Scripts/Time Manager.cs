using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Header("출력 시간 텍스트")]
    [SerializeField] private Text timeText;

    [Header("시간초")]
    [SerializeField] private float time;
    [SerializeField] private int minute;
    [SerializeField] private int second;
    [SerializeField] private int microsecond;

    [SerializeField] private bool state = true;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.Subscribe(Condition.START, Execute);
        EventManager.Subscribe(Condition.PAUSE, Pause);
        EventManager.Subscribe(Condition.FINISH, Exit);
    }

    private void OnDisable()
    {
        EventManager.UnSubscribe(Condition.START, Execute);
        EventManager.UnSubscribe(Condition.PAUSE, Pause);
        EventManager.UnSubscribe(Condition.FINISH, Exit);
    }

    public void Execute()
    {
        state = true;
        StartCoroutine(TimeCheck());
    }

    public void Pause()
    {
        state = false;
    }

    private IEnumerator TimeCheck()
    {
        while (state)
        {
            // time += Time.deltaTime;
            // minute = (int)(time / 60);
            // second = (int)(time % 60f);
            // microsecond = (int)((time * 100) % 100);

            if (time <= 0)
            {
                EventManager.Publish(Condition.FINISH);
                yield break;
            }

            time -= Time.deltaTime;
            minute = (int)(time / 60);
            second = (int)(time % 60);
            microsecond = (int)(time * 100) % 100;

            //timeText.text = $"{minute:00}:{second:00}:{microsecond:00}";
            timeText.text = string.Format("{0:D2} : {1:D2} : {2:D2}", minute, second, microsecond);
            yield return null;
        }
    }

    public void Exit()
    {
        timeText.text = "Game Over";
    }
}
