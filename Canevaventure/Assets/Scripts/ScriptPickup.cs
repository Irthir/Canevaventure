using UnityEngine;

public class ScriptPickup : MonoBehaviour
{
    [SerializeField]
    Animator Animateur = null;
    bool tempbool = true;
    [SerializeField]
    GameObject Fin = null;
    public bool bActivation = false;
    private bool bCollide=false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bCollide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bCollide = false;
        }
    }

    private void Update()
    {
        if (bActivation && bCollide)
        {
            Animateur.SetBool("Ouvert", true);
            Animateur.SetTrigger("Ouverture");
            tempbool = false;
        }
        else
        {
            bActivation = false;
        }

        if (!tempbool)
        {
            Fin.SetActive(true);
        }
    }
}
