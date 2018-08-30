using System;
using Smobiler.Core;
namespace SMOWMS.UI.ConsumablesManager
{
    partial class frmConInventoryCreate : Smobiler.Core.Controls.MobileForm
    {
        #region "SmobilerForm generated code "

        public frmConInventoryCreate()
            : base()
        {
            //This call is required by the SmobilerForm.
            InitializeComponent();

            //Add any initialization after the InitializeComponent() call
        }

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
            this.Title1 = new SMOWMS.UI.UserControl.Title();
            this.plButton = new Smobiler.Core.Controls.Panel();
            this.btnSave = new Smobiler.Core.Controls.Button();
            this.plContent = new Smobiler.Core.Controls.Panel();
            this.Label7 = new Smobiler.Core.Controls.Label();
            this.txtName = new Smobiler.Core.Controls.TextBox();
            this.label3 = new Smobiler.Core.Controls.Label();
            this.label4 = new Smobiler.Core.Controls.Label();
            this.btnWareHouse = new Smobiler.Core.Controls.Button();
            this.btnManager = new Smobiler.Core.Controls.Button();
            this.label1 = new Smobiler.Core.Controls.Label();
            this.button1 = new Smobiler.Core.Controls.Button();
            this.label2 = new Smobiler.Core.Controls.Label();
            this.label5 = new Smobiler.Core.Controls.Label();
            this.btnST = new Smobiler.Core.Controls.Button();
            this.btnSL = new Smobiler.Core.Controls.Button();
            this.popMan = new Smobiler.Core.Controls.PopList();
            this.popWareHouse = new Smobiler.Core.Controls.PopList();
            this.popST = new Smobiler.Core.Controls.PopList();
            this.popSL = new Smobiler.Core.Controls.PopList();
            // 
            // Title1
            // 
            this.Title1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(164)))), ((int)(((byte)(229)))));
            this.Title1.Dock = System.Windows.Forms.DockStyle.Top;
            this.Title1.FontSize = 15F;
            this.Title1.ForeColor = System.Drawing.Color.White;
            this.Title1.Name = "Title1";
            this.Title1.Size = new System.Drawing.Size(300, 40);
            this.Title1.TitleText = "�Ĳ��̵㵥����";
            // 
            // plButton
            // 
            this.plButton.BackColor = System.Drawing.Color.White;
            this.plButton.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.plButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.plButton.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.btnSave});
            this.plButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plButton.Location = new System.Drawing.Point(0, 468);
            this.plButton.Name = "plButton";
            this.plButton.Size = new System.Drawing.Size(300, 40);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(56, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(176, 30);
            this.btnSave.Text = "ȷ��";
            this.btnSave.Press += new System.EventHandler(this.btnSave_Press);
            // 
            // plContent
            // 
            this.plContent.BackColor = System.Drawing.Color.White;
            this.plContent.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.Label7,
            this.txtName,
            this.label3,
            this.label4,
            this.btnWareHouse,
            this.btnManager,
            this.label1,
            this.button1,
            this.label2,
            this.label5,
            this.btnST,
            this.btnSL});
            this.plContent.Flex = 1;
            this.plContent.Location = new System.Drawing.Point(0, 40);
            this.plContent.Name = "plContent";
            this.plContent.Size = new System.Drawing.Size(300, 150);
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.White;
            this.Label7.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.Label7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Label7.Name = "Label7";
            this.Label7.Padding = new Smobiler.Core.Controls.Padding(5F, 0F, 0F, 0F);
            this.Label7.Size = new System.Drawing.Size(100, 30);
            this.Label7.Text = "�̵㵥����";
            // 
            // txtName
            // 
            this.txtName.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.txtName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtName.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.txtName.Location = new System.Drawing.Point(100, 0);
            this.txtName.Name = "txtName";
            this.txtName.Padding = new Smobiler.Core.Controls.Padding(0F, 0F, 5F, 0F);
            this.txtName.Size = new System.Drawing.Size(200, 30);
            this.txtName.WaterMarkText = "�����";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.label3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.label3.Location = new System.Drawing.Point(0, 60);
            this.label3.Name = "label3";
            this.label3.Padding = new Smobiler.Core.Controls.Padding(5F, 0F, 0F, 0F);
            this.label3.Size = new System.Drawing.Size(100, 30);
            this.label3.Text = "�ֿ�";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.label4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.label4.Location = new System.Drawing.Point(0, 30);
            this.label4.Name = "label4";
            this.label4.Padding = new Smobiler.Core.Controls.Padding(5F, 0F, 0F, 0F);
            this.label4.Size = new System.Drawing.Size(100, 30);
            this.label4.Text = "�̵���";
            // 
            // btnWareHouse
            // 
            this.btnWareHouse.BackColor = System.Drawing.Color.White;
            this.btnWareHouse.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.btnWareHouse.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnWareHouse.BorderRadius = 0;
            this.btnWareHouse.ForeColor = System.Drawing.Color.Black;
            this.btnWareHouse.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.btnWareHouse.Location = new System.Drawing.Point(100, 60);
            this.btnWareHouse.Name = "btnWareHouse";
            this.btnWareHouse.Padding = new Smobiler.Core.Controls.Padding(0F, 0F, 5F, 0F);
            this.btnWareHouse.Size = new System.Drawing.Size(200, 30);
            this.btnWareHouse.Text = "ѡ�񣨱��   > ";
            this.btnWareHouse.Press += new System.EventHandler(this.btnLocation_Press);
            // 
            // btnManager
            // 
            this.btnManager.BackColor = System.Drawing.Color.White;
            this.btnManager.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.btnManager.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnManager.BorderRadius = 0;
            this.btnManager.ForeColor = System.Drawing.Color.Black;
            this.btnManager.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.btnManager.Location = new System.Drawing.Point(100, 30);
            this.btnManager.Name = "btnManager";
            this.btnManager.Padding = new Smobiler.Core.Controls.Padding(0F, 0F, 5F, 0F);
            this.btnManager.Size = new System.Drawing.Size(200, 30);
            this.btnManager.Text = "ѡ�񣨱��   > ";
            this.btnManager.Press += new System.EventHandler(this.btnManager_Press);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.label1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.label1.Location = new System.Drawing.Point(0, 90);
            this.label1.Name = "label1";
            this.label1.Padding = new Smobiler.Core.Controls.Padding(5F, 0F, 0F, 0F);
            this.label1.Size = new System.Drawing.Size(100, 30);
            this.label1.Text = "�ֿ�";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.button1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.button1.BorderRadius = 0;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.button1.Location = new System.Drawing.Point(100, 120);
            this.button1.Name = "button1";
            this.button1.Padding = new Smobiler.Core.Controls.Padding(0F, 0F, 5F, 0F);
            this.button1.Size = new System.Drawing.Size(200, 30);
            this.button1.Text = "ѡ�񣨱��   > ";
            this.button1.Press += new System.EventHandler(this.btnLocation_Press);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.label2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.label2.Location = new System.Drawing.Point(0, 90);
            this.label2.Name = "label2";
            this.label2.Padding = new Smobiler.Core.Controls.Padding(5F, 0F, 0F, 0F);
            this.label2.Size = new System.Drawing.Size(100, 30);
            this.label2.Text = "�洢����";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.label5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.label5.Location = new System.Drawing.Point(0, 120);
            this.label5.Name = "label5";
            this.label5.Padding = new Smobiler.Core.Controls.Padding(5F, 0F, 0F, 0F);
            this.label5.Size = new System.Drawing.Size(100, 30);
            this.label5.Text = "��λ";
            // 
            // btnST
            // 
            this.btnST.BackColor = System.Drawing.Color.White;
            this.btnST.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.btnST.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnST.BorderRadius = 0;
            this.btnST.ForeColor = System.Drawing.Color.Black;
            this.btnST.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.btnST.Location = new System.Drawing.Point(100, 90);
            this.btnST.Name = "btnST";
            this.btnST.Padding = new Smobiler.Core.Controls.Padding(0F, 0F, 5F, 0F);
            this.btnST.Size = new System.Drawing.Size(200, 30);
            this.btnST.Text = "ѡ��ѡ�   > ";
            this.btnST.Press += new System.EventHandler(this.btnST_Press);
            // 
            // btnSL
            // 
            this.btnSL.BackColor = System.Drawing.Color.White;
            this.btnSL.Border = new Smobiler.Core.Controls.Border(0F, 1F, 0F, 0F);
            this.btnSL.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnSL.BorderRadius = 0;
            this.btnSL.ForeColor = System.Drawing.Color.Black;
            this.btnSL.HorizontalAlignment = Smobiler.Core.Controls.HorizontalAlignment.Right;
            this.btnSL.Location = new System.Drawing.Point(100, 120);
            this.btnSL.Name = "btnSL";
            this.btnSL.Padding = new Smobiler.Core.Controls.Padding(0F, 0F, 5F, 0F);
            this.btnSL.Size = new System.Drawing.Size(200, 30);
            this.btnSL.Text = "ѡ��ѡ�   > ";
            this.btnSL.Press += new System.EventHandler(this.btnSL_Press);
            // 
            // popMan
            // 
            this.popMan.Name = "popMan";
            this.popMan.Selected += new System.EventHandler(this.popMan_Selected);
            // 
            // popWareHouse
            // 
            this.popWareHouse.Name = "popWareHouse";
            this.popWareHouse.Title = "�ֿ�ѡ��";
            this.popWareHouse.Selected += new System.EventHandler(this.popWareHouse_Selected);
            // 
            // popST
            // 
            this.popST.Name = "popST";
            this.popST.Title = "�洢����ѡ��";
            this.popST.Selected += new System.EventHandler(this.popST_Selected);
            // 
            // popSL
            // 
            this.popSL.Name = "popSL";
            this.popSL.Title = "��λѡ��";
            this.popSL.Selected += new System.EventHandler(this.popSL_Selected);
            // 
            // frmConInventoryCreate
            // 
            this.Components.AddRange(new Smobiler.Core.Controls.MobileComponent[] {
            this.popMan,
            this.popWareHouse,
            this.popST,
            this.popSL});
            this.Controls.AddRange(new Smobiler.Core.Controls.MobileControl[] {
            this.Title1,
            this.plButton,
            this.plContent});
            this.Statusbar = new Smobiler.Core.Controls.MobileFormStatusbar(Smobiler.Core.Controls.MobileFormStatusbarStyle.Default, System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(164)))), ((int)(((byte)(229))))));
            this.Load += new System.EventHandler(this.frmConInventoryCreate_Load);
            this.Name = "frmConInventoryCreate";

        }
        #endregion

        private UserControl.Title Title1;
        internal Smobiler.Core.Controls.Panel plButton;
        internal Smobiler.Core.Controls.Button btnSave;
        private Smobiler.Core.Controls.Panel plContent;
        internal Smobiler.Core.Controls.Label Label7;
        internal Smobiler.Core.Controls.TextBox txtName;
        internal Smobiler.Core.Controls.Label label3;
        internal Smobiler.Core.Controls.Label label4;
        internal Smobiler.Core.Controls.Button btnWareHouse;
        internal Smobiler.Core.Controls.Button btnManager;
        private Smobiler.Core.Controls.PopList popMan;
        private Smobiler.Core.Controls.PopList popWareHouse;
        internal Smobiler.Core.Controls.Label label1;
        internal Smobiler.Core.Controls.Button button1;
        internal Smobiler.Core.Controls.Label label2;
        internal Smobiler.Core.Controls.Label label5;
        internal Smobiler.Core.Controls.Button btnST;
        internal Smobiler.Core.Controls.Button btnSL;
        private Smobiler.Core.Controls.PopList popST;
        private Smobiler.Core.Controls.PopList popSL;
    }
}