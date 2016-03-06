namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        //[Required(ErrorMessageResourceType = typeof(StaticResource.Resources),ErrorMessageResourceName=]
        [Display(Name = "UserName", ResourceType= typeof(StaticResource.Resources))]
        public string UserName { get; set; }

        [StringLength(32)]
        [Display(Name = "Password", ResourceType = typeof(StaticResource.Resources))]
        public string Password { get; set; }

        [StringLength(50)]
        [Display(Name = "Name", ResourceType = typeof(StaticResource.Resources))]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Address", ResourceType = typeof(StaticResource.Resources))]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Phone", ResourceType = typeof(StaticResource.Resources))]
        public string Phone { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
        [Display(Name = "Status", ResourceType = typeof(StaticResource.Resources))]
        public bool Status { get; set; }
    }
}
