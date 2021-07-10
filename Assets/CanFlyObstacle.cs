using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("障碍物/可撞飞")]
public class CanFlyObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    Collider _collider;
    void Start()
    {
        _collider = GetComponent<Collider>() ?? gameObject.AddComponent<BoxCollider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter: " + other.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
