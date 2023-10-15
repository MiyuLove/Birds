using Management;
using UnityEngine;
using UnityEngine.UI;
public class ManageGameManager : Manage
{
    public Text tText, tScore, tBest, tName;
   
    public virtual void SetText()
    {
        tText = GameObject.Find("txtText").GetComponent<Text>();
        tScore = GameObject.Find("txtScore").GetComponent<Text>();
        tBest = GameObject.Find("txtBest").GetComponent<Text>();
        tName = GameObject.Find("txtname").GetComponent<Text>();
    }

    public override void SetStart()
    {

    }
}
