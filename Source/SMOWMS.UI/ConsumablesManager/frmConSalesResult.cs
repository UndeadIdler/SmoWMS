using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using ListView = Smobiler.Core.Controls.ListView;
using Smobiler.Core.Controls;
using System.Data;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.Enum;
using SMOWMS.CommLib;
using SMOWMS.UI.Layout;
using System.Windows.Forms;
using System.Drawing;
using SMOWMS.DTOs.OutputDTO;

namespace SMOWMS.UI.ConsumablesManager
{
    partial class frmConSalesResult : Smobiler.Core.Controls.MobileForm
    {
        #region  �������
        private AutofacConfig autofacConfig = new AutofacConfig();//����������
        public string SOID; //���۵����
        private string UserId;  //�û����
        #endregion
        /// <summary>
        /// ҳ���ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConSalesResult_Load(object sender, EventArgs e)
        {
            try
            {
                UserId = Client.Session["UserID"].ToString();

                ///��ͷ��Ϣ
                ConSalesOrderOutputDto Order = autofacConfig.ConSalesOrderService.GetBySOID(SOID);
                lblRealID.Text = Order.REALID;
                lblName.Text = Order.NAME;
                lblCustomer.Text = Order.CUSTOMERNAME;
                lblDealMan.Text = Order.SALESPERSONNAME;

                //���ݰ�
                Bind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ���ݰ�
        /// </summary>
        public void Bind()
        {
            try
            {
                ConSalesOrderOutputDto Order = autofacConfig.ConSalesOrderService.GetBySOID(SOID);
                List<ConSalesOrderOutboundOutputDto> outRows = autofacConfig.ConSalesOrderService.GetOutRowsBySOID(SOID);
                List<ConSalesOrderRowInputDto> retRows = autofacConfig.ConSalesOrderService.GetRetRowsBySOID(SOID);
                if (Order.STATUS == (int)SalesOrderStatus.����� && outRows.Count == 0 && retRows.Count == 0)        ////����޿��˿�Ĳ�,�޿����Ĳģ������ذ�ť
                {
                    Form.ActionButton.Items.RemoveAt(1);
                    Form.ActionButton.Items.RemoveAt(0);
                }
                if (Form.ActionButton.Items.Count == 0)
                {
                    Form.ActionButton.Enabled = false;
                }

                List<ConPurAndSaleCreateInputDto> AlRows = autofacConfig.ConSalesOrderService.GetOrderRows(SOID);

                lvData.Rows.Clear();
                lvData.DataSource = AlRows;
                lvData.DataBind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ���⡢�˻��������ύ���������۵Ȳ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConSalesResult_ActionButtonPress(object sender, ActionButtonPressEventArgs e)
        {
            try
            {
                ReturnInfo rInfo = new ReturnInfo();
                switch (e.Index)
                {
                    case 0:      //�Ĳĳ���
                        List<ConSalesOrderOutboundOutputDto> outRows = autofacConfig.ConSalesOrderService.GetOutRowsBySOID(SOID);
                        if (outRows.Count > 0)
                        {
                            frmConSOROutbound frmOut = new frmConSOROutbound();
                            frmOut.SOID = SOID;
                            Form.Show(frmOut, (MobileForm sender1, object args) =>
                            {
                                Bind();      //���ݰ�
                            });
                        }
                        else
                        {
                            throw new Exception("�����ĵ���Ŀǰ�޿ɳ���Ĳ�!");
                        }
                        break;
                    case 1:      //�Ĳ��˻�
                        List<ConSalesOrderRowInputDto> retRows = autofacConfig.ConSalesOrderService.GetRetRowsBySOID(SOID);
                        if (retRows.Count > 0)
                        {
                            frmConSORRetiring frmRet = new frmConSORRetiring();
                            frmRet.SOID = SOID;
                            Form.Show(frmRet, (MobileForm sender1, object args) =>
                            {
                                Bind();      //���ݰ�
                            });
                        }
                        else
                        {
                            throw new Exception("�����ĵ���Ŀǰ�޿ɳ���Ĳ�!");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}