using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using dataStructures;

namespace TextureCreate
{
    public partial class levelParameterControl : UserControl
    {
        private bool updateGUIOnly = false;
        private int dataIndex;
        private float minimum = 0.0f;
        private float maximum = 100.0f;
        private float value = 0.0f;
        private float leftLimit = 0.0f;
        private float rightLimit = 0.0f;
        private Color labelBackColor = Color.Transparent;

        public levelParameterControl()
        {
            InitializeComponent();     
      
            // Clear label parameter
            this.labelParameter.Text = "";
        }
             
        public Color LabelBackColor
        {
            set
            {
                labelBackColor = value;
            }
        }

        public void initializeToParameter(customUpDown control, ref randomParameter parameter)
        {
            // Assign the control's data index
            this.dataIndex = control.DataIndex;

            // Initialize label's text and back color
            this.labelParameter.Text = control.label.Text;
            this.labelParameter.BackColor = labelBackColor;
            
            // Assign minimum and maximum value of parameter for use with drawing value limits graphic
            if (control.Maximum == 362)
            {
                this.minimum = 0.0f;
                this.maximum = 2.0f*(float)Math.PI;
            }
            else
            {
                this.minimum = (float)control.Minimum;
                this.maximum = (float)control.Maximum;
            }
            
            // Enable parameter modes
            this.parameterModeGroup.Enabled = true;
            
            // Update the gui to the random parameter
            this.updateGUIToData(ref parameter);
        }

        public void disable()
        {
            // Set to update GUI only
            this.updateGUIOnly = true;
            
            // Clear parameter text and back color, disable the parameter modes,
            // and clear the value limits graphic  
            this.labelParameter.Text = "";
            this.labelParameter.BackColor = Color.Transparent;
            this.parameterModeGroup.Enabled = false;
            this.radioAnimation.Checked = true;
            this.valueLimitsGraphic.Invalidate();

            // Set back to false
            this.updateGUIOnly = false;
        }

        public void updateGUIToData(ref randomParameter parameter)
        {
            // Set to update GUI only
            this.updateGUIOnly = true;

            // Update value and limits of the random parameter and then trigger a paint of the graphic
            this.value = parameter.value;
            this.leftLimit = parameter.leftLimit;
            this.rightLimit = parameter.rightLimit;
            this.valueLimitsGraphic.Invalidate();

            // Update parameter mode radio buttons
            switch((parameterModeType) parameter.parameterMode)
            {
                default:
                case parameterModeType.fixedParam:
                    this.radioFixed.Checked = true;
                    break;
               
                case parameterModeType.randomParam:
                    this.radioRandom.Checked = true;
                    break;

                case parameterModeType.animationParam:
                    this.radioAnimation.Checked = true;
                    break;

                case parameterModeType.animationBounceParam:
                    this.radioAnimationBounce.Checked = true;
                    break;
            }

            // Set back to false
            this.updateGUIOnly = false;
        }

        private void valueLimitsGraphic_Paint(object sender, PaintEventArgs e)
        {
            // Draw a background rectangle
            SolidBrush drawBrush = new SolidBrush(Color.DarkGray);
            Rectangle drawRectangle = new Rectangle(0, 0, this.valueLimitsGraphic.Width, this.valueLimitsGraphic.Height);
            e.Graphics.FillRectangle(drawBrush, drawRectangle);
            
            // Draw a left and right limit range rectangle and a narrow value rectangle
            if (this.labelParameter.Text != "")
            {
                drawBrush = new SolidBrush(Color.Gray);
                int startX;
                if (this.leftLimit < this.rightLimit)
                {
                    startX = (int)((this.leftLimit - this.minimum) / (this.maximum - this.minimum) * (float)this.valueLimitsGraphic.Width + 0.5f);
                }
                else
                {
                    startX = (int)((this.rightLimit - this.minimum) / (this.maximum - this.minimum) * (float)this.valueLimitsGraphic.Width + 0.5f);
                }                
                if (startX > this.valueLimitsGraphic.Width - 4) startX = this.valueLimitsGraphic.Width - 4;
                int drawWidth = (int) ((float)Math.Abs(this.rightLimit-this.leftLimit)/(this.maximum-this.minimum)* (float) this.valueLimitsGraphic.Width + 0.5f);
                if (drawWidth < 2) drawWidth = 2;
                drawRectangle = new Rectangle(startX-1, 0, drawWidth+1, this.valueLimitsGraphic.Height);
                e.Graphics.FillRectangle(drawBrush, drawRectangle);
                drawBrush = new SolidBrush(Color.White);
                startX = (int)((this.value-this.minimum) / (this.maximum - this.minimum) * (float)this.valueLimitsGraphic.Width + 0.5f);
                if (startX > this.valueLimitsGraphic.Width - 4) startX = this.valueLimitsGraphic.Width - 4;
                drawRectangle = new Rectangle(startX, 0, 2, this.valueLimitsGraphic.Height);
                e.Graphics.FillRectangle(drawBrush, drawRectangle);
            }               
        }      

        private void radioFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (mainForm.Instance != null && this.updateGUIOnly == false)
            {
                mainForm.Instance.updateParameterMode(this.dataIndex, parameterModeType.fixedParam);
            }
        }

        private void radioRandom_CheckedChanged(object sender, EventArgs e)
        {
            if (mainForm.Instance != null && this.updateGUIOnly == false)
            {
                mainForm.Instance.updateParameterMode(this.dataIndex, parameterModeType.randomParam);
            }
        }

        private void radioAnimation_CheckedChanged(object sender, EventArgs e)
        {
            if (mainForm.Instance != null && this.updateGUIOnly == false)
            {
                mainForm.Instance.updateParameterMode(this.dataIndex, parameterModeType.animationParam);
            }
        }

        private void radioAnimationBounce_CheckedChanged(object sender, EventArgs e)
        {
            if (mainForm.Instance != null && this.updateGUIOnly == false)
            {
                mainForm.Instance.updateParameterMode(this.dataIndex, parameterModeType.animationBounceParam);
            }
        }
    }
}
