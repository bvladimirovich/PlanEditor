using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PlanEditor.Helpers.IO
{
    /// <summary>
    /// Класс сохранения во временный файл-проект
    /// </summary>
    public class SaveFile
    {
        public static void Save(string fileName, Entities.Building building)
        {
            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                try
                {
                    // Сохранение в бинарном формате
                    var bf = new BinaryFormatter();
                    building.PrepareForExport();
                    bf.Serialize(fs, building);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    // Запист ошибок в log файл
                    PELogger.GetLogger.WriteLn(ex.Message); 
                }
            }
        }
    }
}
