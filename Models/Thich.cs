using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangaStore.Models
{
    public class Thich
    {
            DataMangaDataContext db = new DataMangaDataContext();
            public int iMaTruyen { set; get; }
            public string sTenTruyen { set; get; }
            public string sAnhbia { set; get; }
            public string sGiaban { set; get; }
            public int iSoluong { set; get; }
            public Thich(int MaTruyen)
            {
                iMaTruyen = MaTruyen;
                TRUYEN truyen = db.TRUYENs.Single(n => n.MaTruyen == iMaTruyen);
                sTenTruyen = truyen.TenTruyen;
                sAnhbia = truyen.Anhbia;
                sGiaban = truyen.Giaban;
                iSoluong = 1;
            }
        }
    }