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
    public class MessageBoxService : ICrud<MessageBox>
    {
        //PaguyubanDB db;
        RedisConnectionProvider provider;
        IRedisCollection<MessageBox> db;
        UserProfileService UserSvc;

        public MessageBoxService(RedisConnectionProvider provider, UserProfileService userservice)
        {
            this.UserSvc = userservice;
            this.provider = provider;
            db = this.provider.RedisCollection<MessageBox>();
        }

        public void RefreshEntity(MessageBox item)
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
        public bool DeleteData(MessageBox item)
        {
            db.Delete(item);
            return true;
        }

        public int GetUnreadMessageByUser(string username)
        {
            
            var data = db.Where(x => x.Username == username && !x.IsRead).Count();
            return data;
        }
        
        public MessageBox GetInboxByUid(string uid, string username)
        {
            var data = db.Where(x => x.Username == username && x.Uid == uid).FirstOrDefault();
            return data;
        } 
        
        public MessageBox GetInboxByFromAndTo(string FromUsername,string ToUsername)
        {
            var data = db.Where(x => x.Username == FromUsername && x.ToUsername == ToUsername).FirstOrDefault();
            return data;
        }

        public List<MessageBox> FindByKeyword(string Keyword)
        {
            var data = db.Where(x => x.Title.Contains(Keyword));
            return data.ToList();
        }
        public List<MessageBox> GetLatestMessage(string username)
        {
            var data = db.Where(x => x.Username == username).ToList();
            return data.Where(x => !x.IsRead).OrderByDescending(x => x.CreatedDate).ToList();
        }
        public List<Inbox> LoadInbox(string username)
        {
            try
            {
                var messages = GetLatestMessage(username);
                var exist_user = messages.Select(x => x.ToUsername).ToList();
                var currentUser = UserSvc.GetItemByUsername(username);
                var friend_usernames = currentUser?.Contacts.Where(x => !exist_user.Contains(x.User.Username)).Select(x => x.User.Username);
                var friends = UserSvc.GetByUsernames(friend_usernames.ToArray());
                var list_inbox = new List<Inbox>();
                UserProfile usr;
                foreach (var item in messages)
                {
                    list_inbox.Add(new Inbox() { Message = item, User = UserSvc.GetItemByUsername(item.ToUsername) });
                }
                foreach (var friend in friends)
                {
                    list_inbox.Add(new Inbox() { Message = new() { }, User = friend });
                }
                return list_inbox;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return default;
           
        }

        public List<MessageBox> GetAllData()
        {
            return db.ToList();
        }

        public MessageBox GetDataById(string Id)
        {
            return db.Where(x => x.Id == Id).FirstOrDefault();
        }


        public bool InsertData(MessageBox data)
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

        public bool UpdateData(MessageBox data)
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

        public MessageBox GetDataById(object Id)
        {
            return db.Where(x => x.Id == Id.ToString()).FirstOrDefault();
        }

        public long GetLastId()
        {
            throw new NotImplementedException();
        }
    }

}
