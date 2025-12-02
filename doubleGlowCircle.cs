using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using dataStructures;

namespace TextureCreate
{   
    class doubleGlowCircle
    {   
        //// Constructor
        //public doubleGlowCircle(int size, ref layer layerParameters)
        //{
        //    // Create the texture image
        //    this.drawDoubleGlowCircle(size, ref layerParameters);
        //}

        // ********************************************************************************************
        // Create an antialiased circle texture with both inner glow and outer glow
        // NOTE: Negative glow powers reverse the polarity of the decay or amplification of brightness
        // and diameter represents a fraction of the texture size
        // ********************************************************************************************
        unsafe public int drawDoubleGlowCircle(ref texture t, ref layer l, bool addLayer)
        {
            // Set return value of over flow to false
            // NOTE: Trigger when any color channel gets a value greater than 255
            bool overflow = false;

            // Allocate texture
            if (t.bitmap == null || t.bitmap.Width != t.size)
            {
                t.bitmap = new Bitmap(t.size, t.size);
            }

            // Assign texture radius as the radius of a circle inscribed within the texture image
            float textureRadius = ((float) t.size - 1.0f) / 2.0f;

            // Define the draw region for the radial brightness function
            int rFilterPassCount = 23;// (int)l.collisionDiameter1.value;
            int filterPassCount = 0;// (int)l.collisionDiameter2.value;
            float drawRegionRadius = textureRadius - (float)filterPassCount;

            // Allocate memory for a texture image
            //Bitmap texture = new Bitmap(size, size);
            //Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
            //GLubyte *textureImage = (GLubyte *) malloc(4*size*size*sizeof(GLubyte));

             // Assign glow circle's radius in pixels
            // and determine whether to draw a circle or square
            // NOTE: Draw a circle if the diameter is positive and a square
            // if it is negative.
            bool drawSquare = false;
            float glowCircleRadius = l.diameter.value * drawRegionRadius;
            if (glowCircleRadius < 0.0f)
            {
                drawSquare = true;
                glowCircleRadius = Math.Abs(glowCircleRadius);
            }

            // Transform radial and polar brightness amplitudes, frequencies, and phases
            // and assign brightness modes
            float radialAmp = (4.0f * l.radialAmp.value + 2.0f) % 4.0f;
            if (radialAmp < 0.0f) radialAmp += 4.0f;
            radialAmp -= 2.0f;
            int radialBrightnessMode = 0;
            if (radialAmp > 0.0f) radialBrightnessMode = 1;
            if (radialAmp < 0.0f) radialBrightnessMode = 2;
            float radialFreq = l.radialFreq.value;
            if (radialFreq < 0.0) radialFreq = (int)(-radialFreq) + 1;
            float radialPhase = l.radialPhase.value;
            
            float polarAmp = (4.0f * l.polarAmp.value + 2.0f) % 4.0f;
            if (polarAmp < 0.0f) polarAmp += 4.0f;
            polarAmp -= 2.0f;
            int polarBrightnessMode = 0;
            if (polarAmp > 0.0f) polarBrightnessMode = 1;
            if (polarAmp < 0.0f) polarBrightnessMode = 2;
            float polarFreq = l.polarFreq.value;
            if (polarFreq < 0.0) polarFreq = (int)(-polarFreq) + 1;
            float polarPhase = 2.0f * (float)Math.PI * l.polarPhase.value;

              // Transform radial and polar brightness powers
            float radialPower = (float)Math.Exp(l.radialPower.value);
            float polarPower = (float)Math.Exp(l.polarPower.value);

            // Initialize radial and polar brightness factors to 1
            float radialBrightnessFactor = 1.0f;
            float polarBrightnessFactor = 1.0f;

            // Copy/transform morph wave amplitude
            float morphAmp = (4.0f * l.morphAmp.value + 2.0f) % 4.0f;
            if (morphAmp < 0.0f) morphAmp += 4.0f;
            morphAmp -= 2.0f;
            float morphFreq = l.morphFreq.value;
            if (morphFreq < 0.0) morphFreq = (int)(-morphFreq) + 1;
            morphFreq *= 2.0f * (float)Math.PI;
            float morphPhase = l.morphPhase.value;
            //morphPhase = morphPhase % 1.0f;
            //if (morphPhase > 0.5f) morphPhase -= 1.0f;
            //if (morphPhase < -0.5f) morphPhase += 1.0f;   
            float morphPower = (float)Math.Exp(l.morphPower.value);       
           
            // Transform inner glow power
            float innerGlowPower = l.innerGlowPower.value;
            if (innerGlowPower < 0.0f)
            {
                innerGlowPower = 1.0f / (1.0f - innerGlowPower);
            }
            else
            {
                innerGlowPower += 1.0f;
            }

            // Transform outer glow power
            float outerGlowPower = l.outerGlowPower.value;
            if (outerGlowPower < 0.0f)
            {
                outerGlowPower = 1.0f / (1.0f - outerGlowPower);
            }
            else
            {
                outerGlowPower += 1.0f;
            }

            // Copy stretch variables, transform circular stretch variables,
            // and assign circular stretch modes
            float linearStretchX = l.linearStretchX.value;
            float linearStretchY = l.linearStretchY.value;
            float circularStretchX = (float)Math.Exp(Math.Abs(l.circularStretchX.value)) - 1.0f;
            int circularStretchXMode = 0;
            if (l.circularStretchX.value > 0.0f) circularStretchXMode = 1;
            if (l.circularStretchX.value < 0.0f) circularStretchXMode = 2;
            float circularStretchY = (float)Math.Exp(Math.Abs(l.circularStretchY.value)) - 1.0f;
            int circularStretchYMode = 0;
            if (l.circularStretchY.value > 0.0f) circularStretchYMode = 1;
            if (l.circularStretchY.value < 0.0f) circularStretchYMode = 2;

            // Double glow brightness has 3 regions of brightness governed by 2 functions
            // REGION 1: inside circle diameter
            //      A1*pow(m1*r+b1, innerGlowPower)+d1
            // REGION 2: outside circle diameter & inside texture diameter
            //      A2*pow(m2*r+b2, outerGlowPower)+d2
            // REGION 3: outside texture diameter
            //      Zero

            // Assign brightness function parameters
            // NOTE: Make glow powers positive definite
            // but use negative to indicate reverse slope
            float A1, m1, d1; // b1
            float m2, b2, d2; // A2

            // REGION 1
            // 0 <= r <= glow circle radius
            // A1 * (float)Math.Pow(Math.Abs(m1 * r), innerGlowPower) + d1;
            // Assign so that quantity m1*r+b1 varies from center brightness to 1
            float diameterBrightness = l.diameterBrightness.value;
            float colorScaler = 255.0f;
            if (diameterBrightness > 1.0f)
            {         
                colorScaler *= diameterBrightness;
                diameterBrightness = 1.0f;
            }
            else if (diameterBrightness < -1.0f)
            {
                colorScaler *= -diameterBrightness;
                diameterBrightness = -1.0f;
            }
            if (glowCircleRadius != 0.0)
            {
                d1 = l.centerBrightness.value;
                A1 = diameterBrightness - d1;
                m1 = 1.0f / glowCircleRadius;
            }
            else
            {
                d1 = diameterBrightness;
                A1 = 0.0f;
                m1 = 0.0f;
            }
            //b1 = 0.0f;
           
            // REGION 2
            // glow circle radius < r <= texture radius  
            // d2 * (1.0f - (float)Math.Pow(Math.Abs(m2 * r + b2), outerGlowPower));
            // Assign so that quantity m2*r+b2 varies from diameter brightness to zero
            d2 = diameterBrightness;
            if (glowCircleRadius != drawRegionRadius)
            {
                //A2 = -d2;
                m2 = 1.0f / (drawRegionRadius - glowCircleRadius);
            }
            else
            {
                //A2 = 0.0f;
                m2 = 0.0f;
                d2 = 0.0f;
            }
            b2 = -m2 * glowCircleRadius;

            // Create a double glow brightness function lookup table for faster computation.
            // NOTE: Double glow circle brightness is only a function of the radial distance
            // from the texture's center and the brightness map indices go from 0 to 2*size-1
            // as the radius varies from 0 to texture radius.
            int bMapSize = 3*t.size;
            float deltaRadius = 1.5f*drawRegionRadius / (float)bMapSize;
            float[] brightnessMap = new float[bMapSize];
            float[] dBrightness_dR = new float[bMapSize];
            brightnessMap[0] = l.centerBrightness.value;
            float r;
            for(int i=1; i<bMapSize; i++) {
                
                r = (float) i*deltaRadius;
                //r = drawRegionRadius * (float) Math.Abs((Math.Cos(0.5*r/drawRegionRadius*2.0*Math.PI)));

                switch (radialBrightnessMode)
                {
                    default:
                    case 0:
                        break;
                    case 1:
                        radialBrightnessFactor = (1.0f - radialAmp) + radialAmp * (float)Math.Pow(0.5 + 0.5 *
                            Math.Cos(2.0 * Math.PI * (radialFreq * r / drawRegionRadius - radialPhase)), radialPower);
                        break;
                    case 2:
                        radialBrightnessFactor = 1.0f + radialAmp * (float)Math.Pow(0.5 + 0.5 *
                            Math.Cos(2.0 * Math.PI * (radialFreq * r / drawRegionRadius - radialPhase)), radialPower);
                        break;
                }
        
                if (r < glowCircleRadius)
                {
                    brightnessMap[i] = A1 * (float)Math.Pow(Math.Abs(m1 * r), innerGlowPower) + d1;
                }
                else if (r < drawRegionRadius)
                {
                    brightnessMap[i] = d2 * (1.0f - (float)Math.Pow(Math.Abs(m2 * r + b2), outerGlowPower));
                }
                else
                {
                    brightnessMap[i] = 0.0f;
                }
                brightnessMap[i] *= (float)Math.Abs(radialBrightnessFactor);

                if (float.IsNaN(brightnessMap[i]))
                {
                    Debug.Print("hey hey");
                }
            }

            // Pass the brightness array thru a 1 by 2 box filter n times
            for (int j = 0; j < rFilterPassCount; j++)
            {
                for (int i = 1; i < bMapSize - 1; i++)
                {
                    brightnessMap[i] = 0.5f * (brightnessMap[i] + brightnessMap[i + 1]);
                }
            }

            // Calculate delta brightness
            for (int i = 1; i < bMapSize; i++)
            {
                dBrightness_dR[i - 1] = (brightnessMap[i] - brightnessMap[i - 1]) / deltaRadius;
            }
            dBrightness_dR[bMapSize - 1] = (0.0f - brightnessMap[bMapSize - 1]) / deltaRadius;


            // Assign color interpolation constants
            float cRed = l.centerRed.value;
            float cGreen = l.centerGreen.value;
            float cBlue = l.centerBlue.value;
            float dRed = l.diameterRed.value;
            float dGreen = l.diameterGreen.value;
            float dBlue = l.diameterBlue.value;
            float red_m, red_b;
            float green_m, green_b;
            float blue_m, blue_b;
            if (glowCircleRadius == 0.0f)
            {
                red_b = dRed;
                red_m = 0.0f;
                green_b = dGreen;
                green_m = 0.0f;
                blue_b = dBlue;
                blue_m = 0.0f;
            }
            else
            {
                red_b = cRed;
                red_m = (dRed - cRed) / glowCircleRadius;
                green_b = cGreen;
                green_m = (dGreen - cGreen) / glowCircleRadius;
                blue_b = cBlue;
                blue_m = (dBlue - cBlue) / glowCircleRadius;
            }

            // Lock the texture bitmap's data to work directly with it
            System.Drawing.Imaging.BitmapData bmd = t.bitmap.LockBits(new Rectangle(0, 0, t.size, t.size),
             System.Drawing.Imaging.ImageLockMode.ReadOnly, t.bitmap.PixelFormat);

            // Assign color, mirror, swirl, and morph states
            int colorMode = (int)l.colorMode.value;
            int mirrorMode = (int)l.mirrorMode.value;
            float swirl = l.swirl.value;
            int swirlMode = (int)l.swirlMode.value;
            if (swirl == 0.0f) swirlMode = 0;           
            int morphMode = (int)l.morphMode.value;
            if (morphAmp == 0.0f) morphMode = 0;

            // Assign scale factor
            float scaleFactor = 1.0f / l.scale.value;          

            // Assign limits for x and y to compute based on mirroring mode
            // in order to minimize fully computed pixels
            // jea tbd maybe mirror diagonals
            int yLeftLimit = 0;
            int yRightLimit = t.size;
            int xLeftLimit = 0;
            int xRightLimit = t.size;
            switch (mirrorMode)
            {
                default:
                case 0: // No mirroring                       
                    break;
                case 1: // Mirror right half
                    xLeftLimit = t.size / 2;
                    break;
                case 2: // Mirror left half
                    xRightLimit = t.size / 2;
                    break;
                case 3: // Mirror upper half
                    yLeftLimit = t.size / 2;
                    break;
                case 4: // Mirror lower half
                    yRightLimit = t.size / 2;
                    break;
                case 5: // Mirror upper right quadrant
                    xLeftLimit = t.size / 2;
                    yLeftLimit = t.size / 2;
                    break;
                case 6: // Mirror upper left quadrant
                    xRightLimit = t.size / 2;
                    yLeftLimit = t.size / 2;
                    break;
                case 7: // Mirror lower left quadrant
                    xRightLimit = t.size / 2;
                    yRightLimit = t.size / 2;
                    break;
                case 8: // Mirror lower right quadrant
                    xLeftLimit = t.size / 2;
                    yRightLimit = t.size / 2;
                    break;
                case 9: // Mirror upper right quadrant's lower half octact
                    xLeftLimit = t.size / 2;
                    yLeftLimit = t.size / 2;
                    break;
            }
           
            // Loop thru each pixel and calculate a color
            float xOffset = l.xOffset.value;
            float yOffset = l.yOffset.value;
            float xFromCenter2, yFromCenter2;
            float r2;
            float thetaTMS;
            float rotateAngle = l.rotateAngle.value;
            float swirlAngle;
            float xFromCenter1, yFromCenter1;
            float alphaX, alphaY;
            float morphAngle = 0.0f;
            float morphFactor;
            float xFromCenter, yFromCenter;
            float phi, yDistance;
            int rIndex;
            float brightness;
            int colorRed, colorBlue, colorGreen, colorGrey;
            float[] rgbColor = new float[3];
            float[] hsiColor = new float[3];
            float[] hslColor = new float[3];
            int rowOffset;
            bool simpleRadialPolar = true;
            if (scaleFactor != 1.0f || xOffset != 0.0f || yOffset != 0.0f || rotateAngle != 0.0 ||
                swirlMode != 0 || morphMode != 0 || circularStretchXMode != 0 || circularStretchYMode != 0)
            {
                simpleRadialPolar = false;
            }
            for (int y = yLeftLimit; y < yRightLimit; y++)
            {
                // Get a pointer to the bitmap's y row
                byte* bitmapRow = (byte*)bmd.Scan0 + ((t.size - 1 - y) * bmd.Stride);

                // Get a pointer to the bitmap's mirror y row
                byte* bitmapMirrorRow = (byte*)bmd.Scan0 + (y * bmd.Stride);

                // Set limit for octant mirroring
                if (mirrorMode == 9) xLeftLimit = y;
                
                for (int x = xLeftLimit; x < xRightLimit; x++)
                {
                    if (simpleRadialPolar)
                    {
                        // No offset, rotate, swirl, morph, linear stretch, and/or circular stretch
                        xFromCenter = ((float)x - textureRadius) / linearStretchX;
                        yFromCenter = ((float)y - textureRadius) / linearStretchY;
                    }
                    else
                    {
                        // Offset, rotate, swirl, morph, linear stretch, and/or circular stretch

                        xFromCenter2 = (float)x - (1.0f + 2.0f * xOffset) * textureRadius;
                        yFromCenter2 = (float)y - (1.0f + 2.0f * yOffset) * textureRadius;

                        r2 = scaleFactor * (float)Math.Sqrt(xFromCenter2 * xFromCenter2 + yFromCenter2 * yFromCenter2);
                        thetaTMS = (float)Math.Atan2(yFromCenter2, xFromCenter2);

                        // Calculate swirl angle based on swirl style
                        switch (swirlMode)
                        {
                            default:
                            case 0: // No swirl
                                swirlAngle = 0.0f;
                                break;

                            case 1: // Swirl based on radial position squared
                                swirlAngle = swirl * r2 * r2 / textureRadius / textureRadius;
                                break;

                            case 2: // Swirl based on radial position
                                swirlAngle = swirl * r2 / textureRadius;
                                break;

                            case 3: // Swirl based on reversed radial position squared
                                swirlAngle = swirl * (textureRadius - r2) / textureRadius * (textureRadius - r2) / textureRadius;
                                break;

                            case 4: // Swirl based on x times y position
                                swirlAngle = swirl * xFromCenter2 * yFromCenter2 / textureRadius / textureRadius;
                                break;

                            case 5: // Swirl based on x position
                                swirlAngle = swirl * xFromCenter2 / textureRadius;
                                break;

                            case 6: // Swirl based on y position
                                swirlAngle = -swirl * yFromCenter2 / textureRadius;
                                break;

                            case 7: // Swirl based on x position squared
                                swirlAngle = swirl * xFromCenter2 * xFromCenter2 / textureRadius / textureRadius;
                                break;

                            case 8: // Swirl based on y position squared
                                swirlAngle = swirl * yFromCenter2 * yFromCenter2 / textureRadius / textureRadius;
                                break;

                            case 9: // Swirl toward vertical based on angular position
                                swirlAngle = -swirl * (float)Math.Sin(thetaTMS - Math.PI / 2.0);
                                break;

                            case 10: // Swirl toward horizon based on angular position
                                swirlAngle = -swirl * (float)Math.Cos(thetaTMS - Math.PI / 2.0);
                                break;
                        }

                        // Rotate and swirl the translated and mirrored coordinates
                        thetaTMS -= 2.0f * (float)Math.PI * (l.rotateAngle.value + swirlAngle);

                        // Force the TMS angle to be between -pi and pi
                        thetaTMS = thetaTMS % (float)(2.0 * Math.PI);
                        if (thetaTMS > (float)Math.PI) thetaTMS -= (float)(2.0 * Math.PI);
                        if (thetaTMS < -(float)Math.PI) thetaTMS += (float)(2.0 * Math.PI);

                        //thetaTMS -= swirlAngle*sin(2.0*M_PI*rTMS/(diameter*textureRadius));

                        xFromCenter1 = r2 * (float)Math.Cos(thetaTMS);
                        yFromCenter1 = r2 * (float)Math.Sin(thetaTMS);

                        // Initialize alphas to linear stretch values
                        // NOTE: Alphas are morphing parameters that scale x and y positions
                        // of the glow circle or square
                        alphaX = linearStretchX;
                        alphaY = linearStretchY;

                        // Force the morph angle to be between -pi and pi
                        // NOTE: In normalized units this is between -1/2 and 1/2
                        if (morphMode > 13)
                        {
                            morphAngle = -0.25f + thetaTMS / (2.0f * (float)Math.PI) - morphPhase;
                            morphAngle = morphAngle % 1.0f;
                            if (morphAngle > 0.5f) morphAngle -= 1.0f;
                            if (morphAngle < -0.5f) morphAngle += 1.0f;
                        }

                        // Apply a morph wave
                        switch (morphMode)
                        {
                            default:
                            case 0:  // No morphing
                                break;

                            case 1: // Morph y based on x position from center
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        alphaY *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                    else
                                    {
                                        alphaY *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                }
                                break;

                            case 2:  // Morph x based on y position from center
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        alphaX *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                    else
                                    {
                                        alphaX *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                }
                                break;

                            case 3: // Morph x and y based on x position from center
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        morphFactor = (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                    else
                                    {
                                        morphFactor = 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                }
                                break;

                            case 4:  // Morph x and y based on y position from center
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        morphFactor = (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                    else
                                    {
                                        morphFactor = 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                }
                                break;

                            case 5: // Morph x and y based on y and x position from center
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        alphaX *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaY *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                    else
                                    {
                                        alphaX *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaY *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                }
                                break;

                            case 6: // Morph y based on y position from center
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        alphaY *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                    else
                                    {
                                        alphaY *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                }
                                break;

                            case 7: // Morph x based on x position from center
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        alphaX *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                    else
                                    {
                                        alphaX *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                }
                                break;

                            case 8: // Morph x and y based on y position from center
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        morphFactor = (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                    else
                                    {
                                        morphFactor = 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                }
                                break;

                            case 9: // Morph x and y based on x position from center
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        morphFactor = (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                    else
                                    {
                                        morphFactor = 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                }
                                break;

                            case 10: // Morph x and y based on x and y position from center
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        alphaX *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaY *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                    else
                                    {
                                        alphaX *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (xFromCenter1 / textureRadius - morphPhase)), morphPower);
                                        alphaY *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (yFromCenter1 / textureRadius - morphPhase)), morphPower);
                                    }
                                }
                                break;

                            case 11: // Morph x based on radial position
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        alphaX *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (r2 / textureRadius - morphPhase)), morphPower);
                                    }
                                    else
                                    {
                                        alphaX *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (r2 / textureRadius - morphPhase)), morphPower);
                                    }
                                }
                                break;

                            case 12: // Morph y based on radial position
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        alphaY *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (r2 / textureRadius - morphPhase)), morphPower);
                                    }
                                    else
                                    {
                                        alphaY *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (r2 / textureRadius - morphPhase)), morphPower);
                                    }
                                }
                                break;

                            case 13: // Morph x and y morph based on radial position
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        morphFactor = (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (r2 / textureRadius - morphPhase)), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                    else
                                    {
                                        morphFactor = 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * (r2 / textureRadius - morphPhase)), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                }
                                break;

                            case 14: // Morph x based on angular position
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        alphaX *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * morphAngle), morphPower);

                                    }
                                    else
                                    {
                                        alphaX *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * morphAngle), morphPower);
                                    }
                                }
                                break;

                            case 15: // Morph y based on angular position 
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        alphaY *= (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * morphAngle), morphPower);
                                    }
                                    else
                                    {
                                        alphaY *= 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * morphAngle), morphPower);
                                    }
                                }
                                break;

                            case 16: // Morph x and y based on angular position
                                {
                                    if (morphAmp >= 0.0f)
                                    {
                                        morphFactor = (1.0f - morphAmp) + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * morphAngle), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                    else
                                    {
                                        morphFactor = 1.0f + morphAmp * (float)Math.Pow(0.5 + 0.5 *
                                            Math.Cos(morphFreq * morphAngle), morphPower);
                                        alphaX *= morphFactor;
                                        alphaY *= morphFactor;
                                    }
                                }
                                break;
                        }

                        // Apply linear and circular stretches
                        switch (circularStretchXMode)
                        {
                            default:
                            case 0:
                                break;
                            case 1:
                                alphaX *= (float)Math.Pow(1.0 - (yFromCenter1 / textureRadius * yFromCenter1 / textureRadius), circularStretchX);
                                break;
                            case 2:
                                alphaX *= (float)Math.Pow(0.5 + 0.5 * (yFromCenter1 / textureRadius * yFromCenter1 / textureRadius), circularStretchX);
                                break;
                        }
                        switch (circularStretchYMode)
                        {
                            default:
                            case 0:
                                break;
                            case 1:
                                alphaY *= (float)Math.Pow(1.0 - (xFromCenter1 / textureRadius * xFromCenter1 / textureRadius), circularStretchY);
                                break;
                            case 2:
                                alphaY *= (float)Math.Pow(0.5 + 0.5 * (xFromCenter1 / textureRadius * xFromCenter1 / textureRadius), circularStretchY);
                                break;
                        }

                        // Apply alpha scaling to calculate
                        // the glow circle/square position
                        if (alphaX == 0.0f) alphaX = 0.0f;
                        if (alphaY == 0.0f) alphaY = 0.0f;
                        xFromCenter = xFromCenter1 / alphaX;
                        yFromCenter = yFromCenter1 / alphaY;
                    }
                                        
                    // Compute r
                    if (drawSquare)
                    {
                        // Square
                        // NOTE: Assigning the greater of actual x and y distances
                        r = Math.Abs(xFromCenter);
                        yDistance = Math.Abs(yFromCenter);
                        if (r < yDistance)
                        {
                            r = yDistance;
                        }
                    }
                    else 
                    {
                        // Circle
                        r = (float) Math.Sqrt(xFromCenter * xFromCenter + yFromCenter * yFromCenter);
                    }                                      
                    
                    // Compute phi
                    phi = (float) Math.Atan2(yFromCenter, xFromCenter);

                    // Compute a polar brightness factor applied to the circle/square
                    switch (polarBrightnessMode)
                    {
                        default:
                        case 0:
                            break;
                        case 1:
                            polarBrightnessFactor = (1.0f - polarAmp) + polarAmp * (float)Math.Pow(0.5 + 0.5 *
                                Math.Cos(polarFreq * (phi - polarPhase)), polarPower);
                            break;
                        case 2:
                            polarBrightnessFactor = 1.0f + polarAmp * (float)Math.Pow(0.5 + 0.5 *
                                Math.Cos(polarFreq * (phi - polarPhase)), polarPower);
                            break;
                    }                  
                    
                    // Compute brightness with brightness map interpolation
                    rIndex = (int)(r / deltaRadius);

                    // jea tbd determine why this check has to occur - alphaY becoming not a number
                    if (rIndex < 0) rIndex = 0;
                    
                    brightness = 0.0f;
                    if (rIndex < bMapSize)
                    {
                        brightness = brightnessMap[rIndex] + (r % deltaRadius) * dBrightness_dR[rIndex];
                        brightness *= (float)Math.Abs(polarBrightnessFactor);
                    }

                    // Zero color and add in the sum of any previous layer's pixel color
                    // NOTE: Assuming 32 bit color with byte order B, G, R, A
                    const int pixelSize = 4;
                    colorRed = 0;
                    colorGreen = 0;
                    colorBlue = 0;
                    if (addLayer == true)
                    {                       
                        colorRed += (int)bitmapRow[x * pixelSize + 2];
                        colorGreen += (int)bitmapRow[x * pixelSize + 1];
                        colorBlue += (int)bitmapRow[x * pixelSize + 0];  
                    }

                    // Compute pixel color
                    // NOTE: RGB, HSI, and HSL colors are assumed to be normalized in range of 0 to 1
                    if (r < glowCircleRadius)
                    {
                        switch (colorMode)
                        {
                            default:
                            case 0:  // RGB color
                                {
                                    colorRed += (int)(colorScaler * brightness * (red_m * r + red_b));
                                    colorGreen += (int)(colorScaler * brightness * (green_m * r + green_b));
                                    colorBlue += (int)(colorScaler * brightness * (blue_m * r + blue_b));
                                }
                                break;

                            case 1: // HSI color
                                {
                                    // Red value is used for hue, green for saturation,
                                    // and brightness for intensity
                                    hsiColor[0] = (red_m * r + red_b);
                                    hsiColor[1] = (green_m * r + green_b);
                                    hsiColor[2] = (blue_m * r + blue_b);

                                    //Debug.Print("hsl = " + hslColor[0].ToString() + " " + hslColor[1].ToString() + " " + hslColor[2].ToString());

                                    convertHSItoRGB(ref hsiColor, ref rgbColor);

                                    //Debug.Print("rgb = " + rgbColor[0].ToString() + " " + rgbColor[1].ToString() + " " + rgbColor[2].ToString());

                                    colorRed += (int)(colorScaler * brightness * rgbColor[0]);
                                    colorGreen += (int)(colorScaler * brightness * rgbColor[1]);
                                    colorBlue += (int)(colorScaler * brightness * rgbColor[2]);
                                }
                                break;

                            case 2: // HSL color
                                {
                                    // Red value is used for hue, green for saturation, and brightness for lightness
                                    hslColor[0] = (red_m * r + red_b);
                                    hslColor[1] = (green_m * r + green_b);
                                    hslColor[2] = (blue_m * r + blue_b);

                                    //Debug.Print("hsl = " + hslColor[0].ToString() + " " + hslColor[1].ToString() + " " + hslColor[2].ToString());

                                    convertHSLtoRGB(ref hslColor, ref rgbColor);

                                    //Debug.Print("rgb = " + rgbColor[0].ToString() + " " + rgbColor[1].ToString() + " " + rgbColor[2].ToString());

                                    colorRed += (int)(colorScaler * brightness * rgbColor[0]);
                                    colorGreen += (int)(colorScaler * brightness * rgbColor[1]);
                                    colorBlue += (int)(colorScaler * brightness * rgbColor[2]);
                                }
                                break;

                            case 3:  // Grey color
                                {
                                    colorGrey = (int)(colorScaler * brightness * (red_m * r + red_b));
                                    colorRed += colorGrey;
                                    colorGreen += colorGrey;
                                    colorBlue += colorGrey;
                                }
                                break;
                        }
                    }
                    else if (r < textureRadius)
                    {
                        switch (colorMode)
                        {
                            default:
                            case 0:  // RGB color
                                {
                                    colorRed += (int)(colorScaler * brightness * dRed);
                                    colorGreen += (int)(colorScaler * brightness * dGreen);
                                    colorBlue += (int)(colorScaler * brightness * dBlue);
                                }
                                break;

                            case 1: // HSI color
                                {
                                    // Red value is used for hue, green for saturation,
                                    // and brightness for intensity
                                    hsiColor[0] = dRed;
                                    hsiColor[1] = dGreen;
                                    hsiColor[2] = dBlue;
                                    
                                    convertHSItoRGB(ref hsiColor, ref rgbColor);

                                    colorRed += (int)(colorScaler * brightness * rgbColor[0]);
                                    colorGreen += (int)(colorScaler * brightness * rgbColor[1]);
                                    colorBlue += (int)(colorScaler * brightness * rgbColor[2]);
                                }
                                break;

                            case 2:  // HSL color
                                {
                                    // Red value is used for hue, green for saturation, and brightness for lightness
                                    hslColor[0] = dRed;
                                    hslColor[1] = dGreen;
                                    hslColor[2] = dBlue;

                                    convertHSLtoRGB(ref hslColor, ref rgbColor);

                                    colorRed += (int)(colorScaler * brightness * rgbColor[0]);
                                    colorGreen += (int)(colorScaler * brightness * rgbColor[1]);
                                    colorBlue += (int)(colorScaler * brightness * rgbColor[2]);
                                }
                                break;

                            case 3:  // Grey color
                                {
                                    colorGrey = (int)(colorScaler * brightness * dRed);
                                    colorRed += colorGrey;
                                    colorGreen += colorGrey;
                                    colorBlue += colorGrey;
                                }
                                break;
                        }
                    }

                    // Bounds check pixel color
                    if (colorRed > 255)
                    {
                        colorRed = 255;
                        overflow = true;
                    }
                    else if (colorRed < 0)
                    {
                        colorRed = 0;
                    }
                    if (colorGreen > 255)
                    {
                        colorGreen = 255;
                        overflow = true;
                    }
                    else if (colorGreen < 0)
                    {
                        colorGreen = 0;
                    }
                    if (colorBlue > 255)
                    {
                        colorBlue = 255;
                        overflow = true;
                    }
                    else if (colorBlue < 0)
                    {
                        colorBlue = 0;
                    }

                    // Assign pixel color
                    rowOffset = x * pixelSize;
                    bitmapRow[rowOffset + 3] = 255;
                    bitmapRow[rowOffset + 2] = (byte)colorRed;
                    bitmapRow[rowOffset + 1] = (byte)colorGreen;
                    bitmapRow[rowOffset] = (byte)colorBlue;
                    
                    // Copy to other pixels based on mirroring mode
                    switch (mirrorMode)
                    {
                        default:
                        case 0: // No mirroring                       
                            break;
                        case 1: // Mirror right half
                        case 2: // Mirror left half

                            // Assign row with x replaced by (size-1-x)
                            rowOffset = (t.size - 1 - x) * pixelSize;
                            bitmapRow[rowOffset + 3] = 255;
                            bitmapRow[rowOffset + 2] = (byte)colorRed;
                            bitmapRow[rowOffset + 1] = (byte)colorGreen;
                            bitmapRow[rowOffset] = (byte)colorBlue;
                            break;

                        case 3: // Mirror upper half
                        case 4: // Mirror lower half

                            // Assign mirror row with y replaced by (size-1-y)
                            bitmapMirrorRow[rowOffset + 3] = 255;
                            bitmapMirrorRow[rowOffset + 2] = (byte)colorRed;
                            bitmapMirrorRow[rowOffset + 1] = (byte)colorGreen;
                            bitmapMirrorRow[rowOffset] = (byte)colorBlue;
                            break;

                        case 5: // Mirror upper right quadrant
                        case 6: // Mirror upper left quadrant
                        case 7: // Mirror lower left quadrant
                        case 8: // Mirror lower right quadrant

                            // Assign mirror row
                            bitmapMirrorRow[rowOffset + 3] = 255;
                            bitmapMirrorRow[rowOffset + 2] = (byte)colorRed;
                            bitmapMirrorRow[rowOffset + 1] = (byte)colorGreen;
                            bitmapMirrorRow[rowOffset] = (byte)colorBlue;

                            // Assign row with x replaced by (size-1-x)
                            rowOffset = (t.size - 1 - x) * pixelSize;
                            bitmapRow[rowOffset + 3] = 255;
                            bitmapRow[rowOffset + 2] = (byte)colorRed;
                            bitmapRow[rowOffset + 1] = (byte)colorGreen;
                            bitmapRow[rowOffset] = (byte)colorBlue;

                            // Assign mirror row with x replaced by (size-1-x)
                            bitmapMirrorRow[rowOffset + 3] = 255;
                            bitmapMirrorRow[rowOffset + 2] = (byte)colorRed;
                            bitmapMirrorRow[rowOffset + 1] = (byte)colorGreen;
                            bitmapMirrorRow[rowOffset] = (byte)colorBlue;
                            break;

                        case 9: // Mirror upper right quadrant's lower half octant

                            // Assign mirror row
                            bitmapMirrorRow[rowOffset + 3] = 255;
                            bitmapMirrorRow[rowOffset + 2] = (byte)colorRed;
                            bitmapMirrorRow[rowOffset + 1] = (byte)colorGreen;
                            bitmapMirrorRow[rowOffset] = (byte)colorBlue;

                            // Assign row with x replaced by (size-1-x)
                            rowOffset = (t.size - 1 - x) * pixelSize;
                            bitmapRow[rowOffset + 3] = 255;
                            bitmapRow[rowOffset + 2] = (byte)colorRed;
                            bitmapRow[rowOffset + 1] = (byte)colorGreen;
                            bitmapRow[rowOffset] = (byte)colorBlue;

                            // Assign mirror row with x replaced by (size-1-x)
                            bitmapMirrorRow[rowOffset + 3] = 255;
                            bitmapMirrorRow[rowOffset + 2] = (byte)colorRed;
                            bitmapMirrorRow[rowOffset + 1] = (byte)colorGreen;
                            bitmapMirrorRow[rowOffset] = (byte)colorBlue;

                            // Get a pointer to the bitmap's x row
                            byte* bitmapRowX = (byte*)bmd.Scan0 + ((t.size - 1 - x) * bmd.Stride);

                            // Get a pointer to the bitmap's mirror x row
                            byte* bitmapMirrorRowX = (byte*)bmd.Scan0 + (x * bmd.Stride);

                            // Assign upper octant
                            rowOffset = y * pixelSize;
                            bitmapRowX[rowOffset + 3] = 255;
                            bitmapRowX[rowOffset + 2] = (byte)colorRed;
                            bitmapRowX[rowOffset + 1] = (byte)colorGreen;
                            bitmapRowX[rowOffset] = (byte)colorBlue;

                            // Assign mirror row
                            bitmapMirrorRowX[rowOffset + 3] = 255;
                            bitmapMirrorRowX[rowOffset + 2] = (byte)colorRed;
                            bitmapMirrorRowX[rowOffset + 1] = (byte)colorGreen;
                            bitmapMirrorRowX[rowOffset] = (byte)colorBlue;

                            // Assign row with y replaced by (size-1-y)
                            rowOffset = (t.size - 1 - y) * pixelSize;
                            bitmapRowX[rowOffset + 3] = 255;
                            bitmapRowX[rowOffset + 2] = (byte)colorRed;
                            bitmapRowX[rowOffset + 1] = (byte)colorGreen;
                            bitmapRowX[rowOffset] = (byte)colorBlue;

                            // Assign mirror row with y replaced by (size-1-y)
                            bitmapMirrorRowX[rowOffset + 3] = 255;
                            bitmapMirrorRowX[rowOffset + 2] = (byte)colorRed;
                            bitmapMirrorRowX[rowOffset + 1] = (byte)colorGreen;
                            bitmapMirrorRowX[rowOffset] = (byte)colorBlue;
                            break;
                    }

                    //hsl[0] = l.centerHue;
                    //hsl[1] = l.centerSaturation;
                    //hsl[2] = l.centerBrightness;

                    //hsl[0] = l.diameterHue;
                    //hsl[1] = l.diameterSaturation;
                    //hsl[2] = l.diameterBrightness;

                    // Convert from hsl to rgb
                    //convertHSLtoRGB(ref hsl, ref rgb);

                    //// Assign color to pixel 
                    //Color color = Color.FromArgb(
                    //    255,
                    //    (int)(255.0f * rgb[0]),
                    //    (int)(255.0f * rgb[1]),
                    //    (int)(255.0f * rgb[2]));
                    //Color color = Color.FromArgb(255, 255, 255, 255);
                    //Color color = new Color(brightness, brightness, brightness, 0.0f);
                }
            }

            // Allocate smoothing buffer to store filtered values while processing
            int smoothingBufferSize = 2 * 3 * (t.size - 2);
            float[] smoothingBuffer = new float[smoothingBufferSize];
            int bufferReadIndex = 0;
            int bufferWriteIndex = smoothingBufferSize/2;

            // Loop thru and apply a binomial 3x3 smoothing filter to the image
            for (int i=0; i<filterPassCount; i++) {

                for (int y = 1; y < t.size - 1; y++)
                {
                    // Get a pointer to the bitmap's y row 1, 2, and 3
                    byte* bitmapRow1 = (byte*)bmd.Scan0 + ((t.size - y) * bmd.Stride);
                    byte* bitmapRow2 = (byte*)bmd.Scan0 + ((t.size - 1 - y) * bmd.Stride);
                    byte* bitmapRow3 = (byte*)bmd.Scan0 + ((t.size - 2 - y) * bmd.Stride);
                    for (int x = 1; x < t.size - 1; x++)
                    {
                        // Assign image row offset to point to center pixel of filter
                        rowOffset = 4 * x;

                        // Computed a weighted average of neighboring pixels and store
                        // in the buffer

                        // Red
                        smoothingBuffer[bufferWriteIndex + 2] = bitmapRow1[rowOffset - 2] + bitmapRow1[rowOffset + 2] + bitmapRow1[rowOffset + 6];
                        smoothingBuffer[bufferWriteIndex + 2] += bitmapRow2[rowOffset - 2] + bitmapRow2[rowOffset + 2] + bitmapRow2[rowOffset + 6];
                        smoothingBuffer[bufferWriteIndex + 2] += bitmapRow3[rowOffset - 2] + bitmapRow3[rowOffset + 2] + bitmapRow3[rowOffset + 6];
                        smoothingBuffer[bufferWriteIndex + 2] /= 9.0f;

                        // Green
                        smoothingBuffer[bufferWriteIndex + 1] = bitmapRow1[rowOffset - 3] + bitmapRow1[rowOffset + 1] + bitmapRow1[rowOffset + 5];
                        smoothingBuffer[bufferWriteIndex + 1] += bitmapRow2[rowOffset - 3] + bitmapRow2[rowOffset + 1] + bitmapRow2[rowOffset + 5];
                        smoothingBuffer[bufferWriteIndex + 1] += bitmapRow3[rowOffset - 3] + bitmapRow3[rowOffset + 1] + bitmapRow3[rowOffset + 5];
                        smoothingBuffer[bufferWriteIndex + 1] /= 9.0f;

                        // Blue
                        smoothingBuffer[bufferWriteIndex] = bitmapRow1[rowOffset - 4] + bitmapRow1[rowOffset] + bitmapRow1[rowOffset + 4];
                        smoothingBuffer[bufferWriteIndex] += bitmapRow2[rowOffset - 4] + bitmapRow2[rowOffset] + bitmapRow2[rowOffset + 4];
                        smoothingBuffer[bufferWriteIndex] += bitmapRow3[rowOffset - 4] + bitmapRow3[rowOffset] + bitmapRow3[rowOffset + 4];
                        smoothingBuffer[bufferWriteIndex] /= 9.0f;
                        
                        if (x > 1)
                        {
                            bitmapRow1[rowOffset - 2] = (byte)smoothingBuffer[bufferReadIndex - 1];
                            bitmapRow1[rowOffset - 3] = (byte)smoothingBuffer[bufferReadIndex - 2];
                            bitmapRow1[rowOffset - 4] = (byte)smoothingBuffer[bufferReadIndex - 3];
                            if (y == t.size - 2)
                            {
                                bitmapRow2[rowOffset - 2] = (byte)smoothingBuffer[bufferWriteIndex - 1];
                                bitmapRow2[rowOffset - 3] = (byte)smoothingBuffer[bufferWriteIndex - 2];
                                bitmapRow2[rowOffset - 4] = (byte)smoothingBuffer[bufferWriteIndex - 3];
                            }
                            if (x == t.size - 2)
                            {
                                bitmapRow1[rowOffset + 2] = (byte)smoothingBuffer[bufferReadIndex + 2];
                                bitmapRow1[rowOffset + 1] = (byte)smoothingBuffer[bufferReadIndex + 1];
                                bitmapRow1[rowOffset] = (byte)smoothingBuffer[bufferReadIndex];

                                if (y == t.size - 2)
                                {
                                    bitmapRow2[rowOffset + 2] = (byte)smoothingBuffer[bufferWriteIndex + 2];
                                    bitmapRow2[rowOffset + 1] = (byte)smoothingBuffer[bufferWriteIndex + 1];
                                    bitmapRow2[rowOffset] = (byte)smoothingBuffer[bufferWriteIndex];

                                }
                            }
                        }

                        // Increment read and write buffers
                        bufferReadIndex += 3;
                        bufferWriteIndex += 3;
                    }

                    // Loop the buffer indices
                    bufferReadIndex = bufferReadIndex % smoothingBufferSize;
                    bufferWriteIndex = bufferWriteIndex % smoothingBufferSize;
                }
            }  
       
            // Unlock the texture bitmap's data
            t.bitmap.UnlockBits(bmd);

            // Return overflow and fast draw information 
            int returnValue = 0;
            if (overflow)
            {
                returnValue |= 0x01;
            }
            if (simpleRadialPolar == false)
            {
                returnValue |= 0x02;
            }
            return returnValue;            
        }

        void convertRGBtoHSL(ref float[] HSLColor, ref float[] RGBColor)
        {
            // Find max and min of RGB
            // NOTE: RGB and HSL colors are assumed to be normalized with range of 0 to 1
            float max = RGBColor[0];
            float min = RGBColor[0];
            if (RGBColor[1] > max)
            {
                max = RGBColor[1];
            }
            else if (RGBColor[1] < min)
            {
                min = RGBColor[1];
            }
            if (RGBColor[2] > max)
            {
                max = RGBColor[2];
            }
            else if (RGBColor[2] < min)
            {
                min = RGBColor[2];
            }

            // Hue
            // NOTE: Normalized to range of 0 to 1 instead of 0 to 360
            if (max == min)
            {
                HSLColor[0] = 0.0f; 
            }
            else if (max == RGBColor[0])
            {
                HSLColor[0] = ((RGBColor[1] - RGBColor[2]) / (max - min) / 6.0f + 1.0f) % 1.0f;
            }
            else if (max == RGBColor[1])
            {
                HSLColor[0] = ((RGBColor[2] - RGBColor[0]) / (max - min) / 6.0f + 1.0f / 3.0f) % 1.0f;
            }
            else if (max == RGBColor[2])
            {
                HSLColor[0] = ((RGBColor[0] - RGBColor[1]) / (max - min) / 6.0f + 2.0f / 3.0f) % 1.0f;
            }

            // Lightness
            HSLColor[2] = 0.5f * (max + min);

            // Saturation
            if (max == min)
            {
                HSLColor[1] = 0.0f;
            }
            else if (HSLColor[2] <= 0.5f)
            {
                HSLColor[1] = (max - min) / (max + min);
            }
            else
            {
                HSLColor[1] = (max - min) / (2 - (max + min));
            }
        }


        void convertHSLtoRGB(ref float[] HSLColor, ref float[] RGBColor)
        {
            // Define intermediate scalers, q and p, as functions of lightness and saturation
            // NOTE: HSL and RGB colors are assumed to be normalized with range of 0 to 1
            float q;
            if (HSLColor[2] < 0.5f)
            {
                q = HSLColor[2] * (1.0f + HSLColor[1]);
            }
            else
            {
                q = HSLColor[2] + HSLColor[1] - (HSLColor[2] * HSLColor[1]);
            }
            float p = 2.0f * HSLColor[2] - q;

            // Define intermediate vector t as a function of hue
            float[] t = { HSLColor[0] + 1.0f / 3.0f, HSLColor[0], HSLColor[0] - 1.0f / 3.0f };

            // Loop thru t components computing red, green, and then blue color
            for (int i = 0; i < 3; i++)
            {
                // Limit t values to [0, 1]
                if (t[i] < 0.0f)
                {
                    t[i] += 1.0f;
                }
                else if (t[i] > 1.0f)
                {
                    t[i] -= 1.0f;
                }

                // Assign RGB color as a function of t, q, and p
                if (t[i] < 1.0f / 6.0f)
                {
                    RGBColor[i] = p + ((q - p) * 6.0f * t[i]);
                }
                else if (t[i] < 0.5f)
                {
                    RGBColor[i] = q;
                }
                else if (t[i] < 2.0f / 3.0f)
                {
                    RGBColor[i] = p + ((q - p) * 6.0f * (2.0f / 3.0f - t[i]));
                }
                else
                {
                    RGBColor[i] = p;
                }
            }
        }

        void convertHSItoRGB(ref float[] HSIColor, ref float[] RGBColor)
        {
            // NOTE: HSI and RGB colors are assumed to be normalized with range of 0 to 1
            if (HSIColor[0] == 0.0f)
            {
                RGBColor[0] = HSIColor[2] * (1.0f + 2.0f * HSIColor[1]);
                RGBColor[1] = HSIColor[2] * (1.0f - HSIColor[1]);
                RGBColor[2] = RGBColor[1];
            }
            else if (HSIColor[0] < (1.0f / 3.0f))
            {
                float hueFactor = (float)(Math.Cos(2.0f * Math.PI * HSIColor[0]) / Math.Cos(2.0f * Math.PI * ((1.0f / 6.0f) - HSIColor[0])));
                RGBColor[0] = HSIColor[2] * (1.0f + HSIColor[1] * hueFactor);
                RGBColor[1] = HSIColor[2] * (1.0f + HSIColor[1] * (1.0f - hueFactor));
                RGBColor[2] = HSIColor[2] * (1.0f - HSIColor[1]);
            }
            else if (HSIColor[0] == (1.0f / 3.0f))
            {
                RGBColor[0] = HSIColor[2] * (1.0f - HSIColor[1]);
                RGBColor[1] = HSIColor[2] * (1.0f + 2.0f * HSIColor[1]);                
                RGBColor[2] = RGBColor[0];
            }
            else if (HSIColor[0] < (2.0f / 3.0f))
            {
                float hueFactor = (float)(Math.Cos(2.0f * Math.PI * (HSIColor[0] - (1.0f / 3.0f))) / Math.Cos(2.0f * Math.PI * (0.5f - HSIColor[0])));
                RGBColor[0] = HSIColor[2] * (1.0f - HSIColor[1]);
                RGBColor[1] = HSIColor[2] * (1.0f + HSIColor[1] * hueFactor);
                RGBColor[2] = HSIColor[2] * (1.0f + HSIColor[1] * (1.0f - hueFactor));                
            }
            else if (HSIColor[0] == (2.0f / 3.0f))
            {
                RGBColor[0] = HSIColor[2] * (1.0f - HSIColor[1]);
                RGBColor[1] = RGBColor[0];
                RGBColor[2] = HSIColor[2] * (1.0f + 2.0f * HSIColor[1]);                
            }
            else if (HSIColor[0] <= 1.0f)
            {
                float hueFactor = (float)(Math.Cos(2.0f * Math.PI * (HSIColor[0] - 2.0f / 3.0f)) / Math.Cos(2.0f * Math.PI * (5.0f / 6.0f - HSIColor[0])));
                RGBColor[0] = HSIColor[2] * (1.0f + HSIColor[1] * (1.0f - hueFactor));
                RGBColor[1] = HSIColor[2] * (1.0f - HSIColor[1]);
                RGBColor[2] = HSIColor[2] * (1.0f + HSIColor[1] * hueFactor);
            }      
        }

    }
}


