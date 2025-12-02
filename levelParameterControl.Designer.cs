namespace TextureCreate
{
    partial class levelParameterControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.parameterModeGroup = new System.Windows.Forms.GroupBox();
            this.radioAnimationBounce = new System.Windows.Forms.RadioButton();
            this.radioFixed = new System.Windows.Forms.RadioButton();
            this.radioAnimation = new System.Windows.Forms.RadioButton();
            this.radioRandom = new System.Windows.Forms.RadioButton();
            this.labelParameter = new System.Windows.Forms.Label();
            this.valueLimitsGraphic = new System.Windows.Forms.PictureBox();
            this.parameterModeGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valueLimitsGraphic)).BeginInit();
            this.SuspendLayout();
            // 
            // parameterModeGroup
            // 
            this.parameterModeGroup.Controls.Add(this.radioAnimationBounce);
            this.parameterModeGroup.Controls.Add(this.radioFixed);
            this.parameterModeGroup.Controls.Add(this.radioAnimation);
            this.parameterModeGroup.Controls.Add(this.radioRandom);
            this.parameterModeGroup.Location = new System.Drawing.Point(138, 4);
            this.parameterModeGroup.Name = "parameterModeGroup";
            this.parameterModeGroup.Size = new System.Drawing.Size(114, 66);
            this.parameterModeGroup.TabIndex = 0;
            this.parameterModeGroup.TabStop = false;
            this.parameterModeGroup.Text = "parameter mode";
            // 
            // radioAnimationBounce
            // 
            this.radioAnimationBounce.AutoSize = true;
            this.radioAnimationBounce.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.radioAnimationBounce.Location = new System.Drawing.Point(46, 39);
            this.radioAnimationBounce.Name = "radioAnimationBounce";
            this.radioAnimationBounce.Size = new System.Drawing.Size(61, 17);
            this.radioAnimationBounce.TabIndex = 3;
            this.radioAnimationBounce.TabStop = true;
            this.radioAnimationBounce.Text = "bounce";
            this.radioAnimationBounce.UseVisualStyleBackColor = false;
            this.radioAnimationBounce.CheckedChanged += new System.EventHandler(this.radioAnimationBounce_CheckedChanged);
            // 
            // radioFixed
            // 
            this.radioFixed.AutoSize = true;
            this.radioFixed.Location = new System.Drawing.Point(6, 16);
            this.radioFixed.Name = "radioFixed";
            this.radioFixed.Size = new System.Drawing.Size(35, 17);
            this.radioFixed.TabIndex = 2;
            this.radioFixed.TabStop = true;
            this.radioFixed.Text = "fix";
            this.radioFixed.UseVisualStyleBackColor = true;
            this.radioFixed.CheckedChanged += new System.EventHandler(this.radioFixed_CheckedChanged);
            // 
            // radioAnimation
            // 
            this.radioAnimation.AutoSize = true;
            this.radioAnimation.BackColor = System.Drawing.Color.LightGreen;
            this.radioAnimation.Location = new System.Drawing.Point(6, 39);
            this.radioAnimation.Name = "radioAnimation";
            this.radioAnimation.Size = new System.Drawing.Size(39, 17);
            this.radioAnimation.TabIndex = 1;
            this.radioAnimation.TabStop = true;
            this.radioAnimation.Text = "ani";
            this.radioAnimation.UseVisualStyleBackColor = false;
            this.radioAnimation.CheckedChanged += new System.EventHandler(this.radioAnimation_CheckedChanged);
            // 
            // radioRandom
            // 
            this.radioRandom.AutoSize = true;
            this.radioRandom.BackColor = System.Drawing.Color.Violet;
            this.radioRandom.Location = new System.Drawing.Point(47, 16);
            this.radioRandom.Name = "radioRandom";
            this.radioRandom.Size = new System.Drawing.Size(60, 17);
            this.radioRandom.TabIndex = 0;
            this.radioRandom.TabStop = true;
            this.radioRandom.Text = "random";
            this.radioRandom.UseVisualStyleBackColor = false;
            this.radioRandom.CheckedChanged += new System.EventHandler(this.radioRandom_CheckedChanged);
            // 
            // labelParameter
            // 
            this.labelParameter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelParameter.Location = new System.Drawing.Point(4, 27);
            this.labelParameter.Name = "labelParameter";
            this.labelParameter.Size = new System.Drawing.Size(128, 43);
            this.labelParameter.TabIndex = 188;
            this.labelParameter.Text = "parameter name";
            this.labelParameter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // valueLimitsGraphic
            // 
            this.valueLimitsGraphic.BackColor = System.Drawing.Color.DarkGray;
            this.valueLimitsGraphic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.valueLimitsGraphic.Location = new System.Drawing.Point(4, 4);
            this.valueLimitsGraphic.Name = "valueLimitsGraphic";
            this.valueLimitsGraphic.Size = new System.Drawing.Size(128, 20);
            this.valueLimitsGraphic.TabIndex = 189;
            this.valueLimitsGraphic.TabStop = false;
            this.valueLimitsGraphic.Paint += new System.Windows.Forms.PaintEventHandler(this.valueLimitsGraphic_Paint);
            // 
            // levelParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.valueLimitsGraphic);
            this.Controls.Add(this.labelParameter);
            this.Controls.Add(this.parameterModeGroup);
            this.Name = "levelParameterControl";
            this.Size = new System.Drawing.Size(257, 75);
            this.parameterModeGroup.ResumeLayout(false);
            this.parameterModeGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.valueLimitsGraphic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox parameterModeGroup;
        private System.Windows.Forms.RadioButton radioRandom;
        private System.Windows.Forms.RadioButton radioAnimation;
        private System.Windows.Forms.Label labelParameter;
        private System.Windows.Forms.PictureBox valueLimitsGraphic;
        private System.Windows.Forms.RadioButton radioFixed;
        private System.Windows.Forms.RadioButton radioAnimationBounce;
    }
}
