using System;
using System.Drawing;
using System.Diagnostics;

namespace dataStructures
{
    public enum parameterModeType
    {
        fixedParam,
        randomParam,
        animationParam,       
        animationBounceParam 
    }

    public struct randomParameter
    {
        public float value;
        public float leftLimit;
        public float rightLimit;
        public int parameterMode;

        public void initialize(float initValue)
        {
            value = initValue;
            leftLimit = initValue;
            rightLimit = initValue;
            parameterMode = (int) parameterModeType.fixedParam;
        }
    }

    public struct layer
    {
        public String name;

        public float animationPhase;

        public randomParameter diameter;
        public randomParameter scale;

        public randomParameter xOffset;
        public randomParameter yOffset;

        public randomParameter collisionDiameter1;
        public randomParameter collisionDiameter2;

        public randomParameter colorMode;
        public randomParameter centerRed;
        public randomParameter diameterRed;
        public randomParameter centerGreen;
        public randomParameter diameterGreen;
        public randomParameter centerBlue;
        public randomParameter diameterBlue;        

        public randomParameter radialPower;
        public randomParameter polarPower;
        public randomParameter radialAmp;
        public randomParameter polarAmp;
        public randomParameter radialFreq;
        public randomParameter polarFreq;
        public randomParameter radialPhase;
        public randomParameter polarPhase;

        public randomParameter centerBrightness;
        public randomParameter diameterBrightness;
        public randomParameter innerGlowPower;
        public randomParameter outerGlowPower;

        public randomParameter mirrorMode;
        public randomParameter rotateAngle;
        public randomParameter swirlMode;
        public randomParameter swirl;        
                       
        public randomParameter linearStretchX;
        public randomParameter linearStretchY;
        public randomParameter circularStretchX;
        public randomParameter circularStretchY;

        public randomParameter morphMode;
        public randomParameter morphAmp;
        public randomParameter morphFreq;
        public randomParameter morphPower;
        public randomParameter morphPhase;

        public randomParameter colliderXOffset;
        public randomParameter colliderYOffset;
        public randomParameter colliderRotateAngle;
                
        public void initializeLayerToDefault()
        {          
            // Assign layer parameters to default values  
            animationPhase = 0.0f;
             
            diameter.initialize(0.7f);
            scale.initialize(1.0f);

            xOffset.initialize(0.0f);
            yOffset.initialize(0.0f);

            collisionDiameter1.initialize(0.5f);
            collisionDiameter2.initialize(0.0f);

            colorMode.initialize(0.0f);
            centerRed.initialize(1.0f);
            diameterRed.initialize(1.0f);
            centerGreen.initialize(1.0f);
            diameterGreen.initialize(1.0f);
            centerBlue.initialize(1.0f);
            diameterBlue.initialize(1.0f);

            radialPower.initialize(0.0f);
            polarPower.initialize(0.0f);
            radialAmp.initialize(0.0f);
            polarAmp.initialize(0.0f);
            radialFreq.initialize(2.0f);
            polarFreq.initialize(2.0f);
            radialPhase.initialize(0.0f);
            polarPhase.initialize(0.0f);

            centerBrightness.initialize(0.4f);
            diameterBrightness.initialize(1.0f);
            innerGlowPower.initialize(5.0f);
            outerGlowPower.initialize(-1.5f);

            mirrorMode.initialize(0.0f);
            rotateAngle.initialize(0.0f);
            swirlMode.initialize(0.0f);
            swirl.initialize(0.2f);
                                    
            linearStretchX.initialize(1.0f);
            linearStretchY.initialize(1.0f);
            circularStretchX.initialize(0.0f);
            circularStretchY.initialize(0.0f);

            morphMode.initialize(0.0f);
            morphAmp.initialize(0.2f);
            morphFreq.initialize(1.0f);
            morphPower.initialize(0.0f); 
            morphPhase.initialize(0.0f);

            colliderXOffset.initialize(0.0f);
            colliderYOffset.initialize(0.0f);
            colliderRotateAngle.initialize(0.0f);
        }
    }

    public struct texture
    {
        public String name;
        public int size;
        public float clipDuration;
        public float framesPerSecond;
        public float textureScaler;
        public Bitmap bitmap;
        public int layerCount;
        public layer[] layerArray;

        public void assignTextureToDefault()
        {
            // Assign texture parameters to default values
            size = 128;
            clipDuration = 0.0f;
            framesPerSecond = 60.0f;
            textureScaler = 1.0f;
            bitmap = new Bitmap(128, 128);
            layerCount = 1;
            layerArray = new layer[1];
            layerArray[0].name = "layer1";
            layerArray[0].initializeLayerToDefault();
        }
    }

    public struct guiSettings
    {
        public int editCollectionIndex;
        public int editTextureIndex;
        public int editLayerIndex;
        public int selectedTabIndex;
        public int focusedUpDownControlIndex;
        public int xEditControlIndex;
        public int yEditControlIndex;
        public int randomParameterEditMode;
        public bool viewAllLayers; 
        public bool multiEdit;
        public bool editRelative;
        public bool applyToAll;
        public bool generateClip;
        public bool showCollider;
        public bool loop;
    }
}