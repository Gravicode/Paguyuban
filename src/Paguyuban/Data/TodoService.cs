using Microsoft.EntityFrameworkCore;
using Paguyuban.Data;
using Paguyuban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paguyuban.Data
{
    public class TodoService : ICrud<Todo>
    {
        PaguyubanDB db;

        public TodoService()
        {
            if (db == null) db = new PaguyubanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.Todos.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.Todos.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<Todo> FindByKeyword(string Keyword)
        {
            var data = from x in db.Todos
                       where x.Title.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<Todo> GetAllData()
        {
            return db.Todos.OrderBy(x => x.Id).ToList();
        }

        public Todo GetDataById(object Id)
        {
            return db.Todos.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(Todo data)
        {
            try
            {
                db.Todos.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(Todo data)
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
            return db.Todos.Max(x => x.Id);
        }
    }

}
/*











 */
