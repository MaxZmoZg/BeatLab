//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeatLab
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment_Music
    {
        public int ID_Comments_Music { get; set; }
        public string Content_Comments { get; set; }
        public System.DateTime Data_Comments { get; set; }
        public int ID_Music { get; set; }
        public int ID_User { get; set; }
    
        public virtual Music Music { get; set; }
        public virtual User User { get; set; }
    }
}
