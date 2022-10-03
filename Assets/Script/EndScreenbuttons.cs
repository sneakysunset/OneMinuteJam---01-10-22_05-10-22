using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndScreenbuttons : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        int length = 0;
        if (SceneManager.GetActiveScene().name.Length == 6) length = 1;
        else length = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.Substring(0, 5) + (int.Parse(SceneManager.GetActiveScene().name.Substring(5, length)) + 1));
    }

    public void PreviousLevel()
    {
        int length = 0;
        if (SceneManager.GetActiveScene().name.Length == 6) length = 1;
        else length = 2;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.Substring(0, 5) + (int.Parse(SceneManager.GetActiveScene().name.Substring(5, length)) - 1));
    }
}
