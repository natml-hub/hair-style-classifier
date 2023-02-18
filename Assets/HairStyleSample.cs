/* 
*   Hair Style Predictor
*   Copyright © 2023 NatML Inc. All Rights Reserved.
*/

namespace NatML.Examples {

    using UnityEngine;
    using UnityEngine.UI;
    using NatML.VideoKit;

    public sealed class HairStyleSample : MonoBehaviour {

        #region --Inspector--
        [Header(@"Camera Manager")]
        public VideoKitCameraManager cameraManager;

        [Header(@"UI")]
        public Text styleText;
        #endregion


        #region --Operations--
        private HairStylePredictor predictor;

        private async void Start () => predictor = await HairStylePredictor.Create();

        public async void Capture () {
            // Check that camera manager is present
            if (!cameraManager) {
                Debug.LogError(@"Cannot capture hair style because camera manager is missing");
                return;
            }
            // Check that camera is running
            if (!cameraManager.running) {
                Debug.LogError(@"Cannot capture hair style because camera manager is not running");
                return;
            }
            // Take a picture
            //var photo = cameraManager.cameraDevice.CapturePhoto()
        }
        #endregion
    }
}