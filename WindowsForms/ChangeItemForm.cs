using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using HCSMS.Presentation;
using HCSMS.Presentation.Impl;
using HCSMS.Model;
using HCSMS.Model.Application;


namespace HCSMS.Presentation.WindowsForms
{
    public partial class ChangeItemForm : Form
    {
        TableUI ui ;
       
        public ChangeItemForm(Session session)
        {
            InitializeComponent();
            ui = new TableUI("001",session);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Item> itemList = new List<Item>(); // TODO: 初始化为适当的值
            Item old = new Item();
            old.Id = "003";
            Item ne = new Item();
            ne.Id = "002";
            itemList.Add(old);
            itemList.Add(ne);
            ui.OrderItem(itemList);
           
            
        }

        private void ChangeItemForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}
