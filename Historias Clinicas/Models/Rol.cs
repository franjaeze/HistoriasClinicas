using Historias_Clinicas.Helpers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Historias_Clinicas.Models
{
    public class Rol : IdentityRole<int>
    {
        public Rol() : base()
        {

        }

        public Rol(string rolName) : base(rolName)
        {

        }
        
        public override string Name {
            get { return base.Name; }
            set { base.Name = value; }
        }
         public override string NormalizedName 
        { get => base.NormalizedName; 
          set => base.NormalizedName = value; 
        }

    }
}
