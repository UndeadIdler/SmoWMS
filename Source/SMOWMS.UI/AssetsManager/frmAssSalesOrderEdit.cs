using System;
using System.Collections.Generic;
using AutoMapper;
using Smobiler.Core.Controls;
using SMOWMS.CommLib;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.DTOs.OutputDTO;
using SMOWMS.UI.Layout;

namespace SMOWMS.UI.AssetsManager
{
    partial class frmAssSalesOrderEdit : Smobiler.Core.Controls.MobileForm
    {
        #region  �������
        private AutofacConfig _autofacConfig = new AutofacConfig();//����������
        public List<AssRowInputDto> Rows = new List<AssRowInputDto>();
        public List<string> TemplateIds = new List<string>();
        private string UserId;
        private string errorInfo;
        public string SOID;
        #endregion

        /// <summary>
        /// �༭���۵�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                errorInfo = "";
                if (btnDealMan.Tag == null) throw new Exception("��ѡ�������ˣ�");
                if (string.IsNullOrEmpty(txtName.Text)) throw new Exception("���������ƣ�");
                if (btnCus.Tag == null) throw new Exception("��ѡ��ͻ���");
                int CusId = int.Parse(btnCus.Tag.ToString());
                GetRows();
                if (!string.IsNullOrEmpty(errorInfo))
                {
                    return;
                }
                if (Rows.Count != 0)
                {
                    AssSalesOrderInputDto assSalesOrderInput = new AssSalesOrderInputDto()
                    {
                        SALESPERSON = btnDealMan.Tag.ToString(),
                        CREATEUSER = UserId,
                        MODIFYUSER = UserId,
                        NAME = txtName.Text,
                        REALID = txtRealID.Text,
                        STATUS = 0,
                        CUSID = CusId,
                        RowInputDtos = Rows,
                        SOID = SOID
                    };
                    ReturnInfo returnInfo =
                        _autofacConfig.AssSalesOrderService.UpdateSalesOrderOnly(assSalesOrderInput);
                    if (returnInfo.IsSuccess)
                    {
                        ShowResult = ShowResult.Yes;
                        Toast("�޸ĳɹ�");
                        Close();
                    }
                    else
                    {
                        throw new Exception(returnInfo.ErrorInfo);
                    }
                }
                else
                {
                    throw new Exception("��������");
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ѡ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManager_Press(object sender, EventArgs e)
        {
            try
            {
                popMan.Groups.Clear();
                PopListGroup manGroup = new PopListGroup { Title = "������ѡ��" };
                List<coreUser> users = _autofacConfig.coreUserService.GetAll();
                foreach (coreUser Row in users)
                {
                    manGroup.AddListItem(Row.USER_NAME, Row.USER_ID);
                }
                popMan.Groups.Add(manGroup);
                if (!string.IsNullOrEmpty(btnDealMan.Tag.ToString()))   //�������ѡ�������ʾѡ��Ч��
                {
                    foreach (PopListItem Item in manGroup.Items)
                    {
                        if (Item.Value == btnDealMan.Tag.ToString())
                            popMan.SetSelections(Item);
                    }
                }
                popMan.ShowDialog();

            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ѡ���ʲ�ģ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Press(object sender, EventArgs e)
        {
            try
            {
                GetRows();
                frmAssTemplateChoose frm = new frmAssTemplateChoose { Rows = Rows };
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
        /// ɨ���ʲ�ģ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void betGet_Press(object sender, EventArgs e)
        {
            bsAss.GetBarcode();
        }

        /// <summary>
        /// ɨ�赽�ʲ�ģ����ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bsAss_BarcodeScanned(object sender, BarcodeResultArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(e.error))
                {
                    String templateId = e.Value;
                    AssTemplate assTemplate = _autofacConfig.SettingService.GetAtbyId(templateId);
                    if (assTemplate != null)
                    {
                        if (TemplateIds.Contains(templateId))
                        {
                            throw new Exception("��ģ������ӣ������ظ����!");

                        }
                        else
                        {
                            TemplateIds.Add(templateId);
                            decimal price = 0;
                            if (assTemplate.PRICE != null)
                            {
                                price = assTemplate.PRICE.Value;
                            }
                            AssRowInputDto rowInputDto = new AssRowInputDto
                            {
                                TEMPLATEID = templateId,
                                QUANT = 0,
                                PRICE = 0,
                                TPRICE = price,
                                IMAGE = assTemplate.IMAGE,
                                NAME = assTemplate.NAME
                            };
                            Rows.Add(rowInputDto);
                            Bind();
                        }
                    }
                    else
                    {
                        throw new Exception("���Ϊ" + templateId + "ģ�岻���ڣ�����!");
                    }
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ѡ�вɹ���ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popDealMan_Selected(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(popMan.Selection.Text) == false)
            {
                btnDealMan.Text = popMan.Selection.Text + "   > ";
                btnDealMan.Tag = popMan.Selection.Value;         //�ɹ��˱��
            }
        }

        /// <summary>
        /// ���ݰ�
        /// </summary>
        public void Bind()
        {
            try
            {
                if (Rows.Count > 0)
                {
                    ListATs.DataSource = Rows;
                    ListATs.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// �õ���������
        /// </summary>
        public void GetRows()
        {
            try
            {
                foreach (ListViewRow Row in ListATs.Rows)
                {
                    frmAssSORowLayout layout = Row.Control as frmAssSORowLayout;
                    if (layout != null)
                    {
                        AssRowInputDto rowInputDto = Rows.Find(a => a.TEMPLATEID == layout.LblTId.Text);
                        rowInputDto.QUANT = (decimal)layout.numQuant.Value;
                        rowInputDto.PRICE = (decimal)layout.numPrice.Value;
                        if (rowInputDto.QUANT == 0 || rowInputDto.PRICE == 0)
                        {
                            errorInfo = "�뱣֤�����е������͵��۾���Ϊ0��";
                            throw new Exception("�뱣֤�����е������͵��۾���Ϊ0��");
                        }
                    }
                }
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
        private void frmAssSalesOrderEdit_Load(object sender, EventArgs e)
        {
            try
            {
                UserId = Client.Session["UserID"].ToString();
                var assso = _autofacConfig.AssSalesOrderService.GetById(SOID);
                if (assso != null)
                {
                    txtPOID.Text = SOID;
                    txtName.Text = assso.NAME;
                    txtRealID.Text = assso.REALID;
                    btnDealMan.Tag = assso.SALESPERSON;
                    btnDealMan.Text = assso.SALESPERSONNAME + "   > ";
                    btnCus.Tag = assso.CUSID;
                    btnCus.Text=assso.CUSNAME + "   > ";
                }
                var assSorow = _autofacConfig.AssSalesOrderService.GetSORows(SOID);
                if (assSorow != null)
                {
                    Rows = Mapper.Map<List<AssSORowOutputDto>, List<AssRowInputDto>>(assSorow);
                    ListATs.DataSource = Rows;
                    ListATs.DataBind();
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
        private void frmAssSalesOrderEdit_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }

        private void btnCus_Press(object sender, EventArgs e)
        {
            try
            {
                popCus.Groups.Clear();
                PopListGroup cusGroup = new PopListGroup { Title = "�ͻ�ѡ��" };
                List<Customer> customers = _autofacConfig.customerService.GetAll();
                foreach (Customer Row in customers)
                {
                    cusGroup.AddListItem(Row.NAME, Row.CUSID.ToString());
                }
                popCus.Groups.Add(cusGroup);
                if (btnCus.Tag != null)   //�������ѡ�������ʾѡ��Ч��
                {
                    foreach (PopListItem Item in cusGroup.Items)
                    {
                        if (Item.Value == btnCus.Tag.ToString())
                            popCus.SetSelections(Item);
                    }
                }
                popCus.ShowDialog();

            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        private void popCus_Selected(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(popCus.Selection.Text) == false)
            {
                btnCus.Text = popCus.Selection.Text + "   > ";
                btnCus.Tag = popCus.Selection.Value;         //�ɹ��˱��
            }
        }

        /// <summary>
        /// ɾ����ǰѡ������
        /// </summary>
        /// <param name="TemlateID">ģ����</param>
        public void RemoveTemplate(String TemlateID)
        {
            foreach (AssRowInputDto row in Rows)
            {
                if (row.TEMPLATEID == TemlateID)
                {
                    Rows.Remove(row);
                    break;
                }
            }
            Bind();       //ˢ�µ�ǰҳ��
        }
    }
}