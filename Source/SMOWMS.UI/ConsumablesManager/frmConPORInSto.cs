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
    partial class frmConPORInSto : Smobiler.Core.Controls.MobileForm
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
        private void frmConPORInSto_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(POID))     //�Ӳɹ�������������
            {
                plOrder.Visible = false;
                Bind();
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        private void Bind()
        {
            try
            {
                List<ConPORInstorageOutputDto> rows = autofacConfig.ConPurchaseOrderService.GetInStoRowsByPOID(POID);
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
                frmConPORInStoLayout Layout = Row.Control as frmConPORInStoLayout;
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
                frmConPORInStoLayout Layout = Row.Control as frmConPORInStoLayout;
                selectQty += Layout.checkNum();
            }
            if (selectQty == listCons.Rows.Count)
                Checkall.Checked = true;          //ѡ����������ʱ
            else
                Checkall.Checked = false;        //û��ѡ����������
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
                    ConPurchaseOrderOutputDto conPurchaseOrder= autofacConfig.ConPurchaseOrderService.GetByPOID(e.Value);
                    if(conPurchaseOrder != null)
                    {
                        POID = e.Value;
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
        /// ��λɨ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgBS_Press(object sender, EventArgs e)
        {
            bsLoc.GetBarcode();
        }
        /// <summary>
        /// ɨ�赽��λʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsLoc_BarcodeScanned(object sender, BarcodeResultArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(e.error))
                {
                    String Data = e.Value;
                    String[] Datas = Data.Split('/');
                    WHStorageLocationOutputDto whLoc = autofacConfig.wareHouseService.GetSLByID(Datas[0], Datas[1], Datas[2]);
                    if (whLoc == null) throw new Exception("��λ�����ڣ�����!");
                    lblLocation.Text = whLoc.WARENAME + "/" + whLoc.STNAME + "/" + whLoc.SLNAME;
                    lblLocation.Tag = Data;
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ����ύ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(lblLocation.Text)) throw new Exception("��ɨ������λ!");
                List<ConPurchaseOrderRowInputDto> Rows = new List<ConPurchaseOrderRowInputDto>();
                foreach (ListViewRow row in listCons.Rows)
                {
                    frmConPORInStoLayout Layout = row.Control as frmConPORInStoLayout;
                    if (Layout.getData() != null)
                    {
                        Rows.Add(Layout.getData());   //��������Ϣ
                    }
                }
                if (Rows.Count == 0) throw new Exception("��ѡ�����Ĳ�!");
                String[] locDatas = lblLocation.Tag.ToString().Split('/');
                ConPOInStoInputDto stoInputDto = new ConPOInStoInputDto();
                stoInputDto.POID = POID;
                stoInputDto.WAREID = locDatas[0];
                stoInputDto.STID = locDatas[1];
                stoInputDto.SLID = locDatas[2];
                stoInputDto.CREATEUSER = Client.Session["UserID"].ToString();
                stoInputDto.RowDatas = Rows;
                ReturnInfo RInfo = autofacConfig.ConPurchaseOrderService.InStoConPurhcaseOrder(stoInputDto);
                if (RInfo.IsSuccess)
                {
                    List<ConPORInstorageOutputDto> rows = autofacConfig.ConPurchaseOrderService.GetInStoRowsByPOID(POID);
                    if (rows.Count == 0)
                    {
                        Toast("�òɹ���������!");
                        Form.Close();
                    }
                    else
                    {
                        Toast("���ɹ�!");
                        Bind();         //ˢ�µ�ǰҳ���������
                        lblLocation.Text = "";
                        lblLocation.Tag = null;
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