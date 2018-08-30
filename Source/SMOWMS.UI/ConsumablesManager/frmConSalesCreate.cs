using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.UI.Layout;
using SMOWMS.CommLib;
using SMOWMS.DTOs.OutputDTO;

namespace SMOWMS.UI.ConsumablesManager
{
    partial class frmConSalesCreate : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig autofacConfig = new AutofacConfig();     //����������
        public List<ConPurAndSaleCreateInputDto> Rows = new List<ConPurAndSaleCreateInputDto>();         //ѡ��ĺĲı��
        public string SOID;        //�Ĳ����۵���
        #endregion
        /// <summary>
        /// ҳ���ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConSalesCreate_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(SOID))   //SOID��Ϊ��,˵�������۵��޸�
                {
                    this.Title1.TitleText = "�ĲĲɹ����༭";
                    ///��ͷ��Ϣ
                    ConSalesOrderOutputDto Order = autofacConfig.ConSalesOrderService.GetBySOID(SOID);
                    txtRealID.Text = Order.REALID;
                    txtName.Text = Order.NAME;
                    btnDealMan.Tag = Order.SALESPERSON;
                    btnDealMan.Text = Order.SALESPERSONNAME + "   > ";

                    //�Ĳ�������Ϣ
                    List<ConPurAndSaleCreateInputDto> OrderRows = autofacConfig.ConSalesOrderService.GetOrderRows(SOID);
                    if (OrderRows.Count > 0)
                    {
                        Rows = OrderRows;
                        Bind();
                    }
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ������ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManager_Press(object sender, EventArgs e)
        {
            popDealMan.Groups.Clear();       //�������
            PopListGroup poli = new PopListGroup();
            popDealMan.Groups.Add(poli);
            poli.Title = "������ѡ��";
            List<coreUser> users = autofacConfig.coreUserService.GetAll();
            foreach (coreUser Row in users)
            {
                poli.AddListItem(Row.USER_NAME, Row.USER_ID);
            }
            if (btnDealMan.Tag != null)   //�������ѡ�������ʾѡ��Ч��
            {
                foreach (PopListItem Item in poli.Items)
                {
                    if (Item.Value == btnDealMan.Tag.ToString())
                        popDealMan.SetSelections(Item);
                }
            }
            popDealMan.ShowDialog();
        }
        /// <summary>
        /// ѡ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popDealMan_Selected(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(popDealMan.Selection.Text) == false)
            {
                btnDealMan.Text = popDealMan.Selection.Text + "   > ";
                btnDealMan.Tag = popDealMan.Selection.Value;         //�����˱��
            }
        }
        /// <summary>
        /// �ͻ�ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCustomer_Press(object sender, EventArgs e)
        {
            popCustomer.Groups.Clear();       //�������
            PopListGroup poli = new PopListGroup();
            popCustomer.Groups.Add(poli);
            poli.Title = "������ѡ��";
            List<Customer> customerList = autofacConfig.customerService.GetAll();
            foreach (Customer Row in customerList)
            {
                poli.AddListItem(Row.NAME, Row.CUSID.ToString());
            }
            if (btnCustomer.Tag != null)   //�������ѡ�������ʾѡ��Ч��
            {
                foreach (PopListItem Item in poli.Items)
                {
                    if (Item.Value == btnCustomer.Tag.ToString())
                        popCustomer.SetSelections(Item);
                }
            }
            popCustomer.ShowDialog();
        }
        /// <summary>
        /// ѡ���˿ͻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popCustomer_Selected(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(popCustomer.Selection.Text) == false)
            {
                btnCustomer.Text = popCustomer.Selection.Text + "   > ";
                btnCustomer.Tag = popCustomer.Selection.Value;         //�ͻ����
            }
        }
        /// <summary>
        /// ɾ����ǰѡ������
        /// </summary>
        /// <param name="CID"></param>
        public void ReMoveAss(String CID)
        {
            foreach (ConPurAndSaleCreateInputDto Con in Rows)
            {
                if (Con.CID == CID)
                {
                    Rows.Remove(Con);
                    break;
                }
            }
            Bind();       //ˢ�µ�ǰҳ��
        }
        /// <summary>
        /// ���ݼ���
        /// </summary>
        public void Bind()
        {
            try
            {
                if (Rows.Count > 0)
                {
                    ListCons.DataSource = Rows;
                    ListCons.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ����Ĳ�ѡ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Press(object sender, EventArgs e)
        {
            try
            {
                List<ConPurAndSaleCreateInputDto> Data = new List<ConPurAndSaleCreateInputDto>();
                foreach (ListViewRow Row in ListCons.Rows)
                {
                    frmPurchaseCreateLayout Layout = Row.Control as frmPurchaseCreateLayout;
                    Data.Add(Layout.getData());
                }

                frmConsChoose frm = new frmConsChoose();
                frm.Rows = Data;
                frm.type = 1;
                Show(frm, (MobileForm sender1, object args) =>
                {
                    if (frm.ShowResult == ShowResult.Yes)
                    {
                        //���¼�������
                        Rows = frm.Rows;
                        Bind();
                    }
                });
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ɨ����ӺĲ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void betGet_Press(object sender, EventArgs e)
        {
            bsCons.GetBarcode();
        }
        /// <summary>
        /// ɨ�赽�Ĳı��ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsCons_BarcodeScanned(object sender, BarcodeResultArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.error))
                {
                    String CID = e.Value;
                    Consumables con = autofacConfig.consumablesService.GetConsById(CID);
                    if (con != null)
                    {
                        ConPurAndSaleCreateInputDto conDto = new ConPurAndSaleCreateInputDto();
                        conDto.CID = con.CID;
                        conDto.NAME = con.NAME;
                        conDto.IMAGE = con.IMAGE;
                        if (Rows.Contains(conDto))
                        {
                            throw new Exception("�úĲ�����ӣ������ظ����!");
                        }
                        else
                        {
                            Rows.Add(conDto);
                            Bind();
                        }
                    }
                    else
                    {
                        throw new Exception("���Ϊ" + CID + "�ĲĲ����ڣ�����!");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// �Ĳ����۵�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtName.Text)) throw new Exception("���۵����Ʋ���Ϊ��!");
                if (btnDealMan.Tag == null) throw new Exception("��ѡ��������!");
                ConSalesOrderInputDto Data = new ConSalesOrderInputDto();
                Data.REALID = txtRealID.Text;
                Data.NAME = txtName.Text;
                Data.CUSID = Convert.ToInt32(btnCustomer.Tag);
                Data.SALESPERSON = btnDealMan.Tag.ToString();
                Data.CREATEUSER = Client.Session["UserID"].ToString();
                Data.CREATEDATE = DateTime.Now;
                Data.MODIFYUSER = Client.Session["UserID"].ToString();
                Data.MODIFYDATE = DateTime.Now;
                List<ConSalesOrderRowInputDto> RowData = new List<ConSalesOrderRowInputDto>();
                //��ȡ��������
                foreach (ListViewRow Row in ListCons.Rows)
                {
                    frmPurchaseCreateLayout Layout = Row.Control as frmPurchaseCreateLayout;
                    ConPurAndSaleCreateInputDto conPurAndSaleCreateInputDto = Layout.getData();
                    ConSalesOrderRowInputDto row = new ConSalesOrderRowInputDto();
                    row.CID = conPurAndSaleCreateInputDto.CID;
                    row.IMAGE = conPurAndSaleCreateInputDto.IMAGE;
                    row.QUANTSALED = conPurAndSaleCreateInputDto.QUANTPURCHASED;
                    row.REALPRICE = conPurAndSaleCreateInputDto.REALPRICE;
                    RowData.Add(row);
                }

                Data.RowData = RowData;
                ReturnInfo RInfo = new ReturnInfo();

                if (String.IsNullOrEmpty(SOID))     //�����ĲĲɹ���
                {
                    RInfo = autofacConfig.ConSalesOrderService.AddSalesOrder(Data);
                    if (RInfo.IsSuccess)     //�����ɹ�
                    {
                        this.Close();          //�رյ�ǰ���棬���ص��б����
                        ShowResult = ShowResult.Yes;
                        Toast("���۵�" + RInfo.ErrorInfo + "�����ɹ�!");
                    }
                    else       //����ʧ��
                    {
                        throw new Exception(RInfo.ErrorInfo);
                    }
                }
                else               //���ºĲĲɹ���
                {
                    Data.SOID = SOID;
                    RInfo = autofacConfig.ConSalesOrderService.UpdateSalesOrder(Data);
                    if (RInfo.IsSuccess)     //�����ɹ�
                    {
                        this.Close();          //�رյ�ǰ���棬���ص��б����
                        ShowResult = ShowResult.Yes;
                        Toast("���۵�" + SOID + "�༭�ɹ�!");
                    }
                    else       //����ʧ��
                    {
                        throw new Exception(RInfo.ErrorInfo);
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