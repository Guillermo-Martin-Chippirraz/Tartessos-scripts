using UnityEngine;

public class LogInNavigationController : MonoBehaviour
{
    public SignUpValidator validator;
    public RectTransform panelLogin;
    public RectTransform panelSignUpFirst;
    public RectTransform panelSignUpSecond;
    public RectTransform panelSignUpThird;
    public RectTransform panelRecuperacion;

    public Vector3 scaleLogin = new Vector3(0.25f, 0.7352941f, 1f);
    public Vector3 scaleSignUp = new Vector3(0.25f, 1f, 1f);
    public Vector3 scaleRecovery = new Vector3(0.25f, 0.7352941f, 1f);
    public Vector3 scaleSignUpThird = new Vector3(0.25f, 0.5f, 1f);

    private RectTransform panelActual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        // Panel inicial visible
        panelActual = panelLogin;
        panelLogin.localScale = scaleLogin;
        panelSignUpFirst.localScale = Vector3.zero;
        panelSignUpSecond.localScale = Vector3.zero;
        panelSignUpThird.localScale = Vector3.zero;

        panelRecuperacion.localScale = Vector3.zero;

        panelLogin.gameObject.SetActive(true);
        panelSignUpFirst.gameObject.SetActive(false);
        panelSignUpSecond.gameObject.SetActive(false);
        panelSignUpThird.gameObject.SetActive(false);
        panelRecuperacion.gameObject.SetActive(false);
    }

    public void CambiarPanel(RectTransform nuevoPanel, Vector3 targetScale) {
        // Cierra el panel actual
        LeanTween.scale(panelActual, Vector3.zero, 0.4f).setEaseInOutQuad()
            .setOnComplete(() => {
                panelActual.gameObject.SetActive(false);

                // Abre el nuevo panel
                nuevoPanel.gameObject.SetActive(true);
                nuevoPanel.localScale = Vector3.zero;
                LeanTween.scale(nuevoPanel, targetScale, 0.4f).setEaseInOutQuad();

                panelActual = nuevoPanel;
            });
    }

    public void IrASignUpFirst()
    {
        CambiarPanel(panelSignUpFirst, scaleSignUp);
    }

    public void IrASignUpSecond()
    {
        if (validator.ValidatePanel1())
        {
            CambiarPanel(panelSignUpSecond, scaleSignUp);
        }
        else
        {
            Debug.Log("Panel 1 no válido");
        }
    }

    public void IrASignUpThird()
    {
        if (validator.ValidatePanel2())
        {
            CambiarPanel(panelSignUpThird, scaleSignUpThird);
        }
        else
        {
            Debug.Log("Panel 2 no válido");
        }
    }

    public void FinalizarRegistro()
    {
        if (validator.ValidatePanel3())
        {
            Debug.Log("Registro completado");
        }
        else
        {
            Debug.Log("Panel 3 no válido");
        }
    }
    public void IrARecuperacion()
    {
        CambiarPanel(panelRecuperacion, scaleRecovery);
    }

    public void IrALogin()
    {
        CambiarPanel(panelLogin, scaleLogin);
    }
}
