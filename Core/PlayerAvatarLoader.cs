using UnityEngine;
using Sunbox.Avatars;

public class PlayerAvatarLoader : MonoBehaviour
{
    public GameObject avatarPrefab;
    void Start()
    {
        GameObject instance = Instantiate(avatarPrefab, transform.position, transform.rotation);
        AvatarCustomization avatar = instance.GetComponent<AvatarCustomization>();

        string json = PlayerPrefs.GetString("PlayerAvatarConfig", "");

        if(!string.IsNullOrEmpty(json))
        {
            AvatarConfigLoader.ApplyConfig(avatar, json);
        }
    }
}
