namespace TextureCreate
{
    partial class findReplaceForm
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
            this.replaceTextGroup = new System.Windows.Forms.GroupBox();
            this.replaceTextBox = new System.Windows.Forms.TextBox();
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.OK_Button = new System.Windows.Forms.Button();
            this.findTextGroup = new System.Windows.Forms.GroupBox();
            this.findTextBox = new System.Windows.Forms.TextBox();
            this.replaceTextGroup.SuspendLayout();
            this.findTextGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // replaceTextGroup
            // 
            this.replaceTextGroup.Controls.Add(this.replaceTextBox);
            this.replaceTextGroup.Location = new System.Drawing.Point(7, 77);
            this.replaceTextGroup.Name = "replaceTextGroup";
            this.replaceTextGroup.Size = new System.Drawing.Size(350, 54);
            this.replaceTextGroup.TabIndex = 9;
            this.replaceTextGroup.TabStop = false;
            this.replaceTextGroup.Text = "Replace with";
            // 
            // replaceTextBox
            // 
            this.replaceTextBox.Location = new System.Drawing.Point(6, 19);
            this.replaceTextBox.Name = "replaceTextBox";
            this.replaceTextBox.Size = new System.Drawing.Size(338, 20);
            this.replaceTextBox.TabIndex = 1;
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_Button.Location = new System.Drawing.Point(290, 154);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(67, 23);
            this.Cancel_Button.TabIndex = 7;
            this.Cancel_Button.TabStop = false;
            this.Cancel_Button.Text = "Cancel";
            // 
            // OK_Button
            // 
            this.OK_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK_Button.Location = new System.Drawing.Point(217, 154);
            this.OK_Button.Name = "OK_Button";
            this.OK_Button.Size = new System.Drawing.Size(67, 23);
            this.OK_Button.TabIndex = 6;
            this.OK_Button.TabStop = false;
            this.OK_Button.Text = "OK";
            // 
            // findTextGroup
            // 
            this.findTextGroup.Controls.Add(this.findTextBox);
            this.findTextGroup.Location = new System.Drawing.Point(7, 10);
            this.findTextGroup.Name = "findTextGroup";
            this.findTextGroup.Size = new System.Drawing.Size(350, 54);
            this.findTextGroup.TabIndex = 8;
            this.findTextGroup.TabStop = false;
            this.findTextGroup.Text = "Find what";
            // 
            // findTextBox
            // 
            this.findTextBox.Location = new System.Drawing.Point(6, 19);
            this.findTextBox.Name = "findTextBox";
            this.findTextBox.Size = new System.Drawing.Size(338, 20);
            this.findTextBox.TabIndex = 0;
            // 
            // findReplaceForm
            // 
            this.AcceptButton = this.OK_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 188);
            this.Controls.Add(this.replaceTextGroup);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.OK_Button);
            this.Controls.Add(this.findTextGroup);
            this.Name = "findReplaceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "findReplaceForm";
            this.replaceTextGroup.ResumeLayout(false);
            this.replaceTextGroup.PerformLayout();
            this.findTextGroup.ResumeLayout(false);
            this.findTextGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox replaceTextGroup;
        internal System.Windows.Forms.TextBox replaceTextBox;
        internal System.Windows.Forms.Button Cancel_Button;
        internal System.Windows.Forms.Button OK_Button;
        internal System.Windows.Forms.GroupBox findTextGroup;
        internal System.Windows.Forms.TextBox findTextBox;
    }
}