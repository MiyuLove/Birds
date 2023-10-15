using UnityEngine;

public class ManageCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "StoneBall")
        {
            if (gameObject.tag == "Enemy")
            {
                ManageGame.inst.SetAddScore();
                ManageAngry.angryInst.CatchBird();
            }
            Destroy(gameObject);
        }
    }
}
