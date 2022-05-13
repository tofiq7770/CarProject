using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Mangers
{
    internal class ModelManager
    {
        Model[] data = new Model[0];

        public void Add(Model entity)
        {
            int len = data.Length;
            Array.Resize(ref data, len + 1);
            data[len] = entity;
        }

        public void ModelRemove(Model entity)
        {
            int index = Array.IndexOf(data, entity);

            if (index == -1)
            {
                return;
            }

            for (int i = index; i < data.Length - 1; i++)
            {
                data[i] = data[i + 1];
            }
            if (data.Length > 0)
            {
                Array.Resize(ref data, data.Length - 1);
            }

        }

        public void ModelSingle(int value)
        {
            string ModelSingle = "";
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].ModelId == value)
                {
                    ModelSingle = $"Model ID: {data[i].ModelId} || Model's Name: {data[i].ModelName} || Brand ID: {data[i].BrandId1}";
                }
            }
            Console.WriteLine("#################Choosen Model##################");
            Console.WriteLine(ModelSingle);
        }

        public bool CheckModelName(string name)
        {
            name = name.ToLower().Trim();

            for (int i = 0; i < data.Length; i++)
            {
                if (data != null)
                {
                    if (data[i].ModelName.ToLower() == name)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void ModelEditBrandId(int value, int newBrand)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].ModelId == value)
                {
                    Console.WriteLine("Change the Model Brand: ");
                    data[i].BrandId1 = newBrand;
                    break;
                }
            }
        }

        public void ModelEditName(int value)
        {
            for (int i = 0; i < data.Length; i++)
            {
            EditAgain:
                string NewModel = ScannerManager.ReadString("Enter the New Model: ");
                CheckModelName(NewModel);
                if (CheckModelName(NewModel) == false)
                {
                    ScannerManager.PrintError("This Name is Already Used! ");
                    goto EditAgain;
                }
                else
                {
                    data[i].ModelName = data[i].ModelName.Replace(data[i].ModelName, NewModel);
                    break;
                }
            }
        }


        public Model[] GetAll()
        {
            return data;
        }

    }
}
