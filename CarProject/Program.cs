using CarProject;
using CarProject.Infrastructure;
using CarProject.Mangers;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace CarProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CarSystem";

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Welcome to the Car System :)");
            Console.ResetColor();

            CultureInfo ci = new CultureInfo("en-US");
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            ci.NumberFormat.NumberDecimalSeparator = ".";

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var brandMsr = new BrandManager();
            var modelMsr = new ModelManager();
            var carMsr = new CarManager();

        ReadMenu:
            PrintMenu();

            Menu menuNum = ScannerManager.ReadMenu("Choose an Operation in Menu: ");

            switch (menuNum)
            {
                #region BrandAdd
                case Menu.BrandAdd:
                    Console.Clear();
                CheckBrand:
                    string NameBrand = ScannerManager.ReadString("Enter The Name of Brand: ");
                    brandMsr.CheckBrandName(NameBrand);
                    if (brandMsr.CheckBrandName(NameBrand) == false)
                    {
                        ScannerManager.PrintError("This Name is Already Used!");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("To Try Again click <F1> || To Return Menu Click <F2>");
                        Console.ResetColor();

                        ConsoleKeyInfo click = Console.ReadKey();
                        if (click.Key == ConsoleKey.F1)
                        {
                            goto CheckBrand;
                        }
                        else
                        {
                            goto ReadMenu;
                        }
                    }
                    else
                    {
                        Brand b = new Brand();
                        b.Name = NameBrand;
                        brandMsr.Add(b);
                    }
                    goto case Menu.BrandAll;

                #endregion
                #region BrandEdit
                case Menu.BrandEdit:
                tryedit:
                    Console.Clear();
                    ShowAllBrand(brandMsr);

                    int value = ScannerManager.ReadInteger("Enter the ID of Brand: ");

                    var CheckBrandEdit = brandMsr.GetAll().FirstOrDefault(x => x.BrandId == value);
                    if (CheckBrandEdit == null)
                    {
                        ScannerManager.PrintError("It is False Id");

                    click:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                        Console.ResetColor();

                        ConsoleKeyInfo click = Console.ReadKey();
                        if (click.Key == ConsoleKey.F1)
                        {
                            goto tryedit;
                        }
                        else if (click.Key == ConsoleKey.F2)
                        {
                            goto ReadMenu;
                        }
                        else
                        {
                            goto click;
                        }
                    }

                    brandMsr.BrandEdit(value);

                    goto case Menu.BrandAll;

                #endregion
                #region BrandRemove
                case Menu.BrandRemove:
                    Console.Clear();
                    ShowAllBrand(brandMsr);
                    int id = ScannerManager.ReadInteger("Enter the ID of the Group you Want to Delete: ");

                    Brand b1 = brandMsr.GetAll().FirstOrDefault(item => item.BrandId == id);
                    brandMsr.BrandRemove(b1);
                    goto case Menu.BrandAll;

                #endregion
                #region BrandSingle
                case Menu.BrandSingle:
                trysingle:
                    Console.Clear();
                    ShowAllBrand(brandMsr);
                    int idvalue = ScannerManager.ReadInteger("Enter the chosen Brand: ");

                    var CheckBrandSingle = brandMsr.GetAll().FirstOrDefault(x => x.BrandId == idvalue);
                    if (CheckBrandSingle == null)
                    {
                        ScannerManager.PrintError("It is False Id");

                    click1:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                        Console.ResetColor();

                        ConsoleKeyInfo click = Console.ReadKey();
                        if (click.Key == ConsoleKey.F1)
                        {
                            goto trysingle;
                        }
                        else if (click.Key == ConsoleKey.F2)
                        {
                            goto ReadMenu;
                        }
                        else
                        {
                            goto click1;
                        }
                    }
                    Console.Clear();
                    brandMsr.BrandSingle(idvalue);
                    goto ReadMenu;

                #endregion
                #region BrandAll
                case Menu.BrandAll:
                    Console.Clear();
                    ShowAllBrand(brandMsr);

                    goto ReadMenu;
                #endregion
                #region ModelAdd
                case Menu.ModelAdd:
                    Console.Clear();

                CheckModel:
                    string NameModel = ScannerManager.ReadString("Enter The Name of Model: ");

                    modelMsr.CheckModelName(NameModel);
                    if (modelMsr.CheckModelName(NameModel) == false)
                    {
                        ScannerManager.PrintError("This Name is Already Used!");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("To Try Again click <F1> || To Return Menu Click <F2>");
                        Console.ResetColor();

                        ConsoleKeyInfo click = Console.ReadKey();
                        if (click.Key == ConsoleKey.F1)
                        {
                            goto CheckModel;
                        }
                        else
                        {
                            goto ReadMenu;
                        }
                    }
                    else
                    {
                        Model m = new Model();
                        m.ModelName = NameModel;

                    trymodeladd:
                        ShowAllBrand(brandMsr);
                        m.BrandId1 = ScannerManager.ReadInteger("Enter the ID of Brand: ");

                        var CheckModelAdd = brandMsr.GetAll().FirstOrDefault(x => x.BrandId == m.BrandId1);
                        if (CheckModelAdd == null)
                        {
                            ScannerManager.PrintError("It is False Id");

                        click2:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                            Console.ResetColor();

                            ConsoleKeyInfo click = Console.ReadKey();
                            if (click.Key == ConsoleKey.F1)
                            {
                                goto trymodeladd;
                            }
                            else if (click.Key == ConsoleKey.F2)
                            {
                                goto ReadMenu;
                            }
                            else
                            {
                                goto click2;
                            }
                        }

                        modelMsr.Add(m);

                    }
                    goto case Menu.ModelAll;
                #endregion
                #region ModelEdit
                case Menu.ModelEdit:
                trymodeledit:
                    Console.Clear();
                    ShowAllModel(modelMsr);
                    Console.WriteLine("Change for Model's Name ==> 1 || Change for Brand's ID ==> 2");
                    bool success = int.TryParse(Console.ReadLine(), out int menuNumber);
                    if (success && menuNumber == 1)
                    {
                        int value1 = ScannerManager.ReadInteger("Enter the ID of chosen Model: ");

                        var CheckModelEdit = modelMsr.GetAll().FirstOrDefault(x => x.ModelId == value1);
                        if (CheckModelEdit == null)
                        {
                            ScannerManager.PrintError("It is False Id");

                        click3:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                            Console.ResetColor();

                            ConsoleKeyInfo click = Console.ReadKey();
                            if (click.Key == ConsoleKey.F1)
                            {
                                goto trymodeledit;
                            }
                            else if (click.Key == ConsoleKey.F2)
                            {
                                goto ReadMenu;
                            }
                            else
                            {
                                goto click3;
                            }
                        }
                        modelMsr.ModelEditName(value1);
                    }
                    else if (success && menuNumber == 2)
                    {
                        int value1 = ScannerManager.ReadInteger("Enter the ID of chosen Model: ");

                        var CheckModelEdit = modelMsr.GetAll().FirstOrDefault(x => x.ModelId == value1);
                        if (CheckModelEdit == null)
                        {
                            ScannerManager.PrintError("It is False Id");

                        click4:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                            Console.ResetColor();

                            ConsoleKeyInfo click = Console.ReadKey();
                            if (click.Key == ConsoleKey.F1)
                            {
                                goto trymodeledit;
                            }
                            else if (click.Key == ConsoleKey.F2)
                            {
                                goto ReadMenu;
                            }
                            else
                            {
                                goto click4;
                            }
                        }

                        ShowAllBrand(brandMsr);
                        Console.WriteLine("#########################################");

                        int newBrand = ScannerManager.ReadInteger("Enter The New Brand: ");

                        var CheckModelEdit1 = brandMsr.GetAll().FirstOrDefault(x => x.BrandId == newBrand);
                        if (CheckModelEdit1 == null)
                        {
                            ScannerManager.PrintError("It is False Id");

                        click16:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                            Console.ResetColor();

                            ConsoleKeyInfo click = Console.ReadKey();
                            if (click.Key == ConsoleKey.F1)
                            {
                                goto trymodelsingle;
                            }
                            else if (click.Key == ConsoleKey.F2)
                            {
                                goto ReadMenu;
                            }
                            else
                            {
                                goto click16;
                            }
                        }

                        modelMsr.ModelEditBrandId(value1, newBrand);
                    }

                    goto case Menu.ModelAll;

                #endregion
                #region ModelRemove
                case Menu.ModelRemove:
                    Console.Clear();
                    ShowAllModel(modelMsr);
                    int id1 = ScannerManager.ReadInteger("Enter the ID of the Group you Want to Delete: ");

                    Model m1 = modelMsr.GetAll().FirstOrDefault(item => item.ModelId == id1);
                    modelMsr.ModelRemove(m1);
                    goto case Menu.ModelAll;

                #endregion
                #region ModelSingle
                case Menu.ModelSingle:
                trymodelsingle:
                    Console.Clear();
                    ShowAllModel(modelMsr);
                    int idvalue1 = ScannerManager.ReadInteger("Enter the chosen Model: ");

                    var CheckModelSingle = modelMsr.GetAll().FirstOrDefault(x => x.ModelId == idvalue1);
                    if (CheckModelSingle == null)
                    {
                        ScannerManager.PrintError("It is False Id");

                    click5:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                        Console.ResetColor();

                        ConsoleKeyInfo click = Console.ReadKey();
                        if (click.Key == ConsoleKey.F1)
                        {
                            goto trymodelsingle;
                        }
                        else if (click.Key == ConsoleKey.F2)
                        {
                            goto ReadMenu;
                        }
                        else
                        {
                            goto click5;
                        }
                    }
                    Console.Clear();

                    modelMsr.ModelSingle(idvalue1);
                    goto ReadMenu;

                #endregion
                #region ModelAll
                case Menu.ModelAll:
                    Console.Clear();
                    ShowAllModel(modelMsr);

                    goto ReadMenu;
                #endregion
                #region CarAdd
                case Menu.CarAdd:
                    Console.Clear();
                    Car c = new Car();
                    c.Year = ScannerManager.ReadDate("Enter the Car's Year: ");
                    c.Price = ScannerManager.ReadDouble("Enter the Car's Price [$]: ");
                    c.Color = ScannerManager.ReadString("Enter the Car's Color: ");
                    c.Engine = ScannerManager.ReadDouble("Enther the Car's Engine: ");

                    PrintFuelMenu();
                    FuelType menuNum4 = ScannerManager.FuelType("Select the type a fuel: ");

                    switch (menuNum4)
                    {
                        case FuelType.Gasoline:
                            c.FuelType = nameof(FuelType.Gasoline);
                            break;
                        case FuelType.Diesel:
                            c.FuelType = nameof(FuelType.Diesel);
                            break;
                        case FuelType.Hybrid:
                            c.FuelType = nameof(FuelType.Hybrid);
                            break;
                        case FuelType.Electro:
                            c.FuelType = nameof(FuelType.Electro);
                            break;
                        case FuelType.Gas:
                            c.FuelType = nameof(FuelType.Gas);
                            break;
                        default:
                            break;
                    }

                trycaradd:
                    ShowAllModel(modelMsr);
                    c.ModelId1 = ScannerManager.ReadInteger("Enter the Model's ID: ");

                    var CheckCarAdd = modelMsr.GetAll().FirstOrDefault(x => x.ModelId == c.ModelId1);
                    if (CheckCarAdd == null)
                    {
                        ScannerManager.PrintError("It is False Id");

                    click6:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                        Console.ResetColor();

                        ConsoleKeyInfo click = Console.ReadKey();
                        if (click.Key == ConsoleKey.F1)
                        {
                            goto trycaradd;
                        }
                        else if (click.Key == ConsoleKey.F2)
                        {
                            goto ReadMenu;
                        }
                        else
                        {
                            goto click6;
                        }
                    }

                    carMsr.Add(c);

                    goto case Menu.CarAll;

                #endregion
                #region CarEdit
                case Menu.CarEdit:
                TryCarEdit:
                    Console.Clear();
                    ShowAllCar(carMsr);
                    Console.WriteLine("Change for Model's ID ==> 1 || Change for Year ==> 2 || " +
                        "Change for Price ==> 3 || Change for Color ==> 4 || " +
                        "Change for Engine ==> 5 || Change for FuelType ==> 6 ");
                    bool success2 = int.TryParse(Console.ReadLine(), out int menuNumber2);
                    if (success2 && menuNumber2 == 1)
                    {
                        int value1 = ScannerManager.ReadInteger("Enter the ID of choosen Car: ");

                        var CheckCarEdit = carMsr.GetAll().FirstOrDefault(x => x.CarId == value1);
                        if (CheckCarEdit == null)
                        {
                            ScannerManager.PrintError("It is False Id");

                        click8:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                            Console.ResetColor();

                            ConsoleKeyInfo click = Console.ReadKey();
                            if (click.Key == ConsoleKey.F1)
                            {
                                goto TryCarEdit;
                            }
                            else if (click.Key == ConsoleKey.F2)
                            {
                                goto ReadMenu;
                            }
                            else
                            {
                                goto click8;
                            }
                        }

                        ShowAllModel(modelMsr);
                        Console.WriteLine("###############################################");

                        int newmodelid = ScannerManager.ReadInteger("Enter the New Model ID: ");

                        var CheckCarEditNewModel = modelMsr.GetAll().FirstOrDefault(x => x.ModelId == newmodelid);
                        if (CheckCarEditNewModel == null)
                        {
                            ScannerManager.PrintError("It is False Id");

                        click8:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                            Console.ResetColor();

                            ConsoleKeyInfo click = Console.ReadKey();
                            if (click.Key == ConsoleKey.F1)
                            {
                                goto TryCarEdit;
                            }
                            else if (click.Key == ConsoleKey.F2)
                            {
                                goto ReadMenu;
                            }
                            else
                            {
                                goto click8;
                            }
                        }

                        carMsr.CarEditModelId(value1, newmodelid);
                    }
                    else if (success2 && menuNumber2 == 2)
                    {
                        int value1 = ScannerManager.ReadInteger("Enter the ID of choosen Car: ");

                        var CheckCarEdit = carMsr.GetAll().FirstOrDefault(x => x.CarId == value1);
                        if (CheckCarEdit == null)
                        {
                            ScannerManager.PrintError("It is False Id");

                        click9:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                            Console.ResetColor();

                            ConsoleKeyInfo click = Console.ReadKey();
                            if (click.Key == ConsoleKey.F1)
                            {
                                goto TryCarEdit;
                            }
                            else if (click.Key == ConsoleKey.F2)
                            {
                                goto ReadMenu;
                            }
                            else
                            {
                                goto click9;
                            }
                        }

                        carMsr.CarEditYear(value1);
                    }
                    else if (success2 && menuNumber2 == 3)
                    {
                        int value1 = ScannerManager.ReadInteger("Enter the ID of choosen Car: ");

                        var CheckCarEdit = carMsr.GetAll().FirstOrDefault(x => x.CarId == value1);
                        if (CheckCarEdit == null)
                        {
                            ScannerManager.PrintError("It is False Id");

                        click10:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                            Console.ResetColor();

                            ConsoleKeyInfo click = Console.ReadKey();
                            if (click.Key == ConsoleKey.F1)
                            {
                                goto TryCarEdit;
                            }
                            else if (click.Key == ConsoleKey.F2)
                            {
                                goto ReadMenu;
                            }
                            else
                            {
                                goto click10;
                            }
                        }

                        carMsr.CarEditPrice(value1);
                    }
                    else if (success2 && menuNumber2 == 4)
                    {
                        int value1 = ScannerManager.ReadInteger("Enter the ID of choosen Car: ");

                        var CheckCarEdit = carMsr.GetAll().FirstOrDefault(x => x.CarId == value1);
                        if (CheckCarEdit == null)
                        {
                            ScannerManager.PrintError("It is False Id");

                        click11:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                            Console.ResetColor();

                            ConsoleKeyInfo click = Console.ReadKey();
                            if (click.Key == ConsoleKey.F1)
                            {
                                goto TryCarEdit;
                            }
                            else if (click.Key == ConsoleKey.F2)
                            {
                                goto ReadMenu;
                            }
                            else
                            {
                                goto click11;
                            }
                        }

                        carMsr.CarEditColor(value1);
                    }
                    else if (success2 && menuNumber2 == 5)
                    {
                        int value1 = ScannerManager.ReadInteger("Enter the ID of choosen Car: ");

                        var CheckCarEdit = carMsr.GetAll().FirstOrDefault(x => x.CarId == value1);
                        if (CheckCarEdit == null)
                        {
                            ScannerManager.PrintError("It is False Id");

                        click12:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                            Console.ResetColor();

                            ConsoleKeyInfo click = Console.ReadKey();
                            if (click.Key == ConsoleKey.F1)
                            {
                                goto TryCarEdit;
                            }
                            else if (click.Key == ConsoleKey.F2)
                            {
                                goto ReadMenu;
                            }
                            else
                            {
                                goto click12;
                            }
                        }

                        carMsr.CarEditEngine(value1);
                    }
                    else if (success2 && menuNumber2 == 6)
                    {
                        int value1 = ScannerManager.ReadInteger("Enter the ID of choosen Car: ");

                        var CheckCarEdit = carMsr.GetAll().FirstOrDefault(x => x.CarId == value1);
                        if (CheckCarEdit == null)
                        {
                            ScannerManager.PrintError("It is False Id");

                        click13:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                            Console.ResetColor();

                            ConsoleKeyInfo click = Console.ReadKey();
                            if (click.Key == ConsoleKey.F1)
                            {
                                goto TryCarEdit;
                            }
                            else if (click.Key == ConsoleKey.F2)
                            {
                                goto ReadMenu;
                            }
                            else
                            {
                                goto click13;
                            }
                        }

                        Console.Clear();
                        PrintFuelMenu();
                        carMsr.CarEditFuelType(value1);
                    }

                    goto case Menu.CarAll;

                #endregion
                #region CarRemove
                case Menu.CarRemove:
                TryCarRemove:
                    Console.Clear();
                    ShowAllCar(carMsr);
                    int id2 = ScannerManager.ReadInteger("Enter the ID of the Car you Want to Delete: ");

                    var CheckCarRemove = carMsr.GetAll().FirstOrDefault(x => x.CarId == id2);
                    if (CheckCarRemove == null)
                    {
                        ScannerManager.PrintError("It is False Id");

                    click14:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                        Console.ResetColor();

                        ConsoleKeyInfo click = Console.ReadKey();
                        if (click.Key == ConsoleKey.F1)
                        {
                            goto TryCarRemove;
                        }
                        else if (click.Key == ConsoleKey.F2)
                        {
                            goto ReadMenu;
                        }
                        else
                        {
                            goto click14;
                        }
                    }

                    Car c1 = carMsr.GetAll().FirstOrDefault(item => item.CarId == id2);
                    carMsr.CarRemove(c1);
                    goto case Menu.CarAll;

                #endregion
                #region CarSingle
                case Menu.CarSingle:
                TryCarSingle:
                    Console.Clear();
                    ShowAllCar(carMsr);
                    int idvalue2 = ScannerManager.ReadInteger("Enter the choosen Car: ");

                    var CheckCarSingle = carMsr.GetAll().FirstOrDefault(x => x.CarId == idvalue2);
                    if (CheckCarSingle == null)
                    {
                        ScannerManager.PrintError("It is False Id");

                    click15:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("To Try again click <F1> || To Return Menu click <F2> ");
                        Console.ResetColor();

                        ConsoleKeyInfo click = Console.ReadKey();
                        if (click.Key == ConsoleKey.F1)
                        {
                            goto TryCarSingle;
                        }
                        else if (click.Key == ConsoleKey.F2)
                        {
                            goto ReadMenu;
                        }
                        else
                        {
                            goto click15;
                        }
                    }
                    Console.Clear();

                    carMsr.CarSingle(idvalue2);
                    goto ReadMenu;

                #endregion
                #region CarAll
                case Menu.CarAll:
                    Console.Clear();
                    ShowAllCar(carMsr);

                    goto ReadMenu;
                #endregion
                #region Menu All
                case Menu.All:
                    Console.Clear();
                    ShowAllBrand(brandMsr);
                    ShowAllModel(modelMsr);
                    ShowAllCar(carMsr);

                    goto ReadMenu;
                #endregion
                #region Menu Exit
                case Menu.Exit:
                    break;
                #endregion
                #region Default
                default:
                    break;
                    #endregion

            }

        }

        static void PrintMenu()
        {
            Console.WriteLine(new string('-', Console.WindowWidth));

            foreach (var item in Enum.GetNames(typeof(Menu)))
            {
                Menu m = (Menu)Enum.Parse(typeof(Menu), item);

                Console.WriteLine($"{((byte)m).ToString().PadLeft(2)}. {item}");
            }
            Console.WriteLine($"{new string('-', Console.WindowWidth)}\n");
        }

        static void PrintFuelMenu()
        {
            Console.WriteLine(new string('-', Console.WindowWidth));

            foreach (var item in Enum.GetNames(typeof(FuelType)))
            {
                FuelType m = (FuelType)Enum.Parse(typeof(FuelType), item);

                Console.WriteLine($"{((byte)m).ToString().PadLeft(2)}. {item}");
            }
            Console.WriteLine($"{new string('-', Console.WindowWidth)}\n");
        }

        static void ShowAllBrand(BrandManager brandManager)
        {
            Console.WriteLine("**********BRANDS**********");
            foreach (var item in brandManager.GetAll())
            {
                Console.WriteLine(item);
            }
        }

        static void ShowAllCar(CarManager carManager)
        {
            Console.WriteLine("**********CARS**********");
            foreach (var item in carManager.GetAll())
            {
                Console.WriteLine(item);
            }
        }

        static void ShowAllModel(ModelManager modelManager)
        {
            Console.WriteLine("**********MODELS**********");
            foreach (var item in modelManager.GetAll())
            {
                Console.WriteLine(item);
            }
        }
    }
}