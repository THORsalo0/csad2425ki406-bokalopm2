using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APKS_Lab3
{

    /// <summary>
    /// Game config
    /// </summary>
    public class GameSettings
    {
        public string GameMode { get; set; }  // Мод гри, наприклад: "Player vs AI" або "AI vs AI"
    }

    public class Config
    {
        public GameSettings GameSettings { get; set; }
    }
}
