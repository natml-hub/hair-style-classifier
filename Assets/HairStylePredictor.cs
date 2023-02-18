/* 
*   Hair Style Predictor
*   Copyright © 2023 NatML Inc. All Rights Reserved.
*/

namespace NatML.Examples {

    using System;
    using System.Threading.Tasks;
    using NatML;
    using NatML.Internal;
    using NatML.Features;

    /// <summary>
    /// </summary>
    public sealed class HairStylePredictor : IMLPredictor<Task<HairStylePredictor.Style>> {

        #region --Enumerations--
        /// <summary>
        /// </summary>
        public enum Style : int {
            /// <summary>
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// </summary>
            Curly = 1,
            /// <summary>
            /// </summary>
            Straight = 2
        }
        #endregion


        #region --Client API--
        /// <summary>
        /// Predict the hair style present in a given image.
        /// </summary>
        /// <param name="inputs">Input image.</param>
        /// <returns>Hair style.</returns>
        public async Task<Style> Predict (params MLFeature[] inputs) {
            // Check input type
            if (!(inputs[0] is MLImageFeature imageFeature))
                throw new InvalidOperationException(@"Hair style predictor requires an input image");
            // Predict
            using var inputFeature = (imageFeature as IMLCloudFeature).Create(null);
            using var outputFeatures = await model.Predict(inputFeature);
            // Parse output
            var styleString = new MLTextFeature(outputFeatures[0]);
            var style = ToStyle(styleString);
            return style;
        }

        /// <summary>
        /// Dispose the predictor and release resources.
        /// </summary>
        public void Dispose () => model.Dispose();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static async Task<HairStylePredictor> Create (string accessKey = null) {
            var model = await MLCloudModel.Create("@natml/curly-or-straight-hair", accessKey);
            var predictor = new HairStylePredictor(model);
            return predictor;
        }
        #endregion


        #region --Operations--
        private readonly MLCloudModel model;

        private HairStylePredictor (MLCloudModel model) => this.model = model;

        private static Style ToStyle (string input) => input switch {
            @"CURLY"    => Style.Curly,
            @"STRAIGHT" => Style.Straight,
            _           => Style.Unknown,
        };
        #endregion
    }
}