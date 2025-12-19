using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPF_aula1
{
    class BecrowdEntity
    {
        public  string Id { get; set; }
        public bool Resolvido { get; set; }

        public override string ToString()
        {
            return $"Exercício: {Id} - {(Resolvido ? "Resolvido ✔" : "Não resolvido ✖")}";
        }
    }
}
