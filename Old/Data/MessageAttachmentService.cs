using Microsoft.EntityFrameworkCore;
using Paguyuban.Data;
using Paguyuban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paguyuban.Data
{
    public class MessageAttachmentService : ICrud<MessageAttachment>
    {
        PaguyubanDB db;

        public MessageAttachmentService()
        {
            if (db == null) db = new PaguyubanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.MessageAttachments.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.MessageAttachments.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<MessageAttachment> FindByKeyword(string Keyword)
        {
            var data = from x in db.MessageAttachments
                       where x.Title.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<MessageAttachment> GetAllData()
        {
            return db.MessageAttachments.OrderBy(x => x.Id).ToList();
        }

        public MessageAttachment GetDataById(object Id)
        {
            return db.MessageAttachments.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(MessageAttachment data)
        {
            try
            {
                db.MessageAttachments.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(MessageAttachment data)
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
            return db.MessageAttachments.Max(x => x.Id);
        }
    }

}
/*











 */