// ***** Legacy dev code - keep for now, just in case *******
//// Find distance from center
////double xFromCenter = xFromCenter1;

//// first spiked ship - angle of 1.3*PI/4 - x and y mirroring
////double alpha = .25*(.3+pow(fabs(cos(2.0*M_PI*xFromCenter/(diameter*textureRadius))), 26));

////double alpha = .5*fabs(cos(0.3*M_PI*xFromCenter/(diameter*textureRadius)));


////double alpha = .5*fabs(cos(clipTime))*(.3+pow(fabs(cos(M_PI*xFromCenter/textureRadius)), 6));

////double alpha = .15*(0.3+pow(fabs(cos(M_PI*xFromCenter/(diameter*textureRadius))), 6));


////double alpha = .5*fabs(cos(clipTime))*fabs(cos(0.5*M_PI*xFromCenter/textureRadius));

////double alpha = .5*fabs(cos(clipTime))*(.3+pow(fabs(cos(M_PI*xFromCenter/textureRadius)), 6));

////double alpha = 0.0;// .25*(.3+pow(fabs(sin(2.0*M_PI*xFromCenter/(diameter*textureRadius))), 26));


////            // maybe a ship - with mirroring about the y
////            if(yFromCenter1<0) {
////                alpha = .25*(.3+pow(fabs(cos(2.0*M_PI*xFromCenter/(diameter*textureRadius))), 26));
////            }
////            else {
////                alpha = (.25+ 0.75*pow(fabs(sin(2.0*M_PI*xFromCenter/(diameter*textureRadius))), 1));
////            }


