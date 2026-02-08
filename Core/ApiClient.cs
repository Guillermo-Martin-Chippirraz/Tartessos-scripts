using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;

public class ApiClient : MonoBehaviour
{
    public static string baseURL = "http://localhost:3000";

    public static ApiClient Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator GetPerfil()
    {
        string token = PlayerPrefs.GetString("jwt_token", "");
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("No hay token guardado");
            yield break;
        }

        UnityWebRequest request = UnityWebRequest.Get(baseURL + "/perfil");
        request.SetRequestHeader("Authorization", "Bearer " + token);

        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Perfil: " + request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }

    public IEnumerator Login(string identifier, string password, System.Action<bool, string> callback)
{
    var payload = JsonUtility.ToJson(new LoginRequest { identifier = identifier, password = password });
    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(payload);

    using (UnityWebRequest www = new UnityWebRequest(baseURL + "/login", "POST"))
    {
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            if (www.responseCode == 200)
            {
                // Si el backend devuelve { token: "..." }
                string tokenJson = www.downloadHandler.text;
                // Puedes parsear si lo prefieres:
                // var resp = JsonUtility.FromJson<LoginResponse>(tokenJson);
                // callback?.Invoke(true, resp.token);
                callback?.Invoke(true, tokenJson);
            }
            else if (www.responseCode == 401)
            {
                callback?.Invoke(false, "Credenciales incorrectas");
            }
            else if (www.responseCode == 400)
            {
                callback?.Invoke(false, "Validación fallida");
            }
            else
            {
                callback?.Invoke(false, "Error HTTP: " + www.responseCode);
            }
        }
        else
        {
            callback?.Invoke(false, "Error de red: " + www.error);
        }
    }
}

