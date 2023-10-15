using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour {
    private Text tRank; // 1~10위까지의 랭크정보 문자열을 출력한다.
    private int sNum;
    void Start() {
        sNum = SceneManager.GetActiveScene().buildIndex;
        SetResult();
        ManageApp.Inst.Save();// 랭크정보를 저장한다.
        tRank = GameObject.Find ("txtRank").GetComponent<Text>();
        tRank.text = ManageApp.Inst.getRankString(sNum-2);// 랭크정보 문자열 형태로 가져온다.
     }

    void SetResult() {
        // best score update
        ManageApp.Inst.updateBest(sNum - 2, ManageGame.inst.Score);
        
        var list_name = new ArrayList();
        var list_score = new ArrayList();
        string out_name;
        int out_score;
        for (int i = 0; i < 10; i++) {// 기존 데이터를 모두 리스트로 가져온다.
            ManageApp.Inst.GetData(sNum-2,i, out out_name, out out_score);
            list_name.Add(out_name);
            list_score.Add(out_score);
        }

        for (int i = 0; i < 10; i++) {
            // 항목을 순차적으로 탐색하면서 현재의 스코어를 삽입할 위치를 찾는다.
            if (ManageGame.inst.Score > (int) list_score[i]) {
                list_name.Insert(i, ManageApp.Inst.Name);
                list_score.Insert(i, ManageGame.inst.Score);
                break; // 랭크 데이터로 삽입하였기 때문에 탐색을 종료한다.
            }
        }
        // 새롭게 구성된 랭크 데이터를 저장한다.
        for (int i = 0; i < 10; i++) {
            ManageApp.Inst.SetData(sNum-2,i, (string) list_name[i], (int) list_score[i]);
        }
    }

    public void onClick(int next)
    {
        if(next == 0)
        {
            next = sNum;
        }

        GameObject.Find("GameManager").SendMessage("SetFadeOut", next);
    }
}