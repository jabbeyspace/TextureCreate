namespace TextureCreate
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.textureList = new System.Windows.Forms.ListBox();
            this.buttonDeleteTexture = new System.Windows.Forms.Button();
            this.buttonAddTexture = new System.Windows.Forms.Button();
            this.buttonMoveTextureUp = new System.Windows.Forms.Button();
            this.buttonMoveTextureDown = new System.Windows.Forms.Button();
            this.buttonDeleteLayer = new System.Windows.Forms.Button();
            this.buttonAddLayer = new System.Windows.Forms.Button();
            this.buttonMoveLayerUp = new System.Windows.Forms.Button();
            this.buttonMoveLayerDown = new System.Windows.Forms.Button();
            this.layerList = new System.Windows.Forms.ListBox();
            this.checkMultiEdit = new System.Windows.Forms.CheckBox();
            this.checkEditRelative = new System.Windows.Forms.CheckBox();
            this.labelEditLayer = new System.Windows.Forms.Label();
            this.labelTextures = new System.Windows.Forms.Label();
            this.labelLayers = new System.Windows.Forms.Label();
            this.labelOverflow = new System.Windows.Forms.Label();
            this.levelParamEditModeGroup = new System.Windows.Forms.GroupBox();
            this.radioRightLimit = new System.Windows.Forms.RadioButton();
            this.radioLeftLimit = new System.Windows.Forms.RadioButton();
            this.radioValue = new System.Windows.Forms.RadioButton();
            this.buttonExportAllTextureData = new System.Windows.Forms.Button();
            this.buttonRandomize = new System.Windows.Forms.Button();
            this.labelTextureCollections = new System.Windows.Forms.Label();
            this.buttonDeleteCollection = new System.Windows.Forms.Button();
            this.buttonAddCollection = new System.Windows.Forms.Button();
            this.buttonMoveCollectionUp = new System.Windows.Forms.Button();
            this.buttonMoveCollectionDown = new System.Windows.Forms.Button();
            this.buttonCloneCollection = new System.Windows.Forms.Button();
            this.collectionList = new System.Windows.Forms.ListBox();
            this.layerParameterTabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonCopyColorTo = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkShowCollider = new System.Windows.Forms.CheckBox();
            this.checkViewAllLayers = new System.Windows.Forms.CheckBox();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.buttonSetLeftLimitToValue = new System.Windows.Forms.Button();
            this.labelClipTime = new System.Windows.Forms.Label();
            this.labelClipTimeLabel = new System.Windows.Forms.Label();
            this.buttonSetValueToLeftLimit = new System.Windows.Forms.Button();
            this.buttonSetValueToRightLimit = new System.Windows.Forms.Button();
            this.buttonSetRightLimitToValue = new System.Windows.Forms.Button();
            this.checkLoop = new System.Windows.Forms.CheckBox();
            this.checkGenerateClip = new System.Windows.Forms.CheckBox();
            this.checkApplyToAllParams = new System.Windows.Forms.CheckBox();
            this.labelFast = new System.Windows.Forms.Label();
            this.buttonCopyLayer = new System.Windows.Forms.Button();
            this.buttonPasteLayer = new System.Windows.Forms.Button();
            this.buttonCopyTexture = new System.Windows.Forms.Button();
            this.buttonPasteTexture = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonExportCollectionTextureData = new System.Windows.Forms.Button();
            this.buttonFindReplaceTexture = new System.Windows.Forms.Button();
            this.checkRotateImage180 = new System.Windows.Forms.CheckBox();
            this.theSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.buttonPNG = new System.Windows.Forms.Button();
            this.labelFramesPerSecond = new TextureCreate.customLabel();
            this.spinFramesPerSecond = new TextureCreate.customUpDown();
            this.labelClipDuration = new TextureCreate.customLabel();
            this.spinClipDuration = new TextureCreate.customUpDown();
            this.spinAnimationPhase = new TextureCreate.customUpDown();
            this.labelAnimationPhase = new TextureCreate.customLabel();
            this.spinColliderRotateAngle = new TextureCreate.customUpDown();
            this.labelColliderRotateAngle = new TextureCreate.customLabel();
            this.labelColliderXOffset = new TextureCreate.customLabel();
            this.labelColliderYOffset = new TextureCreate.customLabel();
            this.spinColliderXOffset = new TextureCreate.customUpDown();
            this.spinColliderYOffset = new TextureCreate.customUpDown();
            this.labelColorModeText = new TextureCreate.customLabel();
            this.spinColorMode = new TextureCreate.customUpDown();
            this.labelColorMode = new TextureCreate.customLabel();
            this.labelCollisionDiameter2 = new TextureCreate.customLabel();
            this.spinCollisionDiameter2 = new TextureCreate.customUpDown();
            this.labelCollisionDiameter1 = new TextureCreate.customLabel();
            this.spinCollisionDiameter1 = new TextureCreate.customUpDown();
            this.labelScale = new TextureCreate.customLabel();
            this.spinScale = new TextureCreate.customUpDown();
            this.labelXOffset = new TextureCreate.customLabel();
            this.spinDiameter = new TextureCreate.customUpDown();
            this.spinDiameterBrightness = new TextureCreate.customUpDown();
            this.spinCenterBrightness = new TextureCreate.customUpDown();
            this.spinOuterGlowPower = new TextureCreate.customUpDown();
            this.spinInnerGlowPower = new TextureCreate.customUpDown();
            this.labelDiameterBrightness = new TextureCreate.customLabel();
            this.labelCenterBrightness = new TextureCreate.customLabel();
            this.labelDiameter = new TextureCreate.customLabel();
            this.labelOuterGlowPower = new TextureCreate.customLabel();
            this.labelInnerGlowPower = new TextureCreate.customLabel();
            this.spinRadialFreq = new TextureCreate.customUpDown();
            this.labelRadialFreq = new TextureCreate.customLabel();
            this.spinRadialPhase = new TextureCreate.customUpDown();
            this.labelRadialPhase = new TextureCreate.customLabel();
            this.spinRadialAmp = new TextureCreate.customUpDown();
            this.labelRadialAmp = new TextureCreate.customLabel();
            this.labelRadialPower = new TextureCreate.customLabel();
            this.spinPolarAmp = new TextureCreate.customUpDown();
            this.labelPolarPower = new TextureCreate.customLabel();
            this.labelPolarAmp = new TextureCreate.customLabel();
            this.spinRadialPower = new TextureCreate.customUpDown();
            this.spinPolarPhase = new TextureCreate.customUpDown();
            this.spinPolarPower = new TextureCreate.customUpDown();
            this.labelPolarPhase = new TextureCreate.customLabel();
            this.spinPolarFreq = new TextureCreate.customUpDown();
            this.labelYOffset = new TextureCreate.customLabel();
            this.labelPolarFreq = new TextureCreate.customLabel();
            this.spinXOffset = new TextureCreate.customUpDown();
            this.spinCenterRed = new TextureCreate.customUpDown();
            this.spinYOffset = new TextureCreate.customUpDown();
            this.labelCenterRed = new TextureCreate.customLabel();
            this.spinCenterBlue = new TextureCreate.customUpDown();
            this.labelCenterBlue = new TextureCreate.customLabel();
            this.spinCenterGreen = new TextureCreate.customUpDown();
            this.labelCenterGreen = new TextureCreate.customLabel();
            this.spinDiameterRed = new TextureCreate.customUpDown();
            this.labelDiameterRed = new TextureCreate.customLabel();
            this.spinDiameterBlue = new TextureCreate.customUpDown();
            this.labelDiameterBlue = new TextureCreate.customLabel();
            this.spinDiameterGreen = new TextureCreate.customUpDown();
            this.labelDiameterGreen = new TextureCreate.customLabel();
            this.labelSwirlModeText = new TextureCreate.customLabel();
            this.labelMirrorModeText = new TextureCreate.customLabel();
            this.labelMorphModeText = new TextureCreate.customLabel();
            this.spinSwirlMode = new TextureCreate.customUpDown();
            this.labelSwirlMode = new TextureCreate.customLabel();
            this.spinMirrorMode = new TextureCreate.customUpDown();
            this.labelMirrorMode = new TextureCreate.customLabel();
            this.spinMorphMode = new TextureCreate.customUpDown();
            this.labelMorphMode = new TextureCreate.customLabel();
            this.spinCircularStretchX = new TextureCreate.customUpDown();
            this.labelCircularStretchX = new TextureCreate.customLabel();
            this.spinLinearStretchX = new TextureCreate.customUpDown();
            this.labelLinearStretchX = new TextureCreate.customLabel();
            this.spinMorphPhase = new TextureCreate.customUpDown();
            this.labelMorphPhase = new TextureCreate.customLabel();
            this.spinRotateAngle = new TextureCreate.customUpDown();
            this.labelRotateAngle = new TextureCreate.customLabel();
            this.spinSwirl = new TextureCreate.customUpDown();
            this.labelSwirl = new TextureCreate.customLabel();
            this.spinCircularStretchY = new TextureCreate.customUpDown();
            this.labelCircularStretchY = new TextureCreate.customLabel();
            this.spinMorphPower = new TextureCreate.customUpDown();
            this.labelMorphPower = new TextureCreate.customLabel();
            this.spinMorphFreq = new TextureCreate.customUpDown();
            this.labelMorphFreq = new TextureCreate.customLabel();
            this.spinMorphAmp = new TextureCreate.customUpDown();
            this.labelMorphAmp = new TextureCreate.customLabel();
            this.spinLinearStretchY = new TextureCreate.customUpDown();
            this.labelLinearStretchY = new TextureCreate.customLabel();
            this.yEditRandomParamControl = new TextureCreate.levelParameterControl();
            this.xEditRandomParamControl = new TextureCreate.levelParameterControl();
            this.focusedRandomParamControl = new TextureCreate.levelParameterControl();
            this.labelSize = new TextureCreate.customLabel();
            this.spinSize = new TextureCreate.customUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.levelParamEditModeGroup.SuspendLayout();
            this.layerParameterTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinFramesPerSecond)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinClipDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinAnimationPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinColliderRotateAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinColliderXOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinColliderYOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinColorMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCollisionDiameter2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCollisionDiameter1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDiameter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDiameterBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCenterBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOuterGlowPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinInnerGlowPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRadialFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRadialPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRadialAmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPolarAmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRadialPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPolarPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPolarPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPolarFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinXOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCenterRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCenterBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCenterGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDiameterRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDiameterBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDiameterGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSwirlMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMirrorMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMorphMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCircularStretchX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLinearStretchX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMorphPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRotateAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSwirl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCircularStretchY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMorphPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMorphFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMorphAmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLinearStretchY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSize)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mainPictureBox.BackgroundImage")));
            this.mainPictureBox.ErrorImage = null;
            this.mainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(732, 732);
            this.mainPictureBox.TabIndex = 153;
            this.mainPictureBox.TabStop = false;
            this.mainPictureBox.MouseLeave += new System.EventHandler(this.mainPictureBox_MouseLeave);
            this.mainPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseMove);
            this.mainPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseDown);
            this.mainPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.mainPictureBox_Paint);
            // 
            // textureList
            // 
            this.textureList.BackColor = System.Drawing.Color.LightGray;
            this.textureList.FormattingEnabled = true;
            this.textureList.Items.AddRange(new object[] {
            "texture1",
            "texture2",
            "texture3",
            "texture4"});
            this.textureList.Location = new System.Drawing.Point(1095, 434);
            this.textureList.Name = "textureList";
            this.textureList.ScrollAlwaysVisible = true;
            this.textureList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.textureList.Size = new System.Drawing.Size(170, 134);
            this.textureList.TabIndex = 4;
            this.textureList.SelectedIndexChanged += new System.EventHandler(this.textureList_SelectedIndexChanged);
            this.textureList.DoubleClick += new System.EventHandler(this.textureList_DoubleClick);
            // 
            // buttonDeleteTexture
            // 
            this.buttonDeleteTexture.Location = new System.Drawing.Point(1051, 434);
            this.buttonDeleteTexture.Name = "buttonDeleteTexture";
            this.buttonDeleteTexture.Size = new System.Drawing.Size(42, 23);
            this.buttonDeleteTexture.TabIndex = 172;
            this.buttonDeleteTexture.TabStop = false;
            this.buttonDeleteTexture.Text = "-";
            this.buttonDeleteTexture.UseVisualStyleBackColor = true;
            this.buttonDeleteTexture.Click += new System.EventHandler(this.buttonDeleteTexture_Click);
            // 
            // buttonAddTexture
            // 
            this.buttonAddTexture.Location = new System.Drawing.Point(1051, 412);
            this.buttonAddTexture.Name = "buttonAddTexture";
            this.buttonAddTexture.Size = new System.Drawing.Size(42, 23);
            this.buttonAddTexture.TabIndex = 173;
            this.buttonAddTexture.TabStop = false;
            this.buttonAddTexture.Text = "+";
            this.buttonAddTexture.UseVisualStyleBackColor = true;
            this.buttonAddTexture.Click += new System.EventHandler(this.buttonAddTexture_Click);
            // 
            // buttonMoveTextureUp
            // 
            this.buttonMoveTextureUp.Location = new System.Drawing.Point(1051, 456);
            this.buttonMoveTextureUp.Name = "buttonMoveTextureUp";
            this.buttonMoveTextureUp.Size = new System.Drawing.Size(42, 23);
            this.buttonMoveTextureUp.TabIndex = 171;
            this.buttonMoveTextureUp.TabStop = false;
            this.buttonMoveTextureUp.Text = "up";
            this.buttonMoveTextureUp.UseVisualStyleBackColor = true;
            this.buttonMoveTextureUp.Click += new System.EventHandler(this.buttonMoveTextureUp_Click);
            // 
            // buttonMoveTextureDown
            // 
            this.buttonMoveTextureDown.Location = new System.Drawing.Point(1051, 478);
            this.buttonMoveTextureDown.Name = "buttonMoveTextureDown";
            this.buttonMoveTextureDown.Size = new System.Drawing.Size(42, 23);
            this.buttonMoveTextureDown.TabIndex = 169;
            this.buttonMoveTextureDown.TabStop = false;
            this.buttonMoveTextureDown.Text = "down";
            this.buttonMoveTextureDown.UseVisualStyleBackColor = true;
            this.buttonMoveTextureDown.Click += new System.EventHandler(this.buttonMoveTextureDown_Click);
            // 
            // buttonDeleteLayer
            // 
            this.buttonDeleteLayer.Location = new System.Drawing.Point(1051, 234);
            this.buttonDeleteLayer.Name = "buttonDeleteLayer";
            this.buttonDeleteLayer.Size = new System.Drawing.Size(42, 25);
            this.buttonDeleteLayer.TabIndex = 177;
            this.buttonDeleteLayer.TabStop = false;
            this.buttonDeleteLayer.Text = "-";
            this.buttonDeleteLayer.UseVisualStyleBackColor = true;
            this.buttonDeleteLayer.Click += new System.EventHandler(this.buttonDeleteLayer_Click);
            // 
            // buttonAddLayer
            // 
            this.buttonAddLayer.Location = new System.Drawing.Point(1051, 209);
            this.buttonAddLayer.Name = "buttonAddLayer";
            this.buttonAddLayer.Size = new System.Drawing.Size(42, 25);
            this.buttonAddLayer.TabIndex = 178;
            this.buttonAddLayer.TabStop = false;
            this.buttonAddLayer.Text = "+";
            this.buttonAddLayer.UseVisualStyleBackColor = true;
            this.buttonAddLayer.Click += new System.EventHandler(this.buttonAddLayer_Click);
            // 
            // buttonMoveLayerUp
            // 
            this.buttonMoveLayerUp.Location = new System.Drawing.Point(1051, 259);
            this.buttonMoveLayerUp.Name = "buttonMoveLayerUp";
            this.buttonMoveLayerUp.Size = new System.Drawing.Size(42, 25);
            this.buttonMoveLayerUp.TabIndex = 176;
            this.buttonMoveLayerUp.TabStop = false;
            this.buttonMoveLayerUp.Text = "up";
            this.buttonMoveLayerUp.UseVisualStyleBackColor = true;
            this.buttonMoveLayerUp.Click += new System.EventHandler(this.buttonMoveLayerUp_Click);
            // 
            // buttonMoveLayerDown
            // 
            this.buttonMoveLayerDown.Location = new System.Drawing.Point(1051, 284);
            this.buttonMoveLayerDown.Name = "buttonMoveLayerDown";
            this.buttonMoveLayerDown.Size = new System.Drawing.Size(42, 25);
            this.buttonMoveLayerDown.TabIndex = 174;
            this.buttonMoveLayerDown.TabStop = false;
            this.buttonMoveLayerDown.Text = "down";
            this.buttonMoveLayerDown.UseVisualStyleBackColor = true;
            this.buttonMoveLayerDown.Click += new System.EventHandler(this.buttonMoveLayerDown_Click);
            // 
            // layerList
            // 
            this.layerList.BackColor = System.Drawing.Color.LightGray;
            this.layerList.FormattingEnabled = true;
            this.layerList.Items.AddRange(new object[] {
            "layer1",
            "layer2",
            "layer3",
            "layer4"});
            this.layerList.Location = new System.Drawing.Point(1095, 230);
            this.layerList.Name = "layerList";
            this.layerList.ScrollAlwaysVisible = true;
            this.layerList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.layerList.Size = new System.Drawing.Size(170, 160);
            this.layerList.TabIndex = 3;
            this.layerList.SelectedIndexChanged += new System.EventHandler(this.layerList_SelectedIndexChanged);
            this.layerList.DoubleClick += new System.EventHandler(this.layerList_DoubleClick);
            this.layerList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.layerList_MouseDown);
            // 
            // checkMultiEdit
            // 
            this.checkMultiEdit.AutoSize = true;
            this.checkMultiEdit.Location = new System.Drawing.Point(1135, 394);
            this.checkMultiEdit.Name = "checkMultiEdit";
            this.checkMultiEdit.Size = new System.Drawing.Size(67, 17);
            this.checkMultiEdit.TabIndex = 7;
            this.checkMultiEdit.Text = "multi-edit";
            this.checkMultiEdit.UseVisualStyleBackColor = true;
            this.checkMultiEdit.CheckedChanged += new System.EventHandler(this.checkMultiEdit_CheckedChanged);
            // 
            // checkEditRelative
            // 
            this.checkEditRelative.AutoSize = true;
            this.checkEditRelative.Location = new System.Drawing.Point(1201, 394);
            this.checkEditRelative.Name = "checkEditRelative";
            this.checkEditRelative.Size = new System.Drawing.Size(35, 17);
            this.checkEditRelative.TabIndex = 8;
            this.checkEditRelative.TabStop = false;
            this.checkEditRelative.Text = "er";
            this.checkEditRelative.UseVisualStyleBackColor = true;
            this.checkEditRelative.CheckedChanged += new System.EventHandler(this.checkEditRelative_CheckedChanged);
            // 
            // labelEditLayer
            // 
            this.labelEditLayer.BackColor = System.Drawing.SystemColors.Control;
            this.labelEditLayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelEditLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEditLayer.Location = new System.Drawing.Point(1053, 170);
            this.labelEditLayer.Name = "labelEditLayer";
            this.labelEditLayer.Size = new System.Drawing.Size(212, 36);
            this.labelEditLayer.TabIndex = 185;
            this.labelEditLayer.Text = "No Layer Selected";
            this.labelEditLayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTextures
            // 
            this.labelTextures.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTextures.Location = new System.Drawing.Point(1095, 413);
            this.labelTextures.Name = "labelTextures";
            this.labelTextures.Size = new System.Drawing.Size(170, 20);
            this.labelTextures.TabIndex = 187;
            this.labelTextures.Text = "textures";
            this.labelTextures.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelLayers
            // 
            this.labelLayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLayers.Location = new System.Drawing.Point(1095, 209);
            this.labelLayers.Name = "labelLayers";
            this.labelLayers.Size = new System.Drawing.Size(122, 20);
            this.labelLayers.TabIndex = 188;
            this.labelLayers.Text = "layers";
            this.labelLayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelOverflow
            // 
            this.labelOverflow.BackColor = System.Drawing.Color.Green;
            this.labelOverflow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelOverflow.ForeColor = System.Drawing.Color.White;
            this.labelOverflow.Location = new System.Drawing.Point(1219, 209);
            this.labelOverflow.Name = "labelOverflow";
            this.labelOverflow.Size = new System.Drawing.Size(46, 20);
            this.labelOverflow.TabIndex = 189;
            this.labelOverflow.Text = "OK";
            this.labelOverflow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // levelParamEditModeGroup
            // 
            this.levelParamEditModeGroup.Controls.Add(this.radioRightLimit);
            this.levelParamEditModeGroup.Controls.Add(this.radioLeftLimit);
            this.levelParamEditModeGroup.Controls.Add(this.radioValue);
            this.levelParamEditModeGroup.Location = new System.Drawing.Point(738, 88);
            this.levelParamEditModeGroup.Name = "levelParamEditModeGroup";
            this.levelParamEditModeGroup.Size = new System.Drawing.Size(260, 46);
            this.levelParamEditModeGroup.TabIndex = 12;
            this.levelParamEditModeGroup.TabStop = false;
            this.levelParamEditModeGroup.Text = "level parameter edit mode";
            // 
            // radioRightLimit
            // 
            this.radioRightLimit.AutoSize = true;
            this.radioRightLimit.Location = new System.Drawing.Point(130, 20);
            this.radioRightLimit.Name = "radioRightLimit";
            this.radioRightLimit.Size = new System.Drawing.Size(65, 17);
            this.radioRightLimit.TabIndex = 2;
            this.radioRightLimit.Text = "right limit";
            this.radioRightLimit.UseVisualStyleBackColor = true;
            this.radioRightLimit.CheckedChanged += new System.EventHandler(this.radioRightLimit_CheckedChanged);
            // 
            // radioLeftLimit
            // 
            this.radioLeftLimit.AutoSize = true;
            this.radioLeftLimit.Location = new System.Drawing.Point(70, 20);
            this.radioLeftLimit.Name = "radioLeftLimit";
            this.radioLeftLimit.Size = new System.Drawing.Size(59, 17);
            this.radioLeftLimit.TabIndex = 1;
            this.radioLeftLimit.Text = "left limit";
            this.toolTip1.SetToolTip(this.radioLeftLimit, "Left limit and right limit are used as ranges for parameter modes: ani, random an" +
                    "d bounce");
            this.radioLeftLimit.UseVisualStyleBackColor = true;
            this.radioLeftLimit.CheckedChanged += new System.EventHandler(this.radioLeftLimit_CheckedChanged);
            // 
            // radioValue
            // 
            this.radioValue.AutoSize = true;
            this.radioValue.Checked = true;
            this.radioValue.Location = new System.Drawing.Point(18, 20);
            this.radioValue.Name = "radioValue";
            this.radioValue.Size = new System.Drawing.Size(51, 17);
            this.radioValue.TabIndex = 0;
            this.radioValue.TabStop = true;
            this.radioValue.Text = "value";
            this.radioValue.UseVisualStyleBackColor = true;
            this.radioValue.CheckedChanged += new System.EventHandler(this.radioValue_CheckedChanged);
            // 
            // buttonExportAllTextureData
            // 
            this.buttonExportAllTextureData.Location = new System.Drawing.Point(806, 164);
            this.buttonExportAllTextureData.Name = "buttonExportAllTextureData";
            this.buttonExportAllTextureData.Size = new System.Drawing.Size(82, 22);
            this.buttonExportAllTextureData.TabIndex = 207;
            this.buttonExportAllTextureData.TabStop = false;
            this.buttonExportAllTextureData.Text = "export all data";
            this.toolTip1.SetToolTip(this.buttonExportAllTextureData, "Export all textures\r\n\r\nRight click = copy texture data to IOS side by running bat" +
                    "ch file on desktop (mike only)");
            this.buttonExportAllTextureData.UseVisualStyleBackColor = true;
            this.buttonExportAllTextureData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonExportAllTextureData_MouseDown);
            // 
            // buttonRandomize
            // 
            this.buttonRandomize.Location = new System.Drawing.Point(739, 164);
            this.buttonRandomize.Name = "buttonRandomize";
            this.buttonRandomize.Size = new System.Drawing.Size(63, 22);
            this.buttonRandomize.TabIndex = 208;
            this.buttonRandomize.TabStop = false;
            this.buttonRandomize.Text = "randomize";
            this.toolTip1.SetToolTip(this.buttonRandomize, "Randomize all parameters or selected parameter");
            this.buttonRandomize.UseVisualStyleBackColor = true;
            this.buttonRandomize.Click += new System.EventHandler(this.buttonRandomizeAll_Click);
            // 
            // labelTextureCollections
            // 
            this.labelTextureCollections.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTextureCollections.Location = new System.Drawing.Point(1095, 573);
            this.labelTextureCollections.Name = "labelTextureCollections";
            this.labelTextureCollections.Size = new System.Drawing.Size(170, 20);
            this.labelTextureCollections.TabIndex = 216;
            this.labelTextureCollections.Text = "texture collections";
            this.labelTextureCollections.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonDeleteCollection
            // 
            this.buttonDeleteCollection.Location = new System.Drawing.Point(1051, 598);
            this.buttonDeleteCollection.Name = "buttonDeleteCollection";
            this.buttonDeleteCollection.Size = new System.Drawing.Size(42, 25);
            this.buttonDeleteCollection.TabIndex = 214;
            this.buttonDeleteCollection.TabStop = false;
            this.buttonDeleteCollection.Text = "-";
            this.buttonDeleteCollection.UseVisualStyleBackColor = true;
            this.buttonDeleteCollection.Click += new System.EventHandler(this.buttonDeleteCollection_Click);
            // 
            // buttonAddCollection
            // 
            this.buttonAddCollection.Location = new System.Drawing.Point(1051, 573);
            this.buttonAddCollection.Name = "buttonAddCollection";
            this.buttonAddCollection.Size = new System.Drawing.Size(42, 25);
            this.buttonAddCollection.TabIndex = 215;
            this.buttonAddCollection.TabStop = false;
            this.buttonAddCollection.Text = "+";
            this.buttonAddCollection.UseVisualStyleBackColor = true;
            this.buttonAddCollection.Click += new System.EventHandler(this.buttonAddCollection_Click);
            // 
            // buttonMoveCollectionUp
            // 
            this.buttonMoveCollectionUp.Location = new System.Drawing.Point(1051, 623);
            this.buttonMoveCollectionUp.Name = "buttonMoveCollectionUp";
            this.buttonMoveCollectionUp.Size = new System.Drawing.Size(42, 25);
            this.buttonMoveCollectionUp.TabIndex = 213;
            this.buttonMoveCollectionUp.TabStop = false;
            this.buttonMoveCollectionUp.Text = "up";
            this.buttonMoveCollectionUp.UseVisualStyleBackColor = true;
            this.buttonMoveCollectionUp.Click += new System.EventHandler(this.buttonMoveCollectionUp_Click);
            // 
            // buttonMoveCollectionDown
            // 
            this.buttonMoveCollectionDown.Location = new System.Drawing.Point(1051, 648);
            this.buttonMoveCollectionDown.Name = "buttonMoveCollectionDown";
            this.buttonMoveCollectionDown.Size = new System.Drawing.Size(42, 25);
            this.buttonMoveCollectionDown.TabIndex = 211;
            this.buttonMoveCollectionDown.TabStop = false;
            this.buttonMoveCollectionDown.Text = "down";
            this.buttonMoveCollectionDown.UseVisualStyleBackColor = true;
            this.buttonMoveCollectionDown.Click += new System.EventHandler(this.buttonMoveCollectionDown_Click);
            // 
            // buttonCloneCollection
            // 
            this.buttonCloneCollection.Location = new System.Drawing.Point(1051, 673);
            this.buttonCloneCollection.Name = "buttonCloneCollection";
            this.buttonCloneCollection.Size = new System.Drawing.Size(42, 25);
            this.buttonCloneCollection.TabIndex = 212;
            this.buttonCloneCollection.TabStop = false;
            this.buttonCloneCollection.Text = "clone";
            this.buttonCloneCollection.UseVisualStyleBackColor = true;
            this.buttonCloneCollection.Click += new System.EventHandler(this.buttonCloneCollection_Click);
            // 
            // collectionList
            // 
            this.collectionList.BackColor = System.Drawing.Color.LightGray;
            this.collectionList.FormattingEnabled = true;
            this.collectionList.Items.AddRange(new object[] {
            "collection1",
            "collection2",
            "collection3",
            "collection4"});
            this.collectionList.Location = new System.Drawing.Point(1095, 594);
            this.collectionList.Name = "collectionList";
            this.collectionList.ScrollAlwaysVisible = true;
            this.collectionList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.collectionList.Size = new System.Drawing.Size(170, 134);
            this.collectionList.TabIndex = 5;
            this.collectionList.SelectedIndexChanged += new System.EventHandler(this.collectionList_SelectedIndexChanged);
            this.collectionList.DoubleClick += new System.EventHandler(this.collectionList_DoubleClick);
            // 
            // layerParameterTabs
            // 
            this.layerParameterTabs.Controls.Add(this.tabPage1);
            this.layerParameterTabs.Controls.Add(this.tabPage2);
            this.layerParameterTabs.Location = new System.Drawing.Point(736, 276);
            this.layerParameterTabs.Name = "layerParameterTabs";
            this.layerParameterTabs.SelectedIndex = 0;
            this.layerParameterTabs.Size = new System.Drawing.Size(307, 452);
            this.layerParameterTabs.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.spinAnimationPhase);
            this.tabPage1.Controls.Add(this.labelAnimationPhase);
            this.tabPage1.Controls.Add(this.buttonCopyColorTo);
            this.tabPage1.Controls.Add(this.spinColliderRotateAngle);
            this.tabPage1.Controls.Add(this.labelColliderRotateAngle);
            this.tabPage1.Controls.Add(this.labelColliderXOffset);
            this.tabPage1.Controls.Add(this.labelColliderYOffset);
            this.tabPage1.Controls.Add(this.spinColliderXOffset);
            this.tabPage1.Controls.Add(this.spinColliderYOffset);
            this.tabPage1.Controls.Add(this.labelColorModeText);
            this.tabPage1.Controls.Add(this.spinColorMode);
            this.tabPage1.Controls.Add(this.labelColorMode);
            this.tabPage1.Controls.Add(this.labelCollisionDiameter2);
            this.tabPage1.Controls.Add(this.spinCollisionDiameter2);
            this.tabPage1.Controls.Add(this.labelCollisionDiameter1);
            this.tabPage1.Controls.Add(this.spinCollisionDiameter1);
            this.tabPage1.Controls.Add(this.labelScale);
            this.tabPage1.Controls.Add(this.spinScale);
            this.tabPage1.Controls.Add(this.labelXOffset);
            this.tabPage1.Controls.Add(this.spinDiameter);
            this.tabPage1.Controls.Add(this.spinDiameterBrightness);
            this.tabPage1.Controls.Add(this.spinCenterBrightness);
            this.tabPage1.Controls.Add(this.spinOuterGlowPower);
            this.tabPage1.Controls.Add(this.spinInnerGlowPower);
            this.tabPage1.Controls.Add(this.labelDiameterBrightness);
            this.tabPage1.Controls.Add(this.labelCenterBrightness);
            this.tabPage1.Controls.Add(this.labelDiameter);
            this.tabPage1.Controls.Add(this.labelOuterGlowPower);
            this.tabPage1.Controls.Add(this.labelInnerGlowPower);
            this.tabPage1.Controls.Add(this.spinRadialFreq);
            this.tabPage1.Controls.Add(this.labelRadialFreq);
            this.tabPage1.Controls.Add(this.spinRadialPhase);
            this.tabPage1.Controls.Add(this.labelRadialPhase);
            this.tabPage1.Controls.Add(this.spinRadialAmp);
            this.tabPage1.Controls.Add(this.labelRadialAmp);
            this.tabPage1.Controls.Add(this.labelRadialPower);
            this.tabPage1.Controls.Add(this.spinPolarAmp);
            this.tabPage1.Controls.Add(this.labelPolarPower);
            this.tabPage1.Controls.Add(this.labelPolarAmp);
            this.tabPage1.Controls.Add(this.spinRadialPower);
            this.tabPage1.Controls.Add(this.spinPolarPhase);
            this.tabPage1.Controls.Add(this.spinPolarPower);
            this.tabPage1.Controls.Add(this.labelPolarPhase);
            this.tabPage1.Controls.Add(this.spinPolarFreq);
            this.tabPage1.Controls.Add(this.labelYOffset);
            this.tabPage1.Controls.Add(this.labelPolarFreq);
            this.tabPage1.Controls.Add(this.spinXOffset);
            this.tabPage1.Controls.Add(this.spinCenterRed);
            this.tabPage1.Controls.Add(this.spinYOffset);
            this.tabPage1.Controls.Add(this.labelCenterRed);
            this.tabPage1.Controls.Add(this.spinCenterBlue);
            this.tabPage1.Controls.Add(this.labelCenterBlue);
            this.tabPage1.Controls.Add(this.spinCenterGreen);
            this.tabPage1.Controls.Add(this.labelCenterGreen);
            this.tabPage1.Controls.Add(this.spinDiameterRed);
            this.tabPage1.Controls.Add(this.labelDiameterRed);
            this.tabPage1.Controls.Add(this.spinDiameterBlue);
            this.tabPage1.Controls.Add(this.labelDiameterBlue);
            this.tabPage1.Controls.Add(this.spinDiameterGreen);
            this.tabPage1.Controls.Add(this.labelDiameterGreen);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(299, 426);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "double glow";
            // 
            // buttonCopyColorTo
            // 
            this.buttonCopyColorTo.Location = new System.Drawing.Point(153, 296);
            this.buttonCopyColorTo.Name = "buttonCopyColorTo";
            this.buttonCopyColorTo.Size = new System.Drawing.Size(52, 24);
            this.buttonCopyColorTo.TabIndex = 232;
            this.buttonCopyColorTo.Text = "copy to";
            this.toolTip1.SetToolTip(this.buttonCopyColorTo, "Copy the color parameters to all selected layers.\r\nUses the last selected layer a" +
                    "s the source.");
            this.buttonCopyColorTo.UseVisualStyleBackColor = true;
            this.buttonCopyColorTo.Click += new System.EventHandler(this.buttonCopyColorTo_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.labelSwirlModeText);
            this.tabPage2.Controls.Add(this.labelMirrorModeText);
            this.tabPage2.Controls.Add(this.labelMorphModeText);
            this.tabPage2.Controls.Add(this.spinSwirlMode);
            this.tabPage2.Controls.Add(this.labelSwirlMode);
            this.tabPage2.Controls.Add(this.spinMirrorMode);
            this.tabPage2.Controls.Add(this.labelMirrorMode);
            this.tabPage2.Controls.Add(this.spinMorphMode);
            this.tabPage2.Controls.Add(this.labelMorphMode);
            this.tabPage2.Controls.Add(this.spinCircularStretchX);
            this.tabPage2.Controls.Add(this.labelCircularStretchX);
            this.tabPage2.Controls.Add(this.spinLinearStretchX);
            this.tabPage2.Controls.Add(this.labelLinearStretchX);
            this.tabPage2.Controls.Add(this.spinMorphPhase);
            this.tabPage2.Controls.Add(this.labelMorphPhase);
            this.tabPage2.Controls.Add(this.spinRotateAngle);
            this.tabPage2.Controls.Add(this.labelRotateAngle);
            this.tabPage2.Controls.Add(this.spinSwirl);
            this.tabPage2.Controls.Add(this.labelSwirl);
            this.tabPage2.Controls.Add(this.spinCircularStretchY);
            this.tabPage2.Controls.Add(this.labelCircularStretchY);
            this.tabPage2.Controls.Add(this.spinMorphPower);
            this.tabPage2.Controls.Add(this.labelMorphPower);
            this.tabPage2.Controls.Add(this.spinMorphFreq);
            this.tabPage2.Controls.Add(this.labelMorphFreq);
            this.tabPage2.Controls.Add(this.spinMorphAmp);
            this.tabPage2.Controls.Add(this.labelMorphAmp);
            this.tabPage2.Controls.Add(this.spinLinearStretchY);
            this.tabPage2.Controls.Add(this.labelLinearStretchY);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(299, 426);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "morph";
            // 
            // checkShowCollider
            // 
            this.checkShowCollider.AutoSize = true;
            this.checkShowCollider.Location = new System.Drawing.Point(951, 192);
            this.checkShowCollider.Name = "checkShowCollider";
            this.checkShowCollider.Size = new System.Drawing.Size(87, 17);
            this.checkShowCollider.TabIndex = 241;
            this.checkShowCollider.TabStop = false;
            this.checkShowCollider.Text = "show collider";
            this.checkShowCollider.UseVisualStyleBackColor = true;
            this.checkShowCollider.CheckedChanged += new System.EventHandler(this.checkShowCollider_CheckedChanged);
            // 
            // checkViewAllLayers
            // 
            this.checkViewAllLayers.AutoSize = true;
            this.checkViewAllLayers.Location = new System.Drawing.Point(1097, 394);
            this.checkViewAllLayers.Name = "checkViewAllLayers";
            this.checkViewAllLayers.Size = new System.Drawing.Size(36, 17);
            this.checkViewAllLayers.TabIndex = 6;
            this.checkViewAllLayers.TabStop = false;
            this.checkViewAllLayers.Text = "all";
            this.checkViewAllLayers.UseVisualStyleBackColor = true;
            this.checkViewAllLayers.CheckedChanged += new System.EventHandler(this.checkViewAllLayers_CheckedChanged);
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(740, 214);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(44, 28);
            this.buttonPlay.TabIndex = 219;
            this.buttonPlay.TabStop = false;
            this.buttonPlay.Text = "play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(740, 244);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(44, 26);
            this.buttonStop.TabIndex = 218;
            this.buttonStop.TabStop = false;
            this.buttonStop.Text = "stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // animationTimer
            // 
            this.animationTimer.Tick += new System.EventHandler(this.animationTimer_Tick);
            // 
            // buttonSetLeftLimitToValue
            // 
            this.buttonSetLeftLimitToValue.Location = new System.Drawing.Point(812, 138);
            this.buttonSetLeftLimitToValue.Name = "buttonSetLeftLimitToValue";
            this.buttonSetLeftLimitToValue.Size = new System.Drawing.Size(44, 22);
            this.buttonSetLeftLimitToValue.TabIndex = 224;
            this.buttonSetLeftLimitToValue.TabStop = false;
            this.buttonSetLeftLimitToValue.Text = "val > l";
            this.toolTip1.SetToolTip(this.buttonSetLeftLimitToValue, "Copy \"value\" parameters to \"left limit\"");
            this.buttonSetLeftLimitToValue.UseVisualStyleBackColor = true;
            this.buttonSetLeftLimitToValue.Click += new System.EventHandler(this.buttonSetLeftLimitToValue_Click);
            // 
            // labelClipTime
            // 
            this.labelClipTime.BackColor = System.Drawing.Color.White;
            this.labelClipTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelClipTime.Location = new System.Drawing.Point(846, 216);
            this.labelClipTime.Name = "labelClipTime";
            this.labelClipTime.Size = new System.Drawing.Size(42, 20);
            this.labelClipTime.TabIndex = 233;
            this.labelClipTime.Text = "0.00";
            this.labelClipTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelClipTimeLabel
            // 
            this.labelClipTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelClipTimeLabel.Location = new System.Drawing.Point(794, 216);
            this.labelClipTimeLabel.Name = "labelClipTimeLabel";
            this.labelClipTimeLabel.Size = new System.Drawing.Size(50, 20);
            this.labelClipTimeLabel.TabIndex = 234;
            this.labelClipTimeLabel.Text = "clip time";
            this.labelClipTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonSetValueToLeftLimit
            // 
            this.buttonSetValueToLeftLimit.Location = new System.Drawing.Point(908, 138);
            this.buttonSetValueToLeftLimit.Name = "buttonSetValueToLeftLimit";
            this.buttonSetValueToLeftLimit.Size = new System.Drawing.Size(45, 22);
            this.buttonSetValueToLeftLimit.TabIndex = 235;
            this.buttonSetValueToLeftLimit.TabStop = false;
            this.buttonSetValueToLeftLimit.Text = "l > val ";
            this.toolTip1.SetToolTip(this.buttonSetValueToLeftLimit, "Copy \"left limit\" parameters to \"value\"");
            this.buttonSetValueToLeftLimit.UseVisualStyleBackColor = true;
            this.buttonSetValueToLeftLimit.Click += new System.EventHandler(this.buttonSetValueToLeftLimit_Click);
            // 
            // buttonSetValueToRightLimit
            // 
            this.buttonSetValueToRightLimit.Location = new System.Drawing.Point(954, 138);
            this.buttonSetValueToRightLimit.Name = "buttonSetValueToRightLimit";
            this.buttonSetValueToRightLimit.Size = new System.Drawing.Size(45, 22);
            this.buttonSetValueToRightLimit.TabIndex = 237;
            this.buttonSetValueToRightLimit.TabStop = false;
            this.buttonSetValueToRightLimit.Text = "r > val";
            this.toolTip1.SetToolTip(this.buttonSetValueToRightLimit, "Copy \"right limit\" parameters to \"value\"");
            this.buttonSetValueToRightLimit.UseVisualStyleBackColor = true;
            this.buttonSetValueToRightLimit.Click += new System.EventHandler(this.buttonSetValueToRightLimit_Click);
            // 
            // buttonSetRightLimitToValue
            // 
            this.buttonSetRightLimitToValue.Location = new System.Drawing.Point(857, 138);
            this.buttonSetRightLimitToValue.Name = "buttonSetRightLimitToValue";
            this.buttonSetRightLimitToValue.Size = new System.Drawing.Size(44, 22);
            this.buttonSetRightLimitToValue.TabIndex = 236;
            this.buttonSetRightLimitToValue.TabStop = false;
            this.buttonSetRightLimitToValue.Text = "val > r";
            this.toolTip1.SetToolTip(this.buttonSetRightLimitToValue, "Copy \"value\" parameters to \"right limit\"");
            this.buttonSetRightLimitToValue.UseVisualStyleBackColor = true;
            this.buttonSetRightLimitToValue.Click += new System.EventHandler(this.buttonSetRightLimitToValue_Click);
            // 
            // checkLoop
            // 
            this.checkLoop.AutoSize = true;
            this.checkLoop.Location = new System.Drawing.Point(794, 244);
            this.checkLoop.Name = "checkLoop";
            this.checkLoop.Size = new System.Drawing.Size(46, 17);
            this.checkLoop.TabIndex = 238;
            this.checkLoop.TabStop = false;
            this.checkLoop.Text = "loop";
            this.toolTip1.SetToolTip(this.checkLoop, "Loop video during playback in the editor");
            this.checkLoop.UseVisualStyleBackColor = true;
            // 
            // checkGenerateClip
            // 
            this.checkGenerateClip.AutoSize = true;
            this.checkGenerateClip.Location = new System.Drawing.Point(883, 192);
            this.checkGenerateClip.Name = "checkGenerateClip";
            this.checkGenerateClip.Size = new System.Drawing.Size(63, 17);
            this.checkGenerateClip.TabIndex = 242;
            this.checkGenerateClip.TabStop = false;
            this.checkGenerateClip.Text = "gen clip";
            this.checkGenerateClip.UseVisualStyleBackColor = true;
            this.checkGenerateClip.CheckedChanged += new System.EventHandler(this.checkGenerateClip_CheckedChanged);
            // 
            // checkApplyToAllParams
            // 
            this.checkApplyToAllParams.AutoSize = true;
            this.checkApplyToAllParams.Location = new System.Drawing.Point(740, 141);
            this.checkApplyToAllParams.Name = "checkApplyToAllParams";
            this.checkApplyToAllParams.Size = new System.Drawing.Size(73, 17);
            this.checkApplyToAllParams.TabIndex = 243;
            this.checkApplyToAllParams.TabStop = false;
            this.checkApplyToAllParams.Text = "all params";
            this.toolTip1.SetToolTip(this.checkApplyToAllParams, "Used when randomizing and copying parameters");
            this.checkApplyToAllParams.UseVisualStyleBackColor = true;
            // 
            // labelFast
            // 
            this.labelFast.BackColor = System.Drawing.Color.Green;
            this.labelFast.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFast.ForeColor = System.Drawing.Color.White;
            this.labelFast.Location = new System.Drawing.Point(996, 268);
            this.labelFast.Name = "labelFast";
            this.labelFast.Size = new System.Drawing.Size(44, 20);
            this.labelFast.TabIndex = 244;
            this.labelFast.Text = "FAST";
            this.labelFast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCopyLayer
            // 
            this.buttonCopyLayer.Location = new System.Drawing.Point(1051, 309);
            this.buttonCopyLayer.Name = "buttonCopyLayer";
            this.buttonCopyLayer.Size = new System.Drawing.Size(42, 24);
            this.buttonCopyLayer.TabIndex = 245;
            this.buttonCopyLayer.TabStop = false;
            this.buttonCopyLayer.Text = "copy";
            this.buttonCopyLayer.UseVisualStyleBackColor = true;
            this.buttonCopyLayer.Click += new System.EventHandler(this.buttonCopyLayer_Click);
            // 
            // buttonPasteLayer
            // 
            this.buttonPasteLayer.Enabled = false;
            this.buttonPasteLayer.Location = new System.Drawing.Point(1051, 334);
            this.buttonPasteLayer.Name = "buttonPasteLayer";
            this.buttonPasteLayer.Size = new System.Drawing.Size(42, 24);
            this.buttonPasteLayer.TabIndex = 246;
            this.buttonPasteLayer.TabStop = false;
            this.buttonPasteLayer.Text = "paste";
            this.buttonPasteLayer.UseVisualStyleBackColor = true;
            this.buttonPasteLayer.Click += new System.EventHandler(this.buttonPasteLayer_Click);
            // 
            // buttonCopyTexture
            // 
            this.buttonCopyTexture.Location = new System.Drawing.Point(1051, 500);
            this.buttonCopyTexture.Name = "buttonCopyTexture";
            this.buttonCopyTexture.Size = new System.Drawing.Size(42, 23);
            this.buttonCopyTexture.TabIndex = 247;
            this.buttonCopyTexture.TabStop = false;
            this.buttonCopyTexture.Text = "copy";
            this.buttonCopyTexture.UseVisualStyleBackColor = true;
            this.buttonCopyTexture.Click += new System.EventHandler(this.buttonCopyTexture_Click);
            // 
            // buttonPasteTexture
            // 
            this.buttonPasteTexture.Enabled = false;
            this.buttonPasteTexture.Location = new System.Drawing.Point(1051, 522);
            this.buttonPasteTexture.Name = "buttonPasteTexture";
            this.buttonPasteTexture.Size = new System.Drawing.Size(42, 23);
            this.buttonPasteTexture.TabIndex = 248;
            this.buttonPasteTexture.TabStop = false;
            this.buttonPasteTexture.Text = "paste";
            this.buttonPasteTexture.UseVisualStyleBackColor = true;
            this.buttonPasteTexture.Click += new System.EventHandler(this.buttonPasteTexture_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 30000;
            this.toolTip1.InitialDelay = 750;
            this.toolTip1.ReshowDelay = 500;
            // 
            // buttonExportCollectionTextureData
            // 
            this.buttonExportCollectionTextureData.Location = new System.Drawing.Point(889, 164);
            this.buttonExportCollectionTextureData.Name = "buttonExportCollectionTextureData";
            this.buttonExportCollectionTextureData.Size = new System.Drawing.Size(92, 22);
            this.buttonExportCollectionTextureData.TabIndex = 249;
            this.buttonExportCollectionTextureData.Text = "export collection";
            this.toolTip1.SetToolTip(this.buttonExportCollectionTextureData, "Export textures for the selected collection only\r\n\r\nRight click = copy texture da" +
                    "ta to IOS side by running batch file on desktop (mike only)");
            this.buttonExportCollectionTextureData.UseVisualStyleBackColor = true;
            this.buttonExportCollectionTextureData.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonExportCollectionTextureData_MouseDown);
            // 
            // buttonFindReplaceTexture
            // 
            this.buttonFindReplaceTexture.Location = new System.Drawing.Point(1051, 544);
            this.buttonFindReplaceTexture.Name = "buttonFindReplaceTexture";
            this.buttonFindReplaceTexture.Size = new System.Drawing.Size(42, 23);
            this.buttonFindReplaceTexture.TabIndex = 250;
            this.buttonFindReplaceTexture.Text = "f/r";
            this.toolTip1.SetToolTip(this.buttonFindReplaceTexture, "Find and replace text");
            this.buttonFindReplaceTexture.UseVisualStyleBackColor = true;
            this.buttonFindReplaceTexture.Click += new System.EventHandler(this.buttonFindReplaceTexture_Click);
            // 
            // checkRotateImage180
            // 
            this.checkRotateImage180.AutoSize = true;
            this.checkRotateImage180.Location = new System.Drawing.Point(845, 244);
            this.checkRotateImage180.Name = "checkRotateImage180";
            this.checkRotateImage180.Size = new System.Drawing.Size(69, 17);
            this.checkRotateImage180.TabIndex = 251;
            this.checkRotateImage180.Text = "rot image";
            this.toolTip1.SetToolTip(this.checkRotateImage180, resources.GetString("checkRotateImage180.ToolTip"));
            this.checkRotateImage180.UseVisualStyleBackColor = true;
            this.checkRotateImage180.CheckedChanged += new System.EventHandler(this.checkFlipVertical_CheckedChanged);
            // 
            // buttonPNG
            // 
            this.buttonPNG.Location = new System.Drawing.Point(985, 164);
            this.buttonPNG.Name = "buttonPNG";
            this.buttonPNG.Size = new System.Drawing.Size(67, 22);
            this.buttonPNG.TabIndex = 252;
            this.buttonPNG.TabStop = false;
            this.buttonPNG.Text = "create png";
            this.buttonPNG.UseVisualStyleBackColor = true;
            this.buttonPNG.Click += new System.EventHandler(this.buttonPNG_Click);
            // 
            // labelFramesPerSecond
            // 
            this.labelFramesPerSecond.BackColor = System.Drawing.SystemColors.Control;
            this.labelFramesPerSecond.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFramesPerSecond.Location = new System.Drawing.Point(914, 242);
            this.labelFramesPerSecond.Name = "labelFramesPerSecond";
            this.labelFramesPerSecond.Size = new System.Drawing.Size(71, 20);
            this.labelFramesPerSecond.TabIndex = 241;
            this.labelFramesPerSecond.Text = "fps";
            this.labelFramesPerSecond.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinFramesPerSecond
            // 
            this.spinFramesPerSecond.DataIndex = 0;
            this.spinFramesPerSecond.DecimalPlaces = 1;
            this.spinFramesPerSecond.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinFramesPerSecond.Location = new System.Drawing.Point(987, 242);
            this.spinFramesPerSecond.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            131072});
            this.spinFramesPerSecond.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinFramesPerSecond.Name = "spinFramesPerSecond";
            this.spinFramesPerSecond.Size = new System.Drawing.Size(52, 20);
            this.spinFramesPerSecond.TabIndex = 240;
            this.spinFramesPerSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.spinFramesPerSecond.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // labelClipDuration
            // 
            this.labelClipDuration.BackColor = System.Drawing.SystemColors.Control;
            this.labelClipDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelClipDuration.Location = new System.Drawing.Point(914, 216);
            this.labelClipDuration.Name = "labelClipDuration";
            this.labelClipDuration.Size = new System.Drawing.Size(71, 20);
            this.labelClipDuration.TabIndex = 221;
            this.labelClipDuration.Text = "clip duration";
            this.labelClipDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinClipDuration
            // 
            this.spinClipDuration.DataIndex = 0;
            this.spinClipDuration.DecimalPlaces = 2;
            this.spinClipDuration.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spinClipDuration.Location = new System.Drawing.Point(987, 216);
            this.spinClipDuration.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            131072});
            this.spinClipDuration.Name = "spinClipDuration";
            this.spinClipDuration.Size = new System.Drawing.Size(52, 20);
            this.spinClipDuration.TabIndex = 220;
            this.spinClipDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // spinAnimationPhase
            // 
            this.spinAnimationPhase.DataIndex = 0;
            this.spinAnimationPhase.DecimalPlaces = 3;
            this.spinAnimationPhase.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.spinAnimationPhase.Location = new System.Drawing.Point(245, 108);
            this.spinAnimationPhase.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinAnimationPhase.Name = "spinAnimationPhase";
            this.spinAnimationPhase.Size = new System.Drawing.Size(52, 20);
            this.spinAnimationPhase.TabIndex = 233;
            this.spinAnimationPhase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelAnimationPhase
            // 
            this.labelAnimationPhase.BackColor = System.Drawing.SystemColors.Control;
            this.labelAnimationPhase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelAnimationPhase.Location = new System.Drawing.Point(153, 108);
            this.labelAnimationPhase.Name = "labelAnimationPhase";
            this.labelAnimationPhase.Size = new System.Drawing.Size(90, 20);
            this.labelAnimationPhase.TabIndex = 234;
            this.labelAnimationPhase.Text = "animation phase";
            this.labelAnimationPhase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinColliderRotateAngle
            // 
            this.spinColliderRotateAngle.DataIndex = 0;
            this.spinColliderRotateAngle.DecimalPlaces = 3;
            this.spinColliderRotateAngle.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinColliderRotateAngle.Location = new System.Drawing.Point(92, 108);
            this.spinColliderRotateAngle.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147352576});
            this.spinColliderRotateAngle.Name = "spinColliderRotateAngle";
            this.spinColliderRotateAngle.Size = new System.Drawing.Size(52, 20);
            this.spinColliderRotateAngle.TabIndex = 230;
            this.spinColliderRotateAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelColliderRotateAngle
            // 
            this.labelColliderRotateAngle.BackColor = System.Drawing.SystemColors.Control;
            this.labelColliderRotateAngle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColliderRotateAngle.Location = new System.Drawing.Point(0, 108);
            this.labelColliderRotateAngle.Name = "labelColliderRotateAngle";
            this.labelColliderRotateAngle.Size = new System.Drawing.Size(90, 20);
            this.labelColliderRotateAngle.TabIndex = 231;
            this.labelColliderRotateAngle.Text = "collider rot angle";
            this.labelColliderRotateAngle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelColliderXOffset
            // 
            this.labelColliderXOffset.BackColor = System.Drawing.SystemColors.Control;
            this.labelColliderXOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColliderXOffset.Location = new System.Drawing.Point(0, 82);
            this.labelColliderXOffset.Name = "labelColliderXOffset";
            this.labelColliderXOffset.Size = new System.Drawing.Size(90, 20);
            this.labelColliderXOffset.TabIndex = 228;
            this.labelColliderXOffset.Text = "collider x offset";
            this.labelColliderXOffset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelColliderYOffset
            // 
            this.labelColliderYOffset.BackColor = System.Drawing.SystemColors.Control;
            this.labelColliderYOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColliderYOffset.Location = new System.Drawing.Point(153, 82);
            this.labelColliderYOffset.Name = "labelColliderYOffset";
            this.labelColliderYOffset.Size = new System.Drawing.Size(90, 20);
            this.labelColliderYOffset.TabIndex = 229;
            this.labelColliderYOffset.Text = "collider y offset";
            this.labelColliderYOffset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinColliderXOffset
            // 
            this.spinColliderXOffset.DataIndex = 0;
            this.spinColliderXOffset.DecimalPlaces = 3;
            this.spinColliderXOffset.Increment = new decimal(new int[] {
            2,
            0,
            0,
            196608});
            this.spinColliderXOffset.Location = new System.Drawing.Point(92, 82);
            this.spinColliderXOffset.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.spinColliderXOffset.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147352576});
            this.spinColliderXOffset.Name = "spinColliderXOffset";
            this.spinColliderXOffset.Size = new System.Drawing.Size(52, 20);
            this.spinColliderXOffset.TabIndex = 226;
            this.spinColliderXOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // spinColliderYOffset
            // 
            this.spinColliderYOffset.DataIndex = 0;
            this.spinColliderYOffset.DecimalPlaces = 3;
            this.spinColliderYOffset.Increment = new decimal(new int[] {
            2,
            0,
            0,
            196608});
            this.spinColliderYOffset.Location = new System.Drawing.Point(245, 82);
            this.spinColliderYOffset.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.spinColliderYOffset.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147352576});
            this.spinColliderYOffset.Name = "spinColliderYOffset";
            this.spinColliderYOffset.Size = new System.Drawing.Size(52, 20);
            this.spinColliderYOffset.TabIndex = 227;
            this.spinColliderYOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelColorModeText
            // 
            this.labelColorModeText.BackColor = System.Drawing.Color.White;
            this.labelColorModeText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColorModeText.Location = new System.Drawing.Point(0, 303);
            this.labelColorModeText.Name = "labelColorModeText";
            this.labelColorModeText.Size = new System.Drawing.Size(144, 20);
            this.labelColorModeText.TabIndex = 225;
            this.labelColorModeText.Text = "color mode text";
            this.labelColorModeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // spinColorMode
            // 
            this.spinColorMode.DataIndex = 0;
            this.spinColorMode.Location = new System.Drawing.Point(92, 325);
            this.spinColorMode.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.spinColorMode.Name = "spinColorMode";
            this.spinColorMode.Size = new System.Drawing.Size(52, 20);
            this.spinColorMode.TabIndex = 223;
            this.spinColorMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelColorMode
            // 
            this.labelColorMode.BackColor = System.Drawing.SystemColors.Control;
            this.labelColorMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColorMode.Location = new System.Drawing.Point(0, 325);
            this.labelColorMode.Name = "labelColorMode";
            this.labelColorMode.Size = new System.Drawing.Size(90, 20);
            this.labelColorMode.TabIndex = 224;
            this.labelColorMode.Text = "color mode";
            this.labelColorMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelCollisionDiameter2
            // 
            this.labelCollisionDiameter2.BackColor = System.Drawing.SystemColors.Control;
            this.labelCollisionDiameter2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCollisionDiameter2.Location = new System.Drawing.Point(153, 56);
            this.labelCollisionDiameter2.Name = "labelCollisionDiameter2";
            this.labelCollisionDiameter2.Size = new System.Drawing.Size(90, 20);
            this.labelCollisionDiameter2.TabIndex = 205;
            this.labelCollisionDiameter2.Text = "collision diam2";
            this.labelCollisionDiameter2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinCollisionDiameter2
            // 
            this.spinCollisionDiameter2.DataIndex = 0;
            this.spinCollisionDiameter2.DecimalPlaces = 3;
            this.spinCollisionDiameter2.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinCollisionDiameter2.Location = new System.Drawing.Point(245, 56);
            this.spinCollisionDiameter2.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.spinCollisionDiameter2.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147352576});
            this.spinCollisionDiameter2.Name = "spinCollisionDiameter2";
            this.spinCollisionDiameter2.Size = new System.Drawing.Size(52, 20);
            this.spinCollisionDiameter2.TabIndex = 204;
            this.spinCollisionDiameter2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.spinCollisionDiameter2.Value = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            // 
            // labelCollisionDiameter1
            // 
            this.labelCollisionDiameter1.BackColor = System.Drawing.SystemColors.Control;
            this.labelCollisionDiameter1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCollisionDiameter1.Location = new System.Drawing.Point(0, 56);
            this.labelCollisionDiameter1.Name = "labelCollisionDiameter1";
            this.labelCollisionDiameter1.Size = new System.Drawing.Size(90, 20);
            this.labelCollisionDiameter1.TabIndex = 203;
            this.labelCollisionDiameter1.Text = "collision diam1";
            this.labelCollisionDiameter1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinCollisionDiameter1
            // 
            this.spinCollisionDiameter1.DataIndex = 0;
            this.spinCollisionDiameter1.DecimalPlaces = 3;
            this.spinCollisionDiameter1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinCollisionDiameter1.Location = new System.Drawing.Point(92, 56);
            this.spinCollisionDiameter1.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.spinCollisionDiameter1.Name = "spinCollisionDiameter1";
            this.spinCollisionDiameter1.Size = new System.Drawing.Size(52, 20);
            this.spinCollisionDiameter1.TabIndex = 202;
            this.spinCollisionDiameter1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.spinCollisionDiameter1.Value = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            // 
            // labelScale
            // 
            this.labelScale.BackColor = System.Drawing.SystemColors.Control;
            this.labelScale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelScale.Location = new System.Drawing.Point(153, 4);
            this.labelScale.Name = "labelScale";
            this.labelScale.Size = new System.Drawing.Size(90, 20);
            this.labelScale.TabIndex = 199;
            this.labelScale.Text = "scale";
            this.labelScale.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinScale
            // 
            this.spinScale.DataIndex = 0;
            this.spinScale.DecimalPlaces = 3;
            this.spinScale.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            this.spinScale.Location = new System.Drawing.Point(245, 4);
            this.spinScale.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            this.spinScale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spinScale.Name = "spinScale";
            this.spinScale.Size = new System.Drawing.Size(52, 20);
            this.spinScale.TabIndex = 1;
            this.spinScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.spinScale.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // labelXOffset
            // 
            this.labelXOffset.BackColor = System.Drawing.SystemColors.Control;
            this.labelXOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelXOffset.Location = new System.Drawing.Point(0, 30);
            this.labelXOffset.Name = "labelXOffset";
            this.labelXOffset.Size = new System.Drawing.Size(90, 20);
            this.labelXOffset.TabIndex = 192;
            this.labelXOffset.Text = "x offset";
            this.labelXOffset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinDiameter
            // 
            this.spinDiameter.DataIndex = 0;
            this.spinDiameter.DecimalPlaces = 3;
            this.spinDiameter.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinDiameter.Location = new System.Drawing.Point(92, 4);
            this.spinDiameter.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.spinDiameter.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147352576});
            this.spinDiameter.Name = "spinDiameter";
            this.spinDiameter.Size = new System.Drawing.Size(52, 20);
            this.spinDiameter.TabIndex = 0;
            this.spinDiameter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // spinDiameterBrightness
            // 
            this.spinDiameterBrightness.DataIndex = 0;
            this.spinDiameterBrightness.DecimalPlaces = 3;
            this.spinDiameterBrightness.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinDiameterBrightness.Location = new System.Drawing.Point(245, 247);
            this.spinDiameterBrightness.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            131072});
            this.spinDiameterBrightness.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147352576});
            this.spinDiameterBrightness.Name = "spinDiameterBrightness";
            this.spinDiameterBrightness.Size = new System.Drawing.Size(52, 20);
            this.spinDiameterBrightness.TabIndex = 13;
            this.spinDiameterBrightness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // spinCenterBrightness
            // 
            this.spinCenterBrightness.DataIndex = 0;
            this.spinCenterBrightness.DecimalPlaces = 3;
            this.spinCenterBrightness.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinCenterBrightness.Location = new System.Drawing.Point(92, 247);
            this.spinCenterBrightness.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.spinCenterBrightness.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147352576});
            this.spinCenterBrightness.Name = "spinCenterBrightness";
            this.spinCenterBrightness.Size = new System.Drawing.Size(52, 20);
            this.spinCenterBrightness.TabIndex = 12;
            this.spinCenterBrightness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // spinOuterGlowPower
            // 
            this.spinOuterGlowPower.DataIndex = 0;
            this.spinOuterGlowPower.DecimalPlaces = 2;
            this.spinOuterGlowPower.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinOuterGlowPower.Location = new System.Drawing.Point(245, 273);
            this.spinOuterGlowPower.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            131072});
            this.spinOuterGlowPower.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147352576});
            this.spinOuterGlowPower.Name = "spinOuterGlowPower";
            this.spinOuterGlowPower.Size = new System.Drawing.Size(52, 20);
            this.spinOuterGlowPower.TabIndex = 15;
            this.spinOuterGlowPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // spinInnerGlowPower
            // 
            this.spinInnerGlowPower.DataIndex = 0;
            this.spinInnerGlowPower.DecimalPlaces = 2;
            this.spinInnerGlowPower.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.spinInnerGlowPower.Location = new System.Drawing.Point(92, 273);
            this.spinInnerGlowPower.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            131072});
            this.spinInnerGlowPower.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147352576});
            this.spinInnerGlowPower.Name = "spinInnerGlowPower";
            this.spinInnerGlowPower.Size = new System.Drawing.Size(52, 20);
            this.spinInnerGlowPower.TabIndex = 14;
            this.spinInnerGlowPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelDiameterBrightness
            // 
            this.labelDiameterBrightness.BackColor = System.Drawing.SystemColors.Control;
            this.labelDiameterBrightness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDiameterBrightness.Location = new System.Drawing.Point(153, 247);
            this.labelDiameterBrightness.Name = "labelDiameterBrightness";
            this.labelDiameterBrightness.Size = new System.Drawing.Size(90, 20);
            this.labelDiameterBrightness.TabIndex = 26;
            this.labelDiameterBrightness.Text = "diameter bright";
            this.labelDiameterBrightness.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelCenterBrightness
            // 
            this.labelCenterBrightness.BackColor = System.Drawing.SystemColors.Control;
            this.labelCenterBrightness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCenterBrightness.Location = new System.Drawing.Point(0, 247);
            this.labelCenterBrightness.Name = "labelCenterBrightness";
            this.labelCenterBrightness.Size = new System.Drawing.Size(90, 20);
            this.labelCenterBrightness.TabIndex = 25;
            this.labelCenterBrightness.Text = "center bright";
            this.labelCenterBrightness.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDiameter
            // 
            this.labelDiameter.BackColor = System.Drawing.SystemColors.Control;
            this.labelDiameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDiameter.Location = new System.Drawing.Point(0, 4);
            this.labelDiameter.Name = "labelDiameter";
            this.labelDiameter.Size = new System.Drawing.Size(90, 20);
            this.labelDiameter.TabIndex = 16;
            this.labelDiameter.Text = "diameter";
            this.labelDiameter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelOuterGlowPower
            // 
            this.labelOuterGlowPower.BackColor = System.Drawing.SystemColors.Control;
            this.labelOuterGlowPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelOuterGlowPower.Location = new System.Drawing.Point(153, 273);
            this.labelOuterGlowPower.Name = "labelOuterGlowPower";
            this.labelOuterGlowPower.Size = new System.Drawing.Size(90, 20);
            this.labelOuterGlowPower.TabIndex = 28;
            this.labelOuterGlowPower.Text = "outer glow power";
            this.labelOuterGlowPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelInnerGlowPower
            // 
            this.labelInnerGlowPower.BackColor = System.Drawing.SystemColors.Control;
            this.labelInnerGlowPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelInnerGlowPower.Location = new System.Drawing.Point(0, 273);
            this.labelInnerGlowPower.Name = "labelInnerGlowPower";
            this.labelInnerGlowPower.Size = new System.Drawing.Size(90, 20);
            this.labelInnerGlowPower.TabIndex = 27;
            this.labelInnerGlowPower.Text = "inner glow power";
            this.labelInnerGlowPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinRadialFreq
            // 
            this.spinRadialFreq.DataIndex = 0;
            this.spinRadialFreq.DecimalPlaces = 3;
            this.spinRadialFreq.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinRadialFreq.Location = new System.Drawing.Point(92, 171);
            this.spinRadialFreq.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            65536});
            this.spinRadialFreq.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147418112});
            this.spinRadialFreq.Name = "spinRadialFreq";
            this.spinRadialFreq.Size = new System.Drawing.Size(52, 20);
            this.spinRadialFreq.TabIndex = 8;
            this.spinRadialFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelRadialFreq
            // 
            this.labelRadialFreq.BackColor = System.Drawing.SystemColors.Control;
            this.labelRadialFreq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelRadialFreq.Location = new System.Drawing.Point(0, 171);
            this.labelRadialFreq.Name = "labelRadialFreq";
            this.labelRadialFreq.Size = new System.Drawing.Size(90, 20);
            this.labelRadialFreq.TabIndex = 23;
            this.labelRadialFreq.Text = "radial freq";
            this.labelRadialFreq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinRadialPhase
            // 
            this.spinRadialPhase.DataIndex = 0;
            this.spinRadialPhase.DecimalPlaces = 3;
            this.spinRadialPhase.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinRadialPhase.Location = new System.Drawing.Point(92, 221);
            this.spinRadialPhase.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.spinRadialPhase.Name = "spinRadialPhase";
            this.spinRadialPhase.Size = new System.Drawing.Size(52, 20);
            this.spinRadialPhase.TabIndex = 10;
            this.spinRadialPhase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelRadialPhase
            // 
            this.labelRadialPhase.BackColor = System.Drawing.SystemColors.Control;
            this.labelRadialPhase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelRadialPhase.Location = new System.Drawing.Point(0, 221);
            this.labelRadialPhase.Name = "labelRadialPhase";
            this.labelRadialPhase.Size = new System.Drawing.Size(90, 20);
            this.labelRadialPhase.TabIndex = 21;
            this.labelRadialPhase.Text = "radial phase";
            this.labelRadialPhase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinRadialAmp
            // 
            this.spinRadialAmp.DataIndex = 0;
            this.spinRadialAmp.DecimalPlaces = 3;
            this.spinRadialAmp.Increment = new decimal(new int[] {
            3,
            0,
            0,
            196608});
            this.spinRadialAmp.Location = new System.Drawing.Point(92, 146);
            this.spinRadialAmp.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            196608});
            this.spinRadialAmp.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147287040});
            this.spinRadialAmp.Name = "spinRadialAmp";
            this.spinRadialAmp.Size = new System.Drawing.Size(52, 20);
            this.spinRadialAmp.TabIndex = 6;
            this.spinRadialAmp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelRadialAmp
            // 
            this.labelRadialAmp.BackColor = System.Drawing.SystemColors.Control;
            this.labelRadialAmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelRadialAmp.Location = new System.Drawing.Point(0, 146);
            this.labelRadialAmp.Name = "labelRadialAmp";
            this.labelRadialAmp.Size = new System.Drawing.Size(90, 20);
            this.labelRadialAmp.TabIndex = 19;
            this.labelRadialAmp.Text = "radial amp";
            this.labelRadialAmp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelRadialPower
            // 
            this.labelRadialPower.BackColor = System.Drawing.SystemColors.Control;
            this.labelRadialPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelRadialPower.Location = new System.Drawing.Point(0, 196);
            this.labelRadialPower.Name = "labelRadialPower";
            this.labelRadialPower.Size = new System.Drawing.Size(90, 20);
            this.labelRadialPower.TabIndex = 196;
            this.labelRadialPower.Text = "radial power";
            this.labelRadialPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinPolarAmp
            // 
            this.spinPolarAmp.DataIndex = 0;
            this.spinPolarAmp.DecimalPlaces = 3;
            this.spinPolarAmp.Increment = new decimal(new int[] {
            3,
            0,
            0,
            196608});
            this.spinPolarAmp.Location = new System.Drawing.Point(245, 146);
            this.spinPolarAmp.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            196608});
            this.spinPolarAmp.Minimum = new decimal(new int[] {
            20000,
            0,
            0,
            -2147287040});
            this.spinPolarAmp.Name = "spinPolarAmp";
            this.spinPolarAmp.Size = new System.Drawing.Size(52, 20);
            this.spinPolarAmp.TabIndex = 7;
            this.spinPolarAmp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelPolarPower
            // 
            this.labelPolarPower.BackColor = System.Drawing.SystemColors.Control;
            this.labelPolarPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPolarPower.Location = new System.Drawing.Point(153, 196);
            this.labelPolarPower.Name = "labelPolarPower";
            this.labelPolarPower.Size = new System.Drawing.Size(90, 20);
            this.labelPolarPower.TabIndex = 197;
            this.labelPolarPower.Text = "polar power";
            this.labelPolarPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPolarAmp
            // 
            this.labelPolarAmp.BackColor = System.Drawing.SystemColors.Control;
            this.labelPolarAmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPolarAmp.Location = new System.Drawing.Point(153, 146);
            this.labelPolarAmp.Name = "labelPolarAmp";
            this.labelPolarAmp.Size = new System.Drawing.Size(90, 20);
            this.labelPolarAmp.TabIndex = 20;
            this.labelPolarAmp.Text = "polar amp";
            this.labelPolarAmp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinRadialPower
            // 
            this.spinRadialPower.DataIndex = 0;
            this.spinRadialPower.DecimalPlaces = 2;
            this.spinRadialPower.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            this.spinRadialPower.Location = new System.Drawing.Point(92, 196);
            this.spinRadialPower.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.spinRadialPower.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147352576});
            this.spinRadialPower.Name = "spinRadialPower";
            this.spinRadialPower.Size = new System.Drawing.Size(52, 20);
            this.spinRadialPower.TabIndex = 4;
            this.spinRadialPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // spinPolarPhase
            // 
            this.spinPolarPhase.DataIndex = 0;
            this.spinPolarPhase.DecimalPlaces = 3;
            this.spinPolarPhase.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinPolarPhase.Location = new System.Drawing.Point(245, 221);
            this.spinPolarPhase.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.spinPolarPhase.Name = "spinPolarPhase";
            this.spinPolarPhase.Size = new System.Drawing.Size(52, 20);
            this.spinPolarPhase.TabIndex = 11;
            this.spinPolarPhase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // spinPolarPower
            // 
            this.spinPolarPower.DataIndex = 0;
            this.spinPolarPower.DecimalPlaces = 2;
            this.spinPolarPower.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            this.spinPolarPower.Location = new System.Drawing.Point(245, 196);
            this.spinPolarPower.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            131072});
            this.spinPolarPower.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147352576});
            this.spinPolarPower.Name = "spinPolarPower";
            this.spinPolarPower.Size = new System.Drawing.Size(52, 20);
            this.spinPolarPower.TabIndex = 5;
            this.spinPolarPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelPolarPhase
            // 
            this.labelPolarPhase.BackColor = System.Drawing.SystemColors.Control;
            this.labelPolarPhase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPolarPhase.Location = new System.Drawing.Point(153, 221);
            this.labelPolarPhase.Name = "labelPolarPhase";
            this.labelPolarPhase.Size = new System.Drawing.Size(90, 20);
            this.labelPolarPhase.TabIndex = 22;
            this.labelPolarPhase.Text = "polar phase";
            this.labelPolarPhase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinPolarFreq
            // 
            this.spinPolarFreq.DataIndex = 0;
            this.spinPolarFreq.DecimalPlaces = 3;
            this.spinPolarFreq.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinPolarFreq.Location = new System.Drawing.Point(245, 171);
            this.spinPolarFreq.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.spinPolarFreq.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147483648});
            this.spinPolarFreq.Name = "spinPolarFreq";
            this.spinPolarFreq.Size = new System.Drawing.Size(52, 20);
            this.spinPolarFreq.TabIndex = 9;
            this.spinPolarFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelYOffset
            // 
            this.labelYOffset.BackColor = System.Drawing.SystemColors.Control;
            this.labelYOffset.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelYOffset.Location = new System.Drawing.Point(153, 30);
            this.labelYOffset.Name = "labelYOffset";
            this.labelYOffset.Size = new System.Drawing.Size(90, 20);
            this.labelYOffset.TabIndex = 193;
            this.labelYOffset.Text = "y offset";
            this.labelYOffset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPolarFreq
            // 
            this.labelPolarFreq.BackColor = System.Drawing.SystemColors.Control;
            this.labelPolarFreq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelPolarFreq.Location = new System.Drawing.Point(153, 171);
            this.labelPolarFreq.Name = "labelPolarFreq";
            this.labelPolarFreq.Size = new System.Drawing.Size(90, 20);
            this.labelPolarFreq.TabIndex = 24;
            this.labelPolarFreq.Text = "polar freq";
            this.labelPolarFreq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinXOffset
            // 
            this.spinXOffset.DataIndex = 0;
            this.spinXOffset.DecimalPlaces = 3;
            this.spinXOffset.Increment = new decimal(new int[] {
            2,
            0,
            0,
            196608});
            this.spinXOffset.Location = new System.Drawing.Point(92, 30);
            this.spinXOffset.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.spinXOffset.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147352576});
            this.spinXOffset.Name = "spinXOffset";
            this.spinXOffset.Size = new System.Drawing.Size(52, 20);
            this.spinXOffset.TabIndex = 2;
            this.spinXOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // spinCenterRed
            // 
            this.spinCenterRed.DataIndex = 0;
            this.spinCenterRed.DecimalPlaces = 3;
            this.spinCenterRed.Increment = new decimal(new int[] {
            3,
            0,
            0,
            196608});
            this.spinCenterRed.Location = new System.Drawing.Point(92, 350);
            this.spinCenterRed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            196608});
            this.spinCenterRed.Name = "spinCenterRed";
            this.spinCenterRed.Size = new System.Drawing.Size(52, 20);
            this.spinCenterRed.TabIndex = 16;
            this.spinCenterRed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // spinYOffset
            // 
            this.spinYOffset.DataIndex = 0;
            this.spinYOffset.DecimalPlaces = 3;
            this.spinYOffset.Increment = new decimal(new int[] {
            2,
            0,
            0,
            196608});
            this.spinYOffset.Location = new System.Drawing.Point(245, 30);
            this.spinYOffset.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            131072});
            this.spinYOffset.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            -2147352576});
            this.spinYOffset.Name = "spinYOffset";
            this.spinYOffset.Size = new System.Drawing.Size(52, 20);
            this.spinYOffset.TabIndex = 3;
            this.spinYOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCenterRed
            // 
            this.labelCenterRed.BackColor = System.Drawing.SystemColors.Control;
            this.labelCenterRed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCenterRed.Location = new System.Drawing.Point(0, 350);
            this.labelCenterRed.Name = "labelCenterRed";
            this.labelCenterRed.Size = new System.Drawing.Size(90, 20);
            this.labelCenterRed.TabIndex = 155;
            this.labelCenterRed.Text = "center red";
            this.labelCenterRed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinCenterBlue
            // 
            this.spinCenterBlue.DataIndex = 0;
            this.spinCenterBlue.DecimalPlaces = 3;
            this.spinCenterBlue.Increment = new decimal(new int[] {
            3,
            0,
            0,
            196608});
            this.spinCenterBlue.Location = new System.Drawing.Point(92, 402);
            this.spinCenterBlue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            196608});
            this.spinCenterBlue.Name = "spinCenterBlue";
            this.spinCenterBlue.Size = new System.Drawing.Size(52, 20);
            this.spinCenterBlue.TabIndex = 20;
            this.spinCenterBlue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCenterBlue
            // 
            this.labelCenterBlue.BackColor = System.Drawing.SystemColors.Control;
            this.labelCenterBlue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCenterBlue.Location = new System.Drawing.Point(0, 402);
            this.labelCenterBlue.Name = "labelCenterBlue";
            this.labelCenterBlue.Size = new System.Drawing.Size(90, 20);
            this.labelCenterBlue.TabIndex = 157;
            this.labelCenterBlue.Text = "center blue";
            this.labelCenterBlue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinCenterGreen
            // 
            this.spinCenterGreen.DataIndex = 0;
            this.spinCenterGreen.DecimalPlaces = 3;
            this.spinCenterGreen.Increment = new decimal(new int[] {
            3,
            0,
            0,
            196608});
            this.spinCenterGreen.Location = new System.Drawing.Point(92, 376);
            this.spinCenterGreen.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            196608});
            this.spinCenterGreen.Name = "spinCenterGreen";
            this.spinCenterGreen.Size = new System.Drawing.Size(52, 20);
            this.spinCenterGreen.TabIndex = 18;
            this.spinCenterGreen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCenterGreen
            // 
            this.labelCenterGreen.BackColor = System.Drawing.SystemColors.Control;
            this.labelCenterGreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCenterGreen.Location = new System.Drawing.Point(0, 376);
            this.labelCenterGreen.Name = "labelCenterGreen";
            this.labelCenterGreen.Size = new System.Drawing.Size(90, 20);
            this.labelCenterGreen.TabIndex = 159;
            this.labelCenterGreen.Text = "center green";
            this.labelCenterGreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinDiameterRed
            // 
            this.spinDiameterRed.DataIndex = 0;
            this.spinDiameterRed.DecimalPlaces = 3;
            this.spinDiameterRed.Increment = new decimal(new int[] {
            3,
            0,
            0,
            196608});
            this.spinDiameterRed.Location = new System.Drawing.Point(245, 350);
            this.spinDiameterRed.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            196608});
            this.spinDiameterRed.Name = "spinDiameterRed";
            this.spinDiameterRed.Size = new System.Drawing.Size(52, 20);
            this.spinDiameterRed.TabIndex = 17;
            this.spinDiameterRed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelDiameterRed
            // 
            this.labelDiameterRed.BackColor = System.Drawing.SystemColors.Control;
            this.labelDiameterRed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDiameterRed.Location = new System.Drawing.Point(153, 350);
            this.labelDiameterRed.Name = "labelDiameterRed";
            this.labelDiameterRed.Size = new System.Drawing.Size(90, 20);
            this.labelDiameterRed.TabIndex = 161;
            this.labelDiameterRed.Text = "diameter red";
            this.labelDiameterRed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinDiameterBlue
            // 
            this.spinDiameterBlue.DataIndex = 0;
            this.spinDiameterBlue.DecimalPlaces = 3;
            this.spinDiameterBlue.Increment = new decimal(new int[] {
            3,
            0,
            0,
            196608});
            this.spinDiameterBlue.Location = new System.Drawing.Point(245, 402);
            this.spinDiameterBlue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            196608});
            this.spinDiameterBlue.Name = "spinDiameterBlue";
            this.spinDiameterBlue.Size = new System.Drawing.Size(52, 20);
            this.spinDiameterBlue.TabIndex = 21;
            this.spinDiameterBlue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelDiameterBlue
            // 
            this.labelDiameterBlue.BackColor = System.Drawing.SystemColors.Control;
            this.labelDiameterBlue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDiameterBlue.Location = new System.Drawing.Point(153, 402);
            this.labelDiameterBlue.Name = "labelDiameterBlue";
            this.labelDiameterBlue.Size = new System.Drawing.Size(90, 20);
            this.labelDiameterBlue.TabIndex = 163;
            this.labelDiameterBlue.Text = "diameter blue";
            this.labelDiameterBlue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinDiameterGreen
            // 
            this.spinDiameterGreen.DataIndex = 0;
            this.spinDiameterGreen.DecimalPlaces = 3;
            this.spinDiameterGreen.Increment = new decimal(new int[] {
            3,
            0,
            0,
            196608});
            this.spinDiameterGreen.Location = new System.Drawing.Point(245, 376);
            this.spinDiameterGreen.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            196608});
            this.spinDiameterGreen.Name = "spinDiameterGreen";
            this.spinDiameterGreen.Size = new System.Drawing.Size(52, 20);
            this.spinDiameterGreen.TabIndex = 19;
            this.spinDiameterGreen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelDiameterGreen
            // 
            this.labelDiameterGreen.BackColor = System.Drawing.SystemColors.Control;
            this.labelDiameterGreen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDiameterGreen.Location = new System.Drawing.Point(153, 376);
            this.labelDiameterGreen.Name = "labelDiameterGreen";
            this.labelDiameterGreen.Size = new System.Drawing.Size(90, 20);
            this.labelDiameterGreen.TabIndex = 165;
            this.labelDiameterGreen.Text = "diameter green";
            this.labelDiameterGreen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelSwirlModeText
            // 
            this.labelSwirlModeText.BackColor = System.Drawing.Color.White;
            this.labelSwirlModeText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSwirlModeText.Location = new System.Drawing.Point(153, 1);
            this.labelSwirlModeText.Name = "labelSwirlModeText";
            this.labelSwirlModeText.Size = new System.Drawing.Size(144, 20);
            this.labelSwirlModeText.TabIndex = 224;
            this.labelSwirlModeText.Text = "swirl mode text";
            this.labelSwirlModeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMirrorModeText
            // 
            this.labelMirrorModeText.BackColor = System.Drawing.Color.White;
            this.labelMirrorModeText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMirrorModeText.Location = new System.Drawing.Point(0, 1);
            this.labelMirrorModeText.Name = "labelMirrorModeText";
            this.labelMirrorModeText.Size = new System.Drawing.Size(144, 20);
            this.labelMirrorModeText.TabIndex = 223;
            this.labelMirrorModeText.Text = "mirror mode text";
            this.labelMirrorModeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMorphModeText
            // 
            this.labelMorphModeText.BackColor = System.Drawing.Color.White;
            this.labelMorphModeText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMorphModeText.Location = new System.Drawing.Point(0, 141);
            this.labelMorphModeText.Name = "labelMorphModeText";
            this.labelMorphModeText.Size = new System.Drawing.Size(144, 20);
            this.labelMorphModeText.TabIndex = 222;
            this.labelMorphModeText.Text = "morph mode text";
            this.labelMorphModeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // spinSwirlMode
            // 
            this.spinSwirlMode.DataIndex = 0;
            this.spinSwirlMode.Location = new System.Drawing.Point(245, 23);
            this.spinSwirlMode.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinSwirlMode.Name = "spinSwirlMode";
            this.spinSwirlMode.Size = new System.Drawing.Size(52, 20);
            this.spinSwirlMode.TabIndex = 210;
            this.spinSwirlMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelSwirlMode
            // 
            this.labelSwirlMode.BackColor = System.Drawing.SystemColors.Control;
            this.labelSwirlMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSwirlMode.Location = new System.Drawing.Point(153, 23);
            this.labelSwirlMode.Name = "labelSwirlMode";
            this.labelSwirlMode.Size = new System.Drawing.Size(90, 20);
            this.labelSwirlMode.TabIndex = 213;
            this.labelSwirlMode.Text = "swirl mode";
            this.labelSwirlMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinMirrorMode
            // 
            this.spinMirrorMode.DataIndex = 0;
            this.spinMirrorMode.Location = new System.Drawing.Point(92, 23);
            this.spinMirrorMode.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.spinMirrorMode.Name = "spinMirrorMode";
            this.spinMirrorMode.Size = new System.Drawing.Size(52, 20);
            this.spinMirrorMode.TabIndex = 212;
            this.spinMirrorMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMirrorMode
            // 
            this.labelMirrorMode.BackColor = System.Drawing.SystemColors.Control;
            this.labelMirrorMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMirrorMode.Location = new System.Drawing.Point(0, 23);
            this.labelMirrorMode.Name = "labelMirrorMode";
            this.labelMirrorMode.Size = new System.Drawing.Size(90, 20);
            this.labelMirrorMode.TabIndex = 214;
            this.labelMirrorMode.Text = "mirror mode";
            this.labelMirrorMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinMorphMode
            // 
            this.spinMorphMode.DataIndex = 0;
            this.spinMorphMode.Location = new System.Drawing.Point(92, 163);
            this.spinMorphMode.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.spinMorphMode.Name = "spinMorphMode";
            this.spinMorphMode.Size = new System.Drawing.Size(52, 20);
            this.spinMorphMode.TabIndex = 211;
            this.spinMorphMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMorphMode
            // 
            this.labelMorphMode.BackColor = System.Drawing.SystemColors.Control;
            this.labelMorphMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMorphMode.Location = new System.Drawing.Point(0, 163);
            this.labelMorphMode.Name = "labelMorphMode";
            this.labelMorphMode.Size = new System.Drawing.Size(90, 20);
            this.labelMorphMode.TabIndex = 215;
            this.labelMorphMode.Text = "morph mode";
            this.labelMorphMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinCircularStretchX
            // 
            this.spinCircularStretchX.DataIndex = 0;
            this.spinCircularStretchX.DecimalPlaces = 3;
            this.spinCircularStretchX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spinCircularStretchX.Location = new System.Drawing.Point(245, 84);
            this.spinCircularStretchX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147352576});
            this.spinCircularStretchX.Name = "spinCircularStretchX";
            this.spinCircularStretchX.Size = new System.Drawing.Size(52, 20);
            this.spinCircularStretchX.TabIndex = 208;
            this.spinCircularStretchX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCircularStretchX
            // 
            this.labelCircularStretchX.BackColor = System.Drawing.SystemColors.Control;
            this.labelCircularStretchX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCircularStretchX.Location = new System.Drawing.Point(153, 84);
            this.labelCircularStretchX.Name = "labelCircularStretchX";
            this.labelCircularStretchX.Size = new System.Drawing.Size(90, 20);
            this.labelCircularStretchX.TabIndex = 209;
            this.labelCircularStretchX.Text = "circular stretch x";
            this.labelCircularStretchX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinLinearStretchX
            // 
            this.spinLinearStretchX.DataIndex = 0;
            this.spinLinearStretchX.DecimalPlaces = 3;
            this.spinLinearStretchX.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinLinearStretchX.Location = new System.Drawing.Point(92, 84);
            this.spinLinearStretchX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            131072});
            this.spinLinearStretchX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147352576});
            this.spinLinearStretchX.Name = "spinLinearStretchX";
            this.spinLinearStretchX.Size = new System.Drawing.Size(52, 20);
            this.spinLinearStretchX.TabIndex = 206;
            this.spinLinearStretchX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelLinearStretchX
            // 
            this.labelLinearStretchX.BackColor = System.Drawing.SystemColors.Control;
            this.labelLinearStretchX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLinearStretchX.Location = new System.Drawing.Point(0, 84);
            this.labelLinearStretchX.Name = "labelLinearStretchX";
            this.labelLinearStretchX.Size = new System.Drawing.Size(90, 20);
            this.labelLinearStretchX.TabIndex = 207;
            this.labelLinearStretchX.Text = "linear stretch x";
            this.labelLinearStretchX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinMorphPhase
            // 
            this.spinMorphPhase.DataIndex = 0;
            this.spinMorphPhase.DecimalPlaces = 3;
            this.spinMorphPhase.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinMorphPhase.Location = new System.Drawing.Point(92, 267);
            this.spinMorphPhase.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.spinMorphPhase.Name = "spinMorphPhase";
            this.spinMorphPhase.Size = new System.Drawing.Size(52, 20);
            this.spinMorphPhase.TabIndex = 204;
            this.spinMorphPhase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMorphPhase
            // 
            this.labelMorphPhase.BackColor = System.Drawing.SystemColors.Control;
            this.labelMorphPhase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMorphPhase.Location = new System.Drawing.Point(0, 267);
            this.labelMorphPhase.Name = "labelMorphPhase";
            this.labelMorphPhase.Size = new System.Drawing.Size(90, 20);
            this.labelMorphPhase.TabIndex = 205;
            this.labelMorphPhase.Text = "morph phase";
            this.labelMorphPhase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinRotateAngle
            // 
            this.spinRotateAngle.DataIndex = 0;
            this.spinRotateAngle.DecimalPlaces = 3;
            this.spinRotateAngle.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinRotateAngle.Location = new System.Drawing.Point(92, 48);
            this.spinRotateAngle.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147352576});
            this.spinRotateAngle.Name = "spinRotateAngle";
            this.spinRotateAngle.Size = new System.Drawing.Size(52, 20);
            this.spinRotateAngle.TabIndex = 202;
            this.spinRotateAngle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelRotateAngle
            // 
            this.labelRotateAngle.BackColor = System.Drawing.SystemColors.Control;
            this.labelRotateAngle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelRotateAngle.Location = new System.Drawing.Point(0, 48);
            this.labelRotateAngle.Name = "labelRotateAngle";
            this.labelRotateAngle.Size = new System.Drawing.Size(90, 20);
            this.labelRotateAngle.TabIndex = 203;
            this.labelRotateAngle.Text = "rotate angle";
            this.labelRotateAngle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinSwirl
            // 
            this.spinSwirl.DataIndex = 0;
            this.spinSwirl.DecimalPlaces = 3;
            this.spinSwirl.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinSwirl.Location = new System.Drawing.Point(245, 48);
            this.spinSwirl.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            131072});
            this.spinSwirl.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147352576});
            this.spinSwirl.Name = "spinSwirl";
            this.spinSwirl.Size = new System.Drawing.Size(52, 20);
            this.spinSwirl.TabIndex = 27;
            this.spinSwirl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelSwirl
            // 
            this.labelSwirl.BackColor = System.Drawing.SystemColors.Control;
            this.labelSwirl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSwirl.Location = new System.Drawing.Point(153, 48);
            this.labelSwirl.Name = "labelSwirl";
            this.labelSwirl.Size = new System.Drawing.Size(90, 20);
            this.labelSwirl.TabIndex = 28;
            this.labelSwirl.Text = "swirl";
            this.labelSwirl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinCircularStretchY
            // 
            this.spinCircularStretchY.DataIndex = 0;
            this.spinCircularStretchY.DecimalPlaces = 3;
            this.spinCircularStretchY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.spinCircularStretchY.Location = new System.Drawing.Point(245, 108);
            this.spinCircularStretchY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147352576});
            this.spinCircularStretchY.Name = "spinCircularStretchY";
            this.spinCircularStretchY.Size = new System.Drawing.Size(52, 20);
            this.spinCircularStretchY.TabIndex = 25;
            this.spinCircularStretchY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelCircularStretchY
            // 
            this.labelCircularStretchY.BackColor = System.Drawing.SystemColors.Control;
            this.labelCircularStretchY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCircularStretchY.Location = new System.Drawing.Point(153, 108);
            this.labelCircularStretchY.Name = "labelCircularStretchY";
            this.labelCircularStretchY.Size = new System.Drawing.Size(90, 20);
            this.labelCircularStretchY.TabIndex = 26;
            this.labelCircularStretchY.Text = "circular stretch y";
            this.labelCircularStretchY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinMorphPower
            // 
            this.spinMorphPower.DataIndex = 0;
            this.spinMorphPower.DecimalPlaces = 3;
            this.spinMorphPower.Increment = new decimal(new int[] {
            2,
            0,
            0,
            131072});
            this.spinMorphPower.Location = new System.Drawing.Point(92, 241);
            this.spinMorphPower.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            131072});
            this.spinMorphPower.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147352576});
            this.spinMorphPower.Name = "spinMorphPower";
            this.spinMorphPower.Size = new System.Drawing.Size(52, 20);
            this.spinMorphPower.TabIndex = 23;
            this.spinMorphPower.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMorphPower
            // 
            this.labelMorphPower.BackColor = System.Drawing.SystemColors.Control;
            this.labelMorphPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMorphPower.Location = new System.Drawing.Point(0, 241);
            this.labelMorphPower.Name = "labelMorphPower";
            this.labelMorphPower.Size = new System.Drawing.Size(90, 20);
            this.labelMorphPower.TabIndex = 24;
            this.labelMorphPower.Text = "morph power";
            this.labelMorphPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinMorphFreq
            // 
            this.spinMorphFreq.DataIndex = 0;
            this.spinMorphFreq.DecimalPlaces = 3;
            this.spinMorphFreq.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinMorphFreq.Location = new System.Drawing.Point(92, 215);
            this.spinMorphFreq.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            131072});
            this.spinMorphFreq.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            -2147352576});
            this.spinMorphFreq.Name = "spinMorphFreq";
            this.spinMorphFreq.Size = new System.Drawing.Size(52, 20);
            this.spinMorphFreq.TabIndex = 21;
            this.spinMorphFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMorphFreq
            // 
            this.labelMorphFreq.BackColor = System.Drawing.SystemColors.Control;
            this.labelMorphFreq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMorphFreq.Location = new System.Drawing.Point(0, 215);
            this.labelMorphFreq.Name = "labelMorphFreq";
            this.labelMorphFreq.Size = new System.Drawing.Size(90, 20);
            this.labelMorphFreq.TabIndex = 22;
            this.labelMorphFreq.Text = "morph freq";
            this.labelMorphFreq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinMorphAmp
            // 
            this.spinMorphAmp.DataIndex = 0;
            this.spinMorphAmp.DecimalPlaces = 3;
            this.spinMorphAmp.Increment = new decimal(new int[] {
            3,
            0,
            0,
            196608});
            this.spinMorphAmp.Location = new System.Drawing.Point(92, 189);
            this.spinMorphAmp.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            131072});
            this.spinMorphAmp.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147352576});
            this.spinMorphAmp.Name = "spinMorphAmp";
            this.spinMorphAmp.Size = new System.Drawing.Size(52, 20);
            this.spinMorphAmp.TabIndex = 19;
            this.spinMorphAmp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMorphAmp
            // 
            this.labelMorphAmp.BackColor = System.Drawing.SystemColors.Control;
            this.labelMorphAmp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelMorphAmp.Location = new System.Drawing.Point(0, 189);
            this.labelMorphAmp.Name = "labelMorphAmp";
            this.labelMorphAmp.Size = new System.Drawing.Size(90, 20);
            this.labelMorphAmp.TabIndex = 20;
            this.labelMorphAmp.Text = "morph amp";
            this.labelMorphAmp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinLinearStretchY
            // 
            this.spinLinearStretchY.DataIndex = 0;
            this.spinLinearStretchY.DecimalPlaces = 3;
            this.spinLinearStretchY.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.spinLinearStretchY.Location = new System.Drawing.Point(92, 108);
            this.spinLinearStretchY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            131072});
            this.spinLinearStretchY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147352576});
            this.spinLinearStretchY.Name = "spinLinearStretchY";
            this.spinLinearStretchY.Size = new System.Drawing.Size(52, 20);
            this.spinLinearStretchY.TabIndex = 17;
            this.spinLinearStretchY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelLinearStretchY
            // 
            this.labelLinearStretchY.BackColor = System.Drawing.SystemColors.Control;
            this.labelLinearStretchY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLinearStretchY.Location = new System.Drawing.Point(0, 108);
            this.labelLinearStretchY.Name = "labelLinearStretchY";
            this.labelLinearStretchY.Size = new System.Drawing.Size(90, 20);
            this.labelLinearStretchY.TabIndex = 18;
            this.labelLinearStretchY.Text = "linear stretch y";
            this.labelLinearStretchY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // yEditRandomParamControl
            // 
            this.yEditRandomParamControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yEditRandomParamControl.Location = new System.Drawing.Point(1006, 81);
            this.yEditRandomParamControl.Name = "yEditRandomParamControl";
            this.yEditRandomParamControl.Size = new System.Drawing.Size(258, 78);
            this.yEditRandomParamControl.TabIndex = 11;
            // 
            // xEditRandomParamControl
            // 
            this.xEditRandomParamControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xEditRandomParamControl.Location = new System.Drawing.Point(1006, 4);
            this.xEditRandomParamControl.Name = "xEditRandomParamControl";
            this.xEditRandomParamControl.Size = new System.Drawing.Size(258, 78);
            this.xEditRandomParamControl.TabIndex = 10;
            // 
            // focusedRandomParamControl
            // 
            this.focusedRandomParamControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.focusedRandomParamControl.Location = new System.Drawing.Point(738, 4);
            this.focusedRandomParamControl.Name = "focusedRandomParamControl";
            this.focusedRandomParamControl.Size = new System.Drawing.Size(258, 78);
            this.focusedRandomParamControl.TabIndex = 9;
            // 
            // labelSize
            // 
            this.labelSize.BackColor = System.Drawing.SystemColors.Control;
            this.labelSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSize.Location = new System.Drawing.Point(740, 190);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(70, 20);
            this.labelSize.TabIndex = 0;
            this.labelSize.Text = "size";
            this.labelSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spinSize
            // 
            this.spinSize.DataIndex = 0;
            this.spinSize.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.spinSize.Location = new System.Drawing.Point(814, 190);
            this.spinSize.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.spinSize.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.spinSize.Name = "spinSize";
            this.spinSize.Size = new System.Drawing.Size(58, 20);
            this.spinSize.TabIndex = 0;
            this.spinSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.spinSize.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 732);
            this.Controls.Add(this.checkRotateImage180);
            this.Controls.Add(this.buttonPNG);
            this.Controls.Add(this.buttonFindReplaceTexture);
            this.Controls.Add(this.buttonExportCollectionTextureData);
            this.Controls.Add(this.buttonPasteTexture);
            this.Controls.Add(this.buttonCopyTexture);
            this.Controls.Add(this.buttonPasteLayer);
            this.Controls.Add(this.buttonCopyLayer);
            this.Controls.Add(this.labelFast);
            this.Controls.Add(this.checkApplyToAllParams);
            this.Controls.Add(this.checkGenerateClip);
            this.Controls.Add(this.checkShowCollider);
            this.Controls.Add(this.labelFramesPerSecond);
            this.Controls.Add(this.spinFramesPerSecond);
            this.Controls.Add(this.checkLoop);
            this.Controls.Add(this.buttonSetValueToRightLimit);
            this.Controls.Add(this.buttonSetRightLimitToValue);
            this.Controls.Add(this.buttonSetValueToLeftLimit);
            this.Controls.Add(this.labelClipTime);
            this.Controls.Add(this.labelClipTimeLabel);
            this.Controls.Add(this.buttonSetLeftLimitToValue);
            this.Controls.Add(this.labelClipDuration);
            this.Controls.Add(this.spinClipDuration);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.checkViewAllLayers);
            this.Controls.Add(this.layerParameterTabs);
            this.Controls.Add(this.labelTextureCollections);
            this.Controls.Add(this.buttonDeleteCollection);
            this.Controls.Add(this.buttonAddCollection);
            this.Controls.Add(this.buttonMoveCollectionUp);
            this.Controls.Add(this.buttonMoveCollectionDown);
            this.Controls.Add(this.buttonCloneCollection);
            this.Controls.Add(this.collectionList);
            this.Controls.Add(this.buttonRandomize);
            this.Controls.Add(this.buttonExportAllTextureData);
            this.Controls.Add(this.yEditRandomParamControl);
            this.Controls.Add(this.xEditRandomParamControl);
            this.Controls.Add(this.focusedRandomParamControl);
            this.Controls.Add(this.levelParamEditModeGroup);
            this.Controls.Add(this.labelOverflow);
            this.Controls.Add(this.labelLayers);
            this.Controls.Add(this.labelTextures);
            this.Controls.Add(this.labelEditLayer);
            this.Controls.Add(this.checkEditRelative);
            this.Controls.Add(this.checkMultiEdit);
            this.Controls.Add(this.layerList);
            this.Controls.Add(this.buttonDeleteLayer);
            this.Controls.Add(this.buttonAddLayer);
            this.Controls.Add(this.buttonMoveLayerUp);
            this.Controls.Add(this.buttonMoveLayerDown);
            this.Controls.Add(this.buttonDeleteTexture);
            this.Controls.Add(this.buttonAddTexture);
            this.Controls.Add(this.buttonMoveTextureUp);
            this.Controls.Add(this.buttonMoveTextureDown);
            this.Controls.Add(this.textureList);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.spinSize);
            this.Controls.Add(this.mainPictureBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Texture Create";
            this.Shown += new System.EventHandler(this.mainForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.levelParamEditModeGroup.ResumeLayout(false);
            this.levelParamEditModeGroup.PerformLayout();
            this.layerParameterTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinFramesPerSecond)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinClipDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinAnimationPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinColliderRotateAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinColliderXOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinColliderYOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinColorMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCollisionDiameter2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCollisionDiameter1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDiameter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDiameterBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCenterBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOuterGlowPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinInnerGlowPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRadialFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRadialPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRadialAmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPolarAmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRadialPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPolarPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPolarPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinPolarFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinXOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCenterRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinYOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCenterBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCenterGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDiameterRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDiameterBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinDiameterGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSwirlMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMirrorMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMorphMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCircularStretchX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLinearStretchX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMorphPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinRotateAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSwirl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinCircularStretchY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMorphPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMorphFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinMorphAmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinLinearStretchY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox mainPictureBox;
        private customUpDown spinDiameter;
        private customUpDown spinDiameterBrightness;
        private customUpDown spinCenterBrightness;
        private customUpDown spinOuterGlowPower;
        private customUpDown spinInnerGlowPower;
        private customUpDown spinSize;
        private customLabel labelSize;
        private customLabel labelDiameterBrightness;
        private customLabel labelCenterBrightness;
        private customLabel labelDiameter;
        private customLabel labelInnerGlowPower;
        private customLabel labelOuterGlowPower;
        private customLabel labelRadialFreq;
        private customUpDown spinRadialFreq;
        private customLabel labelRadialPhase;
        private customUpDown spinRadialPhase;
        private customLabel labelRadialAmp;
        private customUpDown spinRadialAmp;
        private customLabel labelPolarAmp;
        private customUpDown spinPolarAmp;
        private customLabel labelPolarPhase;
        private customUpDown spinPolarPhase;
        private customLabel labelPolarFreq;
        private customUpDown spinPolarFreq;
        private customLabel labelCenterRed;
        private customUpDown spinCenterRed;
        private customLabel labelCenterBlue;
        private customUpDown spinCenterBlue;
        private customLabel labelCenterGreen;
        private customUpDown spinCenterGreen;
        private customLabel labelDiameterGreen;
        private customUpDown spinDiameterGreen;
        private customLabel labelDiameterBlue;
        private customUpDown spinDiameterBlue;
        private customLabel labelDiameterRed;
        private customUpDown spinDiameterRed;
        private System.Windows.Forms.ListBox textureList;
        internal System.Windows.Forms.Button buttonDeleteTexture;
        internal System.Windows.Forms.Button buttonAddTexture;
        internal System.Windows.Forms.Button buttonMoveTextureUp;
        internal System.Windows.Forms.Button buttonMoveTextureDown;
        internal System.Windows.Forms.Button buttonDeleteLayer;
        internal System.Windows.Forms.Button buttonAddLayer;
        internal System.Windows.Forms.Button buttonMoveLayerUp;
        internal System.Windows.Forms.Button buttonMoveLayerDown;
        private System.Windows.Forms.ListBox layerList;
        private System.Windows.Forms.CheckBox checkMultiEdit;
        private System.Windows.Forms.CheckBox checkEditRelative;
        private System.Windows.Forms.Label labelEditLayer;
        private System.Windows.Forms.Label labelTextures;
        private System.Windows.Forms.Label labelLayers;
        private System.Windows.Forms.Label labelOverflow;
        private customLabel labelXOffset;
        private customLabel labelYOffset;
        private customUpDown spinXOffset;
        private customUpDown spinYOffset;
        private customLabel labelRadialPower;
        private customLabel labelPolarPower;
        private customUpDown spinRadialPower;
        private customUpDown spinPolarPower;
        private System.Windows.Forms.GroupBox levelParamEditModeGroup;
        private levelParameterControl focusedRandomParamControl;
        private levelParameterControl xEditRandomParamControl;
        private levelParameterControl yEditRandomParamControl;
        private System.Windows.Forms.RadioButton radioValue;
        private System.Windows.Forms.RadioButton radioRightLimit;
        private System.Windows.Forms.RadioButton radioLeftLimit;
        internal System.Windows.Forms.Button buttonExportAllTextureData;
        internal System.Windows.Forms.Button buttonRandomize;
        private System.Windows.Forms.Label labelTextureCollections;
        internal System.Windows.Forms.Button buttonDeleteCollection;
        internal System.Windows.Forms.Button buttonAddCollection;
        internal System.Windows.Forms.Button buttonMoveCollectionUp;
        internal System.Windows.Forms.Button buttonMoveCollectionDown;
        internal System.Windows.Forms.Button buttonCloneCollection;
        private System.Windows.Forms.ListBox collectionList;
        private System.Windows.Forms.TabControl layerParameterTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox checkViewAllLayers;
        private customLabel labelScale;
        private customUpDown spinScale;
        private customUpDown spinSwirl;
        private customLabel labelSwirl;
        private customUpDown spinCircularStretchY;
        private customLabel labelCircularStretchY;
        private customUpDown spinMorphPower;
        private customLabel labelMorphPower;
        private customUpDown spinMorphFreq;
        private customLabel labelMorphFreq;
        private customUpDown spinMorphAmp;
        private customLabel labelMorphAmp;
        private customUpDown spinLinearStretchY;
        private customLabel labelLinearStretchY;
        internal System.Windows.Forms.Button buttonPlay;
        internal System.Windows.Forms.Button buttonStop;
        private customLabel labelClipDuration;
        private customUpDown spinClipDuration;
        private System.Windows.Forms.Timer animationTimer;
        internal System.Windows.Forms.Button buttonSetLeftLimitToValue;
        private customLabel labelCollisionDiameter1;
        private customUpDown spinCollisionDiameter1;
        internal System.Windows.Forms.Label labelClipTime;
        internal System.Windows.Forms.Label labelClipTimeLabel;
        internal System.Windows.Forms.Button buttonSetValueToLeftLimit;
        internal System.Windows.Forms.Button buttonSetValueToRightLimit;
        internal System.Windows.Forms.Button buttonSetRightLimitToValue;
        private System.Windows.Forms.CheckBox checkLoop;
        private customUpDown spinRotateAngle;
        private customLabel labelRotateAngle;
        private System.Windows.Forms.CheckBox checkShowCollider;
        private customLabel labelFramesPerSecond;
        private customUpDown spinFramesPerSecond;
        private customLabel labelCollisionDiameter2;
        private customUpDown spinCollisionDiameter2;
        private System.Windows.Forms.CheckBox checkGenerateClip;
        private customUpDown spinMorphPhase;
        private customLabel labelMorphPhase;
        private customUpDown spinCircularStretchX;
        private customLabel labelCircularStretchX;
        private customUpDown spinLinearStretchX;
        private customLabel labelLinearStretchX;
        private customUpDown spinSwirlMode;
        private customLabel labelSwirlMode;
        private customUpDown spinMirrorMode;
        private customLabel labelMirrorMode;
        private customUpDown spinMorphMode;
        private customLabel labelMorphMode;
        private customLabel labelSwirlModeText;
        private customLabel labelMirrorModeText;
        private customLabel labelMorphModeText;
        private customUpDown spinColorMode;
        private customLabel labelColorMode;
        private customLabel labelColorModeText;
        private System.Windows.Forms.CheckBox checkApplyToAllParams;
        private System.Windows.Forms.Label labelFast;
        internal System.Windows.Forms.Button buttonCopyLayer;
        internal System.Windows.Forms.Button buttonPasteLayer;
        internal System.Windows.Forms.Button buttonCopyTexture;
        internal System.Windows.Forms.Button buttonPasteTexture;
        private customLabel labelColliderXOffset;
        private customLabel labelColliderYOffset;
        private customUpDown spinColliderXOffset;
        private customUpDown spinColliderYOffset;
        private customUpDown spinColliderRotateAngle;
        private customLabel labelColliderRotateAngle;
        private System.Windows.Forms.Button buttonCopyColorTo;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonExportCollectionTextureData;
        private System.Windows.Forms.Button buttonFindReplaceTexture;
        private System.Windows.Forms.CheckBox checkRotateImage180;
        private customUpDown spinAnimationPhase;
        private customLabel labelAnimationPhase;
        private System.Windows.Forms.SaveFileDialog theSaveFileDialog;
        internal System.Windows.Forms.Button buttonPNG;
    }
}

