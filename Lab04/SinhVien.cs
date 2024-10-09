using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04
{
    internal class SinhVien
    {
        public string MSSV { get; set; }
        public string HoVaTen { get; set; }
        public string Email {  get; set; }
        public string DiaChi {  get; set; }
        public string Hinh { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool Phai { get; set; }
        public string Lop {get; set; }
        public string SDT {  get; set; }

        //tao lap mac dinh
        public SinhVien()
        { }

        public SinhVien(string mssv, string hovaten, string email, string diachi, string hinh, DateTime ngaysinh, bool phai, string lop, string sdt)
        {
            MSSV = mssv;
            HoVaTen = hovaten;
            Email = email;
            DiaChi = diachi;
            Hinh = hinh;
            NgaySinh = ngaysinh;
            Phai = phai;
            Lop = lop;
            SDT = sdt;

        }


    }
}
