using Microsoft.EntityFrameworkCore;
using Paguyuban.Data;
using Paguyuban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paguyuban.Data
{
    public class GroupMessageMemberService : ICrud<GroupMessageMember>
    {
        PaguyubanDB db;

        public GroupMessageMemberService()
        {
            if (db == null) db = new PaguyubanDB();

        }
        public bool DeleteData(object Id)
        {
            var selData = (db.GroupMessageMembers.Where(x => x.Id == (long)Id).FirstOrDefault());
            db.GroupMessageMembers.Remove(selData);
            db.SaveChanges();
            return true;
        }

        public List<GroupMessageMember> FindByKeyword(string Keyword)
        {
            var data = from x in db.GroupMessageMembers.Include(c=>c.User)
                       where x.User.FullName.Contains(Keyword)
                       select x;
            return data.ToList();
        }

        public List<GroupMessageMember> GetAllData()
        {
            return db.GroupMessageMembers.OrderBy(x => x.Id).ToList();
        }

        public GroupMessageMember GetDataById(object Id)
        {
            return db.GroupMessageMembers.Where(x => x.Id == (long)Id).FirstOrDefault();
        }


        public bool InsertData(GroupMessageMember data)
        {
            try
            {
                db.GroupMessageMembers.Add(data);
                db.SaveChanges();
                return true;
            }
            catch
            {

            }
            return false;

        }



        public bool UpdateData(GroupMessageMember data)
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
            return db.GroupMessageMembers.Max(x => x.Id);
        }
    }

}
/*











 */
