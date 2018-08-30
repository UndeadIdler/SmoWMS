using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.CommLib;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.Enum;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.UI.Layout;
using SMOWMS.DTOs.OutputDTO;

namespace SMOWMS.UI.AssetsManager
{
    partial class frmAssOut : Smobiler.Core.Controls.MobileForm
    {
        #region  �������
        private AutofacConfig _autofacConfig = new AutofacConfig();//����������
        private string UserId;
        private DataTable SNTable = new DataTable(); //���̵���ʲ�
        private static object lockobj = new object();
        private List<string> snList=new List<string>();  //�ʲ��ĳ�ʼ�б�
        public string SOID;
        public bool IsFromSO;
        private List<string> TemplateIds=new List<string>();
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (snList.Count == 0)
                {
                    throw new Exception("�����Ҫ��������кţ�");
                }
                ReturnInfo rInfo = new ReturnInfo();
                AssSalesInputDto inputDto = new AssSalesInputDto
                {
                    SOID = txtSOID.Text,
                    UserId = UserId,
                    sns = snList
                };
                rInfo = _autofacConfig.AssSalesOrderService.SaleAss(inputDto);
                if (rInfo.IsSuccess)
                {
                    ShowResult = ShowResult.Yes;
                    //                    Toast("����ɹ���");
                    snList.Clear();
                    SNTable.Rows.Clear();
                    var so = _autofacConfig.AssSalesOrderService.GetById(txtSOID.Text);
                    if (so != null)
                    {
                        if (IsFromSO)
                        {
                            if (so.STATUS == (int) SalesOrderStatus.�����)
                            {
                                Toast("������ɣ�");
                                Close();

                            }
                            else
                            {
                                Toast("����ɹ���");
                            }
                        }
                        else
                        {
                            if (so.STATUS == (int)SalesOrderStatus.�����)
                            {
                                Toast("������ɣ�");
                                txtSOID.Text = "";
                                txtSOID.Tag = null;
                            }
                            else
                            {
                                Toast("����ɹ���");
                            }
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
        /// �ֻ�ɨ���ά��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelScan_Press(object sender, EventArgs e)
        {
            try
            {
                bcScanForSN.GetBarcode();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ɨ�赽���к�ʱ
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
                    bool isExists = _autofacConfig.SettingService.SOSNIsExists(barCode,TemplateIds);
                    if (isExists)
                    {
                        Assets assets = _autofacConfig.SettingService.GetBySN(barCode);
                        AddSnToDataTable(barCode,assets.IMAGE);
                    }
                    else
                    {
                        throw new Exception("�����кŲ��ڿ��У�");
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
                    bool isExists = _autofacConfig.SettingService.SOSNIsExists(barCode,TemplateIds);
                    if (isExists)
                    {
                        Assets assets = _autofacConfig.SettingService.GetBySN(barCode);
                        AddSnToDataTable(barCode,assets.IMAGE);
                    }
                    else
                    {
                        throw new Exception("�����кŲ��ڿ��У�");
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
                bool isExists = _autofacConfig.SettingService.SOSNIsExists(RFID,TemplateIds);
                if (isExists)
                {
                    Assets assets = _autofacConfig.SettingService.GetBySN(RFID);
                    AddSnToDataTable(RFID,assets.IMAGE);
                }
                else
                {
//                    throw new Exception("�����кŲ��ڿ��У�");
                }
            }
        }

        /// <summary>
        /// ɨ�赽���۵����ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bcScanForSOID_BarcodeScanned(object sender, BarcodeResultArgs e)
        {
            try
            {
                string barCode = e.Value;
                bool IsExist = _autofacConfig.AssSalesOrderService.SOIDIsExist(barCode);
                if (IsExist)
                {
                    txtSOID.Text = barCode;
                    SOID = barCode;
                    List<AssTempOutputDto> tempOutputDtos = _autofacConfig.AssSalesOrderService.GetTemplateList(barCode);
                    TemplateIds.Clear();
                    foreach (var tempOutput in tempOutputDtos)
                    {
                        TemplateIds.Add(tempOutput.TEMPLATEID);
                    }

                }
                else
                {
                    throw new Exception("��ɨ����ȷ�����۵����");
                }
                
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
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
                bcScanForSOID.GetBarcode();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// �����ʼ��ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssOut_Load(object sender, EventArgs e)
        {
            try
            {
                //��Ӹ�������
                if (SNTable.Columns.Count == 0)
                {
                    SNTable.Columns.Add("SN");
                    SNTable.Columns.Add("IMAGE");
                }
                DataColumn[] keys = new DataColumn[1];
                keys[0] = SNTable.Columns["SN"];
                SNTable.PrimaryKey = keys;
                txtSOID.Text = SOID;

                UserId = Client.Session["UserID"].ToString();
                if (!string.IsNullOrEmpty(SOID))
                {
                    txtSOID.Text = SOID;
                    ImgBtnForSOID.Visible = false;
                    txtSOID.Size = new Size(200, 30);
                    var tempOutputDtos = _autofacConfig.AssSalesOrderService.GetTemplateList(SOID);
                    foreach (var tempOutput in tempOutputDtos)
                    {
                        TemplateIds.Add(tempOutput.TEMPLATEID);
                    }
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
        private void frmAssOut_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();

            }
        }

        /// <summary>
        /// ��ɨ�赽���ʲ���ӵ���Ӧ��Dictionary
        /// </summary>
        /// <param name="SN">���к�</param>
        private void AddSnToDataTable(string SN,string Image)
        {
            lock (lockobj)
            {
                DataRow row = SNTable.Rows.Find(SN);
                if (row == null)
                {
                    DataRow newRow = SNTable.NewRow();
                    newRow["SN"] = SN;
                    newRow["IMAGE"] = Image;
                    SNTable.Rows.Add(newRow);

                    var newdt = SNTable.Clone();
                    newdt.ImportRow(newRow);
                    snList.Add(SN);
                    lvSN.NewRow(newdt, "");
                }
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
            }
        }
    }
}