using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeneManager : MonoBehaviour
{


    public void MyFunction()
    {
        Debug.Log("تم استدعاء الدالة MyFunction");
    }
    public void goToSene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }



}
