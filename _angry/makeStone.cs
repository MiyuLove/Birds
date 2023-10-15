using UnityEngine;

public class makeStone : MonoBehaviour
{
    public GameObject stoneBall, _mainCamera;
    void Start()
    {
        _mainCamera = GameObject.FindWithTag("MainCamera");
        makeStoneBall();
    }

    public void makeStoneBall()
    {
        ManageAngry.inst.SetLifeDown();
        if(ManageAngry.inst.Life == -1)
        {
            ManageAngry.inst.Life = 0; ;
            ManageAngry.inst.SetGameOver();
            _mainCamera.SendMessage("setGameOver");
            return;
        }
        Instantiate(stoneBall, transform.position, transform.rotation);
        _mainCamera.GetComponent<HorzScroll>().setInitialCamera();
    }
}
