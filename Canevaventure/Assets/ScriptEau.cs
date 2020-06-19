using UnityEngine;

public class ScriptEau : MonoBehaviour
{
    [SerializeField]
    private Transform Spawn = null;
    [SerializeField]
    private GameObject Joueur = null;
    private Transform TransformJoueur = null;

    private void Start()
    {
        TransformJoueur = Joueur.GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TransformJoueur.position = Spawn.position;
        }
    }
}
