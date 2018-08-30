using System;
using System.Data;
using System.Drawing;
using Smobiler.Core.Controls;
using SMOWMS.UI.Layout;

namespace SMOWMS.UI.AssetsManager
{
    /// <summary>
    /// �̵㵥�б�
    /// </summary>
    partial class frmAssInventory : Smobiler.Core.Controls.MobileForm
    {
        #region  �������
        private AutofacConfig _autofacConfig = new AutofacConfig();//����������
        #endregion

        /// <summary>
        /// ��ʼ������ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssInventory_Load(object sender, EventArgs e)
        {
            Bind();
        }

        //��ActionButtonʱ����
        private void frmAssInventory_ActionButtonPress(object sender, ActionButtonPressEventArgs e)
        {
            switch (e.Index)
            {
                case 0:
                    frmAssInventoryCreate assInventoryCreate=new frmAssInventoryCreate();
                    Show(assInventoryCreate, (MobileForm sender1, object args) =>
                    {
                        if (assInventoryCreate.ShowResult == ShowResult.Yes)
                        {
                            Bind();
                        }
                    });

                    break;
                default:
                    break;
            }
        }

        //������
        public void Bind()
        {
            try
            {

//                string LocationId = "";
                string UserId = Session["UserID"].ToString();
                DataTable assInventoryList = _autofacConfig.AssInventoryService.GetAssInventoryList("");
                listView.Rows.Clear();
                if (assInventoryList.Rows.Count > 0)
                {
                    listView.DataSource = assInventoryList;
                    listView.DataBind();
                }
                foreach (var row in listView.Rows)
                {
                    frmAssInventoryLayout layout = (frmAssInventoryLayout) row.Control;
                    switch (layout.label1.Text)
                    {
                        case "�̵����":
                            layout.label1.ForeColor= Color.FromArgb(43, 125, 43);
                            break;
                        case "�̵���":
                            layout.label1.ForeColor = Color.FromArgb(43, 140, 255);
                            layout.btnStart.Text = "�����̵�";
                            break;
                        case "�̵�δ��ʼ":
                            layout.label1.ForeColor = Color.FromArgb(211, 215, 217);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ����̵㵥
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Press(object sender, EventArgs e)
        {
            try
            {
                frmAssInventoryCreate assInventoryCreate = new frmAssInventoryCreate();
                Show(assInventoryCreate, (MobileForm sender1, object args) =>
                {
                    if (assInventoryCreate.ShowResult == ShowResult.Yes)
                    {
                        Bind();
                    }
                });
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            } 
        }
    }
}