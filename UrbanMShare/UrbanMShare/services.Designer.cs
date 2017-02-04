namespace UrbanMShare
{
    partial class services
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbbrand = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbcolour = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbfueltype = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cblocalization = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbavailability = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cborder = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.urbanMShareDataSet = new UrbanMShare.UrbanMShareDataSet();
            this.baseDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this._01BaseDataTableAdapter = new UrbanMShare.UrbanMShareDataSetTableAdapters._01BaseDataTableAdapter();
            this.Clean = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.urbanMShareDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseDataBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // cbbrand
            // 
            this.cbbrand.FormattingEnabled = true;
            this.cbbrand.Location = new System.Drawing.Point(77, 20);
            this.cbbrand.Name = "cbbrand";
            this.cbbrand.Size = new System.Drawing.Size(107, 21);
            this.cbbrand.TabIndex = 0;
            this.cbbrand.Text = "Todas";
            this.cbbrand.DropDownClosed += new System.EventHandler(this.cbbrand_DropDownClosed);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label1.Location = new System.Drawing.Point(74, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Marca";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label2.Location = new System.Drawing.Point(187, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cor";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbcolour
            // 
            this.cbcolour.FormattingEnabled = true;
            this.cbcolour.Location = new System.Drawing.Point(190, 20);
            this.cbcolour.Name = "cbcolour";
            this.cbcolour.Size = new System.Drawing.Size(84, 21);
            this.cbcolour.TabIndex = 2;
            this.cbcolour.Text = "Todas";
            this.cbcolour.DropDownClosed += new System.EventHandler(this.cbcolour_DropDownClosed);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label3.Location = new System.Drawing.Point(277, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Combustivel";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbfueltype
            // 
            this.cbfueltype.FormattingEnabled = true;
            this.cbfueltype.Location = new System.Drawing.Point(280, 20);
            this.cbfueltype.Name = "cbfueltype";
            this.cbfueltype.Size = new System.Drawing.Size(86, 21);
            this.cbfueltype.TabIndex = 4;
            this.cbfueltype.Text = "Todos";
            this.cbfueltype.DropDownClosed += new System.EventHandler(this.cbfueltype_DropDownClosed);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label4.Location = new System.Drawing.Point(369, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Localização";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cblocalization
            // 
            this.cblocalization.FormattingEnabled = true;
            this.cblocalization.Location = new System.Drawing.Point(372, 20);
            this.cblocalization.Name = "cblocalization";
            this.cblocalization.Size = new System.Drawing.Size(166, 21);
            this.cblocalization.TabIndex = 6;
            this.cblocalization.Text = "Todas";
            this.cblocalization.DropDownClosed += new System.EventHandler(this.cblocalization_DropDownClosed);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label5.Location = new System.Drawing.Point(541, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Disponivel";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbavailability
            // 
            this.cbavailability.FormattingEnabled = true;
            this.cbavailability.Items.AddRange(new object[] {
            "Sim",
            "Não"});
            this.cbavailability.Location = new System.Drawing.Point(544, 20);
            this.cbavailability.Name = "cbavailability";
            this.cbavailability.Size = new System.Drawing.Size(100, 21);
            this.cbavailability.TabIndex = 8;
            this.cbavailability.Text = "Todos";
            this.cbavailability.DropDownClosed += new System.EventHandler(this.cbavailability_DropDownClosed);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.label6.Location = new System.Drawing.Point(647, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Ordenar Por:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cborder
            // 
            this.cborder.FormattingEnabled = true;
            this.cborder.Items.AddRange(new object[] {
            "Marca",
            "Cor",
            "Combustivel",
            "Localização",
            "Preço",
            "Por Importância"});
            this.cborder.Location = new System.Drawing.Point(650, 20);
            this.cborder.Name = "cborder";
            this.cborder.Size = new System.Drawing.Size(121, 21);
            this.cborder.TabIndex = 10;
            this.cborder.Text = "Marca";
            this.cborder.DropDownClosed += new System.EventHandler(this.cborder_DropDownClosed);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(5, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(765, 2);
            this.label8.TabIndex = 13;
            // 
            // urbanMShareDataSet
            // 
            this.urbanMShareDataSet.DataSetName = "UrbanMShareDataSet";
            this.urbanMShareDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // baseDataBindingSource
            // 
            this.baseDataBindingSource.DataMember = "01BaseData";
            this.baseDataBindingSource.DataSource = this.urbanMShareDataSet;
            // 
            // _01BaseDataTableAdapter
            // 
            this._01BaseDataTableAdapter.ClearBeforeFill = true;
            // 
            // Clean
            // 
            this.Clean.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(84)))), ((int)(((byte)(103)))));
            this.Clean.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Clean.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.Clean.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.Clean.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Clean.ForeColor = System.Drawing.SystemColors.Window;
            this.Clean.Location = new System.Drawing.Point(5, 18);
            this.Clean.Name = "Clean";
            this.Clean.Size = new System.Drawing.Size(66, 23);
            this.Clean.TabIndex = 14;
            this.Clean.Text = "Limpar";
            this.Clean.UseVisualStyleBackColor = false;
            this.Clean.Click += new System.EventHandler(this.Clean_Click);
            // 
            // services
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.Clean);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cborder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbavailability);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cblocalization);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbfueltype);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbcolour);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbrand);
            this.Name = "services";
            this.Size = new System.Drawing.Size(780, 329);
            this.Load += new System.EventHandler(this.services_Load);
            ((System.ComponentModel.ISupportInitialize)(this.urbanMShareDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.baseDataBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbrand;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbcolour;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbfueltype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cblocalization;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbavailability;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cborder;
        private System.Windows.Forms.Label label8;
        private UrbanMShareDataSet urbanMShareDataSet;
        private System.Windows.Forms.BindingSource baseDataBindingSource;
        private UrbanMShareDataSetTableAdapters._01BaseDataTableAdapter _01BaseDataTableAdapter;
        private System.Windows.Forms.Button Clean;
    }
}
