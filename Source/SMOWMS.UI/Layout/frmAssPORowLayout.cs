using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.UI.AssetsManager;
using SMOWMS.UI.ConsumablesManager;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmAssPORowLayout : Smobiler.Core.Controls.MobileUserControl
    {

        private void plRow_LongPress(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("��ȷ��Ҫ��������?", "ϵͳ����", MessageBoxButtons.OKCancel, (object sender1, MessageBoxHandlerArgs args) =>
                {
                    try
                    {
                        if (args.Result == ShowResult.OK)     //ɾ��������
                        {
                            if (this.Form.ToString() == "SMOWMS.UI.AssetsManager.frmAssPurchaseOrderCreate")
                            {
                                ((frmAssPurchaseOrderCreate)Form).RemoveTemplate(LblTId.BindDataValue.ToString());
                            }
                            else
                            {
                                ((frmAssPurchaseOrderEdit)Form).RemoveTemplate(LblTId.BindDataValue.ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Form.Toast(ex.Message);
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