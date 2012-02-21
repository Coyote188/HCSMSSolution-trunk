using System;
using System.Collections.Generic;
using System.Text;

namespace HCSMS.Model
{
    [Serializable]
    public class Member
    {
        public string Name { get; set; }
        public string OrganizationName { get; set; }
        public string Phone { get; set; }
        public string OrganizationLocation { get; set; }
        public string Id { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
                
        public List<MemberCard> Card { get; set; }            

    }
}
