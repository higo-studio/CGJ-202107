using UnityEngine;
public static class MyLayerMask
{
    public static int NormalPlaneMask { get => LayerMask.GetMask("NormalPlane"); }
    public static int CharacterMask { get => LayerMask.GetMask("Character"); }
    public static int RailRoadMask { get => LayerMask.GetMask("RailRoad"); }
    public static int NormalPlane { get => LayerMask.NameToLayer("NormalPlane"); }
    public static int Character { get => LayerMask.NameToLayer("Character"); }
    public static int RailRoad { get => LayerMask.NameToLayer("RailRoad"); }

}