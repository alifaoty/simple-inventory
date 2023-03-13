using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_XIIRPL1_16_Alifa_Oty_Salsabilla
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    //Fill data to datatable
                    this.zeamartTableAdapter.Fill(this.appData.Zeamart);
                    zeamartBindingSource.DataSource = this.appData.Zeamart;
                    //dataGridView.DataSource = zeamartBindingSource;
                }
                else
                {
                    //using linq to query data
                    var query = from o in this.appData.Zeamart
                                where o.ItemName == txtSearch.Text || o.ItemCategory == txtSearch.Text || o.ItemPrice == txtSearch.Text || o.ItemStock == txtSearch.Text
                                select o;
                    zeamartBindingSource.DataSource = query.ToList();
                    //dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure want to delete this record ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    zeamartBindingSource.RemoveCurrent();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        pictureBox.Image = Image.FromFile(ofd.FileName);
                    //Load image from file to picturebox
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtName.Focus();
                //Add new row
                this.appData.Zeamart.AddZeamartRow(this.appData.Zeamart.NewZeamartRow());
                zeamartBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                zeamartBindingSource.ResetBindings(false);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            zeamartBindingSource.ResetBindings(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                zeamartBindingSource.EndEdit();
                zeamartTableAdapter.Update(this.appData.Zeamart);
                panel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                zeamartBindingSource.ResetBindings(false);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'appData.Zeamart' table. You can move, or remove it, as needed.
            this.zeamartTableAdapter.Fill(this.appData.Zeamart);
            zeamartBindingSource.DataSource = this.appData.Zeamart;

        }
    }
}
