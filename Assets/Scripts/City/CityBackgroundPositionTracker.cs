using System;
using UnityEngine;

namespace SugarHitBaby
{
    public class CityBackgroundPositionTracker : MonoBehaviour
    {
        [SerializeField] private Ground ground;
        [SerializeField] private GameObject cityBlock;

        private float positionXOffset;

        private void Start()
        {
            SetPositionOffset();
        }

        private void SetPositionOffset()
        {
            positionXOffset = ground.GetComponent<Renderer>().bounds.size.x;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Player>(out Player player))
            {
                Debug.Log("Player registered");
                cityBlock.transform.position = new Vector3(
                    cityBlock.transform.position.x + (positionXOffset * 2), 
                    cityBlock.transform.position.y,
                    cityBlock.transform.position.z);
            }
        }
    }
}

