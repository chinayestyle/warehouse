﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseBL.ProductCategoryManagement;

namespace WarehouseClient.ProductCategoryManagement
{
    public partial class AddProductCategory : Form
    {
        Form mainForm;


        public AddProductCategory(Form form)
        {
            mainForm = form;
            mainForm.Enabled = false;
            InitializeComponent();


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void AddProductCategory_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm.Enabled = true;
        }

        private void addProductCategoryButton_Click(object sender, EventArgs e)
        {
            WarehouseDAL.DataContracts.ProductCategory product = new WarehouseDAL.DataContracts.ProductCategory();
            product.Id = -1;
            product.Name = PCTextBox1.Text.ToString();
            product.ParentId = (int)PCNumericUpDown1.Value;
            ProductCategoryManager manager = new ProductCategoryManager();
            manager.CreateOrUpdateProductCategory(product);

            mainForm.Enabled = true;
            Close();
        }
    }
}
