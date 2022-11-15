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


    public bool isGameStart;

    private void Awake() => instance = this;


    private void Start()
    {
        _fakePlayerCount = Random.Range(7, _fakePlayers.Length);
        StartCoroutine(InitPlayersWithDelay());
    }

    private void ControlGameState()
    {
        if (!player.activeSelf)
        {
            //LoseScreen
        }
        else if (_initlializedFakePlayers.Count == 0)
        {
            //Win Screen
        }
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
            _fakePlayerCount--;
        }
    }

    private IEnumerator InitPlayersWithDelay()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        InitPlayer();

        if (_fakePlayerCount > 0)
            StartCoroutine(InitPlayersWithDelay());

    }

}
