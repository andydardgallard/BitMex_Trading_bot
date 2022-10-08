namespace PC3
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
            this.components = new System.ComponentModel.Container();
            this.ddlSymbols = new System.Windows.Forms.ComboBox();
            this.ddlStratagy = new System.Windows.Forms.ComboBox();
            this.ddlTF = new System.Windows.Forms.ComboBox();
            this.lblAverPricePeriod = new System.Windows.Forms.Label();
            this.nudAverPricePeriod = new System.Windows.Forms.NumericUpDown();
            this.lblPriceChennel = new System.Windows.Forms.Label();
            this.nudPriceChannel = new System.Windows.Forms.NumericUpDown();
            this.lblPrctEquityForSystem = new System.Windows.Forms.Label();
            this.nudPrctEqForSystem = new System.Windows.Forms.NumericUpDown();
            this.lblMpR = new System.Windows.Forms.Label();
            this.nudMpR = new System.Windows.Forms.NumericUpDown();
            this.lblMaxKontract = new System.Windows.Forms.Label();
            this.nudMaxContract = new System.Windows.Forms.NumericUpDown();
            this.btnUpdateCandels = new System.Windows.Forms.Button();
            this.dvgCandles = new System.Windows.Forms.DataGridView();
            this.btnStart = new System.Windows.Forms.Button();
            this.tmrAutoTrade = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.txbKontracts = new System.Windows.Forms.TextBox();
            this.lblKontracts = new System.Windows.Forms.Label();
            this.nudWidthCh = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudAverPricePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriceChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrctEqForSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxContract)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgCandles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidthCh)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlSymbols
            // 
            this.ddlSymbols.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlSymbols.FormattingEnabled = true;
            this.ddlSymbols.Location = new System.Drawing.Point(12, 34);
            this.ddlSymbols.Name = "ddlSymbols";
            this.ddlSymbols.Size = new System.Drawing.Size(143, 21);
            this.ddlSymbols.TabIndex = 0;
            this.ddlSymbols.SelectedIndexChanged += new System.EventHandler(this.ddlSymbols_SelectedIndexChanged);
            // 
            // ddlStratagy
            // 
            this.ddlStratagy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlStratagy.FormattingEnabled = true;
            this.ddlStratagy.Items.AddRange(new object[] {
            "PC",
            "PCS",
            "PCc",
            "PCcS",
            "PCrcS",
            "PCBBС",
            "SM",
            "SMc",
            "SMBBC"});
            this.ddlStratagy.Location = new System.Drawing.Point(12, 7);
            this.ddlStratagy.Name = "ddlStratagy";
            this.ddlStratagy.Size = new System.Drawing.Size(143, 21);
            this.ddlStratagy.TabIndex = 1;
            this.ddlStratagy.SelectedIndexChanged += new System.EventHandler(this.ddlStratagy_SelectedIndexChanged);
            // 
            // ddlTF
            // 
            this.ddlTF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlTF.FormattingEnabled = true;
            this.ddlTF.Items.AddRange(new object[] {
            "1m",
            "5m",
            "1h",
            "1d"});
            this.ddlTF.Location = new System.Drawing.Point(12, 61);
            this.ddlTF.Name = "ddlTF";
            this.ddlTF.Size = new System.Drawing.Size(143, 21);
            this.ddlTF.TabIndex = 2;
            this.ddlTF.SelectedIndexChanged += new System.EventHandler(this.ddlTF_SelectedIndexChanged);
            // 
            // lblAverPricePeriod
            // 
            this.lblAverPricePeriod.AutoSize = true;
            this.lblAverPricePeriod.Location = new System.Drawing.Point(12, 94);
            this.lblAverPricePeriod.Name = "lblAverPricePeriod";
            this.lblAverPricePeriod.Size = new System.Drawing.Size(83, 13);
            this.lblAverPricePeriod.TabIndex = 15;
            this.lblAverPricePeriod.Text = "AverPricePeriod";
            // 
            // nudAverPricePeriod
            // 
            this.nudAverPricePeriod.Location = new System.Drawing.Point(101, 92);
            this.nudAverPricePeriod.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudAverPricePeriod.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudAverPricePeriod.Name = "nudAverPricePeriod";
            this.nudAverPricePeriod.Size = new System.Drawing.Size(54, 20);
            this.nudAverPricePeriod.TabIndex = 16;
            this.nudAverPricePeriod.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudAverPricePeriod.ValueChanged += new System.EventHandler(this.nudAverPricePeriod_ValueChanged);
            // 
            // lblPriceChennel
            // 
            this.lblPriceChennel.AutoSize = true;
            this.lblPriceChennel.Location = new System.Drawing.Point(32, 118);
            this.lblPriceChennel.Name = "lblPriceChennel";
            this.lblPriceChennel.Size = new System.Drawing.Size(24, 13);
            this.lblPriceChennel.TabIndex = 17;
            this.lblPriceChennel.Text = "PC ";
            this.lblPriceChennel.Click += new System.EventHandler(this.lblPriceChennel_Click);
            // 
            // nudPriceChannel
            // 
            this.nudPriceChannel.Location = new System.Drawing.Point(101, 118);
            this.nudPriceChannel.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudPriceChannel.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudPriceChannel.Name = "nudPriceChannel";
            this.nudPriceChannel.Size = new System.Drawing.Size(54, 20);
            this.nudPriceChannel.TabIndex = 18;
            this.nudPriceChannel.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudPriceChannel.ValueChanged += new System.EventHandler(this.nudPriceChannel_ValueChanged);
            // 
            // lblPrctEquityForSystem
            // 
            this.lblPrctEquityForSystem.AutoSize = true;
            this.lblPrctEquityForSystem.Location = new System.Drawing.Point(12, 146);
            this.lblPrctEquityForSystem.Name = "lblPrctEquityForSystem";
            this.lblPrctEquityForSystem.Size = new System.Drawing.Size(71, 13);
            this.lblPrctEquityForSystem.TabIndex = 19;
            this.lblPrctEquityForSystem.Text = "PrctEqForSys";
            // 
            // nudPrctEqForSystem
            // 
            this.nudPrctEqForSystem.Cursor = System.Windows.Forms.Cursors.Default;
            this.nudPrctEqForSystem.DecimalPlaces = 2;
            this.nudPrctEqForSystem.Location = new System.Drawing.Point(101, 144);
            this.nudPrctEqForSystem.Name = "nudPrctEqForSystem";
            this.nudPrctEqForSystem.Size = new System.Drawing.Size(54, 20);
            this.nudPrctEqForSystem.TabIndex = 20;
            this.nudPrctEqForSystem.Value = new decimal(new int[] {
            5120,
            0,
            0,
            131072});
            // 
            // lblMpR
            // 
            this.lblMpR.AutoSize = true;
            this.lblMpR.Location = new System.Drawing.Point(32, 172);
            this.lblMpR.Name = "lblMpR";
            this.lblMpR.Size = new System.Drawing.Size(30, 13);
            this.lblMpR.TabIndex = 21;
            this.lblMpR.Text = "MpR";
            // 
            // nudMpR
            // 
            this.nudMpR.Cursor = System.Windows.Forms.Cursors.Default;
            this.nudMpR.DecimalPlaces = 2;
            this.nudMpR.Location = new System.Drawing.Point(101, 170);
            this.nudMpR.Name = "nudMpR";
            this.nudMpR.Size = new System.Drawing.Size(54, 20);
            this.nudMpR.TabIndex = 22;
            this.nudMpR.Value = new decimal(new int[] {
            125,
            0,
            0,
            131072});
            // 
            // lblMaxKontract
            // 
            this.lblMaxKontract.AutoSize = true;
            this.lblMaxKontract.Location = new System.Drawing.Point(16, 198);
            this.lblMaxKontract.Name = "lblMaxKontract";
            this.lblMaxKontract.Size = new System.Drawing.Size(67, 13);
            this.lblMaxKontract.TabIndex = 23;
            this.lblMaxKontract.Text = "MaxContract";
            // 
            // nudMaxContract
            // 
            this.nudMaxContract.Location = new System.Drawing.Point(101, 196);
            this.nudMaxContract.Maximum = new decimal(new int[] {
            500000,
            0,
            0,
            0});
            this.nudMaxContract.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMaxContract.Name = "nudMaxContract";
            this.nudMaxContract.Size = new System.Drawing.Size(54, 20);
            this.nudMaxContract.TabIndex = 24;
            this.nudMaxContract.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // btnUpdateCandels
            // 
            this.btnUpdateCandels.Location = new System.Drawing.Point(12, 222);
            this.btnUpdateCandels.Name = "btnUpdateCandels";
            this.btnUpdateCandels.Size = new System.Drawing.Size(143, 23);
            this.btnUpdateCandels.TabIndex = 25;
            this.btnUpdateCandels.Text = "Update Candels";
            this.btnUpdateCandels.UseVisualStyleBackColor = true;
            this.btnUpdateCandels.Click += new System.EventHandler(this.btnUpdateCandels_Click);
            // 
            // dvgCandles
            // 
            this.dvgCandles.AllowUserToOrderColumns = true;
            this.dvgCandles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dvgCandles.Location = new System.Drawing.Point(169, 7);
            this.dvgCandles.Name = "dvgCandles";
            this.dvgCandles.Size = new System.Drawing.Size(827, 491);
            this.dvgCandles.TabIndex = 26;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 252);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(143, 23);
            this.btnStart.TabIndex = 27;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tmrAutoTrade
            // 
            this.tmrAutoTrade.Interval = 10000;
            this.tmrAutoTrade.Tick += new System.EventHandler(this.tmrAutoTrade_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 327);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(143, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txbKontracts
            // 
            this.txbKontracts.Location = new System.Drawing.Point(15, 383);
            this.txbKontracts.Name = "txbKontracts";
            this.txbKontracts.Size = new System.Drawing.Size(100, 20);
            this.txbKontracts.TabIndex = 29;
            this.txbKontracts.TextChanged += new System.EventHandler(this.txbKontracts_TextChanged);
            // 
            // lblKontracts
            // 
            this.lblKontracts.AutoSize = true;
            this.lblKontracts.Location = new System.Drawing.Point(15, 296);
            this.lblKontracts.Name = "lblKontracts";
            this.lblKontracts.Size = new System.Drawing.Size(35, 13);
            this.lblKontracts.TabIndex = 30;
            this.lblKontracts.Text = "label1";
            // 
            // nudWidthCh
            // 
            this.nudWidthCh.Location = new System.Drawing.Point(92, 289);
            this.nudWidthCh.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudWidthCh.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudWidthCh.Name = "nudWidthCh";
            this.nudWidthCh.Size = new System.Drawing.Size(54, 20);
            this.nudWidthCh.TabIndex = 31;
            this.nudWidthCh.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 510);
            this.Controls.Add(this.nudWidthCh);
            this.Controls.Add(this.lblKontracts);
            this.Controls.Add(this.txbKontracts);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.dvgCandles);
            this.Controls.Add(this.btnUpdateCandels);
            this.Controls.Add(this.nudMaxContract);
            this.Controls.Add(this.lblMaxKontract);
            this.Controls.Add(this.nudMpR);
            this.Controls.Add(this.lblMpR);
            this.Controls.Add(this.nudPrctEqForSystem);
            this.Controls.Add(this.lblPrctEquityForSystem);
            this.Controls.Add(this.nudPriceChannel);
            this.Controls.Add(this.lblPriceChennel);
            this.Controls.Add(this.nudAverPricePeriod);
            this.Controls.Add(this.lblAverPricePeriod);
            this.Controls.Add(this.ddlTF);
            this.Controls.Add(this.ddlStratagy);
            this.Controls.Add(this.ddlSymbols);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudAverPricePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPriceChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrctEqForSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMpR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxContract)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgCandles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidthCh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlSymbols;
        private System.Windows.Forms.ComboBox ddlStratagy;
        private System.Windows.Forms.ComboBox ddlTF;
        private System.Windows.Forms.Label lblAverPricePeriod;
        private System.Windows.Forms.NumericUpDown nudAverPricePeriod;
        private System.Windows.Forms.Label lblPriceChennel;
        private System.Windows.Forms.NumericUpDown nudPriceChannel;
        private System.Windows.Forms.Label lblPrctEquityForSystem;
        private System.Windows.Forms.NumericUpDown nudPrctEqForSystem;
        private System.Windows.Forms.Label lblMpR;
        private System.Windows.Forms.NumericUpDown nudMpR;
        private System.Windows.Forms.Label lblMaxKontract;
        private System.Windows.Forms.NumericUpDown nudMaxContract;
        private System.Windows.Forms.Button btnUpdateCandels;
        private System.Windows.Forms.DataGridView dvgCandles;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer tmrAutoTrade;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txbKontracts;
        private System.Windows.Forms.Label lblKontracts;
        private System.Windows.Forms.NumericUpDown nudWidthCh;

    }
}

