using System;
using Smobiler.Core.Controls;
using SMOWMS.DTOs.OutputDTO;
using SMOWMS.DTOs.InputDTO;
using System.Data;
using System.Collections.Generic;
using SMOWMS.DTOs.Enum;
using SMOWMS.CommLib;

namespace SMOWMS.UI.ConsumablesManager
{
    partial class frmConPurchaseResult : Smobiler.Core.Controls.MobileForm
    {
        #region  �������
        private AutofacConfig autofacConfig = new AutofacConfig();//����������
        public string POID; //�ɹ������
        private string UserId;  //�û����

        #endregion
        /// <summary>
        /// ҳ���ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConPurChaseResult_Load(object sender, EventArgs e)
        {
            try
            {
                UserId = Client.Session["UserID"].ToString();

                ///��ͷ��Ϣ
                ConPurchaseOrderOutputDto Order = autofacConfig.ConPurchaseOrderService.GetByPOID(POID);
                lblRealID.Text = Order.REALID;
                lblName.Text = Order.NAME;
                lblVendor.Text = Order.VENDORNAME;
                lblDealMan.Text = Order.PURCHASERNAME;

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
                ConPurchaseOrderOutputDto Order = autofacConfig.ConPurchaseOrderService.GetByPOID(POID);
                List<ConPurchaseOrderReturnOutputDto> returnRows = autofacConfig.ConPurchaseOrderService.GetReturnRowsByPOID(POID);
                List<ConPORInstorageOutputDto> rows = autofacConfig.ConPurchaseOrderService.GetInStoRowsByPOID(POID);
                if (Order.STATUS == (int)PurchaseOrderStatus.����� && returnRows.Count == 0 && rows.Count == 0)        ////����޿��˿�Ĳ�,�޿����Ĳģ������ذ�ť
                {
                    Form.ActionButton.Items.RemoveAt(1);
                    Form.ActionButton.Items.RemoveAt(0);
                }
                if (Form.ActionButton.Items.Count == 0)
                {
                    Form.ActionButton.Enabled = false;
                }

                List<ConPurAndSaleCreateInputDto> AlRows = autofacConfig.ConPurchaseOrderService.GetOrderRows(POID);
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
        /// ��⡢���⡢�����ύ����������Ȳ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConPurchaseResult_ActionButtonPress(object sender, ActionButtonPressEventArgs e)
        {
            try
            {
                ReturnInfo rInfo = new ReturnInfo();
                switch (e.Index)
                {
                    case 0:      //�Ĳ����
                        List<ConPORInstorageOutputDto> rows = autofacConfig.ConPurchaseOrderService.GetInStoRowsByPOID(POID);
                        if (rows.Count > 0)
                        {
                            frmConPORInSto frmConPORInSto = new frmConPORInSto();
                            frmConPORInSto.POID = POID;
                            Form.Show(frmConPORInSto, (MobileForm sender1, object args) =>
                            {
                                Bind();      //���ݰ�
                            });
                        }
                        else
                        {
                            throw new Exception("����ⵥ��Ŀǰ�޿����Ĳ�!");
                        }
                        break;
                    case 1:      //�Ĳ��˿�
                        List<ConPurchaseOrderReturnOutputDto> returnRows = autofacConfig.ConPurchaseOrderService.GetReturnRowsByPOID(POID);
                        if (returnRows.Count > 0)
                        {
                            frmConPORReturn frmConPORReturn = new frmConPORReturn();
                            frmConPORReturn.POID = POID;
                            Form.Show(frmConPORReturn, (MobileForm sender1, object args) =>
                            {
                                Bind();      //���ݰ�
                            });
                        }
                        else
                        {
                            throw new Exception("����ⵥ��Ŀǰ�޿��˿�Ĳ�!");
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