////double alpha = .75*(.3+pow(fabs(cos(M_PI*xFromCenter/(diameter*textureRadius))), 4));


////double alpha = .15*(.2+pow(fabs(cos(0.5*M_PI*xFromCenter/(diameter*textureRadius))), 1));

////double alpha = .15*(0.3+pow(fabs(cos(2.0*M_PI*xFromCenter/(diameter*textureRadius))), 6));

////double alpha;

//// ship 1
////            if(yFromCenter1<0) {
//// alpha = .25*fabs(1.5+cos(4.0*M_PI*xFromCenter/(diameter*textureRadius)));
////            }
////            else {
////                alpha = .15*fabs(cos(0.5*M_PI*xFromCenter/(diameter*textureRadius)));
////            }

//// saucer 1
////            if(yFromCenter1<0) {
////                alpha = .15*(0.2+pow(fabs(cos(M_PI*xFromCenter/(diameter*textureRadius))), 6));
////            }
////            else {
////                alpha = .15*fabs(cos(0.5*M_PI*xFromCenter/(diameter*textureRadius)));
////            }

//// dipole swirl
////alpha = (1.2+cos(2.0*theta1))/2.2;

//// 4 arm spiral with a little center and thin arms
////alpha = 0.75*pow((2.2+cos(4.0*theta1))/3.2, 10.0)+0.25;

