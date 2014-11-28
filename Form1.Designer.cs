namespace KeySAV2
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tab_Main = new System.Windows.Forms.TabControl();
            this.Tab_BV = new System.Windows.Forms.TabPage();
            this.B_BKP_BV = new System.Windows.Forms.Button();
            this.L_KeyBV = new System.Windows.Forms.Label();
            this.L_BVTeam = new System.Windows.Forms.Label();
            this.CB_Team = new System.Windows.Forms.ComboBox();
            this.B_GoBV = new System.Windows.Forms.Button();
            this.B_OpenVideo = new System.Windows.Forms.Button();
            this.TB_BV = new System.Windows.Forms.TextBox();
            this.RTB_VID = new System.Windows.Forms.RichTextBox();
            this.Tab_SAV = new System.Windows.Forms.TabPage();
            this.GB_Filter = new System.Windows.Forms.GroupBox();
            this.CHKL_Missing_IVs = new System.Windows.Forms.CheckedListBox();
            this.CHK_Special_Attacker = new System.Windows.Forms.CheckBox();
            this.CHK_Trickroom = new System.Windows.Forms.CheckBox();
            this.CB_No_IVs = new System.Windows.Forms.ComboBox();
            this.L_No_IVs = new System.Windows.Forms.Label();
            this.L_HP_Type = new System.Windows.Forms.Label();
            this.CB_HP_Type = new System.Windows.Forms.ComboBox();
            this.CHK_Enable_Filtering = new System.Windows.Forms.CheckBox();
            this.B_BKP_SAV = new System.Windows.Forms.Button();
            this.L_SAVStats = new System.Windows.Forms.Label();
            this.L_BoxThru = new System.Windows.Forms.Label();
            this.CB_BoxEnd = new System.Windows.Forms.ComboBox();
            this.L_KeySAV = new System.Windows.Forms.Label();
            this.L_BoxSAV = new System.Windows.Forms.Label();
            this.CB_BoxStart = new System.Windows.Forms.ComboBox();
            this.RTB_SAV = new System.Windows.Forms.RichTextBox();
            this.B_GoSAV = new System.Windows.Forms.Button();
            this.B_OpenSAV = new System.Windows.Forms.Button();
            this.TB_SAV = new System.Windows.Forms.TextBox();
            this.Tab_Options = new System.Windows.Forms.TabPage();
            this.CHK_HideFirst = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CHK_R_Table = new System.Windows.Forms.CheckBox();
            this.CHK_ColorBox = new System.Windows.Forms.CheckBox();
            this.CB_BoxColor = new System.Windows.Forms.ComboBox();
            this.CHK_MarkFirst = new System.Windows.Forms.CheckBox();
            this.CHK_Split = new System.Windows.Forms.CheckBox();
            this.CHK_BoldIVs = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.B_BreakFolder = new System.Windows.Forms.Button();
            this.TB_Folder = new System.Windows.Forms.TextBox();
            this.B_Folder = new System.Windows.Forms.Button();
            this.L_instrSAV = new System.Windows.Forms.Label();
            this.L_instrBV = new System.Windows.Forms.Label();
            this.B_Break = new System.Windows.Forms.Button();
            this.B_File2 = new System.Windows.Forms.Button();
            this.B_File1 = new System.Windows.Forms.Button();
            this.TB_File2 = new System.Windows.Forms.TextBox();
            this.TB_File1 = new System.Windows.Forms.TextBox();
            this.B_ShowOptions = new System.Windows.Forms.Button();
            this.L_ExportStyle = new System.Windows.Forms.Label();
            this.CB_ExportStyle = new System.Windows.Forms.ComboBox();
            this.RTB_OPTIONS = new System.Windows.Forms.RichTextBox();
            this.CB_MainLanguage = new System.Windows.Forms.ComboBox();
            this.CB_Game = new System.Windows.Forms.ComboBox();
            this.RAD_IVs_Miss = new System.Windows.Forms.RadioButton();
            this.RAD_IVs = new System.Windows.Forms.RadioButton();
            this.CHK_Is_Shiny = new System.Windows.Forms.CheckBox();
            this.CHK_Hatches_Shiny_For_Me = new System.Windows.Forms.CheckBox();
            this.CHK_Hatches_Shiny_For = new System.Windows.Forms.CheckBox();
            this.TB_SVs = new System.Windows.Forms.TextBox();
            this.tab_Main.SuspendLayout();
            this.Tab_BV.SuspendLayout();
            this.Tab_SAV.SuspendLayout();
            this.GB_Filter.SuspendLayout();
            this.Tab_Options.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab_Main
            // 
            this.tab_Main.AllowDrop = true;
            this.tab_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab_Main.Controls.Add(this.Tab_BV);
            this.tab_Main.Controls.Add(this.Tab_SAV);
            this.tab_Main.Controls.Add(this.Tab_Options);
            this.tab_Main.Location = new System.Drawing.Point(12, 12);
            this.tab_Main.Name = "tab_Main";
            this.tab_Main.SelectedIndex = 0;
            this.tab_Main.Size = new System.Drawing.Size(330, 448);
            this.tab_Main.TabIndex = 3;
            // 
            // Tab_BV
            // 
            this.Tab_BV.Controls.Add(this.B_BKP_BV);
            this.Tab_BV.Controls.Add(this.L_KeyBV);
            this.Tab_BV.Controls.Add(this.L_BVTeam);
            this.Tab_BV.Controls.Add(this.CB_Team);
            this.Tab_BV.Controls.Add(this.B_GoBV);
            this.Tab_BV.Controls.Add(this.B_OpenVideo);
            this.Tab_BV.Controls.Add(this.TB_BV);
            this.Tab_BV.Controls.Add(this.RTB_VID);
            this.Tab_BV.Location = new System.Drawing.Point(4, 22);
            this.Tab_BV.Name = "Tab_BV";
            this.Tab_BV.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_BV.Size = new System.Drawing.Size(322, 422);
            this.Tab_BV.TabIndex = 0;
            this.Tab_BV.Text = "BV";
            this.Tab_BV.UseVisualStyleBackColor = true;
            // 
            // B_BKP_BV
            // 
            this.B_BKP_BV.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B_BKP_BV.Location = new System.Drawing.Point(233, 51);
            this.B_BKP_BV.Name = "B_BKP_BV";
            this.B_BKP_BV.Size = new System.Drawing.Size(55, 20);
            this.B_BKP_BV.TabIndex = 8;
            this.B_BKP_BV.Text = "Backup BV";
            this.B_BKP_BV.UseVisualStyleBackColor = true;
            this.B_BKP_BV.Visible = false;
            this.B_BKP_BV.Click += new System.EventHandler(this.B_BKP_BV_Click);
            // 
            // L_KeyBV
            // 
            this.L_KeyBV.AutoSize = true;
            this.L_KeyBV.Location = new System.Drawing.Point(10, 32);
            this.L_KeyBV.Name = "L_KeyBV";
            this.L_KeyBV.Size = new System.Drawing.Size(0, 13);
            this.L_KeyBV.TabIndex = 20;
            // 
            // L_BVTeam
            // 
            this.L_BVTeam.AutoSize = true;
            this.L_BVTeam.Location = new System.Drawing.Point(85, 53);
            this.L_BVTeam.Name = "L_BVTeam";
            this.L_BVTeam.Size = new System.Drawing.Size(37, 13);
            this.L_BVTeam.TabIndex = 19;
            this.L_BVTeam.Text = "Team:";
            // 
            // CB_Team
            // 
            this.CB_Team.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Team.Enabled = false;
            this.CB_Team.FormattingEnabled = true;
            this.CB_Team.Items.AddRange(new object[] {
            "My Team",
            "Opponent"});
            this.CB_Team.Location = new System.Drawing.Point(128, 50);
            this.CB_Team.Name = "CB_Team";
            this.CB_Team.Size = new System.Drawing.Size(102, 21);
            this.CB_Team.TabIndex = 7;
            // 
            // B_GoBV
            // 
            this.B_GoBV.Enabled = false;
            this.B_GoBV.Location = new System.Drawing.Point(7, 48);
            this.B_GoBV.Name = "B_GoBV";
            this.B_GoBV.Size = new System.Drawing.Size(33, 23);
            this.B_GoBV.TabIndex = 6;
            this.B_GoBV.Text = "Go";
            this.B_GoBV.UseVisualStyleBackColor = true;
            this.B_GoBV.Click += new System.EventHandler(this.dumpBV);
            // 
            // B_OpenVideo
            // 
            this.B_OpenVideo.Location = new System.Drawing.Point(7, 8);
            this.B_OpenVideo.Name = "B_OpenVideo";
            this.B_OpenVideo.Size = new System.Drawing.Size(75, 23);
            this.B_OpenVideo.TabIndex = 4;
            this.B_OpenVideo.Text = "Open Video";
            this.B_OpenVideo.UseVisualStyleBackColor = true;
            this.B_OpenVideo.Click += new System.EventHandler(this.B_OpenVid_Click);
            // 
            // TB_BV
            // 
            this.TB_BV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_BV.Location = new System.Drawing.Point(88, 9);
            this.TB_BV.Name = "TB_BV";
            this.TB_BV.ReadOnly = true;
            this.TB_BV.Size = new System.Drawing.Size(226, 20);
            this.TB_BV.TabIndex = 5;
            // 
            // RTB_VID
            // 
            this.RTB_VID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB_VID.Location = new System.Drawing.Point(0, 77);
            this.RTB_VID.Name = "RTB_VID";
            this.RTB_VID.ReadOnly = true;
            this.RTB_VID.Size = new System.Drawing.Size(320, 341);
            this.RTB_VID.TabIndex = 31;
            this.RTB_VID.Text = "";
            this.RTB_VID.WordWrap = false;
            // 
            // Tab_SAV
            // 
            this.Tab_SAV.Controls.Add(this.GB_Filter);
            this.Tab_SAV.Controls.Add(this.B_BKP_SAV);
            this.Tab_SAV.Controls.Add(this.L_SAVStats);
            this.Tab_SAV.Controls.Add(this.L_BoxThru);
            this.Tab_SAV.Controls.Add(this.CB_BoxEnd);
            this.Tab_SAV.Controls.Add(this.L_KeySAV);
            this.Tab_SAV.Controls.Add(this.L_BoxSAV);
            this.Tab_SAV.Controls.Add(this.CB_BoxStart);
            this.Tab_SAV.Controls.Add(this.RTB_SAV);
            this.Tab_SAV.Controls.Add(this.B_GoSAV);
            this.Tab_SAV.Controls.Add(this.B_OpenSAV);
            this.Tab_SAV.Controls.Add(this.TB_SAV);
            this.Tab_SAV.Location = new System.Drawing.Point(4, 22);
            this.Tab_SAV.Name = "Tab_SAV";
            this.Tab_SAV.Padding = new System.Windows.Forms.Padding(3);
            this.Tab_SAV.Size = new System.Drawing.Size(322, 422);
            this.Tab_SAV.TabIndex = 1;
            this.Tab_SAV.Text = "SAV";
            this.Tab_SAV.UseVisualStyleBackColor = true;
            // 
            // GB_Filter
            // 
            this.GB_Filter.Controls.Add(this.TB_SVs);
            this.GB_Filter.Controls.Add(this.CHK_Hatches_Shiny_For);
            this.GB_Filter.Controls.Add(this.CHK_Hatches_Shiny_For_Me);
            this.GB_Filter.Controls.Add(this.CHK_Is_Shiny);
            this.GB_Filter.Controls.Add(this.RAD_IVs);
            this.GB_Filter.Controls.Add(this.RAD_IVs_Miss);
            this.GB_Filter.Controls.Add(this.CHKL_Missing_IVs);
            this.GB_Filter.Controls.Add(this.CHK_Special_Attacker);
            this.GB_Filter.Controls.Add(this.CHK_Trickroom);
            this.GB_Filter.Controls.Add(this.CB_No_IVs);
            this.GB_Filter.Controls.Add(this.L_No_IVs);
            this.GB_Filter.Controls.Add(this.L_HP_Type);
            this.GB_Filter.Controls.Add(this.CB_HP_Type);
            this.GB_Filter.Controls.Add(this.CHK_Enable_Filtering);
            this.GB_Filter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GB_Filter.Location = new System.Drawing.Point(3, 77);
            this.GB_Filter.Name = "GB_Filter";
            this.GB_Filter.Size = new System.Drawing.Size(316, 166);
            this.GB_Filter.TabIndex = 31;
            this.GB_Filter.TabStop = false;
            this.GB_Filter.Text = "Filtering";
            // 
            // CHKL_Missing_IVs
            // 
            this.CHKL_Missing_IVs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CHKL_Missing_IVs.ColumnWidth = 74;
            this.CHKL_Missing_IVs.FormattingEnabled = true;
            this.CHKL_Missing_IVs.Items.AddRange(new object[] {
            "HP",
            "Atk",
            "Def",
            "Sp. Atk",
            "Sp. Def",
            "Spe"});
            this.CHKL_Missing_IVs.Location = new System.Drawing.Point(135, 70);
            this.CHKL_Missing_IVs.MultiColumn = true;
            this.CHKL_Missing_IVs.Name = "CHKL_Missing_IVs";
            this.CHKL_Missing_IVs.Size = new System.Drawing.Size(175, 45);
            this.CHKL_Missing_IVs.TabIndex = 8;
            // 
            // CHK_Special_Attacker
            // 
            this.CHK_Special_Attacker.AutoSize = true;
            this.CHK_Special_Attacker.Location = new System.Drawing.Point(209, 41);
            this.CHK_Special_Attacker.Name = "CHK_Special_Attacker";
            this.CHK_Special_Attacker.Size = new System.Drawing.Size(104, 17);
            this.CHK_Special_Attacker.TabIndex = 6;
            this.CHK_Special_Attacker.Text = "Special Attacker";
            this.CHK_Special_Attacker.UseVisualStyleBackColor = true;
            // 
            // CHK_Trickroom
            // 
            this.CHK_Trickroom.AutoSize = true;
            this.CHK_Trickroom.Location = new System.Drawing.Point(135, 41);
            this.CHK_Trickroom.Name = "CHK_Trickroom";
            this.CHK_Trickroom.Size = new System.Drawing.Size(73, 17);
            this.CHK_Trickroom.TabIndex = 5;
            this.CHK_Trickroom.Text = "Trickroom";
            this.CHK_Trickroom.UseVisualStyleBackColor = true;
            // 
            // CB_No_IVs
            // 
            this.CB_No_IVs.FormattingEnabled = true;
            this.CB_No_IVs.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.CB_No_IVs.Location = new System.Drawing.Point(93, 39);
            this.CB_No_IVs.Name = "CB_No_IVs";
            this.CB_No_IVs.Size = new System.Drawing.Size(32, 21);
            this.CB_No_IVs.TabIndex = 4;
            // 
            // L_No_IVs
            // 
            this.L_No_IVs.AutoSize = true;
            this.L_No_IVs.Location = new System.Drawing.Point(1, 42);
            this.L_No_IVs.Name = "L_No_IVs";
            this.L_No_IVs.Size = new System.Drawing.Size(90, 13);
            this.L_No_IVs.TabIndex = 3;
            this.L_No_IVs.Text = "No. of perfect IVs";
            // 
            // L_HP_Type
            // 
            this.L_HP_Type.AutoSize = true;
            this.L_HP_Type.Location = new System.Drawing.Point(132, 17);
            this.L_HP_Type.Name = "L_HP_Type";
            this.L_HP_Type.Size = new System.Drawing.Size(49, 13);
            this.L_HP_Type.TabIndex = 2;
            this.L_HP_Type.Text = "HP Type";
            // 
            // CB_HP_Type
            // 
            this.CB_HP_Type.FormattingEnabled = true;
            this.CB_HP_Type.Items.AddRange(new object[] {
            "Any",
            "Fighting",
            "Flying",
            "Poison",
            "Ground",
            "Rock",
            "Bug",
            "Ghost",
            "Steel",
            "Fire",
            "Water",
            "Grass",
            "Electric",
            "Psychic",
            "Ice",
            "Dragon",
            "Dark"});
            this.CB_HP_Type.Location = new System.Drawing.Point(195, 14);
            this.CB_HP_Type.Name = "CB_HP_Type";
            this.CB_HP_Type.Size = new System.Drawing.Size(115, 21);
            this.CB_HP_Type.TabIndex = 1;
            // 
            // CHK_Enable_Filtering
            // 
            this.CHK_Enable_Filtering.AutoSize = true;
            this.CHK_Enable_Filtering.Location = new System.Drawing.Point(5, 16);
            this.CHK_Enable_Filtering.Name = "CHK_Enable_Filtering";
            this.CHK_Enable_Filtering.Size = new System.Drawing.Size(95, 17);
            this.CHK_Enable_Filtering.TabIndex = 0;
            this.CHK_Enable_Filtering.Text = "Enable filtering";
            this.CHK_Enable_Filtering.UseVisualStyleBackColor = true;
            // 
            // B_BKP_SAV
            // 
            this.B_BKP_SAV.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B_BKP_SAV.Location = new System.Drawing.Point(233, 51);
            this.B_BKP_SAV.Name = "B_BKP_SAV";
            this.B_BKP_SAV.Size = new System.Drawing.Size(55, 20);
            this.B_BKP_SAV.TabIndex = 13;
            this.B_BKP_SAV.Text = "Backup SAV";
            this.B_BKP_SAV.UseVisualStyleBackColor = true;
            this.B_BKP_SAV.Visible = false;
            this.B_BKP_SAV.Click += new System.EventHandler(this.B_BKP_SAV_Click);
            // 
            // L_SAVStats
            // 
            this.L_SAVStats.AutoSize = true;
            this.L_SAVStats.Location = new System.Drawing.Point(240, 53);
            this.L_SAVStats.Name = "L_SAVStats";
            this.L_SAVStats.Size = new System.Drawing.Size(0, 13);
            this.L_SAVStats.TabIndex = 25;
            // 
            // L_BoxThru
            // 
            this.L_BoxThru.AutoSize = true;
            this.L_BoxThru.Location = new System.Drawing.Point(174, 53);
            this.L_BoxThru.Name = "L_BoxThru";
            this.L_BoxThru.Size = new System.Drawing.Size(10, 13);
            this.L_BoxThru.TabIndex = 24;
            this.L_BoxThru.Text = "-";
            this.L_BoxThru.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.L_BoxThru.Visible = false;
            // 
            // CB_BoxEnd
            // 
            this.CB_BoxEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_BoxEnd.Enabled = false;
            this.CB_BoxEnd.FormattingEnabled = true;
            this.CB_BoxEnd.Items.AddRange(new object[] {
            "Parsed Text",
            ".PK6 Files"});
            this.CB_BoxEnd.Location = new System.Drawing.Point(190, 50);
            this.CB_BoxEnd.Name = "CB_BoxEnd";
            this.CB_BoxEnd.Size = new System.Drawing.Size(40, 21);
            this.CB_BoxEnd.TabIndex = 12;
            this.CB_BoxEnd.Visible = false;
            // 
            // L_KeySAV
            // 
            this.L_KeySAV.AutoSize = true;
            this.L_KeySAV.Location = new System.Drawing.Point(10, 32);
            this.L_KeySAV.Name = "L_KeySAV";
            this.L_KeySAV.Size = new System.Drawing.Size(0, 13);
            this.L_KeySAV.TabIndex = 22;
            // 
            // L_BoxSAV
            // 
            this.L_BoxSAV.AutoSize = true;
            this.L_BoxSAV.Location = new System.Drawing.Point(85, 53);
            this.L_BoxSAV.Name = "L_BoxSAV";
            this.L_BoxSAV.Size = new System.Drawing.Size(28, 13);
            this.L_BoxSAV.TabIndex = 21;
            this.L_BoxSAV.Text = "Box:";
            // 
            // CB_BoxStart
            // 
            this.CB_BoxStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_BoxStart.Enabled = false;
            this.CB_BoxStart.FormattingEnabled = true;
            this.CB_BoxStart.Items.AddRange(new object[] {
            "All",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.CB_BoxStart.Location = new System.Drawing.Point(128, 50);
            this.CB_BoxStart.Name = "CB_BoxStart";
            this.CB_BoxStart.Size = new System.Drawing.Size(40, 21);
            this.CB_BoxStart.TabIndex = 11;
            this.CB_BoxStart.SelectedIndexChanged += new System.EventHandler(this.changeboxsetting);
            // 
            // RTB_SAV
            // 
            this.RTB_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB_SAV.Location = new System.Drawing.Point(0, 249);
            this.RTB_SAV.Name = "RTB_SAV";
            this.RTB_SAV.ReadOnly = true;
            this.RTB_SAV.Size = new System.Drawing.Size(320, 170);
            this.RTB_SAV.TabIndex = 30;
            this.RTB_SAV.Text = "";
            this.RTB_SAV.WordWrap = false;
            // 
            // B_GoSAV
            // 
            this.B_GoSAV.Enabled = false;
            this.B_GoSAV.Location = new System.Drawing.Point(7, 48);
            this.B_GoSAV.Name = "B_GoSAV";
            this.B_GoSAV.Size = new System.Drawing.Size(33, 23);
            this.B_GoSAV.TabIndex = 10;
            this.B_GoSAV.Text = "Go";
            this.B_GoSAV.UseVisualStyleBackColor = true;
            this.B_GoSAV.Click += new System.EventHandler(this.DumpSAV);
            // 
            // B_OpenSAV
            // 
            this.B_OpenSAV.Location = new System.Drawing.Point(7, 8);
            this.B_OpenSAV.Name = "B_OpenSAV";
            this.B_OpenSAV.Size = new System.Drawing.Size(75, 23);
            this.B_OpenSAV.TabIndex = 9;
            this.B_OpenSAV.Text = "Open SAV";
            this.B_OpenSAV.UseVisualStyleBackColor = true;
            this.B_OpenSAV.Click += new System.EventHandler(this.B_OpenSAV_Click);
            // 
            // TB_SAV
            // 
            this.TB_SAV.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_SAV.Location = new System.Drawing.Point(88, 9);
            this.TB_SAV.Name = "TB_SAV";
            this.TB_SAV.ReadOnly = true;
            this.TB_SAV.Size = new System.Drawing.Size(226, 20);
            this.TB_SAV.TabIndex = 7;
            // 
            // Tab_Options
            // 
            this.Tab_Options.Controls.Add(this.CHK_HideFirst);
            this.Tab_Options.Controls.Add(this.label1);
            this.Tab_Options.Controls.Add(this.CHK_R_Table);
            this.Tab_Options.Controls.Add(this.CHK_ColorBox);
            this.Tab_Options.Controls.Add(this.CB_BoxColor);
            this.Tab_Options.Controls.Add(this.CHK_MarkFirst);
            this.Tab_Options.Controls.Add(this.CHK_Split);
            this.Tab_Options.Controls.Add(this.CHK_BoldIVs);
            this.Tab_Options.Controls.Add(this.groupBox1);
            this.Tab_Options.Controls.Add(this.B_ShowOptions);
            this.Tab_Options.Controls.Add(this.L_ExportStyle);
            this.Tab_Options.Controls.Add(this.CB_ExportStyle);
            this.Tab_Options.Controls.Add(this.RTB_OPTIONS);
            this.Tab_Options.Location = new System.Drawing.Point(4, 22);
            this.Tab_Options.Name = "Tab_Options";
            this.Tab_Options.Size = new System.Drawing.Size(322, 346);
            this.Tab_Options.TabIndex = 2;
            this.Tab_Options.Text = "Options";
            this.Tab_Options.UseVisualStyleBackColor = true;
            // 
            // CHK_HideFirst
            // 
            this.CHK_HideFirst.AutoSize = true;
            this.CHK_HideFirst.Location = new System.Drawing.Point(214, 77);
            this.CHK_HideFirst.Name = "CHK_HideFirst";
            this.CHK_HideFirst.Size = new System.Drawing.Size(81, 17);
            this.CHK_HideFirst.TabIndex = 21;
            this.CHK_HideFirst.Text = "Don\'t Show";
            this.CHK_HideFirst.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "<100% data";
            // 
            // CHK_R_Table
            // 
            this.CHK_R_Table.AutoSize = true;
            this.CHK_R_Table.Location = new System.Drawing.Point(128, 31);
            this.CHK_R_Table.Name = "CHK_R_Table";
            this.CHK_R_Table.Size = new System.Drawing.Size(70, 17);
            this.CHK_R_Table.TabIndex = 17;
            this.CHK_R_Table.Text = "[R] Table";
            this.CHK_R_Table.UseVisualStyleBackColor = true;
            this.CHK_R_Table.Visible = false;
            this.CHK_R_Table.CheckedChanged += new System.EventHandler(this.changeTableStatus);
            this.CHK_R_Table.Click += new System.EventHandler(this.changeTableStatus);
            // 
            // CHK_ColorBox
            // 
            this.CHK_ColorBox.AutoSize = true;
            this.CHK_ColorBox.Checked = true;
            this.CHK_ColorBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHK_ColorBox.Location = new System.Drawing.Point(17, 77);
            this.CHK_ColorBox.Name = "CHK_ColorBox";
            this.CHK_ColorBox.Size = new System.Drawing.Size(99, 17);
            this.CHK_ColorBox.TabIndex = 19;
            this.CHK_ColorBox.Text = "[R] Color Boxes";
            this.CHK_ColorBox.UseVisualStyleBackColor = true;
            // 
            // CB_BoxColor
            // 
            this.CB_BoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_BoxColor.FormattingEnabled = true;
            this.CB_BoxColor.Items.AddRange(new object[] {
            "Cycle",
            "Default",
            "Blue",
            "Green",
            "Yellow",
            "Red"});
            this.CB_BoxColor.Location = new System.Drawing.Point(122, 74);
            this.CB_BoxColor.Name = "CB_BoxColor";
            this.CB_BoxColor.Size = new System.Drawing.Size(80, 21);
            this.CB_BoxColor.TabIndex = 20;
            // 
            // CHK_MarkFirst
            // 
            this.CHK_MarkFirst.AutoSize = true;
            this.CHK_MarkFirst.Checked = true;
            this.CHK_MarkFirst.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHK_MarkFirst.Location = new System.Drawing.Point(214, 62);
            this.CHK_MarkFirst.Name = "CHK_MarkFirst";
            this.CHK_MarkFirst.Size = new System.Drawing.Size(60, 17);
            this.CHK_MarkFirst.TabIndex = 22;
            this.CHK_MarkFirst.Text = "Mark ~";
            this.CHK_MarkFirst.UseVisualStyleBackColor = true;
            // 
            // CHK_Split
            // 
            this.CHK_Split.AutoSize = true;
            this.CHK_Split.Location = new System.Drawing.Point(17, 31);
            this.CHK_Split.Name = "CHK_Split";
            this.CHK_Split.Size = new System.Drawing.Size(78, 17);
            this.CHK_Split.TabIndex = 16;
            this.CHK_Split.Text = "Split Boxes";
            this.CHK_Split.UseVisualStyleBackColor = true;
            // 
            // CHK_BoldIVs
            // 
            this.CHK_BoldIVs.AutoSize = true;
            this.CHK_BoldIVs.Checked = true;
            this.CHK_BoldIVs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHK_BoldIVs.Location = new System.Drawing.Point(17, 54);
            this.CHK_BoldIVs.Name = "CHK_BoldIVs";
            this.CHK_BoldIVs.Size = new System.Drawing.Size(119, 17);
            this.CHK_BoldIVs.TabIndex = 18;
            this.CHK_BoldIVs.Text = "[R] Bold Perfect IVs";
            this.CHK_BoldIVs.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.B_BreakFolder);
            this.groupBox1.Controls.Add(this.TB_Folder);
            this.groupBox1.Controls.Add(this.B_Folder);
            this.groupBox1.Controls.Add(this.L_instrSAV);
            this.groupBox1.Controls.Add(this.L_instrBV);
            this.groupBox1.Controls.Add(this.B_Break);
            this.groupBox1.Controls.Add(this.B_File2);
            this.groupBox1.Controls.Add(this.B_File1);
            this.groupBox1.Controls.Add(this.TB_File2);
            this.groupBox1.Controls.Add(this.TB_File1);
            this.groupBox1.Location = new System.Drawing.Point(1, 191);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 154);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Breaker";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Improve any keys with saves from this folder:";
            // 
            // B_BreakFolder
            // 
            this.B_BreakFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.B_BreakFolder.Enabled = false;
            this.B_BreakFolder.Location = new System.Drawing.Point(239, 100);
            this.B_BreakFolder.Name = "B_BreakFolder";
            this.B_BreakFolder.Size = new System.Drawing.Size(75, 23);
            this.B_BreakFolder.TabIndex = 31;
            this.B_BreakFolder.Text = "Break";
            this.B_BreakFolder.UseVisualStyleBackColor = true;
            this.B_BreakFolder.Click += new System.EventHandler(this.B_BreakFolder_Click);
            // 
            // TB_Folder
            // 
            this.TB_Folder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Folder.Location = new System.Drawing.Point(85, 130);
            this.TB_Folder.Name = "TB_Folder";
            this.TB_Folder.ReadOnly = true;
            this.TB_Folder.Size = new System.Drawing.Size(229, 20);
            this.TB_Folder.TabIndex = 30;
            // 
            // B_Folder
            // 
            this.B_Folder.Location = new System.Drawing.Point(5, 129);
            this.B_Folder.Name = "B_Folder";
            this.B_Folder.Size = new System.Drawing.Size(75, 23);
            this.B_Folder.TabIndex = 29;
            this.B_Folder.Text = "Folder";
            this.B_Folder.UseVisualStyleBackColor = true;
            this.B_Folder.Click += new System.EventHandler(this.loadBreakFolder);
            // 
            // L_instrSAV
            // 
            this.L_instrSAV.AutoSize = true;
            this.L_instrSAV.Location = new System.Drawing.Point(22, 27);
            this.L_instrSAV.Name = "L_instrSAV";
            this.L_instrSAV.Size = new System.Drawing.Size(158, 13);
            this.L_instrSAV.TabIndex = 22;
            this.L_instrSAV.Text = "SAV: File 1 = \"26\", File 2 = \"16\"";
            // 
            // L_instrBV
            // 
            this.L_instrBV.AutoSize = true;
            this.L_instrBV.Location = new System.Drawing.Point(29, 14);
            this.L_instrBV.Name = "L_instrBV";
            this.L_instrBV.Size = new System.Drawing.Size(148, 13);
            this.L_instrBV.TabIndex = 21;
            this.L_instrBV.Text = "BV: File 1 = \"-1\",  File 2 = \"-2\"";
            // 
            // B_Break
            // 
            this.B_Break.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.B_Break.Enabled = false;
            this.B_Break.Location = new System.Drawing.Point(239, 17);
            this.B_Break.Name = "B_Break";
            this.B_Break.Size = new System.Drawing.Size(75, 23);
            this.B_Break.TabIndex = 28;
            this.B_Break.Text = "Break";
            this.B_Break.UseVisualStyleBackColor = true;
            this.B_Break.Click += new System.EventHandler(this.B_Break_Click);
            // 
            // B_File2
            // 
            this.B_File2.Location = new System.Drawing.Point(5, 71);
            this.B_File2.Name = "B_File2";
            this.B_File2.Size = new System.Drawing.Size(75, 23);
            this.B_File2.TabIndex = 26;
            this.B_File2.Text = "File 2";
            this.B_File2.UseVisualStyleBackColor = true;
            this.B_File2.Click += new System.EventHandler(this.loadBreak2);
            // 
            // B_File1
            // 
            this.B_File1.Location = new System.Drawing.Point(5, 42);
            this.B_File1.Name = "B_File1";
            this.B_File1.Size = new System.Drawing.Size(75, 23);
            this.B_File1.TabIndex = 24;
            this.B_File1.Text = "File 1";
            this.B_File1.UseVisualStyleBackColor = true;
            this.B_File1.Click += new System.EventHandler(this.loadBreak1);
            // 
            // TB_File2
            // 
            this.TB_File2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_File2.Location = new System.Drawing.Point(85, 72);
            this.TB_File2.Name = "TB_File2";
            this.TB_File2.ReadOnly = true;
            this.TB_File2.Size = new System.Drawing.Size(229, 20);
            this.TB_File2.TabIndex = 27;
            // 
            // TB_File1
            // 
            this.TB_File1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_File1.Location = new System.Drawing.Point(85, 44);
            this.TB_File1.Name = "TB_File1";
            this.TB_File1.ReadOnly = true;
            this.TB_File1.Size = new System.Drawing.Size(229, 20);
            this.TB_File1.TabIndex = 25;
            // 
            // B_ShowOptions
            // 
            this.B_ShowOptions.Location = new System.Drawing.Point(214, 3);
            this.B_ShowOptions.Name = "B_ShowOptions";
            this.B_ShowOptions.Size = new System.Drawing.Size(75, 35);
            this.B_ShowOptions.TabIndex = 15;
            this.B_ShowOptions.Text = "Show Export Strings";
            this.B_ShowOptions.UseVisualStyleBackColor = true;
            this.B_ShowOptions.Click += new System.EventHandler(this.B_ShowOptions_Click);
            // 
            // L_ExportStyle
            // 
            this.L_ExportStyle.AutoSize = true;
            this.L_ExportStyle.Location = new System.Drawing.Point(14, 7);
            this.L_ExportStyle.Name = "L_ExportStyle";
            this.L_ExportStyle.Size = new System.Drawing.Size(66, 13);
            this.L_ExportStyle.TabIndex = 14;
            this.L_ExportStyle.Text = "Export Style:";
            // 
            // CB_ExportStyle
            // 
            this.CB_ExportStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_ExportStyle.FormattingEnabled = true;
            this.CB_ExportStyle.Items.AddRange(new object[] {
            "Default",
            "Reddit",
            "TSV",
            "Custom 1",
            "Custom 2",
            "Custom 3",
            "CSV",
            "To .PK6 File"});
            this.CB_ExportStyle.Location = new System.Drawing.Point(86, 4);
            this.CB_ExportStyle.Name = "CB_ExportStyle";
            this.CB_ExportStyle.Size = new System.Drawing.Size(112, 21);
            this.CB_ExportStyle.TabIndex = 14;
            this.CB_ExportStyle.SelectedIndexChanged += new System.EventHandler(this.changeExportStyle);
            // 
            // RTB_OPTIONS
            // 
            this.RTB_OPTIONS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB_OPTIONS.Location = new System.Drawing.Point(0, 100);
            this.RTB_OPTIONS.Name = "RTB_OPTIONS";
            this.RTB_OPTIONS.ReadOnly = true;
            this.RTB_OPTIONS.Size = new System.Drawing.Size(320, 90);
            this.RTB_OPTIONS.TabIndex = 29;
            this.RTB_OPTIONS.Text = "";
            this.RTB_OPTIONS.ReadOnlyChanged += new System.EventHandler(this.changeReadOnly);
            this.RTB_OPTIONS.TextChanged += new System.EventHandler(this.changeFormatText);
            // 
            // CB_MainLanguage
            // 
            this.CB_MainLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CB_MainLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_MainLanguage.FormattingEnabled = true;
            this.CB_MainLanguage.Items.AddRange(new object[] {
            "English",
            "日本語",
            "Français",
            "Italiano",
            "Deutsch",
            "Español",
            "한국어"});
            this.CB_MainLanguage.Location = new System.Drawing.Point(214, 11);
            this.CB_MainLanguage.Name = "CB_MainLanguage";
            this.CB_MainLanguage.Size = new System.Drawing.Size(81, 21);
            this.CB_MainLanguage.TabIndex = 1;
            this.CB_MainLanguage.SelectedIndexChanged += new System.EventHandler(this.changeLanguage);
            // 
            // CB_Game
            // 
            this.CB_Game.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CB_Game.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Game.FormattingEnabled = true;
            this.CB_Game.Items.AddRange(new object[] {
            "X",
            "Y",
            "R",
            "S"});
            this.CB_Game.Location = new System.Drawing.Point(299, 11);
            this.CB_Game.Name = "CB_Game";
            this.CB_Game.Size = new System.Drawing.Size(41, 21);
            this.CB_Game.TabIndex = 2;
            this.CB_Game.SelectedIndexChanged += new System.EventHandler(this.changedetectgame);
            // 
            // RAD_IVs_Miss
            // 
            this.RAD_IVs_Miss.AutoSize = true;
            this.RAD_IVs_Miss.Checked = true;
            this.RAD_IVs_Miss.Location = new System.Drawing.Point(6, 70);
            this.RAD_IVs_Miss.Name = "RAD_IVs_Miss";
            this.RAD_IVs_Miss.Size = new System.Drawing.Size(85, 17);
            this.RAD_IVs_Miss.TabIndex = 9;
            this.RAD_IVs_Miss.TabStop = true;
            this.RAD_IVs_Miss.Text = "IVs may miss";
            this.RAD_IVs_Miss.UseVisualStyleBackColor = true;
            // 
            // RAD_IVs
            // 
            this.RAD_IVs.AutoSize = true;
            this.RAD_IVs.Location = new System.Drawing.Point(6, 93);
            this.RAD_IVs.Name = "RAD_IVs";
            this.RAD_IVs.Size = new System.Drawing.Size(96, 17);
            this.RAD_IVs.TabIndex = 10;
            this.RAD_IVs.Text = "IVs musn\'t miss";
            this.RAD_IVs.UseVisualStyleBackColor = true;
            // 
            // CHK_Is_Shiny
            // 
            this.CHK_Is_Shiny.AutoSize = true;
            this.CHK_Is_Shiny.Location = new System.Drawing.Point(6, 121);
            this.CHK_Is_Shiny.Name = "CHK_Is_Shiny";
            this.CHK_Is_Shiny.Size = new System.Drawing.Size(61, 17);
            this.CHK_Is_Shiny.TabIndex = 11;
            this.CHK_Is_Shiny.Text = "Is shiny";
            this.CHK_Is_Shiny.UseVisualStyleBackColor = true;
            // 
            // CHK_Hatches_Shiny_For_Me
            // 
            this.CHK_Hatches_Shiny_For_Me.AutoSize = true;
            this.CHK_Hatches_Shiny_For_Me.Location = new System.Drawing.Point(135, 121);
            this.CHK_Hatches_Shiny_For_Me.Name = "CHK_Hatches_Shiny_For_Me";
            this.CHK_Hatches_Shiny_For_Me.Size = new System.Drawing.Size(125, 17);
            this.CHK_Hatches_Shiny_For_Me.TabIndex = 12;
            this.CHK_Hatches_Shiny_For_Me.Text = "Hatches shiny for me";
            this.CHK_Hatches_Shiny_For_Me.UseVisualStyleBackColor = true;
            // 
            // CHK_Hatches_Shiny_For
            // 
            this.CHK_Hatches_Shiny_For.AutoSize = true;
            this.CHK_Hatches_Shiny_For.Location = new System.Drawing.Point(6, 144);
            this.CHK_Hatches_Shiny_For.Name = "CHK_Hatches_Shiny_For";
            this.CHK_Hatches_Shiny_For.Size = new System.Drawing.Size(108, 17);
            this.CHK_Hatches_Shiny_For.TabIndex = 13;
            this.CHK_Hatches_Shiny_For.Text = "Hatches shiny for";
            this.CHK_Hatches_Shiny_For.UseVisualStyleBackColor = true;
            // 
            // TB_SVs
            // 
            this.TB_SVs.Location = new System.Drawing.Point(135, 142);
            this.TB_SVs.Name = "TB_SVs";
            this.TB_SVs.Size = new System.Drawing.Size(175, 20);
            this.TB_SVs.TabIndex = 14;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 467);
            this.Controls.Add(this.CB_Game);
            this.Controls.Add(this.CB_MainLanguage);
            this.Controls.Add(this.tab_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(700, 750);
            this.MinimumSize = new System.Drawing.Size(370, 370);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KeySAV";
            this.tab_Main.ResumeLayout(false);
            this.Tab_BV.ResumeLayout(false);
            this.Tab_BV.PerformLayout();
            this.Tab_SAV.ResumeLayout(false);
            this.Tab_SAV.PerformLayout();
            this.GB_Filter.ResumeLayout(false);
            this.GB_Filter.PerformLayout();
            this.Tab_Options.ResumeLayout(false);
            this.Tab_Options.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab_Main;
        private System.Windows.Forms.TabPage Tab_BV;
        private System.Windows.Forms.TabPage Tab_SAV;
        private System.Windows.Forms.Button B_GoSAV;
        private System.Windows.Forms.Button B_OpenSAV;
        private System.Windows.Forms.TextBox TB_SAV;
        private System.Windows.Forms.TabPage Tab_Options;
        private System.Windows.Forms.Button B_GoBV;
        private System.Windows.Forms.Button B_OpenVideo;
        private System.Windows.Forms.TextBox TB_BV;
        private System.Windows.Forms.RichTextBox RTB_VID;
        private System.Windows.Forms.RichTextBox RTB_SAV;
        private System.Windows.Forms.Button B_ShowOptions;
        private System.Windows.Forms.Button B_Break;
        private System.Windows.Forms.TextBox TB_File2;
        private System.Windows.Forms.TextBox TB_File1;
        private System.Windows.Forms.Button B_File2;
        private System.Windows.Forms.Button B_File1;
        private System.Windows.Forms.Label L_ExportStyle;
        private System.Windows.Forms.ComboBox CB_ExportStyle;
        private System.Windows.Forms.RichTextBox RTB_OPTIONS;
        private System.Windows.Forms.Label L_BoxSAV;
        private System.Windows.Forms.ComboBox CB_BoxStart;
        private System.Windows.Forms.ComboBox CB_MainLanguage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CB_Game;
        private System.Windows.Forms.Label L_KeyBV;
        private System.Windows.Forms.Label L_BVTeam;
        private System.Windows.Forms.ComboBox CB_Team;
        private System.Windows.Forms.Label L_BoxThru;
        private System.Windows.Forms.ComboBox CB_BoxEnd;
        private System.Windows.Forms.Label L_KeySAV;
        private System.Windows.Forms.CheckBox CHK_MarkFirst;
        private System.Windows.Forms.Label L_SAVStats;
        private System.Windows.Forms.ComboBox CB_BoxColor;
        private System.Windows.Forms.CheckBox CHK_Split;
        private System.Windows.Forms.CheckBox CHK_BoldIVs;
        private System.Windows.Forms.CheckBox CHK_ColorBox;
        private System.Windows.Forms.CheckBox CHK_R_Table;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button B_BKP_BV;
        private System.Windows.Forms.Button B_BKP_SAV;
        private System.Windows.Forms.CheckBox CHK_HideFirst;
        private System.Windows.Forms.Label L_instrSAV;
        private System.Windows.Forms.Label L_instrBV;
        private System.Windows.Forms.TextBox TB_Folder;
        private System.Windows.Forms.Button B_Folder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button B_BreakFolder;
        private System.Windows.Forms.GroupBox GB_Filter;
        private System.Windows.Forms.Label L_HP_Type;
        private System.Windows.Forms.ComboBox CB_HP_Type;
        private System.Windows.Forms.CheckBox CHK_Enable_Filtering;
        private System.Windows.Forms.CheckBox CHK_Trickroom;
        private System.Windows.Forms.ComboBox CB_No_IVs;
        private System.Windows.Forms.Label L_No_IVs;
        private System.Windows.Forms.CheckedListBox CHKL_Missing_IVs;
        private System.Windows.Forms.CheckBox CHK_Special_Attacker;
        private System.Windows.Forms.RadioButton RAD_IVs;
        private System.Windows.Forms.RadioButton RAD_IVs_Miss;
        private System.Windows.Forms.TextBox TB_SVs;
        private System.Windows.Forms.CheckBox CHK_Hatches_Shiny_For;
        private System.Windows.Forms.CheckBox CHK_Hatches_Shiny_For_Me;
        private System.Windows.Forms.CheckBox CHK_Is_Shiny;

    }
}

