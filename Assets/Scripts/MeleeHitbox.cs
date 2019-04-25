using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MeleeHitbox : MonoBehaviour
{
    //https://answers.unity.com/questions/1499405/find-all-objects-inside-box-collider.html

    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders() { return colliders; }

    private void OnTriggerEnter(Collider other)
    {
        if (!colliders.Contains(other)) {
            colliders.Add(other);
           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }

}
