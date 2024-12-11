using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public Image questionImage;
    public Button[] answerButtons;
    private int correctAnswerIndex;

    public void SetQuestion(string question, Sprite image, string[] answers, int correctIndex)
    {
        questionText.text = question;
        questionImage.sprite = image;

        foreach (Button btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white; // إعادة تعيين اللون
            btn.onClick.RemoveAllListeners(); // تنظيف المستمعين
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = answers[i];
            int index = i;
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }

        correctAnswerIndex = correctIndex;
    }

    public void CheckAnswer(int selectedIndex)
    {
        if (selectedIndex == correctAnswerIndex)
        {
            Debug.Log("Correct!");
            answerButtons[selectedIndex].GetComponent<Image>().color = Color.green; // إجابة صحيحة
        }
        else
        {
            Debug.Log("Wrong!");
            answerButtons[selectedIndex].GetComponent<Image>().color = Color.red; // إجابة خاطئة
        }
    }

    void Start()
    {
        string question = "What letter is this?";
        Sprite image = Resources.Load<Sprite>("Path/To/Your/Image");
        string[] answers = { "A", "B", "C" };
        int correctIndex = 0;

        SetQuestion(question, image, answers, correctIndex);
    }
}
