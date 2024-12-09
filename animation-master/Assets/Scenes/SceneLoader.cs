using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // تأكد من أن هذه الدالة public
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
    public void Alphapitic()
    {
        SceneManager.LoadScene("Alphapitic");
    }
    public void Numbers()
    {
        SceneManager.LoadScene("Numbers");
    }
    public void QuizNumbers()
    {
        SceneManager.LoadScene("QuizNumbers");
    }
    public void QuizAlphabetic()
    {
        SceneManager.LoadScene("QuizAlphabetic");
    }
    public void Quiz()
    {
        SceneManager.LoadScene("Quiz");
    }


}
