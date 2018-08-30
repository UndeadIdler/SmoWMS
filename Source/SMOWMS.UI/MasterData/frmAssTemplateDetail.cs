using System;
using Smobiler.Core.Controls;
using SMOWMS.Domain.Entity;

namespace SMOWMS.UI.MasterData
{
    partial class frmAssTemplateDetail : Smobiler.Core.Controls.MobileForm
    {
        #region ����
        public string TempId; //�ʲ����
        private AutofacConfig _autofacConfig = new AutofacConfig();//����������
        private string typeId;
        #endregion


        /// <summary>
        /// ��ת���ʲ�ģ��༭����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Press(object sender, EventArgs e)
        {
            try
            {
                frmAssTemplateDetailEdit assTemplateDetailEdit = new frmAssTemplateDetailEdit { TempId = lblTempID.Text};
                Show(assTemplateDetailEdit, (MobileForm sender1, object args) =>
                {
                    if (assTemplateDetailEdit.ShowResult == ShowResult.Yes)
                    {
                        ShowResult = ShowResult.Yes;
                        Bind();
                    }
                });
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ������
        /// </summary>
        private void Bind()
        {

            try
            {
                AssTemplate outputDto = _autofacConfig.SettingService.GetAtbyId(TempId);
                if (outputDto != null)
                {
                    lblTempID.Text = outputDto.TEMPLATEID;
                    lblName.Text = outputDto.NAME;
                    lblPrice.Text = outputDto.PRICE.ToString();
                    lblSpe.Text = outputDto.SPECIFICATION;
                    lblUnit.Text = outputDto.UNIT;
                    lblVendor.Text = outputDto.VENDOR;
                    ImgPicture.ResourceID = outputDto.IMAGE;
                    lblNote.Text = outputDto.NOTE;
                    var type = _autofacConfig.assTypeService.GetByID(outputDto.TYPEID);
                    lblType.Text = type.NAME;

                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ������ʱ���رյ�ǰ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssTemplateDetail_KeyDown(object sender, KeyDownEventArgs e)
        {

            if (e.KeyCode == KeyCode.Back)
            {
                ShowResult = ShowResult.Yes;
                Close();
            }
        }

        /// <summary>
        /// �����ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssTemplateDetail_Load(object sender, EventArgs e)
        {
            Bind();
        }
    }
}