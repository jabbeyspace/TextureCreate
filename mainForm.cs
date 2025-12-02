using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

using dataStructures;

namespace TextureCreate
{
    public partial class mainForm : Form
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(
            out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
            out long lpFrequency);

        private float currentTime = 0.0f;
        private float oldTime = 0.0f;
        private long clockTime = 0;
        private long clockFrequency = 1;

        private float clipTime = -1.0f;
        private bool animating = false;       
        private bool busyDrawing = false;

        // Properties
        private static mainForm instance = null;
        private customUpDown focusedUpDownControl = null;

        const int randParamDataIndexOffset = 4;
        const int randomParameterCount = 41;
        private customUpDown[] randomParameterControlArray = new customUpDown[randomParameterCount];
        
        private customUpDown xEditControl = null;
        private customUpDown yEditControl = null;

        private bool updateGUIOnly = false;
        private bool suppressUpdateBitmap = false;
        private bool updateBitmap = false;       

        private int mouseX = 0;
        private int mouseY = 0;
        private bool mouseEditing = false;

        private int textureCount = 0;
        private texture[] textureArray = null;

        private layer copyLayer;
        private texture copyTexture;

        private doubleGlowCircle dGlowCircle = new doubleGlowCircle();

        private int randomParameterEditMode = 0;

        private String[] collectionNames;

        private int editCollectionIndex = -1;
        private int editTextureIndex = -1;
        private int setEditLayerIndex = -1;
        private int editLayerIndex = -1;

        private String collectionPath = Application.StartupPath + "\\tc collections\\";
        private guiSettings settings = new guiSettings();      

        private Bitmap[] clip = null;
        private int clipImageCount = 0;
        private int generatedClipIndex = 0;
        private bool generatingClip = false;

        //String[] fun = new[10];


        //private int colorState;
        
        //public textureDescriptor td;// = new textureDescriptor();
        
        //private Bitmap drawingBitmap;

       
        // Constructor
        public mainForm()
        {
            // Don't allow multiple instances
            //if (_instance != null) return;
            this.DoubleBuffered = true;

            // Initialize handle to main form
            instance = this; 

            // Initialize form
            InitializeComponent();

            // Initialize controls 
            this.initializeControls();

            // Load gui settings from file
            this.loadGUISettingsFromFile();

            // Populate the array of texture collection names
            if (Directory.Exists(collectionPath) == true)
            {
                // Get an array of the collection file names
                String[] collectionFilesNames = Directory.GetFiles(collectionPath, "*.tcc");

                // Get an array of the collection indices
                int[] collectionIndices = new int[collectionFilesNames.Count()];
                for (int i = 0; i < collectionFilesNames.Count(); i++)
                {
                    using (BinaryReader reader =
                        new BinaryReader(File.Open(collectionFilesNames[i], FileMode.Open)))
                    {
                        // Read only the collection index
                        collectionIndices[i] = reader.ReadInt32();
                        
                        // Close the file
                        reader.Close();
                    }
                }
               
                // Sort and trim the file names into the collection names array
                collectionNames = new String[collectionFilesNames.Count()];
                for (int i = 0; i < collectionFilesNames.Count(); i++)
                {
                    int nextIndexToFileName = 0;
                    int nextLowestCollectionIndex = 10000;
                    for (int j = 0; j < collectionFilesNames.Count(); j++)
                    {
                        if (collectionIndices[j] != -1 && collectionIndices[j] < nextLowestCollectionIndex)
                        {
                            nextIndexToFileName = j;
                            nextLowestCollectionIndex = collectionIndices[j];
                        }                        
                    }
                    collectionIndices[nextIndexToFileName] = -1;
                    collectionNames[i] = Path.GetFileNameWithoutExtension(collectionFilesNames[nextIndexToFileName]);
                }

                // Add a default collection with a single texture if no collections exist
                if (collectionNames.Count() == 0)
                {
                    Array.Resize(ref collectionNames, 1);
                    collectionNames[0] = "collection1";                  
                }            
            }
            else
            {
                // Texture collections directory does not exist

                // Create a texture collecitons directory and add a default collection with a single texture
                Directory.CreateDirectory(collectionPath);
                Array.Resize(ref collectionNames, 1);
                collectionNames[0] = "collection1";         
            }

            // Populate the collections list box with the collection names
            this.collectionList.Items.Clear();
            for (int i = 0; i < collectionNames.Count(); i++)
            {
                this.collectionList.Items.Add(collectionNames[i]);
            }           
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            // Initialize gui to settings from file
            this.initializeGUIToSettings();
        }
        
        // Form Closing
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save the editor collection to file
            this.saveEditorCollectionToFile(this.editCollectionIndex);

