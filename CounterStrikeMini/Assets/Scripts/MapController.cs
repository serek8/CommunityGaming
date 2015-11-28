using UnityEngine;
using System.Collections;

namespace CommunityGaming {

    public class MapController : MonoBehaviour {

        public IndestructibleObject[] indestructibleObjects;
        public DestroyableObject[] destroyableObjects;

        public void resetMap() {
            foreach(var destroyableObject in destroyableObjects) {
                destroyableObject.gameObject.SetActive(true);
            }
        }
    }
}
