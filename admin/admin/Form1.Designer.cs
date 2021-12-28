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
            this.confirm = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.TextBox();
            this.productDescription = new System.Windows.Forms.TextBox();
            this.productPrice = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Users = new System.Windows.Forms.ListBox();
            this.Products = new System.Windows.Forms.ListBox();
            this.Categories = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.productCategorie = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(136, 597);
            this.confirm.Margin = new System.Windows.Forms.Padding(4);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(200, 60);
            this.confirm.TabIndex = 0;
            this.confirm.Text = "Ajouter";
            this.confirm.UseVisualStyleBackColor = true;
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(136, 232);
            this.name.Margin = new System.Windows.Forms.Padding(4);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(472, 31);
            this.name.TabIndex = 1;
            this.name.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // productDescription
            // 
            this.productDescription.Location = new System.Drawing.Point(137, 424);
            this.productDescription.Margin = new System.Windows.Forms.Padding(4);
            this.productDescription.Name = "productDescription";
            this.productDescription.Size = new System.Drawing.Size(472, 31);
            this.productDescription.TabIndex = 4;
            this.productDescription.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // productPrice
            // 
            this.productPrice.Location = new System.Drawing.Point(136, 330);
            this.productPrice.Margin = new System.Windows.Forms.Padding(4);
            this.productPrice.Name = "productPrice";
            this.productPrice.Size = new System.Drawing.Size(472, 31);
            this.productPrice.TabIndex = 5;
            this.productPrice.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 133);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ajouter un produit";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(852, 133);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(944, 729);
            this.dataGridView1.TabIndex = 7;
            // 
            // Users
            // 
            this.Users.FormattingEnabled = true;
            this.Users.ItemHeight = 25;
            this.Users.Location = new System.Drawing.Point(908, 206);
            this.Users.Margin = new System.Windows.Forms.Padding(4);
            this.Users.Name = "Users";
            this.Users.Size = new System.Drawing.Size(208, 604);
            this.Users.TabIndex = 8;
            this.Users.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // Products
            // 
            this.Products.FormattingEnabled = true;
            this.Products.ItemHeight = 25;
            this.Products.Location = new System.Drawing.Point(1220, 206);
            this.Products.Margin = new System.Windows.Forms.Padding(4);
            this.Products.Name = "Products";
            this.Products.Size = new System.Drawing.Size(208, 604);
            this.Products.TabIndex = 9;
            this.Products.SelectedIndexChanged += new System.EventHandler(this.Products_SelectedIndexChanged);
            // 
            // Categories
            // 
            this.Categories.FormattingEnabled = true;
            this.Categories.ItemHeight = 25;
            this.Categories.Location = new System.Drawing.Point(1532, 206);
            this.Categories.Margin = new System.Windows.Forms.Padding(4);
            this.Categories.Name = "Categories";
            this.Categories.Size = new System.Drawing.Size(208, 604);
            this.Categories.TabIndex = 10;
            this.Categories.SelectedIndexChanged += new System.EventHandler(this.listBox2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nom";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "Prix";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 383);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Description";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(132, 489);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "Catégorie";
            // 
            // productCategorie
            // 
            this.productCategorie.Location = new System.Drawing.Point(136, 528);
            this.productCategorie.Margin = new System.Windows.Forms.Padding(4);
            this.productCategorie.Name = "productCategorie";
            this.productCategorie.Size = new System.Drawing.Size(472, 31);
            this.productCategorie.TabIndex = 15;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2008, 939);
            this.Controls.Add(this.productCategorie);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Categories);
            this.Controls.Add(this.Products);
            this.Controls.Add(this.Users);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.productPrice);
            this.Controls.Add(this.productDescription);
            this.Controls.Add(this.name);
            this.Controls.Add(this.confirm);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Dashboard";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button confirm;
        public System.Windows.Forms.TextBox name;
        public System.Windows.Forms.TextBox productDescription;
        public System.Windows.Forms.TextBox productPrice;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.ListBox Users;
        public System.Windows.Forms.ListBox Products;
        public System.Windows.Forms.ListBox Categories;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox productCategorie;
    }
}