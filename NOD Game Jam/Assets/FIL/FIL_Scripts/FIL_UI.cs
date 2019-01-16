using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FIL
{
    public class FIL_UI : MonoBehaviour
    {
        [SerializeField] private float TotalTime = 3.3f;
        public Image CountDownImage;
        private bool StartCountDown = false;
        private float time = 0;
        public Sprite[] countdownSprites;
        public int countdownsShown = 3;
        

        public void StartTimer()
        {
            StartCountDown = true;
            time = TotalTime;
        }


        private void Update()
        {
            if (StartCountDown)
            {
                time -= Time.deltaTime;
               
                if(time < -1f && countdownsShown == -1)
                {
                    CountDownImage.gameObject.SetActive(false);
                    StartCountDown = false;
                    time = 0;
                }
                else if (time < 0 && countdownsShown == 0)
                {
                    countdownsShown--;
                    CountDownImage.sprite = countdownSprites[0];
                    FIL_GameManager.instance.StartGameLoop();
                }
                else if (time < 1 && countdownsShown == 1)
                {
                    countdownsShown--;
                    CountDownImage.sprite = countdownSprites[1];
                }
                else if (time < 2 && countdownsShown == 2)
                {
                    countdownsShown--;
                    CountDownImage.sprite = countdownSprites[2];
                }
                else if (time < 3 && countdownsShown == 3)
                {
                    CountDownImage.gameObject.SetActive(true);
                    countdownsShown--;
                    CountDownImage.sprite = countdownSprites[3];
                    SoundManager.Instance.PlayCountdown();
                }
            }
        }


    }
}

