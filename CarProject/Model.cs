using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject
{
    internal class Model
    {
        static int counter = 0;
        public Model()
        {
            this.ModelId = ++counter;
        }
        public int ModelId { get; set; }

        public string ModelName { get; set; }

        public int BrandId1 { get; set; }

        public override string ToString()
        {
            return $"Model ID: {ModelId} || Model Name: {ModelName} || Brand ID: {BrandId1}";
        }
    }
}
