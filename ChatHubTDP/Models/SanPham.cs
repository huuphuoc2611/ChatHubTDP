namespace ChatHubTDP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDonHangs = new HashSet<ChiTietDonHang>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }

        [Required]
        [StringLength(100)]
        public string TenSP { get; set; }

        public decimal? Giaban { get; set; }

        public string Mota { get; set; }

        [StringLength(50)]
        public string Anhbia { get; set; }

        public DateTime? Ngaycapnhat { get; set; }

        public int? Soluongton { get; set; }

        public int? MaCD { get; set; }

        public int? IDParent { get; set; }

        public int? STT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }

        public virtual ChuDe ChuDe { get; set; }
    }
}
