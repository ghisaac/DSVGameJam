using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/States/StartGame")]
    public class GGR_StartGame : GGR_State
    {
        public float initialInstructionTime;
        public float distanceFromNote;
        public Vector3 cameraStartPos;
        public Vector3 cameraStartRotation;
        private bool zoomDone = false;

        public override void Enter()
        {
            SoundManager.Instance.PlayBGM();

            Camera.main.transform.position = cameraStartPos;
            Camera.main.transform.rotation = Quaternion.Euler(cameraStartRotation);
            zoomDone = false;
            GGR_GameData.FindPlayers();
            GGR_GameData.instance.StartCoroutine(SplashScreen());
            
            /*
            Player johannaPlayer = new Player(0, "Johanna");
            Player danielPlayer = new Player(1, "Daniel");
            GGR_GameData.SpawnPlayer(johannaPlayer);
            GGR_GameData.SpawnPlayer(danielPlayer);
            */
        }

        private void SplashDone()
        {
            Vector3 cameraDestination = GGR_GameData.instance.instructionNoteTransform.position + GGR_GameData.instance.instructionNoteTransform.up * distanceFromNote;
            GGR_CameraMovement.instance.PanToPosition(GGR_GameData.instance.instructionNoteTransform, cameraDestination, initialInstructionTime, () => GGR_GameData.instance.StartCoroutine(WaitABit()));
        }

        private IEnumerator SplashScreen()
        {
            GGR_GameData.instance.splashScreen.SetActive(true);
            yield return new WaitForSeconds(3.5f);
            GGR_GameData.instance.splashScreen.SetActive(false);
            SplashDone();
        }

        private IEnumerator WaitABit()
        {
            yield return new WaitForSeconds(2f);
            zoomDone = true;
        }

        public override bool Run()
        {
            if(zoomDone)
                Owner.TransitionTo<GGR_RunGame>();
                
            return false;
        }
    }
}