using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DissapearAndAppear : MonoBehaviour
{
    [SerializeField] private GameObject pannelContainer;
    [SerializeField] private float dissapearAndAppearCounter;

    
    void Start()
    {
        StartCoroutine(DissapearAndAppearPannel());
    }

   

    IEnumerator DissapearAndAppearPannel()
    {
        while (true)
        {
            pannelContainer.SetActive(true);
            yield return new WaitForSeconds(dissapearAndAppearCounter);
            pannelContainer.SetActive(false);
            yield return new WaitForSeconds(dissapearAndAppearCounter);
            
            
        }
    }



}
