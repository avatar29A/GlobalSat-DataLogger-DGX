namespace Hqub.GlobalStatDC100.Host
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.afToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuPort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolRefreshPort = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.com1 = new System.Windows.Forms.ToolStripMenuItem();
            this.com2 = new System.Windows.Forms.ToolStripMenuItem();
            this.com3 = new System.Windows.Forms.ToolStripMenuItem();
            this.com4 = new System.Windows.Forms.ToolStripMenuItem();
            this.com5 = new System.Windows.Forms.ToolStripMenuItem();
            this.com6 = new System.Windows.Forms.ToolStripMenuItem();
            this.com7 = new System.Windows.Forms.ToolStripMenuItem();
            this.com8 = new System.Windows.Forms.ToolStripMenuItem();
            this.com9 = new System.Windows.Forms.ToolStripMenuItem();
            this.com10 = new System.Windows.Forms.ToolStripMenuItem();
            this.com11 = new System.Windows.Forms.ToolStripMenuItem();
            this.com12 = new System.Windows.Forms.ToolStripMenuItem();
            this.com13 = new System.Windows.Forms.ToolStripMenuItem();
            this.com14 = new System.Windows.Forms.ToolStripMenuItem();
            this.com15 = new System.Windows.Forms.ToolStripMenuItem();
            this.com16 = new System.Windows.Forms.ToolStripMenuItem();
            this.com17 = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolGetId = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSetId = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolHelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.panelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.contextMenuConsole = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuClear = new System.Windows.Forms.ToolStripMenuItem();
            this.listConsole = new System.Windows.Forms.ListView();
            this.clmDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmEvent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbGlobalSatModel = new System.Windows.Forms.GroupBox();
            this.rbGlobalSatDG200 = new System.Windows.Forms.RadioButton();
            this.rbGlobalSatDG100 = new System.Windows.Forms.RadioButton();
            this.cbAutoClear = new System.Windows.Forms.CheckBox();
            this.statusBar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.contextMenuConsole.SuspendLayout();
            this.gbGlobalSatModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStatusLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 494);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(652, 22);
            this.statusBar.TabIndex = 0;
            this.statusBar.Text = "statusStrip1";
            // 
            // toolStatusLabel
            // 
            this.toolStatusLabel.Name = "toolStatusLabel";
            this.toolStatusLabel.Size = new System.Drawing.Size(76, 17);
            this.toolStatusLabel.Text = "Порт закрыт";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.afToolStripMenuItem,
            this.toolMenuPort,
            this.настройкиToolStripMenuItem,
            this.toolHelpMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(652, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // afToolStripMenuItem
            // 
            this.afToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMenuOpen,
            this.toolStripMenuItem1,
            this.toolMenuExit});
            this.afToolStripMenuItem.Name = "afToolStripMenuItem";
            this.afToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.afToolStripMenuItem.Text = "Файл";
            // 
            // toolMenuOpen
            // 
            this.toolMenuOpen.Name = "toolMenuOpen";
            this.toolMenuOpen.Size = new System.Drawing.Size(121, 22);
            this.toolMenuOpen.Text = "Открыть";
            this.toolMenuOpen.Click += new System.EventHandler(this.toolMenuOpen_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(118, 6);
            // 
            // toolMenuExit
            // 
            this.toolMenuExit.Name = "toolMenuExit";
            this.toolMenuExit.Size = new System.Drawing.Size(121, 22);
            this.toolMenuExit.Text = "Выход";
            this.toolMenuExit.Click += new System.EventHandler(this.toolMenuExit_Click);
            // 
            // toolMenuPort
            // 
            this.toolMenuPort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRefreshPort,
            this.toolStripMenuItem3,
            this.com1,
            this.com2,
            this.com3,
            this.com4,
            this.com5,
            this.com6,
            this.com7,
            this.com8,
            this.com9,
            this.com10,
            this.com11,
            this.com12,
            this.com13,
            this.com14,
            this.com15,
            this.com16,
            this.com17});
            this.toolMenuPort.Name = "toolMenuPort";
            this.toolMenuPort.Size = new System.Drawing.Size(47, 20);
            this.toolMenuPort.Text = "Порт";
            // 
            // toolRefreshPort
            // 
            this.toolRefreshPort.Name = "toolRefreshPort";
            this.toolRefreshPort.Size = new System.Drawing.Size(128, 22);
            this.toolRefreshPort.Text = "Обновить";
            this.toolRefreshPort.Click += new System.EventHandler(this.toolRefreshPort_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(125, 6);
            // 
            // com1
            // 
            this.com1.CheckOnClick = true;
            this.com1.Name = "com1";
            this.com1.Size = new System.Drawing.Size(128, 22);
            this.com1.Text = "COM1";
            this.com1.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com2
            // 
            this.com2.CheckOnClick = true;
            this.com2.Name = "com2";
            this.com2.Size = new System.Drawing.Size(128, 22);
            this.com2.Text = "COM2";
            this.com2.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com3
            // 
            this.com3.CheckOnClick = true;
            this.com3.Name = "com3";
            this.com3.Size = new System.Drawing.Size(128, 22);
            this.com3.Text = "COM3";
            this.com3.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com4
            // 
            this.com4.CheckOnClick = true;
            this.com4.Name = "com4";
            this.com4.Size = new System.Drawing.Size(128, 22);
            this.com4.Text = "COM4";
            this.com4.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com5
            // 
            this.com5.CheckOnClick = true;
            this.com5.Name = "com5";
            this.com5.Size = new System.Drawing.Size(128, 22);
            this.com5.Text = "COM5";
            this.com5.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com6
            // 
            this.com6.CheckOnClick = true;
            this.com6.Name = "com6";
            this.com6.Size = new System.Drawing.Size(128, 22);
            this.com6.Text = "COM6";
            this.com6.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com7
            // 
            this.com7.CheckOnClick = true;
            this.com7.Name = "com7";
            this.com7.Size = new System.Drawing.Size(128, 22);
            this.com7.Text = "COM7";
            this.com7.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com8
            // 
            this.com8.CheckOnClick = true;
            this.com8.Name = "com8";
            this.com8.Size = new System.Drawing.Size(128, 22);
            this.com8.Text = "COM8";
            this.com8.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com9
            // 
            this.com9.CheckOnClick = true;
            this.com9.Name = "com9";
            this.com9.Size = new System.Drawing.Size(128, 22);
            this.com9.Text = "COM9";
            this.com9.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com10
            // 
            this.com10.CheckOnClick = true;
            this.com10.Name = "com10";
            this.com10.Size = new System.Drawing.Size(128, 22);
            this.com10.Text = "COM10";
            this.com10.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com11
            // 
            this.com11.CheckOnClick = true;
            this.com11.Name = "com11";
            this.com11.Size = new System.Drawing.Size(128, 22);
            this.com11.Text = "COM11";
            this.com11.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com12
            // 
            this.com12.CheckOnClick = true;
            this.com12.Name = "com12";
            this.com12.Size = new System.Drawing.Size(128, 22);
            this.com12.Text = "COM12";
            this.com12.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com13
            // 
            this.com13.CheckOnClick = true;
            this.com13.Name = "com13";
            this.com13.Size = new System.Drawing.Size(128, 22);
            this.com13.Text = "COM13";
            this.com13.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com14
            // 
            this.com14.CheckOnClick = true;
            this.com14.Name = "com14";
            this.com14.Size = new System.Drawing.Size(128, 22);
            this.com14.Text = "COM14";
            this.com14.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com15
            // 
            this.com15.CheckOnClick = true;
            this.com15.Name = "com15";
            this.com15.Size = new System.Drawing.Size(128, 22);
            this.com15.Text = "COM15";
            this.com15.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com16
            // 
            this.com16.CheckOnClick = true;
            this.com16.Name = "com16";
            this.com16.Size = new System.Drawing.Size(128, 22);
            this.com16.Text = "COM16";
            this.com16.Click += new System.EventHandler(this.CheckedPort);
            // 
            // com17
            // 
            this.com17.CheckOnClick = true;
            this.com17.Name = "com17";
            this.com17.Size = new System.Drawing.Size(128, 22);
            this.com17.Text = "COM17";
            this.com17.Click += new System.EventHandler(this.CheckedPort);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolGetId,
            this.toolSetId,
            this.toolStripMenuItem2,
            this.toolClear});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.настройкиToolStripMenuItem.Text = "Устройство";
            // 
            // toolGetId
            // 
            this.toolGetId.Name = "toolGetId";
            this.toolGetId.Size = new System.Drawing.Size(168, 22);
            this.toolGetId.Text = "Получить ID";
            this.toolGetId.Click += new System.EventHandler(this.GetIdClick);
            // 
            // toolSetId
            // 
            this.toolSetId.Name = "toolSetId";
            this.toolSetId.Size = new System.Drawing.Size(168, 22);
            this.toolSetId.Text = "Назначить ID";
            this.toolSetId.ToolTipText = "Устанавливает устройство 8-разрядный идентификационный номер";
            this.toolSetId.Click += new System.EventHandler(this.SetIdClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(165, 6);
            // 
            // toolClear
            // 
            this.toolClear.Name = "toolClear";
            this.toolClear.Size = new System.Drawing.Size(168, 22);
            this.toolClear.Text = "Очистить память";
            this.toolClear.Click += new System.EventHandler(this.ClearClick);
            // 
            // toolHelpMenu
            // 
            this.toolHelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolAbout});
            this.toolHelpMenu.Name = "toolHelpMenu";
            this.toolHelpMenu.Size = new System.Drawing.Size(24, 20);
            this.toolHelpMenu.Text = "?";
            // 
            // toolAbout
            // 
            this.toolAbout.Name = "toolAbout";
            this.toolAbout.Size = new System.Drawing.Size(149, 22);
            this.toolAbout.Text = "О программе";
            this.toolAbout.Click += new System.EventHandler(this.toolAbout_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelButtons.Controls.Add(this.btnExport);
            this.panelButtons.Controls.Add(this.btnClear);
            this.panelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.panelButtons.Location = new System.Drawing.Point(1, 457);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(649, 36);
            this.panelButtons.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(569, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Выгрузить";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.ExportClick);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(447, 3);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(116, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Очистить лог";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.ContextMenuClearClick);
            // 
            // contextMenuConsole
            // 
            this.contextMenuConsole.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuClear});
            this.contextMenuConsole.Name = "contextMenuConsole";
            this.contextMenuConsole.Size = new System.Drawing.Size(127, 26);
            // 
            // contextMenuClear
            // 
            this.contextMenuClear.Name = "contextMenuClear";
            this.contextMenuClear.Size = new System.Drawing.Size(126, 22);
            this.contextMenuClear.Text = "Очистить";
            this.contextMenuClear.Click += new System.EventHandler(this.ContextMenuClearClick);
            // 
            // listConsole
            // 
            this.listConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listConsole.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmDate,
            this.clmEvent});
            this.listConsole.ContextMenuStrip = this.contextMenuConsole;
            this.listConsole.FullRowSelect = true;
            this.listConsole.GridLines = true;
            this.listConsole.Location = new System.Drawing.Point(1, 83);
            this.listConsole.MultiSelect = false;
            this.listConsole.Name = "listConsole";
            this.listConsole.Size = new System.Drawing.Size(649, 372);
            this.listConsole.TabIndex = 3;
            this.listConsole.UseCompatibleStateImageBehavior = false;
            this.listConsole.View = System.Windows.Forms.View.Details;
            // 
            // clmDate
            // 
            this.clmDate.Text = "Дата";
            this.clmDate.Width = 87;
            // 
            // clmEvent
            // 
            this.clmEvent.Text = "Событие";
            this.clmEvent.Width = 553;
            // 
            // gbGlobalSatModel
            // 
            this.gbGlobalSatModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGlobalSatModel.Controls.Add(this.cbAutoClear);
            this.gbGlobalSatModel.Controls.Add(this.rbGlobalSatDG200);
            this.gbGlobalSatModel.Controls.Add(this.rbGlobalSatDG100);
            this.gbGlobalSatModel.Location = new System.Drawing.Point(1, 30);
            this.gbGlobalSatModel.Name = "gbGlobalSatModel";
            this.gbGlobalSatModel.Size = new System.Drawing.Size(649, 47);
            this.gbGlobalSatModel.TabIndex = 4;
            this.gbGlobalSatModel.TabStop = false;
            this.gbGlobalSatModel.Text = "Модель устройства:";
            // 
            // rbGlobalSatDG200
            // 
            this.rbGlobalSatDG200.AutoSize = true;
            this.rbGlobalSatDG200.Location = new System.Drawing.Point(133, 24);
            this.rbGlobalSatDG200.Name = "rbGlobalSatDG200";
            this.rbGlobalSatDG200.Size = new System.Drawing.Size(111, 17);
            this.rbGlobalSatDG200.TabIndex = 1;
            this.rbGlobalSatDG200.TabStop = true;
            this.rbGlobalSatDG200.Tag = "230400";
            this.rbGlobalSatDG200.Text = "GlobalSat DG 200";
            this.rbGlobalSatDG200.UseVisualStyleBackColor = true;
            this.rbGlobalSatDG200.CheckedChanged += new System.EventHandler(this.GlobalSatModelCheckedChanged);
            // 
            // rbGlobalSatDG100
            // 
            this.rbGlobalSatDG100.AutoSize = true;
            this.rbGlobalSatDG100.Location = new System.Drawing.Point(16, 24);
            this.rbGlobalSatDG100.Name = "rbGlobalSatDG100";
            this.rbGlobalSatDG100.Size = new System.Drawing.Size(111, 17);
            this.rbGlobalSatDG100.TabIndex = 0;
            this.rbGlobalSatDG100.TabStop = true;
            this.rbGlobalSatDG100.Tag = "115200";
            this.rbGlobalSatDG100.Text = "GlobalSat DG 100";
            this.rbGlobalSatDG100.UseVisualStyleBackColor = true;
            this.rbGlobalSatDG100.CheckedChanged += new System.EventHandler(this.GlobalSatModelCheckedChanged);
            // 
            // cbAutoClear
            // 
            this.cbAutoClear.AutoSize = true;
            this.cbAutoClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.cbAutoClear.Location = new System.Drawing.Point(450, 16);
            this.cbAutoClear.Name = "cbAutoClear";
            this.cbAutoClear.Size = new System.Drawing.Size(196, 28);
            this.cbAutoClear.TabIndex = 3;
            this.cbAutoClear.Text = "Очищать данные после выгрузки";
            this.cbAutoClear.UseVisualStyleBackColor = true;
            this.cbAutoClear.CheckedChanged += new System.EventHandler(this.cbAutoClear_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 516);
            this.Controls.Add(this.gbGlobalSatModel);
            this.Controls.Add(this.listConsole);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dg Manager.Net [h-qub v.2.0]";
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.contextMenuConsole.ResumeLayout(false);
            this.gbGlobalSatModel.ResumeLayout(false);
            this.gbGlobalSatModel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStatusLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem afToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolMenuOpen;
        private System.Windows.Forms.ToolStripMenuItem toolMenuPort;
        private System.Windows.Forms.ToolStripMenuItem com3;
        private System.Windows.Forms.ToolStripMenuItem com4;
        private System.Windows.Forms.ToolStripMenuItem com5;
        private System.Windows.Forms.ToolStripMenuItem com6;
        private System.Windows.Forms.ToolStripMenuItem com7;
        private System.Windows.Forms.ToolStripMenuItem com8;
        private System.Windows.Forms.ToolStripMenuItem com9;
        private System.Windows.Forms.ToolStripMenuItem com10;
        private System.Windows.Forms.ToolStripMenuItem com11;
        private System.Windows.Forms.ToolStripMenuItem com12;
        private System.Windows.Forms.ToolStripMenuItem com13;
        private System.Windows.Forms.ToolStripMenuItem com14;
        private System.Windows.Forms.ToolStripMenuItem com15;
        private System.Windows.Forms.ToolStripMenuItem com16;
        private System.Windows.Forms.ToolStripMenuItem com17;
        private System.Windows.Forms.FlowLayoutPanel panelButtons;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolMenuExit;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolSetId;
        private System.Windows.Forms.ContextMenuStrip contextMenuConsole;
        private System.Windows.Forms.ToolStripMenuItem contextMenuClear;
        private System.Windows.Forms.ToolStripMenuItem toolGetId;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolClear;
        private System.Windows.Forms.ToolStripMenuItem toolHelpMenu;
        private System.Windows.Forms.ToolStripMenuItem toolAbout;
        private System.Windows.Forms.ListView listConsole;
        private System.Windows.Forms.ColumnHeader clmDate;
        private System.Windows.Forms.ColumnHeader clmEvent;
        private System.Windows.Forms.ToolStripMenuItem com1;
        private System.Windows.Forms.ToolStripMenuItem com2;
        private System.Windows.Forms.ToolStripMenuItem toolRefreshPort;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.GroupBox gbGlobalSatModel;
        private System.Windows.Forms.RadioButton rbGlobalSatDG200;
        private System.Windows.Forms.RadioButton rbGlobalSatDG100;
        private System.Windows.Forms.CheckBox cbAutoClear;
    }
}

