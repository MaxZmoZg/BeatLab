//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeatLab.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order_Plugin
    {
        public int ID_Order_Plugin { get; set; }
        public int ID_Plugin { get; set; }
        public int ID_User { get; set; }
        public string Card_Number { get; set; }
        public string Card_expiration_date { get; set; }
        public string Card_secure_code { get; set; }
        public string Card_owner { get; set; }
    
        public virtual Plugins Plugins { get; set; }
        public virtual User User { get; set; }
    }
}
