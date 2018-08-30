using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using System.Data;
using SMOWMS.Domain.Entity;
using SMOWMS.UI.Layout;
using SMOWMS.DTOs.Enum;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.CommLib;
using SMOWMS.DTOs.OutputDTO;

namespace SMOWMS.UI.ConsumablesManager
{
    partial class frmConPurchaseCreate : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig autofacConfig = new AutofacConfig();     //����������
        public List<ConPurAndSaleCreateInputDto> Rows = new List<ConPurAndSaleCreateInputDto>();         //ѡ��ĺĲı��
        public string POID;        //�ĲĲɹ�����
        #endregion
        /// <summary>
        /// ҳ���ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConPurchaseCreate_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(POID))   //POID��Ϊ��,˵���ǲɹ����޸�
                {
                    this.Title1.TitleText = "�ĲĲɹ����༭";
                    ///��ͷ��Ϣ
                    ConPurchaseOrderOutputDto Order = autofacConfig.ConPurchaseOrderService.GetByPOID(POID);
                    txtRealID.Text = Order.REALID;
                    txtName.Text = Order.NAME;
                    btnDealMan.Tag = Order.PURCHASER;
                    btnDealMan.Text = Order.PURCHASERNAME + "   > ";

                    //�Ĳ�������Ϣ
                    List<ConPurAndSaleCreateInputDto> OrderRows = autofacConfig.ConPurchaseOrderService.GetOrderRows(POID);
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
        /// ɾ����ǰѡ������
        /// </summary>
        /// <param name="ASSID"></param>
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
        /// �ɹ���ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManager_Press(object sender, EventArgs e)
        {
            popDealMan.Groups.Clear();       //�������
            PopListGroup poli = new PopListGroup();
            popDealMan.Groups.Add(poli);
            poli.Title = "�ɹ���ѡ��";
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
        /// ѡ���˲ɹ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popDealMan_Selected(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(popDealMan.Selection.Text) == false)
            {
                btnDealMan.Text = popDealMan.Selection.Text + "   > ";
                btnDealMan.Tag = popDealMan.Selection.Value;         //�ɹ��˱��
            }
        }
        /// <summary>
        /// ������ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVendor_Press(object sender, EventArgs e)
        {
            popVendor.Groups.Clear();       //�������
            PopListGroup poli = new PopListGroup();
            popVendor.Groups.Add(poli);
            poli.Title = "������ѡ��";
            List<Vendor> vendorList = autofacConfig.vendorService.GetAll();
            foreach (Vendor Row in vendorList)
            {
                poli.AddListItem(Row.NAME, Row.VID.ToString());
            }
            if (btnVendor.Tag != null)   //�������ѡ�������ʾѡ��Ч��
            {
                foreach (PopListItem Item in poli.Items)
                {
                    if (Item.Value == btnVendor.Tag.ToString())
                        popVendor.SetSelections(Item);
                }
            }
            popVendor.ShowDialog();
        }
        /// <summary>
        /// ѡ���˹�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popVendor_Selected(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(popVendor.Selection.Text) == false)
            {
                btnVendor.Text = popVendor.Selection.Text + "   > ";
                btnVendor.Tag = popVendor.Selection.Value;         //�����̱��
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
                frm.type = 0;
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
        /// ɨ�赽��������ʱ
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
        /// �����ɹ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtName.Text)) throw new Exception("�ɹ������Ʋ���Ϊ��!");
                if (btnDealMan.Tag == null) throw new Exception("��ѡ��ɹ���!");
                ConPurchaseOrderInputDto Data = new ConPurchaseOrderInputDto();
                Data.REALID = txtRealID.Text;
                Data.NAME = txtName.Text;
                Data.PURCHASER = btnDealMan.Tag.ToString();
                Data.VID = Convert.ToInt32(btnVendor.Tag);
                Data.CREATEUSER = Client.Session["UserID"].ToString();
                Data.CREATEDATE = DateTime.Now;
                Data.MODIFYUSER = Client.Session["UserID"].ToString();
                Data.MODIFYDATE = DateTime.Now;
                List<ConPurchaseOrderRowInputDto> RowData = new List<ConPurchaseOrderRowInputDto>();
                //��ȡ��������
                foreach (ListViewRow Row in ListCons.Rows)
                {
                    frmPurchaseCreateLayout Layout = Row.Control as frmPurchaseCreateLayout;
                    ConPurAndSaleCreateInputDto conPurAndSaleCreateInputDto = Layout.getData();
                    ConPurchaseOrderRowInputDto row = new ConPurchaseOrderRowInputDto();
                    row.CID = conPurAndSaleCreateInputDto.CID;
                    row.IMAGE = conPurAndSaleCreateInputDto.IMAGE;
                    row.QUANTPURCHASED = conPurAndSaleCreateInputDto.QUANTPURCHASED;
                    row.REALPRICE = conPurAndSaleCreateInputDto.REALPRICE;
                    RowData.Add(row);
                }

                Data.RowData = RowData;
                ReturnInfo RInfo = new ReturnInfo();

                if (String.IsNullOrEmpty(POID))     //�����ĲĲɹ���
                {
                    RInfo = autofacConfig.ConPurchaseOrderService.AddPurchaseOrder(Data);
                    if (RInfo.IsSuccess)     //�����ɹ�
                    {
                        this.Close();          //�رյ�ǰ���棬���ص��б����
                        ShowResult = ShowResult.Yes;
                        Toast("�ɹ���" + RInfo.ErrorInfo + "�����ɹ�!");
                    }
                    else       //����ʧ��
                    {
                        throw new Exception(RInfo.ErrorInfo);
                    }
                }
                else               //���ºĲĲɹ���
                {
                    Data.POID = POID;
                    RInfo = autofacConfig.ConPurchaseOrderService.UpdatePruchaseOrder(Data);
                    if (RInfo.IsSuccess)     //�����ɹ�
                    {
                        this.Close();          //�رյ�ǰ���棬���ص��б����
                        ShowResult = ShowResult.Yes;
                        Toast("�ɹ���" + POID + "�༭�ɹ�!");
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