using System;
using Smobiler.Core;
namespace SMOWMS.UI.Menu
{
    partial class frmMenu : Smobiler.Core.Controls.MobileForm
    {
        #region "SmobilerForm generated code "

        //SmobilerForm overrides dispose to clean up the component list.
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }


        //NOTE: The following procedure is required by the SmobilerForm
        //It can be modified using the SmobilerForm.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            Smobiler.Core.Controls.IconMenuViewGroup iconMenuViewGroup3 = new Smobiler.Core.Controls.IconMenuViewGroup();
            Smobiler.Core.Controls.IconMenuViewItem iconMenuViewItem12 = new Smobiler.Core.Controls.IconMenuViewItem();
            Smobiler.Core.Controls.IconMenuViewItem iconMenuViewItem13 = new Smobiler.Core.Controls.IconMenuViewItem();
            Smobiler.Core.Controls.IconMenuViewItem iconMenuViewItem14 = new Smobiler.Core.Controls.IconMenuViewItem();
            Smobiler.Core.Controls.IconMenuViewItem iconMenuViewItem15 = new Smobiler.Core.Controls.IconMenuViewItem();
            Smobiler.Core.Controls.IconMenuViewItem iconMenuViewItem16 = new Smobiler.Core.Controls.IconMenuViewItem();
            Smobiler.Core.Controls.IconMenuViewItem iconMenuViewItem17 = new Smobiler.Core.Controls.IconMenuViewItem();
            Smobiler.Core.Controls.IconMenuViewGroup iconMenuViewGroup4 = new Smobiler.Core.Controls.IconMenuViewGroup();
            Smobiler.Core.Controls.IconMenuViewItem iconMenuViewItem18 = new Smobiler.Core.Controls.IconMenuViewItem();
            Smobiler.Core.Controls.IconMenuViewItem iconMenuViewItem19 = new Smobiler.Core.Controls.IconMenuViewItem();
            Smobiler.Core.Controls.IconMenuViewItem iconMenuViewItem20 = new Smobiler.Core.Controls.IconMenuViewItem();
            Smobiler.Core.Controls.IconMenuViewItem iconMenuViewItem21 = new Smobiler.Core.Controls.IconMenuViewItem();
            Smobiler.Core.Controls.IconMenuViewItem iconMenuViewItem22 = new Smobiler.Core.Controls.IconMenuViewItem();
            this.popWareHouse = new Smobiler.Core.Controls.PopList();
            this.lblTitle = new Smobiler.Core.Controls.Label();
            this.plWareHouse = new Smobiler.Core.Controls.Panel();
            this.btnWareHouse = new Smobiler.Core.Controls.Button();
            this.iconMenu = new Smobiler.Core.Controls.IconMenuView();
            this.menuToolbar = new SMOWMS.UI.UserControl.MenuToolbar();
            // 
            // popWareHouse
            // 
            this.popWareHouse.Name = "popWareHouse";
            this.popWareHouse.Selected += new System.EventHandler(this.popWareHouse_Selected);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(164)))), ((int)(((byte)(229)))));
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.FontSize = 15F;
            this.lblTitle.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Center;
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 35);
            this.lblTitle.Text = "仓库";
            // 
            // plWareHouse
            // 
            this.plWareHouse.BackColor = System.Drawing.Color.White;
            this.plWareHouse.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.btnWareHouse});
            this.plWareHouse.Name = "plWareHouse";
            this.plWareHouse.Size = new System.Drawing.Size(0, 35);
            // 
            // btnWareHouse
            // 
            this.btnWareHouse.BackColor = System.Drawing.Color.White;
            this.btnWareHouse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnWareHouse.BorderRadius = 0;
            this.btnWareHouse.ForeColor = System.Drawing.Color.Black;
            this.btnWareHouse.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.btnWareHouse.Location = new System.Drawing.Point(200, 0);
            this.btnWareHouse.Name = "btnWareHouse";
            this.btnWareHouse.Padding = new Smobiler.Core.Controls.Padding(0F, 0F, 5F, 0F);
            this.btnWareHouse.Size = new System.Drawing.Size(100, 35);
            this.btnWareHouse.Text = "全部仓库   > ";
            this.btnWareHouse.Press += new System.EventHandler(this.btnWareHouse_Press);
            // 
            // iconMenu
            // 
            this.iconMenu.Flex = 1;
            this.iconMenu.GridLines = true;
            iconMenuViewGroup3.IconBorderRadius = 0;
            iconMenuViewItem12.Icon = "zichan";
            iconMenuViewItem12.ID = "0";
            iconMenuViewItem12.Text = "资产管理";
            iconMenuViewItem12.Value = "Assets";
            iconMenuViewItem13.Icon = "muban";
            iconMenuViewItem13.ID = "1";
            iconMenuViewItem13.Text = "资产模板";
            iconMenuViewItem13.Value = "AssTemplate";
            iconMenuViewItem14.Icon = "ruku";
            iconMenuViewItem14.ID = "2";
            iconMenuViewItem14.Text = "入库";
            iconMenuViewItem14.Value = "AssIn";
            iconMenuViewItem15.Icon = "chuku";
            iconMenuViewItem15.ID = "3";
            iconMenuViewItem15.Text = "出库";
            iconMenuViewItem15.Value = "AssOut";
            iconMenuViewItem16.Icon = "diaobo";
            iconMenuViewItem16.ID = "5";
            iconMenuViewItem16.Text = "调拨";
            iconMenuViewItem16.Value = "AssTransfer";
            iconMenuViewItem17.Icon = "pandian";
            iconMenuViewItem17.ID = "4";
            iconMenuViewItem17.Text = "盘点";
            iconMenuViewItem17.Value = "AssInventory";
            iconMenuViewGroup3.Items.AddRange(new Smobiler.Core.Controls.IconMenuViewItem[] {
            iconMenuViewItem12,
            iconMenuViewItem13,
            iconMenuViewItem14,
            iconMenuViewItem15,
            iconMenuViewItem16,
            iconMenuViewItem17});
            iconMenuViewGroup3.ShowTitle = true;
            iconMenuViewGroup3.Text = "资产";
            iconMenuViewGroup4.IconBorderRadius = 0;
            iconMenuViewItem18.Icon = "haocai";
            iconMenuViewItem18.ID = "0";
            iconMenuViewItem18.Text = "耗材管理";
            iconMenuViewItem18.Value = "ConManage";
            iconMenuViewItem19.Icon = "ruku";
            iconMenuViewItem19.ID = "1";
            iconMenuViewItem19.Text = "入库";
            iconMenuViewItem19.Value = "ConIn";
            iconMenuViewItem20.Icon = "chuku";
            iconMenuViewItem20.ID = "2";
            iconMenuViewItem20.Text = "出库";
            iconMenuViewItem20.Value = "ConOut";
            iconMenuViewItem21.Icon = "diaobo";
            iconMenuViewItem21.ID = "2";
            iconMenuViewItem21.Text = "调拨";
            iconMenuViewItem21.Value = "ConTransfer";
            iconMenuViewItem22.Icon = "pandian";
            iconMenuViewItem22.ID = "3";
            iconMenuViewItem22.Text = "盘点";
            iconMenuViewItem22.Value = "ConInventory";
            iconMenuViewGroup4.Items.AddRange(new Smobiler.Core.Controls.IconMenuViewItem[] {
            iconMenuViewItem18,
            iconMenuViewItem19,
            iconMenuViewItem20,
            iconMenuViewItem21,
            iconMenuViewItem22});
            iconMenuViewGroup4.ShowTitle = true;
            iconMenuViewGroup4.Text = "耗材";
            this.iconMenu.Groups.AddRange(new Smobiler.Core.Controls.IconMenuViewGroup[] {
            iconMenuViewGroup3,
            iconMenuViewGroup4});
            this.iconMenu.Margin = new Smobiler.Core.Controls.Margin(0F, 10F, 0F, 0F);
            this.iconMenu.Name = "iconMenu";
            this.iconMenu.Size = new System.Drawing.Size(0, 0);
            this.iconMenu.ItemPress += new Smobiler.Core.Controls.IconMenuViewItemPressClickHandler(this.iconMenu_ItemPress);
            // 
            // menuToolbar
            // 
            this.menuToolbar.BackColor = System.Drawing.Color.White;
            this.menuToolbar.Name = "menuToolbar";
            this.menuToolbar.SelectedIndex = -1;
            this.menuToolbar.Size = new System.Drawing.Size(0, 50);
            // 
            // frmMenu
            // 
            this.Components.AddRange(new Smobiler.Core.Controls.MobileComponent[] {
            this.popWareHouse});
            this.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.lblTitle,
            this.plWareHouse,
            this.iconMenu,
            this.menuToolbar});
            this.Layout = Smobiler.Core.Controls.LayoutPosition.Relative;
            this.Statusbar = new Smobiler.Core.Controls.MobileFormStatusbar(Smobiler.Core.Controls.MobileFormStatusbarStyle.Default, System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(164)))), ((int)(((byte)(229))))));
            this.KeyDown += new Smobiler.Core.Controls.KeyDownEventHandler(this.frmMenu_KeyDown);
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.Name = "frmMenu";

        }
        #endregion
        private Smobiler.Core.Controls.PopList popWareHouse;
        private Smobiler.Core.Controls.Label lblTitle;
        private Smobiler.Core.Controls.Panel plWareHouse;
        internal Smobiler.Core.Controls.Button btnWareHouse;
        private Smobiler.Core.Controls.IconMenuView iconMenu;
        private UserControl.MenuToolbar menuToolbar;
    }
}