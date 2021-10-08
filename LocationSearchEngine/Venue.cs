//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LocationSearchEngine
{
    using System;
    using System.Collections.Generic;
    
    public partial class Venue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venue()
        {
            this.Photos = new HashSet<Photo>();
        }
    
        public int VenueId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public int LocationId { get; set; }
        public int CategoryId { get; set; }
        public Nullable<bool> Verified { get; set; }
        public int UserId { get; set; }
        public Nullable<System.DateTime> CaptureDate { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Location Location { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual User User { get; set; }
    }
}
