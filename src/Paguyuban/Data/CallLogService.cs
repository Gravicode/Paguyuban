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
using ServiceStack;

namespace Paguyuban.Data
{
    public class CallLogService : ICrud<CallLog>
    {
        //PaguyubanDB db;
        RedisConnectionProvider provider;
        IRedisCollection<CallLog> db;
        UserProfileService UserSvc;

        public CallLogService(RedisConnectionProvider provider, UserProfileService userservice)
        {
            this.UserSvc = userservice;
            this.provider = provider;
            db = this.provider.RedisCollection<CallLog>();
        }

        public void RefreshEntity(CallLog item)
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
        public bool DeleteData(CallLog item)
        {
            db.Delete(item);
            return true;
        }

        public List<CallLog> FindByKeyword(string Keyword)
        {
            var data = db.Where(x => x.FromUser.FullName.Contains(Keyword) || x.ToUser.FullName.Contains(Keyword));
            return data.ToList();
        }
        
        public List<CallLog> FindByKeyword(string Keyword,string Username)
        {
            var data = db.Where(x => x.Username == Username).ToList();
            return data.Where(x => x.FromUser.FullName.Contains(Keyword) || x.ToUser.FullName.Contains(Keyword)).OrderBy(x=>x.CallDate).ToList();
        } 
        
        public List<CallItem> FindByKeywordCall(string Keyword,string Username)
        {
            var data = db.Where(x => x.Username == Username).ToList();

            data = string.IsNullOrEmpty(Keyword) ? data.OrderBy(x => x.CallDate).ToList() : data.Where(x => x.FromUser.FullName.Contains(Keyword) || x.ToUser.FullName.Contains(Keyword)).OrderBy(x => x.CallDate).ToList();
            var temp = from x in data
                       select new { username = x.Username == x.ToUser.Username ? x.FromUser.Username : x.ToUser.Username, touser = x.Username == x.ToUser.Username ? x.FromUser : x.ToUser, Data = x };

            var ListCall = temp.GroupBy(x => x.username).Select(y => new CallItem()
            {
                Username = y.Key,
                ToUser = y.First().touser,
                Datas = y.Select(x=>x.Data).ToList()
            }).ToList();
            return ListCall;
        }

        public List<CallLog> GetAllData()
        {
            return db.ToList();
        }

        public CallLog GetDataById(string Id)
        {
            return db.Where(x => x.Id == Id).FirstOrDefault();
        }


        public bool InsertData(CallLog data)
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

        public bool UpdateData(CallLog data)
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

        public CallLog GetDataById(object Id)
        {
            return db.Where(x => x.Id == Id.ToString()).FirstOrDefault();
        }

        public long GetLastId()
        {
            throw new NotImplementedException();
        }
    }

}
