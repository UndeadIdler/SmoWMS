using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.CommLib;
using SMOWMS.UI.ConsumablesManager;
using SMOWMS.UI.Menu;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmConPurchaseLayout : Smobiler.Core.Controls.MobileUserControl
    {
        #region �������
        AutofacConfig _autofacConfig=new AutofacConfig();
        #endregion
        /// <summary>
        /// �ɹ������
        /// </summary>
        internal string POID
        {
            get
            {
                return this.lblName.BindDataValue.ToString();
            }

        }
        /// <summary>
        /// ����������ɹ�����鿴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plRow_Press(object sender, EventArgs e)
        {
            frmConPurchaseResult frm = new frmConPurchaseResult();
            frm.POID = lblName.BindDataValue.ToString();
            Form.Show(frm, (MobileForm sender1, object args) => {
                ((frmOrder)Form).Bind(((frmOrder)Form).type, ((frmOrder)Form).orderType);
            });
        }
        /// <summary>
        /// �ɹ����༭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ibEdit_Press(object sender, EventArgs e)
        {
            frmConPurchaseCreate frm = new frmConPurchaseCreate { POID = lblName.BindDataValue.ToString() };
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