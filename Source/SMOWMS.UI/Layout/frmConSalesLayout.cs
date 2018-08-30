using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.UI.ConsumablesManager;
using SMOWMS.CommLib;
using SMOWMS.UI.Menu;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmConSalesLayout : Smobiler.Core.Controls.MobileUserControl
    {
        #region �������
        AutofacConfig _autofacConfig = new AutofacConfig();
        #endregion
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
        /// ������������������ҳ�鿴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plRow_Press(object sender, EventArgs e)
        {
            frmConSalesResult frm = new frmConSalesResult();
            frm.SOID = lblName.BindDataValue.ToString();
            Form.Show(frm, (MobileForm sender1, object args) => {
                ((frmOrder)Form).Bind(((frmOrder)Form).type, ((frmOrder)Form).orderType);
            });
        }
        /// <summary>
        /// �Ĳ����۵��༭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibEdit_Press(object sender, EventArgs e)
        {
            frmConSalesCreate frm = new frmConSalesCreate { SOID = lblName.BindDataValue.ToString() };
            Form.Show(frm, (MobileForm sender1, object args) =>
            {
                if (frm.ShowResult == ShowResult.Yes)
                {
                    ((frmOrder)Form).Bind(((frmOrder)Form).type, ((frmOrder)Form).orderType);
                }
            });
        }
    }
}