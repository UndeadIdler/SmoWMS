using System;
using System.Collections.Generic;
using Smobiler.Core.Controls;
using SMOWMS.CommLib;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.UI.Layout;

namespace SMOWMS.UI.AssetsManager
{
    partial class frmAssPurchaseOrderResult : Smobiler.Core.Controls.MobileForm
    {
        #region  �������
        private AutofacConfig _autofacConfig = new AutofacConfig();//����������
        public List<string> PORowIds=new List<string>();
        public string POID;
        public List<int> selectRowList=new List<int>();
        public int Status;
        #endregion

        /// <summary>
        /// �����ʼ��ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssPurchaseOrderResult_Load(object sender, EventArgs e)
        {
            try
            {
                Bind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ��ActionButtonʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssPurchaseOrderResult_ActionButtonPress(object sender, ActionButtonPressEventArgs e)
        {
            try
            {
                switch (e.Index)
                {
                    case 0:
                        //���
                        switch (Status)
                        {
                            case 2:
                                throw new Exception("�������ɣ�");
                            case 0:
                            case 1:
                                frmAssIn frmAssIn = new frmAssIn
                                {
                                    POID = POID,
                                    IsFromPO = true
                                };
                                Show(frmAssIn, (MobileForm sender1, object args) =>
                                {
                                    if (frmAssIn.ShowResult == ShowResult.Yes)
                                    {
                                        Bind();
                                    }
                                });
                                break;
                        }
                        break;
                    case 1:
                        //�˻�
                        switch (Status)
                        {
                            case 0:
                                throw new Exception("���δ��ʼ,�޷��˻���");
                            case 2:
                            case 1:
                                frmAssReturn frmAssReturn = new frmAssReturn
                                {
                                    POID = POID,
                                    IsFromPO = true
                                };
                                Show(frmAssReturn, (MobileForm sender1, object args) =>
                                {
                                    if (frmAssReturn.ShowResult == ShowResult.Yes)
                                    {
                                        Bind();
                                    }
                                });
                                break;
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        public void Bind()
        {
            try
            {
                var po = _autofacConfig.AssPurchaseOrderService.GetById(POID);
                lblName.Text = po.NAME;
                lblPMan.Text = po.PURCHASERNAME;
                lblRealId.Text = po.REALID;
                lblStatus.Tag = po.STATUS;
                lblVendor.Text = po.VNAME;
                lblTID.Text = POID;
                Status = po.STATUS;
                switch (po.STATUS)
                {
                    case 1:
                        lblStatus.Text = "�����";
                        break;
                    case 2:
                        lblStatus.Text = "�����";
                        break;
                    case 0:
                        lblStatus.Text = "�ɹ���";
                        break;
                }
                var row = _autofacConfig.AssPurchaseOrderService.GetRows(POID);
                if (row.Rows.Count > 0)
                {
                    lvPORow.DataSource = row;
                    lvPORow.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// �����ˣ���رմ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssPurchaseOrderResult_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }
    }
}