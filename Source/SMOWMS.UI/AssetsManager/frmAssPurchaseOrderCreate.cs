using System;
using System.Collections.Generic;
using Smobiler.Core.Controls;
using SMOWMS.CommLib;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.UI.Layout;

namespace SMOWMS.UI.AssetsManager
{
    partial class frmAssPurchaseOrderCreate : Smobiler.Core.Controls.MobileForm
    {

        #region  �������
        private AutofacConfig _autofacConfig = new AutofacConfig();//����������
        public List<AssRowInputDto> Rows=new List<AssRowInputDto>();
        public List<string> TemplateIds=new List<string>();
        private string UserId;
        private string errorInfo;
        #endregion

        /// <summary>
        /// �����ɹ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                errorInfo = "";
                if (btnDealMan.Tag== null) throw new Exception("��ѡ��ɹ��ˣ�");
                if (string.IsNullOrEmpty(txtName.Text)) throw new Exception("���������ƣ�");
                if (btnVendor.Tag == null) throw new Exception("��ѡ��Ӧ�̣�");
                int VId = int.Parse(btnVendor.Tag.ToString());
                GetRows();
                if (!string.IsNullOrEmpty(errorInfo))
                {
                    return;
                }
                if (Rows.Count != 0)
                {
                    AssPurchaseOrderInputDto assPurchaseOrderInput = new AssPurchaseOrderInputDto()
                    {
                        PURCHASER = btnDealMan.Tag.ToString(),
                        CREATEUSER = UserId,
                        MODIFYUSER = UserId,
                        NAME = txtName.Text,
                        REALID = txtRealID.Text,
                        STATUS = 0,
                        VID = VId,
                        RowInputDtos = Rows
                    };
                    ReturnInfo returnInfo =
                        _autofacConfig.AssPurchaseOrderService.AddPurchaseOrder(assPurchaseOrderInput);
                    if (returnInfo.IsSuccess)
                    {
                        ShowResult = ShowResult.Yes;
                        Toast("��ӳɹ�");
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
        /// ѡ��ɹ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManager_Press(object sender, EventArgs e)
        {
            try
            {
                popMan.Groups.Clear();
                PopListGroup manGroup = new PopListGroup { Title = "�ɹ���ѡ��" };
                List<coreUser> users = _autofacConfig.coreUserService.GetAll();
                foreach (coreUser Row in users)
                {
                    manGroup.AddListItem(Row.USER_NAME, Row.USER_ID);
                }
                popMan.Groups.Add(manGroup);
                if (btnDealMan.Tag!=null)   //�������ѡ�������ʾѡ��Ч��
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
        /// �ʲ�ģ��ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Press(object sender, EventArgs e)
        {
            try
            {
                GetRows();
                frmAssTemplateChoose frm = new frmAssTemplateChoose {Rows = Rows};
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
        /// ɨ������ʲ�ģ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void betGet_Press(object sender, EventArgs e)
        {
            bsAss.GetBarcode();

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
                            if (assTemplate.PRICE!= null)
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
        /// ���ݼ���
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
        /// ɾ����ǰѡ������
        /// </summary>
        /// <param name="TemlateID">ģ����</param>
        public void ReMoveAss(String TemlateID)
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

        public void GetRows()
        {
            try
            {
                foreach (ListViewRow Row in ListATs.Rows)
                {
                    frmAssPORowLayout layout = Row.Control as frmAssPORowLayout;
                    if (layout != null)
                    {
                        AssRowInputDto rowInputDto = Rows.Find(a => a.TEMPLATEID == layout.LblTId.Text);
                        rowInputDto.QUANT = (decimal) layout.numQuant.Value;
                        rowInputDto.PRICE = (decimal) layout.numPrice.Value;
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
        private void frmAssPurchaseOrderCreate_Load(object sender, EventArgs e)
        {
            UserId = Client.Session["UserID"].ToString();
        }

        /// <summary>
        /// �����ˣ���رմ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssPurchaseOrderCreate_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }

        private void btnVendor_Press(object sender, EventArgs e)
        {
            try
            {
                popVendor.Groups.Clear();
                PopListGroup vendorGroup = new PopListGroup { Title = "��Ӧ��ѡ��" };
                List<Vendor> vendors = _autofacConfig.vendorService.GetAll();
                foreach (Vendor Row in vendors)
                {
                    vendorGroup.AddListItem(Row.NAME, Row.VID.ToString());
                }
                popVendor.Groups.Add(vendorGroup);
                if (btnVendor.Tag != null)   //�������ѡ�������ʾѡ��Ч��
                {
                    foreach (PopListItem Item in vendorGroup.Items)
                    {
                        if (Item.Value == btnVendor.Tag.ToString())
                            popVendor.SetSelections(Item);
                    }
                }
                popVendor.ShowDialog();

            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        private void popVendor_Selected(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(popVendor.Selection.Text) == false)
            {
                btnVendor.Text = popVendor.Selection.Text + "   > ";
                btnVendor.Tag = popVendor.Selection.Value;         //�ɹ��˱��
            }
        }
    }
}