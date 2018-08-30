using System;
using Smobiler.Core.Controls;
using SMOWMS.UI.AssetsManager;
using SMOWMS.UI.ConsumablesManager;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmTransferRowsLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// �鿴����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plContent_Press(object sender, EventArgs e)
        {
            try
            {
                if (Form.ToString() == "SMOWMS.UI.AssetsManager.frmAssOrder")
                {
                    frmAssTransferDetail frm = new frmAssTransferDetail();
                    frm.TOID = lblID.BindDataValue.ToString();
                    Form.Show(frm, (MobileForm sender1, object args) => {
                        if (frm.ShowResult == ShowResult.Yes)
                        {
                            ((frmAssTransferRows)Form).Bind();        //���°�����
                        }
                    });             
                }
                else if (Form.ToString() == "SMOWMS.UI.ConsumablesManager.frmTransferRows")
                {
                    frmTransferDetail frm = new frmTransferDetail();
                    frm.TOID = lblID.BindDataValue.ToString();
                    Form.Show(frm, (MobileForm sender1, object args) => {
                        if (frm.ShowResult == ShowResult.Yes)
                        {
                            ((frmTransferRows)Form).Bind();        //���°�����
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Form.Toast(ex.Message);
            }
        }
    }
}