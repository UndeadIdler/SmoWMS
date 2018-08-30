using System;
using System.Collections.Generic;
using Smobiler.Core.Controls;
using SMOWMS.CommLib;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.InputDTO;

namespace SMOWMS.UI.AssetsManager
{
    /// <summary>
    /// ����̵㵥����
    /// </summary>
    partial class frmAssInventoryCreate : Smobiler.Core.Controls.MobileForm
    {
        #region  �������
        private AutofacConfig _autofacConfig = new AutofacConfig();//����������
        private string UserId;
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
                if (btnManager.Tag == null) throw new Exception("��ѡ���̵��ˣ�");
                if (btnWareHouse.Tag == null) throw new Exception("��ѡ��ֿ⣡");
                string typeId="";
                string stId="";
                string slId="";
                if (btnType.Tag != null)
                {
                    typeId = btnType.Tag.ToString();
                }
                if (btnST.Tag != null)
                {
                    stId = btnST.Tag.ToString();
                }
                if (btnSL.Tag != null)
                {
                    slId = btnSL.Tag.ToString();
                }

                AssInventoryInputDto assInventoryInput = new AssInventoryInputDto()
                {
                    HANDLEMAN = btnManager.Tag.ToString(),
                    CREATEUSER = UserId,
//                    LOCATIONID = LocationId,
                    TYPEID = typeId,
//                    DEPARTMENTID = DepId,
                    IsEnd = false,
                    MODIFYUSER = UserId,
                    WAREID = btnWareHouse.Tag.ToString(),
                    STID = stId,
                    SLID = slId,
                    NAME = txtName.Text
                };
                ReturnInfo returnInfo = _autofacConfig.AssInventoryService.AddAssInventory(assInventoryInput);
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
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }


