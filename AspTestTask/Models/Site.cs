//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AspTestTask.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Site
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public Nullable<int> Rating { get; set; }
        public Nullable<int> Hosting { get; set; }
        public System.DateTime Last_edited { get; set; }
    
        public virtual HostingPlan HostingPlan { get; set; }
    }
}