////double alpha = .25*(.3+pow(fabs(cos(2.0*M_PI*xFromCenter/textureRadius)), 26));

////alpha = (1.0 + 0.95*sin(3.0*theta1))/1.95;

////alpha = 1.5*(0.3 + cos(3.0*theta1)*sin(3.0*theta1));


////alpha = .25*(.3+pow(fabs(cos(2.0*M_PI*xFromCenter/(diameter*textureRadius))), 26));



////            double a = 0.0;
////            double n = 5.0;
////            double cosine = cos(n*theta1);
////            double w;
////            if(cosine < 0.0) {
////                w = pow(-cosine, 1.0/n);
////                w *= -1.0;
////            }
////            else {
////                w = pow(cosine, 1.0/n);
////            }
////            alpha = (a + w)/(a + 1.0);
////            if(isnan(alpha) == TRUE) {
////                NSLog(@"hey hey");
////            }

////alpha += 2.0;
////alpha /= 3.0;
////if(alpha == 0.0) alpha = 1.0;
//// alpha = fabs(alpha);

////            double r;
////            if(alpha <= 0.0) {
////                r = 0.0;
////            }
////            else {
////                r = r2/alpha;
////            }

////r = fabs(r);


//float alphaX = 1.0f;
////double alphaY = pow(1.0-(xFromCenter1/(diameter*textureRadius)*xFromCenter1/(diameter*textureRadius)), 5.0);

