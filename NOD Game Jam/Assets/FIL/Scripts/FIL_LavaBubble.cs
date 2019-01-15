using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FIL
{
    public class FIL_LavaBubble : MonoBehaviour
    {

        public ParticleSystem _lavaSplash;
        public bool _startedParticle = false;
        public Renderer _rend;

        void Update()
        {
            if (transform.localScale.x < 0.4f)
            {
                transform.localScale += transform.localScale * 0.02f;
            } else if (!_startedParticle)
            {
                _startedParticle = true;
                _lavaSplash.Play();
                _rend.enabled = false;
            }

            if(_startedParticle && !_lavaSplash.isPlaying)
            {
                Destroy(this);
            }
        }
    }
}
