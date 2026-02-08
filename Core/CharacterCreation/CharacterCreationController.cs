using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sunbox.Avatars;

public class CharacterCreationController : MonoBehaviour
{
    [Header("Avatar")]
    public AvatarSpawner spawner;
    private AvatarCustomization avatar;
    private bool avatarReady = false;

    [Header("UI")]
    public TMP_InputField nameField;
    public Slider heightSlider;
    public Slider fatSlider;
    public Slider muscleSlider;
    public Slider noseSlider;

    public Button maleButton;
    public Button femaleButton;
    public Button confirmButton;

    private IEnumerator Start()
    {
        // 1) Spawneamos el avatar
        spawner.Spawn();
        avatar = spawner.GetAvatar();

        // 2) Esperamos un frame para que AvatarCustomization.Start() termine
        yield return null;

        avatarReady = true;

        // 3) Conectamos botones DESPUÉS de que el avatar esté listo
        maleButton.onClick.AddListener(SetGenderMale);
        femaleButton.onClick.AddListener(SetGenderFemale);
        confirmButton.onClick.AddListener(Confirm);
    }

    private bool CanUseAvatar()
    {
        return avatarReady && avatar != null && avatar.AvatarReferences != null;
    }

    public void SetHeight(float value)
    {
        if (!CanUseAvatar()) return;

        avatar.BodyHeight = value;
        avatar.UpdateCustomization();
    }

    public void SetFat(float value)
    {
        if (!CanUseAvatar()) return;

        avatar.BodyFat = value;
        avatar.UpdateCustomization();
    }

    public void SetMuscle(float value)
    {
        if (!CanUseAvatar()) return;

        avatar.BodyMuscle = value;
        avatar.UpdateCustomization();
    }

    public void SetNose(float value)
    {
        if (!CanUseAvatar()) return;

        avatar.NoseLength = value;
        avatar.UpdateCustomization();
    }

    public void SetGenderMale()
    {
        if (!CanUseAvatar()) return;

        avatar.SetGender(AvatarCustomization.AvatarGender.Male, true);
        avatar.UpdateCustomization();
        avatar.UpdateClothing();
    }

    public void SetGenderFemale()
    {
        if (!CanUseAvatar()) return;

        avatar.SetGender(AvatarCustomization.AvatarGender.Female, true);
        avatar.UpdateCustomization();
        avatar.UpdateClothing();
    }

    public void Confirm()
    {
        if (!CanUseAvatar()) return;

        string json = AvatarCustomization.ToConfigString(avatar);
        PlayerPrefs.SetString("PlayerAvatarConfig", json);
        PlayerPrefs.SetString("PlayerName", nameField.text);
        PlayerPrefs.Save();

        UnityEngine.SceneManagement.SceneManager.LoadScene("AcademiaScene");
    }
}
