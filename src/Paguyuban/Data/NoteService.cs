using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Pdf.Content.Objects;
using Redis.OM;
using Redis.OM.Searching;
using Paguyuban.Data;
using Paguyuban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
namespace Paguyuban.Data
{
    public class NoteService : ICrud<Note>
    {
        //PaguyubanDB db;
        RedisConnectionProvider provider;
        IRedisCollection<Note> db;
        UserProfileService UserSvc;

        public NoteService(RedisConnectionProvider provider, UserProfileService userservice)
        {
            this.UserSvc = userservice;
            this.provider = provider;
            db = this.provider.RedisCollection<Note>();
        }

        public void RefreshEntity(Note item)
        {
            try
            {
                //do nothing
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }


        }
        public bool DeleteData(Note item)
        {
            db.Delete(item);
            return true;
        }

        public List<Note> FindByKeyword(string Keyword)
        {
            var data = db.Where(x => x.Message.Contains(Keyword));
            return data.ToList();
        }

        public List<Note> GetAllData()
        {
            return db.ToList();
        } 
        
        public List<Note> GetByUsername(string Username)
        {
            return db.Where(x=>x.Username == Username).ToList();
        }

        public Note GetDataById(string Id)
        {
            return db.Where(x => x.Id == Id).FirstOrDefault();
        }


        public bool InsertData(Note data)
        {
            try
            {
                db.Insert(data);
                //db.Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;

        }

        public bool UpdateData(Note data)
        {
            try
            {
                db.Update(data);
                //db.Save();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }

        public Note GetDataById(object Id)
        {
            return db.Where(x => x.Id == Id.ToString()).FirstOrDefault();
        }

        public long GetLastId()
        {
            throw new NotImplementedException();
        }
    }

}
