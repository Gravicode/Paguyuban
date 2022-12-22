using Microsoft.EntityFrameworkCore;
using Paguyuban.Data;
using Paguyuban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paguyuban.Data
{
    public class GroupMessageService : ICrud<GroupMessage>
    {
        PaguyubanDB db;

        public GroupMessageService()
        {
            if (db == null) db = new PaguyubanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.GroupMessages.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.GroupMessages.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<GroupMessage> FindByKeyword(string Keyword)
        {
            var data = from x in db.GroupMessages
                       where x.Name.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<GroupMessage> GetAllData()
        {
            return db.GroupMessages.OrderBy(x => x.Id).ToList();
        }

        public GroupMessage GetDataById(object Id)
        {
            return db.GroupMessages.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(GroupMessage data)
        {
            try
            {
                db.GroupMessages.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(GroupMessage data)
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
            return db.GroupMessages.Max(x => x.Id);
        }
    }

}
/*











 */
