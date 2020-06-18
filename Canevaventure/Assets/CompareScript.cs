using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CompareScript : MonoBehaviour
{
    private DifficultScript monScript;
    
    void Start()
    {
        monScript = GameObject.Find("Bouton_Difficult").GetComponent<DifficultScript>();
    }


    [SerializeField]
    private Texture2D second=null;
    [SerializeField]
    private Texture2D first=null;
    [SerializeField]
    private bool bDifficult = false;


    public void CompareTexture()
    {
        bDifficult = monScript.bDifficile;

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
        
        fResult = (float)nEgale / (float)nTotal;
        fResult = fResult * 10000;
        nResult = (int)fResult;
        fResult = (float)nResult;
        fResult = fResult / 100;

        Debug.Log("nEgale = " + nEgale + " nTotal = " + nTotal + ".");
        Debug.Log("Resultat : " + fResult + "%.");

    }
}
