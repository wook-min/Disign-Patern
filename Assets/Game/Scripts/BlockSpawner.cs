using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float StartSize = 5f;
    [SerializeField] private int level1To2Count = 5;
    [SerializeField] private int level2To3Count = 10;
    [SerializeField] private float currentSize = 5f;
    
    private static BlockSpawner instance;
    public static BlockSpawner Instance => instance;

    public event Action OnClick;

    public bool gameOver { get; private set; }

    public bool IsReady { get; private set; }

    private int count = 0;


    private GameObject currentBlock;

    private void Start()
    {
        StartCo();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (currentBlock.TryGetComponent<Block>(out var cs))
            {
                cs.SetStop();
                StartCo();
            }
        }
    }



    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);

        gameOver = false;
        IsReady = false;

        prefab = Resources.Load<GameObject>("Block");
    }

    public void StartCo()
    {
        StartCoroutine(SpawnBlock());
    }


    public void SetSpawn(bool isOn)
    {
        IsReady = isOn;
    }


    public void SetGameOver()
    {
        gameOver = true;
    }

    private IEnumerator SpawnBlock()
    {
        while (!gameOver)
        {
            if (currentSize <= 0.1f)
            {
                gameOver = true;
                yield break;
            }

            if (currentBlock != null)
                currentBlock.GetComponent<Block>().IsActive = false;

            IsReady = false;
            currentBlock = Instantiate(prefab, transform);
            currentBlock.GetComponent<Block>().IsActive = true;
            if (currentBlock.TryGetComponent<Block>(out var cs))
            {
                cs.Init(currentSize, Check(),
                    new Vector3(0, (count * 0.15f) + 0.5f, 0));
            }

            count++;
            yield return new WaitUntil(() => IsReady);

        }
    }

    private MoveSpeed Check()
    {
        MoveSpeed level;

        if (count < level1To2Count)
        {
            level = MoveSpeed.LEVEL1;
        }
        else if (count < level2To3Count)
        {
            level = MoveSpeed.LEVEL2;
        }
        else
        {
            level = MoveSpeed.LEVEL3;
        }

        return level;
    }

    public void SetSize(float z)
    {
        currentSize -= z;
    }

}
