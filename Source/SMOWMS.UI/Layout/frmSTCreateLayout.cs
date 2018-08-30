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
    partial class frmSTCreateLayout : Smobiler.Core.Controls.MobileUserControl
    {
        #region
        public String WareID;    //�ֿ���
        public Boolean isCreate;     //ҳ���Ƿ�Ϊ����״̬
        public Boolean isEdit;       //ҳ���Ƿ�Ϊ�༭״̬
        AutofacConfig autofacConfig = new AutofacConfig();     //����������
        public string STID;
        #endregion

        /// <summary>
        /// �رյ�ǰ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Press(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// �ύ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtSTID.Text)) throw new Exception("���ͱ�Ų���Ϊ��");
                if (String.IsNullOrEmpty(txtSTName.Text)) throw new Exception("�������Ʋ���Ϊ��");

                WHStorageTypeInputDto inputDto = new WHStorageTypeInputDto
                {
                    WAREID = txtWareID.Text,
                    ISENABLE = switchIsEnable.Checked ? 1 : 0,
                    NAME = txtSTName.Text,
                    STID = txtSTID.Text,
                    MODIFYUSER = Client.Session["UserID"].ToString()
                };
                if (isCreate)
                {
                    inputDto.CREATEUSER = Client.Session["UserID"].ToString();
                    //���
                    ReturnInfo rInfo = autofacConfig.wareHouseService.AddWhStorageType(inputDto);
                    if (rInfo.IsSuccess)
                    {
                        this.Close();
                        Toast("�����ֿ����ͳɹ�");
                        //ˢ��ҳ������
                        ((frmWHStorgageType)Form).Bind();
                    }
                    else
                    {
                        throw new Exception(rInfo.ErrorInfo);
                    }
                }
                else if (isEdit)
                {
                    //�༭
                    ReturnInfo rInfo = autofacConfig.wareHouseService.UpdateWhStorageType(inputDto);
                    if (rInfo.IsSuccess)
                    {
                        this.Close();
                        Toast("�༭�ֿ����ͳɹ�");
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
        /// ҳ���ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmSTCreateLayout_Load(object sender, EventArgs e)
        {
            try
            {
                WareHouse wareHouse = autofacConfig.wareHouseService.GetByWareID(WareID);
                if (wareHouse != null)
                {
                    txtWareID.Text = wareHouse.WAREID;
                    txtWareID.ReadOnly = true;
                    txtWareName.Text = wareHouse.NAME;

                }
                txtWareName.ReadOnly = true;
                if (isEdit)
                {
                    WHStorageTypeOutputDto whStorageType = autofacConfig.wareHouseService.GetSTByWareIDandSTID(WareID, STID);
                    txtSTID.Text = whStorageType.STID;
                    txtSTID.ReadOnly = true;
                    txtSTName.Text = whStorageType.NAME;
                    switchIsEnable.Checked = whStorageType.ISENABLE == 1 ? true : false;
                }
                else if (isCreate)
                {
                    switchIsEnable.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Form.Toast(ex.Message);
            }
        }
    }
}