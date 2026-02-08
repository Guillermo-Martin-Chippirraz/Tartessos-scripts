using UnityEngine;
using Sunbox.Avatars;

public class AvatarSpawner : MonoBehaviour
{
    public GameObject avatarPrefab;
    private AvatarCustomization avatar;

    public void Spawn()
    {
        GameObject instance = Instantiate(avatarPrefab);
        instance.transform.position = transform.position;
        instance.transform.rotation = transform.rotation;
        avatar = instance.GetComponent<AvatarCustomization>();
    }

    public AvatarCustomization GetAvatar()
    {
        return avatar;
    }
}
