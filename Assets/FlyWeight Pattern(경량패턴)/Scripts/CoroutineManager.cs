using System.Collections.Generic;
using UnityEngine;

public static class CoroutineManager
{
    private static Dictionary<float, WaitForSeconds> dictionary = new();

    public static WaitForSeconds GetCachedWait(float time)
    {
        if (!dictionary.ContainsKey(time))
            dictionary.Add(time, new WaitForSeconds(time));

        return dictionary[time];
    }
}
