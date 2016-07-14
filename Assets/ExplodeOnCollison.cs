using UnityEngine;
using System.Collections;

public class ExplodeOnCollison : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Transform otherTransform = other.transform.parent;
        Transform thisTransform = this.transform.parent;
        Transform otherParent = otherTransform.parent;
        Transform thisParent = thisTransform.parent;
        if (other.name == "Leaf" ||
            otherParent == thisParent ||
            otherTransform == thisParent ||
            otherParent == thisTransform
            )
        {
            return;
        }
        Destroy(thisTransform.gameObject);
        
    }
}
