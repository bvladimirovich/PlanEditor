using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using PlanEditor.Helpers;

namespace PlanEditor.Entities
{
    /// <summary>
    /// Класс, инициализирующий помещение и его атрибуты.
    /// </summary>
    [Serializable]
    public class Place : Entity
    {
        // Конструктор класса помещения
        public Place()
        {
            MainType = -1;
            SubType = -1;
            Ppl = 0;
            MaxPeople = 0;
            Name = "";
            IsMovable = true;
            ScanarioID = -1;
            FireType = 0;
        }

        /// <summary>
        /// Количество узлов сетки, принадлежащих помещению 
        /// </summary>
        public int CountNodes { get; set; }	
        /// <summary>
        /// Количество людей в помещении (фактическое наличие)
        /// </summary>
        public int Ppl { get; set; }
        /// <summary>
        /// Максимальное количество людей в помещении
        /// </summary>
        public int MaxPeople { get; set; }       
        /// <summary>
        /// Название помещения 
        /// </summary>
        public string Name { get; set; }   
        /// <summary>
        /// высота помещения, м   
        /// </summary>
        public double Height { get; set; }	    
        /// <summary>
        /// высота  площадки,  на  которой  находятся  люди, над полом, м
        /// </summary>
        public double SHeigthRoom { get; set; } 
        /// <summary>
        /// разность высот пола, равная нулю при горизонтальном его расположении, м	
        /// </summary>
        public double DifHRoom { get; set; }
        /// <summary>
        /// Ширина пути эвакуации
        /// </summary>
        public double EvacWide { get; set; }
        /// <summary>
        /// Тип помещения
        /// </summary>
        public int MainType { get; set; }
        /// <summary>
        /// Подтип помещения
        /// </summary>
        public int SubType { get; set; }        
        /// <summary>
        /// Флаг фиксации помещения. True, если помещение без дверей
        /// </summary>
        public bool IsMovable { get; set; }
        /// <summary>
        /// 0 - обычное помещение
        /// 1 - очаг возгорания в помещениия
        /// 2 - помещение заблокированно
        /// </summary>
        public int FireType { get; set; }
        /// <summary>
        /// Идентификатор сценариев
        /// </summary>
        public int ScanarioID { get; set; }

        /// <summary>
        /// Метод для отладки получаемых переменных
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            
            sb.Append(CountNodes + " ");
            sb.Append(Ppl + " ");
            sb.Append(MaxPeople + " ");
            sb.Append(Name + " ");          
            sb.Append(Wide + " ");
            sb.Append(Height + " ");
            sb.Append(Length + " ");
            sb.Append(SHeigthRoom + " ");
            sb.Append(DifHRoom + " ");
            sb.Append(MainType + " ");
            sb.Append(SubType + " ");
            
            return sb.ToString();
        }

        /// <summary>
        /// Метод, добавляющий высоту препятствия в общий список препятствий
        /// </summary>
        public void HideObstacles()
        {
            if (Obstacles == null) return;
            foreach (var obstacle in Obstacles)
            {
                obstacle.Hide();
            }
        }
        /// <summary>
        /// Метод, показывающий препятсвия на плане
        /// </summary>
        public void ShowObstacles()
        {
            if (Obstacles == null) return;
            foreach (var obstacle in Obstacles)
            {
                obstacle.Show();
            }
        }
        /// <summary>
        /// Метод, определяющий список лини, для последущего прикрепления дверей
        /// </summary>
        public List<Line> Lines 
        { 
            get 
            {
                var lst = new List<Line>();
                var x = PointsX;
                var y = PointsY;
                for (int i = 1, j = 1; i < x.Count && j < y.Count; ++i, ++j)
                {
                    var l = new Line { X1 = x[i - 1], Y1 = y[j - 1], X2 = x[i], Y2 = y[j] };
                    lst.Add(l);                
                }
                
                return lst;
            }         
        }
        /// <summary>
        /// Список выходов
        /// </summary>
        [NonSerialized]
        public List<Portal> Exits = new List<Portal>();
        /// <summary>
        /// Флаг определения пересечения помещений.
        /// True - имеется пересечение
        /// False - персечения отсутствуют
        /// </summary>
        [NonSerialized]
        private bool _isCollide;

        public bool IsCollide
        {
            get { return _isCollide; }
        } 
        /// <summary>
        /// Метод подсветки помещения, имеющее пересечение
        /// </summary>
        public void Collide()
        {
            UI.Fill = Colours.Red;
            _isCollide = true;
        }
        /// <summary>
        /// Метод установки начальных значений подсветки помещений
        /// </summary>
        public void NonCollide()
        {
            switch (Type)
            {
                case EntityType.Place:
                    UI.Fill = Colours.Indigo;
                    break;
                case EntityType.Halfway:
                    UI.Fill = Colours.Green;
                    break;
                case EntityType.Stairway:
                    UI.Fill = Colours.Violet;
                    break;
                case EntityType.Portal:
                    UI.Fill = Colours.LightGray;
                    break;
            }
            _isCollide = false;
        }
        /// <summary>
        /// Список непроходимых препятсвий
        /// </summary>
        public List<Obstacle> Obstacles = new List<Obstacle>();
    }
}
