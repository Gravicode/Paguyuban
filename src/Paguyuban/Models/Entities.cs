using GemBox.Document;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Redis.OM.Modeling;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Paguyuban.Models
{
    #region helpers model
    public class OutputCls
    {
        public OutputCls()
        {
            this.IsSucceed = false;
            this.Message = "";
        }
        public object Data { get; set; }
        public string Message { get; set; }
        public bool IsSucceed { get; set; }
    }
    public class StorageObject
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public string FileUrl { get; set; }
        public string ContentType { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? LastAccess { get; set; }
    }
    public class StorageSetting
    {
        public string EndpointUrl { get; set; } = "https://is3.cloudhost.id";
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; } = "USWest1";
        public string Bucket { get; set; }
        public string BaseUrl { get; set; }
        public bool Ssl { get; set; } = true;
        public StorageSetting()
        {

        }
        public StorageSetting(string Endpoint, string Accesskey, string Secretkey, string Region, string Bucket)
        {
            this.EndpointUrl = Endpoint;
            this.AccessKey = Accesskey;
            this.SecretKey = Secretkey;
            this.Region = Region;
            this.Bucket = Bucket;
            GenerateBaseUrl();
        }
        public void GenerateBaseUrl()
        {
            this.BaseUrl = EndpointUrl + "/{bucket}/{key}";
        }
    }
    public class Inbox
    {
        public UserProfile User { get; set; }
        public MessageBox Message { get; set; }

    }
    #endregion
    [Document(StorageType = StorageType.Json)]
    public class MessageBox
    {

        [RedisIdField] public string Id { get; set; }

        [Indexed(Sortable = true)]
        public string UserId { get; set; }

        [Indexed(Sortable = true)]
        public string ToUserId { get; set; }

        [Indexed(Sortable = true)]
        public string Uid { get; set; }
        [Indexed(Sortable = true)]
        public string Username { get; set; }
        [Indexed(Sortable = true)]
        public string ToUsername { get; set; }
        [Searchable(Sortable = true)]
        public string? Title { set; get; }
        [Indexed]
        public string? Desc { set; get; }
        [Indexed(Sortable = true)]
        public DateTime CreatedDate { set; get; }
        public string? LastMessage { set; get; }
        [Indexed]
        public DateTime LastUpdate { set; get; }
        [Indexed]
        public bool IsArchived { set; get; } = false;
        [Indexed]
        public bool IsRead { set; get; } = false;
        [Indexed]
        public bool IsGroup { set; get; } = false;
        [Indexed]
        //public string? WallpaperUrl { set; get; }
        public bool IsMuted { set; get; } = false;
        [Indexed]
        public bool IsBlocked { set; get; } = false;
        public List<MessageDetail> Chats { get; set; } = new();
    }
    public class MessageDetail
    {
        public UserProfile FromUser { set; get; }
        public DateTime CreatedDate { set; get; }
        [Indexed]
        public string Message { set; get; }
        public bool HasAttachment { get; set; } = false;
        public List<MessageAttachment> Attachments { get; set; } = new();

    }
    public class MessageAttachment
    {
        [Indexed]
        public string Title { set; get; }
        [Indexed]
        public string? Url { set; get; }
        [Indexed]
        public string? Desc { set; get; }
        public string? Longitude { set; get; }
        public string? Latitude { set; get; }

        public AttachmentTypes AttachmentType { set; get; }
        [Indexed(Sortable = true)]
        public DateTime CreatedDate { set; get; }
    }
    //public enum AttachmentTypes { Doc, Video, Audio, Link, Location };

    //[Table("contact")]
    public class Contact
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        //[RedisIdField] public string Id { get; set; }
        //[ForeignKey(nameof(User)), Column(Order = 0)]
        [Indexed(Sortable = true)]
        public string UserId { set; get; }
        [Indexed(Sortable = true)]
        public UserProfile User { set; get; }
        [Indexed(Sortable = true)]
        public string? FullName { set; get; }
        [Indexed(Sortable = true)]
        public string? Phone { set; get; }
        [Indexed(Sortable = true)]
        public string? Email { set; get; }
        [Indexed(Sortable = true)]
        public string? Website { set; get; }
        [Indexed(Sortable = true)]
        public string? Address { set; get; }
        [Indexed(Sortable = true)]
        public DateTime? BirthDate { set; get; }
        [Indexed(Sortable = true)]
        public string? Facebook { set; get; }
        [Indexed(Sortable = true)]
        public string? Twitter { set; get; }
        [Indexed(Sortable = true)]
        public string? Instagram { set; get; }
        [Indexed(Sortable = true)]
        public string? LinkedIn { set; get; }
    }
    [Table("group_message")]
    [Document(StorageType = StorageType.Json)]
    public class GroupMessage
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        [RedisIdField] public string Id { get; set; }
        [Indexed(Sortable = true)]
        public string MessageUid { set; get; }
        [Indexed(Sortable = true)]
        public string Name { set; get; }
        [Indexed(Sortable = true)]
        public string? Desc { set; get; }
        [Indexed(Sortable = true)]
        public DateTime CreatedDate { set; get; }
        //[ForeignKey(nameof(CreatedBy)), Column(Order = 0)]
        [Indexed(Sortable = true)]
        public string UserId { set; get; }
        [Indexed(Sortable = true)]
        public UserProfile User { set; get; }

        //[InverseProperty(nameof(GroupMessageMember.GroupMessage))]
        [Indexed(Sortable = true)]
        public List<GroupMessageMember> GroupMessageMembers { get; set; } = new();

    }

    [Table("group_message_member")]
    public class GroupMessageMember
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        //[RedisIdField] public string Id { get; set; }
        //[ForeignKey(nameof(GroupMessage)), Column(Order = 0)]
        //public long GroupMessageId { set; get; }
        //public GroupMessage GroupMessage { set; get; }
        //public string MessageHeaderUid { set; get; }
        //[ForeignKey(nameof(User)), Column(Order = 1)]
        //public string UserId { set; get; }
        [Indexed(Sortable = true)]
        public UserProfile User { set; get; }
        [Indexed(Sortable = true)]
        public string UserId { get; set; }
    }

    //[Table("note")]
    public class Note
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        //[RedisIdField] public string Id { get; set; }
        //[ForeignKey(nameof(User)), Column(Order = 0)]
        [Indexed(Sortable = true)]
        public string UserId { set; get; }
        [Indexed(Sortable = true)]
        public UserProfile User { set; get; }
        [Indexed(Sortable = true)]
        public string Title { set; get; }
        [Indexed(Sortable = true)]
        public string? Message { set; get; }
        [Indexed(Sortable = true)]
        public string? Tag { set; get; }
        [Indexed(Sortable = true)]
        public DateTime CreatedDate { set; get; }

    }

    //[Table("todo")]
    public class Todo
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        //[RedisIdField] public string Id { get; set; }

        //[ForeignKey(nameof(User)), Column(Order = 0)]
        [Indexed(Sortable = true)]
        public string UserId { set; get; }
        [Indexed(Sortable = true)]
        public UserProfile User { set; get; }
        [Indexed(Sortable = true)]
        public string Title { set; get; }
        [Indexed(Sortable = true)]
        public string? Message { set; get; }
        [Indexed(Sortable = true)]
        public bool IsDone { set; get; } = false;
        [Indexed(Sortable = true)]
        public DateTime CreatedDate { set; get; }

    }
        public enum AttachmentTypes { Doc, Video, Audio, Link, Contact, Location };
    public enum MessageTypes { Personal, Group };

    [Table("notification")]
    [Document(StorageType = StorageType.Json)]
    public class Notification
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        [RedisIdField] public string Id { get; set; }
        [Indexed(Sortable = true)]
        public DateTime CreatedDate { set; get; }
        [Indexed(Sortable = true)]
        //[ForeignKey(nameof(User)), Column(Order = 0)]
        public string UserId { set; get; }
        [Indexed(Sortable = true)]
        public UserProfile User { set; get; }
        [Indexed(Sortable = true)]
        public string Action { set; get; }
        [Indexed(Sortable = true)]
        public string LinkUrl { set; get; }
        [Indexed(Sortable = true)]
        public string LinkDesc { set; get; }
        [Indexed(Sortable = true)]
        public string Message { set; get; }
        [Indexed(Sortable = true)]
        public bool IsRead { set; get; } = false;
    }
    public enum LogCategory
    {
        Info, Error, Warning
    }
    [Table("logs")]
    [Document(StorageType = StorageType.Json)]
    public class Log
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        [RedisIdField] public string Id { get; set; }
        [Indexed(Sortable = true)]
        public string CreatedBy { get; set; }
        [Indexed(Sortable = true)]
        public DateTime LogDate { get; set; }
        [Indexed(Sortable = true)]
        public string Message { get; set; }
        [Indexed(Sortable = true)]
        public LogCategory Category { get; set; }
    }

    [Table("userprofile")]
    [Document(StorageType = StorageType.Json)]
    public class UserProfile
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key, Column(Order = 0)]
        [RedisIdField] public string Id { get; set; }
        [Indexed(Sortable = true)]
        public string Username { get; set; }
        [Indexed(Sortable = true)]
        public string Password { get; set; }
        [Indexed(Sortable = true)]
        public string FullName { get; set; }
        [Indexed(Sortable = true)]
        public string? Phone { get; set; }
        [Indexed(Sortable = true)]
        public string? Email { get; set; }
        [Indexed(Sortable = true)]
        public string? Address { get; set; }
        [Indexed(Sortable = true)]
        public string? KTP { get; set; }
        [Indexed(Sortable = true)]
        public string? PicUrl { get; set; }
        [Indexed(Sortable = true)]
        public bool Aktif { get; set; } = true;
        [Indexed(Sortable = true)]
        public string? Daerah { get; set; }
        [Indexed(Sortable = true)]
        public string? Desa { get; set; }
        [Indexed(Sortable = true)]
        public string? Kelompok { get; set; }
        [Indexed(Sortable = true)]
        public Char Gender { get; set; } = 'N';
        [Indexed(Sortable = true)]
        public Roles Role { set; get; } = Roles.User;
        [Indexed(Sortable = true)]
        public DateTime CreatedDate { get; set; }
        [Indexed(Sortable = true)]
        public double? Longitude { get; set; }
        [Indexed(Sortable = true)]
        public double? Latitude { get; set; }
        [Indexed(Sortable = true)]
        public string? FirstName { set; get; }
        [Indexed(Sortable = true)]
        public string? LastName { set; get; }
        [Indexed(Sortable = true)]
        public string? AboutMe { set; get; }
        [Indexed(Sortable = true)]
        public DateTime? BirthDate { set; get; }
        [Indexed(Sortable = true)]
        public string? Website { set; get; }
        [Indexed(Sortable = true)]
        public string? Facebook { set; get; }
        [Indexed(Sortable = true)]
        public string? Twitter { set; get; }
        [Indexed(Sortable = true)]
        public string? Instagram { set; get; }
        [Indexed(Sortable = true)]
        public string? LinkedIn { set; get; }
        [Indexed(Sortable = true)]
        public bool ViewProfilePic { set; get; }
        [Indexed(Sortable = true)]
        public bool ViewLastSeen { set; get; }
        [Indexed(Sortable = true)]
        public bool AddToGroup { set; get; }
        [Indexed(Sortable = true)]
        public bool ViewStatus { set; get; }
        [Indexed(Sortable = true)]
        public bool ReadReceipt { set; get; }
        [Indexed(Sortable = true)]
        public bool NotificationMuted { set; get; }
        [Indexed(Sortable = true)]
        public bool AutoBackup { set; get; }
        [Indexed(Sortable = true)]
        public bool ScreenLock { set; get; }
        [Indexed(Sortable = true)]
        public bool AutoDownload { set; get; }


        //[InverseProperty(nameof(MessageHeader.User))]
        //public List<MessageHeader> UserMessages { get; set; }

        //[InverseProperty(nameof(Contact.User))]
        public List<Contact> Contacts { get; set; } = new();

        //[InverseProperty(nameof(GroupMessage.CreatedBy))]
        //public List<string> GroupMessageIds { get; set; }

        //[InverseProperty(nameof(Note.User))]
        public List<Note> Notes { get; set; } = new();

        //[InverseProperty(nameof(Todo.User))]
        public List<Todo> Todos { get; set; } = new();

    }



    public enum Roles { Admin, User, Pengurus }


}
