using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool state;
    public bool State => state;
    [SerializeField] static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        state = true;
    }

    public void OnClick()
    {
        state = !state;
    }
}
