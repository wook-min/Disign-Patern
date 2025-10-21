using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneryManager : MonoBehaviour
{
    [SerializeField] static SceneryManager instance;
    [SerializeField] Slider slider;
    [SerializeField] GameObject screen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(instance);
        }
    }

    public void LoadScene(int buildIndex)
    {
        screen.SetActive(true);
        StartCoroutine(TranstionScene(buildIndex));
    }

    // 웬만하면 씬은 이넘으로 작업하기
    public IEnumerator TranstionScene(int index)
    {
        slider.value = 0;

        // <AsyncOperation>
        // - allowSceneActivation
        // 장면이 준비된 즉시 장면이 활성화되는 것을 허용하는 변수입니다.
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);

        asyncOperation.allowSceneActivation = false;
        // fake Load Scene을 위해 자동으로 씬이 넘어가는 것을 방지

        // <AsyncOperation>
        // - isDone
        // 해당 동작이 완료되었는지 나타내는 변수입니다. (읽기 전용)
        while(asyncOperation.isDone == false)
        {
            // <AsyncOperation>
            // - progress
            // 작업의 진행상태를 나타내는 변수입니다. (읽기 전용)
           

            if (asyncOperation.progress >= 0.9f)
            {
                slider.value = Mathf.Lerp(slider.value, 1.0f, Time.deltaTime);

                if (slider.value >= 0.99f)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
            else
            {
                slider.value = asyncOperation.progress;
            }
            
            yield return null;
        }

        screen.SetActive(false);

    }
    
}
