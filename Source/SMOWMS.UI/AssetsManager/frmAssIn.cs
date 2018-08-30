using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Smobiler.Core.Controls;
using Smobiler.Plugins.RongIM;
using SMOWMS.CommLib;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.Enum;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.DTOs.OutputDTO;

namespace SMOWMS.UI.AssetsManager
{
    partial class frmAssIn : Smobiler.Core.Controls.MobileForm
    {
        #region  �������
        private AutofacConfig _autofacConfig = new AutofacConfig();//����������
        private string UserId;
        private DataTable SNTable = new DataTable(); //���̵���ʲ�
        private static object lockobj = new object();
        private List<string> snList=new List<string>();  //�ʲ��ĳ�ʼ�б�
        public string POID;
        public bool IsFromPO;
        private string Image;
        private decimal Total;
        #endregion

        /// <summary>
        /// ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                string WareId = "";
                string STID = "";
                string SLID = "";
                if (txtSLID.Tag != null)
                {
                    string[] Ids = txtSLID.Tag.ToString().Split('/');
                    WareId = Ids[0];
                    if (Ids.Length == 3)
                    {
                        STID = Ids[1];
                        SLID = Ids[2];
                    }
                }
                if (string.IsNullOrEmpty(SLID))
                {
                    throw new Exception("��ɨ����ȷ�Ŀ�λ��");
                }
                if (snList.Count == 0)
                {
                    throw new Exception("�����Ҫ�������кţ�");
                }
                ReturnInfo rInfo = new ReturnInfo();
                AssInStorageInputDto inputDto = new AssInStorageInputDto
                {
                    POID = txtPOID.Text,
                    SLID = SLID,
                    STID = STID,
                    WAREID = WareId,
                    TEMPLATEID = btnTemplate.Tag.ToString(),
                    UserId = UserId,
                    SnList = snList
                };
                rInfo = _autofacConfig.AssPurchaseOrderService.InstorageAss(inputDto);
                if (rInfo.IsSuccess)
                {
                    ShowResult = ShowResult.Yes;
//                    Toast("���ɹ���");
                    btnTemplate.Text = "ѡ�񣨱��   > ";
                    btnTemplate.Tag = null;
                    txtSLID.Text = "�����";
                    txtSLID.Tag = null;
                    snList.Clear();
                    lvSN.Rows.Clear();
                    SNTable.Rows.Clear();
                    //��ѯ�ɹ���״̬
                    var po = _autofacConfig.AssPurchaseOrderService.GetById(txtPOID.Text);
                    if (IsFromPO)
                    {
                        //���ȫ����ɣ���ر�
                        if (po.STATUS == (int) PurchaseOrderStatus.�����)
                        {
                            Toast("�ö�����ȫ�������ɣ�");
                            Close();
                        }
                        else
                        {
                            Toast("���ɹ���");
                            DealLastTemp();
                            GetTotal();
                            lblQuant.Text = GetRest();
                        }
                    }
                    else
                    {
                        //���ȫ����ɣ�����ʾ�ö����Ѿ�������
                        if (po.STATUS == (int)PurchaseOrderStatus.�����)
                        {
                            Toast("�ö�����ȫ�������ɣ�");
                            txtPOID.Text = "";
                            txtPOID.Tag = null;
                        }
                        else
                        {
                            Toast("���ɹ���");
                            DealLastTemp();
                            GetTotal();
                            lblQuant.Text = GetRest();
                        }
                    }
                }
                else
                {
                    Toast(rInfo.ErrorInfo);
                }

            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ��ά��ɨ�����к�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelScan_Press(object sender, EventArgs e)
        {
            try
            {
                if (btnTemplate.Tag == null)
                {
                    throw new Exception("����ѡ��ģ��.");
                }
                bcScanForSN.GetBarcode();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// �����ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssIn_Load(object sender, EventArgs e)
        {
            try
            {
                //��Ӹ�������
                if (SNTable.Columns.Count == 0)
                {
                    SNTable.Columns.Add("IMAGE");
                    SNTable.Columns.Add("SN");
                }
                DataColumn[] keys = new DataColumn[1];
                keys[0] = SNTable.Columns["SN"];
                SNTable.PrimaryKey = keys;

                UserId = Client.Session["UserID"].ToString();
                if (!string.IsNullOrEmpty(POID))
                {
                    txtPOID.Text = POID;
                    txtPOID.Tag = POID;
                    ImgBtnForPOID.Visible = false;
                    txtPOID.Size = new Size(200, 30);
                    DealLastTemp();
                    GetTotal();
                    lblQuant.Text = GetRest();
                }
                
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// �ֻ�ɨ�赽�ɹ������ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bcScanForPOID_BarcodeScanned(object sender, BarcodeResultArgs e)
        {
            try
            {
                string barCode = e.Value;
                bool IsExist = _autofacConfig.AssPurchaseOrderService.POIDIsExist(barCode);
                if (IsExist)
                {
                    txtPOID.Text = barCode;
                    if (POID != barCode)
                    {
                        POID = barCode;
                        ClearInfo();
                    }
                    GetTotal();
                }
                else
                {
                    throw new Exception("��ɨ����ȷ�Ĳɹ�����ţ�");
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// �ֻ�ɨ�赽��λʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bcScanForSLID_BarcodeScanned(object sender, BarcodeResultArgs e)
        {
            try
            {
                string barCode = e.Value;
                string WareId = "";
                string STID = "";
                string SLID = "";

                string[] Ids = barCode.Split('/');
                WareId = Ids[0];
                if (Ids.Length == 3)
                {
                    STID = Ids[1];
                    SLID = Ids[2];
                }
                else
                {
                    throw new Exception("�ÿ�λ��Ų����ڣ�");
                }
                if (_autofacConfig.wareHouseService.SLIsExists(WareId, STID, SLID))
                {
                    WHStorageLocationOutputDto whLoc = _autofacConfig.wareHouseService.GetSLByID(WareId, STID, SLID);
                    if (whLoc == null) throw new Exception("��λ�����ڣ�����!");
                    txtSLID.Text = whLoc.WARENAME + "/" + whLoc.STNAME + "/" + whLoc.SLNAME;
                    txtSLID.Tag = barCode;
                }
                else
                {
                    throw new Exception("�ÿ�λ��Ų����ڣ�");
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// �ֻ�ɨ�赽���к�ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bcScanForSN_BarcodeScanned(object sender, BarcodeResultArgs e)
        {
            try
            {
                string barCode = e.Value;
                if (!snList.Contains(barCode))
                {
                    bool isExists = _autofacConfig.SettingService.SNIsExists(barCode);
                    if (!isExists)
                    {
                        AddSnToDataTable(barCode);
                    }
                    else
                    {
                        throw new Exception("�����к��Ѿ����ڣ�");
                    }
                }

            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// �ֳ�ɨ�赽���к�ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void r2000ScanForSN_BarcodeDataCaptured(object sender, Smobiler.Device.R2000BarcodeScanEventArgs e)
        {
            try
            {
                string barCode = e.Data;
                if (!snList.Contains(barCode))
                {
                    bool isExists = _autofacConfig.SettingService.SNIsExists(barCode);
                    if (!isExists)
                    {  AddSnToDataTable(barCode);}
                    else
                    {
                        throw new Exception("�����к��Ѿ����ڣ�");
                    }
                }

            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// �ֳ�ɨ�赽RFIDʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void r2000ScanForSN_RFIDDataCaptured(object sender, Smobiler.Device.R2000RFIDScanEventArgs e)
        {
            string RFID = e.Epc;
            if (!snList.Contains(RFID))
            {
                bool isExists = _autofacConfig.SettingService.SNIsExists(RFID);
                if (!isExists)
                    AddSnToDataTable(RFID);
            }
        }

        /// <summary>
        /// ���ˣ���رմ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssIn_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }

        /// <summary>
        /// ɨ�����۵����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgBtnForPOID_Press(object sender, EventArgs e)
        {
            try
            {
                bcScanForPOID.GetBarcode();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ɨ���λ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImgBtnForSLID_Press(object sender, EventArgs e)
        {
            try
            {
                bcScanForSLID.GetBarcode();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ѡ���ʲ�ģ��ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTemplate_Press(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPOID.Text))
                {
                    popTemp.Groups.Clear();
                    PopListGroup tempGroup = new PopListGroup {Title = "�ʲ�ģ��"};
                    List<AssTempOutputDto> assTemp =
                        _autofacConfig.AssPurchaseOrderService.GetTemplateList(txtPOID.Text);
                    foreach (AssTempOutputDto Row in assTemp)
                    {
                        tempGroup.AddListItem(Row.NAME, Row.TEMPLATEID);
                    }
                    popTemp.Groups.Add(tempGroup);
                    if (btnTemplate.Tag != null) //�������ѡ�������ʾѡ��Ч��
                    {
                        foreach (PopListItem Item in tempGroup.Items)
                        {
                            if (Item.Value == btnTemplate.Tag.ToString())
                                popTemp.SetSelections(Item);
                        }
                    }
                    popTemp.ShowDialog();
                }
                else
                {
                    throw new Exception("��������ɹ�����ţ�");
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ѡ���ʲ�ģ��ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popTemp_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popTemp.Selection != null)
                {
                    btnTemplate.Text = popTemp.Selection.Text + "   > ";
                    btnTemplate.Tag = popTemp.Selection.Value;
                    AssTemplate tempOutput = _autofacConfig.SettingService.GetAtbyId(btnTemplate.Tag.ToString());
                    Image = tempOutput.IMAGE;
                    GetTotal();

                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ��ɨ�赽���ʲ���ӵ���Ӧ��Dictionary
        /// </summary>
        /// <param name="SN">���к�</param>
        private void AddSnToDataTable(string SN)
        {
            lock (lockobj)
            {
                DataRow row = SNTable.Rows.Find(SN);
                if (row == null)
                {
                    DataRow newRow = SNTable.NewRow();
                    newRow["IMAGE"] = Image;
                    newRow["SN"] = SN;
                    SNTable.Rows.Add(newRow);

                    var newdt = SNTable.Clone();
                    newdt.ImportRow(newRow);
                    snList.Add(SN);
                    lvSN.NewRow(newdt, "");
                }
                lblQuant.Text = GetRest();
            }
        }

        /// <summary>
        /// �Ƴ����к�
        /// </summary>
        /// <param name="SN"></param>
        public void RemoveSN(string SN)
        {
            DataRow row = SNTable.Rows.Find(SN);
            if (row != null)
            {
                SNTable.Rows.Remove(row);
                snList.Remove(SN);
                lvSN.DataSource = SNTable;
                lvSN.DataBind();
                lblQuant.Text = GetRest();
            }
        }

        /// <summary>
        /// ����ʲ�ģ������
        /// </summary>
        private void ClearInfo()
        {
            try
            {
                btnTemplate.Tag = null;
                btnTemplate.Text="" + "   > ";
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        private string GetRest()
        {
            int sncount = snList.Count;
            int rest = (int)Total - sncount;
            return rest.ToString();
        }

        private void GetTotal()
        {
            //�õ���Ҫ�����SN����
            if (btnTemplate.Tag != null && txtPOID.Tag != null)
            {
                var porow = _autofacConfig.AssPurchaseOrderService.GetPORows(txtPOID.Tag.ToString());
                if (porow == null) throw new ArgumentNullException(nameof(porow));
                var row = porow.Find(a => a.TEMPLATEID == btnTemplate.Tag.ToString());
                if (row != null)
                {
                    Total = row.QUANTPURCHASED - row.QUANTSTORED;
                }
                
            }
        }

        private void DealLastTemp()
        {
            try
            {
                //�ж��Ƿ�ʣһ��
                //���ֻʣ���һ������ֱ��ѡ�У�����poplist
                List<AssTempOutputDto> assTemp =
                    _autofacConfig.AssPurchaseOrderService.GetTemplateList(txtPOID.Text);
                if (assTemp.Count == 1)
                {
                    btnTemplate.Text = assTemp.FirstOrDefault().NAME + "   > ";
                    btnTemplate.Tag = assTemp.FirstOrDefault().TEMPLATEID;
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}