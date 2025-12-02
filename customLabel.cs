using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextureCreate
{
    public class customLabel : System.Windows.Forms.Label
    {
        public customUpDown upDown = null;

        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {   
            // Tell the main form a label was mouse clicked       
            if (mainForm.Instance != null)
            {
                mainForm.Instance.labelMouseClicked(this.upDown, e.Button);
            }
        }
    }
}
