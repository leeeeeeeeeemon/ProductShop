//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProductShop.BD
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductIntake
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductIntake()
        {
            this.ProductIntakeProduct = new HashSet<ProductIntakeProduct>();
        }
    
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public decimal TotalAmount { get; set; }
        public System.DateTime Data { get; set; }
    
        public virtual Supplier Supplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductIntakeProduct> ProductIntakeProduct { get; set; }
    }
}
