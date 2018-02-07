﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RIFTGroup.GCTSC.Core.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using RIFTGroup.GCTSC.Core;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GoldmineEntities : DbContext
    {
        public GoldmineEntities()
            : base("name=GoldmineEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CONTACT1> CONTACT1 { get; set; }
        public virtual DbSet<CONTACT2> CONTACT2 { get; set; }
        public virtual DbSet<CONTSUPP> CONTSUPPs { get; set; }
    
        public virtual ObjectResult<CONTACT1ChangeTracking_Result> CONTACT1ChangeTracking(Nullable<int> lastVersionNumber)
        {
            var lastVersionNumberParameter = lastVersionNumber.HasValue ?
                new ObjectParameter("LastVersionNumber", lastVersionNumber) :
                new ObjectParameter("LastVersionNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CONTACT1ChangeTracking_Result>("CONTACT1ChangeTracking", lastVersionNumberParameter);
        }
    
        public virtual ObjectResult<CONTACT2ChangeTracking_Result> CONTACT2ChangeTracking(Nullable<int> lastVersionNumber)
        {
            var lastVersionNumberParameter = lastVersionNumber.HasValue ?
                new ObjectParameter("LastVersionNumber", lastVersionNumber) :
                new ObjectParameter("LastVersionNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CONTACT2ChangeTracking_Result>("CONTACT2ChangeTracking", lastVersionNumberParameter);
        }
    
        public virtual int TESTS_CreateContact1Change(string accountno)
        {
            var accountnoParameter = accountno != null ?
                new ObjectParameter("accountno", accountno) :
                new ObjectParameter("accountno", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TESTS_CreateContact1Change", accountnoParameter);
        }
    
        public virtual int TESTS_CreateContact2Change(string accountno)
        {
            var accountnoParameter = accountno != null ?
                new ObjectParameter("accountno", accountno) :
                new ObjectParameter("accountno", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TESTS_CreateContact2Change", accountnoParameter);
        }
    
        public virtual int TESTS_CreateContsuppChange(string accountno)
        {
            var accountnoParameter = accountno != null ?
                new ObjectParameter("accountno", accountno) :
                new ObjectParameter("accountno", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("TESTS_CreateContsuppChange", accountnoParameter);
        }
    
        public virtual ObjectResult<CONTSUPPChangeTracking_Result> CONTSUPPChangeTracking(Nullable<int> lastVersionNumber)
        {
            var lastVersionNumberParameter = lastVersionNumber.HasValue ?
                new ObjectParameter("LastVersionNumber", lastVersionNumber) :
                new ObjectParameter("LastVersionNumber", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CONTSUPPChangeTracking_Result>("CONTSUPPChangeTracking", lastVersionNumberParameter);
        }
    }
}