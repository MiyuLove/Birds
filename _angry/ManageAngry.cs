using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageAngry : ManageGame
{
    public static ManageAngry angryInst;
    public Transform _parent;
    private GameObject _plank, _bird,_mainCamera;
    private float _wid = 1.6f;
    private int birdNum = 3;

    protected override void Awake()
    {
        base.Awake();
        inst = this;
        angryInst = this;
        _plank = (GameObject)Resources.Load("Plank");
        _bird = (GameObject)Resources.Load("Bird");
        _mainCamera = GameObject.FindWithTag("MainCamera");
        tScore = GameObject.Find("txtScore").GetComponent<Text>();
        tBest = GameObject.Find("txtBest").GetComponent<Text>();
        tLife = GameObject.Find("txtLife").GetComponent<Text>();
        tBest.text = string.Format("Player : {0}\nBestscore : {1}",
            ManageApp.Inst.Name,
            ManageApp.Inst.Best[SceneManager.GetActiveScene().buildIndex-2]);
        Invoke("setCountDown", 1f);
    }

    void Start()
    {
        int maxcol = 8;
        for(int r = 0; r <=2; r++)
        {
            maxcol = Random.Range(1, 1 + maxcol);
            CreateRows(r, maxcol);
        }
        Score = ManageApp.Inst.score;
        tScore.text = string.Format("Score : {0}",ManageApp.Inst.score);
    }

    void CreateRows(int row, int col)
    {
        float s = _wid * (-col / 2) - (_wid / 2) * (col % 2);
        for (int i = 0; i < col + 1; i++) CreatePlank(s, row, i, true);
        for (int i = 0; i < col; i++) CreatePlank(s + _wid / 2, row, i, false);

        GameObject o = Instantiate(_bird, transform.position, Quaternion.identity);
        o.transform.SetParent(_parent);
        float x = s + _wid / 2 + Random.Range(0, col) * _wid;
        float y = -0.5f + 2f * row;
        o.transform.localPosition = new Vector2(x, y);
    }

    void CreatePlank(float s, int r, int c, bool v)
    {
        GameObject o = Instantiate(_plank, transform.position, Quaternion.identity);
        o.transform.SetParent(_parent);
        if (v)
        {
            o.transform.localRotation = Quaternion.Euler(0, 0, 90);
            o.transform.localPosition = new Vector2(s + c * _wid, r * 2);
        }
        else
        {
            o.transform.localPosition = new Vector2(s + c * _wid, r * 2 + 1);
        }
    }
    public override void SetStart()
    {
        Debug.Log(GameObject.Find("StoneBall"));
        _mainCamera.GetComponent<HorzScroll>().GameStart(20.48f,
            GameObject.Find("StoneBall"));
    }

    public void CatchBird()
    {
        Debug.Log(birdNum--);
        if (birdNum == 0) Invoke("newGame", 1f);
    }

    private void newGame()
    {
        ManageApp.Inst.score = Score;
        NextScene(3);
    }
}
