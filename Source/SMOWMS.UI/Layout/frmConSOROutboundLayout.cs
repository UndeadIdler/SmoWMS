using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.UI.ConsumablesManager;
using SMOWMS.DTOs.InputDTO;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ�����Զ���ؼ��������䣬true��ӣ�false�����
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmConSOROutboundLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ѡ��/��ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_CheckedChanged(object sender, EventArgs e)
        {
            ((frmConSOROutbound)Form).upCheckState();
        }
        /// <summary>
        /// ����ÿ��CheckBox״̬
        /// </summary>
        public void setCheck(Boolean state)
        {
            Check.Checked = state;
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
        /// ѡ��/��ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plRow_Press(object sender, EventArgs e)
        {
            if (Check.Checked)
                Check.Checked = false;
            else
                Check.Checked = true;
            ((frmConSOROutbound)Form).upCheckState();
        }
        /// <summary>
        /// ��ȡ�����Ϣ����
        /// </summary>
        /// <returns></returns>
        internal ConSalesOrderRowInputDto getData()
        {
            if (Check.Checked)
            {
                ConSalesOrderRowInputDto conRow = new ConSalesOrderRowInputDto();
                conRow.CID = lblName.BindDataValue.ToString();
                conRow.SOROWID = Convert.ToInt32(imgCon.BindDataValue);
                if (numOutQuant.Value == 0)
                {
                    conRow.QUANTOUT = Convert.ToDecimal(lblQuant.Text);
                }
                else
                {
                    if (Convert.ToDecimal(numOutQuant.Value) > Convert.ToDecimal(lblQuant.Text))
                    {
                        throw new Exception("ʵ�ʳ����������ɴ������������!");
                    }
                    conRow.QUANTOUT = Convert.ToDecimal(numOutQuant.Value);
                }
                return conRow;
            }
            else
            {
                return null;
            }
        }
    }
}