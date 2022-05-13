using System;
using CarProject.Mangers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarProject.Mangers
{
    internal class BrandManager
    {
        Brand[] data = new Brand[0];

        public void Add(Brand entity)
        {
            int len = data.Length;
            Array.Resize(ref data, len + 1);
            data[len] = entity;
        }


        public void BrandRemove(Brand entity)
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


        public void BrandSingle(int value)
        {
            string singleBrand = "";
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i].BrandId == value)
                {
                    singleBrand = $"Brand ID: {data[i].BrandId} || Brand's Name: {data[i].Name}";
                }
            }
            Console.WriteLine("#################Choosen Brand##################");
            Console.WriteLine(singleBrand);
        }

        public bool CheckBrandName(string name)
        {
            name = name.ToLower().Trim();

            for (int i = 0; i < data.Length; i++)
            {
                if (data != null)
                {
                    if (data[i].Name.ToLower() == name)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void BrandEdit(int value)
        {
            for (int i = 0; i < data.Length; i++)
            {
                Console.WriteLine("Change The Brand Name: ");
            EditAgain:
                string NewBrand = ScannerManager.ReadString("Enter the New Brand: ");
                CheckBrandName(NewBrand);
                if (CheckBrandName(NewBrand) == false)
                {
                    ScannerManager.PrintError("This Name is Already Used! ");
                    goto EditAgain;
                }
                else
                {
                    data[i].Name = data[i].Name.Replace(data[i].Name, NewBrand);
                    break;
                }
            }
        }

        public Brand[] GetAll()
        {
            return data;
        }
    }
}

