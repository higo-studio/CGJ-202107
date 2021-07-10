using UnityEngine;
public static class MyLayerMask
{
    public static int RaycasatBaseMask { get => LayerMask.GetMask("RaycastBase"); }
    public static int CharacterMask { get => LayerMask.GetMask("Character"); }
    public static int RaycasatBase { get => LayerMask.NameToLayer("RaycastBase"); }
    public static int Character { get => LayerMask.NameToLayer("Character"); }
}