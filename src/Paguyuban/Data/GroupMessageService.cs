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
    public class GroupMessageService : ICrud<GroupMessage>
    {
        //PaguyubanDB db;
        RedisConnectionProvider provider;
        IRedisCollection<GroupMessage> db;
        UserProfileService UserSvc;

        public GroupMessageService(RedisConnectionProvider provider, UserProfileService userservice)
        {
            this.UserSvc = userservice;
            this.provider = provider;
            db = this.provider.RedisCollection<GroupMessage>();
        }

        public void RefreshEntity(GroupMessage item)
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
        public bool DeleteData(GroupMessage item)
        {
            db.Delete(item);
            return true;
        }

        public List<GroupMessage> FindByKeyword(string Keyword)
        {
            var data = db.Where(x => x.Name.Contains(Keyword));
            return data.ToList();
        }
       
        public List<GroupMessage> GetAllData()
        {
            return db.ToList();
        }

        public GroupMessage GetDataById(string Id)
        {
            return db.Where(x => x.Id == Id).FirstOrDefault();
        }


        public bool InsertData(GroupMessage data)
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

        public bool UpdateData(GroupMessage data)
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

        public GroupMessage GetDataById(object Id)
        {
            return db.Where(x => x.Id == Id.ToString()).FirstOrDefault();
        }

        public long GetLastId()
        {
            throw new NotImplementedException();
        }
    }

}
