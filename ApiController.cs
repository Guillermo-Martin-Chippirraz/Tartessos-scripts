using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem; // nuevo Input System
using UnityEngine.SceneManagement;

public class ApiController : MonoBehaviour
{
    public ApiClient apiClient;
    public SignUpValidator validator;
    public LogInNavigationController navigation;

    public TMP_InputField loginIdentifierField;
    public TMP_InputField loginPasswordField;
    public TMP_Text feedbackText;
    public TextMeshProUGUI bienvenidaText;
    public TextMeshProUGUI pulsaParaEntrarText;

    public Camera camera;
    public float distancia = 10f;
    public float duracion = 0.8f;
    public string siguienteEscena ="MenuPartida";

    private bool loginCompletado = false;


    // Acción de entrada para detectar click
    private InputAction clickAction;

    private void Awake()
    {
        clickAction = new InputAction(type: InputActionType.Button, binding: "<Mouse>/leftButton");

        // 🔥 Desactivar textos desde el inicio
        bienvenidaText.gameObject.SetActive(false);
        pulsaParaEntrarText.gameObject.SetActive(false);
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
                loginCompletado = true;
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
        if (!loginCompletado) return;

        bienvenidaText.text = mensaje + "\n\nHaz click en cualquier lugar para continuar";
        bienvenidaText.gameObject.SetActive(true);

        pulsaParaEntrarText.text = "Pulsa para entrar";
        pulsaParaEntrarText.gameObject.SetActive(true);
    }

    // Callback del nuevo Input System
    private void OnClickPerformed(InputAction.CallbackContext ctx)
    {
        if (!loginCompletado) return;
        
        if (pulsaParaEntrarText.gameObject.activeSelf)
        {
            pulsaParaEntrarText.gameObject.SetActive(false);
            bienvenidaText.gameObject.SetActive(false);
            
            StartCoroutine(TransicionEntrada());
        }
    }

    private IEnumerator TransicionEntrada()
    {
        Vector3 inicio = camera.transform.position;
        Vector3 destino = inicio + camera.transform.forward * distancia;

        float tiempo = 0f;

        while(tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float t = tiempo / duracion;

            camera.transform.position = Vector3.Lerp(inicio, destino, t);

            yield return null;
        }

        SceneManager.LoadScene(siguienteEscena);
    }
}
