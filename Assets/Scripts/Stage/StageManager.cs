using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    private GameManager _main;

    private const int Max_Stage = 20;   // 마지막 스테이지는 무조건 보스
    private const int Rest_Stage = 10;

    private int currentStage = 0;
    [SerializeField]
    private List<Stage> _stages;
    [SerializeField]
    private Stage _restStage;
    [SerializeField]
    private Stage _bossStage;

    private GameObject _currStage;  // 현재 스테이지 오브젝트
    private Dictionary<int, GameObject> _poolStage = new();    //생성된 스테이지를 랜덤으로 가져오기 위해 사용.

    public List<Stage> StageList { get { return _stages; } }


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Initialize(GameManager.instance);
        CreateStage();
            
    }


    public void Initialize(GameManager gameManager)
    {
        _main = gameManager;
        for(int i = 0; i < _stages.Count; i++) 
        {
            GameObject obj = Instantiate(_stages[i].Stg);
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            obj.transform.parent = this.transform;
            _poolStage.Add(i, obj); // StageList의 Index도 같이 사용하기 위해 i 값도 같이 저장
        }
    }

    public void CreateStage()
    {
        if(_currStage != null)
        _currStage.SetActive(false);

        if (currentStage == Rest_Stage)     // 일회성이기 때문에 이 때 생성
        {
            _currStage = Instantiate(_restStage.Stg);
            _currStage.SetActive(true);
            _currStage.transform.position = Vector3.zero;
            _currStage.transform.parent = this.transform;

        }
        else if (currentStage == Max_Stage) // 일회성이기 때문에 이 때 생성
        {
            _currStage = Instantiate(_bossStage.Stg);
            _currStage.SetActive(true);
            _currStage.transform.position = Vector3.zero;
            _currStage.transform.parent = this.transform;

        }
        else
        {
            int selectIdx = Random.Range(0, StageList.Count);
            _currStage = _poolStage[selectIdx];
            _currStage.SetActive(true);

        }
    }
}
