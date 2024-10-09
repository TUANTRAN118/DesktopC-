using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04
{
    internal class DanhSachSinhVien
    {
        public List<SinhVien> DanhSach;
        // delegate SoSanh
        public delegate int SoSanh(object sv1, object sv2);

        public DanhSachSinhVien()
        {
            DanhSach = new List<SinhVien>();
        }

        public void Them(SinhVien sv)
        {
            this.DanhSach.Add(sv);
        }

        public SinhVien this [int index]
        {
            get { return DanhSach[index]; }
            set { DanhSach[index] = value; }
        }

        //xoa
        public void Xoa(object obj, SoSanh ss)
        {
            int i = DanhSach.Count - 1;
            for (; i >= 0; i--)
                if (ss(obj, this[i]) == 0)
                    this.DanhSach.RemoveAt(i);
        }

        //tim
        public SinhVien Tim(object obj, SoSanh ss)
        {
            SinhVien svresult = null;
            foreach(SinhVien sv in DanhSach)
                if(ss(obj, sv) == 0)
                {
                    svresult = sv;
                    break;
                }
            return svresult;

        }

        //sua
        public bool Sua(SinhVien svsua, object obj, SoSanh ss)
        {
            int i, count;
            bool kq = false;
            count = this.DanhSach.Count - 1;
            for (i = 0; i < count; i++)
                if(ss(obj, this[i]) == 0)
                {
                    this[i] = svsua;
                    kq = true;
                    break;
                }
            return kq;
        }



        // doc data tu txt
        public void DocTuFile()
        {
            string filename = "D:\\desktop\\Lab04\\Lab04\\data.txt", t;
            string[] s;
            SinhVien sv;
            StreamReader sr = new StreamReader(new FileStream(filename, FileMode.Open));
            while((t = sr.ReadLine()) != null)
            {
                s = t.Split('*');
                sv = new SinhVien();
                sv.MSSV = s[0];
                sv.HoVaTen = s[1];
                sv.Phai = false;
                if (s[2] == "1")
                    sv.Phai = true;
                sv.NgaySinh = DateTime.Parse(s[3]);
                sv.Lop = s[4];
                sv.SDT = s[5];
                sv.Email = s[6];
                sv.DiaChi= s[7];
                sv.Hinh = s[8];
                this.Them(sv);
            }
        }




    }
}