////double alphaY = .75*(.3+pow(fabs(cos(2.8*M_PI*xFromCenter1/(textureRadius))), 22.4));

////double p = 4.005;

////if(xFromCenter1 > diameter*textureRadius) xFromCenter1 = diameter*textureRadius;
////if(xFromCenter1 < -diameter*textureRadius) xFromCenter1 = -diameter*textureRadius;

////            if(xFromCenter1 < diameter*textureRadius && xFromCenter1 > -diameter*textureRadius) {
////                alphaY *= pow((1.0-(xFromCenter1/(diameter*textureRadius)*xFromCenter1/(diameter*textureRadius))), p-0.5);
////            }
////            else {
////                alphaY = 222.0;
////
////            }
////if(yFromCenter1 < diameter*textureRadius && yFromCenter1 > -diameter*textureRadius) {
////    alphaX *= pow((1.0-(yFromCenter1/(diameter*textureRadius)*yFromCenter1/(diameter*textureRadius))), p-0.5);
////}

////alphaY *= pow(fabs(1.0-(xFromCenter1/(diameter*textureRadius)*xFromCenter1/(diameter*textureRadius))), 0.045);

//float alphaY = 1.0f;
//if (l.a.value != 1.0f)
//{
//    // Transform mode 0
    
//    alphaY = l.a.value * powf((1.0f - (xFromCenter1/textureRadius*xFromCenter1/textureRadius)), l.p.value);
//    alphaY *= (1.0f + l.d.value*powf(fabsf(cosf(l.n.value*PI*xFromCenter1/textureRadius)), l.q.value))/(1.0f + l.d.value);
//}
//else
//{
//    // Transform mode 1
//    // n and p are integers
    
