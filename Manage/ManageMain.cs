using UnityEngine;
using UnityEngine.UI;
using Management;

public class ManageMain : Manage
{
    private GameObject _oBoardOption;

    private Text _tPlayer, _tCoin;
    private Button _btnOption;

    protected override void Awake()
    {
        base.Awake();
        _oBoardOption = GameObject.Find("boardOption");
        _oBoardOption.SetActive(false);
        _btnOption = GameObject.Find("btnOption").GetComponent<Button>();

        _tPlayer = GameObject.Find("txtPlayer").GetComponent<Text>();
        _tCoin = GameObject.Find("txtCoin").GetComponent<Text>();
        updateInfo();
    }

    public void showOption()
    {
        _btnOption.enabled = false;
        _oBoardOption.SetActive(true);
    }

    void updateInfo()
    {
        _tPlayer.text = "Player : " + ManageApp.Inst.Name;
        _tCoin.text = string.Format("Coin : {0:#,0}", ManageApp.Inst.Coin);
    }

    void backToMain()
    {
        updateInfo();
        _btnOption.enabled = true;
    }

    public override void SetStart() { }
}
