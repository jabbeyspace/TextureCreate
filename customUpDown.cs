using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace TextureCreate
{
    public class customUpDown : System.Windows.Forms.NumericUpDown
    {
        public customLabel label = null;
        private int dataIndex = 0;
        
        public int DataIndex
        {
            get
            {
                return this.dataIndex;
            }
            set
            {
                dataIndex = value;
                if (this.label != null)
                {
                    this.label.upDown = this;
                }
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {           
            // Select all text on got focus
            this.Select(0, this.ToString().Length);

            // Update main form to change in focused up down control
            if (mainForm.Instance != null)
            {
                mainForm.Instance.updateToFocusedUpDownControl(this, true);

                // If the parameter mode changed - this will force the controls' backgound to draw a different color
                //mainForm.Instance.updateGUIToCurrentLayer();
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            // Update main form to change in focused up down control
            if (mainForm.Instance != null)
            {
                // Redisplay and update the value in the text box on losing focus
                decimal currentValue = this.Value;
                TextBox valueTextBox = (TextBox)this.Controls[1];
                if (valueTextBox.Text == "")
                {
                    valueTextBox.Text = this.Value.ToString();
                    if (currentValue != this.Value)
                    {
                        // Update data
                        mainForm.Instance.updateTextureData(this);

                        // Update focused up down control
                        mainForm.Instance.updateToFocusedUpDownControl(this, false);
                    }
                }

                // If the parameter mode changed - this will force the controls' backgound to draw a different color
                mainForm.Instance.updateGUIToCurrentLayer();
            }
        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            // Select all text on mouse down
            this.Select(0, this.ToString().Length);
        }

        protected override void OnValueChanged(EventArgs e)
        {
            // Update texture parameter
            if (mainForm.Instance != null)
            {
                mainForm.Instance.updateTextureData(this);
            }
        }
    }
}
