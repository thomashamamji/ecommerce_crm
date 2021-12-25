namespace admin
{
    partial class Dashboard
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
            this.Ajouter = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.productDescription = new System.Windows.Forms.TextBox();
            this.productPrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Users = new System.Windows.Forms.ListBox();
            this.Products = new System.Windows.Forms.ListBox();
            this.Categories = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Ajouter
            // 
            this.Ajouter.Location = new System.Drawing.Point(68, 253);
            this.Ajouter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Ajouter.Name = "Ajouter";
            this.Ajouter.Size = new System.Drawing.Size(100, 31);
            this.Ajouter.TabIndex = 0;
            this.Ajouter.Text = "Ajouter";
            this.Ajouter.UseVisualStyleBackColor = true;
            this.Ajouter.Click += new System.EventHandler(this.button1_Click);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(68, 107);
            this.name.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(238, 20);
            this.name.TabIndex = 1;
            this.name.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(68, 144);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(238, 20);
            this.textBox3.TabIndex = 3;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // productDescription
            // 
            this.productDescription.Location = new System.Drawing.Point(68, 209);
            this.productDescription.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.productDescription.Name = "productDescription";
            this.productDescription.Size = new System.Drawing.Size(238, 20);
            this.productDescription.TabIndex = 4;
            this.productDescription.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // productPrice
            // 
            this.productPrice.Location = new System.Drawing.Point(68, 178);
            this.productPrice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.productPrice.Name = "productPrice";
            this.productPrice.Size = new System.Drawing.Size(238, 20);
            this.productPrice.TabIndex = 5;
            this.productPrice.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 69);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ajouter un produit";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(426, 69);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(472, 379);
            this.dataGridView1.TabIndex = 7;
            // 
            // Users
            // 
            this.Users.FormattingEnabled = true;
            this.Users.Location = new System.Drawing.Point(454, 107);
            this.Users.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Users.Name = "Users";
            this.Users.Size = new System.Drawing.Size(106, 316);
            this.Users.TabIndex = 8;
            this.Users.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Products
            // 
            this.Products.FormattingEnabled = true;
            this.Products.Location = new System.Drawing.Point(610, 107);
            this.Products.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Products.Name = "Products";
            this.Products.Size = new System.Drawing.Size(106, 316);
            this.Products.TabIndex = 9;
            this.Products.SelectedIndexChanged += new System.EventHandler(this.Products_SelectedIndexChanged);
            // 
            // Categories
            // 
            this.Categories.FormattingEnabled = true;
            this.Categories.Location = new System.Drawing.Point(766, 107);
            this.Categories.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Categories.Name = "Categories";
            this.Categories.Size = new System.Drawing.Size(106, 316);
            this.Categories.TabIndex = 10;
            this.Categories.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 486);
            this.Controls.Add(this.Categories);
            this.Controls.Add(this.Products);
            this.Controls.Add(this.Users);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.productPrice);
            this.Controls.Add(this.productDescription);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.name);
            this.Controls.Add(this.Ajouter);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Dashboard";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button Ajouter;
        public System.Windows.Forms.TextBox name;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.TextBox productDescription;
        public System.Windows.Forms.TextBox productPrice;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.ListBox Users;
        public System.Windows.Forms.ListBox Products;
        public System.Windows.Forms.ListBox Categories;
    }
}