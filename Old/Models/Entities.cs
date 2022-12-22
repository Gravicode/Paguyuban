using GemBox.Document;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    #endregion

    [Table("message_header")]
    public class MessageHeader
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public string Uid { set; get; }
        [ForeignKey(nameof(FromUser)), Column(Order = 0)]
        public long FromUserId { set; get; }
        public UserProfile FromUser { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 1)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string? Title { set; get; }
        public string? Desc { set; get; }
        public DateTime CreatedDate { set; get; }
        public MessageTypes MessageType { set; get; }
        public string MemberIds { set; get; }
        public int MemberCount { set; get; } = 1;
        public string? LastMessage { set; get; }
        public DateTime LastUpdate { set; get; }
        public bool IsArchived { set; get; } = false;
        public bool IsRead { set; get; } = false;
        public string? WallpaperUrl { set; get; }
        public bool IsMuted { set; get; } = false;
        public bool IsBlocked { set; get; } = false;
    }

    [Table("message_detail")]
    public class MessageDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public string Uid { set; get; }
        [ForeignKey(nameof(MessageHeader)), Column(Order = 0)]
        public long MessageHeaderId { set; get; }
        public MessageHeader MessageHeader { get; set; }
        public DateTime CreatedDate { set; get; }
        [ForeignKey(nameof(FromUser)), Column(Order = 1)]
        public long FromUserId { set; get; }
        public UserProfile FromUser { set; get; }
        public string Message { set; get; }

        public bool HasAttachment { get; set; } = false;

        [InverseProperty(nameof(MessageAttachment.MessageDetail))]
        public ICollection<MessageAttachment> MessageAttachments { get; set; }
    }
    [Table("message_attachment")]
    public class MessageAttachment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(MessageDetail)), Column(Order = 0)]
        public long MessageDetailId { set; get; }
        public MessageDetail MessageDetail { set; get; }
        public string MessageHeaderUid { set; get; }
        public string MessageDetailUid { set; get; }
        public string Title { set; get; }
        public string? Url { set; get; }
        public string? Desc { set; get; }
        public string? Longitude { set; get; }
        public string? Latitude { set; get; }
        [ForeignKey(nameof(Contact)), Column(Order = 1)]
        public long ContactId { set; get; }
        public Contact Contact { set; get; }
        public AttachmentTypes AttachmentType { set; get; }
        public DateTime CreatedDate { set; get; }
    }

    [Table("contact")]
    public class Contact
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string? FullName { set; get; }
        public string? Phone { set; get; }
        public string? Email { set; get; }
        public string? Website { set; get; }
        public string? Address { set; get; }
        public DateTime? BirthDate { set; get; }
      
        public string? Facebook { set; get; }
        public string? Twitter { set; get; }
        public string? Instagram { set; get; }
        public string? LinkedIn { set; get; }
    }
    [Table("group_message")]
    public class GroupMessage
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string MessageHeaderUid { set; get; }
        public string Name { set; get; }
        public string? Desc { set; get; }
        public DateTime CreatedDate { set; get; }
        [ForeignKey(nameof(CreatedBy)), Column(Order = 0)]
        public long CreatedById { set; get; }
        public UserProfile CreatedBy { set; get; }

        [InverseProperty(nameof(GroupMessageMember.GroupMessage))]
        public ICollection<GroupMessageMember> GroupMessageMembers { get; set; }

    }

    [Table("group_message_member")]
    public class GroupMessageMember
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(GroupMessage)), Column(Order = 0)]
        public long GroupMessageId { set; get; }
        public GroupMessage GroupMessage { set; get; }
        public string MessageHeaderUid { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 1)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
    }

    [Table("note")]
    public class Note
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string Title { set; get; }
        public string? Message { set; get; }
        public string? Tag { set; get; }
        public DateTime CreatedDate { set; get; }

    }

    [Table("todo")]
    public class Todo
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string Title { set; get; }
        public string? Message { set; get; }
        public bool IsDone { set; get; } = false;
        public DateTime CreatedDate { set; get; }

    }
        public enum AttachmentTypes { Doc, Video, Audio, Link, Contact, Location };
    public enum MessageTypes { Personal, Group };

    [Table("notification")]
    public class Notification
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }

        public DateTime CreatedDate { set; get; }
        [ForeignKey(nameof(User)), Column(Order = 0)]
        public long UserId { set; get; }
        public UserProfile User { set; get; }
        public string Action { set; get; }
        public string LinkUrl { set; get; }
        public string LinkDesc { set; get; }
        public string Message { set; get; }
        public bool IsRead { set; get; } = false;
    }
    public enum LogCategory
    {
        Info, Error, Warning
    }
    [Table("logs")]
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LogDate { get; set; }
        public string Message { get; set; }
        public LogCategory Category { get; set; }
    }

    [Table("userprofile")]
    public class UserProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? KTP { get; set; }
        public string? PicUrl { get; set; }
        public bool Aktif { get; set; } = true;
        public string? Daerah { get; set; }
        public string? Desa { get; set; }
        public string? Kelompok { get; set; }
        public Char Gender { get; set; } = 'N';
        public Roles Role { set; get; } = Roles.User;
        public DateTime CreatedDate { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public string? FirstName { set; get; }
        public string? LastName { set; get; }

        public string? AboutMe { set; get; }

        public DateTime? BirthDate { set; get; }
        public string? Website { set; get; }
        public string? Facebook { set; get; }
        public string? Twitter { set; get; }
        public string? Instagram { set; get; }
        public string? LinkedIn { set; get; }
        public bool ViewProfilePic { set; get; }
        public bool ViewLastSeen { set; get; }
        public bool AddToGroup { set; get; }
        public bool ViewStatus { set; get; }
        public bool ReadReceipt { set; get; }
        public bool NotificationMuted { set; get; }
        public bool AutoBackup { set; get; }
        public bool ScreenLock { set; get; }
        public bool AutoDownload { set; get; }


        [InverseProperty(nameof(MessageHeader.User))]
        public ICollection<MessageHeader> UserMessages { get; set; }
      
        [InverseProperty(nameof(Contact.User))]
        public ICollection<Contact> Contacts { get; set; }

        [InverseProperty(nameof(GroupMessage.CreatedBy))]
        public ICollection<GroupMessage> GroupMessages { get; set; }

        [InverseProperty(nameof(Note.User))]
        public ICollection<Note> Notes { get; set; }

        [InverseProperty(nameof(Todo.User))]
        public ICollection<Todo> Todos { get; set; }

    }



    public enum Roles { Admin, User, Pengurus }


}
