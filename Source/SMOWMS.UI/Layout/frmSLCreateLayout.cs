using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.CommLib;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.DTOs.OutputDTO;
using SMOWMS.UI.MasterData;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmSLCreateLayout : Smobiler.Core.Controls.MobileUserControl
    {
        #region
        public String WareID;    //�ֿ���
        public Boolean isCreate;     //ҳ���Ƿ�Ϊ����״̬
        public Boolean isEdit;       //ҳ���Ƿ�Ϊ�༭״̬
        AutofacConfig autofacConfig = new AutofacConfig();     //����������
        public string STID;
        public string SLID;
        #endregion

        /// <summary>
        /// �ύ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtSLID.Text)) throw new Exception("��λ��Ų���Ϊ��");
                if (String.IsNullOrEmpty(txtSLName.Text)) throw new Exception("��λ���Ʋ���Ϊ��");
                if (String.IsNullOrEmpty(btnManager.Text)) throw new Exception("�����˲���Ϊ��");

                WHStorageLocationInputDto inputDto = new WHStorageLocationInputDto
                {
                    SLID = txtSLID.Text,
                    WAREID = txtWareID.Text,
                    STID = txtSTID.Text,
                    NAME = txtSLName.Text,
                    MANAGER = btnManager.Tag.ToString(),
                    MAXVOLUME = txtMAXVOLUME.Text,
                    MAXCAPACITY = txtMAXCAPACITY.Text,
                    MODIFYUSER = Client.Session["UserID"].ToString()
                };
                if (isCreate)
                {
                    inputDto.CREATEUSER = Client.Session["UserID"].ToString();
                    inputDto.STATUS = 0;
                    //���
                    ReturnInfo rInfo = autofacConfig.wareHouseService.AddWhStorageLocation(inputDto);
                    if (rInfo.IsSuccess)
                    {
                        this.Close();
                        Toast("������λ�ɹ�");
                        //ˢ��ҳ������
                        ((frmWHStorageLocation)Form).Bind();
                    }
                    else
                    {
                        throw new Exception(rInfo.ErrorInfo);
                    }
                }
                else if (isEdit)
                {
                    //�༭
                    ReturnInfo rInfo = autofacConfig.wareHouseService.UpdateWhStorageLocation(inputDto);
                    if (rInfo.IsSuccess)
                    {
                        this.Close();
                        Toast("�༭��λ�ɹ�");
                        //ˢ��ҳ������
                        ((frmWHStorageLocation)Form).Bind();
                    }
                    else
                    {
                        throw new Exception(rInfo.ErrorInfo);
                    }
                }

            }
            catch (Exception ex)
            {
                Form.Toast(ex.Message);
            }
        }

        /// <summary>
        /// �رյ�ǰ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Press(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnManager_Press(object sender, EventArgs e)
        {
            try
            {
                popMan.Groups.Clear();
                PopListGroup manGroup = new PopListGroup { Title = "������ѡ��" };
                List<coreUser> users = autofacConfig.coreUserService.GetDealInAdmin();
                foreach (coreUser Row in users)
                {
                    manGroup.AddListItem(Row.USER_NAME, Row.USER_ID);
                }
                popMan.Groups.Add(manGroup);
                if (btnManager.Tag != null)   //�������ѡ�������ʾѡ��Ч��
                {
                    foreach (PopListItem Item in manGroup.Items)
                    {
                        if (Item.Value == btnManager.Tag.ToString())
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

        private void popMan_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popMan.Selection != null)
                {
                    btnManager.Text = popMan.Selection.Text + "   > ";
                    btnManager.Tag = popMan.Selection.Value;
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ҳ���ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSLCreateLayout_Load(object sender, EventArgs e)
        {
            try
            {
                WHStorageTypeOutputDto whStorageType = autofacConfig.wareHouseService.GetSTByWareIDandSTID(WareID, STID);
                txtWareID.Text = whStorageType.WAREID;
                txtWareID.ReadOnly = true;
                txtWareName.Text = whStorageType.WARENAME;
                txtWareName.ReadOnly = true;
                txtSTID.Text = whStorageType.STID;
                txtSTID.ReadOnly = true;
                txtSTName.Text = whStorageType.NAME;
                txtSTName.ReadOnly = true;
                if (isEdit)
                {
                    WHStorageLocationOutputDto whStorageLocation = autofacConfig.wareHouseService.GetSLByID(WareID, STID, SLID);
                    txtSLID.Text = whStorageLocation.SLID;
                    txtSLID.ReadOnly = true;
                    txtSLName.Text = whStorageLocation.SLNAME;
                    btnManager.Tag = whStorageLocation.MANAGER;
                    btnManager.Text = whStorageLocation.MANAGERNAME + "   > ";
                    txtMAXCAPACITY.Text = whStorageLocation.MAXCAPACITY;
                    txtMAXVOLUME.Text = whStorageLocation.MAXVOLUME;
                }
            }
            catch (Exception ex)
            {
                Form.Toast(ex.Message);
            }
        }
    }
}