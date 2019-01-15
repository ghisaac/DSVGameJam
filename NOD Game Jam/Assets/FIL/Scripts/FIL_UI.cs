using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FIL
{
    public class FIL_UI : MonoBehaviour
    {
        [SerializeField] private float TotalTime = 3.3f;
        public Text CountDownText;
        private bool StartCountDown = false;
        private float time = 0;
        

        public void StartTimer()
        {
            StartCountDown = true;
            CountDownText.gameObject.SetActive(true);
            time = TotalTime;
        }


        private void Update()
        {
            if (StartCountDown)
            {
                time -= Time.deltaTime;

                if (time < 3)
                    CountDownText.text = "3";

                if (time < 2)
                    CountDownText.text = "2";

                if (time < 1)
                    CountDownText.text = "1";

                if (time < 0)
                {
                    CountDownText.gameObject.SetActive(false);
                    StartCountDown = false;
                    time = 0;

                    FIL_GameManager.instance.StartGameLoop();
                }
            }
        }


    }
}

