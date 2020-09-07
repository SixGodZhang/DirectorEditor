using MVPFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectorEditor.Models
{
    public class DataPart1Model:IModel
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string Age { set; get; }
    }
}
