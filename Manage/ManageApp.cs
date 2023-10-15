using System.Collections.Generic;
using UnityEngine;

public class ManageApp : MonoBehaviour
{
    public static ManageApp Inst;

    private string nick;
    private int usern, coin;
    private const string PLAYER = "Player";
    private const string COUNT = "Count";
    private const string USER = "User";
    private const string COIN = "Coin";
    private const string FB = "UserDATAFB7171";
    private const string AB = "UserDATAAB1717";
    private string[] GAMENAME;

    private const int GAMECOUNT = 2;
    private int[] best = new int[GAMECOUNT];

    private List<string> names = new List<string>();
    private Dictionary<string, int>[] dat = new Dictionary<string, int>[GAMECOUNT];

    public string Name { get { return nick; } set { nick = value; } }
    public int[] Best { get { return best; } set { best = value; } }
    public int Coin { get { return coin; } set { coin = value; } }
    public int score = 0;
    private int[][] arScores = new int[GAMECOUNT][];
    private string[][] arNames = new string[GAMECOUNT][];

    private string defaultScores = "0,0,0,0,0,0,0,0,0,0";
    private string defaultNames = "N/A,N/A,N/A,N/A,N/A,N/A,N/A,N/A,N/A,N/A";

    void Awake()
    {
        Inst = this;
        SetGameName();
        Load();
        DontDestroyOnLoad(gameObject);
    }

    private void SetGameName()
    {
        //for adding to 10 game later
        GAMENAME = new string[GAMECOUNT];
        for (int i = 0; i < GAMECOUNT; i++)
            GAMENAME[i] = "BirdGame" + i;
    }

    void Load()
    {
        usern = PlayerPrefs.GetInt(COUNT, 0);
        nick = PlayerPrefs.GetString(USER, "none");
        coin = PlayerPrefs.GetInt(COIN, 0);
        score = 0;
        for (int i = 0; i < usern; i++)
            names.Add(PlayerPrefs.GetString(PLAYER + i, "N/A"));

        for (int i = 0; i < GAMECOUNT; i++)
            LoadHelper(i);
    }

    private void LoadHelper(int idx)
    {
        dat[idx] = new Dictionary<string, int>();
        for (int i = 0; i < usern; i++)
        {
            int _best = PlayerPrefs.GetInt(GAMENAME[idx] + names[i], 0);
            dat[idx].Add(names[i], _best);

            if (names[i] == nick) best[idx] = _best;
        }

        string score = PlayerPrefs.GetString(GAMENAME[idx] + "Scores", defaultScores);
        string snames = PlayerPrefs.GetString(GAMENAME[idx] + "Names", defaultNames);

        arNames[idx] = new string[10];
        arScores[idx] = new int[10];
        string[] tmps = snames.Split(',');
        string[] tmpi = score.Split(',');

        for (int i = 0; i < 10; i++)
        {
            arNames[idx][i] = tmps[i];
            arScores[idx][i] = int.Parse(tmpi[i]);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt(COUNT, usern);
        PlayerPrefs.SetString(USER, nick);
        PlayerPrefs.SetInt(COIN, coin);
        score = 0;

        for (int i = 0; i < usern; i++)
            PlayerPrefs.SetString(PLAYER + i, names[i]);

        for (int i = 0; i < GAMECOUNT; i++)
            SaveHelper(i);
    }

    private void SaveHelper(int idx)
    {
        for (int i = 0; i < usern; i++)
            PlayerPrefs.SetInt(GAMENAME[idx] + names[i], dat[idx][names[i]]);

        string _scores = "" + arScores[idx][0];
        string _names = "" + arNames[idx][0];
        for (int i = 1; i < 10; i++)
        {
            _scores += "," + arScores[idx][i];
            _names += "," + arNames[idx][i];
        }

        PlayerPrefs.SetString(GAMENAME[idx] + "Scores", _scores);
        PlayerPrefs.SetString(GAMENAME[idx] + "Names", _names);
    }

    public void SetData(int idx, int index, string name, int score)
    {
        arNames[idx][index] = name;
        arScores[idx][index] = score;
    }

    public void GetData(int idx, int index, out string out_name, out int out_score)
    {
        out_name = arNames[idx][index];
        out_score = arScores[idx][index];
    }

    public void updateBest(int idx, int _score)
    {
        if (best[idx] < _score) { best[idx] = _score; dat[idx][nick] = best[idx]; }
        Save();
    }

    public void changeUser(string _name)//if adding a new game, add a new index
    {
        if (dat[0].ContainsKey(_name) || dat[1].ContainsKey(_name))
        {
            nick = _name;
            best[0] = dat[0][nick];
            best[1] = dat[1][nick];
        }
        else
        {
            usern++;
            nick = _name;
            best[0] = 0;
            best[1] = 0;
            names.Add(nick);
            dat[0].Add(nick, 0);
            dat[1].Add(nick, 0);
        }

        Debug.Log($"{nick} {best[0]} {best[1]}");

        Save();
    }

    public string getRankString(int idx)
    {
        string res = "";
        for (int i = 0; i < 10; i++)
        {
            res += string.Format("{0:D2}. {1} ({2:#,0})\n",
                i + 1, arNames[idx][i], arScores[idx][i]);
        }
        return res;
    }

    public bool gameStartCoin()
    {
        if (Coin < 500)
        {
            return true;
        }
        Coin -= 500;
        return false;
    }
}