//    float cosFunction = (cosf(0.5f*l.n.value*thetaTMS));
//    if (l.q.value >= 0.0f)
//    {
//        if (fmod(l.p.value, 2.0f) > 0.5f)
//        {
//            // p is odd
//            alphaY = powf((1.0f + l.d.value*powf(cosFunction, l.p.value))/(1.0f + fabsf(l.d.value)), l.q.value);
//        }
//        else
//        {
//            // p is even
//            if (l.d.value > 0.0f)
//            {
//                alphaY = powf((1.0f + l.d.value*powf(cosFunction, l.p.value))/(1.0f + l.d.value), l.q.value);
//            }
//            else
//            {
//                alphaY = powf((1.0f + l.d.value*powf(cosFunction, l.p.value)), l.q.value);
//            }
//        }
//    }
//    else
//    {
//        // Assign an unnormalized alphaY
//        //alphaY = (float)Math.Pow((1.0 + l.d.value * Math.Pow(cosFunction, l.p.value)), l.q.value);
        
//        if (fmod(l.p.value, 2.0f) > 0.5f)
//        {
//            // p is odd
//            alphaY = powf((1.0f + l.d.value*powf(cosFunction, l.p.value))/(1.0f - fabsf(l.d.value)), l.q.value);
//        }
//        else
//        {
//            // p is even
//            if (l.d.value > 0.0f)
//            {
//                alphaY = powf((1.0f + l.d.value*powf(cosFunction, l.p.value)), l.q.value);
//            }
//            else
//            {
//                alphaY = powf((1.0f + l.d.value*powf(cosFunction, l.p.value))/(1.0f + l.d.value), l.q.value);
//            }
//        }
//    }
//    alphaX = alphaY;
//}

////alphaX *= pow((1.0-(yFromCenter1/(diameter*textureRadius)*yFromCenter1/(diameter*textureRadius))), p-0.5);

//float xFromCenter = xFromCenter1/alphaX;
////double yFromCenter = yFromCenter1/alphaY;

//float yFromCenter = yFromCenter1/alphaY;
