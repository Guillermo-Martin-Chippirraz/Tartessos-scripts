using UnityEngine;
using Sunbox.Avatars;

[System.Serializable]
public class AvatarConfigData
{
    public float BodyHeight;
    public float BodyFat;
    public float BodyMuscle;
    public float BreastSize;

    public float NoseLength;
    public float LipsWidth;
    public float JawWidth;
    public float BrowWidth;
    public float BrowHeight;
    public float EyesSize;
    public float EyesClosedDefault;

    public int SkinMaterialIndex;
    public int NailsMaterialIndex;
    public int EyeMaterialIndex;
    public int BrowMaterialIndex;
    public int LashesMaterialIndex;

    public int HairStyleIndex;
    public int HairMaterialIndex;

    public int FacialHairStyleIndex;
    public int FacialHairMaterialIndex;

    public int ClothingItemHatVariationIndex;
    public int ClothingItemTopVariationIndex;
    public int ClothingItemBottomVariationIndex;
    public int ClothingItemGlassesVariationIndex;
    public int ClothingItemShoesVariationIndex;

    public AvatarCustomization.AvatarGender CurrentGender;
}

public static class AvatarConfigLoader
{
    public static void ApplyConfig(AvatarCustomization avatar, string json)
    {
        AvatarConfigData data = JsonUtility.FromJson<AvatarConfigData>(json);

        avatar.BodyHeight = data.BodyHeight;
        avatar.BodyFat = data.BodyFat;
        avatar.BodyMuscle = data.BodyMuscle;
        avatar.BreastSize = data.BreastSize;

        avatar.NoseLength = data.NoseLength;
        avatar.LipsWidth = data.LipsWidth;
        avatar.JawWidth = data.JawWidth;
        avatar.BrowWidth = data.BrowWidth;
        avatar.BrowHeight = data.BrowHeight;
        avatar.EyesSize = data.EyesSize;
        avatar.EyesClosedDefault = data.EyesClosedDefault;

        avatar.SkinMaterialIndex = data.SkinMaterialIndex;
        avatar.NailsMaterialIndex = data.NailsMaterialIndex;
        avatar.EyeMaterialIndex = data.EyeMaterialIndex;
        avatar.BrowMaterialIndex = data.BrowMaterialIndex;
        avatar.LashesMaterialIndex = data.LashesMaterialIndex;

        avatar.HairStyleIndex = data.HairStyleIndex;
        avatar.HairMaterialIndex = data.HairMaterialIndex;

        avatar.FacialHairStyleIndex = data.FacialHairStyleIndex;
        avatar.FacialHairMaterialIndex = data.FacialHairMaterialIndex;

        avatar.ClothingItemHatVariationIndex = data.ClothingItemHatVariationIndex;
        avatar.ClothingItemTopVariationIndex = data.ClothingItemTopVariationIndex;
        avatar.ClothingItemBottomVariationIndex = data.ClothingItemBottomVariationIndex;
        avatar.ClothingItemGlassesVariationIndex = data.ClothingItemGlassesVariationIndex;
        avatar.ClothingItemShoesVariationIndex = data.ClothingItemShoesVariationIndex;

        avatar.SetGender(data.CurrentGender, true);

        avatar.UpdateCustomization();
        avatar.UpdateClothing();
    }
}
