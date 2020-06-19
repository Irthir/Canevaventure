using UnityEngine;
using UnityEngine.UI;


public class CompareScript : MonoBehaviour
{
    private DifficultScript monScript;
    private Mouvement JoueurScript;
    private ScriptPickup CoffreScript;

    void Start()
    {
        monScript = GameObject.Find("Bouton_Difficult").GetComponent<DifficultScript>();
        JoueurScript = GameObject.Find("Personnage").GetComponent<Mouvement>();
        CoffreScript = GameObject.Find("Coffre").GetComponent<ScriptPickup>();
    }

    [SerializeField]
    private Text TextScore = null;


    [SerializeField]
    private Texture2D second = null;
    [SerializeField]
    private Texture2D first = null;
    [SerializeField]
    private bool bDifficult = false;
    [SerializeField]
    private GameObject PatronObjet = null;


    public void CompareTexture()
    {
        bDifficult = monScript.bDifficile;

        second = PatronObjet.GetComponent<SpriteRenderer>().sprite.texture;

        Color[] firstPix = first.GetPixels();
        Color[] secondPix = second.GetPixels();

        int nTotal = 0;
        int nEgale = 0;
        float fResult = 0.0f;
        int nResult = 0;

        if (bDifficult)
        {
            for (int nI = 0; nI < firstPix.Length; nI++)
            {
                if (!(firstPix[nI] == Color.white && secondPix[nI] == Color.white))
                {
                    if (firstPix[nI] == secondPix[nI])
                    {
                        nEgale++;
                    }
                    nTotal++;
                }
            }
        }
        else
        {
            for (int nI = 0; nI < firstPix.Length; nI++)
            {
                if (firstPix[nI] == secondPix[nI])
                {
                    nEgale++;
                }
                nTotal++;
            }
        }

        if (nTotal <= 0)
        {
            fResult = 100.00f;
        }
        else
        {
            fResult = (float)nEgale / (float)nTotal;
            fResult = fResult * 10000;
            nResult = (int)fResult;
            fResult = (float)nResult;
            fResult = fResult / 100;
        }


        Debug.Log("nEgale = " + nEgale + " nTotal = " + nTotal + ".");
        Debug.Log("Resultat : " + fResult + "%.");
        TextScore.text = "Score : " + fResult + "%";

        MouvementActif(second);
    }

    private void MouvementActif (Texture Patron)
    {
        if(Patron.name=="Triangle2") //Rond2 Fleche_Droite
        {
            JoueurScript.bSaut = true;
            JoueurScript.bDroite = true;
        }
        if(Patron.name=="Fleche_Droite")
        {
            JoueurScript.bDroite = true;
        }
        if(Patron.name=="Rond2")
        {
            CoffreScript.bActivation = true;
        }
    }
}