[System.Serializable]
public class LoginRequest { public string identifier; public string password; }
// [System.Serializable] public class LoginResponse { public string token; }



    public IEnumerator SignUp(string username, string email, string password, string birthdate, string idioma, string zonaHoraria, string[] discapacidades)
    {
        string jsonData = JsonUtility.ToJson(new SignUpData
        {
            username = username,
            email = email,
            password = password,
            birthdate = birthdate,
            idioma = idioma,
            zona_horaria = zonaHoraria,
            discapacidades = discapacidades
        });
        
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

    
        using (UnityWebRequest www = UnityWebRequest.PostWwwForm(baseURL + "/signup", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.Success)
            {
                if(www.responseCode == 200)
                {
                    Debug.Log("Usuario creado correctamente");
                }else if (www.responseCode == 409)
                {
                    Debug.LogWarning("Usuario o correo ya en uso");
                }else if (www.responseCode == 400)
                {
                    Debug.LogWarning("Validación fallida");
                }
            }
            else
            {
                Debug.LogError("Error de red: " + www.error);
            }
        }
    }

    [System.Serializable]
    public class SignUpData
    {
        public string username;
        public string email;
        public string password;
        public string birthdate;
        public string idioma;
        public string zona_horaria;
        public string[] discapacidades;
    }
    public IEnumerator ForgotPassword(string email)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);

        using (UnityWebRequest www = UnityWebRequest.Post(baseURL + "/forgot-password", form))
        {
            yield return www.SendWebRequest();
            
            if(www.result == UnityWebRequest.Result.Success)
            {
                if(www.responseCode == 200)
                {
                    Debug.Log("Correo de recuperación enviado");
                }else if (www.responseCode == 400)
                {
                    Debug.LogWarning("Validación fallida (email inválido)");
                }
                else
                {
                    Debug.LogError("Error de red: " + www.error);
                }
            }
        }
    }

    public IEnumerator GetGameSlots(bool singleSlot, Action<bool, SlotListResponse, string> callback)
    {
        string token = PlayerPrefs.GetString("jwt_token", "");
        if (string.IsNullOrEmpty(token))
        {
            callback?.Invoke(false, null, "No hay token guardado");
            yield break;
        }

        string url = baseURL + "/game/slots?singleSlot=" + (singleSlot ? "true" : "false");

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Authorization", "Bearer " + token);
            www.SetRequestHeader("Content-Type", "application/json");

            if (www.result == UnityWebRequest.Result.Success)
            {
                if (www.responseCode == 200)
                {
                    string json = www.downloadHandler.text;
                    SlotListResponse resp = JsonUtility.FromJson<SlotListResponse>(json);
                    callback?.Invoke(true, resp, null);
                }
                else
                {
                    callback?.Invoke(false, null, "Error HTTP: " + www.responseCode);
                }
            }
            else
            {
                callback?.Invoke(false, null, "Error de red: " + www.error);
            }
        }
    }

    [System.Serializable]
    public class SlotListResponse
    {
        public SlotData[] partidas;
        public bool autoCreated;
    }

    [System.Serializable]
    public class SlotData
    {
        public int id_partida;
        public string personaje_principal;
        public string ultimo_logro;
        public string ultimo_guardado;
        public string snapshot_url;
    }

    public IEnumerator CreatePartida(Action<bool, CreatePartidaResponse, string> callback)
    {
        string token = PlayerPrefs.GetString("jwt_token", "");
        if (string.IsNullOrEmpty(token))
        {
            callback?.Invoke(false, null, "No hay token guardado");
            yield break;
        }

        var payloadObj = new CreatePartidaRequest
        {
            estado = "activa",
            nivel_aventura = 1,
            nivel_mundo = 1,
            perfil_inicial = new PerfilInicial
            {
                avatar = "default.png",
                idioma = "es",
                modo_accesible = false
            }
        };

        string jsonData = JsonUtility.ToJson(payloadObj);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest www = new UnityWebRequest(baseURL + "/partidas", "POST"))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer " + token);

            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.Success)
            {
                if (www.responseCode == 201)
                {
                    string json = www.downloadHandler.text;
                    CreatePartidaResponse resp = JsonUtility.FromJson<CreatePartidaResponse>(json);
                    callback?.Invoke(true, resp, null);
                }
                else
                {
                    callback?.Invoke(false, null, "Error HTTP: " + www.responseCode);
                }
            }
            else
            {
                callback?.Invoke(false, null, "Error de red: " + www.error);
            }
        }
    }

    [System.Serializable]
    public class CreatePartidaRequest
    {
        public string estado;
        public int nivel_aventura;
        public int nivel_mundo;
        public PerfilInicial perfil_inicial;
    }

    [System.Serializable]
    public class PerfilInicial
    {
        public string avatar;
        public string idioma;
        public bool modo_accesible;
    }

    [System.Serializable]
    public class CreatePartidaResponse
    {
        public string message;
        public int id_partida;
    }

    public IEnumerator CargarPartida(int idPartida, Action<bool, GameStateResponse, string> callback)
    {
        string token = PlayerPrefs.GetString("jwt_token", "");
        if (string.IsNullOrEmpty(token))
        {
            callback?.Invoke(false, null, "No hay token guardado");
            yield break;
        }

        string url = baseURL + "/partidas/" + idPartida + "/cargar";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.SetRequestHeader("Authoritazion", "Bearer " + token);
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if (www.responseCode == 200)
                {
                    string json = www.downloadHandler.text;
                    GameStateResponse resp = JsonUtility.FromJson<GameStateResponse>(json);
                    callback?.Invoke(true, resp, null);
                }
                else
                {
                    callback?.Invoke(false, null, "Error HTTP: " + www.responseCode);
                }
            }
            else
            {
                callback?.Invoke(false, null, "Error de red: " + www.error);
            }
        }
    }

    public static UnityWebRequest AuthGet(string url)
    {
        UnityWebRequest req = UnityWebRequest.Get(url);
        req.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
        req.SetRequestHeader("Content-Type", "application/json");
        return req;
    }

    public static UnityWebRequest AuthPost(string url, object body)
    {
        string json = JsonUtility.ToJson(body);
        UnityWebRequest req = new UnityWebRequest(url, "POST");
        byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
        req.uploadHandler = new UploadHandlerRaw(data);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
        req.SetRequestHeader("Content-Type", "application/json");
        return req;
    }

    public static UnityWebRequest AuthPut(string url, object body)
    {
        string json = JsonUtility.ToJson(body);
        UnityWebRequest req = new UnityWebRequest(url, "PUT");
        byte[] data = System.Text.Encoding.UTF8.GetBytes(json);
        req.uploadHandler = new UploadHandlerRaw(data);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("token"));
        req.SetRequestHeader("Content-Type", "application/json");
        return req;
    }

}