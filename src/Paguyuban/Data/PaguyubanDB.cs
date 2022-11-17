using Microsoft.EntityFrameworkCore;
using Paguyuban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace Paguyuban.Data
{
    public class PaguyubanDB : DbContext
    {

        public PaguyubanDB()
        {
        }

        public PaguyubanDB(DbContextOptions<PaguyubanDB> options)
            : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }     
       
        public DbSet<Notification> Notifications { get; set; }      
      
        public DbSet<Contact> Contacts { get; set; }      
        public DbSet<Log> Logs { get; set; }      
        public DbSet<MessageHeader> MessageHeaders { get; set; }      
        public DbSet<MessageDetail> MessageDetails { get; set; }      
        public DbSet<MessageAttachment> MessageAttachments { get; set; }      
        public DbSet<GroupMessage> GroupMessages { get; set; }      
        public DbSet<GroupMessageMember> GroupMessageMembers { get; set; }      
        public DbSet<Note> Notes { get; set; }      
        public DbSet<Todo> Todos { get; set; }      
    
       
       
       
      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*
            builder.Entity<DataEventRecord>().HasKey(m => m.DataEventRecordId);
            builder.Entity<SourceInfo>().HasKey(m => m.SourceInfoId);

            // shadow properties
            builder.Entity<DataEventRecord>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<SourceInfo>().Property<DateTime>("UpdatedTimestamp");
            */
          
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            /*
            ChangeTracker.DetectChanges();

            updateUpdatedProperty<SourceInfo>();
            updateUpdatedProperty<DataEventRecord>();
            */
            return base.SaveChanges();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(AppConstants.SQLConn,ServerVersion.AutoDetect(AppConstants.SQLConn));
            }
        }
        private void updateUpdatedProperty<T>() where T : class
        {
            /*
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
            */
        }

    }
}
