using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private int _nextScene = 0;
    private bool okayNextScene = false;

    public void endFadeIn()
    {
        gameObject.SetActive(false);
    }

    public void endFadeOut()
    {
        if (okayNextScene)
        {

        }
        SceneManager.LoadScene(_nextScene);
    }

    public void setFadeout()
    {
        GetComponent<Animator>().SetTrigger("SetFadeout");
    }

    public void setNextScene(int _idx)
    {
        _nextScene = _idx;
    }
}
