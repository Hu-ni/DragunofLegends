using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    private GameManager _main;

    private const int Max_Stage = 20;   // 마지막 스테이지는 무조건 보스
    private const int Rest_Stage = 10;

    private int currentStageIdx = 0;
    [SerializeField]
    private List<StageData> _stages;

    private Stage _currStage;  // 현재 스테이지 오브젝트
    private Dictionary<StageType, List<Stage>> _poolStage = new();    //생성된 스테이지를 랜덤으로 가져오기 위해 사용.

    public List<StageData> StageList { get { return _stages; } }
    public int CurrentStageIdx { get { return currentStageIdx; } }


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
    }


    public void Initialize(GameManager gameManager)
    {
        _main = gameManager;

        foreach (StageData stage in _stages) 
        {
            if (!_poolStage.ContainsKey(stage.Type)) _poolStage[stage.Type] = new List<Stage>();

            GameObject obj = Instantiate(stage.Prefabs);
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            obj.transform.parent = this.transform;

            Stage tmp = obj.GetComponent<Stage>();
            if (tmp == null)
            {
                Debug.LogError("Stage 컴포넌트 등록이 필요!");
                Destroy(obj);
                continue;
            }

            _poolStage[stage.Type].Add(tmp);
        }
    }

    public void CreateStage()
    {
        if(_currStage != null)
            _currStage.gameObject.SetActive(false);

        currentStageIdx++;

        StageType type = NextStageType();

        List<Stage> list = _poolStage[type];
        int selectIdx = Random.Range(0, _poolStage[type].Count);

        _currStage = list[selectIdx];
        _currStage.gameObject.SetActive(true);
    }

    public StageType NextStageType()
    {
        if (currentStageIdx == Rest_Stage)
            return StageType.Rest;
        if (currentStageIdx == Max_Stage)
            return StageType.Boss;
        else return StageType.Combat;
    }

    public void SpawnMonster()
    {
        _currStage.Execute(currentStageIdx);
    }
}
