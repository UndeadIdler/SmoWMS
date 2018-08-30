using System;
using Smobiler.Core.Controls;
using SMOWMS.DTOs.OutputDTO;
using Smobiler.Device;

namespace SMOWMS.UI.MasterData
{
    /// <summary>
    /// �ʲ��������
    /// </summary>
    partial class frmAssetsDetail : Smobiler.Core.Controls.MobileForm
    {
        #region ����
        public string AssId; //�ʲ����
        private string SLID; //��λ���
        private string TypeId; //���ͱ��
        private string ManagerId; //����Ա���
        private AutofacConfig _autofacConfig = new AutofacConfig();//����������
        private string LastUser; //�ʲ������ӵ����
        #endregion

        /// <summary>
        /// �鿴�����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLog_Press(object sender, EventArgs e)
        {
            try
            {
                frmPrShow prShow = new frmPrShow { AssId = AssId };
                Show(prShow);
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ��ת���ʲ��༭����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Press(object sender, EventArgs e)
        {
            try
            {
                frmAssetsDetailEdit assetsDetailEdit = new frmAssetsDetailEdit { AssId = AssId};
                Show(assetsDetailEdit, (MobileForm sender1, object args) =>
                {
                    if (assetsDetailEdit.ShowResult == ShowResult.Yes)
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
                AssetsOutputDto outputDto = _autofacConfig.SettingService.GetAssetsByID(AssId);
                if (outputDto != null)
                {
                    txtEDate.Text = outputDto.ExpiryDate.ToString("yyyy-MM-dd");
                    txtAssId1.Text = outputDto.AssId;
                    txtBuyDate.Text = outputDto.BuyDate.ToString("yyyy-MM-dd");
                    txtSL.Text = outputDto.SLName;
                    txtName1.Text = outputDto.Name;
                    txtPlace1.Text = outputDto.Place;
                    txtPrice1.Text = outputDto.Price.ToString();
                    txtSN1.Text = outputDto.SN;
                    txtSPE1.Text = outputDto.Specification;
                    txtType.Text = outputDto.TypeName;
                    txtUnit1.Text = outputDto.Unit;
                    txtVendor1.Text = outputDto.Vendor;
                    image2.ResourceID = outputDto.Image;
                    txtNote1.Text = outputDto.Note;
                    SLID = outputDto.SLID;
                    TypeId = outputDto.TypeId;
                    txtATID.Text = outputDto.ATID;
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
        private void frmAssetsDetail_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                ShowResult = ShowResult.Yes;
                Close();
            }
        }

        /// <summary>
        /// �����ʼ��ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAssetsDetail_Load(object sender, EventArgs e)
        {
            try
            {
                Bind();
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Press(object sender, EventArgs e)
        {
            try
            {
                AssetsOutputDto outputDto = _autofacConfig.SettingService.GetAssetsByID(AssId);
                PosPrinterEntityCollection Commands = new PosPrinterEntityCollection();
                Commands.Add(new PosPrinterProtocolEntity(PosPrinterProtocol.Initial));
                Commands.Add(new PosPrinterProtocolEntity(PosPrinterProtocol.EnabledBarcode));
                Commands.Add(new PosPrinterProtocolEntity(PosPrinterProtocol.AbsoluteLocation));
                Commands.Add(new PosPrinterBarcodeEntity(PosBarcodeType.CODE128Height, "62"));
                Commands.Add(new PosPrinterBarcodeEntity(PosBarcodeType.CODE128, outputDto.SN));
                Commands.Add(new PosPrinterProtocolEntity(PosPrinterProtocol.DisabledBarcode));
                Commands.Add(new PosPrinterContentEntity(System.Environment.NewLine));
                Commands.Add(new PosPrinterProtocolEntity(PosPrinterProtocol.Cut));

                posPrinter1.Print(Commands, (obj, args) =>
                {
                    if (args.isError == true)
                        this.Toast("Error: " + args.error);
                    else
                        this.Toast("��ӡ�ɹ�");
                });
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
    }
}