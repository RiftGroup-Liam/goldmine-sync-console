//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RIFTGroup.GCTSC.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class GCTSC_ChangeTracking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GCTSC_ChangeTracking()
        {
            this.GCTSC_ChangeTracking_Requests = new HashSet<GCTSC_ChangeTracking_Requests>();
            this.GCTSC_ChangeTracking_UpdateRequests = new HashSet<GCTSC_ChangeTracking_UpdateRequests>();
        }
    
        public int Id { get; set; }
        public string Accountno { get; set; }
        public System.DateTime Created { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GCTSC_ChangeTracking_Requests> GCTSC_ChangeTracking_Requests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GCTSC_ChangeTracking_UpdateRequests> GCTSC_ChangeTracking_UpdateRequests { get; set; }
    }
}
