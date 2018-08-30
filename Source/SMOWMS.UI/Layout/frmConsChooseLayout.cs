using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.UI.ConsumablesManager;
using SMOWMS.Domain.Entity;
using SMOWMS.DTOs.InputDTO;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmConsChooseLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ����ѡ��״̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_CheckedChanged(object sender, EventArgs e)
        {
            ((frmConsChoose)Form).upCheckState();     //����ȫѡ��״̬
        }
        /// <summary>
        /// ��ȡ��ǰ���Ƿ�ѡ��
        /// </summary>
        public Int32 checkNum()
        {
            if (Check.Checked)
                return 1;
            else
                return 0;
        }
        /// <summary>
        /// �õ�ѡ���кĲı��
        /// </summary>
        public ConPurAndSaleCreateInputDto getData()
        {
            if (Check.Checked)
            {
                ConPurAndSaleCreateInputDto cons = new ConPurAndSaleCreateInputDto();
                cons.CID = imgCon.BindDataValue.ToString();
                cons.NAME = lblName.BindDataValue.ToString();
                cons.IMAGE = imgCon.BindDisplayValue.ToString();
                cons.QUANTPURCHASED = Convert.ToDecimal(numQuant.Value);
                cons.REALPRICE = Convert.ToDecimal(numPrice.Value);
                return cons;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// ����ÿ��CheckBox״̬
        /// </summary>
        public void setCheck(Boolean state)
        {
            Check.Checked = state;
        }
        /// <summary>
        /// ���ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plRow_Press(object sender, EventArgs e)
        {
            if (Check.Checked)
                Check.Checked = false;
            else
                Check.Checked = true;
            ((frmConsChoose)Form).upCheckState();
        }
        /// <summary>
        /// ��ʾ��ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConsChooseLayout_Load(object sender, EventArgs e)
        {
            if (((frmConsChoose)Form).type == 1)
            {
                lblQuant.Text = "��������";
                lblPrice.Text = "���ۼ۸�";
            }
        }
    }
}