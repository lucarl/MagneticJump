using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileTrigger : MonoBehaviour
{
    /// <summary>
        /// Public fields
        /// </summary>
        public GameObject m_Projectile;    // this is a reference to your projectile prefab
        public Transform m_SpawnTransform; // this is a reference to the transform where the prefab will spawn
        public Magnet magnet;
        private float magnetSpeed = 200f;
        private bool increase;
        
        public GameObject player;
        
        public List<GameObject> createdBalls;
        public AudioSource shootAudio;
        public AudioSource reLoadAudio;
        
        private bool changeValue;
        private Slider powerBar;
        private float powerBarTreshold = 10f;
        private float powerBarValue = 0f;
        private GameObject currentBall;

        /// <summary>
        /// Message that is called once per frame
        /// </summary>
        private void Start()
        {
            createdBalls = new List<GameObject>();
            powerBar = GameObject.Find("Power Bar").GetComponent<Slider>();

            powerBar.minValue = 0f;
            powerBar.maxValue = 10f;
            powerBar.value = powerBarValue;
        }

        private void Update()
        {
            //Increase or decrease value of projectile and change power bar
            if (Input.GetMouseButton(0) && player.GetComponent<Player>().allowBallCreation)
            {
                if (magnetSpeed >= 400)
                {
                    changeValue = true;
                }
                if (magnetSpeed <= 200)
                {
                    changeValue = false;
                }
                if (changeValue)
                {
                    magnetSpeed -= 200 * Time.deltaTime;  
                    powerBarValue -= powerBarTreshold * Time.deltaTime;
                    powerBar.value = powerBarValue;
                }
                else
                {
                    magnetSpeed += 200 * Time.deltaTime;
                    powerBarValue += powerBarTreshold * Time.deltaTime;
                    powerBar.value = powerBarValue;
                }
                increase = true;
            } else if (increase && !Input.GetMouseButton(0) && player.GetComponent<Player>().allowBallCreation)
            {
                powerBarValue = 0f;
                powerBar.value = powerBarValue;
                magnet.speed = magnetSpeed;
                GameObject ball = Instantiate(m_Projectile, m_SpawnTransform.position, m_SpawnTransform.rotation);
                createdBalls.Add(ball);
                magnetSpeed = 200f;
                increase = false;
                shootAudio.mute = false;
                shootAudio.Play(0);
            }

            if( Input.GetMouseButtonDown(1))
            {
                if (createdBalls.Count != 0)
                {
                    /*currentBall.transform.position = transform.position + new Vector3(1f, 3f, 0);
                    currentBall = createdBalls[createdBalls.Count - 1];
                    currentBall.GetComponent<Rigidbody>().isKinematic = false;*/
                    Destroy(createdBalls[createdBalls.Count-1]);
                    createdBalls.RemoveAt(createdBalls.Count-1);
                    reLoadAudio.mute = false;
                    reLoadAudio.Play(0);
                }
            }
        }
}
