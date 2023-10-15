using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
    private Text textCount;
    private int count;

    void Start() {
        count = 3;
        textCount = GetComponent<Text>();
    }

    public void ChangeCount() {
        count--;// 카운트 다운한다.
        textCount.text = (count >= 0) ? "" + count : "GO";// 마지막으로 시작을 알린다.

        if (count < -1) {
            GameObject.Find("GameManager").SendMessage("SetStart");
            Destroy(gameObject);// 카운트 다운 Text 오브젝트를 메모리에서 제거한다.
        }
    }
}