using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bird : MonoBehaviour {
    public float upForce = 200f;
    private Rigidbody2D _rb2d;
    private Animator anim;

    private PolygonCollider2D coll;
    private SpriteRenderer renderer;

    private bool isBlink = false, isShow = true;
    private int blinkCount = 5;
    private float fBlink = 3f;

    void Start () {
        _rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        _rb2d.bodyType=RigidbodyType2D.Kinematic;

        renderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<PolygonCollider2D>();
        coll.isTrigger = true;
    }

    void Update () {
        if (ManageGame.inst.gameMode!=1) return;
        
        if (Input.GetMouseButtonDown (0)) {
            _rb2d.velocity = Vector2.zero;
            _rb2d.AddForce (new Vector2 (0f, upForce));
            anim.SetTrigger ("SetFlap");
        }
        
        BirdBlink(); 
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name.Contains("Ground")) {
            coll.isTrigger=false;
            anim.SetTrigger ("SetDie"); 
            ManageGame.inst.SetGameOver ();
            return;
        }

        if (isBlink) return;
        if (col.gameObject.tag != "Column") return;

        isBlink = true;
        fBlink = 3f;
        ManageGame.inst.SetLifeDown();
    }

    void OnCollisionEnter2D (Collision2D other) {
        anim.SetTrigger ("SetDie"); 
        ManageGame.inst.SetGameOver ();
    }

    void GameStart() {
        _rb2d.bodyType=RigidbodyType2D.Dynamic;
    }

    void BirdBlink() {
        if (!isBlink) return; 

        if (--blinkCount <= 0) {
            blinkCount = 5;
            if (isShow = !isShow) renderer.color = Color.white;
            else renderer.color = Color.clear;
        }

        fBlink -= Time.deltaTime;
        if (fBlink < 0f) { 
            isBlink = false;
            renderer.color = Color.white;
            if (ManageGame.inst.Life==0) coll.isTrigger=false;
        }
    }
}