            // Save the gui settings to file
            this.saveGUISettingsToFile();
        }      

        public static mainForm Instance
        {            
            get
            {
                return instance;
            }
        }

        unsafe private void loadEditorCollectionFromFile(int collectionIndex)
        {
            if (File.Exists(collectionPath + collectionNames[collectionIndex] + ".tcc") == false)
            {
                // File does not exist

                // Create a default file with one texture
                this.textureCount = 1;
                this.textureArray = new texture[1];
                this.textureArray[0].name = "texture1";
                this.textureArray[0].assignTextureToDefault();
                this.saveEditorCollectionToFile(collectionIndex);
            }
            else
            {
                // File exists
                try
                {   // Read the collection's texture array from file
                    using (BinaryReader reader =
                        new BinaryReader(File.Open(collectionPath + collectionNames[collectionIndex] + ".tcc", FileMode.Open)))
                    {
                        // Read the collection index
                        // NOTE: Reading index but not storing on load.
                        // Only used at start of app to order the collection list and for ordering output data folders
                        int temp = reader.ReadInt32();

                        // Read the texture count
                        this.textureCount = reader.ReadInt32();

                        // Read the textures
                        this.textureArray = new texture[textureCount];
                        for (int i = 0; i < textureCount; i++)
                        {
                            // Read texture name, size, clip duration, fps, texture scaler, and layer count
                            this.textureArray[i].name = reader.ReadString();
                            this.textureArray[i].size = reader.ReadInt32();
                            this.textureArray[i].clipDuration = reader.ReadSingle();
                            this.textureArray[i].framesPerSecond = reader.ReadSingle();
                            this.textureArray[i].textureScaler = reader.ReadSingle();
                            this.textureArray[i].layerCount = reader.ReadInt32();

                            // Allocate the texture's bitmap
                            this.textureArray[i].bitmap = new Bitmap(this.textureArray[i].size, this.textureArray[i].size);

                            // Read the layers
                            this.textureArray[i].layerArray = new layer[this.textureArray[i].layerCount];
                            for (int j = 0; j < this.textureArray[i].layerCount; j++)
                            {
                                // Read layer name
                                this.textureArray[i].layerArray[j].name = reader.ReadString();

                                // Read animation phase
                                this.textureArray[i].layerArray[j].animationPhase = reader.ReadSingle();

                                // jea tbd code - for use when adding new parameters
                                // ATTENTION: select each collection once and close to update data
                                //this.textureArray[i].layerArray[j].initializeLayerToDefault();                           

                                // Read random layer parameters
                                fixed (randomParameter* randomParameterArray = &this.textureArray[i].layerArray[j].diameter)
                                {
                                    for (int k = 0; k < randomParameterCount; k++)
                                    {
                                        //if (k >= 38) continue;
                                        randomParameterArray[k].value = reader.ReadSingle();
                                        randomParameterArray[k].leftLimit = reader.ReadSingle();
                                        randomParameterArray[k].rightLimit = reader.ReadSingle();
                                        randomParameterArray[k].parameterMode = reader.ReadInt32();
                                    }
                                }
                            }
                        }

                        // Close the file
                        reader.Close();
                    }
                }
                catch
                {
                    // Create a default file with one texture
                    this.textureCount = 1;
                    this.textureArray = new texture[1];
                    this.textureArray[0].name = "texture1";
                    this.textureArray[0].assignTextureToDefault();
                    this.saveEditorCollectionToFile(collectionIndex);
                }             
            }
        }

        private void saveCollectionIndicesToFile()
        {
            for (int i = 0; i < this.collectionNames.Count(); i++)
            {
                if (File.Exists(collectionPath + collectionNames[i] + ".tcc") == true)
                {
                    // Write the texture array to file
                    using (BinaryWriter writer =
                        new BinaryWriter(File.Open(collectionPath + collectionNames[i] + ".tcc", FileMode.Open)))
                    {
                        // Write the collection index
                        writer.Write(i);

                        // Close the file
                        writer.Close();
                    }
                }   
            }            
        }

        unsafe private void  saveEditorCollectionToFile(int collectionIndex)
        {
            // Write the texture array to file
            using (BinaryWriter writer =
                new BinaryWriter(File.Open(collectionPath + collectionNames[collectionIndex] + ".tcc", FileMode.Create)))
            {
                // Write the collection index
                writer.Write(collectionIndex);

                // Write the texture count
                writer.Write(this.textureCount);

                // Write the textures
                for (int i = 0; i < this.textureCount; i++)
                {
                    // Compute texture scaler as inverse of collision diameter
                    // NOTE: Value will always be greater than zero
                    this.textureArray[i].textureScaler = 1.0f;// / this.textureArray[i].layerArray[0].collisionDiameter1.value;

                    // Write texture name, size, clip duration, fps, texture scaler, and layer count
                    writer.Write(this.textureArray[i].name);
                    writer.Write(this.textureArray[i].size);
                    writer.Write(this.textureArray[i].clipDuration);
                    writer.Write(this.textureArray[i].framesPerSecond);
                    writer.Write(this.textureArray[i].textureScaler);
                    writer.Write(this.textureArray[i].layerCount);

                    // Write the layers
                    for (int j = 0; j < this.textureArray[i].layerCount; j++)
                    {
                        // Write layer name
                        writer.Write(this.textureArray[i].layerArray[j].name);  
  
                        // Write animation phase
                        writer.Write(this.textureArray[i].layerArray[j].animationPhase);
              
                        // Write layer's random parameters
                        fixed (randomParameter* randomParameterArray = &this.textureArray[i].layerArray[j].diameter)
                        {                           
                            for(int k=0; k<randomParameterCount; k++)
                            {
                                writer.Write(randomParameterArray[k].value);
                                writer.Write(randomParameterArray[k].leftLimit);
                                writer.Write(randomParameterArray[k].rightLimit);
                                writer.Write(randomParameterArray[k].parameterMode);
                            }
                        }               
                    }
                }

                // Close the file
                writer.Close();
            }
        }

        unsafe private void saveTextureDataToFile(int textureIndex, String path)
        {
            // Write the texture array to file
            using (BinaryWriter writer =
                new BinaryWriter(File.Open(path + this.textureArray[textureIndex].name + ".dat", FileMode.Create)))
            {
                // Compute texture scaler as inverse of collision diameter
                // NOTE: Value will always be greater than zero
                this.textureArray[textureIndex].textureScaler = 1.0f;// / this.textureArray[textureIndex].layerArray[0].collisionDiameter1.value;

                // Write texture size, clip duration, frames per second, texture scaler, and layer count
                writer.Write(this.textureArray[textureIndex].size);
                writer.Write(this.textureArray[textureIndex].clipDuration);
                writer.Write(this.textureArray[textureIndex].framesPerSecond);
                writer.Write(this.textureArray[textureIndex].textureScaler);
                writer.Write(this.textureArray[textureIndex].layerCount);

                // Write the layers
                for (int i = 0; i < this.textureArray[textureIndex].layerCount; i++)
                {
                    // Write animation phase
                    writer.Write(this.textureArray[textureIndex].layerArray[i].animationPhase);

                    // Write layer's random parameters
                    fixed (randomParameter* randomParameterArray = &this.textureArray[textureIndex].layerArray[i].diameter)
                    {
                        for (int j = 0; j < randomParameterCount; j++)
                        {
                            writer.Write(randomParameterArray[j].value);
                            writer.Write(randomParameterArray[j].leftLimit);
                            writer.Write(randomParameterArray[j].rightLimit);
                            writer.Write(randomParameterArray[j].parameterMode);
                        }
                    }
                }         

                // Close the file
                writer.Close();
            }
        }

        private void loadGUISettingsFromFile()
        {
            // Load gui settings from file

            // Read the settings from file if file exists
            bool setToDefault = false;
            if (File.Exists(Application.StartupPath + "\\settings.tc") == true)
            {         
                // File exists
                try
                {   // Read the texture array from file
                    using (BinaryReader reader =
                        new BinaryReader(File.Open(Application.StartupPath + "\\settings.tc", FileMode.Open)))
                    {
                        // Read the gui settings structure data into a byte array
                        byte[] byteArray = reader.ReadBytes(Marshal.SizeOf(typeof(guiSettings)));

                        // Get a handle to the byte array, marshall in the data into the settings structure, and free the handle
                        GCHandle handleByteArray = GCHandle.Alloc(byteArray, GCHandleType.Pinned);
                        settings = (guiSettings)Marshal.PtrToStructure(handleByteArray.AddrOfPinnedObject(), typeof(guiSettings));
                        handleByteArray.Free();

                        // Close the reader and its file stream
                        reader.Close();
                    }              
                }
                catch
                {
                    // Set to default
                    setToDefault = true;
                }                
            }
            else
            {
                // Set to default
                setToDefault = true;
            }

            // Assign default values if load settings failed or no file exists
            if(setToDefault == true)
            {
                // File does not exist
            
                // Set settings to default values
                settings.editCollectionIndex = 0;
                settings.editTextureIndex = 0;
                settings.editLayerIndex = 0;
                settings.selectedTabIndex = 0;
                settings.focusedUpDownControlIndex = -1;
                settings.xEditControlIndex = -1;
                settings.yEditControlIndex = -1;
                settings.randomParameterEditMode = 0;
                settings.viewAllLayers = false; 
                settings.multiEdit = false;
                settings.editRelative = false;
                settings.applyToAll = true;
                settings.generateClip = true;
                settings.showCollider = false;
                settings.loop = false;
            }
        }

        private void saveGUISettingsToFile()
        {   
            // Populate a gui settings structure with the current settings
            guiSettings settings = new guiSettings();
            settings.editCollectionIndex = this.editCollectionIndex;
            settings.editTextureIndex = this.editTextureIndex;
            settings.editLayerIndex = this.editLayerIndex;
            settings.selectedTabIndex = this.layerParameterTabs.SelectedIndex;
            if(this.focusedUpDownControl != null)
            {
                settings.focusedUpDownControlIndex = this.focusedUpDownControl.DataIndex;
            }
            else
            {
                settings.focusedUpDownControlIndex = -1;
            }
            if(this.xEditControl != null)
            {
                settings.xEditControlIndex = this.xEditControl.DataIndex;
            }
            else
            {
                settings.xEditControlIndex = -1;
            }
            if(this.yEditControl != null)
            {
                settings.yEditControlIndex = this.yEditControl.DataIndex;
            }
            else
            {
                settings.yEditControlIndex = -1;
            }
            settings.randomParameterEditMode = this.randomParameterEditMode;
            settings.viewAllLayers = this.checkViewAllLayers.Checked;
            settings.multiEdit = this.checkMultiEdit.Checked;
            settings.editRelative = this.checkEditRelative.Checked;
            settings.applyToAll = this.checkApplyToAllParams.Checked;
            settings.generateClip = this.checkGenerateClip.Checked;
            settings.showCollider = this.checkShowCollider.Checked;
            settings.loop = this.checkLoop.Checked;
                        
            // Marshall the structure data into a byte array            
            // NOTE: Create a byte array, get a handle to it, marshall the settings structure into it, and then free the handle
            byte[] byteArray = new byte[Marshal.SizeOf(typeof(guiSettings))];
            GCHandle handleByteArray = GCHandle.Alloc(byteArray, GCHandleType.Pinned);
            Marshal.StructureToPtr(settings, handleByteArray.AddrOfPinnedObject(), false);
            handleByteArray.Free();
            
            // Write the settings to file as a byte array 
            using (BinaryWriter writer =
                new BinaryWriter(File.Open(Application.StartupPath + "\\settings.tc", FileMode.Create)))
            {
                // Write the byte array
                writer.Write(byteArray);
                              
                // Close the reader and its file stream
                writer.Close();              
            }
        }

        private void initializeControls()
        {
            // Assign label back colors to the random parameter controls
            this.focusedRandomParamControl.LabelBackColor = Color.White;
            this.xEditRandomParamControl.LabelBackColor = Color.LightSkyBlue;
            this.yEditRandomParamControl.LabelBackColor = Color.LightPink;

            // Assign labels for the spin controls and initialize
            // the random paramater control array to all the layer spin controls

            // Texture spin controls
            this.spinSize.label = this.labelSize;
            this.spinClipDuration.label = this.labelClipDuration;
            this.spinFramesPerSecond.label = this.labelFramesPerSecond;            
   
            // Layer spin controls    
            this.spinAnimationPhase.label = this.labelAnimationPhase;

            this.randomParameterControlArray[0] = this.spinDiameter;
            this.randomParameterControlArray[0].label = this.labelDiameter;

            this.randomParameterControlArray[1] = this.spinScale;
            this.randomParameterControlArray[1].label = this.labelScale;

            this.randomParameterControlArray[2] = this.spinXOffset;
            this.randomParameterControlArray[2].label = this.labelXOffset;

            this.randomParameterControlArray[3] = this.spinYOffset;
            this.randomParameterControlArray[3].label = this.labelYOffset;

            this.randomParameterControlArray[4] = this.spinCollisionDiameter1;
            this.randomParameterControlArray[4].label = this.labelCollisionDiameter1;

            this.randomParameterControlArray[5] = this.spinCollisionDiameter2;
            this.randomParameterControlArray[5].label = this.labelCollisionDiameter2;

            this.randomParameterControlArray[6] = this.spinColorMode;
            this.randomParameterControlArray[6].label = this.labelColorMode;

            this.randomParameterControlArray[7] = this.spinCenterRed;
            this.randomParameterControlArray[7].label = this.labelCenterRed;

            this.randomParameterControlArray[8] = this.spinDiameterRed;
            this.randomParameterControlArray[8].label = this.labelDiameterRed;

            this.randomParameterControlArray[9] = this.spinCenterGreen;
            this.randomParameterControlArray[9].label = this.labelCenterGreen;

            this.randomParameterControlArray[10] = this.spinDiameterGreen;
            this.randomParameterControlArray[10].label = this.labelDiameterGreen;

            this.randomParameterControlArray[11] = this.spinCenterBlue;
            this.randomParameterControlArray[11].label = this.labelCenterBlue;

            this.randomParameterControlArray[12] = this.spinDiameterBlue;
            this.randomParameterControlArray[12].label = this.labelDiameterBlue;           

            this.randomParameterControlArray[13] = this.spinRadialPower;
            this.randomParameterControlArray[13].label = this.labelRadialPower;

            this.randomParameterControlArray[14] = this.spinPolarPower;
            this.randomParameterControlArray[14].label = this.labelPolarPower;

            this.randomParameterControlArray[15] = this.spinRadialAmp;
            this.randomParameterControlArray[15].label = this.labelRadialAmp;

            this.randomParameterControlArray[16] = this.spinPolarAmp;
            this.randomParameterControlArray[16].label = this.labelPolarAmp;

            this.randomParameterControlArray[17] = this.spinRadialFreq;
            this.randomParameterControlArray[17].label = this.labelRadialFreq;

            this.randomParameterControlArray[18] = this.spinPolarFreq;
            this.randomParameterControlArray[18].label = this.labelPolarFreq;

            this.randomParameterControlArray[19] = this.spinRadialPhase;
            this.randomParameterControlArray[19].label = this.labelRadialPhase;

            this.randomParameterControlArray[20] = this.spinPolarPhase;
            this.randomParameterControlArray[20].label = this.labelPolarPhase;

            this.randomParameterControlArray[21] = this.spinCenterBrightness;
            this.randomParameterControlArray[21].label = this.labelCenterBrightness;

            this.randomParameterControlArray[22] = this.spinDiameterBrightness;
            this.randomParameterControlArray[22].label = this.labelDiameterBrightness;

            this.randomParameterControlArray[23] = this.spinInnerGlowPower;
            this.randomParameterControlArray[23].label = this.labelInnerGlowPower;
            
            this.randomParameterControlArray[24] = this.spinOuterGlowPower;
            this.randomParameterControlArray[24].label = this.labelOuterGlowPower;

            this.randomParameterControlArray[25] = this.spinMirrorMode;
            this.randomParameterControlArray[25].label = this.labelMirrorMode;

            this.randomParameterControlArray[26] = this.spinRotateAngle;
            this.randomParameterControlArray[26].label = this.labelRotateAngle;

            this.randomParameterControlArray[27] = this.spinSwirlMode;
            this.randomParameterControlArray[27].label = this.labelSwirlMode;

            this.randomParameterControlArray[28] = this.spinSwirl;
            this.randomParameterControlArray[28].label = this.labelSwirl; 
         
            this.randomParameterControlArray[29] = this.spinLinearStretchX;
            this.randomParameterControlArray[29].label = this.labelLinearStretchX;

            this.randomParameterControlArray[30] = this.spinLinearStretchY;
            this.randomParameterControlArray[30].label = this.labelLinearStretchY;

            this.randomParameterControlArray[31] = this.spinCircularStretchX;
            this.randomParameterControlArray[31].label = this.labelCircularStretchX;

            this.randomParameterControlArray[32] = this.spinCircularStretchY;
            this.randomParameterControlArray[32].label = this.labelCircularStretchY;

            this.randomParameterControlArray[33] = this.spinMorphMode;
            this.randomParameterControlArray[33].label = this.labelMorphMode;

            this.randomParameterControlArray[34] = this.spinMorphAmp;
            this.randomParameterControlArray[34].label = this.labelMorphAmp;

            this.randomParameterControlArray[35] = this.spinMorphFreq;
            this.randomParameterControlArray[35].label = this.labelMorphFreq;
            
            this.randomParameterControlArray[36] = this.spinMorphPower;
            this.randomParameterControlArray[36].label = this.labelMorphPower;

            this.randomParameterControlArray[37] = this.spinMorphPhase;
            this.randomParameterControlArray[37].label = this.labelMorphPhase;

            this.randomParameterControlArray[38] = this.spinColliderXOffset;
            this.randomParameterControlArray[38].label = this.labelColliderXOffset;

            this.randomParameterControlArray[39] = this.spinColliderYOffset;
            this.randomParameterControlArray[39].label = this.labelColliderYOffset;

            this.randomParameterControlArray[40] = this.spinColliderRotateAngle;
            this.randomParameterControlArray[40].label = this.labelColliderRotateAngle;


            // Assign data indices for spin controls
            this.spinSize.DataIndex = 0;
            this.spinClipDuration.DataIndex = 1;
            this.spinFramesPerSecond.DataIndex = 2;
            this.spinAnimationPhase.DataIndex = 3;
            for (int i = 0; i < randomParameterCount; i++)
            {
                this.randomParameterControlArray[i].DataIndex = i + randParamDataIndexOffset;
            }
        }

        private void initializeGUIToSettings()
        {
            // Set to update GUI only
            this.updateGUIOnly = true;

            // Turn on double buffering
            this.DoubleBuffered = true;
                     
            // Initalize the gui to settings
            if (this.collectionList.Items.Count > settings.editCollectionIndex)
            {
                this.collectionList.SelectedIndex = settings.editCollectionIndex;
            }
            else
            {
                this.collectionList.SelectedIndex = 0;
            }
            this.textureList.SelectedIndex = -1;
            if (this.textureList.Items.Count > settings.editTextureIndex)
            {
                this.textureList.SelectedIndex = settings.editTextureIndex;
            }
            else
            {
                if (this.textureList.Items.Count > 0)
                {
                    this.textureList.SelectedIndex = 0;
                }
                else
                {
                    this.textureList.SelectedIndex = -1;
                }
            }
            this.layerList.SelectedIndex = -1;
            if (this.layerList.Items.Count > settings.editLayerIndex)
            {
                this.layerList.SelectedItems.Clear();
                this.layerList.SelectedIndex = settings.editLayerIndex;
            }
            else
            {
                if (this.layerList.Items.Count > 0)
                {
                    this.layerList.SelectedIndex = 0;
                }
                else
                {
                    this.layerList.SelectedIndex = -1;
                }
            }
            this.layerParameterTabs.SelectedIndex = settings.selectedTabIndex;
            switch(settings.focusedUpDownControlIndex)
            {
                case -1: // Do nothing
                    break;

                case 0: // Size
                    this.spinSize.Focus();
                    break;

                case 1: // Animation duration
                    this.spinClipDuration.Focus();
                    break;

                case 2: // Frames per second
                    this.spinFramesPerSecond.Focus();
                    break;

                case 3: // Animation phase
                    this.spinAnimationPhase.Focus();
                    break;

                default: // Random parameter                        
                    this.randomParameterControlArray[settings.focusedUpDownControlIndex - randParamDataIndexOffset].Focus();
                    break;
            }
            switch (settings.xEditControlIndex)
            {
                case -1: // Do nothing
                    break;

                case 0: // Size
                    this.labelMouseClicked(this.spinSize, MouseButtons.Left);
                    break;

                case 1: // Animation duration
                    this.labelMouseClicked(this.spinClipDuration, MouseButtons.Left);
                    break;

                case 2: // Frames per second
                    this.labelMouseClicked(this.spinFramesPerSecond, MouseButtons.Left);
                    break;

                case 3: // Animation phase
                    this.labelMouseClicked(this.spinAnimationPhase, MouseButtons.Left);
                    break;

                default: // Random parameter                        
                    this.labelMouseClicked(this.randomParameterControlArray[settings.xEditControlIndex - randParamDataIndexOffset], MouseButtons.Left);
                    break;
            }
            switch (settings.yEditControlIndex)
            {
                case -1: // Do nothing
                    break;

                case 0: // Size
                    this.labelMouseClicked(this.spinSize, MouseButtons.Right);
                    break;

                case 1: // Animation duration
                    this.labelMouseClicked(this.spinClipDuration, MouseButtons.Right);
                    break;

                case 2: // Frames per second
                    this.labelMouseClicked(this.spinFramesPerSecond, MouseButtons.Right);
                    break;

                case 3: // Animation phase
                    this.labelMouseClicked(this.spinAnimationPhase, MouseButtons.Right);
                    break;

                default: // Random parameter                        
                    this.labelMouseClicked(this.randomParameterControlArray[settings.yEditControlIndex - randParamDataIndexOffset], MouseButtons.Right);
                    break;
            }
            switch (settings.randomParameterEditMode)
            {
                default:
                case 0:
                    this.radioValue.Checked = true;
                    break;
                case 1:
                    this.radioLeftLimit.Checked = true;
                    break;
                case 2:
                    this.radioRightLimit.Checked = true;
                    break;
            }
            this.checkViewAllLayers.Checked = settings.viewAllLayers;
            this.checkMultiEdit.Checked = settings.multiEdit;
            this.checkEditRelative.Checked = settings.editRelative;
            this.checkApplyToAllParams.Checked = settings.applyToAll;
            this.checkGenerateClip.Checked = settings.generateClip;
            this.checkShowCollider.Checked = settings.showCollider;
            this.checkLoop.Checked = settings.loop;
                               
            // Set back to false
            this.updateGUIOnly = false;
        }

        unsafe public void updateGUIToCurrentLayer()
        {
            this.updateGUIToLayer(ref this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex]);
        }

        unsafe private void updateGUIToLayer(ref layer updateLayer)
        {
            // Initialize layer controls
            
            // Set to update gui only
            bool startingUpdateGUIOnlyState = this.updateGUIOnly;
            this.updateGUIOnly = true;

            // Update animation phase control value
            this.spinAnimationPhase.Value = (decimal)updateLayer.animationPhase;

            // Update the random parameter control values
            fixed (randomParameter* randomParameterArray = &updateLayer.diameter)
            {               
                for (int i = 0; i < randomParameterCount; i++)
                {
                    float parameterFactor = 1.0f;
                    if (this.randomParameterControlArray[i].Maximum == 362)
                    {
                        parameterFactor = 180.0f / (float)Math.PI;
                    }

                    switch (this.randomParameterEditMode)
                    {
                        case 0: // Value
                            this.randomParameterControlArray[i].Value = (decimal) (parameterFactor*randomParameterArray[i].value);
                            break;

                        case 1: // Left limit
                            this.randomParameterControlArray[i].Value = (decimal)(parameterFactor * randomParameterArray[i].leftLimit);
                            break;

                        case 2: // Right limit
                            this.randomParameterControlArray[i].Value = (decimal)(parameterFactor * randomParameterArray[i].rightLimit);
                            break;
                    }

                    // Set background color of controls' value based on parameters' mode
                    if (randomParameterArray[i].parameterMode == (int)parameterModeType.fixedParam)
                    {
                        this.randomParameterControlArray[i].BackColor = Color.White;
                    }
                    else if (randomParameterArray[i].parameterMode == (int)parameterModeType.randomParam)
                    {
                        this.randomParameterControlArray[i].BackColor = Color.Violet;
                    }
                    else if (randomParameterArray[i].parameterMode == (int)parameterModeType.animationParam)
                    {
                        this.randomParameterControlArray[i].BackColor = Color.LightGreen;
                    }
                    else if (randomParameterArray[i].parameterMode == (int)parameterModeType.animationBounceParam)
                    {
                        this.randomParameterControlArray[i].BackColor = Color.MediumSeaGreen;
                    }

                }            
            }

            // Color, mirror, swirl, and morph modes 
            this.updateColorModeText((int)this.spinColorMode.Value);
            this.updateMirrorModeText((int)this.spinMirrorMode.Value);
            this.updateSwirlModeText((int)this.spinSwirlMode.Value);
            this.updateMorphModeText((int)this.spinMorphMode.Value);
            
            // Check if a texture and layer are selected
            if (this.editTextureIndex != -1 && this.editLayerIndex != -1)
            {
                // Initialize or disable focused, x edit, and y edit random parameter controls if they are non-null 
                fixed (randomParameter* parameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                {
                    if (this.focusedUpDownControl != null)
                    {
                        if (this.focusedUpDownControl.DataIndex >= randParamDataIndexOffset)
                        {
                            this.focusedRandomParamControl.initializeToParameter(this.focusedUpDownControl,
                                ref parameterArray[this.focusedUpDownControl.DataIndex - randParamDataIndexOffset]);
                        }
                    }
                    if (this.xEditControl != null)
                    {
                        if (this.xEditControl.DataIndex >= randParamDataIndexOffset)
                        {
                            this.xEditRandomParamControl.initializeToParameter(this.xEditControl,
                                ref parameterArray[this.xEditControl.DataIndex - randParamDataIndexOffset]);
                        }                     
                    }
                    if (this.yEditControl != null)
                    {
                        if (this.yEditControl.DataIndex >= randParamDataIndexOffset)
                        {
                            this.yEditRandomParamControl.initializeToParameter(this.yEditControl,
                                ref parameterArray[this.yEditControl.DataIndex - randParamDataIndexOffset]);
                        }                       
                    }
                }
            }
            else
            {
                // No texture or layer is selected

                // Disable any focused, x edit, or y edit random parameter controls
                if (this.focusedUpDownControl != null)
                {
                    this.focusedRandomParamControl.disable();
                }
                if (this.xEditControl != null)
                {
                    this.xEditRandomParamControl.disable();
                }
                if (this.yEditControl != null)
                {
                    this.yEditRandomParamControl.disable();
                }
            }

            // Set back to starting state
            this.updateGUIOnly = startingUpdateGUIOnlyState;
        }

        unsafe private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (this.editTextureIndex != -1 && this.editLayerIndex != -1)
            {
                // Check for animation of generated clip
                if (this.animating == true && this.generatingClip == false && this.clipImageCount > 0)
                {
                    // Playing back a generated clip

                    // Draw the current clip time's clip image to the main picture box
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    e.Graphics.Clear(Color.Transparent);
                    int clipIndex = (int)(this.clipTime*this.textureArray[this.editTextureIndex].framesPerSecond);
                    clipIndex = clipIndex % this.clipImageCount;

                    // Rotate the texture 180 deg about the center of the pictureBox image
                    if (checkRotateImage180.Checked == true)
                    {
                        this.clip[clipIndex].RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    }

                    e.Graphics.DrawImage(this.clip[clipIndex], this.mainPictureBox.DisplayRectangle);                      
                }
                else
                {
                    // Check for update of bitmap
                    if (this.updateBitmap == true && this.busyDrawing == false)
                    {
                        // Set update bitmap to false
                        this.updateBitmap = false;

                        // Set busy drawing to true
                        this.busyDrawing = true;

                        // Draw to the texture's bitmap
                        int drawFunctionReturn;
                        bool overflow = false;
                        bool fastDraw = true;
                        //if (this.editTextureIndex != -1 && this.editLayerIndex != -1)
                        {
                            // Draw the layers
                            bool addLayer = false;
                            for (int i = 0; i < this.layerList.Items.Count; i++)
                            {
                                if (this.checkViewAllLayers.Checked == true || this.layerList.GetSelected(i) == true)
                                {
                                    layer drawLayer = this.textureArray[this.editTextureIndex].layerArray[i];
                                    randomParameter* randomParameterArray = &drawLayer.diameter;

                                    // Compute a normalized time for animation
                                    float normalTime = 0.0f;
                                    if (this.textureArray[this.editTextureIndex].clipDuration > 0.0f)
                                    {
                                        normalTime = (this.clipTime / this.textureArray[this.editTextureIndex].clipDuration
                                                        + drawLayer.animationPhase) % 1.0f;
                                    }

                                    // Loop thru and assign random parameter values based on normal time
                                    for (int j = 0; j < randomParameterCount; j++)
                                    {
                                        // Reassign value based on parameter edit mode, type, and animation state
                                        if (this.animating == false)
                                        {
                                            // Not animating
                                            switch (this.randomParameterEditMode)
                                            {
                                                case 0: // Value
                                                    break;

                                                case 1: // Left limit
                                                    randomParameterArray[j].value = randomParameterArray[j].leftLimit;
                                                    break;

                                                case 2: // Right Limit
                                                    randomParameterArray[j].value = randomParameterArray[j].rightLimit;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            // Animating
                                            if (this.textureArray[this.editTextureIndex].clipDuration > 0.0f)
                                            {
                                                if (randomParameterArray[j].parameterMode == (int)parameterModeType.animationParam)
                                                //if (this.textureArray[this.editTextureIndex].clipBounce == 0)
                                                {                                                   
                                                    // Animate from left limit to right limit
                                                    randomParameterArray[j].value = randomParameterArray[j].leftLimit +
                                                        normalTime * (randomParameterArray[j].rightLimit - randomParameterArray[j].leftLimit);
                                                }
                                                else if(randomParameterArray[j].parameterMode == (int)parameterModeType.animationBounceParam)
                                                {
                                                    // Animate from left limit to right limit and then back to left limit
                                                    float a = 0.5f * (randomParameterArray[j].leftLimit - randomParameterArray[j].rightLimit);
                                                    float b = 0.5f * (randomParameterArray[j].leftLimit + randomParameterArray[j].rightLimit);
                                                    randomParameterArray[j].value = a * (float)Math.Cos(2.0 * Math.PI * normalTime) + b;
                                                }
                                            }
                                        }
                                    }
                                    drawFunctionReturn = this.dGlowCircle.drawDoubleGlowCircle(ref this.textureArray[this.editTextureIndex], ref drawLayer, addLayer);
                                    if ((drawFunctionReturn & 0x01) > 0)
                                    {
                                        overflow = true;
                                    }
                                    if ((drawFunctionReturn & 0x02) > 0)
                                    {
                                        fastDraw = false;
                                    } 
                                    addLayer = true;
                                }
                            }
                        }
                        //else
                        //{
                        //    // Draw a rectangle to the edit texture's bitmap to clear it
                        //    // jea tbd look at different ways to do this
                        //    Rectangle rect = new Rectangle(0, 0, this.textureArray[this.editTextureIndex].size, this.textureArray[this.editTextureIndex].size);
                        //    this.textureArray[this.editTextureIndex].bitmap.Clone(rect, System.Drawing.Imaging.PixelFormat.Format32bppArgb);              
                        //}

                        // Update overflow label
                        if (overflow == false)
                        {
                            this.labelOverflow.BackColor = Color.Green;
                            this.labelOverflow.Text = "OK";
                        }
                        else
                        {
                            this.labelOverflow.BackColor = Color.Crimson;
                            this.labelOverflow.Text = "OVER";
                        }

                        // Update draw fast label
                        if (fastDraw)
                        {
                            this.labelFast.BackColor = Color.Green;
                            this.labelFast.Text = "FAST";
                        }
                        else
                        {
                            this.labelFast.BackColor = Color.Crimson;
                            this.labelFast.Text = "SLOW";
                        }

                        //this.dGlowCircle.drawDoubleGlowCircle(ref td);

                        // Update the drawing bitmap with the new texture
                        //this.drawingBitmap = new Bitmap(dGlowCircle.texture);

                        // Set busy drawing to false
                        this.busyDrawing = false;

                        // Trigger next bitmap if generating a clip
                        if (this.generatingClip == true)
                        {
                            this.generateNextClipBitmap();
                        }
                    }

                    // Draw the edit texture's bitmap to the main picture box
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    e.Graphics.Clear(Color.Transparent);

                    // Rotate the texture 180 deg about the center of the pictureBox image
                    if (checkRotateImage180.Checked == true)
                    {
                        this.textureArray[this.editTextureIndex].bitmap.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    }

                    e.Graphics.DrawImage(this.textureArray[this.editTextureIndex].bitmap, this.mainPictureBox.DisplayRectangle);                   
                }

                // Draw the white circle collider if set to show
                if (this.checkShowCollider.Checked == true)
                {
                    for (int i = 0; i < this.layerList.Items.Count; i++)
                    {
                        if (this.checkViewAllLayers.Checked == true || this.layerList.GetSelected(i) == true)
                        {
                            // Draw a simple collider

                            // Get pointers to the layer and the first collider parameter
                            layer l = this.textureArray[this.editTextureIndex].layerArray[i];
                            randomParameter* p = &l.collisionDiameter1;

                            // Compute a normalized time for animation
                            float normalTime = 0.0f;
                            if (this.textureArray[this.editTextureIndex].clipDuration > 0.0f)
                            {
                                normalTime = (this.clipTime / this.textureArray[this.editTextureIndex].clipDuration + l.animationPhase) % 1.0f;
                            }

                            // Loop thru and update layer's collider animation values
                            for (int j = 0; j < 5; j++)
                            {
                                switch (j)
                                {
                                    case 0:
                                    default:
                                        break;

                                    case 1:
                                        p = &l.collisionDiameter2;
                                        break;

                                    case 2:
                                        p = &l.colliderXOffset;
                                        break;

                                    case 3:
                                        p = &l.colliderYOffset;
                                        break;

                                    case 4:
                                        p = &l.colliderRotateAngle;
                                        break;
                                }

                                // Reassign value based on parameter edit mode, type, and animation state
                                if (this.animating == false)
                                {
                                    // Not animating
                                    switch (this.randomParameterEditMode)
                                    {
                                        case 0: // Value
                                            break;

                                        case 1: // Left limit
                                            p->value = p->leftLimit;
                                            break;

                                        case 2: // Right Limit
                                            p->value = p->rightLimit;
                                            break;
                                    }
                                }
                                else
                                {
                                    // Animating
                                    if (this.textureArray[this.editTextureIndex].clipDuration > 0.0f)
                                    {
                                        if (p->parameterMode == (int)parameterModeType.animationParam)
                                        //if (this.textureArray[this.editTextureIndex].clipBounce == 0)
                                        {
                                            // Animate from left limit to right limit
                                            p->value = p->leftLimit +  normalTime * (p->rightLimit - p->leftLimit);
                                        }
                                        else if(p->parameterMode == (int)parameterModeType.animationBounceParam)
                                        {
                                            // Animate from left limit to right limit and then back to left limit
                                            float a = 0.5f * (p->leftLimit - p->rightLimit);
                                            float b = 0.5f * (p->leftLimit + p->rightLimit);
                                            p->value = a * (float)Math.Cos(2.0 * Math.PI * normalTime) + b;
                                        }
                                    }
                                }
                            }

                            // Draw a circle, ellipse, or rectangle collider
                            if (l.collisionDiameter1.value != 0.0f)
                            {
                                e.Graphics.ResetTransform();
                                e.Graphics.TranslateTransform((float)this.mainPictureBox.Width * (0.5f + l.colliderXOffset.value), (float)this.mainPictureBox.Width * (0.5f - l.colliderYOffset.value));

                                // Need to translate to other side of center image when rotate 180 is enabled
                                if (checkRotateImage180.Checked == true)
                                {
                                    e.Graphics.TranslateTransform((float)this.mainPictureBox.Width * -2.0f * l.colliderXOffset.value, (float)this.mainPictureBox.Width * 2.0f * l.colliderYOffset.value);
                                }

                                e.Graphics.RotateTransform(-360.0f * l.colliderRotateAngle.value);

                                int drawRadiusX = (int)(l.collisionDiameter1.value * (float)this.mainPictureBox.Width) / 2;
                                if (l.collisionDiameter2.value == 0.0f)
                                {
                                    // Draw a circle collider
                                    e.Graphics.DrawEllipse(Pens.White, new Rectangle(-drawRadiusX, -drawRadiusX, 2 * drawRadiusX, 2 * drawRadiusX));
                                }
                                else if (l.collisionDiameter2.value > 0.0f)
                                {
                                    // Draw an ellipse collider
                                    int drawRadiusY = (int)(l.collisionDiameter2.value * (float)this.mainPictureBox.Width) / 2;
                                    e.Graphics.DrawEllipse(Pens.White, new Rectangle(-drawRadiusX, -drawRadiusY, 2 * drawRadiusX, 2 * drawRadiusY));
                                }
                                else
                                {
                                    // Draw a rectangle collider
                                    int drawRadiusY = (int)(-l.collisionDiameter2.value * (float)this.mainPictureBox.Width) / 2;
                                    e.Graphics.DrawRectangle(Pens.White, new Rectangle(-drawRadiusX, -drawRadiusY, 2 * drawRadiusX, 2 * drawRadiusY));
                                }
                            }                
                        }
                    }                   
                }               
            }          
        }

        unsafe public void updateToFocusedUpDownControl(customUpDown control, bool gotFocus)
        {                   
            // Update focused random parameter control if a random parameter is selected
            if (gotFocus == true)
            {
                // Update focused up down control
                if (this.focusedUpDownControl != null)
                {
                    // Update back color of control and its label
                    if (this.focusedUpDownControl == this.xEditControl)
                    {
                        if (this.mouseEditing == false)
                        {
                            this.focusedUpDownControl.BackColor = Color.White;
                        }
                        else
                        {
                            this.focusedUpDownControl.BackColor = Color.Yellow;
                        }
                        this.focusedUpDownControl.label.BackColor = Color.LightSkyBlue;
                    }
                    else if (this.focusedUpDownControl == this.yEditControl)
                    {
                        if (this.mouseEditing == false)
                        {
                            this.focusedUpDownControl.BackColor = Color.White;
                        }
                        else
                        {
                            this.focusedUpDownControl.BackColor = Color.Yellow;
                        }
                        this.focusedUpDownControl.label.BackColor = Color.LightPink;
                    }
                    else
                    {
                        this.focusedUpDownControl.BackColor = Color.White;
                        this.focusedUpDownControl.label.BackColor = Color.Transparent;
                    }
                }
                this.focusedUpDownControl = control;

                // Set back color to aqua and label's back color to white 
                control.BackColor = Color.Aqua;
                if (control.label != null)
                {
                    control.label.BackColor = Color.White;
                }                

                // Check if a random parameter, texture, and layer are selected
                if (control.DataIndex >= randParamDataIndexOffset && this.editTextureIndex != -1 && this.editLayerIndex != -1)
                {
                    // Initialize the focused random parameter control to the newly focused spin control 
                    fixed (randomParameter* parameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                    {
                        this.focusedRandomParamControl.initializeToParameter(control, ref parameterArray[control.DataIndex - randParamDataIndexOffset]);
                    }
                }
                else
                {
                    // Disable the focused random parameter control
                    this.focusedRandomParamControl.disable();
                }           
            }
            else
            {
                // Control lost focus
                
                // Set focused up down back color to white
                this.focusedUpDownControl.BackColor = Color.White;
            }      
        }

        unsafe public void updateTextureData(customUpDown control)
        {
            // Update texture data only if update only and texture and layer are selected
            if (this.updateGUIOnly == false && this.editTextureIndex != -1 && this.editLayerIndex != -1)
            {
                // Update data changes to layer(s) and redraw the texture image
             
                //// Force size to be even
                //if (this.spinSize.Value % 2 == 1)
                //{
                //    this.spinSize.Value -= 1;
                //}

                // Assign data index, value, min, max, and data index based on control's state
                int dataIndex = control.DataIndex;
                float value, min, max;
                if (control.Maximum == 362)
                {
                    // Phase parameters

                    // Wrap at limits
                    if (control.Value > control.Maximum - control.Increment)
                    {
                        control.Value = control.Minimum + control.Increment;
                    }
                    else if (control.Value < control.Minimum + control.Increment)
                    {
                        control.Value = control.Maximum - control.Increment;
                    }

                    // Assign value and limits
                    value = (float)control.Value * (float)(Math.PI / 180.0);                       
                    min = 0.0f;                        
                    max =(float)(2.0 * Math.PI);
                }
                else
                {
                    //if (this.Maximum == 256)
                    //{
                    //    // Wrap at limits
                    //    if (this.Value > this.Maximum - this.Increment)
                    //    {
                    //        this.Value = this.Minimum + this.Increment;
                    //    }
                    //    else if (this.Value < this.Minimum + this.Increment)
                    //    {
                    //        this.Value = this.Maximum - this.Increment;
                    //    }
                    //}

                    // Assign value and limits
                    value = (float) control.Value;
                    min = (float) control.Minimum;
                    max = (float) control.Maximum;
                }             
                                                
                // Update texture or layer parameter
                switch (dataIndex)
                {
                    case 0: // Texture size
                        this.textureArray[this.editTextureIndex].size = (int)value;
                        break;

                    case 1: // Clip duration
                        this.textureArray[this.editTextureIndex].clipDuration = (float)value;
                        break;

                    case 2: // Frames per second
                        this.textureArray[this.editTextureIndex].framesPerSecond = (float)value;
                        break;

                    case 3: // Animation phase
                        this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].animationPhase = (float)value;
                        break;

                    default:
                        {
                            // Update texture layer parameters
                            // NOTE: All layer parameters are random parameters except for animation phase

                            // Assign a parameter index offset by texture parameter count
                            int parameterIndex = dataIndex - randParamDataIndexOffset;

                            // Update any change in color, mirror, swirl, or morph modes 
                            if (control == this.spinColorMode)
                            {
                                this.updateColorModeText((int)this.spinColorMode.Value);
                            }
                            else if (control == this.spinMirrorMode)
                            {
                                this.updateMirrorModeText((int)this.spinMirrorMode.Value);
                            }
                            else if (control == this.spinSwirlMode)
                            {
                                this.updateSwirlModeText((int)this.spinSwirlMode.Value);
                            }
                            else if (control == this.spinMorphMode)
                            {
                                this.updateMorphModeText((int)this.spinMorphMode.Value);
                            }
                            
                            //// Check left and right control limits of data values
                            //if (this.randomParameterEditMode != 0)
                            //{
                            //    fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                            //    {
                            //        if (this.randomParameterEditMode == 1)
                            //        {
                            //            if (value > randomParameterArray[parameterIndex].rightLimit)
                            //            {
                            //                if (control.Maximum == 362)
                            //                {
                            //                    control.Value = (decimal)(180.0f / ((float)Math.PI) * randomParameterArray[parameterIndex].leftLimit);
                            //                }
                            //                else
                            //                {
                            //                    control.Value = (decimal)randomParameterArray[parameterIndex].leftLimit;
                            //                }
                            //                return;
                            //            }
                            //        }
                            //        else
                            //        {
                            //            if (value < randomParameterArray[parameterIndex].leftLimit)
                            //            {
                            //                if (control.Maximum == 362)
                            //                {
                            //                    control.Value = (decimal)(180.0f / ((float)Math.PI) * randomParameterArray[parameterIndex].rightLimit);
                            //                }
                            //                else
                            //                {
                            //                    control.Value = (decimal)randomParameterArray[parameterIndex].rightLimit;
                            //                }
                            //                return;
                            //            }
                            //        }
                            //    }
                            //}

                            // Check for multi-edit
                            if (this.checkMultiEdit.Checked == true)
                            {
                                // Multiple layer edit

                                // Check for edit relative
                                if (this.checkEditRelative.Checked == true)
                                {
                                    // Edit relative
                                    // NOTE: Only value editing is allowed

                                    // Get the delta value
                                    float deltaValue;
                                    fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                                    {
                                        deltaValue = value - randomParameterArray[parameterIndex].value;
                                    }

                                    // Now loop thru selected layers and add the delta value with bounds checks
                                    for (int i = 0; i < this.layerList.SelectedItems.Count; i++)
                                    {
                                        fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameter)
                                        {
                                            randomParameterArray[parameterIndex].value += deltaValue;
                                            if (randomParameterArray[parameterIndex].value > max)
                                            {
                                                randomParameterArray[parameterIndex].value = max;
                                            }
                                            else if (randomParameterArray[parameterIndex].value < min)
                                            {
                                                randomParameterArray[parameterIndex].value = min;
                                            }

                                            // If the parameter is 'fixed' then set left and right values to same
                                            if (randomParameterArray[parameterIndex].parameterMode == (int)parameterModeType.fixedParam)
                                            {
                                                randomParameterArray[parameterIndex].leftLimit = randomParameterArray[parameterIndex].value;
                                                randomParameterArray[parameterIndex].rightLimit = randomParameterArray[parameterIndex].value;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // Edit absolute

                                    // Loop thru selected layers and set layer parameter to its control's value
                                    for (int i = 0; i < this.layerList.SelectedItems.Count; i++)
                                    {
                                        fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameter)
                                        {
                                            // If the parameter is 'fixed' then set all values
                                            if (randomParameterArray[parameterIndex].parameterMode == (int)parameterModeType.fixedParam)
                                            {
                                                randomParameterArray[parameterIndex].value = value;
                                                randomParameterArray[parameterIndex].leftLimit = value;
                                                randomParameterArray[parameterIndex].rightLimit = value;
                                            }
                                            else
                                            {
                                                switch (this.randomParameterEditMode)
                                                {
                                                    default:
                                                    case 0: // Value
                                                        randomParameterArray[parameterIndex].value = value;
                                                        break;

                                                    case 1: // Left Limit
                                                        randomParameterArray[parameterIndex].leftLimit = value;
                                                        break;

                                                    case 2: // Right Limit
                                                        randomParameterArray[parameterIndex].rightLimit = value;
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Single layer edit

                                // Set layer parameter to its control's value
                                fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                                {
                                    // If the parameter is 'fixed' then set all values
                                    if (randomParameterArray[parameterIndex].parameterMode == (int)parameterModeType.fixedParam)
                                    {
                                        randomParameterArray[parameterIndex].value = value;
                                        randomParameterArray[parameterIndex].leftLimit = value;
                                        randomParameterArray[parameterIndex].rightLimit = value;
                                    }
                                    else
                                    {
                                        switch (this.randomParameterEditMode)
                                        {
                                            default:
                                            case 0: // Value
                                                randomParameterArray[parameterIndex].value = value;
                                                break;

                                            case 1: // Left Limit
                                                randomParameterArray[parameterIndex].leftLimit = value;
                                                break;

                                            case 2: // Right Limit
                                                randomParameterArray[parameterIndex].rightLimit = value;
                                                break;
                                        }
                                    }
                                }
                            }

                            // Update the value limits graphic
                            fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                            {
                                if (control == this.focusedUpDownControl)
                                {
                                    // Make sure random parameter control are in the same state
                                    // if both a focused and x or y edit control are active for the same up down control
                                    this.focusedRandomParamControl.updateGUIToData(ref randomParameterArray[parameterIndex]);
                                    if (this.focusedUpDownControl == this.xEditControl)
                                    {
                                        this.xEditRandomParamControl.updateGUIToData(ref randomParameterArray[parameterIndex]);
                                    }
                                    else if (this.focusedUpDownControl == this.yEditControl)
                                    {
                                        this.yEditRandomParamControl.updateGUIToData(ref randomParameterArray[parameterIndex]);
                                    }
                                }
                                else if (control == this.xEditControl)
                                {
                                    this.xEditRandomParamControl.updateGUIToData(ref randomParameterArray[parameterIndex]);
                                }
                                else if (control == this.yEditControl)
                                {
                                    this.yEditRandomParamControl.updateGUIToData(ref randomParameterArray[parameterIndex]);
                                }
                            }
                        }
                        break;
                                                
                }

                // Check if clip generation is occurring
                if (this.generatingClip == false)
                {
                    // Clip generation not occurring

                    // Reset the clip image count
                    // NOTE: Forces regeneration of clip animation
                    this.clipImageCount = 0;

                    // Trigger update of bimap if not suppressed
                    if (this.suppressUpdateBitmap == false)
                    {
                        // Set to update edit texture's bitmap
                        this.updateBitmap = true;

                        // Invalidate the main picture box
                        this.mainPictureBox.Invalidate();
                    }                               
                }
                else
                {
                    // Clip generation is occurring

                    // Restart clip generation

                    // Set generating clip to false
                    this.generatingClip = false;

                    // Reset the clip image count
                    // NOTE: Forces regeneration of clip animation
                    this.clipImageCount = 0;

                    // Reset the clip time to -1
                    this.clipTime = -1.0f;

                    // Restart the animation timer
                    this.animationTimer.Enabled = true;                   
                }                
            }          
        }

        public void updateColorModeText(int colorMode)
        {
            switch (colorMode)
            {
                default:
                case 0:  // RGB color
                    {
                        this.labelColorModeText.Text = "rgb color";

                        this.labelCenterRed.Text = "center red";
                        this.labelCenterGreen.Text = "center green";
                        this.labelCenterBlue.Text = "center blue";

                        this.labelDiameterRed.Text = "diameter red";
                        this.labelDiameterGreen.Text = "diameter green";
                        this.labelDiameterBlue.Text = "diameter blue";
                    }
                    break;

                case 1: // HSI color
                    {
                        this.labelColorModeText.Text = "hsi color";

                        this.labelCenterRed.Text = "center hue";
                        this.labelCenterGreen.Text = "center saturation";
                        this.labelCenterBlue.Text = "center intensity";

                        this.labelDiameterRed.Text = "diameter hue";
                        this.labelDiameterGreen.Text = "diameter saturation";
                        this.labelDiameterBlue.Text = "diameter intensity";
                    }
                    break;

                case 2: // HSL color
                    {
                        this.labelColorModeText.Text = "hsl color";

                        this.labelCenterRed.Text = "center hue";
                        this.labelCenterGreen.Text = "center saturation";
                        this.labelCenterBlue.Text = "center lightness";

                        this.labelDiameterRed.Text = "diameter hue";                        
                        this.labelDiameterGreen.Text = "diameter saturation";
                        this.labelDiameterBlue.Text = "diameter lightness";
                    }
                    break;

                case 3: // Grey
                    {
                        this.labelColorModeText.Text = "grey";

                        this.labelCenterRed.Text = "center brightnes 2";
                        this.labelCenterGreen.Text = "usused";
                        this.labelCenterBlue.Text = "unused";

                        this.labelDiameterRed.Text = "dia. brightness 2";
                        this.labelDiameterGreen.Text = "unused";
                        this.labelDiameterBlue.Text = "unused";
                    }
                    break;
            }
        }

        public void updateMirrorModeText(int mirrorMode)
        {
            switch (mirrorMode)
            {
                default:
                case 0:
                    this.labelMirrorModeText.Text = "mirror off";
                    break;

                case 1:
                    this.labelMirrorModeText.Text = "mirror right half";
                    break;

                case 2:
                    this.labelMirrorModeText.Text = "mirror left half";
                    break;

                case 3:
                    this.labelMirrorModeText.Text = "mirror upper half";
                    break;

                case 4:
                    this.labelMirrorModeText.Text = "mirror lower half";
                    break;

                case 5:
                    this.labelMirrorModeText.Text = "mirror upper right quad";
                    break;

                case 6:
                    this.labelMirrorModeText.Text = "mirror upper left quad";
                    break;

                case 7:
                    this.labelMirrorModeText.Text = "mirror lower left quad";
                    break;

                case 8:
                    this.labelMirrorModeText.Text = "mirror lower right quad";
                    break;

                case 9:
                    this.labelMirrorModeText.Text = "mirror first quad octant";
                    break;

                //case 9:
                //    this.labelMirrorModeText.Text = "vertical angle swirl";
                //    break;

                //case 10:
                //    this.labelMirrorModeText.Text = "horizontal angle swirl";
                //    break;
            }
        }

        public void updateSwirlModeText(int swirlMode)
        {
            switch (swirlMode)
            {
                default:
                case 0:
                    this.labelSwirlModeText.Text = "swirl off";
                    break;

                case 1:
                    this.labelSwirlModeText.Text = "r squared swirl";
                    break;

                case 2:
                    this.labelSwirlModeText.Text = "r swirl";
                    break;

                case 3:
                    this.labelSwirlModeText.Text = "reverse r sqrd swirl";
                    break;

                case 4:
                    this.labelSwirlModeText.Text = "x * y swirl";
                    break;

                case 5:
                    this.labelSwirlModeText.Text = "x swirl";
                    break;

                case 6:
                    this.labelSwirlModeText.Text = "y swirl";
                    break;

                case 7:
                    this.labelSwirlModeText.Text = "x squared swirl";
                    break;

                case 8:
                    this.labelSwirlModeText.Text = "y squared swirl";
                    break;

                case 9:
                    this.labelSwirlModeText.Text = "vertical angle swirl";
                    break;

                case 10:
                    this.labelSwirlModeText.Text = "horizontal angle swirl";
                    break;
            }
        }

        public void updateMorphModeText(int morphMode)
        {
            switch (morphMode)
            {
                default:
                case 0:
                    this.labelMorphModeText.Text = "morph off";
                    break;

                case 1:
                    this.labelMorphModeText.Text = "y morph by x";
                    break;

                case 2:
                    this.labelMorphModeText.Text = "x morph by y";
                    break;

                case 3:
                    this.labelMorphModeText.Text = "x,y morph by x";
                    break;

                case 4:
                    this.labelMorphModeText.Text = "x,y morph by y";
                    break;

                case 5:
                    this.labelMorphModeText.Text = "x,y morphed by y,x";
                    break;

                case 6:
                    this.labelMorphModeText.Text = "y morph by y";
                    break;

                case 7:
                    this.labelMorphModeText.Text = "x morph by y";
                    break;

                case 8:
                    this.labelMorphModeText.Text = "x,y morph by y";
                    break;

                case 9:
                    this.labelMorphModeText.Text = "x,y morph by x";
                    break;

                case 10:
                    this.labelMorphModeText.Text = "x,y morph by x,y";
                    break;

                case 11:
                    this.labelMorphModeText.Text = "x morph by r";
                    break;

                case 12:
                    this.labelMorphModeText.Text = "y morph by r";
                    break;

                case 13:
                    this.labelMorphModeText.Text = "x,y morph by r";
                    break;

                case 14:
                    this.labelMorphModeText.Text = "x morph by angle";
                    break;

                case 15:
                    this.labelMorphModeText.Text = "y morph by angle";
                    break;

                case 16:
                    this.labelMorphModeText.Text = "x,y morph by angle";
                    break;
            }
        }

        unsafe public void updateParameterMode(int dataIndex, parameterModeType parameterMode)
        {
            // Assign a parameter index offset by texture parameter count
            int parameterIndex = dataIndex - randParamDataIndexOffset;

            // Check for multi-edit
            if (this.checkMultiEdit.Checked == true)
            {
                // Multiple layer edit

                // Loop thru selected layers and set parameter mode of the layer parameter to its control's value
                for (int i = 0; i < this.layerList.SelectedItems.Count; i++)
                {
                    fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameter)
                    {
                        randomParameterArray[parameterIndex].parameterMode = (int)parameterMode;
                    }
                }
            }
            else
            {
                // Single layer edit

                // Set layer parameter to its control's value
                fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                {
                    randomParameterArray[parameterIndex].parameterMode = (int) parameterMode;
                }
            }           

            // Check if there is a focused up down control
            if (this.focusedUpDownControl != null)
            {
                // Make sure random parameter controls are in the same state
                // if both a focused and x or y edit control are active for the same up down control
                if (this.focusedUpDownControl == this.xEditControl)
                {
                    fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                    {
                        this.focusedRandomParamControl.updateGUIToData(ref randomParameterArray[parameterIndex]);
                        this.xEditRandomParamControl.updateGUIToData(ref randomParameterArray[parameterIndex]);
                    }               
                }
                else if (this.focusedUpDownControl == this.yEditControl)
                {
                    fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                    {
                        this.focusedRandomParamControl.updateGUIToData(ref randomParameterArray[parameterIndex]);
                        this.yEditRandomParamControl.updateGUIToData(ref randomParameterArray[parameterIndex]);
                    }               
                }

                // Set focus back to focused up down
                this.focusedUpDownControl.Focus();
            }

            // Check if clip generation is occurring
            if (this.generatingClip == false)
            {
                // Clip generation not occurring

                // Reset the clip image count
                // NOTE: Forces regeneration of clip animation
                this.clipImageCount = 0;
            }
            else
            {
                // Clip generation is occurring

                // Restart clip generation

                // Set generating clip to false
                this.generatingClip = false;

                // Reset the clip image count
                // NOTE: Forces regeneration of clip animation
                this.clipImageCount = 0;

                // Reset the clip time to -1
                this.clipTime = -1.0f;

                // Restart the animation timer
                this.animationTimer.Enabled = true;
            }

            // If the parameter mode changed - this will force the controls' backgound to draw a different color
            this.updateGUIToLayer(ref this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex]);
        }

        unsafe public void labelMouseClicked(customUpDown control, MouseButtons mouseButton)
        {
            // Toggle x and y mouse edit controls on and off
            if (mouseButton == MouseButtons.Left)
            {
                // On x edit control not null, set label back to clear or white if focused
                if (this.xEditControl != null)
                {
                    if (this.focusedUpDownControl != this.xEditControl)
                    {
                        this.xEditControl.label.BackColor = Color.Transparent;
                    }
                    else
                    {
                        this.xEditControl.label.BackColor = Color.White;
                    }
                }             

                // Left button hit - toggle x edit control
                if (this.xEditControl == control)
                {
                    // Turn off x edit control

                    // Disable the random parameter control
                    this.xEditRandomParamControl.disable();

                    // Set x edit control to null
                    this.xEditControl = null;
                }
                else
                {
                    if (this.yEditControl == control)
                    {
                        // Clear the y edit control to null and disable the y edit random parameter control
                        this.yEditControl = null;
                        this.yEditRandomParamControl.disable();
                    }
                    this.xEditControl = control;
                    this.xEditControl.label.BackColor = Color.LightSkyBlue;

                    // Update x edit random parameter control if a random parameter is selected
                    if (control.DataIndex >= randParamDataIndexOffset && this.editTextureIndex != -1 && this.editLayerIndex != -1)
                    {
                        // Initialize the x edit random parameter control to the newly focused up down control 
                        fixed (randomParameter* parameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                        {
                            this.xEditRandomParamControl.initializeToParameter(control, ref parameterArray[control.DataIndex - randParamDataIndexOffset]);
                        }
                    }
                    else
                    {
                        // Disable the x edit random parameter control
                        this.xEditRandomParamControl.disable();
                    }    
                }                
            }
            else if(mouseButton == MouseButtons.Right)
            {
                // On y edit control not null, set label back to clear or white if focused
                if (this.yEditControl != null)
                {
                    if (this.focusedUpDownControl != this.yEditControl)
                    {
                        this.yEditControl.label.BackColor = Color.Transparent;
                    }
                    else
                    {
                        this.yEditControl.label.BackColor = Color.White;
                    }
                }

                // Right button hit - toggle y edit control
                if (this.yEditControl == control)
                {
                    // Turn off y edit control

                    // Disable the random parameter control
                    this.yEditRandomParamControl.disable();

                    // Set y edit control to null
                    this.yEditControl = null;
                }
                else
                {
                    if (this.xEditControl == control)
                    {
                        // Clear the x edit control to null and disable the x edit random parameter control
                        this.xEditControl = null;                        
                        this.xEditRandomParamControl.disable();
                    }
                    this.yEditControl = control;
                    this.yEditControl.label.BackColor = Color.LightPink;

                    // Update y edit random parameter control if a random parameter is selected
                    if (control.DataIndex >= randParamDataIndexOffset && this.editTextureIndex != -1 && this.editLayerIndex != -1)
                    {
                        // Initialize the y edit random parameter control to the newly focused up down control 
                        fixed (randomParameter* parameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                        {
                            this.yEditRandomParamControl.initializeToParameter(control, ref parameterArray[control.DataIndex - randParamDataIndexOffset]);
                        }
                    }
                    else
                    {
                        // Disable the y edit random parameter control
                        this.yEditRandomParamControl.disable();
                    }    
                }                
            }
        }
   
        private void mainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {         
            // Initialize mouse position
            this.mouseX = e.X;
            this.mouseY = e.Y;
                                  
            // Toggle mouse editing on/off
            if (this.mouseEditing == true)
            {
                this.mouseEditing = false;
                if (this.xEditControl != null)
                {
                    this.xEditControl.BackColor = Color.White;
                }
                if (this.yEditControl != null)
                {
                    this.yEditControl.BackColor = Color.White;
                }
                if (this.focusedUpDownControl != null)
                {
                    this.focusedUpDownControl.BackColor = Color.Aqua;
                }
            }
            else
            {
                if (this.xEditControl != null)
                {
                    this.xEditControl.BackColor = Color.Yellow;
                }
                if (this.yEditControl != null)
                {
                    this.yEditControl.BackColor = Color.Yellow;
                }
                this.mouseEditing = true;
            }

            //this.mainPictureBox.Capture = true;
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            // Edit parameters based on mouse edit mode
            if (this.mouseEditing == false)
            {
                this.mouseX = e.X;
                this.mouseY = e.Y;
            }
            else
            {
                bool valueChange = false;
                this.suppressUpdateBitmap = true;
                if (this.xEditControl != null)
                {
                    if (this.xEditControl.BackColor != Color.Yellow)
                    {
                        this.xEditControl.BackColor = Color.Yellow;
                    }
                    int deltaX = e.X - this.mouseX;
                    decimal newValue = this.xEditControl.Value;
                    //double rangeFactor = 2.0 * (double)(this.xEditControl.Maximum - this.xEditControl.Minimum) / this.mainPictureBox.Width;
                    newValue += (decimal)(deltaX * this.xEditControl.Increment);
                    if (newValue > this.xEditControl.Maximum)
                    {
                        newValue = this.xEditControl.Maximum;
                        this.mouseX = e.X;
                    }
                    else if (newValue < this.xEditControl.Minimum)
                    {
                        newValue = this.xEditControl.Minimum;
                        this.mouseX = e.X;
                    }
                    if (this.xEditControl.Value != newValue)
                    {
                        valueChange = true;
                        this.xEditControl.Value = newValue;
                        this.mouseX = e.X;
                    }
                }
                if (this.yEditControl != null)
                {
                    if (this.yEditControl.BackColor != Color.Yellow)
                    {
                        this.yEditControl.BackColor = Color.Yellow;
                    }
                    int deltaY = this.mouseY - e.Y;
                    decimal newValue = this.yEditControl.Value;
                    //double rangeFactor = 2.0 * (double)(this.yEditControl.Maximum - this.yEditControl.Minimum) / this.mainPictureBox.Width;
                    newValue += (decimal)(deltaY * this.yEditControl.Increment);
                    if (newValue > this.yEditControl.Maximum)
                    {
                        newValue = this.yEditControl.Maximum;
                        this.mouseY = e.Y;
                    }
                    else if (newValue < this.yEditControl.Minimum)
                    {
                        newValue = this.yEditControl.Minimum;
                        this.mouseY = e.Y;
                    }
                    if (this.yEditControl.Value != newValue)
                    {
                        valueChange = true;                       
                        this.yEditControl.Value = newValue;
                        this.mouseY = e.Y;
                    }
                }
                this.suppressUpdateBitmap = false;
                if (valueChange == true)
                {
                    // Set to update edit texture's bitmap
                    this.updateBitmap = true;

                    // Invalidate the main picture box
                    this.mainPictureBox.Invalidate();
                }               
            }
        }

        unsafe private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // Switch random parameter edit mode
            if (e.KeyCode == Keys.F)
            {
                if (this.radioValue.Checked == true)
                {
                    this.radioLeftLimit.Checked = true;
                }
                else if (this.radioLeftLimit.Checked == true)
                {
                    this.radioRightLimit.Checked = true;
                }
                else
                {
                    this.radioValue.Checked = true;
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.D)
            {
                if (this.radioValue.Checked == true)
                {
                    this.radioRightLimit.Checked = true;
                }
                else if (this.radioRightLimit.Checked == true)
                {
                    this.radioLeftLimit.Checked = true;
                }
                else
                {
                    this.radioValue.Checked = true;
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.R)
            {
                if (this.editTextureIndex != -1 && this.editLayerIndex != -1)
                {
                    // Loop thru selected layers and randomize the active parameters between the left and right limits
                    for (int i = 0; i < this.layerList.SelectedItems.Count; i++)
                    {
                        fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameter)
                        {
                            if (this.focusedUpDownControl != null)
                            {
                                randomParameter* pParameter = &randomParameterArray[this.focusedUpDownControl.DataIndex - randParamDataIndexOffset];
                                if (pParameter->parameterMode == (int)parameterModeType.randomParam)
                                {
                                    pParameter->value = customMath.randomFloat(pParameter->leftLimit, pParameter->rightLimit);
                                    if (this.layerList.SelectedIndices[i] == this.editLayerIndex)
                                    {
                                        this.focusedRandomParamControl.updateGUIToData(ref *pParameter);
                                    }
                                }
                            }
                            if (this.xEditControl != null)
                            {
                                randomParameter* pParameter = &randomParameterArray[this.xEditControl.DataIndex - randParamDataIndexOffset];
                                if (pParameter->parameterMode == (int)parameterModeType.randomParam)
                                {
                                    pParameter->value = customMath.randomFloat(pParameter->leftLimit, pParameter->rightLimit);
                                    if (this.layerList.SelectedIndices[i] == this.editLayerIndex)
                                    {
                                        this.xEditRandomParamControl.updateGUIToData(ref *pParameter);
                                    }
                                }
                            }
                            if (this.yEditControl != null)
                            {
                                randomParameter* pParameter = &randomParameterArray[this.yEditControl.DataIndex - randParamDataIndexOffset];
                                if (pParameter->parameterMode == (int)parameterModeType.randomParam)
                                {
                                    pParameter->value = customMath.randomFloat(pParameter->leftLimit, pParameter->rightLimit);
                                    if (this.layerList.SelectedIndices[i] == this.editLayerIndex)
                                    {
                                        this.yEditRandomParamControl.updateGUIToData(ref *pParameter);
                                    }
                                }
                               
                            }          
                        }
                    }
                    if (this.radioValue.Checked == false)
                    {
                        this.radioValue.Checked = true;
                    }
                    else
                    {
                        this.updateToRandomParameterEditMode();
                    }
                }
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.G)
            {
                this.randomizeRandomParameters();
                e.SuppressKeyPress = true;
            }
            //// Toggle mouse editing on space bar
            //if (e.KeyCode == Keys.Space)
            //{
            //    // Suppress key press
            //    e.SuppressKeyPress = true;
            //}            
        }

        private void mainPictureBox_MouseLeave(object sender, EventArgs e)
        {
            // If mouse editing, turn it off   
            if (this.mouseEditing == true)
            {
                this.mouseEditing = false;
                if (this.xEditControl != null)
                {
                    this.xEditControl.BackColor = Color.White;
                }
                if (this.yEditControl != null)
                {
                    this.yEditControl.BackColor = Color.White;
                }
                if (this.focusedUpDownControl != null)
                {
                    this.focusedUpDownControl.BackColor = Color.Aqua;
                }
            }
        }

        //
        // Texture list functions
        //

        private void textureList_DoubleClick(object sender, EventArgs e)
        {           
            // Do an texture rename on double click of texture list
            if(this.textureList.SelectedItems.Count == 1)
            {
                // Initialize texture index and name
                int textureIndex = this.textureList.SelectedIndex;          
                String textureName = "";

                // Keep showing a name form until a valid result
                while(true)
                {
                    // Pop up a dialog for renaming the texture

                    // Set texture name only if first time through
                    if(textureName == "") textureName = this.textureArray[textureIndex].name;
                    nameForm theNameForm = new nameForm();
                    theNameForm.Text = "Rename Texture";
                    theNameForm.nameGroup.Text = "Enter a new name for the texture";
                    theNameForm.nameTextBox.Text = textureName;
                    theNameForm.nameTextBox.Focus();
                    theNameForm.nameTextBox.SelectAll();
                    theNameForm.nameTextBox.Focus();
                    theNameForm.ShowDialog();
                    if(theNameForm.DialogResult == DialogResult.OK)
                    {
                        // User clicked ok

                        // Get the name and close the form
                        textureName = theNameForm.nameTextBox.Text;
                        theNameForm.Close();

                        // Check if the name is valid
                        // NOTE: Not allowing zero length strings and names already in use
                        if(textureName.Length != 0)
                        {
                            bool nameInUse = false;
                            for(int i=0; i<this.textureCount; i++)
                            {
                                if(i != textureIndex && textureName == this.textureArray[i].name)
                                {
                                    nameInUse = true;
                                    break;
                                }
                            }
                            
                            // Reassign name if not in use and pop up message if in use
                            if(nameInUse == false)
                            {
                                this.textureArray[textureIndex].name = textureName;
                                this.updateGUIOnly = true;
                                this.textureList.Items[textureIndex] = textureName;
                                this.updateGUIOnly = false;
                                break;
                            }
                            else
                            {
                                // Prompt user that texture name is in use
                                MessageBox.Show(null, "'" + textureName + "' is already in use. Try again!", "Texture Name in use", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            // Prompt user that texture name is empty
                            MessageBox.Show(null, "Empty texture name.  Try again!", "Empty Texture Name", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // User cancelled
                        break;
                    }
                }
            }
        }

        private void textureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the edit texture index
            this.editTextureIndex = this.textureList.SelectedIndex;

            // Update gui to change in edit texture
            if (this.editTextureIndex != -1)
            {
                // Update gui to the texture
                this.updateGUIOnly = true;
                this.spinSize.Value = (decimal)this.textureArray[this.editTextureIndex].size;
                this.spinClipDuration.Value = (decimal)this.textureArray[this.editTextureIndex].clipDuration;
                this.spinFramesPerSecond.Value = (decimal)this.textureArray[this.editTextureIndex].framesPerSecond;
                this.updateGUIOnly = false;

                // Populate the layer list box
                this.layerList.Items.Clear();
                for (int i = 0; i < this.textureArray[editTextureIndex].layerCount; i++)
                {
                    this.layerList.Items.Add(this.textureArray[editTextureIndex].layerArray[i].name);
                }

                // Select all the layers if any exists
                if (this.layerList.Items.Count > 0)
                {
                    // Invoke layer list select index change implicitly
                    for(int i=0; i<this.layerList.Items.Count; i++)
                    {
                        this.layerList.SelectedIndex = i;
                    }
                }
                else
                {
                    // Invoke layer list select index explicitly
                    this.layerList_SelectedIndexChanged(sender, e);
                }                
            }
            else
            {
                // No texture selected
                
                // Clear the layer list and the GUI if not already cleared
                if (this.layerList.Items.Count > 0)
                {
                    // Clear the layer list
                    this.layerList.Items.Clear();

                    // Invoke layer list select index explicitly
                    this.layerList_SelectedIndexChanged(sender, e);
                }                
            }
        }
       
        private void buttonAddTexture_Click(object sender, EventArgs e)
        {
            // Add a new texture after the selected texture index to both the texture array and texture list
            // and then select the new texture
            if (this.editCollectionIndex != -1 && this.textureList.SelectedItems.Count <= 1)
            {
                int addIndex = this.textureList.SelectedIndex + 1;

                // Keep showing a name form until valid result
                while (true)
                {
                    // Find an unused texture name to initialize a name texture dialog
                    int nameIndex = 1;
                    String textureName = "texture1";
                    bool uniqueNameFound = false;
                    while (uniqueNameFound == false)
                    {
                        uniqueNameFound = true;
                        for (int i = 0; i < this.textureCount; i++)
                        {
                            if (textureName == this.textureArray[i].name)
                            {
                                uniqueNameFound = false;
                                break;
                            }
                        }
                        if (uniqueNameFound == false)
                        {
                            nameIndex += 1;
                            textureName = "texture" + nameIndex.ToString();
                        }
                    }

                    // Pop up a dialog for naming the texture
                    nameForm theNameForm = new nameForm();
                    theNameForm.Text = "Enter Name for New Texture";
                    theNameForm.nameGroup.Text = "Enter a name for the new texture";
                    theNameForm.nameTextBox.Text = textureName;
                    theNameForm.nameTextBox.SelectAll();
                    theNameForm.nameTextBox.Focus();
                    theNameForm.ShowDialog();
                    if (theNameForm.DialogResult == DialogResult.OK)
                    {
                        // User clicked ok

                        // Get the name and close the form
                        textureName = theNameForm.nameTextBox.Text;
                        theNameForm.Close();

                        // Check if the name is valid
                        // NOTE: Not allowing zero length strings and names already in use
                        if (textureName.Length != 0)
                        {
                            bool nameInUse = false;
                            for (int i = 0; i < this.textureCount; i++)
                            {
                                if (textureName == this.textureArray[i].name)
                                {
                                    nameInUse = true;
                                    break;
                                }
                            }

                            // Add texture if name is not in use and pop up message if in use
                            if (nameInUse == false)
                            {
                                // Add a texture to the texture array 
                                this.textureCount += 1;

                                // Resize and shift indices to make room for new add index
                                Array.Resize(ref textureArray, this.textureCount);
                                for (int i = this.textureCount - 1; i > addIndex; i--)
                                {
                                    this.textureArray[i] = this.textureArray[i - 1];
                                }
                               
                                // Assign name and initialize to default with a single layer
                                this.textureArray[addIndex].name = textureName;

                                // Initialize the new texture to default with one layer                                                             
                                this.textureArray[addIndex].assignTextureToDefault();
                              
                                // Insert the new texture into the texture list and select it 
                                this.updateGUIOnly = true;
                                this.textureList.SelectedIndex = -1;
                                this.textureList.Items.Insert(addIndex, textureName);
                                this.updateGUIOnly = false;
                                this.textureList.SelectedIndex = addIndex;
                                break;
                            }
                            else
                            {
                                // Prompt user that texture name is in use
                                MessageBox.Show(null, "'" + textureName + "' is already in use. Try again!", "Texture Name in use", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            // Prompt user that texture name is empty
                            MessageBox.Show(null, "Empty Texture name.  Try again!", "Empty Texture Name", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // User cancelled
                        break;
                    }
                }
            }
        }

        private void buttonDeleteTexture_Click(object sender, EventArgs e)
        {
            // Remove selected textures from both the texture array and texture list
            // and select the next texture or the last texture if no next
            int selectedTextureCount = this.textureList.SelectedIndices.Count;
            if (selectedTextureCount > 0)
            {
                if (MessageBox.Show(null, "Are your sure you want to delete the selected texture(s)?", "Confirm Texture Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {     
                    // Remove textures from the texture array
                    for (int i = 0; i < selectedTextureCount; i++)
                    {
                        // Remove the texture from the array
                        int removeIndex = this.textureList.SelectedIndices[i] - i;
                        for (int j = removeIndex; j < this.textureCount - 1; j++)
                        {
                            this.textureArray[j] = this.textureArray[j + 1];
                        }

                        // Decrement the texture count
                        this.textureCount -= 1;

                        // Resize the texture array
                        Array.Resize(ref this.textureArray, this.textureCount);
                    }

                    // Update the texture list and select next or last index
                    int selectIndex = this.textureList.SelectedIndices[selectedTextureCount - 1] - selectedTextureCount + 1;
                    this.textureList.SelectedIndex = -1;
                    for (int i = (this.textureCount + selectedTextureCount - 1); i >= this.textureCount; i--)
                    {
                        this.textureList.Items.RemoveAt(i);
                    }
                    for (int i = 0; i < this.textureCount; i++)
                    {
                        this.textureList.Items[i] = this.textureArray[i].name;
                    }
                    if (selectIndex != this.textureCount)
                    {
                        this.textureList.SelectedIndex = selectIndex;
                    }
                    else
                    {
                        this.textureList.SelectedIndex = this.textureCount - 1;
                    }
                }               
            }         
        }

        private void buttonMoveTextureUp_Click(object sender, EventArgs e)
        {
            // Move selected textures up in both the texture array and texture list
            // and then reselect the moved textures
            if (this.textureList.SelectedIndex > 0)
            {
                // Store value for the new edit texture index
                int newEditTextureIndex = this.editTextureIndex - 1;

                // Copy the selected list
                int selectedCount = this.textureList.SelectedIndices.Count;
                int[] selectedIndices = new int[selectedCount];
                this.textureList.SelectedIndices.CopyTo(selectedIndices, 0);

                // Set to update gui only
                this.updateGUIOnly = true;

                // Loop thru selected textures and move each one up
                for (int i = 0; i < selectedCount; i++)
                {
                    // Assign move index
                    int moveIndex = selectedIndices[i];

                    // Move texture up in the array by swapping each selected texture with the texture above it
                    texture swapTexture = this.textureArray[moveIndex];
                    this.textureArray[moveIndex] = this.textureArray[moveIndex - 1];
                    this.textureArray[moveIndex - 1] = swapTexture;

                    // Update the textures names in the texture list
                    this.textureList.Items[moveIndex - 1] = this.textureArray[moveIndex - 1].name;
                    this.textureList.Items[moveIndex] = this.textureArray[moveIndex].name;
                }

                // Clear selections
                this.textureList.SelectedIndex = -1;

                // Reselect the moved textures
                // NOTE: Ensuring that edit texture index does not change
                for (int i = 0; i < selectedCount; i++)
                {
                    this.textureList.SetSelected(selectedIndices[i] - 1, true);
                }
                this.textureList.SetSelected(newEditTextureIndex, true);

                // Put back to false
                this.updateGUIOnly = false;
            }
        }

        private void buttonMoveTextureDown_Click(object sender, EventArgs e)
        {
            // Move selected textures down in both the texture array and texture list
            // and then reselect the moved textures            
            if (this.textureList.SelectedItems.Count > 0)
            {
                int lastSelectedTexture = this.textureList.SelectedIndices[this.textureList.SelectedIndices.Count - 1];
                if (lastSelectedTexture < this.textureList.Items.Count - 1)
                {
                    // Store value for the new edit texture index
                    int newEditTextureIndex = this.editTextureIndex + 1;

                    // Copy the selected list
                    int selectedCount = this.textureList.SelectedIndices.Count;
                    int[] selectedIndices = new int[selectedCount];
                    this.textureList.SelectedIndices.CopyTo(selectedIndices, 0);

                    // Set to update gui only
                    this.updateGUIOnly = true;

                    // Loop thru selected textures in reverse order and move each one down
                    for (int i = selectedCount - 1; i >= 0; i--)
                    {
                        // Assign move index
                        int moveIndex = selectedIndices[i];

                        // Move texture down in the array by swapping each selected texture with the texture below it
                        texture swapTexture = this.textureArray[moveIndex];
                        this.textureArray[moveIndex] = this.textureArray[moveIndex + 1];
                        this.textureArray[moveIndex + 1] = swapTexture;

                        // Update the texture names in the texture list
                        this.textureList.Items[moveIndex] = this.textureArray[moveIndex].name;
                        this.textureList.Items[moveIndex + 1] = this.textureArray[moveIndex + 1].name;
                    }

                    // Clear selections
                    this.textureList.SelectedIndex = -1;

                    // Reselect the moved textures
                    // NOTE: Ensuring that edit texture index does not change
                    for (int i = 0; i < selectedCount; i++)
                    {
                        this.textureList.SetSelected(selectedIndices[i] + 1, true);
                    }
                    this.textureList.SetSelected(newEditTextureIndex, true);

                    // Put back to false
                    this.updateGUIOnly = false;
                }
            }
        }

        private void buttonCloneTexture_Click(object sender, EventArgs e)
        {
            // Add a new texture that is a clone of the selected texture, place it after the
            // selected texture in both the texture array and texture list, and then select the new texture
            if(this.textureList.SelectedItems.Count == 1)
            {
                int addIndex = this.textureList.SelectedIndex + 1;
                String textureName = "";

                // Keep showing a name form until valid result
                while(true)
                {
                    // Find an unused texture name to initialize a name texture dialog
                    int nameIndex = 1;
                    String baseName = this.textureList.SelectedItem.ToString();
                    int clonePosition = baseName.IndexOf("-clone");
                    if(clonePosition != -1)
                    {
                        int.TryParse(baseName.Substring(clonePosition + 6), out nameIndex);
                        baseName = baseName.Substring(0, clonePosition);
                    }

                    // Set texture name only if first time through
                    if(textureName == "")
                    {
                        textureName = baseName + "-clone" + nameIndex.ToString();
                        bool uniqueNameFound = false;
                        while(uniqueNameFound == false)
                        {
                            uniqueNameFound = true;
                            for(int i=0; i<this.textureCount; i++)
                            {
                                if(textureName == this.textureArray[i].name)
                                {
                                    uniqueNameFound = false;
                                    break;
                                }                               
                            }
                            if(uniqueNameFound == false)
                            {
                                nameIndex += 1;
                            }
                            textureName = baseName + "-clone" + nameIndex.ToString();
                        }
                    }

                    // Pop up a dialog for naming the texture
                    nameForm theNameForm = new nameForm();
                    theNameForm.Text = "Enter Name for '" + this.textureList.SelectedItem + "' Clone";
                    theNameForm.nameGroup.Text = "Enter a name for the clone of '" + this.textureList.SelectedItem + "' texture";
                    theNameForm.nameTextBox.Text = textureName;
                    theNameForm.nameTextBox.SelectAll();
                    theNameForm.nameTextBox.Focus();
                    theNameForm.ShowDialog();
                    if(theNameForm.DialogResult == DialogResult.OK)
                    {
                        // User clicked ok

                        // Get the name and close the form
                        textureName = theNameForm.nameTextBox.Text;
                        theNameForm.Close();

                        // Check if the name is valid
                        // NOTE: Not allowing zero length strings and names already in use
                        if(textureName.Length != 0)
                        {
                            bool nameInUse = false;
                            for(int i=0; i<this.textureCount; i++)
                            {
                                if(textureName == this.textureArray[i].name)
                                {
                                    nameInUse = true;
                                    break;
                                }
                            }

                            // Clone texture if name not in use and pop up message if in use
                            if(nameInUse == false)
                            {
                                // Add a texture to the texture array 
                                this.textureCount += 1;
                                                                
                                // Resize and shift indices to make room for new add index
                                Array.Resize(ref textureArray, this.textureCount);
                                for (int i = this.textureCount - 1; i > addIndex; i--)
                                {
                                    this.textureArray[i] = this.textureArray[i - 1];
                                }
                               
                                // Assign name
                                this.textureArray[addIndex].name = textureName;

                                // Clone size
                                this.textureArray[addIndex].size = this.textureArray[addIndex-1].size;

                                // Clone clip duration, frames per second, and texture scaler
                                this.textureArray[addIndex].clipDuration = this.textureArray[addIndex - 1].clipDuration;
                                this.textureArray[addIndex].framesPerSecond = this.textureArray[addIndex - 1].framesPerSecond;
                                this.textureArray[addIndex].textureScaler = this.textureArray[addIndex - 1].textureScaler;

                                // Clone layer count and layers
                                this.textureArray[addIndex].layerCount = this.textureArray[addIndex-1].layerCount;
                                this.textureArray[addIndex].layerArray = new layer[this.textureArray[addIndex].layerCount];
                                for (int i = 0; i < this.textureArray[addIndex].layerCount; i++)
                                {
                                    this.textureArray[addIndex].layerArray[i] = this.textureArray[addIndex - 1].layerArray[i];
                                }
                                
                                // Insert the new texture into the texture list and select it 
                                this.updateGUIOnly = true;
                                this.textureList.SelectedIndex = -1;
                                this.textureList.Items.Insert(addIndex, textureName);
                                this.updateGUIOnly = false;
                                this.textureList.SelectedIndex = addIndex;
                                break;                          
                         }
                            else
                            {
                                // Prompt user that texture name is in use
                                MessageBox.Show(null, "'" + textureName + "' is already in use. Try again!", "Texture Name in use", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            // Prompt user that texture name is empty
                            MessageBox.Show(null, "Empty Texture name.  Try again!", "Empty Texture Name", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // User cancelled
                        break;
                    }
                
                }
            }
        }

        //
        // Layer list functions
        //

        private void layerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset the clip image count
            // NOTE: Forces regeneration of clip animation
            this.clipImageCount = 0;

            // Update the edit layer index
            if (this.setEditLayerIndex != -1)
            {               
                if (this.layerList.GetSelected(this.setEditLayerIndex) == true)
                {
                    this.editLayerIndex = this.setEditLayerIndex;                    
                }
                else
                {
                    this.editLayerIndex = this.layerList.SelectedIndex;
                }
                this.setEditLayerIndex = -1;
            }
            else
            {
                this.editLayerIndex = this.layerList.SelectedIndex;
            }

            // Update the edit layer label
            this.updateEditLayerLabel();
            
            // Update gui to change of edit layer
            if (this.editTextureIndex != -1 && this.editLayerIndex != -1)
            {
                // Update the layers in order
                
                // Update the gui to the edit layer
                this.updateGUIToLayer(ref this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex]);
            }
            else
            {
                // No edit layer index selected

                // Update the gui to default values
                layer defaultLayer = new layer();
                defaultLayer.initializeLayerToDefault();
                this.updateGUIToLayer(ref defaultLayer);
            }
            
            // Trigger a redraw of the texture's bitmap
            this.updateBitmap = true;
            this.mainPictureBox.Invalidate();
        }

        private void layerList_DoubleClick(object sender, EventArgs e)
        {
            // Do a layer rename on double click of layer list
            if (this.editTextureIndex != -1 && this.layerList.SelectedItems.Count == 1)
            {
                // Initialize layer index and name
                int layerIndex = this.layerList.SelectedIndex;
                String layerName = "";

                // Keep showing a name form until a valid result
                while (true)
                {
                    // Pop up a dialog for renaming the layer

                    // Set layer name only if first time through
                    if (layerName == "") layerName = this.textureArray[this.editTextureIndex].layerArray[layerIndex].name;
                    nameForm theNameForm = new nameForm();
                    theNameForm.Text = "Rename Layer";
                    theNameForm.nameGroup.Text = "Enter a new name for the layer";
                    theNameForm.nameTextBox.Text = layerName;
                    theNameForm.nameTextBox.Focus();
                    theNameForm.nameTextBox.SelectAll();
                    theNameForm.nameTextBox.Focus();
                    theNameForm.ShowDialog();
                    if (theNameForm.DialogResult == DialogResult.OK)
                    {
                        // User clicked ok

                        // Get the name and close the form
                        layerName = theNameForm.nameTextBox.Text;
                        theNameForm.Close();

                        // Check if the name is valid
                        // NOTE: Not allowing zero length strings and names already in use
                        if (layerName.Length != 0)
                        {
                            bool nameInUse = false;
                            for (int i = 0; i < this.textureArray[this.editTextureIndex].layerCount; i++)
                            {
                                if (i != layerIndex && layerName == this.textureArray[this.editTextureIndex].layerArray[i].name)
                                {
                                    nameInUse = true;
                                    break;
                                }
                            }

                            // Reassign name if not in use and pop up message if in use
                            if (nameInUse == false)
                            {
                                this.textureArray[this.editTextureIndex].layerArray[layerIndex].name = layerName;
                                this.updateGUIOnly = true;
                                this.setEditLayerIndex = -1;
                                this.layerList.Items[layerIndex] = layerName;
                                this.updateGUIOnly = false;
                                break;
                            }
                            else
                            {
                                // Prompt user that layer name is in use
                                MessageBox.Show(null, "'" + layerName + "' is already in use. Try again!", "Layer Name in use", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            // Prompt user that layer name is empty
                            MessageBox.Show(null, "Empty layer name.  Try again!", "Empty Layer Name", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // User cancelled
                        break;
                    }
                }
            }
        }        

        private void layerList_MouseDown(object sender, MouseEventArgs e)
        {
            // Set to make last clicked layer index the edit layer index
            this.setEditLayerIndex = this.layerList.IndexFromPoint(e.X, e.Y);
        }

        private void checkMultiEdit_CheckedChanged(object sender, EventArgs e)
        {
            // Update the edit layer label
            this.updateEditLayerLabel();
        }

        private void updateEditLayerLabel()
        {
            if (this.checkMultiEdit.Checked == true)
            {
                this.labelEditLayer.Text = "multi-edit layer:\n";
            }
            else
            {
                this.labelEditLayer.Text = "single edit layer:\n";
            }
            if (this.editLayerIndex != -1)
            {
                this.labelEditLayer.Text += "'" + this.layerList.Items[this.editLayerIndex] + "'";
            }
            else
            {
                this.labelEditLayer.Text += "no layer selected";
            }
        }
    
        private void buttonAddLayer_Click(object sender, EventArgs e)
        {
            // Add a new layer after the selected layer index to both the layer array and layer list
            // and then select the new layer
            if (this.editTextureIndex != -1)
            {
                // Compute the add index
                int addIndex;
                if (this.layerList.SelectedItems.Count > 0)
                {
                    addIndex = this.layerList.SelectedIndices[this.layerList.SelectedItems.Count - 1] + 1;
                }
                else
                {
                    addIndex = 0;
                }

                // Keep showing a name form until valid result
                while (true)
                {
                    // Find an unused layer name to initialize a name layer dialog
                    int nameIndex = 1;
                    String layerName = "layer1";
                    bool uniqueNameFound = false;
                    while (uniqueNameFound == false)
                    {
                        uniqueNameFound = true;
                        for (int i = 0; i < this.textureArray[this.editTextureIndex].layerCount; i++)
                        {
                            if (layerName == this.textureArray[this.editTextureIndex].layerArray[i].name)
                            {
                                uniqueNameFound = false;
                                break;
                            }
                        }
                        if (uniqueNameFound == false)
                        {
                            nameIndex += 1;
                            layerName = "layer" + nameIndex.ToString();
                        }
                    }

                    // Pop up a dialog for naming the layer
                    nameForm theNameForm = new nameForm();
                    theNameForm.Text = "Enter Name for New Layer";
                    theNameForm.nameGroup.Text = "Enter a name for the new layer";
                    theNameForm.nameTextBox.Text = layerName;
                    theNameForm.nameTextBox.SelectAll();
                    theNameForm.nameTextBox.Focus();
                    theNameForm.ShowDialog();
                    if (theNameForm.DialogResult == DialogResult.OK)
                    {
                        // User clicked ok

                        // Get the name and close the form
                        layerName = theNameForm.nameTextBox.Text;
                        theNameForm.Close();

                        // Check if the name is valid
                        // NOTE: Not allowing zero length strings and names already in use
                        if (layerName.Length != 0)
                        {
                            bool nameInUse = false;
                            for (int i = 0; i < this.textureArray[editTextureIndex].layerCount; i++)
                            {
                                if (layerName == this.textureArray[editTextureIndex].layerArray[i].name)
                                {
                                    nameInUse = true;
                                    break;
                                }
                            }

                            // Add layer if name is not in use and pop up message if in use
                            if (nameInUse == false)
                            {
                                // Add a layer to the layer array 
                                this.textureArray[this.editTextureIndex].layerCount += 1;
                                Array.Resize(ref this.textureArray[this.editTextureIndex].layerArray, this.textureArray[this.editTextureIndex].layerCount);
                                for (int i = this.textureArray[this.editTextureIndex].layerCount - 1; i > addIndex; i--)
                                {
                                    textureArray[editTextureIndex].layerArray[i] = textureArray[editTextureIndex].layerArray[i - 1];
                                }

                                // Initialize the new layer
                                textureArray[editTextureIndex].layerArray[addIndex].name = layerName;
                                textureArray[editTextureIndex].layerArray[addIndex].initializeLayerToDefault();
                                                                                                        
                                // Insert the new layer into the layer list and select it 
                                this.updateGUIOnly = true;
                                this.layerList.SelectedIndex = -1;
                                this.layerList.Items.Insert(addIndex, layerName);
                                this.updateGUIOnly = false;
                                this.layerList.SelectedIndex = addIndex;

                                // Break out 
                                break;
                            }
                            else
                            {
                                // Prompt user that layer name is in use
                                MessageBox.Show(null, "'" + layerName + "' is already in use. Try again!", "Layer Name in use", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            // Prompt user that layer name is empty
                            MessageBox.Show(null, "Empty layer name.  Try again!", "Empty Layer Name", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // User cancelled
                        break;
                    }
                }
            }
        }

        private void buttonDeleteLayer_Click(object sender, EventArgs e)
        {
            // Remove selected layers from both the layer array and layer list
            // and select the next layer or the last layer if no next
            int selectedLayerCount = this.layerList.SelectedIndices.Count;
            if(selectedLayerCount > 0)
            {
                if (MessageBox.Show(null, "Are your sure you want to delete the selected layer(s)?", "Confirm Layer Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Remove layers from the layer array                
                    for (int i = 0; i < selectedLayerCount; i++)
                    {
                        // Remove the layer from the array
                        int removeIndex = this.layerList.SelectedIndices[i] - i;
                        for (int j = removeIndex; j < this.textureArray[this.editTextureIndex].layerCount - 1; j++)
                        {
                            this.textureArray[this.editTextureIndex].layerArray[j] = this.textureArray[this.editTextureIndex].layerArray[j + 1];
                        }

                        // Decrement the layer count
                        this.textureArray[this.editTextureIndex].layerCount -= 1;

                        // Resize the layer array
                        Array.Resize(ref this.textureArray[this.editTextureIndex].layerArray, this.textureArray[this.editTextureIndex].layerCount);
                    }

                    // Update the layer list and select next or last index
                    int layerCount = this.textureArray[this.editTextureIndex].layerCount;
                    int selectIndex = this.layerList.SelectedIndices[selectedLayerCount - 1] - selectedLayerCount + 1;
                    this.layerList.SelectedIndex = -1;
                    for (int i = (layerCount + selectedLayerCount - 1); i >= layerCount; i--)
                    {
                        this.layerList.Items.RemoveAt(i);
                    }
                    for (int i = 0; i < layerCount; i++)
                    {
                        this.layerList.Items[i] = this.textureArray[this.editTextureIndex].layerArray[i].name;
                    }
                    if (selectIndex != layerCount)
                    {
                        this.layerList.SelectedIndex = selectIndex;
                    }
                    else
                    {
                        this.layerList.SelectedIndex = layerCount - 1;
                    }
                }
            }         
        }

        private void buttonMoveLayerUp_Click(object sender, EventArgs e)
        {
            // Move selected layers up in both the layer array and layer list
            // and then reselect the moved layers
            if (this.layerList.SelectedIndex > 0)
            {
                // Store value for the new edit layer index
                int newEditLayerIndex = this.editLayerIndex - 1;

                // Copy the selected list
                int selectedCount = this.layerList.SelectedIndices.Count;
                int[] selectedIndices = new int[selectedCount];
                this.layerList.SelectedIndices.CopyTo(selectedIndices, 0);

                // Set to update gui only
                this.updateGUIOnly = true;

                // Loop thru selected layers and move each one up
                for (int i = 0; i < selectedCount; i++)
                {
                    // Assign move index
                    int moveIndex = selectedIndices[i];

                    // Move layer up in the array by swapping each selected layer with the layer above it
                    layer swapLayer = this.textureArray[this.editTextureIndex].layerArray[moveIndex];
                    this.textureArray[this.editTextureIndex].layerArray[moveIndex] = this.textureArray[this.editTextureIndex].layerArray[moveIndex - 1];
                    this.textureArray[this.editTextureIndex].layerArray[moveIndex - 1] = swapLayer;

                    // Update the layer names in the layer list
                    this.layerList.Items[moveIndex - 1] = this.textureArray[this.editTextureIndex].layerArray[moveIndex - 1].name;
                    this.layerList.Items[moveIndex] = this.textureArray[this.editTextureIndex].layerArray[moveIndex].name;
                }

                // Clear selections
                this.layerList.SelectedIndex = -1;

                // Reselect the moved layers
                // NOTE: Ensuring that edit layer index does not change
                for (int i = 0; i < selectedCount; i++)
                {
                    this.setEditLayerIndex = newEditLayerIndex;
                    this.layerList.SetSelected(selectedIndices[i] - 1, true);
                }

                // Put back to false
                this.updateGUIOnly = false;
            }
        }

        private void buttonMoveLayerDown_Click(object sender, EventArgs e)
        {
            // Move selected layers down in both the layer array and layer list
            // and then reselect the moved layers            
            if (this.layerList.SelectedItems.Count > 0)
            {
                int lastSelectedLayer = this.layerList.SelectedIndices[this.layerList.SelectedIndices.Count - 1];
                if (lastSelectedLayer < this.layerList.Items.Count - 1)
                {
                    // Store value for the new edit layer index
                    int newEditLayerIndex = this.editLayerIndex + 1;

                    // Copy the selected list
                    int selectedCount = this.layerList.SelectedIndices.Count;
                    int[] selectedIndices = new int[selectedCount];
                    this.layerList.SelectedIndices.CopyTo(selectedIndices, 0);

                    // Set to update gui only
                    this.updateGUIOnly = true;

                    // Loop thru selected layers in reverse order and move each one down
                    for (int i = selectedCount - 1; i >= 0; i--)
                    {
                        // Assign move index
                        int moveIndex = selectedIndices[i];

                        // Move layer down in the array by swapping each selected layer with the layer below it
                        layer swapLayer = this.textureArray[this.editTextureIndex].layerArray[moveIndex];
                        this.textureArray[this.editTextureIndex].layerArray[moveIndex] = this.textureArray[this.editTextureIndex].layerArray[moveIndex + 1];
                        this.textureArray[this.editTextureIndex].layerArray[moveIndex + 1] = swapLayer;

                        // Update the layer names in the layer list
                        this.layerList.Items[moveIndex] = this.textureArray[this.editTextureIndex].layerArray[moveIndex].name;
                        this.layerList.Items[moveIndex + 1] = this.textureArray[this.editTextureIndex].layerArray[moveIndex + 1].name;
                    }

                    // Clear selections
                    this.layerList.SelectedIndex = -1;

                    // Reselect the moved layers
                    // NOTE: Ensuring that edit layer index does not change
                    for (int i = 0; i < selectedCount; i++)
                    {
                        this.setEditLayerIndex = newEditLayerIndex;
                        this.layerList.SetSelected(selectedIndices[i] + 1, true);
                    }

                    // Put back to false
                    this.updateGUIOnly = false;
                }
            }
        }

        private void buttonCloneLayer_Click(object sender, EventArgs e)
        {
            // Add a new layer that is a clone of the selected layer, place it after the
            // selected layer in both the layer array and layer list, and then select the new layer
            if (this.layerList.SelectedItems.Count == 1)
            {
                int addIndex = this.layerList.SelectedIndex + 1;
                String layerName = "";

                // Keep showing a name form until valid result
                while (true)
                {
                    // Find an unused layer name to initialize a name layer dialog
                    int nameIndex = 1;
                    String baseName = this.layerList.SelectedItem.ToString();
                    int clonePosition = baseName.IndexOf("-clone");
                    if (clonePosition != -1)
                    {
                        int.TryParse(baseName.Substring(clonePosition + 6), out nameIndex);
                        baseName = baseName.Substring(0, clonePosition);
                    }

                    // Set layer name only if first time through
                    if (layerName == "")
                    {
                        layerName = baseName + "-clone" + nameIndex.ToString();
                        bool uniqueNameFound = false;
                        while (uniqueNameFound == false)
                        {
                            uniqueNameFound = true;
                            for (int i = 0; i < this.textureArray[this.editTextureIndex].layerCount; i++)
                            {
                                if (layerName == this.textureArray[this.editTextureIndex].layerArray[i].name)
                                {
                                    uniqueNameFound = false;
                                    break;
                                }
                            }
                            if (uniqueNameFound == false)
                            {
                                nameIndex += 1;
                            }
                            layerName = baseName + "-clone" + nameIndex.ToString();
                        }
                    }

                    // Pop up a dialog for naming the layer
                    nameForm theNameForm = new nameForm();
                    theNameForm.Text = "Enter Name for '" + this.layerList.SelectedItem + "' Clone";
                    theNameForm.nameGroup.Text = "Enter a name for the clone of '" + this.layerList.SelectedItem + "' layer";
                    theNameForm.nameTextBox.Text = layerName;
                    theNameForm.nameTextBox.SelectAll();
                    theNameForm.nameTextBox.Focus();
                    theNameForm.ShowDialog();
                    if (theNameForm.DialogResult == DialogResult.OK)
                    {
                        // User clicked ok

                        // Get the name and close the form
                        layerName = theNameForm.nameTextBox.Text;
                        theNameForm.Close();

                        // Check if the name is valid
                        // NOTE: Not allowing zero length strings and names already in use
                        if (layerName.Length != 0)
                        {
                            bool nameInUse = false;
                            for (int i = 0; i < this.textureArray[this.editTextureIndex].layerCount; i++)
                            {
                                if (layerName == this.textureArray[this.editTextureIndex].layerArray[i].name)
                                {
                                    nameInUse = true;
                                    break;
                                }
                            }

                            // Clone layer if name not in use and pop up message if in use
                            if (nameInUse == false)
                            {
                                // Add a layer to the layer array 
                                this.textureArray[this.editTextureIndex].layerCount += 1;

                                // Resize and shift indices to make room for new add index
                                Array.Resize(ref this.textureArray[this.editTextureIndex].layerArray, this.textureArray[this.editTextureIndex].layerCount);
                                for (int i = this.textureArray[this.editTextureIndex].layerCount - 1; i > addIndex; i--)
                                {
                                    this.textureArray[this.editTextureIndex].layerArray[i] = this.textureArray[this.editTextureIndex].layerArray[i - 1];
                                }

                                // Clone layer
                                this.textureArray[this.editTextureIndex].layerArray[addIndex] = this.textureArray[this.editTextureIndex].layerArray[addIndex - 1];
                                this.textureArray[this.editTextureIndex].layerArray[addIndex].name = layerName;
                                
                                // Insert the new layer into the layer list and select it 
                                this.updateGUIOnly = true;
                                this.layerList.SelectedIndex = -1;
                                this.layerList.Items.Insert(addIndex, layerName);
                                this.updateGUIOnly = false;
                                this.layerList.SelectedIndex = addIndex;
                                break;
                            }
                            else
                            {
                                // Prompt user that layer name is in use
                                MessageBox.Show(null, "'" + layerName + "' is already in use. Try again!", "Layer Name in use", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            // Prompt user that layer name is empty
                            MessageBox.Show(null, "Empty Layer name.  Try again!", "Empty Layer Name", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // User cancelled
                        break;
                    }
                }
            }
        }

        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private void radioValue_CheckedChanged(object sender, EventArgs e)
        {         
            // Set to edit value of random parameter
            this.randomParameterEditMode = 0;
            this.updateToRandomParameterEditMode();

            // Set focus back to focused up down
            if (this.focusedUpDownControl != null) this.focusedUpDownControl.Focus();
        }

        private void radioLeftLimit_CheckedChanged(object sender, EventArgs e)
        {
            // Set to edit left or lower limit of random parameter
            if (this.checkEditRelative.Checked == true) this.checkEditRelative.Checked = false;
            this.randomParameterEditMode = 1;
            this.updateToRandomParameterEditMode();

            // Set focus back to focused up down
            if (this.focusedUpDownControl != null) this.focusedUpDownControl.Focus();
        }

        private void radioRightLimit_CheckedChanged(object sender, EventArgs e)
        {
            // Set to edit right or upper limit of random parameter
            if (this.checkEditRelative.Checked == true) this.checkEditRelative.Checked = false;
            this.randomParameterEditMode = 2;
            this.updateToRandomParameterEditMode();

            // Set focus back to focused up down
            if (this.focusedUpDownControl != null) this.focusedUpDownControl.Focus();
        }

        private void updateToRandomParameterEditMode()
        {
            // Update gui to change of edit layer
            if (this.editTextureIndex != -1 && this.editLayerIndex != -1)
            {
                // Update the layers in order

                // Update the gui to the edit layer
                this.updateGUIToLayer(ref this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex]);
            }
            else
            {
                // No edit layer index selected

                // Update the gui to default values
                layer defaultLayer = new layer();
                defaultLayer.initializeLayerToDefault();
                this.updateGUIToLayer(ref defaultLayer);
            }

            // Trigger a redraw of the texture's bitmap
            this.updateBitmap = true;
            this.mainPictureBox.Invalidate();
        }

        private void checkEditRelative_CheckedChanged(object sender, EventArgs e)
        {
            // Allow edit relative only for random value changes, not random limit changes
            if (this.checkEditRelative.Checked && this.radioValue.Checked == false)
            {
                this.checkEditRelative.Checked = false;
            }
        }

        unsafe private void buttonRandomizeAll_Click(object sender, EventArgs e)
        {
            // Randomize all random parameters within limits
            this.randomizeRandomParameters();

            // Set focus back to focused up down
            if (this.focusedUpDownControl != null) this.focusedUpDownControl.Focus();
        }

        unsafe private void randomizeRandomParameters()
        {
            if (this.editTextureIndex != -1 && this.editLayerIndex != -1)
            {
                // Loop thru selected layers and randomize the all the parameters between the left and right limits
                for (int i = 0; i < this.layerList.SelectedItems.Count; i++)
                {
                    fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameter)
                    {
                        if (this.checkApplyToAllParams.Checked)
                        {
                            // Randomize all random parameters
                            for (int j = 0; j < randomParameterCount; j++)
                            {
                                randomParameter* pParameter = &randomParameterArray[j];
                                if (pParameter->parameterMode == (int)parameterModeType.randomParam)
                                {
                                    pParameter->value = customMath.randomFloat(pParameter->leftLimit, pParameter->rightLimit);
                                }

                                if (this.focusedUpDownControl != null)
                                {
                                    pParameter = &randomParameterArray[this.focusedUpDownControl.DataIndex - randParamDataIndexOffset];                                   
                                    this.focusedRandomParamControl.updateGUIToData(ref *pParameter);
                                }
                                if (this.xEditControl != null)
                                {
                                    pParameter = &randomParameterArray[this.xEditControl.DataIndex - randParamDataIndexOffset];                                 
                                    this.xEditRandomParamControl.updateGUIToData(ref *pParameter);
                                }
                                if (this.yEditControl != null)
                                {
                                    pParameter = &randomParameterArray[this.yEditControl.DataIndex - randParamDataIndexOffset];
                                    this.yEditRandomParamControl.updateGUIToData(ref *pParameter);
                                }
                            }
                        }
                        else
                        {
                            // Randomize only selected parameters
                            if (this.layerList.SelectedIndices[i] == this.editLayerIndex)
                            {
                                if (this.focusedUpDownControl != null)
                                {                                    
                                    randomParameter* pParameter = &randomParameterArray[this.focusedUpDownControl.DataIndex - randParamDataIndexOffset];
                                    if (pParameter->parameterMode == (int)parameterModeType.randomParam)
                                    {
                                        pParameter->value = customMath.randomFloat(pParameter->leftLimit, pParameter->rightLimit);
                                    }
                                    this.focusedRandomParamControl.updateGUIToData(ref *pParameter);
                                }
                                if (this.xEditControl != null)
                                {
                                    randomParameter* pParameter = &randomParameterArray[this.xEditControl.DataIndex - randParamDataIndexOffset];
                                    if (pParameter->parameterMode == (int)parameterModeType.randomParam)
                                    {
                                        pParameter->value = customMath.randomFloat(pParameter->leftLimit, pParameter->rightLimit);
                                    }
                                    this.xEditRandomParamControl.updateGUIToData(ref *pParameter);
                                }
                                if (this.yEditControl != null)
                                {
                                    randomParameter* pParameter = &randomParameterArray[this.yEditControl.DataIndex - randParamDataIndexOffset];
                                    if (pParameter->parameterMode == (int)parameterModeType.randomParam)
                                    {
                                        pParameter->value = customMath.randomFloat(pParameter->leftLimit, pParameter->rightLimit);
                                    }
                                    this.yEditRandomParamControl.updateGUIToData(ref *pParameter);
                                }
                            }
                        }                       
                    }
                }
                if (this.radioValue.Checked == false)
                {
                    this.radioValue.Checked = true;
                }
                else
                {
                    this.updateToRandomParameterEditMode();
                }
            }
        }

        private void collectionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if user switching to edit a different collection 
            if (this.editCollectionIndex != -1 &&
                this.editCollectionIndex != this.collectionList.SelectedIndex)
            {
                // Save the current collection to file
                this.saveEditorCollectionToFile(this.editCollectionIndex);
            }

            // Assign edit collection index as the selected index
            this.editCollectionIndex = this.collectionList.SelectedIndex;

            // Clear the texture list
            this.textureList.SelectedIndex = -1;
            this.textureList.Items.Clear();
                                   
            // Load the first selected collection
            if (this.editCollectionIndex != -1)
            {
                // Load editor collection from file
                this.loadEditorCollectionFromFile(this.editCollectionIndex);
                
                // Populate the texture list box
                for (int i = 0; i < this.textureCount; i++)
                {
                    this.textureList.Items.Add(this.textureArray[i].name);
                }                  
            }

            // Select the first texture if any
            if (this.updateGUIOnly == false)
            {
                if (this.textureList.Items.Count > 0)
                {
                    this.textureList.SelectedIndex = 0;
                }
                else
                {
                    this.textureList.SelectedIndex = -1;
                }        
            }
           
        }

        private void checkViewAllLayers_CheckedChanged(object sender, EventArgs e)
        {
            // Trigger a redraw of the texture's bitmap
            if (this.updateGUIOnly == false)
            {
                this.updateBitmap = true;
                this.mainPictureBox.Invalidate();
            }
        }

        private void buttonAddCollection_Click(object sender, EventArgs e)
        {
            // Add a new collection after the selected collection index
            // and then select the new collection
            if (this.collectionList.SelectedItems.Count <= 1)
            {
                int addIndex = this.collectionList.SelectedIndex + 1;

                // Keep showing a name form until valid result
                while (true)
                {
                    // Find an unused collection name to initialize a name collection dialog
                    int nameIndex = 1;
                    String collectionName = "collection1";
                    bool uniqueNameFound = false;
                    while (uniqueNameFound == false)
                    {
                        uniqueNameFound = true;
                        for (int i = 0; i < this.collectionNames.Count(); i++)
                        {
                            if (collectionName == this.collectionNames[i])
                            {
                                uniqueNameFound = false;
                                break;
                            }
                        }
                        if (uniqueNameFound == false)
                        {
                            nameIndex += 1;
                            collectionName = "collection" + nameIndex.ToString();
                        }
                    }

                    // Pop up a dialog for naming the collection
                    nameForm theNameForm = new nameForm();
                    theNameForm.Text = "Enter Name for New Collection";
                    theNameForm.nameGroup.Text = "Enter a name for the new collection";
                    theNameForm.nameTextBox.Text = collectionName;
                    theNameForm.nameTextBox.SelectAll();
                    theNameForm.nameTextBox.Focus();
                    theNameForm.ShowDialog();
                    if (theNameForm.DialogResult == DialogResult.OK)
                    {
                        // User clicked ok

                        // Get the name and close the form
                        collectionName = theNameForm.nameTextBox.Text;
                        theNameForm.Close();

                        // Check if the name is valid
                        // NOTE: Not allowing zero length strings and names already in use
                        if (collectionName.Length != 0)
                        {
                            bool nameInUse = false;
                            for (int i = 0; i < this.collectionNames.Count(); i++)
                            {
                                if (collectionName == this.collectionNames[i])
                                {
                                    nameInUse = true;
                                    break;
                                }
                            }
                            
                            // Add collection if name is not in use and pop up message if in use
                            if (nameInUse == false)
                            {
                                // Add collection 

                                // Deselect the current selection
                                // NOTE: This will trigger a save to file of the current selection
                                this.collectionList.SelectedIndex = -1;                               
                                
                                // Add the name to the collection names
                  
                                // Resize and shift indices to make room for new add index and then add the name
                                Array.Resize(ref collectionNames, this.collectionNames.Count()+1);
                                for (int i = this.collectionNames.Count() - 1; i > addIndex; i--)
                                {
                                    this.collectionNames[i] = this.collectionNames[i - 1];
                                }
                                this.collectionNames[addIndex] = collectionName;

                                // Create an empty collection and save it to file
                                this.textureCount = 0;
                                this.saveEditorCollectionToFile(addIndex);

                                // Save the changed collections indices to each collection file
                                this.saveCollectionIndicesToFile();

                                // Insert the new collection into the collection list and select it 
                                this.updateGUIOnly = true;
                                this.collectionList.SelectedIndex = -1;
                                this.collectionList.Items.Insert(addIndex, collectionName);
                                this.updateGUIOnly = false;
                                this.collectionList.SelectedIndex = addIndex;
                                break;
                            }
                            else
                            {
                                // Prompt user that collection name is in use
                                MessageBox.Show(null, "'" + collectionName + "' is already in use. Try again!", "Collection Name in use", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            // Prompt user that collection name is empty
                            MessageBox.Show(null, "Empty Collection name.  Try again!", "Empty Collection Name", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // User cancelled
                        break;
                    }
                }
            }
        }

        private void buttonDeleteCollection_Click(object sender, EventArgs e)
        {          
            // Remove selected collections and select the next collection or the last collection if no next
            int selectedCollectionCount = this.collectionList.SelectedIndices.Count;
            if (selectedCollectionCount > 0)
            {
                if (MessageBox.Show(null, "Are your sure you want to delete the selected collection(s)?", "Confirm Collection Deletion", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Remove collections
                    for (int i = 0; i < selectedCollectionCount; i++)
                    {
                        // Assign the remove index
                        int removeIndex = this.collectionList.SelectedIndices[i] - i;

                        // Remove the collection file
                        if (File.Exists(collectionPath + collectionNames[removeIndex] + ".tcc"))
                        {
                            File.Delete(collectionPath + collectionNames[removeIndex] + ".tcc");
                        }

                        // Remove the collection from the array of collection names                    
                        for (int j = removeIndex; j < this.collectionNames.Count() - 1; j++)
                        {
                            this.collectionNames[j] = this.collectionNames[j + 1];
                        }

                        // Resize the collection names array
                        Array.Resize(ref this.collectionNames, this.collectionNames.Count() - 1);
                    }

                    // Save the changed collections indices to each collection file
                    this.saveCollectionIndicesToFile();

                    // Update the collection list and select next or last index
                    int selectIndex = this.collectionList.SelectedIndices[selectedCollectionCount - 1] - selectedCollectionCount + 1;
                    this.editCollectionIndex = -1;
                    this.collectionList.SelectedIndex = -1;
                    for (int i = (this.collectionNames.Count() + selectedCollectionCount - 1); i >= this.collectionNames.Count(); i--)
                    {
                        this.collectionList.Items.RemoveAt(i);
                    }
                    for (int i = 0; i < this.collectionNames.Count(); i++)
                    {
                        this.collectionList.Items[i] = this.collectionNames[i];
                    }
                    if (selectIndex != this.collectionNames.Count())
                    {
                        this.collectionList.SelectedIndex = selectIndex;
                    }
                    else
                    {
                        this.collectionList.SelectedIndex = this.collectionNames.Count() - 1;
                    }
                }
            }        
        }

        private void buttonMoveCollectionUp_Click(object sender, EventArgs e)
        {
            // Move selected collections up in the collection list
            // and then reselect the moved collections
            if (this.collectionList.SelectedIndex > 0)
            {
                // Copy the selected list
                int selectedCount = this.collectionList.SelectedIndices.Count;
                int[] selectedIndices = new int[selectedCount];
                this.collectionList.SelectedIndices.CopyTo(selectedIndices, 0);

                // Clear selections
                this.collectionList.SelectedIndex = -1;

                // Loop thru selected collections and move each one up
                for (int i = 0; i < selectedCount; i++)
                {
                    // Assign move index
                    int moveIndex = selectedIndices[i];

                    // Move collection up in the collection names array by swapping each selected collection with the collection above it
                    String swapName = this.collectionNames[moveIndex];
                    this.collectionNames[moveIndex] = this.collectionNames[moveIndex - 1];
                    this.collectionNames[moveIndex - 1] = swapName;

                    // Update the collection names in the collection list
                    this.collectionList.Items[moveIndex - 1] = this.collectionNames[moveIndex - 1];
                    this.collectionList.Items[moveIndex] = this.collectionNames[moveIndex];
                }

                // Save the changed collections indices to each collection file
                this.saveCollectionIndicesToFile();

                // Reselect the moved collections
                for (int i = 0; i < selectedCount; i++)
                {
                    this.collectionList.SetSelected(selectedIndices[i] - 1, true);
                }
            }
        }

        private void buttonMoveCollectionDown_Click(object sender, EventArgs e)
        {
            // Move selected collections down in the collection list
            // and then reselect the moved collections            
            if (this.collectionList.SelectedItems.Count > 0)
            {
                int lastSelectedCollection = this.collectionList.SelectedIndices[this.collectionList.SelectedIndices.Count - 1];
                if (lastSelectedCollection < this.collectionList.Items.Count - 1)
                {
                    // Copy the selected list
                    int selectedCount = this.collectionList.SelectedIndices.Count;
                    int[] selectedIndices = new int[selectedCount];
                    this.collectionList.SelectedIndices.CopyTo(selectedIndices, 0);

                    // Clear selections
                    this.collectionList.SelectedIndex = -1;

                    // Loop thru selected collections in reverse order and move each one down
                    for (int i = selectedCount - 1; i >= 0; i--)
                    {
                        // Assign move index
                        int moveIndex = selectedIndices[i];

                        // Move collection down in the collection names array by swapping each selected collection with the collection below it
                        String swapName = this.collectionNames[moveIndex];
                        this.collectionNames[moveIndex] = this.collectionNames[moveIndex + 1];
                        this.collectionNames[moveIndex + 1] = swapName;

                        // Update the collection names in the collection list
                        this.collectionList.Items[moveIndex] = this.collectionNames[moveIndex];
                        this.collectionList.Items[moveIndex + 1] = this.collectionNames[moveIndex + 1];
                    }

                    // Save the changed collections indices to each collection file
                    this.saveCollectionIndicesToFile();
         
                    // Reselect the moved collections
                    for (int i = 0; i < selectedCount; i++)
                    {
                        this.collectionList.SetSelected(selectedIndices[i] + 1, true);
                    }
                }
            }
        }

        private void buttonCloneCollection_Click(object sender, EventArgs e)
        {
            // Add a new collection that is a clone of the selected collection, place it after the
            // selected collection in the collection list, and then select the new collection
            if (this.collectionList.SelectedItems.Count == 1)
            {
                int addIndex = this.collectionList.SelectedIndex + 1;
                String collectionName = "";

                // Keep showing a name form until valid result
                while (true)
                {
                    // Find an unused collection name to initialize a name collection dialog
                    int nameIndex = 1;
                    String baseName = this.collectionList.SelectedItem.ToString();
                    int clonePosition = baseName.IndexOf("-clone");
                    if (clonePosition != -1)
                    {
                        int.TryParse(baseName.Substring(clonePosition + 6), out nameIndex);
                        baseName = baseName.Substring(0, clonePosition);
                    }

                    // Set collection name only if first time through
                    if (collectionName == "")
                    {
                        collectionName = baseName + "-clone" + nameIndex.ToString();
                        bool uniqueNameFound = false;
                        while (uniqueNameFound == false)
                        {
                            uniqueNameFound = true;
                            for (int i = 0; i < this.collectionNames.Count(); i++)
                            {
                                if (collectionName == this.collectionNames[i])
                                {
                                    uniqueNameFound = false;
                                    break;
                                }
                            }
                            if (uniqueNameFound == false)
                            {
                                nameIndex += 1;
                            }
                            collectionName = baseName + "-clone" + nameIndex.ToString();
                        }
                    }

                    // Pop up a dialog for naming the collection
                    nameForm theNameForm = new nameForm();
                    theNameForm.Text = "Enter Name for '" + this.collectionList.SelectedItem + "' Clone";
                    theNameForm.nameGroup.Text = "Enter a name for the clone of '" + this.collectionList.SelectedItem + "' collection";
                    theNameForm.nameTextBox.Text = collectionName;
                    theNameForm.nameTextBox.SelectAll();
                    theNameForm.nameTextBox.Focus();
                    theNameForm.ShowDialog();
                    if (theNameForm.DialogResult == DialogResult.OK)
                    {
                        // User clicked ok

                        // Get the name and close the form
                        collectionName = theNameForm.nameTextBox.Text;
                        theNameForm.Close();

                        // Check if the name is valid
                        // NOTE: Not allowing zero length strings and names already in use
                        if (collectionName.Length != 0)
                        {
                            bool nameInUse = false;
                            for (int i = 0; i < this.collectionNames.Count(); i++)
                            {
                                if (collectionName == this.collectionNames[i])
                                {
                                    nameInUse = true;
                                    break;
                                }
                            }

                            // Clone collection if name not in use and pop up message if in use
                            if (nameInUse == false)
                            {
                                // Clone collection 

                                // Clone the collection file
                                File.Copy(collectionPath + collectionNames[this.collectionList.SelectedIndex] + ".tcc",
                                    collectionPath + collectionName + ".tcc");

                                // Deselect the current selection
                                // NOTE: This will trigger a save to file of the current selection
                                this.collectionList.SelectedIndex = -1;

                                // Add the name to the collection names

                                // Resize and shift indices to make room for new add index and then add the name
                                Array.Resize(ref collectionNames, this.collectionNames.Count() + 1);
                                for (int i = this.collectionNames.Count() - 1; i > addIndex; i--)
                                {
                                    this.collectionNames[i] = this.collectionNames[i - 1];
                                }
                                this.collectionNames[addIndex] = collectionName;

                                // Save the changed collections indices to each collection file
                                this.saveCollectionIndicesToFile();

                                // Insert the new collection into the collection list and select it 
                                this.updateGUIOnly = true;
                                this.collectionList.SelectedIndex = -1;
                                this.collectionList.Items.Insert(addIndex, collectionName);
                                this.updateGUIOnly = false;
                                this.collectionList.SelectedIndex = addIndex;
                                break;
                            }
                            else
                            {
                                // Prompt user that collection name is in use
                                MessageBox.Show(null, "'" + collectionName + "' is already in use. Try again!", "Collection Name in use", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            // Prompt user that collection name is empty
                            MessageBox.Show(null, "Empty Collection name.  Try again!", "Empty Collection Name", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // User cancelled
                        break;
                    }

                }
            }
        }

        private void checkShowCollider_CheckedChanged(object sender, EventArgs e)
        {
            // Trigger a redraw of the texture's bitmap
            if (this.updateGUIOnly == false)
            {
                this.updateBitmap = true;
                this.mainPictureBox.Invalidate();           
                
                // Set focus back to focused up down
                if (this.focusedUpDownControl != null) this.focusedUpDownControl.Focus();
            }
        }

        unsafe private void buttonSetLeftLimitToValue_Click(object sender, EventArgs e)
        {
            // Set all selected layer's left limits to value
            this.transferLevelParameterLeftRightAndValue(0);
        }

        unsafe private void buttonSetRightLimitToValue_Click(object sender, EventArgs e)
        {
            // Set all selected layer's left limits to value
            this.transferLevelParameterLeftRightAndValue(1);
        }

        private void buttonSetLRToMinMax_Click(object sender, EventArgs e)
        {
            // Set all selected layer's left and right limits to control's minimum and maximum values
            this.transferLevelParameterLeftRightAndValue(2);
        }

        unsafe private void buttonSetValueToLeftLimit_Click(object sender, EventArgs e)
        {
            // Set all selected layer's values to left limit
            this.transferLevelParameterLeftRightAndValue(3);
        }

        private void buttonSetValueToRightLimit_Click(object sender, EventArgs e)
        {
            // Set all selected layer's values to right limit
            this.transferLevelParameterLeftRightAndValue(4);
        }        

        unsafe void transferLevelParameterLeftRightAndValue(int transferMode)
        {
            // Loop thru selected layers and set all layer's right limits to value
            if (this.editTextureIndex != -1 && this.editLayerIndex != -1)
            {
                // Check for multi-edit
                if (this.checkMultiEdit.Checked == true)
                {
                    for (int i = 0; i < this.layerList.SelectedItems.Count; i++)
                    {
                        fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameter)
                        {                          
                            for (int j = 0; j < randomParameterCount; j++)
                            {
                                // Determine whether to change parameter
                                // NOTE: Only change parameter if apply to all or its control is selected
                                bool changeParameter = this.checkApplyToAllParams.Checked;
                                if (changeParameter == false)
                                {
                                    // Check for selected controls
                                    if (this.focusedUpDownControl != null)
                                    {
                                        if (this.focusedUpDownControl.DataIndex - randParamDataIndexOffset == j) changeParameter = true;
                                    }
                                    if (this.xEditControl != null)
                                    {
                                        if (this.xEditControl.DataIndex - randParamDataIndexOffset == j) changeParameter = true;
                                    }
                                    if (this.yEditControl != null)
                                    {
                                        if (this.yEditControl.DataIndex - randParamDataIndexOffset == j) changeParameter = true;
                                    }
                                }

                                // Change the parameter
                                if (changeParameter)
                                {
                                    switch (transferMode)
                                    {
                                        case 0: // Left limit gets value
                                            randomParameterArray[j].leftLimit = randomParameterArray[j].value;
                                            break;
                                        case 1: // Right limit gets value
                                            randomParameterArray[j].rightLimit = randomParameterArray[j].value;
                                            break;
                                        default:
                                        case 2: // Left and right limits get control's mininum and maximum values
                                            if (this.randomParameterControlArray[j].Maximum == 362)
                                            {
                                                randomParameterArray[j].leftLimit = (float)(Math.PI / 180.0) * (float)this.randomParameterControlArray[j].Minimum;
                                                randomParameterArray[j].rightLimit = (float)(Math.PI / 180.0) * (float)this.randomParameterControlArray[j].Maximum;
                                            }
                                            else
                                            {
                                                randomParameterArray[j].leftLimit = (float)this.randomParameterControlArray[j].Minimum;
                                                randomParameterArray[j].rightLimit = (float)this.randomParameterControlArray[j].Maximum;
                                            }
                                            break;
                                        case 3: // Value gets left limit
                                            randomParameterArray[j].value = randomParameterArray[j].leftLimit;
                                            break;
                                        case 4: // Value gets right limit
                                            randomParameterArray[j].value = randomParameterArray[j].rightLimit;
                                            break;
                                    }
                                }    
                            }                          
                        }
                    }
                }
                else
                {
                    // Single layer edit
                    fixed (randomParameter* randomParameterArray = &this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex].diameter)
                    {
                        for (int j = 0; j < randomParameterCount; j++)
                        {
                            // Determine whether to change parameter
                            // NOTE: Only change parameter if apply to all or its control is selected
                            bool changeParameter = this.checkApplyToAllParams.Checked;
                            if (changeParameter == false)
                            {
                                // Check for selected controls
                                if (this.focusedUpDownControl != null)
                                {
                                    if (this.focusedUpDownControl.DataIndex - randParamDataIndexOffset == j) changeParameter = true;
                                }
                                if (this.xEditControl != null)
                                {
                                    if (this.xEditControl.DataIndex - randParamDataIndexOffset == j) changeParameter = true;
                                }
                                if (this.yEditControl != null)
                                {
                                    if (this.yEditControl.DataIndex - randParamDataIndexOffset == j) changeParameter = true;
                                }
                            }

                            // Change the parameter
                            if (changeParameter)
                            {
                                switch (transferMode)
                                {
                                    case 0: // Left limit gets value
                                        randomParameterArray[j].leftLimit = randomParameterArray[j].value;
                                        break;
                                    case 1: // Right limit gets value
                                        randomParameterArray[j].rightLimit = randomParameterArray[j].value;
                                        break;
                                    default:
                                    case 2: // Left and right limits get control's mininum and maximum values
                                        if (this.randomParameterControlArray[j].Maximum == 362)
                                        {
                                            randomParameterArray[j].leftLimit = (float)(Math.PI / 180.0) * (float)this.randomParameterControlArray[j].Minimum;
                                            randomParameterArray[j].rightLimit = (float)(Math.PI / 180.0) * (float)this.randomParameterControlArray[j].Maximum;
                                        }
                                        else
                                        {
                                            randomParameterArray[j].leftLimit = (float)this.randomParameterControlArray[j].Minimum;
                                            randomParameterArray[j].rightLimit = (float)this.randomParameterControlArray[j].Maximum;
                                        }
                                        break;
                                    case 3: // Value gets left limit
                                        randomParameterArray[j].value = randomParameterArray[j].leftLimit;
                                        break;
                                    case 4: // Value gets right limit
                                        randomParameterArray[j].value = randomParameterArray[j].rightLimit;
                                        break;
                                }
                            }                          
                        }
                    }
                }

                // Update gui
                this.updateToRandomParameterEditMode();
            }
            
            // Set focus back to focused up down
            if (this.focusedUpDownControl != null) this.focusedUpDownControl.Focus();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            if (this.editTextureIndex != -1)
            {
                if (this.textureArray[this.editTextureIndex].clipDuration > 0.0f)
                {
                    if (this.buttonPlay.Text == "play")
                    {
                        // Begin "play" state

                        // Display "pause"
                        this.buttonPlay.Text = "pause";

                        if (this.animating == false)
                        {
                            // Start animating

                            // Disable list boxes
                            this.collectionList.Enabled = false;
                            this.textureList.Enabled = false;
                            this.layerList.Enabled = false;

                            // Enable animation timer with a slow initial trigger time 
                            this.animating = true;
                            this.animationTimer.Interval = 100;
                            this.animationTimer.Enabled = true;
                        }
                    }
                    else
                    {
                        // Begin "pause" state

                        // Display "play"
                        this.buttonPlay.Text = "play";
                    }
                }              
            }

            // Set focus back to focused up down
            if (this.focusedUpDownControl != null) this.focusedUpDownControl.Focus();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (this.animating == true)
            {
                // Disable animation
                this.animationTimer.Enabled = false;
                this.animating = false;

                // Set generating clip to false
                if (this.generatingClip == true)
                {
                    // Set to false
                    this.generatingClip = false;

                    // Reset the clip image count
                    // NOTE: Forces regeneration of clip animation
                    this.clipImageCount = 0;
                }                            

                // Reset clip time
                this.clipTime = -1.0f;

                // Display "play"
                this.buttonPlay.Text = "play";

                // Set displayed clip time to zero
                this.labelClipTime.Text = "0.00";

                // Reenable list boxes
                this.collectionList.Enabled = true;
                this.textureList.Enabled = true;
                this.layerList.Enabled = true;

                // Trigger a draw of the texture's bitmap
                this.updateBitmap = true;
                this.mainPictureBox.Invalidate();
            }

            // Set focus back to focused up down
            if (this.focusedUpDownControl != null) this.focusedUpDownControl.Focus();
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            // Return if not animating or busy drawing or no edit texture is selected
            if(this.animating == false || this.busyDrawing == true || this.editTextureIndex == -1) return;

            // Check for end of animation
            if (this.clipTime > this.textureArray[this.editTextureIndex].clipDuration)
            {
                // Stop the animation and return
                this.buttonStop.PerformClick();
                return;              
            }

            if (this.clipImageCount == 0 && this.checkGenerateClip.Checked == true)
            {
                // Turn off the animation timer
                this.animationTimer.Enabled = false;

                // Create an array of bitmap images
                this.clipImageCount = (int) (this.textureArray[this.editTextureIndex].framesPerSecond * this.textureArray[this.editTextureIndex].clipDuration);
                this.clip = new Bitmap[this.clipImageCount];

                // Initialize clip time to zero
                this.clipTime = 0.0f;

                // Initialize generated clip index to zero
                this.generatedClipIndex = 0;

                // Set paint to trigger generate clip
                this.generatingClip = true;
            }
            else
            {
                // Set animation tick interval in milliseconds based on frames per second
                this.animationTimer.Interval = (int)(1000.0f / this.textureArray[this.editTextureIndex].framesPerSecond);

                // Initialize/update clock
                if (this.clipTime < 0.0)
                {
                    // Initialize clock
                    QueryPerformanceFrequency(out this.clockFrequency);
                    QueryPerformanceCounter(out this.clockTime);
                    this.currentTime = (float)this.clockTime / (float)this.clockFrequency;
                    this.clipTime = 0.0f;
                }
                else
                {
                    // Update clock
                    QueryPerformanceCounter(out this.clockTime);
                    this.currentTime = (float)this.clockTime / (float)this.clockFrequency;

                    // Check for animation in "play" state
                    if (this.buttonPlay.Text == "pause")
                    {
                        // Animation is in "play" state

                        // Update clip time
                        this.clipTime += this.currentTime - this.oldTime;

                        // Check for loop
                        if (this.checkLoop.Checked == true && this.clipTime > this.textureArray[this.editTextureIndex].clipDuration)
                        {
                            // Loop the animation
                            this.clipTime -= this.textureArray[this.editTextureIndex].clipDuration;
                        }
                    }
                }

                // Update old time
                this.oldTime = this.currentTime;
            }

            // Trigger a draw of the texture's bitmap
            this.updateBitmap = true;
            this.mainPictureBox.Invalidate();

            // Update displayed clip time
            this.labelClipTime.Text = String.Format("{0:0.00}", this.clipTime);
        }

        private void generateNextClipBitmap()
        {
            // Assign the current texture's bitmap to the clip index
            this.clip[this.generatedClipIndex] = new Bitmap(this.textureArray[this.editTextureIndex].bitmap);

            // Determine if end of clip
            bool endOfClip;
            if(this.generatedClipIndex < this.clipImageCount-1)
            {
                endOfClip = false;
            }
            else
            {
                endOfClip = true;
            }
            if (this.clip[generatedClipIndex] == null)
            {
                Debug.Print("hey hey");
            }

            // Increment the generated clip index
            this.generatedClipIndex += 1;

            // Check for end of clip generation
            if (endOfClip == false)
            {
                 // Increment the clip time
                this.clipTime += 1.0f / this.textureArray[this.editTextureIndex].framesPerSecond;

                // Update displayed clip time
                this.labelClipTime.Text = String.Format("{0:0.00}", this.clipTime);

                // Trigger a draw of texture's bitmap
                this.updateBitmap = true;
                this.mainPictureBox.Invalidate();
            }
            else
            {
                // Set generating clip to false
                this.generatingClip = false;

                // Reset the clip time to -1.0
                this.clipTime = -1.0f;

                // Restart the animation timer
                this.animationTimer.Enabled = true;
            }          
        }

        private void collectionList_DoubleClick(object sender, EventArgs e)
        {
            // Do a collection rename on double click of collection list
            if (this.collectionList.SelectedItems.Count == 1)
            {
                // Initialize collection index and name
                int collectionIndex = this.collectionList.SelectedIndex;
                String collectionName = "";

                // Keep showing a name form until a valid result
                while (true)
                {
                    // Pop up a dialog for renaming the collection

                    // Set collection name only if first time through
                    if (collectionName == "") collectionName = this.collectionNames[collectionIndex];
                    nameForm theNameForm = new nameForm();
                    theNameForm.Text = "Rename Collection";
                    theNameForm.nameGroup.Text = "Enter a new name for the collection";
                    theNameForm.nameTextBox.Text = collectionName;
                    theNameForm.nameTextBox.Focus();
                    theNameForm.nameTextBox.SelectAll();
                    theNameForm.nameTextBox.Focus();
                    theNameForm.ShowDialog();
                    if (theNameForm.DialogResult == DialogResult.OK)
                    {
                        // User clicked ok

                        // Get the name and close the form
                        collectionName = theNameForm.nameTextBox.Text;
                        theNameForm.Close();

                        // Check if the name is valid
                        // NOTE: Not allowing zero length strings and names already in use
                        if (collectionName.Length != 0)
                        {
                            bool nameInUse = false;
                            for (int i = 0; i < this.collectionNames.Count(); i++)
                            {
                                if (i != collectionIndex && collectionName == this.collectionNames[i])
                                {
                                    nameInUse = true;
                                    break;
                                }
                            }

                            // Reassign name if not in use and pop up message if in use
                            if (nameInUse == false)
                            {
                                // Need to deal with case sensitivity
                                // Note: the file.move command below is case insensitive and will fail if the user just changes the case of a character in the new name

                                // Case insensitive check
                                bool equals = collectionName.Equals(this.collectionNames[collectionIndex], StringComparison.OrdinalIgnoreCase);

                                if (equals)
                                {
                                    // User either kept the name the same or changed the case of character(s)
                                    // In this case, need to rename to a unique name and then rename back to new name

                                    // Rename to temporary name
                                    File.Move(collectionPath + collectionNames[collectionIndex] + ".tcc", collectionPath + collectionName + ".temp");
                                    
                                    // Rename back to original with proper case
                                    File.Move(collectionPath + collectionNames[collectionIndex] + ".temp", collectionPath + collectionName + ".tcc");
                                    
                                    this.collectionNames[collectionIndex] = collectionName;
                                    this.collectionList.Items[collectionIndex] = collectionName;
                                    break;
                                    
                                }
                                else
                                {
                                    // Rename the collection's file name, array name, list item name, and then reselect it
                                    File.Move(collectionPath + collectionNames[collectionIndex] + ".tcc", collectionPath + collectionName + ".tcc");
                                    this.collectionNames[collectionIndex] = collectionName;
                                    this.collectionList.Items[collectionIndex] = collectionName;
                                    break;

                                }

                            }
                            else
                            {
                                // Prompt user that collection name is in use
                                MessageBox.Show(null, "'" + collectionName + "' is already in use. Try again!", "Collection Name in use", MessageBoxButtons.OK);
                            }
                        }
                        else
                        {
                            // Prompt user that collection name is empty
                            MessageBox.Show(null, "Empty collection name.  Try again!", "Empty Collection Name", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // User cancelled
                        break;
                    }
                }
            }
        }

        private void checkGenerateClip_CheckedChanged(object sender, EventArgs e)
        {
            // Set focus back to focused up down
            if (this.focusedUpDownControl != null) this.focusedUpDownControl.Focus();
        }

        private void buttonExportAllTextureData_MouseDown(object sender, MouseEventArgs e)
        {
            //if (this.editTextureIndex != -1)
            //{
            //    this.textureArray[this.editTextureIndex].bitmap.Save(Application.StartupPath + "\\fun.png", System.Drawing.Imaging.ImageFormat.Png);
            //}

            // jea tbd - disabling the form for now during image creation
            // maybe something else later - thread?
            this.Enabled = false;

            // Store the active edit layer, texture, and collection
            int activeEditLayer = this.editLayerIndex;
            int activeEditTexture = this.editTextureIndex;
            int activeEditCollection = this.editCollectionIndex;

            // Deselect any selected collection
            String textureDataPath = Application.StartupPath + "\\texture data\\";
            this.collectionList.SelectedIndex = -1;

            // Delete any existing texture data directory and all its sub-directories
            if (Directory.Exists(textureDataPath) == true)
            {
                Directory.Delete(textureDataPath, true);
            }

            // Create a new texture data directory
            Directory.CreateDirectory(textureDataPath);

            // Create a file of collection names written in proper order
            // for populating the level editor's collection list box
            File.WriteAllLines(textureDataPath + "collections.txt", collectionNames);

            // Loop thru all collections - load them, compute each collection's textures,
            // and then store both the textures and texture data in collection directories
            // for use by both the level editor and iOS 
            for (int i = 0; i < this.collectionNames.Count(); i++)
            {
                // Load collection from file                   
                this.loadEditorCollectionFromFile(i);

                // Create the collection directory 
                String collectionDirPath = textureDataPath + this.collectionNames[i] + "\\";
                Directory.CreateDirectory(collectionDirPath);

                // Loop thru collection's textures - populate a string array with the texture names,
                // create and save all texture bitmap, and texture data for use by both the level editor and iOS 
                String[] textureNames = new String[this.textureCount];
                for (int j = 0; j < this.textureCount; j++)
                {
                    // Populate the texture name array with the texture name
                    // NOTE: Writing names below to a text file
                    textureNames[j] = this.textureArray[j].name;

                    // Draw the layers of the texture to bitmap
                    bool addLayer = false;
                    for (int k = 0; k < this.textureArray[j].layerCount; k++)
                    {
                        this.dGlowCircle.drawDoubleGlowCircle(ref this.textureArray[j], ref this.textureArray[j].layerArray[k], addLayer);
                        addLayer = true;
                    }

                    // Write the bitmap to file
                    this.textureArray[j].bitmap.Save(collectionDirPath + this.textureArray[j].name + ".png", System.Drawing.Imaging.ImageFormat.Png);

                    // Write the texture data to file
                    this.saveTextureDataToFile(j, collectionDirPath);
                }

                // Create a file of texture names written in proper order
                // for populating the level editor's texture list box
                File.WriteAllLines(collectionDirPath + "textures.txt", textureNames);
            }

            // Try to get an application handle to "Space Shooter Level Editor"
            // and send a key to reload textures if process is running
            IntPtr handleToLevelEditor = FindWindow(null, "Space Shooter Level Editor");
            if (handleToLevelEditor != IntPtr.Zero)
            {
                // Set space shooter level editor to foreground, wait half second, send ctrl-r
                // and then reactivate texture create window
                SetForegroundWindow(handleToLevelEditor);
                System.Threading.Thread.Sleep(500);
                SendKeys.SendWait("^r");
            }

            // Reselect the edit collection, texture, and layer
            this.collectionList.SelectedIndex = activeEditCollection;
            this.textureList.SelectedIndex = -1;
            this.textureList.SelectedIndex = activeEditTexture;
            this.layerList.SelectedIndex = -1;
            this.layerList.SelectedIndex = activeEditLayer;

            // Reenable and activate the main form
            this.Enabled = true;
            this.Activate();

            // Pressing right button will also run the .bat file on the desktop - copies textures to IOS side
            // NOTE: only valid on Mike's system
            if (e.Button == MouseButtons.Right)
            {
                System.Diagnostics.Process.Start("c:\\documents and settings\\mike\\desktop\\CopyTextures.bat");
            }

            // Set focus back to focused up down
            if (this.focusedUpDownControl != null) this.focusedUpDownControl.Focus();
        }

        private void buttonCopyLayer_Click(object sender, EventArgs e)
        {
            if (this.layerList.SelectedItems.Count == 1)
            {
                // Copy layer
                this.copyLayer = this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndex];

                // Enable paste button now that we have copy data
                buttonPasteLayer.Enabled = true;
            }
            else
            {
                // Prompt user that only 1 layer can be copied at a time
                MessageBox.Show(null, "Only 1 layer can be copied at a time, select 1 layer and try again", "Too many layers selected", MessageBoxButtons.OK);
            }
        }

        private void buttonPasteLayer_Click(object sender, EventArgs e)
        {
            int addIndex = this.layerList.SelectedIndex + 1;
            String layerName = "";

            // Keep showing a name form until valid result
            while (true)
            {
                // Find an unused layer name to initialize a name layer dialog
                int nameIndex = 1;
                String baseName = this.copyLayer.name;

                int copyPosition = baseName.IndexOf("-copy");
                if (copyPosition != -1)
                {
                    int.TryParse(baseName.Substring(copyPosition + 5), out nameIndex);
                    baseName = baseName.Substring(0, copyPosition);
                }

                // Set layer name only if first time through
                if (layerName == "")
                {
                    layerName = baseName;

                    bool uniqueNameFound = false;
                    while (uniqueNameFound == false)
                    {
                        uniqueNameFound = true;
                        for (int i = 0; i < this.textureArray[this.editTextureIndex].layerCount; i++)
                        {
                            if (layerName == this.textureArray[this.editTextureIndex].layerArray[i].name)
                            {
                                uniqueNameFound = false;
                                break;
                            }
                        }
                        if (uniqueNameFound == false)
                        {
                            // Append -copy to name since name already exists
                            layerName = baseName + "-copy" + nameIndex.ToString();
                            nameIndex += 1;
                        }
                    }
                }

                // Pop up a dialog for naming the layer
                nameForm theNameForm = new nameForm();
                theNameForm.Text = "Enter name for new layer";
                theNameForm.nameGroup.Text = "Enter a name for the new layer";
                theNameForm.nameTextBox.Text = layerName;
                theNameForm.nameTextBox.SelectAll();
                theNameForm.nameTextBox.Focus();
                theNameForm.ShowDialog();
                if (theNameForm.DialogResult == DialogResult.OK)
                {
                    // User clicked ok

                    // Get the name and close the form
                    layerName = theNameForm.nameTextBox.Text;
                    theNameForm.Close();

                    // Check if the name is valid
                    // NOTE: Not allowing zero length strings and names already in use
                    if (layerName.Length != 0)
                    {
                        bool nameInUse = false;
                        for (int i = 0; i < this.textureArray[this.editTextureIndex].layerCount; i++)
                        {
                            if (layerName == this.textureArray[this.editTextureIndex].layerArray[i].name)
                            {
                                nameInUse = true;
                                break;
                            }
                        }

                        // Paste layer if name not in use and pop up message if in use
                        if (nameInUse == false)
                        {
                            // Add a layer to the layer array 
                            this.textureArray[this.editTextureIndex].layerCount += 1;

                            // Resize and shift indices to make room for new add index
                            Array.Resize(ref this.textureArray[this.editTextureIndex].layerArray, this.textureArray[this.editTextureIndex].layerCount);
                            for (int i = this.textureArray[this.editTextureIndex].layerCount - 1; i > addIndex; i--)
                            {
                                this.textureArray[this.editTextureIndex].layerArray[i] = this.textureArray[this.editTextureIndex].layerArray[i - 1];
                            }

                            // Paste layer
                            this.textureArray[this.editTextureIndex].layerArray[addIndex] = this.copyLayer;
                            this.textureArray[this.editTextureIndex].layerArray[addIndex].name = layerName;

                            // Insert the new layer into the layer list and select it 
                            this.updateGUIOnly = true;
                            this.layerList.SelectedIndex = -1;
                            this.layerList.Items.Insert(addIndex, layerName);
                            this.updateGUIOnly = false;
                            this.layerList.SelectedIndex = addIndex;
                            break;
                        }
                        else
                        {
                            // Prompt user that layer name is in use
                            MessageBox.Show(null, "'" + layerName + "' is already in use. Try again!", "Layer Name in use", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // Prompt user that layer name is empty
                        MessageBox.Show(null, "Empty Layer name.  Try again!", "Empty Layer Name", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    // User cancelled
                    break;
                }
            }

        }

        private void buttonCopyTexture_Click(object sender, EventArgs e)
        {
            if (this.textureList.SelectedItems.Count == 1)
            {

                // Copy name
                this.copyTexture.name = this.textureArray[this.editTextureIndex].name;

                // Copy size
                this.copyTexture.size = this.textureArray[this.editTextureIndex].size;

                // Copy clip duration, frames per second, and texture scaler
                this.copyTexture.clipDuration = this.textureArray[this.editTextureIndex].clipDuration;
                this.copyTexture.framesPerSecond = this.textureArray[this.editTextureIndex].framesPerSecond;
                this.copyTexture.textureScaler = this.textureArray[this.editTextureIndex].textureScaler;

                // Copy layer count and layers
                this.copyTexture.layerCount = this.textureArray[this.editTextureIndex].layerCount;
                this.copyTexture.layerArray = new layer[this.copyTexture.layerCount];
                for (int i = 0; i < this.copyTexture.layerCount; i++)
                {
                    this.copyTexture.layerArray[i] = this.textureArray[this.editTextureIndex].layerArray[i];
                }

                // Enable paste button now that we have copy data
                buttonPasteTexture.Enabled = true;
            }
            else
            {
                // Prompt user that only 1 texture can be copied at a time
                MessageBox.Show(null, "Only 1 texture can be copied at a time, select 1 and try again", "Too many textures selected", MessageBoxButtons.OK);
            }
        }

        private void buttonPasteTexture_Click(object sender, EventArgs e)
        {
            int addIndex = this.textureList.SelectedIndex + 1;
            String textureName = "";

            // Keep showing a name form until valid result
            while (true)
            {
                // Find an unused texture name to initialize a name texture dialog
                int nameIndex = 1;
                String baseName = this.copyTexture.name;

                int copyPosition = baseName.IndexOf("-copy");
                if (copyPosition != -1)
                {
                    int.TryParse(baseName.Substring(copyPosition + 5), out nameIndex);
                    baseName = baseName.Substring(0, copyPosition);
                }

                // Set texture name only if first time through
                if (textureName == "")
                {
                    textureName = baseName;
                    
                    bool uniqueNameFound = false;
                    while (uniqueNameFound == false)
                    {
                        uniqueNameFound = true;
                        for (int i = 0; i < this.textureCount; i++)
                        {
                            if (textureName == this.textureArray[i].name)
                            {
                                uniqueNameFound = false;
                                break;
                            }
                        }
                        if (uniqueNameFound == false)
                        {
                            // Append -copy to texture name since name already exists
                            textureName = baseName + "-copy" + nameIndex.ToString();
                            nameIndex += 1;
                        }
                    }
                }

                // Pop up a dialog for naming the texture
                nameForm theNameForm = new nameForm();
                theNameForm.Text = "Enter Name for texture";
                theNameForm.nameGroup.Text = "Enter a name for the texture";
                theNameForm.nameTextBox.Text = textureName;
                theNameForm.nameTextBox.SelectAll();
                theNameForm.nameTextBox.Focus();
                theNameForm.ShowDialog();
                if (theNameForm.DialogResult == DialogResult.OK)
                {
                    // User clicked ok

                    // Get the name and close the form
                    textureName = theNameForm.nameTextBox.Text;
                    theNameForm.Close();

                    // Check if the name is valid
                    // NOTE: Not allowing zero length strings and names already in use
                    if (textureName.Length != 0)
                    {
                        bool nameInUse = false;
                        for (int i = 0; i < this.textureCount; i++)
                        {
                            if (textureName == this.textureArray[i].name)
                            {
                                nameInUse = true;
                                break;
                            }
                        }

                        // Paste texture if name not in use and pop up message if in use
                        if (nameInUse == false)
                        {
                            // Add a texture to the texture array 
                            this.textureCount += 1;

                            // Resize and shift indices to make room for new add index
                            Array.Resize(ref textureArray, this.textureCount);
                            for (int i = this.textureCount - 1; i > addIndex; i--)
                            {
                                this.textureArray[i] = this.textureArray[i - 1];
                            }

                            // Paste name
                            this.textureArray[addIndex].name = textureName;

                            // Paste size
                            this.textureArray[addIndex].size = this.copyTexture.size;

                            // Paste clip duration, frames per second, and texture scaler
                            this.textureArray[addIndex].clipDuration = this.copyTexture.clipDuration;
                            this.textureArray[addIndex].framesPerSecond = this.copyTexture.framesPerSecond;
                            this.textureArray[addIndex].textureScaler = this.copyTexture.textureScaler;

                            // Paste layer count and layers
                            this.textureArray[addIndex].layerCount = this.copyTexture.layerCount;
                            this.textureArray[addIndex].layerArray = new layer[this.textureArray[addIndex].layerCount];
                            for (int i = 0; i < this.textureArray[addIndex].layerCount; i++)
                            {
                                this.textureArray[addIndex].layerArray[i] = this.copyTexture.layerArray[i];
                            }

                            // Insert the new texture into the texture list and select it 
                            this.updateGUIOnly = true;
                            this.textureList.SelectedIndex = -1;
                            this.textureList.Items.Insert(addIndex, textureName);
                            this.updateGUIOnly = false;
                            this.textureList.SelectedIndex = addIndex;
                            break;
                        }
                        else
                        {
                            // Prompt user that texture name is in use
                            MessageBox.Show(null, "'" + textureName + "' is already in use. Try again!", "Texture Name in use", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // Prompt user that texture name is empty
                        MessageBox.Show(null, "Empty Texture name.  Try again!", "Empty Texture Name", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    // User cancelled
                    break;
                }
            }
        }

        private void buttonCopyColorTo_Click(object sender, EventArgs e)
        {
            if (this.updateGUIOnly == false && this.editTextureIndex != -1 && this.editLayerIndex != -1)
            {
                layer CopyLayer = this.textureArray[this.editTextureIndex].layerArray[this.editLayerIndex];

                // Now loop thru selected layers and add the delta value with bounds checks
                for (int i = 0; i < this.layerList.SelectedItems.Count; i++)
                {
                    if (radioValue.Checked == true) 
                    {
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].centerRed.value = CopyLayer.centerRed.value;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].centerGreen.value = CopyLayer.centerGreen.value;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].centerBlue.value = CopyLayer.centerBlue.value;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameterRed.value = CopyLayer.diameterRed.value;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameterGreen.value = CopyLayer.diameterGreen.value;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameterBlue.value = CopyLayer.diameterBlue.value;
                    }
                    else if (radioLeftLimit.Checked == true)
                    {
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].centerRed.leftLimit = CopyLayer.centerRed.leftLimit;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].centerGreen.leftLimit = CopyLayer.centerGreen.leftLimit;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].centerBlue.leftLimit = CopyLayer.centerBlue.leftLimit;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameterRed.leftLimit = CopyLayer.diameterRed.leftLimit;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameterGreen.leftLimit = CopyLayer.diameterGreen.leftLimit;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameterBlue.leftLimit = CopyLayer.diameterBlue.leftLimit;
                    }
                    else if (radioRightLimit.Checked == true)
                    {
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].centerRed.rightLimit = CopyLayer.centerRed.rightLimit;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].centerGreen.rightLimit = CopyLayer.centerGreen.rightLimit;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].centerBlue.rightLimit = CopyLayer.centerBlue.rightLimit;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameterRed.rightLimit = CopyLayer.diameterRed.rightLimit;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameterGreen.rightLimit = CopyLayer.diameterGreen.rightLimit;
                        this.textureArray[this.editTextureIndex].layerArray[this.layerList.SelectedIndices[i]].diameterBlue.rightLimit = CopyLayer.diameterBlue.rightLimit;
                    }
                }

                // Reset the clip image count
                // NOTE: Forces regeneration of clip animation
                this.clipImageCount = 0;

                // Trigger update of bimap if not suppressed
                if (this.suppressUpdateBitmap == false)
                {
                    // Set to update edit texture's bitmap
                    this.updateBitmap = true;

                    // Invalidate the main picture box
                    this.mainPictureBox.Invalidate();
                }                               

            }
        }

        private void buttonExportCollectionTextureData_MouseDown(object sender, MouseEventArgs e)
        {

            if (this.collectionList.SelectedItems.Count == 1)
            {
                // jea tbd - disabling the form for now during image creation
                // maybe something else later - thread?
                this.Enabled = false;

                // Store the active edit layer, texture, and collection
                int activeEditLayer = this.editLayerIndex;
                int activeEditTexture = this.editTextureIndex;
                int activeEditCollection = this.editCollectionIndex;

                // Deselect any selected collection
                String textureDataPath = Application.StartupPath + "\\texture data\\";
                this.collectionList.SelectedIndex = -1;

                String collectionDirPath = textureDataPath + this.collectionNames[activeEditCollection] + "\\";

                // Delete any existing texture data directory for the selected collection
                if (Directory.Exists(collectionDirPath) == true)
                {
                    Directory.Delete(collectionDirPath, true);
                }

                // Create a new texture data directory for the selected collection
                Directory.CreateDirectory(collectionDirPath);

                // Create a file of collection names written in proper order
                // for populating the level editor's collection list box
                File.WriteAllLines(textureDataPath + "collections.txt", collectionNames);

                // Load collection, compute collection's textures,
                // and then store both the textures and texture data in collection directory
                // for use by both the level editor and iOS 

                // Load collection from file                   
                this.loadEditorCollectionFromFile(activeEditCollection);

                // Loop thru collection's textures - populate a string array with the texture names,
                // create and save all texture bitmap, and texture data for use by both the level editor and iOS 
                String[] textureNames = new String[this.textureCount];
                for (int j = 0; j < this.textureCount; j++)
                {
                    // Populate the texture name array with the texture name
                    // NOTE: Writing names below to a text file
                    textureNames[j] = this.textureArray[j].name;

                    // Draw the layers of the texture to bitmap
                    bool addLayer = false;
                    for (int k = 0; k < this.textureArray[j].layerCount; k++)
                    {
                        this.dGlowCircle.drawDoubleGlowCircle(ref this.textureArray[j], ref this.textureArray[j].layerArray[k], addLayer);
                        addLayer = true;
                    }

                    // Write the bitmap to file
                    this.textureArray[j].bitmap.Save(collectionDirPath + this.textureArray[j].name + ".png", System.Drawing.Imaging.ImageFormat.Png);

                    // Write the texture data to file
                    this.saveTextureDataToFile(j, collectionDirPath);
                }

                // Create a file of texture names written in proper order
                // for populating the level editor's texture list box
                File.WriteAllLines(collectionDirPath + "textures.txt", textureNames);

                // Try to get an application handle to "Space Shooter Level Editor"
                // and send a key to reload textures if process is running
                IntPtr handleToLevelEditor = FindWindow(null, "Space Shooter Level Editor");
                if (handleToLevelEditor != IntPtr.Zero)
                {
                    // Set space shooter level editor to foreground, wait half second, send ctrl-r
                    // and then reactivate texture create window
                    SetForegroundWindow(handleToLevelEditor);
                    System.Threading.Thread.Sleep(500);
                    SendKeys.SendWait("^r");
                }

                // Reselect the edit collection, texture, and layer
                this.collectionList.SelectedIndex = activeEditCollection;
                this.textureList.SelectedIndex = -1;
                this.textureList.SelectedIndex = activeEditTexture;
                this.layerList.SelectedIndex = -1;
                this.layerList.SelectedIndex = activeEditLayer;

                // Reenable and activate the main form
                this.Enabled = true;
                this.Activate();

                // Pressing right button will also run the .bat file on the desktop - copies textures to IOS side
                // NOTE: only valid on Mike's system
                if (e.Button == MouseButtons.Right)
                {
                    System.Diagnostics.Process.Start("c:\\documents and settings\\mike\\desktop\\CopyTextures.bat");
                }

                // Set focus back to focused up down
                if (this.focusedUpDownControl != null) this.focusedUpDownControl.Focus();
            }
            else
            {
                // Prompt user that only 1 texture can be copied at a time
                MessageBox.Show(null, "Only 1 collection can be exported at a time, select 1 and try again", "Too many collections selected", MessageBoxButtons.OK);
            }
        }

        private void buttonFindReplaceTexture_Click(object sender, EventArgs e)
        {
            // Keep showing a find replace form until valid result
            while (true)
            {
                // Pop up a dialog for naming the texture
                findReplaceForm theForm = new findReplaceForm();
                theForm.Text = "Find and replace texture names";
                theForm.findTextBox.Focus();
                theForm.ShowDialog();
                if (theForm.DialogResult == DialogResult.OK)
                {
                    // User clicked ok

                    // Get the find and replace strings and close the form
                    String findText = theForm.findTextBox.Text;
                    String replaceText = theForm.replaceTextBox.Text;
                    theForm.Close();

                    // Check if the name is valid
                    // NOTE: Not allowing zero length strings and names already in use
                    if (findText.Length != 0)
                    {

                        bool nameInUse = false;
                        //for (int i = 0; i < this.textureCount; i++)
                        //{
                        //    if (textureName == this.textureArray[i].name)
                        //    {
                        //        nameInUse = true;
                        //        break;
                        //    }
                        //}

                        // Paste texture if name not in use and pop up message if in use
                        if (nameInUse == false)
                        {
                            // Copy the selected list
                            int selectedCount = this.textureList.SelectedIndices.Count;
                            int[] selectedIndices = new int[selectedCount];
                            this.textureList.SelectedIndices.CopyTo(selectedIndices, 0);

                            // Loop thru selected textures and replace the text
                            for (int i = 0; i < selectedCount; i++)
                            {
                                int index = selectedIndices[i];

                                string textureName = this.textureArray[index].name;
                                textureName = textureName.Replace(findText, replaceText);
                                this.textureArray[index].name = textureName;

                                this.updateGUIOnly = true;
                                //this.textureList.Items[index] = textureName;
                                this.textureList.Items.RemoveAt(index);
                                this.textureList.Items.Insert(index, textureName);
                                this.updateGUIOnly = false;
                            }

                            // Clear selections
                            this.textureList.SelectedIndex = -1;

                            // Reselect the textures
                            // NOTE: Ensuring that edit texture index does not change
                            for (int i = 0; i < selectedCount; i++)
                            {
                                this.textureList.SetSelected(selectedIndices[i], true);
                            }

                            break;
                        }
                        else
                        {
                            // Prompt user that texture name is in use
                            //MessageBox.Show(null, "'" + textureName + "' is already in use. Try again!", "Texture Name in use", MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        // Prompt user that texture name is empty
                        MessageBox.Show(null, "Empty Texture name.  Try again!", "Empty Texture Name", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    // User cancelled
                    break;
                }
            }

        }

        private void checkFlipVertical_CheckedChanged(object sender, EventArgs e)
        {
            // Trigger a redraw of the texture's bitmap
            this.updateBitmap = true;
            this.mainPictureBox.Invalidate();
        }

        private void buttonPNG_Click(object sender, EventArgs e)
        {
            // Save the currently displayed bitmap as a PNG image file

            // Display a save file dialog to get the file name
            // NOTE: Setting initial directory to either the last existing directory 
            // or the application startup path
            theSaveFileDialog.Title = "Save current bitmap as a PNG image file";
            theSaveFileDialog.DefaultExt = "png";
            theSaveFileDialog.Filter = "PNG image (*.png)|*.png|All files (*.*)|*.*";
            if (theSaveFileDialog.FileName == "") {
                theSaveFileDialog.InitialDirectory = Application.StartupPath;                
            }
            else {
                String dirName = Path.GetDirectoryName(theSaveFileDialog.FileName);
                if (Directory.Exists(dirName))
                {
                    theSaveFileDialog.InitialDirectory = dirName;
                }
                else
                {
                    theSaveFileDialog.InitialDirectory = Application.StartupPath; 
                }
            }
            theSaveFileDialog.FileName = this.textureArray[this.editTextureIndex].name;           

            // Write the PNG image file
            if (theSaveFileDialog.ShowDialog() == DialogResult.OK) 
            {
                this.textureArray[this.editTextureIndex].bitmap.Save(theSaveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
