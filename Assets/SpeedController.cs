using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpeedController : MonoBehaviour {
    public AnimationCurve curve;
    public float SpeedUpTotalTime = 10;
    public float FinalSpeed = 13;
#if UNITY_EDITOR
    public float DebugTimeAccumulator;
    public float DebugSpeed;
#endif

    float timeAccumulator;
    Rigidbody body;

    private void Awake() {
        body = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        var coefficient = timeAccumulator / SpeedUpTotalTime;
        body.velocity = new Vector3(0, 0, curve.Evaluate(coefficient) * FinalSpeed);
        timeAccumulator += Time.fixedDeltaTime;
#if UNITY_EDITOR
        DebugTimeAccumulator = timeAccumulator;
        DebugSpeed = body.velocity.z;
#endif
    }
}