using System;
using System.Collections.Generic;
using Smobiler.Core.Controls;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.Domain.Entity;
using SMOWMS.UI.Layout;
using SMOWMS.CommLib;
using System.Data;
using SMOWMS.DTOs.Enum;
using SMOWMS.DTOs.OutputDTO;

namespace SMOWMS.UI.ConsumablesManager
{
    partial class frmTransferCreate : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig autofacConfig = new AutofacConfig();     //����������
        public List<ConsumablesOrderRow> RowData = new List<ConsumablesOrderRow>();    //δ����SN����
        public String CID;               //�Ĳı��
        #endregion
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                TOInputDto TransferData = new TOInputDto();        //ά�޵���Ϣ
                if (btnDealInMan.Tag == null)
                    throw new Exception("�������Ա����Ϊ��");
                else
                    TransferData.MANAGER = btnDealInMan.Tag.ToString();     //�������Ա

                if (lblLocation.Tag == null)
                    throw new Exception("�����λ����Ϊ��");
                else
                {
                    string[] Datas = lblLocation.Tag.ToString().Split('/');
                    TransferData.WAREID = Datas[0];     //����ֿ�
                    TransferData.STID = Datas[1];       //��������
                    TransferData.DESSLID = Datas[2];    //�����λ
                }


                if (btnDealMan.Tag == null)
                    throw new Exception("�����˲���Ϊ��");
                else
                    TransferData.HANDLEMAN = btnDealMan.Tag.ToString();     //������

                TransferData.TRANSFERDATE = DatePicker.Value;   //ά�޻���
                TransferData.NOTE = txtNote.Text;                           //��ע
                TransferData.STATUS = 0;                                    //ά�޵�״̬
                TransferData.CREATEUSER = Client.Session["UserID"].ToString();      //�����û�
                TransferData.CREATEDATE = DateTime.Now;

