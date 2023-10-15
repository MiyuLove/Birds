using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Management;

public class ManageGame : Manage {
    public static ManageGame inst;
    [HideInInspector] public bool isGameOver = false;
    [HideInInspector] public int gameMode=0;
    protected Text tScore, tBest, tLife;
    private int score = 0, life = 3;

    public int Life {
        get { return life; }
        set { life = value; }
    }

    public int Score {
        get { return score; }
        set { score = value; }
    }

    protected override void Awake () {
        base.Awake();
        inst = this;
        tScore=GameObject.Find("txtScore").GetComponent<Text>();
        tBest=GameObject.Find("txtBest").GetComponent<Text>();
        tLife=GameObject.Find("txtLife").GetComponent<Text>();
        tBest.text = string.Format("Player : {0}\n Bestscore : {1}",
            ManageApp.Inst.Name,
            ManageApp.Inst.Best[SceneManager.GetActiveScene().buildIndex - 2]);
        Invoke("setCountDown", 1f);
    }

    void setCountDown()
    {
        InstantiateUI("txtCount", "Canvas", false);
    }

    public void SetGameOver () {
        if (ManageGame.inst.gameMode == 2) return;
        ManageGame.inst.gameMode=2;
        isGameOver = true;

        Debug.Log("GG");
        InstantiateUI("boardResult", "Canvas", false);
    }

    public void SetAddScore () {
        tScore.text = string.Format ("Score : {0}", score += 10);
    }
    
    public void SetLifeDown() {
        tLife.text = string.Format("Life : {0}", --life);
    }

    public void NextScene(int index) {// 결과창에서 버튼 클릭 함수로 사용됨.
        SceneManager.LoadScene(index);
    }

    public override void SetStart()
    {
        ManageGame.inst.gameMode = 1;
        GameObject.Find("GameManager").GetComponent<ObstaclePool>().
            InitColumnCreate();
        GameObject.Find("Bird").SendMessage("GameStart");
        GameObject[] objs = GameObject.FindGameObjectsWithTag("HorzScroll");

        foreach (var o in objs)
            o.SendMessage("GameStart");
    }
}