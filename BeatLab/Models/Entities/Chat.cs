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
    
    public partial class Chat
    {
        public int ID_Chat { get; set; }
        public int ID_Sender { get; set; }
        public int ID_Receiver { get; set; }
        public string Message { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
