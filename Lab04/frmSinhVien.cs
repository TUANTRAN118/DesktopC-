using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab04
{
    public partial class frmSinhVien : Form
    {
        private bool isChanged = false; // theo doi thay doi


        DanhSachSinhVien dssv;
        public frmSinhVien()
        {
            InitializeComponent();
        }

        // lay thong tin tu control sv
        private SinhVien GetSinhVien()
        {
            SinhVien sv = new SinhVien();
            bool phai = true;
            sv.MSSV = this.txtMSSV.Text;
            sv.HoVaTen = this.txtHovaTen.Text;
            if(rbNu.Checked)
                phai = false;
            sv.Phai = phai;
            sv.NgaySinh = this.dtpNgaySinh.Value;
            sv.Lop = this.cboLop.Text;
            sv.SDT = this.mtxtSoDienThoai.Text;
            sv.Email = this.txtEmail.Text;
            sv.DiaChi = this.txtDiaChi.Text;
            sv.Hinh = this.txtHinh.Text;

            return sv;
        }

        // lay thong tin sv tu item của lv
        private SinhVien GetSinhVienLV(ListViewItem lvitem)
        {
            SinhVien sv = new SinhVien();
            sv.MSSV = lvitem.SubItems[0].Text;
            sv.HoVaTen = lvitem.SubItems[1].Text;
            sv.Phai = lvitem.SubItems[2].Text == "Nam";
            sv.NgaySinh = DateTime.Parse(lvitem.SubItems[3].Text);
            sv.Lop = lvitem.SubItems[4].Text;
            sv.SDT = lvitem.SubItems[5].Text;
            sv.Email = lvitem.SubItems[6].Text;
            sv.DiaChi = lvitem.SubItems[7].Text;
            sv.Hinh = lvitem.SubItems[8].Text;

            return sv;
        }

        // thiet lap thong tin len control
        private void ThietLapthongTin(SinhVien sv)
        {
            this.txtMSSV.Text = sv.MSSV;
            this.txtHovaTen.Text = sv.HoVaTen;
            if (sv.Phai)
                this.rbNam.Checked = true;
            else 
                this.rbNu.Checked = false;
            this.dtpNgaySinh.Value = sv.NgaySinh;
            this.cboLop.Text = sv.Lop;
            this.mtxtSoDienThoai.Text = sv.SDT;
            this.txtEmail.Text = sv.Email;
            this.txtDiaChi.Text = sv.DiaChi;
            this.txtHinh.Text = sv.Hinh;
        }

        // them sinh vien vao list View
        private void ThemSV(SinhVien sv)
        {
            ListViewItem lvitem = new ListViewItem(sv.MSSV);
            lvitem.SubItems.Add(sv.HoVaTen);
            string phai = sv.Phai ? "Nam" : "Nữ";
            lvitem.SubItems.Add(phai);
            lvitem.SubItems.Add(sv.NgaySinh.ToShortDateString());
            lvitem.SubItems.Add(sv.Lop);
            lvitem.SubItems.Add(sv.SDT);
            lvitem.SubItems.Add(sv.Email);
            lvitem.SubItems.Add(sv.DiaChi);
            lvitem.SubItems.Add(sv.Hinh);
            this.lvSinhvien.Items.Add(lvitem);
        }

        // hien thi các sv trong dssv lên listView
        private void LoadListView()
        {
            this.lvSinhvien.Items.Clear();
            foreach(SinhVien sv in dssv.DanhSach)
            {
                ThemSV(sv);
            }
        }

        // gan thong tin len control
        private void lvSinhvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = this.lvSinhvien.SelectedItems.Count;
            if(count > 0)
            {
                ListViewItem lvitem = this.lvSinhvien.SelectedItems[0];
                SinhVien sv = GetSinhVienLV(lvitem);
                ThietLapthongTin(sv);
            }    
        }

        // them sinh vien
        private void btnLuu_Click(object sender, EventArgs e)
        {
            SinhVien sv = GetSinhVien();
            SinhVien kq = dssv.Tim(sv.MSSV, delegate (object obj1, object obj2)
            {
                return (obj2 as SinhVien).MSSV.CompareTo(obj1.ToString());
            });
            if (kq != null)
                MessageBox.Show("Mã sinh viên đã tồn tại!", "Lỗi thêm dữ liệu",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                this.dssv.Them(sv);
                this.LoadListView();
                isChanged = true; // danh dau thay doi
            }

        }




        //------------------------------------------------
        //su kien load form
        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            dssv = new DanhSachSinhVien();
            dssv.DocTuFile();
            LoadListView();
        }






        // chi cho nhap so
        private void txtMSSV_KeyPress(object sender, KeyPressEventArgs e)
        {
               if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
               {
                   e.Handled = true;
               }   
        }

        private void btnChonHinh_Click(object sender, EventArgs e)
        {
            // tạo OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";
            openFileDialog.Title = "Chon file hình ảnh";

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
          
                string filePath = openFileDialog.FileName; // lay duong dan anh
                this.txtHinh.Text = filePath;
                pbUpload.Image = System.Drawing.Image.FromFile(filePath); // tai anh len pb
                pbUpload.SizeMode = PictureBoxSizeMode.StretchImage; // fix anh
                
            }    

        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            this.txtMSSV.Text = "";
            this.txtHovaTen.Text = "";
            this.txtEmail.Text = "";
            this.txtDiaChi.Text = "";
            this.dtpNgaySinh.Value = DateTime.Now;
            this.rbNam.Checked = true;
            this.cboLop.SelectedIndex = 0;
            this.mtxtSoDienThoai.Text = "";
            this.txtHinh.Text = "";
            pbUpload.Image = null;


        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }


        private void xóaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra xem có mục nào được chọn không
            if (lvSinhvien.SelectedItems.Count > 0)
            {
                // Xóa tất cả các mục đã chọn
                foreach (ListViewItem item in lvSinhvien.SelectedItems)
                {
                    lvSinhvien.Items.Remove(item);
                }
                isChanged = true; // danh dau thay doi
            }

            else
            {
                MessageBox.Show("Vui lòng chọn một mục để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tảiLạiDanhSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dssv.DocTuFile(); 
            LoadListView();
        }





        //----------------------------------------------------







    }
}
