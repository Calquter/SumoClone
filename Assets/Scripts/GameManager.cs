using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject[] _fakePlayers;

    public List<GameObject> _initlializedFakePlayers;
    public GameObject player;

    private int _fakePlayerCount;

    [SerializeField] private float _gameTimer;
    public bool isGameStart;

    public int playerScore = 0;

    private void Awake() => instance = this;


    private void Start()
    {
        _fakePlayerCount = Random.Range(7, _fakePlayers.Length);
        StartCoroutine(InitPlayersWithDelay());
    }

    private void Update()
    {
        GameTimeControl();
    }

    private void ControlGameState()
    {
        if (!player.activeSelf)
        {
            
            isGameStart = false;
        }
        else if (_initlializedFakePlayers.Count == 0)
        {
            

            isGameStart = false;
        }
    }

    private void GameTimeControl()
    {
        if (_gameTimer > 0)
        {
            _gameTimer -= Time.deltaTime;
        }
        else if (_gameTimer <= 0)
        {
            _gameTimer = 0;
            isGameStart = false;

            foreach (var item in _initlializedFakePlayers)
            {
                Character character = item.GetComponent<Character>();
                character.animator.SetTrigger("Victory");
            }
        }

        UIController.instance.timerText.text = _gameTimer.ToString();
    }


    public void EleminatePlayer(GameObject obj)
    {
        if (_initlializedFakePlayers.Contains(obj))
            _initlializedFakePlayers.Remove(obj);

        obj.SetActive(false);

        ControlGameState();
    }

    private void InitPlayer()
    {
        GameObject selectedPlayer = _fakePlayers[Random.Range(0, _fakePlayers.Length - 1)];

        if (!_initlializedFakePlayers.Contains(selectedPlayer))
        {
            selectedPlayer.SetActive(true);
            _initlializedFakePlayers.Add(selectedPlayer);
            UIController.instance.playerCountText.text = (_initlializedFakePlayers.Count + 1).ToString();
            _fakePlayerCount--;
        }
    }

    public void AddScoreToPlayer(int amount)
    {
        playerScore += amount;

        UIController.instance.scoreText.text = $"Score: {playerScore}";
    }

    private IEnumerator InitPlayersWithDelay()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        InitPlayer();

        if (_fakePlayerCount > 0)
            StartCoroutine(InitPlayersWithDelay());
        else
        {
            UIController.instance.waitingText.text = "All Players are Ready";
            UIController.instance.readyButton.SetActive(true);
        }

    }

}
