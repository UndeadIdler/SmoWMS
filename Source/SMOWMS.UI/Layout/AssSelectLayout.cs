using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMOWMS.UI.AssetsManager;
using Smobiler.Core;
using Smobiler.Core.Controls;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class AssSelectLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// CheckBoxѡ��״̬�仯ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                frmAssSourceChoose source = (frmAssSourceChoose)this.Form;
                if (CheckBox1.Checked)
                {
                    
                    source.AddAss(lblASSID.BindDataValue.ToString(),LblSN.BindDataValue.ToString(),Image.ResourceID,LblName.BindDataValue.ToString());
                }
                else
                {
                    source.RemoveAss(lblASSID.BindDataValue.ToString());
                }
                source.UpdateCheckState();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}