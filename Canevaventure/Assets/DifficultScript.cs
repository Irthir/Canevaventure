using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class DifficultScript : MonoBehaviour
{
    private Image MonImage;
    private Button MonBouton;
    public bool bDifficile=false;


    private void Start()
    {
        MonBouton = GetComponent<Button>();
        MonImage = MonBouton.GetComponent<Image>();
    }

    public void DifficultChange()
    {
        bDifficile = !bDifficile;
        if (bDifficile)
        {
            MonImage.color = Color.red;
        }
        else
        {
            MonImage.color = Color.white;
        }
    }
    
}
