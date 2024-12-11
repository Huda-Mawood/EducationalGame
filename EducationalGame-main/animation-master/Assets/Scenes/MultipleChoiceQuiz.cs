using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Question
{
    public string questionText; // نص السؤال
    public string[] answers; // خيارات الإجابة
    public int correctAnswerIndex; // الفهرس للإجابة الصحيحة
    public Sprite questionImage; // صورة السؤال
}

public class MultipleChoiceQuiz : MonoBehaviour
{
    public Text questionText; // لربط نص السؤال
    public Button[] answerButtons; // لربط الأزرار
    public Text resultText; // لربط نص النتيجة
    public Button nextButton; // زر الانتقال إلى السؤال التالي
    public Button submitButton; // زر التقديم في النهاية
    public Image questionImage; // لربط صورة السؤال
    public Text timerText; // لربط الوقت المتبقي
    public Text scoreText; // لربط النص الخاص بالاسكور
    public GameObject endQuizPanel; // لوحة عرض النتيجة في نهاية الكويز
    public Text finalScoreText; // لربط النص الخاص بالاسكور النهائي في لوحة النهاية

    public List<Question> questions = new List<Question>(); // لربط الأسئلة والإجابات
    private int currentQuestionIndex = 0; // مؤشر السؤال الحالي
    private int score = 0; // الأسكور
    private float timeRemaining = 60f; // الوقت المتبقي (دقيقة واحدة)
    private bool quizCompleted = false; // حالة الكويز إذا انتهى

    void Start()
    {
        // التحقق إذا كانت قائمة الأسئلة فارغة
        if (questions.Count == 0)
        {
            Debug.LogError("No questions available!");
            return;
        }

        nextButton.gameObject.SetActive(false); // إخفاء زر التالي في البداية
        nextButton.onClick.AddListener(NextQuestion); // إضافة حدث الزر
        submitButton.gameObject.SetActive(false); // إخفاء زر التقديم في البداية
        submitButton.onClick.AddListener(EndQuiz); // إضافة حدث زر التقديم
        SetQuestion(); // تعيين السؤال الأول
        endQuizPanel.SetActive(false); // إخفاء لوحة النتيجة في البداية
    }

    void Update()
    {
        if (!quizCompleted)
        {
            // تحديث الوقت المتبقي
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                EndQuiz(); // إنهاء الكويز عند انتهاء الوقت
            }

            // عرض الوقت المتبقي
            timerText.text = "Time: " + Mathf.Round(timeRemaining).ToString() + "s";
        }
    }

    void SetQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            // إعادة تلوين الأزرار إلى اللون الافتراضي
            foreach (Button button in answerButtons)
            {
                button.GetComponentInChildren<Text>().color = Color.black; // إعادة اللون الأسود (الافتراضي)
            }

            // تعيين نص السؤال
            questionText.text = questions[currentQuestionIndex].questionText;

            // تعيين الصورة للسؤال
            questionImage.sprite = questions[currentQuestionIndex].questionImage;

            // تعيين الإجابات للأزرار
            for (int i = 0; i < answerButtons.Length; i++)
            {
                if (i < questions[currentQuestionIndex].answers.Length)
                {
                    int index = i; // نسخ المتغير لتجنب الأخطاء
                    answerButtons[i].GetComponentInChildren<Text>().text = questions[currentQuestionIndex].answers[i];
                    answerButtons[i].onClick.RemoveAllListeners(); // إزالة الاستماع السابق
                    answerButtons[i].onClick.AddListener(() => CheckAnswer(index)); // إضافة الاستماع للنقر
                    answerButtons[i].gameObject.SetActive(true); // تأكيد ظهور الأزرار إذا كانت هناك إجابة
                }
                else
                {
                    answerButtons[i].gameObject.SetActive(false); // إخفاء الأزرار الزائدة إذا كانت هناك إجابات أقل
                }
            }

            // إعادة النص النتيجة إلى فارغ
            resultText.text = "";
            nextButton.gameObject.SetActive(false); // إخفاء زر التالي بعد كل سؤال
            submitButton.gameObject.SetActive(false); // إخفاء زر التقديم حتى الإجابة على كل الأسئلة
        }
    }

    void CheckAnswer(int selectedAnswer)
    {
        // التحقق إذا كانت الإجابة صحيحة
        if (selectedAnswer == questions[currentQuestionIndex].correctAnswerIndex)
        {
            resultText.text = "Correct Answer!"; // عرض النتيجة على الشاشة
            resultText.color = Color.green; // تغيير اللون إلى الأخضر

            // إظهار الإجابة الصحيحة باللون الأخضر
            answerButtons[selectedAnswer].GetComponentInChildren<Text>().color = Color.green;
            score++; // زيادة النقاط
        }
        else
        {
            resultText.text = "Wrong Answer!"; // عرض النتيجة على الشاشة
            resultText.color = Color.red; // تغيير اللون إلى الأحمر

            // إظهار الإجابة الخاطئة باللون الأحمر
            answerButtons[selectedAnswer].GetComponentInChildren<Text>().color = Color.red;

            // إظهار الإجابة الصحيحة باللون الأخضر
            answerButtons[questions[currentQuestionIndex].correctAnswerIndex].GetComponentInChildren<Text>().color = Color.green;
        }

        scoreText.text = "Score: " + score.ToString(); // تحديث النقاط
        nextButton.gameObject.SetActive(true); // إظهار زر التالي بعد الإجابة

        // إذا كانت هذه آخر سؤال، إظهار زر التقديم
        if (currentQuestionIndex == questions.Count - 1)
        {
            submitButton.gameObject.SetActive(true); // إظهار زر التقديم بعد الإجابة على آخر سؤال
        }
    }

    void NextQuestion()
    {
        // الانتقال إلى السؤال التالي
        currentQuestionIndex++;

        // التحقق إذا كنا قد وصلنا إلى آخر سؤال
        if (currentQuestionIndex < questions.Count)
        {
            SetQuestion(); // تعيين السؤال التالي
        }
        else
        {
            EndQuiz(); // إذا انتهت الأسئلة، إنهاء الكويز
        }

        nextButton.gameObject.SetActive(false); // إخفاء زر التالي بعد الانتقال للسؤال الجديد
    }

    void EndQuiz()
    {
        quizCompleted = true; // تم إنهاء الكويز
        resultText.text = "Quiz Completed!"; // عرض رسالة عند الانتهاء من الأسئلة
        finalScoreText.text = "Your final score: " + score.ToString(); // عرض النقاط النهائية في النهاية
        endQuizPanel.SetActive(true); // عرض اللوحة النهائية عند الانتهاء من الكويز
        timerText.gameObject.SetActive(false); // إخفاء المؤقت بعد انتهاء الكويز
        submitButton.gameObject.SetActive(false); // إخفاء زر التقديم بعد إتمام الكويز
    }
}
