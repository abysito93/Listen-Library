//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lis_Lib.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Credit_Card
    {
        public int Id { get; set; }
        public string User_Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zip_code { get; set; }
        public string Card_number { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
