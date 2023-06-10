using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    #region Instance
    public static GameManager m_Instance;
    public static GameManager Instance
    {
        get
        {
            return m_Instance;
        }
    }
    #endregion
    public enum GAME_STATE
    {
        GAMEPLAY,

        FINISH
    }
    public GAME_STATE m_State;
    public TextMeshProUGUI finishRaceTxt;
    public Button restartBtn;
    private void Awake()
    {
        m_Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetState(GAME_STATE.GAMEPLAY);
    }

    public void SetState(GAME_STATE st)
    {
        m_State = st;
        switch(m_State)
        {
            case GAME_STATE.GAMEPLAY:
                HideUICanvas();
                break;
            case GAME_STATE.FINISH:
                ShowUICanvas();
                break;
        }
    }

    void ShowUICanvas()
    {
        finishRaceTxt.gameObject.SetActive(true);
        restartBtn.gameObject.SetActive(true);
    }

    void HideUICanvas()
    {
        finishRaceTxt.gameObject.SetActive(false);
        restartBtn.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
