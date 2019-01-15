using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GGR
{
    public class GGR_CameraMovement : MonoBehaviour
    {
        //Attributes
        public float minYPosition, maxYPosition;
        public Vector2 minPlayerDistance, maxPlayerDistance;
        public List<Transform> cameraWeights;



        private void Start()
        {
            //Lägg in alla spelares transform i listan
        }

        private void Update()
        {
            UpdateHorizontal();
            UpdateVertical();
            transform.position += Vector3.back * Mathf.Tan(Mathf.Deg2Rad * Vector3.Angle(Vector3.down, Camera.main.transform.forward)) * transform.position.y;
        }

        private void UpdateVertical()
        {
            float xDifference =  -(GetMinPosition().x - GetMaxPosition().x);

            float xFactor = (xDifference - minPlayerDistance.x) / (maxPlayerDistance.x - minPlayerDistance.x);

            xFactor = xFactor * (maxPlayerDistance.x - minPlayerDistance.x) + minPlayerDistance.x;

            float zDifference = -(GetMinPosition().y - GetMaxPosition().y);

            float zFactor = (zDifference - minPlayerDistance.y) / (maxPlayerDistance.y - minPlayerDistance.y);

            zFactor = zFactor * (maxPlayerDistance.y - minPlayerDistance.y) + minPlayerDistance.y;

            float yPos = Mathf.Clamp(Mathf.Max(xFactor, zFactor), minYPosition, maxYPosition);

            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }

        private void UpdateHorizontal()
        {
            Vector2 middlePosition = (GetMinPosition() + GetMaxPosition()) / 2;
            transform.position = new Vector3(middlePosition.x, minYPosition, middlePosition.y);
        }

        private Vector2 GetMinPosition()
        {
            Vector2 minPosition = Vector2.positiveInfinity;
            foreach(Transform trans in cameraWeights)
            {
                if(trans.position.x < minPosition.x)
                {
                    minPosition.x = trans.position.x;
                }
                if(trans.position.z < minPosition.y)
                {
                    minPosition.y = trans.position.z;
                }

            }
            return minPosition;
        }

        private Vector2 GetMaxPosition()
        {
            Vector2 maxPosition = Vector2.negativeInfinity;
            foreach (Transform trans in cameraWeights)
            {
                if (trans.position.x > maxPosition.x)
                {
                    maxPosition.x = trans.position.x;
                }
                if (trans.position.z > maxPosition.y)
                {
                    maxPosition.y = trans.position.z;
                }

            }
            return maxPosition;
        }
    }
}

