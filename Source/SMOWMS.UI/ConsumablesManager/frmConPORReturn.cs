using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.DTOs.OutputDTO;
using SMOWMS.UI.Layout;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.CommLib;

namespace SMOWMS.UI.ConsumablesManager
{
    partial class frmConPORReturn : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig autofacConfig = new AutofacConfig();     //����������
        public String POID;               //�ĲĲɹ������
        #endregion
        /// <summary>
        /// ҳ���ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConPORReturn_Load(object sender, EventArgs e)
        {
            Bind();
        }
        /// <summary>
        /// ���ݼ���
        /// </summary>
        public void Bind()
        {
            try
            {
                List<ConPurchaseOrderReturnOutputDto> rows = autofacConfig.ConPurchaseOrderService.GetReturnRowsByPOID(POID);
                listCons.DataSource = rows;
                listCons.DataBind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            setCheck();
        }
        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plAll_Press(object sender, EventArgs e)
        {
            if (Checkall.Checked)
            {
                Checkall.Checked = false;
            }
            else
            {
                Checkall.Checked = true;
            }
            setCheck();
        }
        /// <summary>
        /// �޸�����ѡ��״̬
        /// </summary>
        private void setCheck()
        {
            foreach (ListViewRow Row in listCons.Rows)
            {
                frmConPORReturnLayout Layout = Row.Control as frmConPORReturnLayout;
                Layout.setCheck(Checkall.Checked);
            }
        }
        /// <summary>
        /// ȫѡ�����
        /// </summary>
        internal void upCheckState()
        {
            Int32 selectQty = 0;        //��ǰѡ��������
            foreach (ListViewRow Row in listCons.Rows)
            {
                frmConPORReturnLayout Layout = Row.Control as frmConPORReturnLayout;
                selectQty += Layout.checkNum();
            }
            if (selectQty == listCons.Rows.Count)
                Checkall.Checked = true;          //ѡ����������ʱ
            else
                Checkall.Checked = false;        //û��ѡ����������
        }
        /// <summary>
        /// �ύ�˿����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                List<ConPurchaseOrderRowInputDto> Rows = new List<ConPurchaseOrderRowInputDto>();
                foreach (ListViewRow row in listCons.Rows)
                {
                    frmConPORReturnLayout Layout = row.Control as frmConPORReturnLayout;
                    if (Layout.getData() != null)
                    {
                        Rows.Add(Layout.getData());   //��������Ϣ
                    }
                }
                if (Rows.Count == 0) throw new Exception("��ѡ���˿�Ĳ�!");
                ConPOInStoInputDto stoInputDto = new ConPOInStoInputDto();
                stoInputDto.POID = POID;
                stoInputDto.CREATEUSER = Client.Session["UserID"].ToString();
                stoInputDto.RowDatas = Rows;
                ReturnInfo RInfo = autofacConfig.ConPurchaseOrderService.ReturnConPurchaseOrder(stoInputDto);
                if (RInfo.IsSuccess)
                {
                    List<ConPurchaseOrderReturnOutputDto> rows = autofacConfig.ConPurchaseOrderService.GetReturnRowsByPOID(POID);
                    if (rows.Count == 0)
                    {
                        Toast("�òɹ����˿����!");
                        Form.Close();
                    }
                    else
                    {
                        Toast("�˿�ɹ�!");
                        Bind();         //ˢ�µ�ǰҳ���������
                        Checkall.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}