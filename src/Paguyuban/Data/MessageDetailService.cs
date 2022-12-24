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
    public class MessageDetailService : ICrud<MessageDetail>
    {
        //PaguyubanDB db;
        RedisConnectionProvider provider;
        IRedisCollection<MessageDetail> db;
        UserProfileService UserSvc;

        public MessageDetailService(RedisConnectionProvider provider, UserProfileService userservice)
        {
            this.UserSvc = userservice;
            this.provider = provider;
            db = this.provider.RedisCollection<MessageDetail>();
        }

        public void RefreshEntity(MessageDetail item)
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
        public bool DeleteData(MessageDetail item)
        {
            db.Delete(item);
            return true;
        } 
        public bool DeleteDataByUid(string Uid, string Username)
        {
            var deleted = db.Where(x => x.Uid == Uid && x.Username == Username).ToList();
            foreach(var item in deleted)
            {
                db.Delete(item);
            }
            return true;
        }

        public List<MessageDetail> FindByKeyword(string Keyword)
        {
            var data = db.Where(x => x.Message.Contains(Keyword));
            return data.ToList();
        }

        public List<MessageDetail> GetAllData()
        {
            return db.ToList();
        }
        
        public List<MessageDetail> GetByUid(string Uid,string Username)
        {
            var datas = db.Where(x=>x.Uid==Uid && x.Username==Username).ToList();
            return datas == null ? new() : datas.OrderBy(x => x.CreatedDate).ToList();
        }

        public MessageDetail GetDataById(string Id)
        {
            return db.Where(x => x.Id == Id).FirstOrDefault();
        }


        public bool InsertData(MessageDetail data)
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

        public bool UpdateData(MessageDetail data)
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

        public MessageDetail GetDataById(object Id)
        {
            return db.Where(x => x.Id == Id.ToString()).FirstOrDefault();
        }

        public long GetLastId()
        {
            throw new NotImplementedException();
        }
    }

}
