using UnityEngine;

public class ScriptPickup : MonoBehaviour
{
    [SerializeField]
    Animator Animateur = null;
    bool tempbool = true;
    [SerializeField]
    GameObject Fin = null;
    public bool bActivation = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (bActivation)
            {
                Animateur.SetBool("Ouvert", true);
                Animateur.SetTrigger("Ouverture");
                tempbool = false;
            }
        }
    }

    private void Update()
    {
        if (!tempbool)
        {
            Fin.SetActive(true);
        }
    }
}
