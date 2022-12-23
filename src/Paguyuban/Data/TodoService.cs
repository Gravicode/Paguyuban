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
    public class TodoService : ICrud<Todo>
    {
        //PaguyubanDB db;
        RedisConnectionProvider provider;
        IRedisCollection<Todo> db;
        UserProfileService UserSvc;

        public TodoService(RedisConnectionProvider provider, UserProfileService userservice)
        {
            this.UserSvc = userservice;
            this.provider = provider;
            db = this.provider.RedisCollection<Todo>();
        }
        public List<Todo> GetByUsername(string Username)
        {
            return db.Where(x => x.Username == Username).ToList();
        }
        public void RefreshEntity(Todo item)
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
        public bool DeleteData(Todo item)
        {
            db.Delete(item);
            return true;
        }

        public List<Todo> FindByKeyword(string Keyword)
        {
            var data = db.Where(x => x.Message.Contains(Keyword));
            return data.ToList();
        }

        public List<Todo> GetAllData()
        {
            return db.ToList();
        }

        public Todo GetDataById(string Id)
        {
            return db.Where(x => x.Id == Id).FirstOrDefault();
        }


        public bool InsertData(Todo data)
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

        public bool UpdateData(Todo data)
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

        public Todo GetDataById(object Id)
        {
            return db.Where(x => x.Id == Id.ToString()).FirstOrDefault();
        }

        public long GetLastId()
        {
            throw new NotImplementedException();
        }
    }

}
