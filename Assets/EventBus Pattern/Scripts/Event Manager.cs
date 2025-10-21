using System;
using System.Collections.Generic;
using UnityEngine;

public enum Condition
{
    START,
    PAUSE,
    FINISH,
}

public static class EventManager
{

    private static Dictionary<Condition, Action> dictionary = new();

    // 이벤트 등록용 함수
    public static void Subscribe(Condition condition, Action action)
    {
        // key가 있다면 event 추가 : +=
        if (dictionary.ContainsKey(condition))
        {
            dictionary[condition] += action;
        }
        // key 가 없다면 event 등록 : =
        else
        {
            dictionary.Add(condition, action);
            // 또는 dictionary[condition] = action;
        }
        // OnEnable에 등록, OnDisable에 해제
    }

    // 이벤트 해제용 함수
    public static void UnSubscribe(Condition condition, Action action)
    {
        if (dictionary.ContainsKey(condition))
        {
            if (dictionary.ContainsValue(action))
            {
                dictionary[condition] -= action;
            }
            else
            {
                Debug.LogError($"VALUE {action} doesn't exist");
                return;
            }
        }
        else
        {
            Debug.LogError($"KEY {condition} doesn't exist");
            return;
        }
    }

    public static void Publish(Condition condition)
    {
        if (dictionary.TryGetValue(condition, out Action action))
        {
            action?.Invoke();
        }
        else
        {
            Debug.LogError($"{condition} doesn't exist");
            return;
        }
    }
}
