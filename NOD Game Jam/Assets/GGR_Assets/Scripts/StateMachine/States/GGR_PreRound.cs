using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/RunGame/PreRound")]
    public class GGR_PreRound : GGR_State
    {
        public float showPictureLength;
        public float cameraDistanceFromPicture;
        public Vector3 cameraStartPos;
        public Vector3 cameraStartRotation;
        public float cameraZoomTime;

        private float currentTime;
        private GGR_Location currentLocation;
        private int slotIndex = 0;
        private bool zoomDone = false;

        public override void Enter()
        {
            zoomDone = false;
            Camera.main.transform.position = cameraStartPos;
            Camera.main.transform.rotation = Quaternion.Euler(cameraStartRotation);

            GGR_GameData.FreezeAllPlayers();
            for(int i = 0; i < GGR_GameData.GetAllPlayers().Count; i++)
            {
                GGR_GameData.SetPlayerPosition(GGR_GameData.GetAllPlayers()[i], GGR_GameData.GetSpawnPosition(i));
            }
            currentTime = 0;
            currentLocation = GGR_GameData.GetNextLocation();
            PictureSetup();
        }

        public override bool Run()
        {
            if (!zoomDone)
                return false;

            if(currentTime >= showPictureLength)
                Owner.TransitionTo<GGR_Round>();

            currentTime += Time.deltaTime;
            return false;
        }

        public override void Exit()
        {
            Camera.main.transform.rotation = Quaternion.Euler(GGR_CameraMovement.instance.defaultRotation);
        }

        private void PictureSetup()
        {
            GameObject picuture = ObjectPool.Instantiate(currentLocation.picture, GGR_GameData.GetPictureSlots()[slotIndex].position, GGR_GameData.GetPictureSlots()[slotIndex].rotation);
            Vector3 cameraDestination = GGR_GameData.GetPictureSlots()[slotIndex].position - GGR_GameData.GetPictureSlots()[slotIndex].right * cameraDistanceFromPicture;
            GGR_CameraMovement.instance.PanToPosition(GGR_GameData.GetPictureSlots()[slotIndex], cameraDestination, cameraZoomTime, () => zoomDone = true);
           // GGR_GameData.instance.StartCoroutine(ZoomToPicture());
            slotIndex++;

        }
    }
}