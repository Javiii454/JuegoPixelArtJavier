using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiarNivel : MonoBehaviour

{
 
public GameObject victoriaCanvas;
    

 void OnTriggerEnter2D(Collider2D collider)
 {
    if(collider.gameObject.CompareTag("Player"))
    {
    victoriaCanvas.SetActive(true);
    //StartCoroutine(FinishLevel());
    
    }
    
 }
 
 /*float finishDelay = 5;
 IEnumerator FinishLevel()
 {
    yield return new WaitForSeconds(finishDelay);
    SceneManager.LoadScene(1);
 }*/

 public void ChangeLevel(int sceneIndex)
 {
    SceneManager.LoadScene(sceneIndex);
 }
 

}
