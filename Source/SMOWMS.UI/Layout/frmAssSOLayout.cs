using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.CommLib;
using SMOWMS.UI.AssetsManager;
using SMOWMS.UI.Menu;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmAssSOLayout : Smobiler.Core.Controls.MobileUserControl
    {
        private AutofacConfig _autofacConfig = new AutofacConfig();//����������
        /// <summary>
        /// ���۵����
        /// </summary>
        internal string SOID
        {
            get
            {
                return this.lblName.BindDataValue.ToString();
            }

        }

       
        /// <summary>
        /// �༭���۵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibEdit_Press(object sender, EventArgs e)
        {
            try
            {
                frmAssSalesOrderEdit edit = new frmAssSalesOrderEdit { SOID = lblName.BindDataValue.ToString() };
                Form.Show(edit, (MobileForm sender1, object args) =>
                {
                    if (edit.ShowResult == ShowResult.Yes)
                    {
                        ((frmOrder)Form).Bind(((frmOrder)Form).type, ((frmOrder)Form).orderType);
                    }

                }
                );
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plRow_Press(object sender, EventArgs e)
        {
            try
            {
                frmAssSalesOrderResult result = new frmAssSalesOrderResult { SOID = lblName.BindDataValue.ToString() };
                Form.Show(result, (MobileForm sender1, object args) =>
                {
                    if (result.ShowResult == ShowResult.Yes || result.ShowResult == ShowResult.None)
                    {
                        ((frmOrder)Form).Bind(((frmOrder)Form).type, ((frmOrder)Form).orderType);
                    }
                });
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}