using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatingCharacters.Player;

namespace CreatingCharacters.Abilities
{
    public class TracerRecall : MonoBehaviour
    {
        [SerializeField] private int maxRecallData = 20;
        [SerializeField] private float secondsBetweenData = 0.5f;
        [SerializeField] private float recallDuration = 1.25f;

        private PlayerCameraController playerCameraController;
        private bool canCollectRecallData = true;
        private float currentDataTimer = 0f;
        public Camera StandartCam;
        public Camera PostProcessCam;
        [SerializeField] private ParticleSystem speedLinesParticleSystem;
            
        [System.Serializable]
        private class RecallData
        {
            public Vector3 characterPosition;
            public Quaternion characterRotation;
            public Quaternion cameraRotation;
        }

        [SerializeField] private List<RecallData> recallData = new List<RecallData>();

        private void Awake()
        {
            StandartCam.enabled = true;
            PostProcessCam.enabled = false;
        }

        private void Start()
        {
            playerCameraController = GetComponentInChildren<PlayerCameraController>();
        }

        private void Update()
        {
            StoreRecallData();
            for(int i = 0; i< recallData.Count - 1; i++)
            {   
                Debug.DrawLine(recallData[i].characterPosition,recallData[i+1].characterPosition);
            }
            
            RecallInput();
            
        }

        private void RecallInput()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {   
                StartCoroutine(Recall());
            }
            
        }

        private IEnumerator Recall()
        {   PostProcessCamChange();
            
            playerCameraController.Lock(true);
            canCollectRecallData = false;
            float secondsForEachData = recallDuration / recallData.Count;
            Vector3 currentDataPlayerStartPos = transform.position;
            Quaternion currentDataPlayerStartRotation = transform.rotation;
            Quaternion currentDataCameraStartRotation = playerCameraController.transform.rotation;

            while (recallData.Count > 0)
            {   
                float t = 0;
                while (t<secondsForEachData)

                {
                    transform.position = Vector3.Lerp(currentDataPlayerStartPos,
                        recallData[recallData.Count - 1].characterPosition,
                        t / secondsForEachData);
                    transform.rotation = Quaternion.Lerp(currentDataPlayerStartRotation,
                        recallData[recallData.Count-1].characterRotation,
                        t/secondsForEachData );
                    playerCameraController.transform.rotation = Quaternion.Lerp(currentDataCameraStartRotation,
                        recallData[recallData.Count-1].cameraRotation,
                        t/secondsForEachData );
                    t += Time.deltaTime;
                    yield return null;
                }

                currentDataPlayerStartPos = recallData[recallData.Count - 1].characterPosition;
                currentDataPlayerStartRotation = recallData[recallData.Count - 1].characterRotation;
                currentDataCameraStartRotation = recallData[recallData.Count - 1].cameraRotation;
                recallData.RemoveAt(recallData.Count - 1);
            }
            playerCameraController.Lock(false);
            canCollectRecallData = true;
            BackToNormal();
            
        }

        private void StoreRecallData()
        {
            currentDataTimer += Time.deltaTime;
            if (canCollectRecallData)
            {
                if (currentDataTimer >= secondsBetweenData)
                {
                    if (recallData.Count >= maxRecallData)
                    {
                        recallData.RemoveAt(0);
                    }

                    recallData.Add(GetRecallData());
                    currentDataTimer = 0f;
                }
            }
        }

        private RecallData GetRecallData()
        {
            return new RecallData()
            {
                characterPosition = transform.position,
                characterRotation = transform.rotation,
                cameraRotation = playerCameraController.transform.rotation
            };
        }

        
        public void PostProcessCamChange()
        {   speedLinesParticleSystem.Play();
            StandartCam.enabled = false;
            PostProcessCam.enabled = true;
        }
        public void BackToNormal()
        {   speedLinesParticleSystem.Stop();
            StandartCam.enabled = true;
            PostProcessCam.enabled = false;
        }
        
    } 
}

