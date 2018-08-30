using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.UI.MasterData;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmAssTemplateLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// CheckBox�ı�ѡ��ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ((frmAssTemplate)Form).SelectTemplateId = CheckBox1 .Checked ? LblTemplateId.Text : "";
                ((frmAssTemplate)Form).Bind();
            }
            catch (Exception ex)
            {
                Form.Toast(ex.Message);
            }
        }

        /// <summary>
        /// �����������ʲ�ģ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plRow_Press(object sender, EventArgs e)
        {
            try
            {
                frmAssTemplateDetail detail = new frmAssTemplateDetail { TempId = LblTemplateId.BindDataValue.ToString() };
                Form.Show(detail, (MobileForm sender1, object args) =>
                    {
                        if (detail.ShowResult == ShowResult.Yes)
                        {
                            ((frmAssTemplate)Form).Bind();
                        }

                    }
                );
            }
            catch (Exception ex)
            {
                Parent.Form.Toast(ex.Message);
            }
        }
    }
}