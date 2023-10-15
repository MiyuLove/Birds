using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool clickedOn = false;
    private Ray _rayToCatapult;
    private float _maxLength = 3f;
    public Transform _zeroPoint;
    private Rigidbody2D _rb2d;
    private SpringJoint2D _spring;
    private Vector2 _prev_velocity;
    private LineRenderer _lineback, _linefore;
    private bool _isShowLine = true;

    void Start()
    {
        gameObject.name = "StoneBall";
        _zeroPoint = GameObject.Find("CatapultPosition").GetComponent<Transform>();
        _rayToCatapult = new Ray(_zeroPoint.position, Vector3.zero);
        _rb2d = GetComponent<Rigidbody2D>();
        _spring = GetComponent<SpringJoint2D>();
        _lineback = GameObject.Find("LineBack").GetComponent<LineRenderer>();
        _linefore = GameObject.Find("LineFore").GetComponent<LineRenderer>();
        GameObject.FindWithTag("MainCamera").GetComponent<HorzScroll>().
            setPov(gameObject);
    }

    private void Update()
    {
        if (clickedOn)
        {
            Vector3 mouseWorldPoint =
                Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 _newVector = mouseWorldPoint - _zeroPoint.position;
            if (_newVector.sqrMagnitude > _maxLength * _maxLength)
            {
                _rayToCatapult.direction = _newVector;
                mouseWorldPoint = _rayToCatapult.GetPoint(_maxLength);
            }

            mouseWorldPoint.z = 0f;
            transform.position = mouseWorldPoint;
        }

        if(_spring != null)
        {
            if(_prev_velocity.sqrMagnitude > _rb2d.velocity.sqrMagnitude)
            {
                Destroy(_spring);
                deleteLine();
                _rb2d.velocity = _prev_velocity;
                Invoke("makeNewStone", 7f);
            }

            if (clickedOn == false) _prev_velocity = _rb2d.velocity;
        }
        updateLine();
    }

    private void OnMouseDown()
    {
        if (_spring == null || ManageAngry.inst.gameMode != 1) return;
        clickedOn = true;
    }

    private void OnMouseUp()
    {
        if (_spring == null || ManageAngry.inst.gameMode != 1) return;
        clickedOn = false;
        _rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    void updateLine()
    {
        if (!_isShowLine) return;

        _lineback.SetPosition(1, transform.position);
        _linefore.SetPosition(1, transform.position);
    }

    void deleteLine()
    {
        _isShowLine = false;
        _lineback.gameObject.SetActive(false);
        _linefore.gameObject.SetActive(false);
    }

    public void makeNewStone()
    {
        if (_isShowLine) return;

        _lineback.gameObject.SetActive(true);
        _linefore.gameObject.SetActive(true);

        GameObject.Find("Stone").GetComponent<makeStone>().makeStoneBall();
        
        Destroy(gameObject);
    }
}
