//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeatLab.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment_Plugin
    {
        public int ID_Comment_Plugin { get; set; }
        public string Content_Comment { get; set; }
        public System.DateTime Date_Comment { get; set; }
        public int ID_Plugin { get; set; }
        public int ID_User { get; set; }
    
        public virtual Plugins Plugins { get; set; }
        public virtual User User { get; set; }
    }
}
