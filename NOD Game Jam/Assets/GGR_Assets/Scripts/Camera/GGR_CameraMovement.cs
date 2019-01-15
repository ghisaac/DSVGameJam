using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace GGR
{
    public class GGR_CameraMovement : MonoBehaviour
    {
        //Attributes
        public float minYPosition, maxYPosition;
        public Vector2 minPlayerDistance, maxPlayerDistance;
        public float zoomTime;
        public float distanceFromZoomPoint;

        private bool zooming = false;
        private Vector3 zoomPosition;
        private Vector3 zoomStartPos;
        private float currentTime = 0;

        private Action zoomCallback;

        private void Update()
        {
            if (!zooming) {
                UpdateHorizontal();
                UpdateVertical();
                transform.position += Vector3.back * Mathf.Tan(Mathf.Deg2Rad * Vector3.Angle(Vector3.down, Camera.main.transform.forward)) * transform.position.y;
            }
            else
            {
                if (currentTime >= zoomTime)
                {
                    zooming = false;
                    zoomCallback();
                }
                   

                Vector3 cameraToZoomPos = zoomPosition - zoomStartPos;
                transform.position = Vector3.Lerp(zoomStartPos, zoomPosition, currentTime/zoomTime);
                currentTime += Time.deltaTime;
              
            }
        }

        public void ZoomInToPosition(Vector3 goalPosition, Action callback)
        {
            zoomCallback = callback;
            zooming = true;
            currentTime = 0;
            zoomStartPos = transform.position;
            zoomPosition = goalPosition - Camera.main.transform.forward * distanceFromZoomPoint;
        }

        private void UpdateVertical()
        {
            float xDifference =  -(GetMinPosition().x - GetMaxPosition().x);

            float xFactor = (xDifference - minPlayerDistance.x) / (maxPlayerDistance.x - minPlayerDistance.x);

            xFactor = xFactor * (maxPlayerDistance.x - minPlayerDistance.x) + minPlayerDistance.x;

            xFactor /= maxPlayerDistance.x;

            float zDifference = -(GetMinPosition().y - GetMaxPosition().y);

            float zFactor = (zDifference - minPlayerDistance.y) / (maxPlayerDistance.y - minPlayerDistance.y);

            zFactor = zFactor * (maxPlayerDistance.y - minPlayerDistance.y) + minPlayerDistance.y;

            zFactor /= maxPlayerDistance.y;

            float yPos = Mathf.Clamp(Mathf.Max(xFactor, zFactor) * maxYPosition, minYPosition, maxYPosition);
          //  float yPos = Mathf.Clamp(Mathf.Max(xFactor, zFactor), minYPosition, maxYPosition);

            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }

        private void UpdateHorizontal()
        {
            Vector2 middlePosition = (GetMinPosition() + GetMaxPosition()) / 2;
            if (float.IsNaN(middlePosition.x) || float.IsNaN(middlePosition.y))
                return;
            transform.position = new Vector3(middlePosition.x, minYPosition, middlePosition.y);
        }

        private Vector2 GetMinPosition()
        {
            Vector2 minPosition = Vector2.positiveInfinity;
            foreach(PlayerController pc in GGR_GameData.GetAllPlayerControllers())
            {
                if(pc.transform.position.x < minPosition.x)
                {
                    minPosition.x = pc.transform.position.x;
                }
                if(pc.transform.position.z < minPosition.y)
                {
                    minPosition.y = pc.transform.position.z;
                }

            }
            return minPosition;
        }

        private Vector2 GetMaxPosition()
        {
            Vector2 maxPosition = Vector2.negativeInfinity;
            foreach (PlayerController pc in GGR_GameData.GetAllPlayerControllers())
            {
                if (pc.transform.position.x > maxPosition.x)
                {
                    maxPosition.x = pc.transform.position.x;
                }
                if (pc.transform.position.z > maxPosition.y)
                {
                    maxPosition.y = pc.transform.position.z;
                }

            }
            return maxPosition;
        }

    }
}

