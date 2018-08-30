using System;
using Smobiler.Core.Controls;
using SMOWMS.DTOs.InputDTO;
using System.Data;
using SMOWMS.UI.Layout;
using System.Collections.Generic;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.OutputDTO;

namespace SMOWMS.UI.ConsumablesManager
{
    partial class frmTransferConsChoose : Smobiler.Core.Controls.MobileForm
    {
        #region "definition"
        AutofacConfig autofacConfig = new AutofacConfig();     //����������
        public List<ConsumablesOrderRow> RowData = new List<ConsumablesOrderRow>();    //δ����SN����
        public String LocInID;           //����������
        #endregion
        /// <summary>
        /// �������ƽ��в�ѯ�Ĳ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            Bind(txtName.Text);
        }
        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewRow Row in ListCons.Rows)
                {
                    frmConsLayout Layout = Row.Control as frmConsLayout;
                    Layout.setCheck(Checkall.Checked);
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
                                        //if (tpvAssets.PageIndex == 0)
                                        //{
            foreach (ListViewRow Row in ListCons.Rows)
            {
                frmConsLayout Layout = Row.Control as frmConsLayout;
                selectQty += Layout.checkNum();
            }
            if (selectQty == ListCons.Rows.Count)
                Checkall.Checked = true;          //ѡ����������ʱ
            else
                Checkall.Checked = false;        //û��ѡ����������
        }
        /// <summary>
        /// ѡ��Ĳ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                if (RowData.Count > 0) RowData.Clear();
                foreach (ListViewRow Row in ListCons.Rows)
                {
                    frmConsLayout Layout = Row.Control as frmConsLayout;
                    if (Layout.getData() != null)
                    {
                        RowData.Add(Layout.getData());     //���δ����SN�ʲ���Ϣ
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
        /// <summary>
        /// ҳ���ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssetsChoose_Load(object sender, EventArgs e)
        {
            Bind(null);
            upCheckState();      //����ȫѡ��״̬
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
                tableAssets.Columns.Add("NAME");               //�ʲ����� 
                tableAssets.Columns.Add("LOCID");              //   �ֿ�/�洢����/��λ
                tableAssets.Columns.Add("LOCNAME");            //   �ֿ�����/��������/��λ����
                tableAssets.Columns.Add("IMAGE");              //ͼƬ���
                tableAssets.Columns.Add("QUANTITY");           //��������
                tableAssets.Columns.Add("SELECTQTY");          //ѡ������

                string[] LCData = LocInID.Split('/');
                List<ConQuantOutputDto> listAss = new List<ConQuantOutputDto>();
                if (String.IsNullOrEmpty(Name))     //��ѯ���кĲ�
                    listAss = autofacConfig.orderCommonService.GetUnUseCon(LCData[0], LCData[1], LCData[2], null);
                else
                {
                    List<Consumables> cons = autofacConfig.consumablesService.GetConsByName(Name);
                    foreach(Consumables con in cons)
                    {
                        List<ConQuantOutputDto> list = autofacConfig.orderCommonService.GetUnUseCon(LCData[0], LCData[1], LCData[2], con.CID);
                        listAss.AddRange(list);
                    }
                }
                foreach (ConQuantOutputDto Row in listAss)
                {
                    Consumables cons = autofacConfig.consumablesService.GetConsById(Row.CID);
                    WHStorageLocationOutputDto WHLoc = autofacConfig.wareHouseService.GetSLByID(Row.WAREID, Row.STID, Row.SLID);
                    if (RowData.Count > 0)
                    {
                        Boolean isAdd = false;
                        foreach (ConsumablesOrderRow HaveRow in RowData)
                        {
                            if (HaveRow.CID == Row.CID && HaveRow.SLID == Row.SLID)
                            {
                                tableAssets.Rows.Add(true, Row.CID, cons.NAME, Row.WAREID + "/" + Row.STID + "/" + Row.SLID, Row.WARENAME + "/" + Row.STNAME + "/" + Row.SLNAME, cons.IMAGE, Row.QUANTITY, HaveRow.QTY);
                                isAdd = true;
                                break;
                            }
                        }
                        if (isAdd == false)
                            tableAssets.Rows.Add(false, Row.CID, cons.NAME, Row.WAREID + "/" + Row.STID + "/" + Row.SLID, Row.WARENAME + "/" + Row.STNAME + "/" + Row.SLNAME, cons.IMAGE, Row.QUANTITY, 0);
                    }
                    else
                    {
                        tableAssets.Rows.Add(false, Row.CID, cons.NAME, Row.WAREID + "/" + Row.STID + "/" + Row.SLID, Row.WARENAME + "/" + Row.STNAME + "/" + Row.SLNAME, cons.IMAGE, Row.QUANTITY, 0);
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
    }
}