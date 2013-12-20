using System;
using System.Collections.Generic;
using System.Text;

namespace PlanEditor.Entities
{
    [Serializable]  // Сериализация объекта для сохнанения в бинарном виде
    public class Building
    {
        public static int CurID = 0;
        // Users data
        /// <summary>
        /// Наименование организации
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Адрес объекта
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Функциональное назначение здания
        /// </summary>
        public int Functionality { get; set; }
        /// <summary>
        /// Количесво людей в здании
        /// </summary>
        public int People { get; set; }
        /// <summary>
        /// Максимальное количество людей в здании
        /// </summary>
        public int MaxPeople { get; set; }
        public int FireSafetySys { get; set; } // 1 - 3
        public int FireSignal { get; set; } // 1 - 3
        public int Notification { get; set; } // 1 - 3
        public int AntiFog { get; set; } // 1 - 3
        public int Insurance { get; set; }
        /// <summary>
        /// Количество этажей
        /// </summary>
        public int Stages { get; set; }         
        public string Addition { get; set; }   
        /// <summary>
        /// Размеры проекции здания по Х, метры 
        /// </summary>
        public double Lx { get; set; }
        /// <summary>
        /// Размеры проекции здания по Y, метры 
        /// </summary>
        public double Ly { get; set; }
        /// <summary>
        /// Высота этажа здания
        /// </summary>
        public double HeightStage { get; set; } 

        // Computing data
        public int Row { get; set; }
        public int Col { get; set; }
        public double xMax { get; set; }
        public double yMax { get; set; }
        public int NumNodes { get; set; }    // Характерное число узлов (входной параметр)    
        public int PplCell { get; set; }    // Расчет Количество ячеек сетки в здании, где могут находиться люди 

        /// <summary>
        /// Метод, возвращающий количество помещений в здании
        /// </summary>
        public int GetNumPlaces
        {
            get 
            {
                int num = 0;

                foreach (var v in Places)
                    num += v.Count;

                return num;
            }
        }
        /// <summary>
        /// Метод, возвращающий количество дверей
        /// </summary>
        public int GetNumPortal
        {
            get 
            {
                int num = 0;
                
                foreach (var v in Portals)
                    num += v.Count;

                return num;
            }
        }
        /// <summary>
        /// Метод, возвращающи количество шахт в здании
        /// </summary>
        public int GetNumMines
        {
            get 
            {
                return Stairways.Count;
            }
        }

        /// <summary>
        /// Список помещений
        /// </summary>
        public List<List<Place>> Places = new List<List<Place>>();
        /// <summary>
        /// Список лестниц
        /// </summary>
        public List<Stairway> Stairways = new List<Stairway>();  
        /// <summary>
        /// Список порталов (дверей)
        /// </summary>
        public List<List<Portal>> Portals = new List<List<Portal>>();    
        /// <summary>
        /// Список QR меток здания
        /// </summary>
        public List<QRPointer> Pointer = new List<QRPointer>();

        /// <summary>
        /// Метод для отладки получаемых переменных
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(Name + " ");
            sb.Append(Address + " ");
            sb.Append(Functionality + " ");
            sb.Append(People + " ");
            sb.Append(MaxPeople + " ");
            sb.Append(FireSafetySys + " ");
            sb.Append(FireSignal + " "); 
            sb.Append(Notification + " "); 
            sb.Append(AntiFog + " ");
            sb.Append(Insurance + " ");
            sb.Append(Stages + " ");
            sb.Append(NumNodes + " ");
            sb.Append(Row + " ");
            sb.Append(Col + " ");
            sb.Append(xMax + " ");
            sb.Append(yMax + " ");
            sb.Append(Lx + " ");
            sb.Append(Ly + " ");
            sb.Append(HeightStage + " ");
            sb.Append(Addition + " ");
            sb.Append(Stages + " ");
            sb.Append(PplCell);

            return sb.ToString();
        }

        /// <summary>
        /// Метод приведения введенных данных.
        /// Вызывается перед сохранение проекта. Содержит координаты и атрибуты объектов.
        /// </summary>
        public void PrepareForExport()
        {
            for (var i = 0; i < Stages; ++i)
            {
                if (Places.Count > i)
                {
                    foreach (var v in Places[i])
                    {
                        v.PrepareForSave();
                        if (v.Obstacles == null) continue;

                        foreach (var obstacle in v.Obstacles) obstacle.PrepareForSave();
                    }
                }

                if (Portals.Count <= i) continue;
                foreach (var v in Portals[i])
                    v.PrepareForSave();
            }

            foreach (var v in Stairways)
                v.PrepareForSave();
        }

        /// <summary>
        /// Метод очстки списков дверей, лестниц и помещений
        /// </summary>
        public void RemoveAll()
        {
            if (Places != null) Places.Clear(); 
            if (Stairways != null) Stairways.Clear();
            if (Portals != null) Portals .Clear();
        }
    }
}
