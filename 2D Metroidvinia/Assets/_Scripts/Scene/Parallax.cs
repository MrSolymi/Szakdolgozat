using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace Solymi._Scripts.Scene
{
    public class Parallax : MonoBehaviour
    {
        private float startPos, length;
        private GameObject cam;
        public float parallaxEffect;

        private void Start()
        {
            cam = FindObjectOfType<CinemachineBrain>().transform.GetComponent<Camera>().gameObject;
            var parentParallaxObject = GameObject.FindGameObjectWithTag("ParallaxBackground");
            parentParallaxObject.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0);
            startPos = transform.position.x;
            length   = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void FixedUpdate()
        {
            if (cam == null)
                cam = GameObject.FindWithTag("MainCamera");
            
            float distance = cam.transform.position.x * parallaxEffect;
            float movement = cam.transform.position.x * (1 - parallaxEffect);
            
            transform.position = new Vector3(
                startPos + distance,
                cam.transform.position.y,
                transform.position.z
            );
            
            
            if (movement > startPos + length)
            {
                startPos += length;
            }
            else if (movement < startPos - length)
            {
                startPos -= length;
            }
        }
    }
}