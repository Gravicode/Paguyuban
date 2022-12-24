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
    public class ContactService : ICrud<Contact>
    {
        //PaguyubanDB db;
        RedisConnectionProvider provider;
        IRedisCollection<Contact> db;
        UserProfileService UserSvc;

        public ContactService(RedisConnectionProvider provider, UserProfileService userservice)
        {
            this.UserSvc = userservice;
            this.provider = provider;
            db = this.provider.RedisCollection<Contact>();
        }
        public List<Contact> GetByUsername(string Username)
        {
            return db.Where(x => x.Username == Username).ToList();
        }
        public void RefreshEntity(Contact item)
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
        public bool DeleteData(Contact item)
        {
            db.Delete(item);
            return true;
        }

        public List<Contact> FindByKeyword(string Keyword)
        {
            var data = db.Where(x => x.FullName.Contains(Keyword) || x.Email.Contains(Keyword) || x.Phone.Contains(Keyword));
            return data.ToList();
        }
        
      
        
        public List<Contact> FindByKeyword(string Keyword,string Username)
        {
            var data = string.IsNullOrEmpty(Keyword) ? db.Where(x => x.Username == Username) : db.Where(x => (x.FullName.Contains(Keyword) || x.Email.Contains(Keyword) || x.Phone.Contains(Keyword)) && x.Username == Username);
            return data.ToList();
        }
        public List<ContactAlphabet> FindByKeywordAlphabet(string Keyword, string Username)
        {
            var data = string.IsNullOrEmpty(Keyword) ? db.Where(x => x.Username == Username).ToList() : db.Where(x => (x.FullName.Contains(Keyword) || x.Email.Contains(Keyword) || x.Phone.Contains(Keyword)) && x.Username == Username).ToList();

            var ListContact = data.GroupBy(x => x.FullName.Substring(0, 1).ToUpper()).Select(y => new ContactAlphabet()
            {
                Key = y.Key,
                Datas = y.ToList()
            }
        ).ToList();
            return ListContact;
        }


        public List<Contact> GetAllData()
        {
            return db.ToList();
        }

        public Contact GetDataById(string Id)
        {
            return db.Where(x => x.Id == Id).FirstOrDefault();
        }


        public bool InsertData(Contact data)
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

        public bool UpdateData(Contact data)
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

        public Contact GetDataById(object Id)
        {
            return db.Where(x => x.Id == Id.ToString()).FirstOrDefault();
        }

        public long GetLastId()
        {
            throw new NotImplementedException();
        }
    }

}