                List<AssTransferOrderRow> Data = new List<AssTransferOrderRow>();
                if (ListCons.Rows.Count == 0) throw new Exception("���������Ϊ��!");
                foreach (ListViewRow Row in ListCons.Rows)
                {
                    frmOrderCreateLayout Layout = Row.Control as frmOrderCreateLayout;
                    ConsumablesOrderRow RowData = Layout.getData();
                    AssTransferOrderRow assRow = new AssTransferOrderRow();

                    assRow.IMAGE = RowData.IMAGE;
                    assRow.CID = RowData.CID;
                    assRow.INTRANSFERQTY = RowData.QTY;
                    assRow.WAREID = RowData.WAREID;
                    assRow.STID = RowData.STID;
                    assRow.SLID = RowData.SLID;
                    assRow.STATUS = RowData.STATUS;
                    assRow.CREATEDATE = DateTime.Now;
                    Data.Add(assRow);
                }
                TransferData.Rows = Data;
                ReturnInfo r = autofacConfig.assTransferOrderService.AddAssTransferOrder(TransferData, OperateType.�Ĳ�);
                if (r.IsSuccess)
                {
                    ShowResult = ShowResult.Yes;
                    Form.Close();          //�����ɹ�
                    Toast("�����������ɹ�!");
                }
                else
                {
                    throw new Exception(r.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// �������Աѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDealInMan_Press(object sender, EventArgs e)
        {
            try
            {
                popDealInMan.Groups.Clear();       //�������
                PopListGroup poli = new PopListGroup();
                popDealInMan.Groups.Add(poli);
                poli.Title = "�������Աѡ��";
                List<coreUser> users = autofacConfig.coreUserService.GetDealInAdmin();
                foreach (coreUser Row in users)
                {
                    poli.AddListItem(Row.USER_NAME, Row.USER_ID);
                }
                if (btnDealMan.Tag != null)   //�������ѡ�������ʾѡ��Ч��
                {
                    foreach (PopListItem Item in poli.Items)
                    {
                        if (Item.Value == btnDealMan.Tag.ToString())
                            popDealInMan.SetSelections(Item);
                    }
                }
                popDealInMan.ShowDialog();
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
        private void btnDealMan_Press(object sender, EventArgs e)
        {
            try
            {
                popDealMan.Groups.Clear();       //�������
                PopListGroup poli = new PopListGroup();
                popDealMan.Groups.Add(poli);
                poli.Title = "������ѡ��";
                List<coreUser> users = autofacConfig.coreUserService.GetAdmin();
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
                DataTable tableAssets = new DataTable();       //δ����SN���ʲ��б�
                tableAssets.Columns.Add("CID");                //�ʲ����
                tableAssets.Columns.Add("NAME");               //�ʲ����� 
                tableAssets.Columns.Add("LOCID");              //   �ֿ�/�洢����/��λ
                tableAssets.Columns.Add("LOCNAME");            //   �ֿ�����/��������/��λ����
                tableAssets.Columns.Add("IMAGE");              //ͼƬ���
                tableAssets.Columns.Add("QUANTITY");           //��������
                tableAssets.Columns.Add("SELECTQTY");          //ѡ������
                if (RowData.Count > 0)
                {
                    foreach (ConsumablesOrderRow Row in RowData)
                    {
                        ConQuant conQuant = autofacConfig.orderCommonService.GetUnUseConByCID(Row.CID, Row.WAREID, Row.STID, Row.SLID);
                        Consumables con = autofacConfig.consumablesService.GetConsById(Row.CID);
                        WHStorageLocationOutputDto WHLoc = autofacConfig.wareHouseService.GetSLByID(Row.WAREID, Row.STID, Row.SLID);
                        tableAssets.Rows.Add(Row.CID, con.NAME, Row.WAREID + "/" + Row.STID + "/" + Row.SLID, WHLoc.WARENAME + "/" + WHLoc.STNAME + "/" + WHLoc.SLNAME, con.IMAGE, conQuant.QUANTITY, Row.QTY);
                    }
                }
                ListCons.DataSource = tableAssets;
                ListCons.DataBind();
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
            foreach (ConsumablesOrderRow Row in RowData)
            {
                if (Row.CID == CID)
                {
                    RowData.Remove(Row);
                    break;
                }
            }
            Bind();       //ˢ�µ�ǰҳ��
        }
        /// <summary>
        /// �Ĳ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Press(object sender, EventArgs e)
        {
            try
            {
                if (lblLocation.Tag == null) throw new Exception("��ѡ������λ!");
                List<ConsumablesOrderRow> Data = new List<ConsumablesOrderRow>();
                foreach (ListViewRow Row in ListCons.Rows)
                {
                    frmOrderCreateLayout Layout = Row.Control as frmOrderCreateLayout;
                    Data.Add(Layout.getData());
                }

                frmTransferConsChoose frm = new frmTransferConsChoose();
                frm.RowData = Data;
                frm.LocInID = lblLocation.Tag.ToString();
                Show(frm, (MobileForm sender1, object args) =>
                {
                    if (frm.ShowResult == ShowResult.Yes)
                    {
                        //���¼�������
                        RowData = frm.RowData;
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
        /// ɨ����������ʲ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void betGet_Press(object sender, EventArgs e)
        {
            try
            {
                if (lblLocation.Tag == null) throw new Exception("��ѡ������λ!");
                BarcodeScanner1.GetBarcode();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ɨ�赽����ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BarcodeScanner1_BarcodeScanned(object sender, BarcodeResultArgs e)
        {
            try
            {
                ConsumablesOrderRow Data = new ConsumablesOrderRow();
                if (string.IsNullOrEmpty(e.error))
                    CID = e.Value;
                else
                    throw new Exception(e.error);
                string[] LCData = lblLocation.Tag.ToString().Split('/');
                List<ConQuantOutputDto> assList = autofacConfig.orderCommonService.GetUnUseCon(LCData[0], LCData[1], LCData[2], CID);
                if (assList.Count > 1)
                {
                    popLocation.Groups.Clear();
                    PopListGroup poli = new PopListGroup();
                    popLocation.Groups.Add(poli);
                    foreach (ConQuantOutputDto Row in assList)
                    {
                        if (Row.WAREID + "/" + Row.STID + "/" + Row.SLID != lblLocation.Tag.ToString())
                        {
                            poli.AddListItem(Row.WARENAME + "/" + Row.STNAME + "/" + Row.SLNAME, Row.WAREID + "/" + Row.STID + "/" + Row.SLID);
                        }
                    }
                    popLocation.ShowDialog();
                }
                else
                {
                    if (assList[0].WAREID + "/" + assList[0].STID + "/" + assList[0].SLID == lblLocation.Tag.ToString()) throw new Exception("���ʲ�����Ŀ�Ŀ���!");
                    Consumables cons = autofacConfig.consumablesService.GetConsById(CID);
                    Data.CID = CID;
                    Data.WAREID = assList[0].WAREID;
                    Data.STID = assList[0].STID;
                    Data.SLID = assList[0].SLID;
                    Data.IMAGE = cons.IMAGE;
                    Data.QTY = 0;
                    if (RowData.Count > 0)
                    {
                        RowData.Add(Data);
                    }
                    else
                    {
                        List<ConsumablesOrderRow> Datas = new List<ConsumablesOrderRow>();
                        Datas.Add(Data);
                        RowData = Datas;
                    }
                    Bind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ѡ������
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
        /// ѡ��������Ա
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popDealInMan_Selected(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(popDealInMan.Selection.Text) == false)
            {
                btnDealInMan.Text = popDealInMan.Selection.Text + "   > ";
                btnDealInMan.Tag = popDealInMan.Selection.Value;         //�������Ա���
            }
        }
        /// <summary>
        /// ɨ���λ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgBS_Press(object sender, EventArgs e)
        {
            bsLoc.GetBarcode();
        }
        /// <summary>
        /// ɨ�赽��λ����ʱ
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
        /// ѡ���˿�λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popLocation_Selected(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(popLocation.Selection.Text) == false)
                {
                    String[] locData = popLocation.Selection.Value.Split('/');
                    Consumables cons = autofacConfig.consumablesService.GetConsById(CID);
                    ConsumablesOrderRow Data = new ConsumablesOrderRow();
                    Data.CID = CID;
                    Data.WAREID = locData[0];
                    Data.STID = locData[1];
                    Data.SLID = locData[2];
                    Data.IMAGE = cons.IMAGE;
                    Data.QTY = 0;
                    if (RowData.Count > 0)
                    {
                        RowData.Add(Data);
                    }
                    else
                    {
                        List<ConsumablesOrderRow> Datas = new List<ConsumablesOrderRow>();
                        Datas.Add(Data);
                        RowData = Datas;
                    }
                    Bind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}