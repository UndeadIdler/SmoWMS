using System;
using System.Collections.Generic;
using Smobiler.Core.Controls;
using SMOWMS.UI.Layout;
using System.Data;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.InputDTO;

namespace SMOWMS.UI.ConsumablesManager
{
    partial class frmConsChoose : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig autofacConfig = new AutofacConfig();     //����������
        public List<ConPurAndSaleCreateInputDto> Rows = new List<ConPurAndSaleCreateInputDto>();    //ѡ��Ĳı��
        public int type;  // 0-�ɹ���1-�Ĳ�
        #endregion
        /// <summary>
        /// ҳ���ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPurchaseConsChoose_Load(object sender, EventArgs e)
        {
            Bind(null);
            upCheckState();      //����ȫѡ��״̬
        }
        /// <summary>
        /// �������Ʋ�ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            Bind(txtName.Text);
        }
        /// <summary>
        /// ���ݰ�
        /// </summary>
        /// <param name="Name"></param>
        public void Bind(String Name)
        {
            try
            {
                DataTable tableAssets = new DataTable();       //δ����SN���ʲ��б�
                tableAssets.Columns.Add("CHECK");              //�ʲ����
                tableAssets.Columns.Add("CID");                //�Ĳı��
                tableAssets.Columns.Add("NAME");               //�Ĳ����� 
                tableAssets.Columns.Add("IMAGE");              //ͼƬ���
                tableAssets.Columns.Add("QUANTPURCHASED");              //Ԥ������
                tableAssets.Columns.Add("REALPRICE");              //Ԥ���۸�

                List<Consumables> cons = autofacConfig.consumablesService.GetConsByName(Name);
                foreach (Consumables con in cons)
                {
                    if (Rows.Count > 0)
                    {
                        Boolean isAdd = false;
                        foreach (ConPurAndSaleCreateInputDto Row in Rows)
                        {
                            if (con.CID == Row.CID)
                            {
                                tableAssets.Rows.Add(true, Row.CID, con.NAME, Row.IMAGE, Row.QUANTPURCHASED, Row.REALPRICE);
                                isAdd = true;
                                break;
                            }
                        }
                        if (isAdd == false)
                            tableAssets.Rows.Add(false, con.CID, con.NAME, con.IMAGE, 0, 0);
                    }
                    else
                    {
                        tableAssets.Rows.Add(false, con.CID, con.NAME, con.IMAGE, 0, 0);
                    }
                }

                if (tableAssets.Rows.Count > 0)
                {
                    ListCons.DataSource = tableAssets;
                    ListCons.DataBind();
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// ����ȫѡ״̬
        /// </summary>
        public void upCheckState()
        {
            Int32 selectQty = 0;        //��ǰѡ��������
            foreach (ListViewRow Row in ListCons.Rows)
            {
                frmConsChooseLayout Layout = Row.Control as frmConsChooseLayout;
                selectQty += Layout.checkNum();
            }
            if (selectQty == ListCons.Rows.Count)
                Checkall.Checked = true;          //ѡ����������ʱ
            else
                Checkall.Checked = false;        //û��ѡ����������
        }
        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            setCheck();
        }
        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plAll_Press(object sender, EventArgs e)
        {
            if (Checkall.Checked)
            {
                Checkall.Checked = false;
            }
            else
            {
                Checkall.Checked = true;
            }
            setCheck();
        }
        /// <summary>
        /// �޸�����ѡ��״̬
        /// </summary>
        private void setCheck()
        {
            foreach (ListViewRow Row in ListCons.Rows)
            {
                frmConsChooseLayout Layout = Row.Control as frmConsChooseLayout;
                Layout.setCheck(Checkall.Checked);
            }
        }
        /// <summary>
        /// �Ĳ�ѡ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (Rows.Count > 0) Rows.Clear();
                foreach (ListViewRow Row in ListCons.Rows)
                {
                    frmConsChooseLayout Layout = Row.Control as frmConsChooseLayout;
                    if (Layout.getData() != null)
                    {
                        Rows.Add(Layout.getData());     //���ѡ��ĺĲı��     
                    }
                }
                ShowResult = ShowResult.Yes;
                Form.Close();       //�رյ�ǰҳ��
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}