using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.UI.Layout;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.CommLib;
using SMOWMS.DTOs.OutputDTO;

namespace SMOWMS.UI.ConsumablesManager
{
    partial class frmConSOROutbound : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig autofacConfig = new AutofacConfig();     //����������
        public String SOID;               //�Ĳ����۵����
        #endregion
        /// <summary>
        /// ҳ���ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConSOROutbound_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(SOID))       //�����۵�����������
            {
                plOrder.Visible = false;
                Bind();
            }          
        }
        /// <summary>
        /// ����ɨ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgOrder_Press(object sender, EventArgs e)
        {
            bsOrder.GetBarcode();
        }
        /// <summary>
        /// ɨ�赽����ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsOrder_BarcodeScanned(object sender, BarcodeResultArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(e.error))
                {
                    ConSalesOrderOutputDto conSales= autofacConfig.ConSalesOrderService.GetBySOID(e.Value);
                    if(conSales != null)
                    {
                        SOID = e.Value;
                        Bind();
                    }
                    else
                    {
                        throw new Exception("�ö����Ų�����");
                    }
                }
            }
            catch(Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ���ݼ���
        /// </summary>
        public void Bind()
        {
            try
            {
                List<ConSalesOrderOutboundOutputDto> rows = autofacConfig.ConSalesOrderService.GetOutRowsBySOID(SOID);
                listCons.DataSource = rows;
                listCons.DataBind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ȫѡ/ȫ��ѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            setCheck();
        }
        /// <summary>
        /// ȫѡ/ȫ��ѡ
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
                frmConSOROutboundLayout Layout = Row.Control as frmConSOROutboundLayout;
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
                frmConSOROutboundLayout Layout = Row.Control as frmConSOROutboundLayout;
                selectQty += Layout.checkNum();
            }
            if (selectQty == listCons.Rows.Count)
                Checkall.Checked = true;          //ѡ����������ʱ
            else
                Checkall.Checked = false;        //û��ѡ����������
        }
        /// <summary>
        /// �����ύ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                List<ConSalesOrderRowInputDto> Rows = new List<ConSalesOrderRowInputDto>();
                foreach (ListViewRow row in listCons.Rows)
                {
                    frmConSOROutboundLayout Layout = row.Control as frmConSOROutboundLayout;
                    if (Layout.getData() != null)
                    {
                        Rows.Add(Layout.getData());   //��������Ϣ
                    }
                }
                if (Rows.Count == 0) throw new Exception("��ѡ���˿�Ĳ�!");
                ConSOOutboundInputDto outInputDto = new ConSOOutboundInputDto();
                outInputDto.SOID = SOID;
                outInputDto.CREATEUSER = Client.Session["UserID"].ToString();
                outInputDto.RowDatas = Rows;
                ReturnInfo RInfo = autofacConfig.ConSalesOrderService.OutboundConSalesOrder(outInputDto);
                if (RInfo.IsSuccess)
                {
                    List<ConSalesOrderOutboundOutputDto> rows = autofacConfig.ConSalesOrderService.GetOutRowsBySOID(SOID);
                    if (rows.Count == 0)
                    {
                        Toast("�����۵��������!");
                        Form.Close();
                    }
                    else
                    {
                        Toast("����ɹ�!");
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