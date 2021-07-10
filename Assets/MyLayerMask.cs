using UnityEngine;
public static class MyLayerMask
{
    public static LayerMask NormalPlaneMask { get => LayerMask.GetMask("NormalPlane"); }
    public static LayerMask CharacterMask { get => LayerMask.GetMask("Character"); }
    public static LayerMask CanAttackMask { get => NormalPlaneMask | RailRoadMask; }
    public static LayerMask RailRoadMask { get => LayerMask.GetMask("RailRoad"); }
    public static int NormalPlane { get => LayerMask.NameToLayer("NormalPlane"); }
    public static int Character { get => LayerMask.NameToLayer("Character"); }
    public static int RailRoad { get => LayerMask.NameToLayer("RailRoad"); }

    public static bool IsInMask(int layerMask, int layerindx)
    {
        return (layerMask & (1 << layerindx)) > 0;
    }
}