using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Windshield.Models
{
    public abstract class Game
    {
        public static int id;
        public static String name; 
        public static String description;
        public static uint minPlayers;
        public static uint maxPlayers;
        public static String rules;
        public static uint timesPlayed;
        public static uint idBoard;
        public static String status;
        public static DateTime startDate;
        public static DateTime endDate;
        public static uint idOwner;
        //TODO: method and validation.
    }
}