using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private GameObject _menu;

    public TMP_Text playerCountText;
    public TMP_Text waitingText;
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public GameObject readyButton;

    public GameObject winPanel;
    public GameObject losePanel;

    private void Awake() => instance = this;


    public void ReadyButton()
    {
        GameManager.instance.isGameStart = true;

        foreach (var item in GameManager.instance._initlializedFakePlayers)
        {
            Character character = item.GetComponent<Character>();
            character.animator.SetTrigger("Start");
        }

        _menu.SetActive(false);
    }


}
