using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SMOWMS.UI.AssetsManager;
using SMOWMS.DTOs.InputDTO;
using SMOWMS.UI.ConsumablesManager;

namespace SMOWMS.UI.Layout
{
    ////ToolboxItem���ڿ����Ƿ������Զ���ؼ��������䣬true���ӣ�false������
    //[System.ComponentModel.ToolboxItem(true)]
    partial class frmConsLayout : Smobiler.Core.Controls.MobileUserControl
    {
        /// <summary>
        /// ����ѡ��״̬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_CheckedChanged(object sender, EventArgs e)
        {
            ((frmTransferConsChoose)Form).upCheckState();     //����ȫѡ��״̬
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
        /// �õ�ѡ��������
        /// </summary>
        public ConsumablesOrderRow getData()
        {
            if (Check.Checked)
            {
                if (numNumber.Value >Convert.ToSingle(lblNumber.BindDataValue)) throw new Exception("ѡ���������ɳ�����������!");
                if (numNumber.Value == 0) throw new Exception("ѡ����������Ϊ0!");
                ConsumablesOrderRow Data = new ConsumablesOrderRow();
                Data.IMAGE = imgAss.BindDisplayValue.ToString();
                Data.CID =lblName.BindDataValue.ToString();
                Data.QTY =Convert.ToDecimal(numNumber.Value);
                string[] datas = lblLocation.BindDataValue.ToString().Split('/');
                Data.WAREID = datas[0];
                Data.STID = datas[1];
                Data.SLID = datas[2]; 
                Data.STATUS = 0;
                return Data;
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
            ((frmTransferConsChoose)Form).upCheckState();
        }
    }
}