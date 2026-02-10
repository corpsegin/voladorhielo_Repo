using UnityEngine;

public class ObjetosRecogibles : MonoBehaviour
{
    public BoxCollider2D proximityCollider;

    public Animator anim;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (other.IsTouching(proximityCollider))
        {
            anim.SetBool("JugadorCerca", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (!other.IsTouching(proximityCollider))
        {
            anim.SetBool("JugadorCerca", false);
        }
    }

}
