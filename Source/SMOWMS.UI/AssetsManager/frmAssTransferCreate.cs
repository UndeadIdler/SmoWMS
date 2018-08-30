using System;
using System.Collections.Generic;
using Smobiler.Core.Controls;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.Domain.Entity;
using SMOWMS.CommLib;
using SMOWMS.UI.Layout;
using System.Data;
using SMOWMS.DTOs.Enum;
using SMOWMS.DTOs.OutputDTO;

namespace SMOWMS.UI.AssetsManager
{
    partial class frmAssTransferCreate : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig autofacConfig = new AutofacConfig();     //����������
        public List<AssetsOrderRow> SNRowData;   //����SN����
        public TOInputDto TransferData = new TOInputDto();         //��������Ϣ
        private String SN;       //���к�
        #endregion
        /// <summary>
        /// ��������
        /// </summary>
        public void Bind()
        {
            try
            {
                DataTable tableAssets = new DataTable();       //δ����SN���ʲ��б�
                tableAssets.Columns.Add("ASSID");              //�ʲ����
                tableAssets.Columns.Add("NAME");               //�ʲ����� 
                tableAssets.Columns.Add("IMAGE");              //ͼƬ���
                tableAssets.Columns.Add("SN");                 //���к�
                if (SNRowData.Count > 0)
                {
                    foreach (AssetsOrderRow Row in SNRowData)
                    {
                        Assets assets = autofacConfig.orderCommonService.GetAssetsByID(Row.ASSID);
                        tableAssets.Rows.Add(Row.ASSID, assets.NAME, assets.IMAGE, Row.SN);
                    }
                }
                ListAssetsSN.DataSource = tableAssets;
                ListAssetsSN.DataBind();
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
                AssetsOrderRow Data = new AssetsOrderRow();
                if (string.IsNullOrEmpty(e.error))
                {
                    SN = e.Value;
                }
                else
                    throw new Exception(e.error);
                Assets assets = autofacConfig.orderCommonService.GetUnusedAssetsBySN(SN);
                if (assets == null) throw new Exception("���������к�Ϊ" + SN + "�������ʲ�");
                if (assets.WAREID+"/" +assets.STID+"/"+assets.SLID == lblLocation.Tag.ToString()) throw new Exception("���ʲ�����Ŀ�Ŀ�λ!");
                Data.ASSID = assets.ASSID;
                Data.WAREID = assets.WAREID;
                Data.STID = assets.STID;
                Data.SLID = assets.SLID;
                Data.IMAGE = assets.IMAGE;
                Data.QTY = 0;
                Data.SN = SN;
                if (SNRowData != null)
                {
                    foreach (AssetsOrderRow Row in SNRowData)
                    {
                        if (Data.ASSID == Row.ASSID && Data.SN == Row.SN)
                            throw new Exception("���ʲ�����ӣ������ظ����!");
                    }
                    SNRowData.Add(Data);
                }
                else
                {
                    List<AssetsOrderRow> Datas = new List<AssetsOrderRow>();
                    Datas.Add(Data);
                    SNRowData = Datas;
                }
                Bind();        //���°�����
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ɾ����ǰ��ѡ����
        /// </summary>
        /// <param name="ASSID"></param>
        /// <param name="SN"></param>
        public void ReMoveAssSN(String ASSID, String SN)
        {
            try
            {
                foreach (AssetsOrderRow Row in SNRowData)
                {
                    if (Row.ASSID == ASSID && Row.SN == SN)
                    {
                        SNRowData.Remove(Row);
                        break;
                    }
                }
                Bind();       //ˢ�µ�ǰҳ��
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
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
                TransferData.ISSNCONTROL = (Int32)ISSNCONTROL.����;         //�Ƿ�����SN
                TransferData.CREATEUSER = Client.Session["UserID"].ToString();      //�����û�
                TransferData.CREATEDATE = DateTime.Now;

                List<AssTransferOrderRow> Data = new List<AssTransferOrderRow>();
                if (ListAssetsSN.Rows.Count == 0) throw new Exception("���������Ϊ��!");
                foreach (ListViewRow Row in ListAssetsSN.Rows)
                {
                    frmOrderCreateSNLayout Layout = Row.Control as frmOrderCreateSNLayout;
                    AssetsOrderRow RowData = Layout.getData();
                    AssTransferOrderRow assRow = new AssTransferOrderRow();
                    Assets assets = autofacConfig.orderCommonService.GetAssetsByID(RowData.ASSID);

                    assRow.IMAGE = RowData.IMAGE;
                    assRow.ASSID = RowData.ASSID;
                    assRow.INTRANSFERQTY = RowData.QTY;
                    assRow.SN = RowData.SN;
                    assRow.STATUS = RowData.STATUS;
                    assRow.WAREID = assets.WAREID;
                    assRow.STID = assets.STID;
                    assRow.SLID = assets.SLID;
                    assRow.CREATEDATE = DateTime.Now;
                    Data.Add(assRow);
                }
                TransferData.Rows = Data;
                ReturnInfo r = autofacConfig.assTransferOrderService.AddAssTransferOrder(TransferData, OperateType.�ʲ�);
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
        /// ���ֳ�ɨ�赽����ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void r2000Scanner1_BarcodeDataCaptured(object sender, Smobiler.Device.R2000BarcodeScanEventArgs e)
        {
            try
            {
                AssetsOrderRow Data = new AssetsOrderRow();
                SN = e.Data;
                Assets assets = autofacConfig.orderCommonService.GetUnusedAssetsBySN(SN);
                if (assets == null) throw new Exception("���������к�Ϊ" + SN + "�������ʲ�");
                if (assets.SLID == lblLocation.Tag.ToString()) throw new Exception("���ʲ�����Ŀ�Ŀ�λ!");
                Data.ASSID = assets.ASSID;
                Data.WAREID = assets.WAREID;
                Data.STID = assets.STID;
                Data.SLID = assets.SLID;
                Data.IMAGE = assets.IMAGE;
                Data.QTY = 0;
                Data.SN = SN;
                if (SNRowData != null)
                {
                    foreach (AssetsOrderRow Row in SNRowData)
                    {
                        if (Data.ASSID == Row.ASSID && Data.SN == Row.SN)
                            throw new Exception("���ʲ�����ӣ������ظ����!");
                    }
                    SNRowData.Add(Data);
                }
                else
                {
                    List<AssetsOrderRow> Datas = new List<AssetsOrderRow>();
                    Datas.Add(Data);
                    SNRowData = Datas;
                }
                Bind();        //���°�����
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ���ֳ�ɨ�赽RFIDʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void r2000Scanner1_RFIDDataCaptured(object sender, Smobiler.Device.R2000RFIDScanEventArgs e)
        {
            try
            {
                AssetsOrderRow Data = new AssetsOrderRow();
                SN = e.Epc;
                Assets assets = autofacConfig.orderCommonService.GetUnusedAssetsBySN(SN);
                if (assets == null) throw new Exception();
                if (assets.SLID == lblLocation.Tag.ToString()) throw new Exception();
                Data.ASSID = assets.ASSID;
                Data.WAREID = assets.WAREID;
                Data.STID = assets.STID;
                Data.SLID = assets.SLID;
                Data.IMAGE = assets.IMAGE;
                Data.QTY = 0;
                Data.SN = SN;
                if (SNRowData != null)
                {
                    foreach (AssetsOrderRow Row in SNRowData)
                    {
                        if (Data.ASSID == Row.ASSID && Data.SN == Row.SN)
                            throw new Exception();
                    }
                    SNRowData.Add(Data);
                }
                else
                {
                    List<AssetsOrderRow> Datas = new List<AssetsOrderRow>();
                    Datas.Add(Data);
                    SNRowData = Datas;
                }
                Bind();        //���°�����
            }
            catch (Exception ex)
            {
                if (ex.Message != "��������Ϊ��System.Exception�����쳣��")
                {
                    Toast(ex.Message);
                }
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
                    WHStorageLocationOutputDto whLoc = autofacConfig.wareHouseService.GetSLByID(Datas[0],Datas[1],Datas[2]);
                    if (whLoc == null) throw new Exception("��λ�����ڣ�����!");
                    lblLocation.Text = whLoc.WARENAME + "/" + whLoc.STNAME + "/" + whLoc.SLNAME;
                    lblLocation.Tag = Data;
                }                
            }
            catch(Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}