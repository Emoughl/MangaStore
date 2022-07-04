using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangaStore.Models
{
    public class Giohang
    {
        DataMangaDataContext db = new DataMangaDataContext();
        public int iMaTruyen{ set; get; }
        public string sTenTruyen { set; get; }
        public string sAnhbia { set; get; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dDongia; }

        }
        public Giohang(int MaTruyen)
        {
            iMaTruyen = MaTruyen;
            TRUYEN truyen = db.TRUYENs.Single(n => n.MaTruyen == iMaTruyen);
            sTenTruyen = truyen.TenTruyen;
            sAnhbia = truyen.Anhbia;
            dDongia = double.Parse(truyen.Giaban.ToString());
            iSoluong = 1;
        }
    }
}