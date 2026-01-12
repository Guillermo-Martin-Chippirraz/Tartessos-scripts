using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using TMPro;
using System.Globalization;

public class SignUpValidator : MonoBehaviour
{
    public TMP_InputField usernameField;
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public TMP_InputField passwordCheckField;
    public TMP_InputField birthdateField;

    public TMP_Text usernameErrorText;
    public TMP_Text emailErrorText;
    public TMP_Text passwordErrorText;
    public TMP_Text confirmPasswordErrorText;
    public TMP_Text birthDateErrorText;
    
    public TMP_Dropdown languageDropdown;
    public TMP_Dropdown timezoneDropdown;
    public Toggle colorblindnessToggle;
    public Toggle lowVisionToggle;
    public Toggle hypoacusticToggle;
    public Toggle deafToggle;
    public Toggle dislexya;
    public TMP_InputField anotherDisability;

    public Toggle confirmReadAndTermsToggle;

    public bool ValidatePanel1()
    {
        bool valid = true;

        usernameErrorText.text = "";
        emailErrorText.text = "";
        passwordErrorText.text = "";
        confirmPasswordErrorText.text = "";
        birthDateErrorText.text = "";

        if (string.IsNullOrWhiteSpace(usernameField.text))
        {
            usernameErrorText.text = "El nombre de usuario es obligatorio";
            Debug.Log("Usuario vacío");
            valid = false;
        }
        if(!Regex.IsMatch(emailField.text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            emailErrorText.text = "Formato de email inválido";
            Debug.LogWarning("Email no válido: " + emailField.text);
            valid = false;
        }
        if(passwordField.text.Length < 6)
        {
            passwordErrorText.text = "La contraseña debe tener al menos 6 caracteres";
            Debug.LogWarning("Contraseña demasiado corta");
            valid = false;
        }
        if(passwordCheckField.text != passwordField.text){
            passwordCheckField.text = "Las contraseñas no coinciden";
            Debug.LogWarning("Las contraseñas no coinciden");
            valid = false;
        }
        if (!ValidateBirthdate(birthdateField.text))
        {
            birthDateErrorText.text = "Fecha no válida o edad insuficiente (mínimo 16 años)";
            Debug.LogWarning("Fecha de nacimiento no válida o menor de edad");
            valid = false;
        }
        Debug.Log("Panel 1 válido");
        return valid;
    }

    private bool ValidateBirthdate(string input)
    {
        string[] formatos = {"dd/MM/yyyy", "yyyy-MM-dd", "MM/dd/yyyy"};
        DateTime birthdate;
        if(!DateTime.TryParseExact(input, formatos, null, System.Globalization.DateTimeStyles.None, out birthdate)) return false;

        int age = DateTime.Now.Year - birthdate.Year;
        if(birthdate > DateTime.Now.AddYears(-age)) age--;

        return age >= 16;
    }

    public bool ValidatePanel2()
    {
        if(languageDropdown.value == 0){
            Debug.LogWarning("Idioma no seleccionado");
            return false;
        }
        if(timezoneDropdown.value == 0)
        {
            Debug.LogWarning("Zona horaria no seleccionada");
            return false;
        }
        Debug.Log("Panel 2 válido");
        return true;
    }

    public bool ValidatePanel3()
    {
        if (!confirmReadAndTermsToggle.isOn)
        {
            Debug.LogWarning("No se confirmó la lectura o no se aceptaron los términos");
            return false;
        }
        Debug.Log("Panel 3 válido");
        return true;
    }
}
