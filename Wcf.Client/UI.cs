using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wcf.Server;

namespace Wcf.Client
{
    delegate void make();
    class UI
    {
        #region MenuItem
        class MenuOption
        {
            static public int globalIndex = 0;
            public string Text { get; set; }
            public int OptionsIndex { get; set; }

            Action Action { get; set; }

            public override string ToString()
            {
                return Text + " -> " + OptionsIndex.ToString();
            }

            public void Make()
            {
                Action();
            }

            public MenuOption(string text, Action action)
            {
                OptionsIndex = globalIndex++;
                Text = text;
                Action = action;
            }
        }

        #endregion

        #region StaticVariableToServiceUI
        static bool contineu = true;
        #endregion

        #region MainMenuFun
      
        public void UiExit()
        {
            contineu = false;
        }

        public void UiSearchConnection()
        {
            Console.WriteLine("Podaj miasto startowe -portA");
            var portA = ChooseOptionString();
            Console.WriteLine("Podaj miasto koncowe - portB");
            var portB = ChooseOptionString();
            try
            {
                var result = proxy.SearchByLocation(portA, portB);
                ShowResult(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UiSearchConnectionWithDates()
        {
            Console.WriteLine("Podaj miasto startowe -portA");
            var portA = ChooseOptionString();
            Console.WriteLine("Podaj miasto koncowe - portB");
            var portB = ChooseOptionString();
            Console.WriteLine("Podaj data wylotu -portA");
            var departureDateTime = ChooseDateTime();
            Console.WriteLine("Podaj date przylotu -portB");
            var arrivaldateTime = ChooseDateTime();
            var result = proxy.SearchByLocationAndDate(portA, portB, departureDateTime, arrivaldateTime);
            ShowResult(result);
        }

        #endregion

        #region Properties
        string[] AllMainOptions = new string[] {
            "Wyszukaj tylko po mjescach",
            "Wyszukaj po mjescach i datach",
            "Koniec",
        };
        Action[] AllMainAction;
        List<MenuOption> mainOptions;
        IMessageService proxy;
        #endregion

        #region Constructor
        public UI(IMessageService _proxy)
        {
            AllMainAction = new Action[]
            {
                    UiSearchConnection,
                    UiSearchConnectionWithDates,
                    UiExit
            };
            proxy = _proxy;
            mainOptions = new List<MenuOption>();
            if (AllMainAction.Count() != AllMainAction.Count())
                throw new Exception("the size of AllMainOptions and AllMainAction is not equal");
            for (int i = 0; i < AllMainAction.Count(); i++)
                mainOptions.Add(new MenuOption(AllMainOptions[i], AllMainAction[i]));
        }
        #endregion

        #region ShowMainMenu
        public void ShowMainMenu()
        {
            mainOptions.ForEach((e) => Console.WriteLine(e));
        }
        #endregion

        #region ChooseOption
        public static int ChooseOptionInt()
        {
            try
            {
                return Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                return -1;
            }
        }
        public static string ChooseOptionString()
        {
            return Console.ReadLine();
        }

        public static DateTime ChooseDateTime()
        {
            Console.WriteLine("podaj rok");
            var year = ChooseOptionInt();
            Console.WriteLine("podaj miesiac");
            var mounth = ChooseOptionInt();
            Console.WriteLine("podaj dzien");
            var day = ChooseOptionInt();
            Console.WriteLine("podaj godzine");
            var hour = ChooseOptionInt();
            Console.WriteLine("podaj minute");
            var minute = ChooseOptionInt();
            try
            {
                return new DateTime(year, mounth, day, hour, minute, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("zle wprowadzona data");
            }
            Console.WriteLine("Wprowadz date jeszcze raz");
            return ChooseDateTime();
        }
        #endregion

        #region RunUI
        public void RunUI()
        {
            int opt;
            while (contineu)
            {
                ShowMainMenu();
                opt = ChooseOptionInt();
                if (opt < AllMainAction.Count() && opt >= 0)
                    mainOptions[opt].Make();
                else
                    Console.WriteLine("Zla opcja wpisz jeszcze raz:");
            }
        }
        #endregion

        #region ShowResult
        public void ShowResult(List<List<AirConnection>> result)
        {
            if ( result.Last().Last().portA == "ERROR")
            {
                Console.WriteLine(result.Last().Last().portB);
                return;
            }
            int numberOption = 0;
            int numberConnetion;
            result.ForEach(x =>
            {
                Console.WriteLine("Propozycja nr. " + ++numberOption);
                numberConnetion = 0;
                x.ForEach(connection =>
                {
                    Console.WriteLine($"    {++numberConnetion}. {connection}");
                });
            });
        }
        #endregion
    }
}
