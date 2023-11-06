using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] private Transform levelHolder;
    [SerializeField] private byte totalLevelCount;

    #endregion

    #region Private Variables

    private OnLevelLoaderCommand _levelLoaderCommand;
    private OnLevelDestroyerCommand _levelDestroyerCommand;

    private short _currentLevel;
    private LevelData _levelData;




    #endregion


    private void Awake()
    {
        _levelData = GetLevelData();
        _currentLevel = GetActiveLevel();

        Init();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }


    void Start()
    {
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
    }

    void Update()
    {

    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void Init()
    {
        _levelLoaderCommand = new(levelHolder);
        _levelDestroyerCommand = new(levelHolder);
    }

    private LevelData GetLevelData()
    {
        return Resources.Load<CD_Level>("Data/CD_Level").Levels[_currentLevel];
    }

    private short GetActiveLevel()
    {
        return _currentLevel;
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize += _levelLoaderCommand.Execute;
        CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyerCommand.Execute;
        CoreGameSignals.Instance.onGetLevelValue += OnGetLevelValue;
        CoreGameSignals.Instance.onNextLevel += OnNextLevel;
        CoreGameSignals.Instance.onRestart += OnRestartLevel;
    }

    private void UnsubscribeEvents()
    {
        CoreGameSignals.Instance.onLevelInitialize -= _levelLoaderCommand.Execute;
        CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyerCommand.Execute;
        CoreGameSignals.Instance.onGetLevelValue -= OnGetLevelValue;
        CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
        CoreGameSignals.Instance.onRestart -= OnRestartLevel;
    }

    private void OnNextLevel()
    {
        _currentLevel++;
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
    }

    private void OnRestartLevel()
    {
        CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
        CoreGameSignals.Instance.onReset?.Invoke();
        CoreGameSignals.Instance.onLevelInitialize?.Invoke((byte)(_currentLevel % totalLevelCount));
    }


    private byte OnGetLevelValue()
    {
        return (byte)_currentLevel;
    }



}
