﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Project.Assets.ControlClasses
{
    public class SavesControls
    {

        public SavesControls()
        {

        }

        public void CreateSave(string saveFileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string savesDirectory = Path.Combine(currentDirectory, "Saves");
            if (!Directory.Exists(savesDirectory))
            {
                Directory.CreateDirectory(savesDirectory);
            }
            string fullPath = Path.Combine(savesDirectory, saveFileName);
            FileStream fs = File.Create(fullPath);
            fs.Close();

            string initialData = "maxScore: 0; maxTime: 0;";
            WriteSaveData(saveFileName, initialData);
        }
        public string GetSaveFullPath(string saveFileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string savesDirectory = Path.Combine(currentDirectory, "Saves");
            string fullPath = Path.Combine(savesDirectory, saveFileName);
            return fullPath;
        }
        public bool CheckSaveExistence(string saveFileName)
        {
            string fullPath = GetSaveFullPath(saveFileName);
            if (File.Exists(fullPath)) return true;
            else return false;
        }
        public void DeleteSave(string saveFileName)
        {
            string fullPath = GetSaveFullPath(saveFileName);
            File.Delete(fullPath);
        }
        public string ReadSaveData(string saveFileName)
        {
            string fullPath = GetSaveFullPath(saveFileName);
            if (File.Exists(fullPath))
            {
                return File.ReadAllText(fullPath);
            }
            else
            {
                return null;
            }
        }
        public void WriteSaveData(string saveFileName, string saveData)
        {
            string fullPath = GetSaveFullPath(saveFileName);
            File.WriteAllText(fullPath, saveData);
        }
    }
}
