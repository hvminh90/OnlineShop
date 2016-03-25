namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessageResourceType = typeof(StaticResource.Resources),ErrorMessageResourceName="rqContenName")]
        [Display(Name = "ContentName", ResourceType = typeof(StaticResource.Resources))]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(500)]
        [Required(ErrorMessageResourceType = typeof(StaticResource.Resources), ErrorMessageResourceName = "rqContenDescription")]
        [Display(Name = "ContentDescription", ResourceType = typeof(StaticResource.Resources))]
        public string Description { get; set; }

        [StringLength(250)]
        //[Required(ErrorMessageResourceType = typeof(StaticResource.Resources), ErrorMessageResourceName = "rqContentImage")]
        [Display(Name = "ContentImage", ResourceType = typeof(StaticResource.Resources))]
        public string Image { get; set; }

        [Required(ErrorMessageResourceType = typeof(StaticResource.Resources), ErrorMessageResourceName = "rqContentCategory")]
        [Display(Name = "ContentCategory", ResourceType = typeof(StaticResource.Resources))]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name="ContentDetail", ResourceType = typeof(StaticResource.Resources))]
        [Required(ErrorMessageResourceType=typeof(StaticResource.Resources), ErrorMessageResourceName="rqContentDetail")]
        public string Detail { get; set; }

        public int? Warranty { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }
        [Display(Name = "ContentStatus", ResourceType = typeof(StaticResource.Resources))]
        public bool Status { get; set; }

        public DateTime? TopHot { get; set; }

        public int? ViewCount { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }

        [StringLength(2)]
        public string Language { get; set; }
    }
}
