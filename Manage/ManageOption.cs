using UnityEngine;
using UnityEngine.UI;

public class ManageOption : MonoBehaviour
{
    private GameObject _pIfName;
    private Text _tName, _tIfText;
    private Text _tPlayer, _tBest, _tCoin;

    void Awake()
    {
        _pIfName = GameObject.Find("ifName");

        _tName = GameObject.Find("tName").GetComponent<Text>();
        _tIfText = GameObject.Find("ifText").GetComponent<Text>();
        _tPlayer = GameObject.Find("tPlayer").GetComponent<Text>();
        _tBest = GameObject.Find("tBest").GetComponent<Text>();
        _tCoin = GameObject.Find("tCoin").GetComponent<Text>();
    }

    void OnEnable()
    {
        _pIfName.SetActive(false);
        _tName.text = "Name";
        updateInfo();
    }

    void updateInfo()
    {
        _tPlayer.text = "Player : " + ManageApp.Inst.Name;
        _tBest.text = string.Format("best score : F({0:#,0}), A({1:#,0})",
            ManageApp.Inst.Best[0], ManageApp.Inst.Best[1]);
        _tCoin.text = string.Format("Coin : {0:#,0}", ManageApp.Inst.Coin);
    }

    public void AddCoin()
    {
        ManageApp.Inst.Coin += 100;
        updateInfo();
    }

    public void SetInputName()
    {
        _pIfName.SetActive(!_pIfName.activeSelf);
        _tName.text = (_pIfName.activeSelf) ? "OK" : "Name";
        if (!_pIfName.activeSelf)
        {
            ManageApp.Inst.changeUser(_tIfText.text);
            updateInfo();
        }
    }

    public void exit()
    {
        ManageApp.Inst.Save();
        GameObject.Find("Scene Manager").SendMessage("backToMain");
        gameObject.SetActive(false);
    }
}
