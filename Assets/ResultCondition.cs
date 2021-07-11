using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class ResultCondition : MonoBehaviour {
    public LayerMask triggerLayerMask;
    public ResultType resultType;

    Collider _collider;

    private void Awake() {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (MyLayerMask.IsInMask(triggerLayerMask, other.gameObject.layer))
        {
            EventSystem.current.BroadcastMessage("GameOver", resultType);
        }
    }
}