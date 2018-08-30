using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.CommLib;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.UI.MasterData;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmWarehouseCreateLayout : Smobiler.Core.Controls.MobileUserControl
    {
        #region
        public String WareID;    //�ֿ���
        public Boolean isCreate;     //ҳ���Ƿ�Ϊ����״̬
        public Boolean isEdit;       //ҳ���Ƿ�Ϊ�༭״̬
        AutofacConfig autofacConfig = new AutofacConfig();     //����������
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
                if (String.IsNullOrEmpty(txtWareID.Text)) throw new Exception("�ֿ��Ų���Ϊ��");
                if (String.IsNullOrEmpty(txtWareName.Text)) throw new Exception("�ֿ����Ʋ���Ϊ��");
                if (String.IsNullOrEmpty(btnManager.Text)) throw new Exception("�����˲���Ϊ��");

                WareHouseInputDto inputDto = new WareHouseInputDto
                {
                    WAREID = txtWareID.Text,
                    ISENABLE = switchIsEnable.Checked ? 1 : 0,
                    NAME = txtWareName.Text,
                    MANAGER = btnManager.Tag.ToString(),
                    PLACE = txtPlace.Text,
                    MODIFYUSER = Client.Session["UserID"].ToString()
                };
                if (isCreate)
                {
                    inputDto.CREATEUSER = Client.Session["UserID"].ToString();
                    //���
                    ReturnInfo rInfo = autofacConfig.wareHouseService.AddWareHouse(inputDto);
                    if (rInfo.IsSuccess)
                    {
                        this.Close();
                        Toast("�����ֿ�ɹ�");
                        //ˢ��ҳ������
                        ((frmWH)Form).Bind();
                    }
                    else
                    {
                        throw new Exception(rInfo.ErrorInfo);
                    }
                }
                else if(isEdit)
                {
                    //�༭
                    ReturnInfo rInfo = autofacConfig.wareHouseService.UpdateWareHouse(inputDto);
                    if (rInfo.IsSuccess)
                    {
                        this.Close();
                        Toast("�༭�ֿ�ɹ�");
                        //ˢ��ҳ������
                        ((frmWHStorgageType)Form).Bind();
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
        private void frmWarehouseCreateLayout_Load(object sender, EventArgs e)
        {
            try
            {
                if (isEdit)
                {
                    WareHouse wareHouse = autofacConfig.wareHouseService.GetByWareID(WareID);
                    txtWareID.Text = wareHouse.WAREID;
                    txtWareID.ReadOnly = true;
                    txtWareName.Text = wareHouse.NAME;
                    btnManager.Tag = wareHouse.MANAGER;
                    txtPlace.Text = wareHouse.PLACE;
                    switchIsEnable.Checked = wareHouse.ISENABLE == 1 ? true : false;
                    coreUser user = autofacConfig.coreUserService.GetUserByID(wareHouse.MANAGER);
                    btnManager.Text = user.USER_NAME + "   > ";
                }
                else if(isCreate)
                {
                    btnManager.Text ="   > ";
                    switchIsEnable.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Form.Toast(ex.Message);
            }
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
                if (btnManager.Tag!=null)   //�������ѡ�������ʾѡ��Ч��
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
    }
}