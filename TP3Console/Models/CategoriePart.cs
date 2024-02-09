using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3Console.Models.EntityFramework
{
    public partial class Categorie
    {
        public Categorie() { }
        public override string ToString()
        {
            return $"Id : {this.Id}\nNom : {this.Nom}\nDescription : {this.Description}";
        }
    }
}
