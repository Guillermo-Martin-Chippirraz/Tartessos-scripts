using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem; // nuevo Input System

public class ApiController : MonoBehaviour
{
    public ApiClient apiClient;
    public SignUpValidator validator;
    public LogInNavigationController navigation;

    public TMP_InputField loginIdentifierField;
    public TMP_InputField loginPasswordField;
    public TMP_Text feedbackText;
    public TextMeshProUGUI bienvenidaText;

    // Acción de entrada para detectar click
    private InputAction clickAction;

    private void Awake()
    {
        // Configuramos la acción para el botón izquierdo del ratón
        clickAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");
    }

    private void OnEnable()
    {
        clickAction.Enable();
        clickAction.performed += OnClickPerformed;
    }

    private void OnDisable()
    {
        clickAction.performed -= OnClickPerformed;
        clickAction.Disable();
    }

    public void OnLoginButton()
    {
        string identifier = loginIdentifierField.text;
        string password = loginPasswordField.text;

        feedbackText.text = "Intentando login...";
        StartCoroutine(apiClient.Login(identifier, password, (succes, tokenOrMessage) =>
        {
            if (succes)
            {
                PlayerPrefs.SetString("jwt_token", tokenOrMessage);
                PlayerPrefs.Save();
                MostrarBienvenida("¡Bienvenido/a/e de nuevo, " + identifier + "!");
            }
            else
            {
                feedbackText.text = "<color=red>" + tokenOrMessage + "</color>";
            }
        }));
    }

    public void OnSignupButton()
    {
        if (validator.ValidatePanel1() && validator.ValidatePanel2() && validator.ValidatePanel3())
        {
            string username = validator.usernameField.text;
            string email = validator.emailField.text;
            string password = validator.passwordField.text;
            string birthdate = validator.birthdateField.text;
            string idioma = validator.languageDropdown.options[validator.languageDropdown.value].text;
            string zonaHoraria = validator.timezoneDropdown.options[validator.timezoneDropdown.value].text;

            var discapacidades = new System.Collections.Generic.List<string>();
            if (validator.colorblindnessToggle.isOn) discapacidades.Add("daltonismo");
            if (validator.lowVisionToggle.isOn) discapacidades.Add("bajaVision");
            if (validator.hypoacusticToggle.isOn) discapacidades.Add("hipoacusia");
            if (validator.deafToggle.isOn) discapacidades.Add("sordera");
            if (validator.dislexya.isOn) discapacidades.Add("dislexia");
            if (!string.IsNullOrEmpty(validator.anotherDisability.text)) discapacidades.Add(validator.anotherDisability.text);

            feedbackText.text = "Registrando usuario...";
            StartCoroutine(apiClient.SignUp(username, email, password, birthdate, idioma, zonaHoraria, discapacidades.ToArray()));
        }
        else
        {
            feedbackText.text = "<color=red> Validación fallida en algún panel </color>";
        }
    }

    public void OnForgotPasswordButton()
    {
        string email = validator.emailField.text;
        feedbackText.text = "Enviando correo de recuperación...";
        StartCoroutine(apiClient.ForgotPassword(email));
    }

    private void MostrarBienvenida(string mensaje)
    {
        bienvenidaText.text = mensaje + "\n\nHaz click en cualquier lugar para continuar";
        bienvenidaText.gameObject.SetActive(true);
    }

    // Callback del nuevo Input System
    private void OnClickPerformed(InputAction.CallbackContext ctx)
    {
        if (bienvenidaText.gameObject.activeSelf)
        {
            bienvenidaText.gameObject.SetActive(false);
            navigation.IrALogin();
        }
    }
}