        /// <summary>
        /// �̵���ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManager_Press(object sender, EventArgs e)
        {
            try
            {
                popMan.Groups.Clear();
                PopListGroup manGroup = new PopListGroup {Title = "�̵���ѡ��"};
                List<coreUser> users = _autofacConfig.coreUserService.GetAll();
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

        /// <summary>
        /// �ʲ�����ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnType_Press(object sender, EventArgs e)
        {
            try
            {
                popType.Groups.Clear();
                PopListGroup typeGroup = new PopListGroup {Title = "�ʲ�����"};
                var typelist = _autofacConfig.assTypeService.GetAll();
                PopListItem first = new PopListItem
                {
                    Text = "ȫ��",
                    Value = ""
                };
                typeGroup.Items.Add(first);
                foreach (var type in typelist)
                {
                    PopListItem item = new PopListItem
                    {
                        Value = type.TYPEID,
                        Text = type.NAME
                    };
                    typeGroup.Items.Add(item);
                }
                popType.Groups.Add(typeGroup);
                if (btnType.Tag!=null)
                {
                    foreach (PopListItem row in popType.Groups[0].Items)
                    {
                        if (row.Value == btnType.Tag.ToString())
                        {
                            popType.SetSelections(row);
                        }
                    }
                }
                popType.ShowDialog();
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
        private void frmAssInventoryCreate_Load(object sender, EventArgs e)
        {
            try
            {
                UserId = Client.Session["UserID"].ToString();
//                UserId = "12345678912";
//                if (Client.Session["Role"].ToString() == "SMOSECAdmin")
//                {
//                    var user = _autofacConfig.coreUserService.GetUserByID(UserId);
////                    LocationId = user.USER_LOCATIONID;
////                    var location = _autofacConfig.assLocationService.GetByID(LocationId);
////                    if (location != null) btnLocation.Text = location.NAME;
////                    btnLocation.Enabled = false;
//                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// �̵���ѡ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// �ʲ�����ѡ�к�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popType_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popType.Selection != null)
                {
                    btnType.Text = popType.Selection.Text + "   > ";
                    btnType.Tag = popType.Selection.Value;
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        private void btnST_Press(object sender, EventArgs e)
        {
            try
            {
                if (btnWareHouse.Tag==null)
                {
                    throw new Exception("����ѡ��ֿ⣡");
                }
                popST.Groups.Clear();
                PopListGroup stGroup = new PopListGroup { Title = "�洢����" };
                var stlist = _autofacConfig.wareHouseService.GetSTByWareID(btnWareHouse.Tag.ToString());
                PopListItem first = new PopListItem
                {
                    Text = "ȫ��",
                    Value = ""
                };
                stGroup.Items.Add(first);
                foreach (var st in stlist)
                {
                    PopListItem item = new PopListItem
                    {
                        Value = st.STID,
                        Text = st.NAME
                    };
                    stGroup.Items.Add(item);
                }
                popST.Groups.Add(stGroup);
                if (btnST.Tag!=null)
                {
                    foreach (PopListItem row in popST.Groups[0].Items)
                    {
                        if (row.Value == btnST.Tag.ToString())
                        {
                            popST.SetSelections(row);
                        }
                    }
                }
                popST.ShowDialog();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        private void btnSL_Press(object sender, EventArgs e)
        {
            try
            {
                if (btnWareHouse.Tag==null)
                {
                    throw new Exception("����ѡ��ֿ⣡");
                }
                if (btnST.Tag==null)
                {
                    throw new Exception("����ѡ��洢���ͣ�");
                }
                popSL.Groups.Clear();
                PopListGroup slGroup = new PopListGroup { Title = "��λ" };
                var sllist = _autofacConfig.wareHouseService.GetSLByWareIDAndSTID(btnWareHouse.Tag.ToString(),btnST.Tag.ToString());
                PopListItem first = new PopListItem
                {
                    Text = "ȫ��",
                    Value = ""
                };
                slGroup.Items.Add(first);
                foreach (var sl in sllist)
                {
                    PopListItem item = new PopListItem
                    {
                        Value = sl.SLID,
                        Text = sl.NAME
                    };
                    slGroup.Items.Add(item);
                }
                popSL.Groups.Add(slGroup);
                if (btnSL.Tag!=null)
                {
                    foreach (PopListItem row in popSL.Groups[0].Items)
                    {
                        if (row.Value == btnSL.Tag.ToString())
                        {
                            popSL.SetSelections(row);
                        }
                    }
                }
                popSL.ShowDialog();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        private void btnWareHouse_Press(object sender, EventArgs e)
        {
            try
            {
                popWH.Groups.Clear();
                PopListGroup whGroup = new PopListGroup { Title = "�ֿ�" };
                var whlist = _autofacConfig.wareHouseService.GetAllWareHouse();

                foreach (var wh in whlist)
                {
                    PopListItem item = new PopListItem
                    {
                        Value = wh.WAREID,
                        Text = wh.NAME
                    };
                    whGroup.Items.Add(item);
                }
                popWH.Groups.Add(whGroup);
                if (btnWareHouse.Tag!=null)
                {
                    foreach (PopListItem row in popWH.Groups[0].Items)
                    {
                        if (row.Value == btnWareHouse.Tag.ToString())
                        {
                            popWH.SetSelections(row);
                        }
                    }
                }
                popWH.ShowDialog();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        private void popSL_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popSL.Selection != null)
                {
                    btnSL.Text = popSL.Selection.Text + "   > ";
                    if (btnSL.Tag == null || btnSL.Tag.ToString() != popSL.Selection.Value)
                    {
                        //�����µ�ֵ
                        btnSL.Tag = popSL.Selection.Value;
                    }

                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        private void popST_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popST.Selection != null)
                {
                    btnST.Text = popST.Selection.Text + "   > ";
                    if (btnST.Tag == null || btnST.Tag.ToString() != popST.Selection.Value)
                    {

                        popSL.Groups.Clear();
                        btnSL.Tag = null;
                        btnSL.Text = "ȫ��" + "   > ";
                        //�����µ�ֵ
                        btnST.Tag = popST.Selection.Value;
                    }

                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        private void popWH_Selected(object sender, EventArgs e)
        {
            try
            {
                if (popWH.Selection != null)
                {
                    btnWareHouse.Text = popWH.Selection.Text + "   > ";
                    if (btnWareHouse.Tag==null||btnWareHouse.Tag.ToString() != popWH.Selection.Value)
                    {
                        //���֮ǰ��ѡ��������ڵĲ�ͬ,�����ص�ѡ�����
                        popST.Groups.Clear();
                        popSL.Groups.Clear();
                        btnST.Tag = null;
                        btnSL.Tag = null;
                        btnST.Text = "ȫ��" + "   > ";
                        btnSL.Text = "ȫ��" + "   > ";
                        //��btnWareHouse.Tag��ֵ���µ�ֵ
                        btnWareHouse.Tag = popWH.Selection.Value;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        private void frmAssInventoryCreate_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();
            }
        }
    }
}