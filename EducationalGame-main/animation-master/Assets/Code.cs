using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeneManager : MonoBehaviour
{
    // متغير لتخزين اسم الشخصية التي اختارها المستخدم
    public string selectedCharacter;

    // صور الشخصيات
    public Sprite character1Image;
    public Sprite character2Image;
    public Sprite character3Image;

    // أزرار الاختيار
    public Button character1Button;
    public Button character2Button;
    public Button character3Button;

    // الصورة التي سيتم عرضها في واجهة المستخدم (UI) لتحديد الشخصية المختارة
    public Image characterDisplayImage;

    void Start()
    {
        // تعيين الصور لكل زر عند بداية المشهد
        character1Button.GetComponent<Image>().sprite = character1Image;
        character2Button.GetComponent<Image>().sprite = character2Image;
        character3Button.GetComponent<Image>().sprite = character3Image;

        // إضافة الأحداث للأزرار
        character1Button.onClick.AddListener(() => SelectCharacter("Character1"));
        character2Button.onClick.AddListener(() => SelectCharacter("Character2"));
        character3Button.onClick.AddListener(() => SelectCharacter("Character3"));

        // تحميل الشخصية المختارة إذا كانت مخزنة
        LoadCharacter();
    }

    // دالة لاختيار شخصية معينة
    public void SelectCharacter(string characterName)
    {
        selectedCharacter = characterName;  // تخزين اسم الشخصية
        Debug.Log("تم اختيار الشخصية: " + selectedCharacter);

        // تحديث الصورة في واجهة المستخدم لعرض الشخصية المختارة
        UpdateCharacterImage();

        // حفظ الشخصية المختارة باستخدام PlayerPrefs
        SaveCharacter();

        // الانتقال إلى المشهد التالي (أو مشهد اللعبة)
        goToScene("GameScene");
    }

    // دالة للانتقال إلى مشهد آخر
    public void goToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // دالة لاختبار الشخصية المختارة
    public void TestSelectedCharacter()
    {
        Debug.Log("الشخصية المختارة هي: " + selectedCharacter);
    }

    // دالة لحفظ الشخصية المختارة باستخدام PlayerPrefs
    public void SaveCharacter()
    {
        PlayerPrefs.SetString("SelectedCharacter", selectedCharacter);
        PlayerPrefs.Save();
        Debug.Log("تم حفظ الشخصية: " + selectedCharacter);
    }

    // دالة لتحميل الشخصية من PlayerPrefs
    public void LoadCharacter()
    {
        if (PlayerPrefs.HasKey("SelectedCharacter"))
        {
            selectedCharacter = PlayerPrefs.GetString("SelectedCharacter");
            Debug.Log("تم تحميل الشخصية: " + selectedCharacter);

            // تحديث الصورة في واجهة المستخدم بناءً على الشخصية المحفوظة
            UpdateCharacterImage();
        }
        else
        {
            Debug.Log("لا توجد شخصية مخزنة.");
        }
    }

    // دالة لتحديث الصورة في واجهة المستخدم بناءً على الشخصية المختارة
    private void UpdateCharacterImage()
    {
        switch (selectedCharacter)
        {
            case "Character1":
                characterDisplayImage.sprite = character1Image;
                break;
            case "Character2":
                characterDisplayImage.sprite = character2Image;
                break;
            case "Character3":
                characterDisplayImage.sprite = character3Image;
                break;
            default:
                characterDisplayImage.sprite = null;
                break;
        }
    }
}
