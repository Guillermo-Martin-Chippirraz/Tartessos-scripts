using Firebase;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var status = task.Result;
            if (status == DependencyStatus.Available)
            {
                Debug.Log("Firebase inicializado correctamente");
            }
            else
            {
                Debug.LogError("No se pudo inicializar Firebase: " + status);
            }
        });
    }

}
