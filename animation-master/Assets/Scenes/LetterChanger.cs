using UnityEngine;
using UnityEngine.UI;

public class LetterChanger : MonoBehaviour
{
    public Image[] images; // مجموعة الصور
    private int index = 0; // الفهرس الحالي للصورة

    void Start()
    {
        // تأكد من أن هناك صور في المصفوفة
        if (images.Length == 0)
        {
            Debug.LogError("Images array is empty!");
            return; // Exit early to avoid errors
        }

        // تعيين جميع الصور كغير مرئية عند بداية اللعبة
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }

        // تأكد من أن الفهرس يبدأ من 0 أو الصورة الأولى فقط إذا لزم الأمر
        Debug.Log("Initial Image State: All images are hidden.");
    }

    public void NextImage()
    {
        // تأكد من أن هناك صور في المصفوفة
        if (images.Length == 0)
        {
            Debug.LogError("Images array is empty!");
            return; // Exit early to avoid errors
        }

        // تعيين جميع الصور كغير مرئية
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }

        // عرض الصورة التالية
        index = (index + 1) % images.Length; // التنقل بين الصور
        images[index].gameObject.SetActive(true);

        Debug.Log("Current Image Index: " + index);
    }

    public void PreviousImage()
    {
        // تأكد من أن هناك صور في المصفوفة
        if (images.Length == 0)
        {
            Debug.LogError("Images array is empty!");
            return; // Exit early to avoid errors
        }

        // تعيين جميع الصور كغير مرئية
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(false);
        }

        // عرض الصورة السابقة
        index = (index - 1 + images.Length) % images.Length; // التنقل بين الصور
        images[index].gameObject.SetActive(true);

        Debug.Log("Current Image Index: " + index);
    }
}
