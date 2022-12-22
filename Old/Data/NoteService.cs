using Microsoft.EntityFrameworkCore;
using Paguyuban.Data;
using Paguyuban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paguyuban.Data
{
    public class NoteService : ICrud<Note>
    {
        PaguyubanDB db;

        public NoteService()
        {
            if (db == null) db = new PaguyubanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Notes.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Notes.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Note> FindByKeyword(string Keyword)
        {
            var data = from x in db.Notes
                       where x.Title.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Note> GetAllData()
        {
            return db.Notes.OrderBy(x => x.Id).ToList();
        }

        public Note GetDataById(object Id)
        {
            return db.Notes.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Note data)
        {
            try
            {
                db.Notes.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Note data)
        {
            try
            {
                db.Entry(data).State = EntityState.Modified;
                db.SaveChanges();

                /*
                if (sel != null)
                {
                    sel.Nama = data.Nama;
                    sel.Keterangan = data.Keterangan;
                    sel.Tanggal = data.Tanggal;
                    sel.DocumentUrl = data.DocumentUrl;
                    sel.StreamUrl = data.StreamUrl;
                    return true;

                }*/
                return true;
            }
            catch
            {

            }
            return false;
        }

        public long GetLastId()
        {
            return db.Notes.Max(x => x.Id);
        }
    }

}
/*











 */
