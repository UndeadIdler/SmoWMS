using System;
using System.Collections.Generic;
using System.Data;
using Smobiler.Core.Controls;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.UI.Layout;

namespace SMOWMS.UI.AssetsManager
{
    partial class frmAssTemplateChoose : Smobiler.Core.Controls.MobileForm
    {
        #region ����
        public DataTable AllATTable = new DataTable();

        public string UserId;

        private AutofacConfig _autofacConfig = new AutofacConfig();//����������
        private DataTable ATShow=new DataTable();

        public List<AssRowInputDto> Rows=new List<AssRowInputDto>();

        private string errorInfo;
        #endregion

        /// <summary>
        /// �ı�ȫѡ��״̬ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Checkall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Checkall.Checked)
                {
                    foreach (var row in lvAssTemplate.Rows)
                    {
                        frmATChooseLayout ATRow = (frmATChooseLayout)row.Control;
                        ATRow.CheckBox1.Checked = true;
                    }

                }
                
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ��ȷ��ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Press(object sender, EventArgs e)
        {
            try
            {
                errorInfo = "";
                GetAT();
                if (string.IsNullOrEmpty(errorInfo))
                {
                    if (Rows.Count == 0)
                    {
                        throw new Exception("��ѡ�����");
                    }
                    else
                    {
                        ShowResult = ShowResult.Yes;
                        Close();
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
        private void frmAssTemplateChoose_Load(object sender, EventArgs e)
        {
            try
            {
                AllATTable = _autofacConfig.SettingService.GetAllAssTemps();
                foreach (DataRow row in AllATTable.Rows)
                {
                    string TId = row["TEMPLATEID"].ToString();
                    AssRowInputDto inputDto = Rows.Find(a => a.TEMPLATEID == TId);
                    if (inputDto != null)
                    {
                        row["IsChecked"] = true;
                        row["PRICE"] = inputDto.PRICE;
                        row["QUANT"] = inputDto.QUANT;
                    }
                }
                DataColumn[] keys = new DataColumn[1];
                keys[0] = AllATTable.Columns["TEMPLATEID"];
                AllATTable.PrimaryKey = keys;

                UserId = Client.Session["UserID"].ToString();
                Bind(null);
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
        private void frmAssTemplateChoose_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
                Close();
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="name"></param>
        private void Bind(string name)
        {
            try
            {
                ATShow = _autofacConfig.SettingService.QueryAssTemplate(name);

                foreach (DataRow row in ATShow.Rows)
                {

                    string TId = row["TEMPLATEID"].ToString();
                    DataRow allATRow = AllATTable.Rows.Find(TId);
                    if (allATRow != null)
                    {
                        row["IsChecked"] = allATRow["IsChecked"];
                        row["PRICE"] = allATRow["PRICE"];
                        row["QUANT"] = allATRow["QUANT"];
                    }
                }

                if (ATShow.Rows.Count > 0)
                {
                    lvAssTemplate.DataSource = ATShow;
                    lvAssTemplate.DataBind();
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
        private void GetAT()
        {
            try
            {
                //�ȱ��浱ǰҳ����
                GetRow();

                //��AllATTale��ȡ��������

                Rows.Clear();

                foreach (DataRow row in AllATTable.Rows)
                {
                    AssRowInputDto rowInputDto =new AssRowInputDto();
                    if (bool.Parse(row["IsChecked"].ToString()))
                    {
                        rowInputDto.TEMPLATEID = row["TEMPLATEID"].ToString();
                        rowInputDto.IMAGE = row["IMAGE"].ToString();
                        rowInputDto.QUANT = decimal.Parse(row["QUANT"].ToString());
                        rowInputDto.NAME = row["NAME"].ToString();
                        rowInputDto.PRICE = decimal.Parse(row["PRICE"].ToString());
                        if (rowInputDto.QUANT == 0 ||rowInputDto.PRICE == 0)
                        {
                            errorInfo = "�뱣֤�����е������͵��۾���Ϊ0��";
                            throw new Exception("�뱣֤�����е������͵��۾���Ϊ0��");
                        }
                        
                        //                        rowInputDto.TPRICE = decimal.Parse(row["TPRICE"].ToString());

                        Rows.Add(rowInputDto);
                    }
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
        private void GetRow()
        {
            try
            {
                foreach (var row in lvAssTemplate.Rows)
                {
                    frmATChooseLayout ATRow = (frmATChooseLayout)row.Control;
                    //���¶�Ӧ��AllATTale����
                    string TId = ATRow.LblTId.Text;
                    DataRow allATRow = AllATTable.Rows.Find(TId);
                    if (allATRow != null)
                    {
                        decimal Quantity;
                        if (decimal.TryParse(ATRow.numQuant.Value.ToString(), out Quantity) == false)
                        {
                            throw new Exception("ģ��" + ATRow.LblTId.Text + "�ļƻ�������ʽ����ȷ��");
                        }
                        decimal PRICE;
                        if (decimal.TryParse(ATRow.numPrice.Value.ToString(), out PRICE) == false)
                        {
                            throw new Exception("ģ��" + ATRow.LblTId.Text + "�ļƻ����۸�ʽ����ȷ��");
                        }
                        allATRow["IsChecked"] = ATRow.CheckBox1.Checked;
                        allATRow["PRICE"] = PRICE;
                        allATRow["QUANT"] = Quantity;
//                        if (Quantity != 0 && PRICE != 0)
//                        {
//                            allATRow["IsChecked"] = ATRow.CheckBox1.Checked;
//                            allATRow["PRICE"] = PRICE;
//                            allATRow["QUANT"] = Quantity;
//                        }
//                        else
//                        {
//                            errorInfo = "�뱣֤�����е������͵��۾���Ϊ0��";
//                            throw new Exception("�뱣֤�����е������͵��۾���Ϊ0��");
//                        }
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// �ı�����ʱ��ִ�в�ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                GetRow();
                Bind(txtName.Text);
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}