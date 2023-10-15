using UnityEngine;

public class AngryBird : MonoBehaviour
{
    private Animator _anim;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _anim.enabled = false;
    }

    public void SetDestroy()
    {
        Destroy(gameObject);
    }
}
