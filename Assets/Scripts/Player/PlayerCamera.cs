using System;
using UnityEngine;

namespace SugarHitBaby
{
    public class PlayerCamera : MonoBehaviour
    {
        private Vector3 offset;

        private float longestDistanceX;

        private void Start()
        {
            SetOffsetToPlayer();
            SetStartPosition();
        }

        private void SetStartPosition()
        {
            longestDistanceX = transform.position.x;
        }

        private void SetOffsetToPlayer()
        {
            offset = Camera.main.transform.position - transform.position;
        }

        private void LateUpdate()
        {
            MoveCameraPosition();
        }

        private void MoveCameraPosition()
        {
            if (!CheckIfCameraCanMove()) return;

            Vector3 newCameraPosition = transform.position + offset;
            Camera.main.transform.position = new Vector3(newCameraPosition.x, 0f, newCameraPosition.z);
        }

        private bool CheckIfCameraCanMove()
        {
            UpdateLongestDistanceXMadeByPlayer();
            return transform.position.x >= longestDistanceX;
        }

        private void UpdateLongestDistanceXMadeByPlayer()
        {
            if (transform.position.x < longestDistanceX) return;

            longestDistanceX = transform.position.x;
        }
    }
}

