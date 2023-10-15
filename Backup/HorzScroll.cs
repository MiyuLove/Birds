using HorzTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class HorzScroll : HScroll
{
    private float limitX, startX;
    [HideInInspector] public bool previewing = false, focusing = false;
    private GameObject pivotObject;

    void Start()
    {
        startX = transform.position.x;//in angry bird is zero-Vector
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        if (ManageGame.inst != null && ManageGame.inst.isGameOver)
            setStop();
        
        if (previewing)
        {
            if (transform.position.x >= limitX)
            {
                setSpeed(6f);
            }

            if(transform.position.x < 0)
            {
                setStop();
                pivotView(true);
                ManageAngry.inst.gameMode = 1;
                //When preview is finished can start game
            }
        }

        if (focusing &&
            pivotObject.transform.position.x > 1f &&
            pivotObject.transform.position.x < 20.48f)
        {
            transform.position = new Vector3(pivotObject.transform.position.x, 0, -10f);
        }
    }

    void GameStart()
    {
        setRigidbody(2f);
    }

    public void GameStart(float _xP, GameObject pov)
    {
        limitX = _xP;
        transform.position = new Vector3(0, 0, -10f);
        setRigidbody(-6f);
        pivotView(false);
        pivotObject = pov;
    }

    public void setPov(GameObject pov)
    {
        pivotObject = pov;
    }

    void pivotView(bool t)
    {
        previewing = !t;
        focusing = t;
    }

    public void setInitialCamera()
    {
        transform.position = new Vector3(0, 0, -10f);
    }

    public void setGameOver()
    {
        focusing = false;
    }
}